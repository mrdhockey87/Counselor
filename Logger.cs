using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;

namespace CounselQuickPlatinum
{

    internal static class Logger
    {
        internal enum LogVerbosity
        {
            Application = 0,
            Error = 1,
            CriticalError = 2
        }
        
        static StreamWriter log;
        static LogVerbosity verbosity = Logger.LogVerbosity.Application;
        static string fullFilepath;

        static byte[] IV;

        static public string CurrentFunctionToString()
        {
            StringBuilder sb = new StringBuilder(256);
            var frames = new System.Diagnostics.StackTrace().GetFrames();
            var currFrame = frames[2];
            var method = currFrame.GetMethod();
            var lineNumber = currFrame.GetFileLineNumber();
            sb.AppendLine(string.Format("{0}:{1} [{2}]\n", method.ReflectedType != null ? method.ReflectedType.Name : string.Empty, method.Name, lineNumber));
            return sb.ToString();
        }

        static Logger()
        {
            string logDirectoryPath = Utilities.GetCQPUserDataDirectory() + @"\log";
            
            DirectoryInfo logDirectoryInfo = new DirectoryInfo(logDirectoryPath);
            
            if (logDirectoryInfo.Exists == false)
                Directory.CreateDirectory(logDirectoryPath);

            string filename = Guid.NewGuid().ToString("N").Substring(0, 8);
            fullFilepath = logDirectoryPath + @"\~" + filename + ".log";
            
            log = new StreamWriter(fullFilepath, false, Encoding.UTF8);
            IV = Encryption.GenerateIV();
            log.WriteLine(Encryption.GetString(IV));

            log.AutoFlush = true;
            
            Log("Initialized logger");
            Log(DateTime.Now.ToString("~yyyyMMdd-HH-mm-ss"));
        }

        internal static LogVerbosity Verbosity
        {
            set { verbosity = value; }
            get { return verbosity; }
        }


        private static void WriteLine(string message)
        {
            byte[] encrypted = Encryption.EncryptString(message, IV);
            string encryptedBase64 = Convert.ToBase64String(encrypted);
            try
            {
                log.WriteLine(encryptedBase64);
                log.Flush();
            }
            catch(Exception)
            {
                string err = "Try to write to a closed file";
            }
        }

        private static void Log(string message)
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(1);

            string className = stackFrame.GetFileName();
            string methodName = stackFrame.GetMethod().Name;
            string lineNumber = stackFrame.GetFileLineNumber().ToString();

            System.Threading.Thread currentThread = System.Threading.Thread.CurrentThread;

            WriteLine(className + "." + methodName + ":" + lineNumber + " threadID:" + currentThread.ManagedThreadId +"\n" + DateTime.Now + " - " + message + "\n\n");
        }


        private static void Log()
        {
            Log("");
        }


        private static void Log(Exception ex)
        {
            if (ex == null)
                return;

            WriteLine(" ********* EXCEPTION ********* ");

            Log("");
            
            WriteLine(" ***** TYPE: " + ex.GetType().ToString());
            WriteLine(" ***** MESSAGE : " + ex.Message);
            WriteLine(" ***** STACK TRACE: " + ex.StackTrace);
            WriteLine(" ***** TARGET SITE: " + ex.TargetSite.ToString());
            WriteLine(" ***** SOURCE: " + ex.Source);
            WriteLine(" ***** DATA: " + ex.Data.ToString());


            if (ex.InnerException != null)
            {
                WriteLine(" **** LOGGING INNER EXCEPTION: " + ex.InnerException.GetType().ToString());
                Log(ex.InnerException);
                WriteLine(" **** END INNER EXCEPTION: " + ex.InnerException.GetType().ToString());
            }

            log.Flush();
        }

        internal static void Trace()
        {
            if (verbosity > LogVerbosity.Application)
                return;

            Log("");
        }

        internal static void Trace(string message)
        {
            if (verbosity > LogVerbosity.Application)
                return;

            Log(message);
        }

