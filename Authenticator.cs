using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;
using System.Net;
using System.Data;

namespace CounselQuickPlatinum
{
    class Authenticator
    {
        /*private enum ResultValue : int
        {
            DatabaseCorrupt = -2, // should never happen!!
            AuthenticationAttemptFailed = -1,
            SerialNotFound = 0,
            SerialDisabled = 1,
            HardDriveSerialMismatch = 2,
            ResultValueUnknown = 3,
            Enabled = 4
        }*/

        private enum ResultValue : int
        {
            DatabaseCorrupt = -2, // should never happen!!
            Disabled = 0,
            Enabled = 1
        }

        private static bool CheckConnection()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.armycounselingonline.com");
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Check Connection failed.", ex);

                return false;
            }
        }

        internal static bool AuthenticateRegistration()
        {
            if (!AuthenticationFileAndEntryExists())
            {
                // this should really never happen - the database regenerates itself
                // automatically if deleted and should contain a 3 if it was
                ShowDatabaseCorruptError();
                return false;
            }

            int result = AuthenticateRegistrationOnline();

            if (result == -1)
                result = Convert.ToInt32(AuthenticateRegistrationOffline());

            return (result == 1);
        }


        private static int AuthenticateRegistrationOnline()
        {
            HttpWebRequest request = GetAuthenticatorRequest();

            string postDataString = GetPostDataString();
            char[] postData = postDataString.ToCharArray();
            byte[] postDataBytes = Encoding.ASCII.GetBytes(postData);
            int result = 0;

            try
            {
                Stream stream = request.GetRequestStream();
                stream.Write(postDataBytes, 0, postDataBytes.Length);
                stream.Close();

                result = GetAuthenticationResult(request);

                stream.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Error attempting to authenticate online.", ex);
                return -1;
            }

            return result;
        }

        private static bool HandleResult(ResultValue result)
        {
            switch (result)
            {
                    /*
                case (ResultValue.AuthenticationAttemptFailed):
                    return AuthenticateRegistrationOffline();
                case (ResultValue.Enabled):
                    //SaveResult(result);
                    return true;
                case (ResultValue.HardDriveSerialMismatch):
                    //SaveResult(result);
                    //ShowErrorMessage(result);
                    ShowStoredErrorMessage();
                    return false;
                case (ResultValue.SerialDisabled):
                    //SaveResult(result);
                    //ShowErrorMessage(result);
                    ShowStoredErrorMessage();
                    return false;
                case (ResultValue.SerialNotFound):
                    //SaveResult(result);
                    //ShowErrorMessage(result);
                    ShowStoredErrorMessage();
                    return false;
                     * */
                case (ResultValue.DatabaseCorrupt):
                    //ShowErrorMessage(result);
                    ShowStoredErrorMessage();
                    return false;
                    /*
                case(ResultValue.ResultValueUnknown):
                    //SaveResult(result);
                    //ShowErrorMessage(result);
                    ShowStoredErrorMessage();
                    return false;
                     * */
            }

            return false;
        }

        private static void SaveResult(int code, string caption, string message)
        {
            Logger.Trace("Authenticator - SaveResult");

            try
            {
                DatabaseConnection.Update("settings", "settingvalue", code.ToString(), "settingname", "lastresult");
                DatabaseConnection.Update("settings", "settingvalue", caption.ToString(), "settingname", "lastresultstatustext");
                DatabaseConnection.Update("settings", "settingvalue", message.ToString(), "settingname", "lastresultmessagetext");
            }
            catch (DataStoreFailedException ex)
            {
                Logger.Error("    Authenticator - SaveResult - Exception occurred - " + ex.Message + " " + ex.StackTrace, ex);
                throw ex;
            }
        }

        private static HttpWebRequest GetAuthenticatorRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://license.mentorenterprisesinc.com/api/verify");

            request.Timeout = 5000;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";

            return request;
        }

        private static string RemoveSurroundingQuotes(string value)
        {
            if (value.First() != '\"')
                return "";

            if (value.Last() != '\"')
                return "";

            value = value.Remove(value.Length - 1);
            value = value.Substring(1);

            return value;
        }

        private static string CleanupEscapedCharsAndRemoveSurroundingQuotes(string value)
        {
            value = value.Replace("\\n", "\n");
            value = value.Replace("\\r", "\r");
            value = value.Replace("\\\"", "\"");
            value = value.Replace("\\\'", "\'");
            value = value.Replace("\\\\", "\\");

            value = RemoveSurroundingQuotes(value);

            return value;
        }

        private static string GetJsonKeyword(int firstColonIndex, string jsonString)
        {
            // if no more colons
            if (firstColonIndex == -1)
                return "";

            string keywordSubstring = jsonString.Substring(0, firstColonIndex);
            keywordSubstring = RemoveSurroundingQuotes(keywordSubstring);

            return keywordSubstring;
            
        }

        private static string GetJsonValue(int firstCommaIndex, string jsonString)
        {
            if (firstCommaIndex == -1)
                return "";

            string valueSubstring = jsonString.Substring(0, firstCommaIndex);
            valueSubstring = RemoveSurroundingQuotes(valueSubstring);

            return valueSubstring;
        }

        private static SortedDictionary<string, string> ParseJson(string jsonString)
        {
            Newtonsoft.Json.Linq.JObject jobject = Newtonsoft.Json.Linq.JObject.Parse(jsonString);

            SortedDictionary<string, string> json = new SortedDictionary<string, string>();
            json["code"] = CleanupEscapedCharsAndRemoveSurroundingQuotes(jobject["code"].ToString());
            json["message"] = CleanupEscapedCharsAndRemoveSurroundingQuotes(jobject["message"].ToString());
            json["status"] = CleanupEscapedCharsAndRemoveSurroundingQuotes(jobject["status"].ToString());

            return json;
        }

        //[Obfuscation(Feature = "virtualization", Exclude = false)]
        private static int GetAuthenticationResult(HttpWebRequest request)
        {
            Logger.Trace("Authenticator - GetAuthenticationResult ");

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                    return -1;

                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                string responseString = reader.ReadToEnd();

                responseString = responseString.Replace("\\n", "\n");
                responseString = responseString.Replace("\\r", "\r");
                responseString = responseString.Replace("\\\"", "\"");
                responseString = responseString.Replace("\\\'", "\'");
                responseString = responseString.Replace("\\\\", "\\");

                SortedDictionary<string, string> jsonArray = ParseJson(responseString);

                reader.Close();
                response.Close();

                string codeString = jsonArray["code"];
                int code = Convert.ToInt32(codeString);

                if (code == 0)
                {
                    string errorMessage = jsonArray["message"];
                    string caption = jsonArray["status"];

                    ShowErrorMessage(caption, errorMessage);
                    SaveResult(code, caption, errorMessage);
                }
                else
                {
                    SaveResult(code, "", "");
                }

                return code;
            }
            //catch (System.Net.WebException ex, Exception ex2)
            catch(Exception ex)
            {
                Logger.Error("    Authenticator - GetAuthenticationResult - Exception occurred - " + ex.Message + " \n" + ex.StackTrace);

                return -1;
            }

        }

        private static bool AuthenticateRegistrationOffline()
        {
            ResultValue value = GetLastResultValue();

            if(value == ResultValue.Enabled)
                return true;

            ShowStoredErrorMessage();
            return false;
        }

        private static ResultValue GetLastResultValue()
        {
            DataTable table = DatabaseConnection.GetTable("settings");
            if (table == null)
                return ResultValue.DatabaseCorrupt;

            DataRow[] rows = table.Select("settingname = 'lastresult'");
            if (rows.Count() == 0)
                return ResultValue.DatabaseCorrupt;

            if (table.Columns.Contains("settingvalue") == false)
                return ResultValue.DatabaseCorrupt;

            string valueStr = rows.First()["settingvalue"].ToString();
            int valueInt = Convert.ToInt32(valueStr);

            return (ResultValue)valueInt;
        }

        private static bool AuthenticationFileAndEntryExists()
        {
            try
            {
                DataTable table = DatabaseConnection.GetTable("settings");
                if (table == null)
                    return false;

                DataRow[] rows = table.Select("settingname = 'serialnumber'");
                if (rows.Count() == 0)
                    return false;

                if (table.Columns.Contains("settingvalue") == false)
                    return false;
            }
            catch (Exception ex)
            {
                Logger.Error("", ex);

                return false;
            }

            return true;
        }

        private static string GetPostDataString()
        {
            string driveSerial = SettingsModel.GetDriveSerialNumber();
            string programSerialNumber = SettingsModel.GetProgramSerialNumber();

            string username = "";

            try
            {
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string usernamePattern = @".*\\(?<username>.*)\\(?:AppData|Local Settings\\Application Data)\\*.*";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(usernamePattern);
                System.Text.RegularExpressions.MatchCollection matches = regex.Matches(appDataFolder);

                username = matches[0].Groups["username"].Value;
            }
            catch (Exception)
            {
                Logger.Error("Could not retrieve the username from app data directory, using environment.username.");
                username = Environment.UserName;
            }

            string postDataString = "Serialnumber=" + programSerialNumber + "&DriveSerial=" + driveSerial + "&program=Counselor&user=" + username;
            return postDataString;
        }

        private static void ShowErrorMessage(string caption, string message)
        {
            string serialNumber = SettingsModel.GetProgramSerialNumber();

            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

            CQPMessageBox.Show(message, caption, buttons, icon);
        }

        private static void ShowStoredErrorMessage()
        {
            try
            {
                string caption = DatabaseConnection.Query("select settingvalue from settings where settingname = 'lastresultstatustext'").Rows[0]["settingvalue"].ToString();
                string message = DatabaseConnection.Query("select settingvalue from settings where settingname = 'lastresultmessagetext'").Rows[0]["settingvalue"].ToString();

                CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                CQPMessageBox.Show(message, caption, buttons, icon);
            }
            catch (Exception ex)
            {
                Logger.Error("An error occurred attempting to retrieve the stored authentication message values from the database.", ex);

                throw new CQPException("An error occurred attempting to authenticate the program.", ex);
            }
        }

        private static void ShowDatabaseCorruptError()
        {
            string caption = "Authentication Failed";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

            string errorMessage = "Counsel Quick cannot authenticate the program because application data files are missing or corrupt. \n"
                + "\n"
                + "This is a critical error.  Restart the program, and if the problem persists, contact support or reinstall the program.\n\n";

            CQPMessageBox.Show(errorMessage, caption, buttons, icon);
        }

        internal static void SetLicenseKey(string licenseKey)
        {
            try
            {
                DatabaseConnection.Update("settings", "settingvalue", licenseKey, "settingname", "serialnumber");
            }
            catch (Exception ex)
            {
                CQPMessageBox.Show("An error occurred attempting to save the serial number.");
                throw ex;
            }
        }
    }
}
