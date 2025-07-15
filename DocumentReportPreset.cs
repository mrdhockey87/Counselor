using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    //[System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    internal class DocumentReportFilter
    {
        internal bool Saveable { get; set; }
        public int DocumentReportFilterID { get; set; }
        public string DocumentReportFilterName { get; set; }
        internal List<int> CounselingTemplateIDs;
        internal List<int> DocumentTemplateIDs;
        internal List<int> DocumentStatusIDs;
        internal List<int> DocumentCategories;

        internal DocumentReportFilter()
        {
            DocumentReportFilterID = -1;
            DocumentReportFilterName = "";
            CounselingTemplateIDs = new List<int>();
            DocumentTemplateIDs = new List<int>();
            DocumentStatusIDs = new List<int>();
            DocumentCategories = new List<int>();
        }

        internal static void DeepCopyReportFilterToCurrentFilter(DocumentReportFilter lhs, DocumentReportFilter rhs)
        {
            //currentFilter = new DocumentReportFilter();

            lhs.DocumentReportFilterID = rhs.DocumentReportFilterID;
            lhs.DocumentReportFilterName = rhs.DocumentReportFilterName;

            lhs.CounselingTemplateIDs.Clear();
            lhs.DocumentTemplateIDs.Clear();
            lhs.DocumentStatusIDs.Clear();
            lhs.DocumentCategories.Clear();

            foreach (int cID in rhs.CounselingTemplateIDs)
                lhs.CounselingTemplateIDs.Add(cID);
            foreach (int dID in rhs.DocumentTemplateIDs)
                lhs.DocumentTemplateIDs.Add(dID);
            foreach (int sID in rhs.DocumentStatusIDs)
                lhs.DocumentStatusIDs.Add(sID);
            foreach (int dcID in rhs.DocumentCategories)
                lhs.DocumentCategories.Add(dcID);
        }


        internal DocumentReportFilter(int documentReportFilterID)
        {
            DocumentReportFilterID = documentReportFilterID;
            DocumentReportFilterName = GetDocumentReportFilterName(documentReportFilterID);
            Saveable = GetSaveable(documentReportFilterID);
            CounselingTemplateIDs = GetCounselingTemplateIDsForPresetID(documentReportFilterID);
            DocumentTemplateIDs = GetDocumentTemplateIDsForPresetID(documentReportFilterID);
            DocumentStatusIDs = GetStatusIDsForPresetID(documentReportFilterID);
            DocumentCategories = GetDocumentCategoriesForPresetID(documentReportFilterID);
        }

        private List<int> GetDocumentCategoriesForPresetID(int documentReportFilterID)
        {
            return GetIDsForTable("documentreportfiltercategoryids", documentReportFilterID, "documentcategoryid");
        }


        string GetDocumentReportFilterName(int documentReportFilterID)
        {
            //string query = "select documentreportfiltername from documentreportfilters";
            //DataTable documentReportNames = DatabaseConnection.Query(query);

            string name = DatabaseConnection.GetSingleValue("documentreportfilters",
                                                            "documentreportfilterid", documentReportFilterID,
                                                            "documentreportfiltername");
            return name;
        }


        bool GetSaveable(int documentReportFilterID)
        {
            string saveableStr = DatabaseConnection.GetSingleValue("documentreportfilters",
                                                "documentreportfilterid", documentReportFilterID,
                                                "allowdelete");
            bool saveable = saveableStr == "1" ? true : false;
            return saveable;
        }


        List<int> GetCounselingTemplateIDsForPresetID(int documentReportFilterID)
        {
            return GetIDsForTable("documentreportfiltercounselingids", documentReportFilterID, "documentnameid");
        }


        List<int> GetDocumentTemplateIDsForPresetID(int documentReportFilterID)
        {
            return GetIDsForTable("documentreportfilterdocumentids", documentReportFilterID, "documentnameid");
        }


        List<int> GetStatusIDsForPresetID(int documentReportFilterID)
        {
            return GetIDsForTable("documentreportfilterstatuses", documentReportFilterID, "documentstatusid");
        }


        List<int> GetIDsForTable(string tablename, int documentReportFilterID, string idColumnName)
        {
            string query = "select * from " + tablename
                + " where documentreportfilterid = " + documentReportFilterID;

            DataTable table = DatabaseConnection.Query(query);

            List<int> documentNames = new List<int>();

            foreach (DataRow row in table.Rows)
            {
                int documentNameID = Convert.ToInt32(row[idColumnName]);
                documentNames.Add(documentNameID);
            }

            return documentNames;
        }


        internal static List<DocumentReportFilter> GetAllDocumentReportFilters()
        {
            string query = "select documentreportfilterid from documentreportfilters";
            DataTable documentReportFiltersTable = DatabaseConnection.Query(query);

            //List<int> documentReportFiltersIDs = new List<int>();

            List<DocumentReportFilter> reportFilters = new List<DocumentReportFilter>();

            foreach (DataRow row in documentReportFiltersTable.Rows)
            {
                int documentReportFilterID = Convert.ToInt32(row["documentreportfilterid"]);
                DocumentReportFilter documentReportFilter = new DocumentReportFilter(documentReportFilterID);
                reportFilters.Add(documentReportFilter);
            }

            return reportFilters;
        }


        internal void Save()
        {
            if (!DocumentReportFilterExists(DocumentReportFilterID))
                InsertNewDocumentReportFilter();
            else
                SaveDocumentReportFilter();
        }


        internal static bool DocumentReportFilterExists(int DocumentReportFilterID)
        {
            string query = "select documentreportfilterid from documentreportfilters "
                            + "where documentreportfilterid = " + DocumentReportFilterID;

            DataTable results = DatabaseConnection.Query(query);

            if (results.Rows.Count == 0)
                return false;

            return true;
        }


        private void InsertNewDocumentReportFilter()
        {
            InsertDocumentReportFilterEntry();
            InsertCounselingIDFilterEntries();
            InsertDocumentationIDFilterEntries();
            InsertStatusIDFilterEntries();
        }


        private void InsertDocumentReportFilterEntry()
        {
            //changed back to using just the sql command. To fix bug if user addes name of report filet with single quote, added the rplace which replaces a single quote with two single quotes
            //so that it doesn't cause an error when inserting into the database mdail 2-14-19
            string insertCommand
                = "insert into documentreportfilters (documentreportfiltername) "
                    + "values (\'" + DocumentReportFilterName.Replace("'", "''") + "\')";

            // +"values (\'@documentReportFilterName\')";
            //+ "values (\'" + DocumentReportFilterName + "\')";
            // Params paramValues = new Params();
            //  paramValues.Add("@documentReportFilterName", DocumentReportFilterName);

            DocumentReportFilterID = DatabaseConnection.Insert(insertCommand, null); //, paramValues);
        }

        
        private void InsertCounselingIDFilterEntries()
        {
            foreach (int counselingID in CounselingTemplateIDs)
            {
                string insertCommand = "insert into documentreportfiltercounselingids "
                    + "(documentreportfilterid, documentnameid) "
                    //+ "values (" + DocumentReportFilterID + ", " + counselingID + ")";
                    + "values (@documentReportFilterID, @counselingID )";

                Params paramValues = new Params();
                paramValues.Add("@documentReportFilterID", DocumentReportFilterID.ToString());
                paramValues.Add("@counselingID", counselingID.ToString());

                DatabaseConnection.Insert(insertCommand, paramValues);
            }
        }


        private void InsertDocumentationIDFilterEntries()
        {
            foreach (int counselingID in DocumentTemplateIDs)
            {
                string insertCommand = "insert into documentreportfilterdocumentids "
                    + "(documentreportfilterid, documentnameid) "
                    + "values (@DocumentReportFilterID , @counselingID )";

                Params paramValues = new Params();
                paramValues.Add("@DocumentReportFilterID", DocumentReportFilterID.ToString());
                paramValues.Add("@counselingID", counselingID.ToString());

                DatabaseConnection.Insert(insertCommand, paramValues);
            }
        }


        private void InsertStatusIDFilterEntries()
        {
            foreach (int statusID in DocumentStatusIDs)
            {
                string insertCommand = "insert into documentreportfilterstatuses "
                    + "(documentreportfilterid, documentstatusid) "
                    + "values (@DocumentReportFilterID, @statusID )";

                Params paramValues = new Params();
                paramValues.Add("@DocumentReportFilterID", DocumentReportFilterID.ToString());
                paramValues.Add("@statusID", statusID.ToString());

                DatabaseConnection.Insert(insertCommand, paramValues);
            }
        }


        private void SaveDocumentReportFilter()
        {
            SaveDocumentReportName();
            SaveCounselingIDFilters();
            SaveDocumentationIDFilters();
            SaveDocumentStatusIDFilters();
        }


        private void SaveDocumentReportName()
        {
            //string updateCommand = "update documentreportfilters "
            //                        + "set documentreportfiltername=\'" + DocumentReportFilterName + "\'"
            //                        + "where documentreportfilterid=" + DocumentReportFilterID + "\n";

            //DatabaseConnection.Update(updateCommand);

            DatabaseConnection.Update("documentreportfilters", "documentreportfiltername", DocumentReportFilterName,
                                        "documentreportfilterid", DocumentReportFilterID.ToString());
        }


        private void SaveCounselingIDFilters()
        {
            DatabaseConnection.Delete("documentreportfiltercounselingids", 
                                        "documentreportfilterid", 
                                        DocumentReportFilterID.ToString());

            InsertCounselingIDFilterEntries();

            //foreach (int counselingID in CounselingTemplateIDs)
            //{
                //string updateCommand = "update documentreportfiltercounselingids "
                //    + "set documentnameid=" + counselingID + " "
                //    + "where "
                //    + "documentreportfilterid=" + DocumentReportFilterID;

                //DatabaseConnection.Update(updateCommand);
            //}
        }


        private void SaveDocumentationIDFilters()
        {
            DatabaseConnection.Delete("documentreportfilterdocumentids",
                                        "documentreportfilterid",
                                        DocumentReportFilterID.ToString());

            InsertDocumentationIDFilterEntries();

            //foreach (int documentID in DocumentTemplateIDs)
            //{
            //    string updateCommand = "update documentreportfilterdocumentids "
            //        + "set documentnameid=" + documentID + " "
            //        + "where "
            //        + "documentreportfilterid=" + DocumentReportFilterID;

            //    DatabaseConnection.Insert(updateCommand);
            //}
        }


        private void SaveDocumentStatusIDFilters()
        {
            DatabaseConnection.Delete("documentreportfilterstatuses",
                                        "documentreportfilterid",
                                        DocumentReportFilterID.ToString());

            InsertStatusIDFilterEntries();

            //foreach (int statusID in DocumentStatusIDs)
            //{
            //    string updateCommand = "update documentreportfilterstatuses "
            //        + "set documentstatusid=" + statusID + " "
            //        + "where "
            //        + "documentreportfilterid=" + DocumentReportFilterID;

            //    DatabaseConnection.Insert(updateCommand);
            //}
        }

        internal void Delete()
        {
            DatabaseConnection.Backup();

            DeleteDocumentFilterEntry();
            DeleteDocumentFilterCounselingIDs();
            DeleteDocumentFilterDocumentationIDs();
            DeleteDocumentFilterStatusIDs();

            DatabaseConnection.Backup();
        }

        private void DeleteDocumentFilterEntry()
        {
            DatabaseConnection.Delete("documentreportfilters", "documentreportfilterid", DocumentReportFilterID.ToString());
        }

        private void DeleteDocumentFilterCounselingIDs()
        {
            DatabaseConnection.Delete("documentreportfiltercounselingids", "documentreportfilterid", DocumentReportFilterID.ToString());
        }

        private void DeleteDocumentFilterDocumentationIDs()
        {
            DatabaseConnection.Delete("documentreportfilterdocumentids", "documentreportfilterid", DocumentReportFilterID.ToString());
        }

        private void DeleteDocumentFilterStatusIDs()
        {
            DatabaseConnection.Delete("documentreportfilterstatuses", "documentreportfilterid", DocumentReportFilterID.ToString());
        }
    }
}