        internal static void Trace(string message, Exception ex)
        {
            if (verbosity > LogVerbosity.Application)
                return;

            Log(message);
            Log(ex);
        }

        internal static void Frame()
        {
            Log(CurrentFunctionToString());
        }

        internal static void Error(string message)
        {
            if (verbosity > LogVerbosity.Error)
                return;

            Log(" \n\n\n\n ******** ERROR ********");
            Log(message);
            Log("\n\n\n\n");
        }

        internal static void Error(Exception ex)
        {
            if (verbosity > LogVerbosity.Error)
                return;

            Log(" \n\n\n\n ******** ERROR ********");
            Log(ex);
            Log("\n\n\n\n");
        }

        internal static void Error(string message, Exception ex)
        {
            if (verbosity > LogVerbosity.Error)
                return;

            Log(" \n\n\n\n ******** ERROR ********");
            Log(message);
            Log(ex);
            Log("\n\n\n\n");
        }

        internal static void CriticalError(string message)
        {
            Log(" \n\n\n\n ******** CRITICAL ERROR ********");
            Log(message);
            Log("\n\n\n\n");
        }

        internal static void CriticalError(Exception ex)
        {
            Log(" \n\n\n\n ******** CRITICAL ERROR ********");
            Log(ex);
            Log("\n\n\n\n");
        }

        internal static void CriticalError(string message, Exception ex)
        {
            Log(" \n\n\n\n ******** CRITICAL ERROR ********");
            Log(message);
            Log(ex);
            Log("\n\n\n\n");
        }

        internal static void Close()
        {
            if (log != null)
            {
                log.Flush();
                log.Close();
            }
        }
        private static JObject ErrorFIle()
        {
            //Get the error file as an array the using string builder append it together into one long line of a stringbuilder mdail 9-26-19
            string[] lines = File.ReadAllLines(fullFilepath);
            StringBuilder logBuilder = new StringBuilder();
            foreach (string line in lines) {
                logBuilder.AppendLine(line);
            }
            //Make a Json object with all of the data needed adding the string builder string as the last property mdail 9-26-19
            JObject errFile = new JObject(
                new JProperty("app", "Counselor"),
                new JProperty("serial", SettingsModel.GetProgramSerialNumber()),
                new JProperty("driveserial", SettingsModel.GetDriveSerialNumber()),
                new JProperty("username", Environment.UserName),
                new JProperty("error", logBuilder.ToString()));
            return errFile;
        }
        //Modified version of send crash report method to send the larger error log files the old version couldn't send mdail 9-26-19
        internal static bool SendCrashReport()
        {
            //Used to check to see if ReasonPhrase: 'OK' was returned at the end mdail 9-26-19
            bool ok = false;
            //I moved where it closed the report mdail 10-23-19
            log.Close();
            JObject errFIle = ErrorFIle();
            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            using (HttpClient client = new HttpClient())
            {
                Uri errorUri = new Uri("http://api.mentorenterprisesinc.com/report_error/");
                using (var request = new HttpRequestMessage(HttpMethod.Post, errorUri))
                {
                    using (var stringContent = new StringContent(errFIle.ToString(), Encoding.UTF8, "application/json"))
                    {
                        request.Content = stringContent;

                        using (var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)) 
                                                                                                                   
                        {                           
                            String result = response.Result.ToString(); 
                            ok = result.Contains("ReasonPhrase: 'OK'");
                            if (ok)
                            {
                                //if ok is true print the result line to the console and return ok - just did as if for quick check in debugging mdail 9-26-19
                                Console.Write("result = " + result);
                                return ok;
                            }
                            else
                            {
                                //if ok is false print the results line to the console and return ok - just did as if for quick check in debugging mdail 9-26-19
                                Console.Write("result = " + result);
                                return ok;
                            }
                        }
                    }
                }
            }

        }
    }
}
