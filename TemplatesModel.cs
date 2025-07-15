using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;


namespace CounselQuickPlatinum
{
    class TemplatesModel
    {
        private static DataTable counselingChecklists;
        private static DataTable templateGroupsTable;
        private static DataTable templatesTable;


        internal struct PackageGenerationParamters
        {
            internal int headDocumentID;
            internal List<int> documentIDs;
            internal Soldier soldier;
            internal BackgroundWorker backgroundWorker;
        };


        private static void RefreshTemplatesTable()
        {
            string query
                = "select * from templates "
                + " inner join "
                + " templategroups "
                + " on templates.templategroupid = templategroups.templategroupid "
                + " inner join "
                + " documentnames "
                + " on templates.documentnameid = documentnames.documentnameid "
                + " order by templates.templategroupid, documentnames.documentnametext";

            try
            {
                templatesTable = DatabaseConnection.Query(query);
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }
        }


        internal static DataTable TemplatesTable
        {
            get
            {
                try
                {
                    if (templatesTable == null)
                        RefreshTemplatesTable();

                    return templatesTable;
                }
                catch (QueryFailedException ex)
                {
                    throw new DataLoadFailedException("An error occurred attempting to load the templates", ex);
                }
            }
        }


        private static void RefreshTemplateGroupsTable()
        {
            try
            {
                //templateGroupsTable = DatabaseConnection.GetTable("templategroups");
                templateGroupsTable = DatabaseConnection.Query("select * from templategroups order by templategroupname");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }
        }


        internal static DataTable TemplateGroups
        {
            get
            {
                try
                {
                    if (templateGroupsTable == null)
                        RefreshTemplateGroupsTable();

                    return templateGroupsTable;
                }
                catch (QueryFailedException ex)
                {
                    throw new DataLoadFailedException("An error occured attempting to load the templates", ex);
                }
            }
        }


        private static void RefreshCounselingChecklists()
        {
            try
            {
                counselingChecklists = DatabaseConnection.GetTable("counselingchecklists");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }
        }


        public static DataTable CounselingChecklists
        {
            get
            {
                try
                {
                    if (counselingChecklists == null)
                        RefreshCounselingChecklists();

                    return counselingChecklists;
                }
                catch (QueryFailedException ex)
                {
                    throw new DataLoadFailedException("Error retrieveing the General, Specific, and Extract data for counselings.", ex);
                }
            }
        }


        internal static DataTable GetTemplateValuesByTemplateID(int templateID)
        {
            string query = "select * from templates "
                            + " inner join "
                            + " templatevalues "
                            + " on templates.tempateid = templatevalues.templateid "
                            //+ " on templates.templateid = templatevalues.templateid "
                            + " inner join "
                            + " formfields "
                            + " on templatevalues.formfieldid = formfields.formfieldid "
                            + " where templates.tempateid = \"" + templateID + "\""
                            //+ " where templates.templateid = \"" + templateID + "\""
                            + " order by templateid asc, priority asc";

            try
            {
                DataTable results = DatabaseConnection.Query(query);
                return results;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException("Failed to load values for the selected template.", ex);
            }
        }


        internal static string GetTemplateNameByTemplateID(int templateID)
        {
            string query = "select * from templates "
                            + " inner join "
                            + " documentnames "
                            + " on templates.documentnameid = documentnames.documentnameid "
                            + " where templates.tempateid = " + templateID;
                            //+ " where templates.templateid = " + templateID;

            try
            {
                DataTable results = DatabaseConnection.Query(query);
                string templateName = results.Rows[0]["documentnametext"].ToString();
                return templateName;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException("Failed to retrieve document name", ex);
            }
        }


        internal static List<int> GetTemplatesInTemplatePackage(int headTemplateID)
        {
            List<int> templateIDs = new List<int>();

            DataTable counselingPackages = CounselingPackagesForTemplateID(headTemplateID);
            //templateIDs.Add(headTemplateID);

            foreach (DataRow counselingPackage in counselingPackages.Rows)
            {
                int templateID = Convert.ToInt32(counselingPackage["additionaltemplateid"]);
                templateIDs.Add(templateID);
            }

            return templateIDs;
        }


