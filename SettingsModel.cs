using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CounselQuickPlatinum
{
    //[System.Reflection.ObfuscationAttribute(Exclude=true)]
    static class SettingsModel
    {
        //[System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
        private static SortedDictionary<string, object> resourcesDictionary;

        internal static string ReferencesDirectory
        {
            get
            {
                try
                {
                    DataTable table = DatabaseConnection.Query("select settingvalue from settings where settingname='referencesdirectory'");
                    //Query(string tableName, List<string> columnsToRetrieve, List<Condition> conditions)

                    //Dictionary<string, string> paramValuePairs = new Dictionary<string, string>() { {"@settingName", "referencesDirectory" } };
                    //DataTable table = DatabaseConnection.Query("select settingvalue from settings where settingname='@settingName'", paramValuePairs);

                    string directoryName = table.Rows[0]["settingvalue"].ToString();
                    return directoryName;
                }
                catch (QueryFailedException ex)
                {
                    
                    Logger.Error("Unable to get references directory from settings table: \n");
                    Logger.Error(ex);

                    string error = "An error occurred attempting to locate the references directory.\n"
                                    + "The application database is most likely missing or has become corrupt.\n"
                                    + "If this error continues, contact technical support.";

                    throw new DataLoadFailedException(error, ex);
                }
            }
        }

        internal static string HelpDirectory
        {
            get
            {
                try
                {
                    DataTable table = DatabaseConnection.Query("select settingvalue from settings where settingname='helpdocumentsdirectory'");
                    string directoryName = table.Rows[0]["settingvalue"].ToString();
                    return directoryName;
                }
                catch (QueryFailedException ex)
                {
                    Logger.Error("Unable to get help directory from settings table: \n");
                    Logger.Error(ex);

                    throw new DataLoadFailedException("An error occurred attempting to locate the help files directory.\n"
                                    + "The application database is most likely missing or has become corrupt.\n"
                                    + "If this error continues, contact technical support.", ex);
                }
            }
        }

        internal static string FormsDirectory
        {
            get
            {
                try
                {
                    Logger.Trace("Getting FormsDirectory: The cwd is: " + System.IO.Directory.GetCurrentDirectory());

                    DataTable table = DatabaseConnection.Query("select settingvalue from settings where settingname='formsdirectory'");
                    string directoryName = table.Rows[0]["settingvalue"].ToString();

                    System.IO.DirectoryInfo formsDirectory = new System.IO.DirectoryInfo(directoryName);
                    return formsDirectory.FullName;
                }
                catch (QueryFailedException ex)
                {
                    Logger.Error("Unable to get forms directory from settings table: \n");
                    Logger.Error(ex);

                    string error = "An error occurred attempting to locate the forms directory.\n"
                                    + "The application database is most likely missing or has become corrupt.\n"
                                    + "If this error continues, contact technical support.";

                    throw new DataLoadFailedException(error, ex);
                }
            }
        }

        internal static string RankingImageDirectory
        {
            get
            {
                try
                {
                    DataTable table = DatabaseConnection.Query("select settingvalue from settings where settingname='rankingimagepath'");
                    string directoryName = table.Rows[0]["settingvalue"].ToString();
                    return directoryName;
                }
                catch (QueryFailedException ex)
                {
                    Logger.Error("Unable to get rankingimagepath directory from settings table: \n", ex);
                                    //+ "Exception message: " + ex.Message + "\n"
                                    //+ "Exception stack trace: " + ex.StackTrace + "\n");
                    //Logger.Trace(ex);

                    string error = "An error occurred attempting to locate the ranking images directory.\n"
                                    + "The application database is most likely missing or has become corrupt.\n"
                                    + "If this error continues, contact technical support.";
                    throw new DataLoadFailedException(error, ex);
                }
            }
        }


        private static void LoadResourcesDictionary()
        {
            Logger.Trace("LoadResourcesDictionary");

            try
            {
                resourcesDictionary = new SortedDictionary<string, object>();

                Logger.Trace("There are " + typeof(Properties.Resources).GetProperties().Count()
                                + " resources.");

                //System.Resources.ResourceManager rm = new System.Resources.ResourceManager("rmc", Assembly.GetCallingAssembly());

                
                foreach (PropertyInfo property in typeof(Properties.Resources).GetProperties
                    (BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    try
                    {

                        Logger.Trace("Resource: " + property.Name);

                        resourcesDictionary.Add(property.Name, property.GetValue(null, null));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Caught exception : " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Could not do foreach: " + ex.Message);
            }

            Logger.Trace("Loaded Resources Dictionary");
        }


        internal static SortedDictionary<string, object> ResourcesDictionary
        {
            get
            {
                if(resourcesDictionary == null)
                    LoadResourcesDictionary();

                return resourcesDictionary;
            }
        }


        internal static int DatabaseVersion
        {
            get
            {
                DataTable table = DatabaseConnection.Query("select settingvalue from settings where settingname = 'databaseversion'");
                string value = table.Rows[0]["settingvalue"].ToString();
                return Convert.ToInt32(value);
            }
        }


        internal static string GetProgramSerialNumber()
        {
            Logger.Trace("GetProgramSerialNumber..");

            try
            {
                DataTable settingsTable = DatabaseConnection.GetTable("settings");
                DataRow[] rows = settingsTable.Select("settingname = 'serialnumber'");

                if (rows.Count() == 0)
                {
                    Logger.Error("Could not retrieve serial number from db.");
                    throw new DataLoadFailedException("Could not load serial number for authentication!");
                }

                string serialNumber = rows[0]["settingvalue"].ToString();
                return serialNumber;
            }
            catch (DataLoadFailedException ex)
            {
                Logger.Error("*** GetProgramSerialNumber: DataLoadFailedException ! - " + ex.Message + "\n" + ex.StackTrace);
                throw ex;
            }
        }

        internal static string GetDriveSerialNumber()
        {
            string path = Environment.GetEnvironmentVariable("systemroot");

            string driveLetter = path[0].ToString();

            //check to see if the user provided a drive letter
            //if not default it to "C"
            if (driveLetter == "" || driveLetter == null)
            {
                driveLetter = "C";
            }

            //create our ManagementObject, passing it the drive letter to the
            //DevideID using WQL
            System.Management.ManagementObject disk = new System.Management.ManagementObject("win32_logicaldisk.deviceid=\"" + driveLetter + ":\"");

            //bind our management object
            disk.Get();

            string driveSerialNumberInHex = disk["VolumeSerialNumber"].ToString();

            uint driveSerialNumberDecimal = Convert.ToUInt32(driveSerialNumberInHex, 16);

            return driveSerialNumberDecimal.ToString();
        }
    }
}
