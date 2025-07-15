using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace CounselQuickPlatinum
{
    static class Program
    {
        [DllImport("User32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("User32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        
        static SplashScreen loadingSplashDialog;
        static Mutex applicationMutex;
        static Form form;

        public static void BringToCQPToFront(string title)
        {
            // Get a handle to the Counselor application.
            IntPtr handle = FindWindow(null, title);
            // Verify that Counselor is a running process.
            if (handle == IntPtr.Zero)
            {
                return;
            }
            // Make Counselor the foreground application
            SetForegroundWindow(handle);
        }

        private static void EnsureSingleInstance()
        {
            try
            {
                applicationMutex = Mutex.OpenExisting("COUNSELOR");
                if (applicationMutex != null)
                {
                    BringToCQPToFront("Counselor");
                    Environment.Exit(0);
                }
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                applicationMutex = new Mutex(true, "COUNSELOR");
            }
        }

        private static void WaitForUpdateToExit()
        {
                System.Diagnostics.Process[] update
                    = System.Diagnostics.Process.GetProcesses().Where(process => process.ProcessName == "update.exe").ToArray();

                if (update.Count() == 0)
                {
                    return;
                }
                else
                {
                    update.First().WaitForExit();
                }
        }

        private static void Update()
        {
            try
            {
                DatabaseConnection.RunSQLUpdateScripts();
            }
            catch (Exception)
            {
                string errorMessage = "An error occurred while attempting to update Counselor.\n\nThe changes to the database will now be rolled back.";
                string caption = "Update Error";
                CQPMessageBox.ShowDialog(errorMessage, caption, CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                DatabaseConnection.Restore();
            }
        }

        static void ParseArgs(string[] args)
        {
            int numArgs = args.Length;

            for (int i = 0; i < numArgs; i++)
            {
                if (args[i] == "-k" || args[i] == "--set-key")
                {
                    if (numArgs < i + 3)
                    {
                        string message = "USAGE: " + args[i] + " LICENSE_KEY USER_APPDATA_PATH";
                        Logger.Error(message);
                        Cleanup();
                        Environment.Exit(1);
                    }

                    string licenseKey = args[++i];
                    string path = args[++i];
                    
                    path += @"\Counselor\Sqlite";

                    InitializeDatabase(new DirectoryInfo(path));
                    Authenticator.SetLicenseKey(licenseKey);
                    Cleanup();
                    Environment.Exit(0);
                }
                else if (args[i] == "-v" || args[i] == "--log-verbose")
                {
                    Logger.Verbosity = Logger.LogVerbosity.Application;
                    Logger.Trace("Logging in verbose mode.");
                }
                else if (args[i] == "-db" || args[i] == "--database-version")
                {
                    InitializeDatabase();
                    string versionNumberString = SettingsModel.DatabaseVersion.ToString();

                    string debugDatabasePath = Utilities.GetCQPUserDataDirectory() + @"\Sqlite";
                    DirectoryInfo directory = new DirectoryInfo(debugDatabasePath);

                    string path =  directory.FullName + "//dbv.txt";

                    File.WriteAllText(path, versionNumberString);
                    Cleanup();
                    Environment.Exit(0);
                }

            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string []args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            DateTime start = DateTime.Now;

            try
            {
                WaitForUpdateToExit();
                EnsureSingleInstance();

                System.Windows.Forms.Application.ThreadException +=
                    new ThreadExceptionEventHandler(ThreadException);

                ParseArgs(args);

                InitializeDatabase();

                Update();
                //Set the splash screen to start in the center of the primary monitor, the center screen put it in the center of the screen where the cursor is mdail 8-13-19
                loadingSplashDialog = new SplashScreen();

                start = DateTime.Now;

                loadingSplashDialog.UpdateProgress(0);
                loadingSplashDialog.Show();

                loadingSplashDialog.UpdateProgress(0);
                loadingSplashDialog.UpdateProgress(10);
                //okay is set to false, then if debug is set to true it is set to true, else it is set to the return of the authenticator function mdail 1-10-19
                bool okay = false;
#if DEBUG
                okay = true;
#else
                okay = Authenticator.AuthenticateRegistration();
#endif
                if (!okay)
                {
                    Cleanup();
                    Environment.Exit(0);
                }

            }
            catch (Exception ex)
            {
                Application.OnThreadException(ex);
            }

            form = new Form1(loadingSplashDialog);
            loadingSplashDialog.UpdateProgress(100);
            loadingSplashDialog.Invalidate();
            loadingSplashDialog.Refresh();

            DateTime stop = DateTime.Now;
            int  ms = (stop - start).Milliseconds;

            // make sure the splash is up for two seconds
            if (ms < 2000)
                Thread.Sleep(2000 - ms);

            loadingSplashDialog.Dispose();
            loadingSplashDialog.Visible = false;
            loadingSplashDialog = null;

            Application.Run(form);

            Logger.Trace("Application exiting normally..");
            Cleanup();

        }

        private static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                DatabaseConnection.Restore();

                LogException(e);

                string error = "Counselor has encountered an unexpected error and needs to close.";
                string caption = "caption";

                if(form != null && form.Visible == true)
                    CQPMessageBox.ShowDialog(form, error, caption, CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                else
                    CQPMessageBox.ShowDialog(error, caption, CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                PromptToSendCrashReport();
                Cleanup();
            }
            finally
            {
                Environment.Exit(-1);
            }
        }

        private static void LogException(ThreadExceptionEventArgs e)
        {
            Logger.CriticalError("**** FATAL ERROR *** - " + e.Exception.Message + "\n" + e.Exception.StackTrace);

            if (e.Exception is CQPException)
            {
                Logger.CriticalError("\n\n");
                Logger.CriticalError(e.Exception);

                foreach (CQPExceptionCode code in ((CQPException)e.Exception).codes)
                {
                    Logger.CriticalError("InnerException: " + code.ToString());
                }
            }
            else
            {
                Logger.CriticalError("\n\n");
                Logger.CriticalError(e.Exception);
            }
        }

        private static void PromptToSendCrashReport()
        {
            bool prompt = true;
            bool sendCrashReport = true;

            if (DatabaseConnection.IsValid == true)
            {
                InitializeDatabase();

                prompt = OptionsModel.AskToSendCrashReport;
                sendCrashReport = OptionsModel.AutoSubmitCrashReport;
            }

            if (prompt)
            {
                DialogResult result = new PromptToSubmitCrashReport().ShowDialog();
                if (result == DialogResult.Yes)
                    sendCrashReport = true;
                else
                    sendCrashReport = false;
            }

            if (sendCrashReport)
            {
                bool success = Logger.SendCrashReport();

                if (success)
                {
                    string message = "Crash report received successfully.\n\n"
                                    + "Thank you for your submission and your patience while we attempt to address this error.";

                    CQPMessageBox.Show(message, "", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);
                }
                else
                {
                    string message = "Counselor was unable to connect to the servers.  The crash report will not be sent.";

                    CQPMessageBox.Show(message, "", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);
                }
            }
        }

        private static void InitializeDatabase()
        {
            string debugDatabasePath = Utilities.GetCQPUserDataDirectory() + @"\Sqlite";
            DirectoryInfo directory = new DirectoryInfo(debugDatabasePath);

            InitializeDatabase(directory);
        }

        private static void InitializeDatabase(DirectoryInfo directory)
        {
            Logger.Trace("Initializing Database...");

            string debugDatabasePath = directory.FullName;// +@"\Counselor\Sqlite";
            FileInfo debugDatabase = new FileInfo(debugDatabasePath + @"\debugdatabase.db3");

            bool isConnected = DatabaseConnection.Initialize(debugDatabase);

            if (isConnected == false)
                throw new CQPException("Attempt to connect to the working copy failed.");
        }

        private static void Cleanup()
        {
            DatabaseConnection.CloseDatabase();
            DatabaseConnection.DeleteBackups();

            if(applicationMutex != null)
                applicationMutex.ReleaseMutex();

            Logger.Close();
        }
    }
}
