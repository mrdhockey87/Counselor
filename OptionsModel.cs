using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal static class OptionsModel
    {
        internal class ReferenceTabOptions
        {
            internal bool ShowAdobeMissingWarning { get; set; }
        };

        internal class CounselingGenerationOptions
        {
            internal string DefaultCounselorNameAndTitle { get; set; }
            internal string DefaultOrganizationName { get; set; }
        };

        internal class GenericMemoGenerationOptions
        {
            internal string DefaultSignatureName { get; set; }
            internal string DefaultRank { get; set; }
            internal string DefaultTitle { get; set; }
        };

        private static bool askToSubmitCrashReport;
        private static bool autoSendCrashReport;

        static DataTable optionsTable;
        
        internal static ReferenceTabOptions ReferenceOptions;
        internal static CounselingGenerationOptions CounselingOptions;
        internal static GenericMemoGenerationOptions GenericMemoOptions;

        static OptionsModel()
        {
            Refresh();
        }


        static string GetOptionValue(string optionName)
        {
            try
            {
                DataRow[] rows = optionsTable.Select("optionname = '" + optionName + "'");
                string value = rows[0]["optionvalue"].ToString();
                return value;
            }
            catch(Exception)
            {
                throw new DataLoadFailedException("An error occured parsing the options table.");
            }
        }


        static void LoadReferenceTabOptions()
        {
            ReferenceOptions = new ReferenceTabOptions();
            ReferenceOptions.ShowAdobeMissingWarning = GetOptionValue("showadobemissingwarning") == "1" ? true : false;
        }


        static void LoadCounselingOptions()
        {
            CounselingOptions = new CounselingGenerationOptions();
            CounselingOptions.DefaultCounselorNameAndTitle = GetOptionValue("defaultnameandtitle");
            CounselingOptions.DefaultOrganizationName = GetOptionValue("defaultorganization");
        }


        static void LoadGenericMemoOptions()
        {
            GenericMemoOptions = new GenericMemoGenerationOptions();
            GenericMemoOptions.DefaultSignatureName = GetOptionValue("genericmemodefaultsignaturename");
            GenericMemoOptions.DefaultRank = GetOptionValue("genericmemodefaultsignaturerank");
            GenericMemoOptions.DefaultTitle = GetOptionValue("genericmemodefaultsignaturetitle");
        }


        static void LoadCrashValues()
        {
            askToSubmitCrashReport = Convert.ToBoolean(Convert.ToInt32(GetOptionValue("prompttosubmitcrashreport")));
            autoSendCrashReport = Convert.ToBoolean(Convert.ToInt32(GetOptionValue("submitcrashreport")));
        }

        private static void SaveReferenceTabOptions()
        {
            int showAdobeMissing = ReferenceOptions.ShowAdobeMissingWarning ? 1 : 0;
            SetOptionValue("showadobemissingwarning", showAdobeMissing.ToString());


        }


        private static void SaveCounselingOptions()
        {
            SetOptionValue("defaultnameandtitle", CounselingOptions.DefaultCounselorNameAndTitle);
            SetOptionValue("defaultorganization", CounselingOptions.DefaultOrganizationName);
        }


        private static void SaveGenericMemoOptions()
        {
            SetOptionValue("genericmemodefaultsignaturename", GenericMemoOptions.DefaultSignatureName);
            SetOptionValue("genericmemodefaultsignaturerank", GenericMemoOptions.DefaultRank);
            SetOptionValue("genericmemodefaultsignaturetitle", GenericMemoOptions.DefaultTitle);
        }

        private static void SaveCrashValues()
        {
            int askSubmitCrashReportInt = askToSubmitCrashReport == true ? 1 : 0;
            int autoSendCrashReportInt = autoSendCrashReport == true ? 1 : 0;

            SetOptionValue("prompttosubmitcrashreport", askSubmitCrashReportInt.ToString());
            SetOptionValue("submitcrashreport", autoSendCrashReportInt.ToString());
        }


        internal static void SetOptionValue(string optionName, string optionValue)
        {
            try
            {
                //DatabaseConnection.Update("update useroptions set optionvalue = " + optionValue + " where optionname = '" + optionName + "'");
                DatabaseConnection.Update("useroptions", "optionvalue", optionValue, "optionname", optionName);
            }
            catch(Exception)
            {
                throw new DataLoadFailedException("An error occured saving the option value.");
            }
        }

        internal static void Save()
        {
            SaveReferenceTabOptions();
            SaveCounselingOptions();
            SaveGenericMemoOptions();
            SaveCrashValues();

            Refresh();
        }

        internal static bool AskToSendCrashReport
        {
            get
            {
                string value = GetOptionValue("prompttosubmitcrashreport");
                bool prompt = Convert.ToBoolean(Convert.ToInt32(value));
                return prompt;
            }

            set
            {
                 askToSubmitCrashReport = value;
            }
        }

        internal static bool AutoSubmitCrashReport
        {
            get
            {
                string value = GetOptionValue("submitcrashreport");
                bool prompt = Convert.ToBoolean(Convert.ToInt32(value));
                return prompt;
            }

            set
            {
                autoSendCrashReport = value;
            }
        }

        internal static void Refresh()
        {
            optionsTable = DatabaseConnection.GetTable("useroptions");
            LoadReferenceTabOptions();
            LoadCounselingOptions();
            LoadGenericMemoOptions();
            LoadCrashValues();
        }

        /*
        public static bool AskToSendCrashReport
        {
            get
            {
                string value = GetOptionValue("prompttosubmitcrashreport");
                bool ask = Convert.ToBoolean(Convert.ToInt32(value));
                return ask;
            }

            set
            {

            }
        }
         * */
    }
}