        internal static DataTable CounselingPackagesForTemplateID(int headTemplateID)
        {
            DataTable counselingPackage = DatabaseConnection.Query("select * from counselingpackagegroups"
                                                                    + " where headtemplateid = " + headTemplateID);

            if (counselingPackage.Rows.Count == 0)
                return new DataTable();

            int counselingPackageID = Convert.ToInt32(counselingPackage.Rows[0]["counselingpackageid"]);

            DataTable counselingPackageEntries = DatabaseConnection.Query("select * from counselingpackagedocuments "
                                                                            + " where counselingpackageid = " + counselingPackageID);

            return counselingPackageEntries;
        }


        internal static List<string> GetNamesForTemplateIDs(List<int> templateIDs)
        {
            List<string> templateNames = new List<string>();

            //List<string> tables = new List<string>{"templates", "documentnames"};
            //List<string> columns = new List<string>{"documentnameid", "documentnameid"};

            //DataTable templates = databaseConnection.GetJoinedTables(tables, columns, "inner join");
            DataTable templates = DatabaseConnection.Query("select * from templates inner join documentnames "
                                     + " on templates.documentnameid = documentnames.documentnameid ");

            foreach (DataRow template in templates.Rows)
            {
                int templateID = Convert.ToInt32(template["tempateid"]);
                //int templateID = Convert.ToInt32(template["templateid"]);
                if (templateIDs.Contains(templateID) == false)
                    continue;

                string templateName = template["documentnametext"].ToString();
                templateNames.Add(templateName);
            }

            return templateNames;
        }


        internal static DocumentType GetTemplateTypeByTemplateID(int templateID)
        {
            if (templatesTable == null)
                RefreshTemplatesTable();

            DataRow[] template = templatesTable.Select("tempateid = " + templateID);
            //DataRow[] template = templatesTable.Select("templateid = " + templateID);
            DocumentType type = (DocumentType) Convert.ToInt32(template[0]["documenttypeid"]);

            return type;
        }


        internal static DocumentFormIDs GetTemplateFormIDByTemplateID(int templateID)
        {
            if (templatesTable == null)
                RefreshTemplatesTable();

            DataRow[] template = templatesTable.Select("tempateid = " + templateID);
            //DataRow[] template = templatesTable.Select("templateid = " + templateID);
            DocumentFormIDs formID = (DocumentFormIDs)Convert.ToInt32(template[0]["formid"]);

            return formID;
        }


        internal static void GenerateCounselingPackageInserts(PackageGenerationParamters parameters)
        {
            int headDocumentID = parameters.headDocumentID;
            List<int> templatesInGroup = parameters.documentIDs;
            Soldier soldier = parameters.soldier;

            int progress = 0;

            foreach (int templateID in templatesInGroup)
            {
                string formIDString = DatabaseConnection.GetSingleValue("templates", "tempateid", templateID, "formid");
                //string formIDString = DatabaseConnection.GetSingleValue("templates", "templateid", templateID, "formid");
                int formID = Convert.ToInt32(formIDString);

                Document childDocument = new Document();
                Template template = new Template(templateID);

                if (formID == (int)DocumentFormIDs.DA4856PDF)
                {
                    
                    childDocument = DA4856Document.GenerateNewFromTemplate(template);
                    DA4856Document da4856Document = childDocument as DA4856Document;

                    if (soldier.SoldierID != -1)
                        da4856Document.Name = soldier.LastName + ", " + soldier.FirstName + " " + soldier.MiddleInitial;

                    da4856Document.Rank = soldier.Rank;

                    //da4856Document.DateAssessmentDue = DateTime.Now + new TimeSpan(0, 0, 90);
                    //int hoursInThirtyDays = 24 * 30;
                    //da4856Document.DateAssessmentDue = DateTime.Now + new TimeSpan(hoursInThirtyDays, 0, 0);
                }
                else if (formID == (int)DocumentFormIDs.GenericMemo)
                {
                    childDocument = GenericMemo.GenerateNewFromTemplate(template);
                }
                else if (formID == (int)DocumentFormIDs.UserGenerated)
                {
                    continue;
                }

                
                childDocument.ParentDocumentID = headDocumentID;
                childDocument.SoldierID = soldier.SoldierID;

                childDocument.Save();

                progress++;

                float p = progress;
                float c = templatesInGroup.Count;
                int value = (int)(p / c * 100);

                parameters.backgroundWorker.ReportProgress( value );
            }

            //Refresh();
        }

    }
}
