using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    static class Model
    {
        //public static DataSet GetAllUnitInfo()
        //{
        //    DataSet unitInformation = new DataSet();

        //    try
        //    {
        //        DataTable unitTable = DatabaseConnection.GetTable("units");
        //        DataTable platoonsTable = DatabaseConnection.GetTable("platoons");
        //        DataTable squadssectionsTable = DatabaseConnection.GetTable("squadsections");

        //        unitTable.TableName = "units";
        //        platoonsTable.TableName = "platoons";
        //        squadssectionsTable.TableName = "squadsections";

        //        unitInformation.Tables.Add(unitTable);
        //        unitInformation.Tables.Add(platoonsTable);
        //        unitInformation.Tables.Add(squadssectionsTable);
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("An error occured while attempting to load the unit hierarchy.", ex);
        //    }

        //    return unitInformation;
        //}


        //public static DataTable GetDocumentInfo(int generatedDocumentID)
        //{
        //    string query = "select * from usergenerateddocs "
        //        + " inner join "
        //        + " documentnames "
        //        + " on usergenerateddocs.documentnameid = documentnames.documentnameid "
        //        + " inner join "
        //        + " formdatatypes "
        //        + " on usergenerateddocs.formdatatypenameid = formdatatypes.formdatatypeid "
        //        + " where usergenerateddocs.generateddocid = " + generatedDocumentID.ToString();

        //    try
        //    {
        //        DataTable documentInfo = DatabaseConnection.Query(query);
        //        return documentInfo;
        //    }
        //    catch ( QueryFailedException ex )
        //    {
        //        throw new DataLoadFailedException("Could not retrieve document information for "
        //            + "selected document.\n", ex);
        //    }
        //}


        //public static DataTable GetSoldierDatabaseValues(int soldierID)
        //{
        //    DataTable soldierValues;

        //    try
        //    {
        //        soldierValues = DatabaseConnection.Query("select * from soldiers where soldierid = " + soldierID);
        //        if (soldierValues.Rows.Count == 0)
        //            throw new DataLoadFailedException("Could not retrieve soldier from the database.");
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve soldier from the database.", ex);
        //    }
        //    catch (DataLoadFailedException ex)
        //    {
        //        throw ex;
        //    }

        //    return soldierValues;
        //}


        //public static DataTable GetSoldierStatuses(int soldierID)
        //{
        //    DataTable soldierStatusValues;

        //    try
        //    {
        //        soldierStatusValues = databaseConnection.Query("select * from soldierstatuses "
        //            + " inner join soldierstatusenums "
        //            + " on soldierstatuses.statusenumid = soldierstatusenums.statusenumid "
        //            + " where soldierid = " + soldierID);
        //    }
        //    catch (QueryFailedException)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve status information for selected soldier");
        //    }
        //    catch (DataLoadFailedException ex)
        //    {
        //        throw ex;
        //    }

        //    return soldierStatusValues;
        //}

        //public static void RefreshSoldierStatusTable()
        //{
        //    try
        //    {
        //        soldierStatusTable = DatabaseConnection.GetTable("soldierstatuses");
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve the soldier status information.", ex);
        //    }
        //    catch (DataLoadFailedException ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public static DataTable SoldiersStatusTable
        //{
        //    get
        //    {
        //        if (soldierStatusTable == null)
        //            RefreshSoldierStatusTable();

        //        return soldierStatusTable;
        //    }
        //}



        //public static DataTable GetXFDLValuesForForm(int generateddocid)
        //{
        //    string query = "select * from usergenerateddocs "
        //        + " inner join "
        //        + " formfields "
        //        + " on usergenerateddocs.formid = formfields.formid "
        //        + " inner join "
        //        + " usergeneratedvalues "
        //        + " on usergeneratedvalues.formfieldid = "
        //        + "    formfields.formfieldid "
        //        + " inner join "
        //        + " documentnames "
        //        + " on documentnames.documentnameid = usergenerateddocs.documentnameid "
        //        + " where usergenerateddocs.generateddocid=" + generateddocid
        //        + " and usergeneratedvalues.generateddocid = usergenerateddocs.generateddocid";

        //    try
        //    {
        //        DataTable usergeneratedvalues = databaseConnection.Query(query);
        //        return usergeneratedvalues;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve values for selected "
        //            + "document.", ex);
        //    }
        //}


        //public static void RefreshFormattedSoldiersTable()
        //{
        //    //List<string> tables = new List<string>(new string[] { "soldiers", "rankings" });
        //    //List<string> columns = new List<string>(new string[] { "rankingid", "rankingid" });

        //    //formattedSoldiersTable 
        //    //    = databaseConnection.GetJoinedTables(tables, columns, "inner join");
        //    //formattedSoldiersTable.TableName = "soldierstable";

        //    //List<string> tables = new List<string>(new string[] { "soldiers", "rankings", "squadsections", "platoons", "units" });
            
        //    formattedSoldiersTable
        //        = DatabaseConnection.Query("select * from soldiers "
        //                                    + " inner join "
        //                                    + " rankings "
        //                                    + " on soldiers.rankingid = rankings.rankingid "
        //                                    + " inner join "
        //                                    + " squadsections "
        //                                    + " on soldiers.squadsectionid = squadsections.squadsectionid "
        //                                    + " inner join "
        //                                    + " platoons "
        //                                    + " on squadsections.platoonid = platoons.platoonid "
        //                                    + " inner join "
        //                                    + " units "
        //                                    + " on platoons.unitid = units.unitid");
        //    formattedSoldiersTable.TableName = "soldierstable";
        //}


        //public static void RefreshSoldierImageList()
        //{
        //    foreach (DataRow row in formattedSoldiersTable.Rows)
        //    {
        //        int soldierID = Convert.ToInt32(row["soldierid"]);

        //    }
        //}


        //public static DataTable FormattedSoldiersTable
        //{
        //    get
        //    {
        //        //if (formattedSoldiersTable == null)
        //        RefreshFormattedSoldiersTable();

        //        return formattedSoldiersTable;
        //    }
        //}


        //public static DataTable UserGeneratedDocumentsTable
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (userGeneratedDocumentsTable == null)
        //            {
        //                RefreshUserGeneratedDocumentsTable();
        //            }

        //            return userGeneratedDocumentsTable;
        //        }
        //        catch (QueryFailedException ex)
        //        {
        //            throw new DataLoadFailedException(ex.Message);
        //        }
        //    }
        //}


        //public static DataView UserGeneratedDocumentsDataView
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (userGeneratedDocumentsTable == null)
        //            {
        //                RefreshUserGeneratedDocumentsTable();
        //            }

        //            return userGeneratedDocumentsDataView;
        //        }
        //        catch (QueryFailedException ex)
        //        {
        //            throw new DataLoadFailedException(ex.Message);
        //        }
        //    }
        //}


        //public static void RefreshUserGeneratedDocumentsTable(int selectedSoldierID)
        //{
        //    userGeneratedDocumentsDataView.RowFilter = "soldierid = " + selectedSoldierID
        //                                                + " and deleted = 0";
        //}


        //public static void RefreshUserGeneratedDocumentsTable()
        //{
        //    try
        //    {
        //        userGeneratedDocumentsTable 
        //            = databaseConnection.Query("select * from usergenerateddocs "
        //            + " inner join documentstatus "
        //            + "    on usergenerateddocs.statusid = documentstatus.documentstatusid "
        //            + " inner join documenttypes "
        //            + "    on usergenerateddocs.documenttypeid = documenttypes.documenttypeid "
        //            + " inner join documentnames "
        //            + "    on usergenerateddocs.documentnameid = documentnames.documentnameid "
        //            + " inner join formdatatypes "
        //            + "   on usergenerateddocs.formdatatypenameid = formdatatypes.formdatatypeid "
        //            + " inner join (select soldierid, lastname, firstname, rankingid from soldiers) as s "
        //            + "   on usergenerateddocs.soldierid = s.soldierid "
        //            + " inner join rankings "
        //            + "   on s.rankingid = rankings.rankingid "
        //            + " where parentdocumentid=-1 "
        //            + " order by usergenerateddocs.date asc ");

                    
        //        userGeneratedDocumentsTable.TableName = "documentstable";
        //        userGeneratedDocumentsDataView = userGeneratedDocumentsTable.AsDataView();

        //        DataColumn[] primaryKeyColumn = new DataColumn[] { userGeneratedDocumentsTable.Columns["generateddocid"] };
        //        userGeneratedDocumentsTable.PrimaryKey = primaryKeyColumn;
                
        //        List <DataTable>childDataTables = new List<DataTable>();

        //        foreach (DataRow row in userGeneratedDocumentsTable.Rows)
        //        {
        //            DataTable childDocs
        //                = databaseConnection.Query("select * from usergenerateddocs "
        //            + " inner join documentstatus "
        //            + "    on usergenerateddocs.statusid = documentstatus.documentstatusid "
        //            + " inner join documenttypes "
        //            + "    on usergenerateddocs.documenttypeid = documenttypes.documenttypeid "
        //            + " inner join documentnames "
        //            + "    on usergenerateddocs.documentnameid = documentnames.documentnameid "
        //            + " inner join formdatatypes "
        //            + "   on usergenerateddocs.formdatatypenameid = formdatatypes.formdatatypeid "
        //            //+ " inner join soldiers "
        //            + " inner join (select soldierid, lastname, firstname from soldiers) as s "
        //            + "   on usergenerateddocs.soldierid = s.soldierid "
        //            + " where parentdocumentid=" + row["generateddocid"].ToString()
        //            + " order by usergenerateddocs.date asc ");

        //            childDataTables.Add(childDocs);
        //        }

        //        foreach (DataTable childDocsTable in childDataTables)
        //        {
        //            if (childDocsTable.Rows.Count == 0)
        //                continue;

        //            int parentDocID = Convert.ToInt32(childDocsTable.Rows[0]["parentDocumentID"]);

        //            DataRow row = userGeneratedDocumentsTable.Rows.Find(parentDocID);
        //            int index = userGeneratedDocumentsTable.Rows.IndexOf(row);

        //            foreach (DataRow childRow in childDocsTable.Rows)
        //            {
        //                DataRow newRow = userGeneratedDocumentsTable.NewRow();
        //                newRow.ItemArray = childRow.ItemArray;
        //                string name = newRow["documentnametext"].ToString();
        //                newRow["documentnametext"] = "        " + name;
        //                userGeneratedDocumentsTable.Rows.InsertAt(newRow, index+1);
        //                index++;
        //            }
        //        }

        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException(ex.Message);
        //    }
        //}


        



        //internal static bool Initialize(string databaseFile)
        //{
        //    dataSet = new DataSet();
            
        //    userGeneratedDocumentsTable = null;
        //    formattedSoldiersTable = null;

        //    databaseConnection = new DatabaseConnection();
        //    bool isConnected = databaseConnection.Initialize(databaseFile);

        //    if (!isConnected)
        //        throw new DataLoadFailedException("Could not open database file : "
        //            + databaseFile + "\n");

        //    //RefreshFormattedSoldiersTable();
        //    //RefreshUserGeneratedDocumentsTable();
        //    //RefreshDocumentNamesTable();

        //    //dataSet.Tables.Add(formattedSoldiersTable);
        //    //dataSet.Tables.Add(userGeneratedDocumentsTable);

        //    //DataColumn parentColumn = dataSet.Tables["soldierstable"].Columns["soldierid"];
        //    //DataColumn childColumn = dataSet.Tables["documentstable"].Columns["soldierid"];

        //    return isConnected;
        //}


        //public static DataTable GetFormFieldsForDocument(XFDLDocumentModel document)
        //{
        //    DataTable userGeneratedValues = new DataTable();
        //    int docID = document.GeneratedDocID;

        //    try
        //    {
        //        string query = "select * from usergeneratedvalues "
        //                        + " inner join "
        //                        + " formfields "
        //                        + " on usergeneratedvalues.formfieldid = formfields.formfieldid "
        //                        + " where generateddocid = " + docID;
        //        userGeneratedValues = databaseConnection.Query(query);
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve the data for " + document.Name + "\n"
        //                                            + "Unable to save document.", ex);
        //    }

        //    return userGeneratedValues;
        //}


        //internal static void SaveAssessment(XFDLDocumentModel document)
        //{
        //    DataTable xfdlFormFields = new DataTable();
            
        //    try
        //    {
        //        xfdlFormFields = GetFormFieldsForDocument(document);

        //        int pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Assessment");
        //        SaveDocumentValue(pkid, document.Assessment);

        //        pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Date Assessment Performed");
        //        SaveDocumentValue(pkid, document.DateAssessmentPerformed.Ticks.ToString());
        //    }
        //    catch(DataLoadFailedException ex)
        //    {
        //        throw ex;
        //    }
        //}


        //internal static void SaveColumn(string tablename, string keyColumn, int pkid, string updateColumn, string updateValue)
        //{
        //    try
        //    {
        //        DatabaseConnection.Update(tablename, updateColumn, updateValue, keyColumn, pkid.ToString());
        //    }
        //    catch (NonQueryFailedException ex)
        //    {
        //        throw ex;
        //    }
        //}



        //internal static void SaveDocumentValue(int pkid, string value)
        //{
        //    try
        //    {
        //        SaveColumn("usergeneratedvalues", "generatedvalueid", pkid, "generatedvaluetext", value);
        //    }
        //    catch (NonQueryFailedException ex)
        //    {
        //        throw new DataStoreFailedException("An error occurred attempting to save " + value + " for the current document.", ex);
        //    }
        //}


        //internal static void InsertTuple(string tablename, string firstColumn, string firstValue, string secondColumn, string secondValue)
        //{
        //    try
        //    {
        //        databaseConnection.Insert("insert into " + tablename + " (" + firstColumn + ", " + secondColumn + ") "
        //            + " values (" + firstValue + ", " + secondValue + ")");
        //    }
        //    catch (NonQueryFailedException)
        //    {
        //        throw new DataStoreFailedException("An error occured attempting to save " + firstValue + ", " + secondValue + " to the database ");
        //    }
        //}
        

        //internal static int GetPKIDForDocumentEntry(DataTable values, string label)
        //{
        //    try
        //    {
        //        int pkid = Convert.ToInt32(values.Select("fieldlabel = '" + label + "'")[0]["generatedvalueid"]);
        //        return pkid;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DataLoadFailedException("An error occured while attempting to save the document.", ex);
        //    }
        //}


        //internal static void SaveDocumentValues(DataTable userGeneratedValues, XFDLDocumentModel document)
        //{
        //    int pkid;

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Name (Last, First, MI)");
        //    SaveDocumentValue(pkid, document.Name);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Rank/Grade");
        //    SaveDocumentValue(pkid, ((int)document.Rank).ToString());

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date of Counseling");
        //    SaveDocumentValue(pkid, document.DateOfCounseling.Ticks.ToString());

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Organization");
        //    SaveDocumentValue(pkid, document.NameOfOrganization);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Name and Title of Counselor");
        //    SaveDocumentValue(pkid, document.NameAndTitleOfCounselor);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Purpose of Counseling");
        //    SaveDocumentValue(pkid, document.PurposeOfCounseling);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Key Points of Discussion");
        //    SaveDocumentValue(pkid, document.KeyPointsOfDiscussion);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Plan of Actions");
        //    SaveDocumentValue(pkid, document.PlanOfActions);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "I agree");
        //    SaveDocumentValue(pkid, Convert.ToInt32(document.IAgree).ToString());

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "I disagree");
        //    SaveDocumentValue(pkid, Convert.ToInt32(document.IDisagree).ToString());

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Session Closing");
        //    SaveDocumentValue(pkid, document.SessionClosing);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Leader Responsibilities");
        //    SaveDocumentValue(pkid, document.LeaderResponsibilities);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Assessment");
        //    SaveDocumentValue(pkid, document.Assessment);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Counselor");
        //    SaveDocumentValue(pkid, document.Counselor);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Individual Counseled");
        //    SaveDocumentValue(pkid, document.IndividualCounseled);

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date of Assessment");
        //    SaveDocumentValue(pkid, document.DateOfAssessment.Ticks.ToString());

        //    pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date Assessment Performed");
        //    SaveDocumentValue(pkid, document.DateAssessmentPerformed.Ticks.ToString());
        //}


        //internal static void SaveDocumentHeaderInfo(XFDLDocumentModel document)
        //internal static void SaveDocumentHeaderInfo(DocumentModel document)
        //{
        //    int pkid = document.GeneratedDocID;
        //    string pkidStr = pkid.ToString();

        //    string documentNameIDStr = GetDocumentNameID(document.DocumentName).ToString();
        //    string statusIDStr = ((int)document.Status).ToString();
        //    string dateString = document.Date.Ticks.ToString();
        //    string parentDocumentIDString = document.ParentDocumentID.ToString();
        //    string soldierIDStr = document.SoldierID.ToString();
            
        //    string userGeneratedStr = document.UserUploaded ? "1" : "0";

        //    try
        //    {
        //        DatabaseConnection.Update("usergenerateddocs", "soldierid", soldierIDStr, "generateddocid", pkidStr);
        //        DatabaseConnection.Update("usergenerateddocs", "statusid", statusIDStr, "generateddocid", pkidStr);
        //        DatabaseConnection.Update("usergenerateddocs", "date", dateString, "generateddocid", pkidStr);
        //        DatabaseConnection.Update("usergenerateddocs", "parentdocumentid", parentDocumentIDString, "generateddocid", pkidStr);

        //        DatabaseConnection.Update("usergenerateddocs", "documentnameid", documentNameIDStr, "generateddocid", pkidStr);
        //        DatabaseConnection.Update("usergenerateddocs", "filepath", document.Filepath, "generateddocid", pkidStr);

        //        DatabaseConnection.Update("usergenerateddocs", "usergenerated", userGeneratedStr, "generateddocid", pkidStr);

        //        DatabaseConnection.Backup();

        //    }
        //    catch (DataStoreFailedException ex)
        //    {
        //        throw new DataStoreFailedException("An error occurred attempting to save the document.", ex);
        //    }
        //}


        //internal static int GenerateXFDLDocument(/*int soldierID,*/ XFDLDocumentModel document /*, int parentDocumentID*/)
        //{
        //    //int generatedDocID = InsertNewXFDLDocument(soldierID, document, parentDocumentID);
        //    int generatedDocID = InsertNewXFDLDocument(/*soldierID,*/ document /*, parentDocumentID*/);
        //    document.GeneratedDocID = generatedDocID;

        //    GenerateFormFieldsForXFDL(document);

        //    return generatedDocID;
        //}


        //internal static void GenerateFormFieldsForXFDL(XFDLDocumentModel document)
        //{
        //    DataTable formFields = databaseConnection.Query("select * from formfields "
        //                                                    + " where formid = 1");

        //    foreach (DataRow row in formFields.Rows)
        //    {
        //        databaseConnection.Insert("insert into usergeneratedvalues "
        //            + " (generateddocid, formid, formfieldid, generatedvaluetext) "
        //            + " values ("
        //            + document.GeneratedDocID + ", "
        //            + 1 + ", "
        //            + row["formfieldid"].ToString() + ", "
        //            + " \"\" )");
        //    }
        //}


        //internal static int InsertNewXFDLDocument(/*int soldierID,*/ XFDLDocumentModel document /*, int parentDocumentID*/)
        //internal static void InsertNewDocument(Document document)
        //{
        //    DateTime date = document.Date;
            
        //    int soldierID = document.SoldierID;
        //    int parentDocumentID = document.ParentDocumentID;

        //    int documentNameID = GetDocumentNameID(document.DocumentName);
        //    int documentTypeID = 1;
        //    int formID = 1;
        //    int userGenerated = 0;
        //    string filepath = "";
        //    int statusID = (int) document.Status;
        //    int formDataType = 1;

        //    string insertCommand 
        //        = "insert into usergenerateddocs ("
        //        + " soldierid, date, documentnameid, "
        //        + " documenttypeid, formid, usergenerated, "
        //        + " filepath, statusid, parentdocumentid, formdatatypenameid ) "
        //        + " values ( "
        //        + soldierID + ", " + date.Ticks + ", " + documentNameID + ", "
        //        + documentTypeID + ", " + formID + ", " + userGenerated + ", "
        //        + "\"" + filepath + "\", " + statusID + ", \"" + parentDocumentID + "\", " + formDataType + " )";

        //    DatabaseConnection.Insert(insertCommand);

        //    int generatedDocID = DatabaseConnection.GetLastInsertID();
        //    document.GeneratedDocID = generatedDocID;

        //    //return generatedDocID;
        //}


        //internal static int GetDocumentNameID(string documentName)
        //{
        //    //DataTable names = databaseConnection.GetTable("documentnames");
        //    //DataRow[] rows = names.Select("documentnametext = '" + documentName + "'");

        //    DataRow[] rows = documentNamesTable.Select("documentnametext = '" + documentName + "'");
        //    int documentNameID;

        //    if (rows.Count() != 1)
        //    {
        //        documentNameID = CreateNewDocumentNameEntry(documentName);
        //        RefreshDocumentNamesTable();
        //    }
        //    else
        //    {
        //        documentNameID = Convert.ToInt32(rows[0]["documentnameid"]);
        //    }

        //    return documentNameID;
        //}


        //internal static int CreateNewDocumentNameEntry(string documentName)
        //{
        //    databaseConnection.Insert("insert into documentnames ( documentnametext ) "
        //        + " values (\"" + documentName + "\")");

        //    RefreshDocumentNamesTable();

        //    int documentNameID = GetDocumentNameID(documentName);
        //    return documentNameID;
        //}


        //internal static string GetDocumentName(int documentID)
        //{
        //    DataRow[] rows = documentNamesTable.Select("documentnameid = " + documentID);
        //    if (rows.Length != 1)
        //        throw new DataLoadFailedException("Could not locate document name.");

        //    string documentName = rows[0]["documentnametext"].ToString();
        //    return documentName;
        //}

        //internal static void SaveXFDLDocument(XFDLDocumentModel document)
        //{
        //    DataTable userGeneratedValues = new DataTable();

        //    try
        //    {
        //        userGeneratedValues = GetFormFieldsForDocument(document);
        //        SaveDocumentHeaderInfo(document);
        //        SaveDocumentValues(userGeneratedValues, document);
        //    }
        //    catch (DataLoadFailedException ex)
        //    {
        //        throw new DataLoadFailedException("An error occurred while trying to save the document.\n"
        //            + "Unable to save document.", ex);
        //    }
        //}


        //public static DataTable GetAllStatusEnums()
        //{
        //    try
        //    {
        //        DataTable statusEnums = databaseConnection.GetTable("soldierstatusenums");
        //        return statusEnums;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve soldier statuses.", ex);
        //    }
        //}


        //public static int GetPlatoonIDForSquadSection(int squadSectionID)
        //{
        //    DataTable results;
        //    int platoonid;
        //    string error = "Could not retrieve the unit hierarchy for the selected soldier";

        //    try
        //    {
        //        results = databaseConnection.Query("select * from squadsections where squadsectionid = " + squadSectionID);
                
        //        if (results.Rows.Count != 1)
        //            throw new DataLoadFailedException("Could not retrieve the unit hierarchy for the selected soldier");

        //        platoonid = Convert.ToInt32(results.Rows[0]["platoonid"]);
                
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //    catch (DataException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve the unit hierarchy for the selected soldier", ex);
        //    }
        //    catch (InvalidCastException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve the unit hierarchy for the selected soldier", ex);
        //    }


        //    return platoonid;
        //}


        //public static string GetPlatoonNameForPlatoonID(int platoonID)
        //{
        //    string error = "Could not retrieve the unit hierarchy for the selected soldier";

        //    try
        //    {
        //        string platoonName = databaseConnection.GetSingleValue("platoons", "platoonid", platoonID, "platoonname");
        //        return platoonName;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //}


        //internal static string GetSectionSquadNameForSquadID(int sectionSquadID)
        //{
        //    string error = "Could not retrieve the unit hierarchy for the selected soldier";

        //    try
        //    {
        //        string sectionSquadName = databaseConnection.GetSingleValue("squadsections", "squadsectionid", sectionSquadID, "squadsectionname");
        //        return sectionSquadName;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //}


        //internal static int GetUnitIDForPlatoonID(int platoonID)
        //{
        //    string error = "Could not retrieve the unit hierarchy for the selected soldier";

        //    try
        //    {
        //        string unitID = databaseConnection.GetSingleValue("platoons", "platoonid", platoonID, "unitid");
        //        return Convert.ToInt32(unitID);
        //    }
        //    catch (InvalidCastException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //}


        //internal static string GetUnitNameForUnitID(int unitID)
        //{
        //    string error = "Could not retrieve the unit hierarhy for the selected soldier";

        //    try
        //    {
        //        string unitName = databaseConnection.GetSingleValue("units", "unitid", unitID, "unitname");
        //        return unitName;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //}



        //internal static void SaveSoldierValues(Soldier soldier)
        //{
        //    int pkid = soldier.SoldierID;
        //    string table = "soldiers";
        //    string key = "soldierID";

        //    SaveColumn(table, key, pkid, "rankingid", ((int)soldier.Rank).ToString());
        //    SaveColumn(table, key, pkid, "lastname", soldier.LastName);
        //    SaveColumn(table, key, pkid, "firstname", soldier.FirstName);
        //    SaveColumn(table, key, pkid, "middleinitial", soldier.MiddleInitial.ToString());
        //    SaveColumn(table, key, pkid, "dateofrank", soldier.DateOfRank.Ticks.ToString());
        //    SaveColumn(table, key, pkid, "dateofbirth", soldier.DateOfBirth.Ticks.ToString());
        //    SaveColumn(table, key, pkid, "otherstatustext", soldier.OtherStatus);
        //    SaveColumn(table, key, pkid, "imagefilepath", soldier.PictureFilename);
        //    SaveColumn(table, key, pkid, "squadsectionid", soldier.SquadSectionID.ToString());
        //}


        internal static void SaveColumn(string tablename, string keyColumn, int pkid, string updateColumn, string updateValue)
        {
            try
            {
                DatabaseConnection.Update(tablename, updateColumn, updateValue, keyColumn, pkid.ToString());
            }
            catch (NonQueryFailedException ex)
            {
                throw ex;
            }
        }


        //internal static void SaveSoldierStatuses(SoldierModel soldier)
        //{
        //    int soldierID = soldier.SoldierID;
        //    string table = "soldierstatuses";
        //    string soldierIDColumn = "soldierid";
        //    string statusEnumColumn = "statusenumid";

        //    databaseConnection.Delete(table, soldierIDColumn, soldierID.ToString());

        //    foreach (SoldierStatus status in soldier.Statuses)
        //    {
        //        if (status.applies == false)
        //            continue;

        //        InsertTuple(table, soldierIDColumn, soldierID.ToString(), statusEnumColumn, status.statusEnumID.ToString());
        //    }
        //}


        //internal static void SaveSoldier(SoldierModel soldier)
        //{
        //    try
        //    {
        //        SaveSoldierValues(soldier);
        //        SaveSoldierStatuses(soldier);
        //    }
        //    catch ( DataStoreFailedException ex )
        //    {
        //        throw new DataStoreFailedException("An error occurred attempting to save the soldier.", ex);
        //    }
        //}


        //internal static string GetFormTemplateFromFormID(int formID)
        //{
        //    try
        //    {
        //        string templateFilename = databaseConnection.GetSingleValue("forms", "formid", formID, "filepath");
        //        return templateFilename;
        //    }
        //    catch(QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not locate the requested template document.", ex);
        //    }
        //}


        //internal static string GetFormFilename(int formID)
        //{
        //    try
        //    {
        //        string formFilename = databaseConnection.GetSingleValue("forms", "formid", formID, "filepath");
        //        return formFilename;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not locate the requested template document.", ex);
        //    }
        //}



        //internal static DataTable GetTable(string tablename)
        //{
        //    try
        //    {
        //        DataTable results = DatabaseConnection.GetTable(tablename);
        //        return results;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve table " + tablename, ex);
        //    }
        //}


        //internal static void SaveNewSoldier(SoldierModel soldier)
        //{
        //    StringBuilder insertCommand = new StringBuilder();
        //    insertCommand.Append("insert into soldiers (");
        //    insertCommand.Append("  rankingid, lastname, firstname, middleinitial, dateofrank, dateofbirth, ");
        //    insertCommand.Append("  otherstatustext, imagefilepath, squadsectionid");
        //    insertCommand.Append(") values (");
        //    insertCommand.Append(((int)soldier.Rank).ToString() + ", ");
        //    insertCommand.Append("\"" + soldier.LastName + "\" , ");
        //    insertCommand.Append("\"" + soldier.FirstName + "\" , ");
        //    insertCommand.Append("\"" + soldier.MiddleInitial + "\",");
        //    insertCommand.Append(soldier.DateOfRank.Ticks.ToString() + ", ");
        //    insertCommand.Append(soldier.DateOfBirth.Ticks.ToString() + ", ");
        //    insertCommand.Append("\"" + soldier.OtherStatus + "\", ");
        //    insertCommand.Append("\"" + soldier.PictureFilename + "\", ");
        //    insertCommand.Append(soldier.SquadSectionID.ToString());
        //    insertCommand.Append(")");

        //    try
        //    {
        //        databaseConnection.Insert(insertCommand.ToString());
        //    }
        //    catch (NonQueryFailedException ex)
        //    {
        //        throw new DataStoreFailedException("An error occured attempting to save the soldier information.", ex);
        //    }

        //    SaveSoldierStatuses(soldier);
        //}


        //internal static DataTable GetRankingTable()
        //{
        //    if (rankingsTable != null)
        //        return rankingsTable;

        //    try
        //    {
        //        rankingsTable = databaseConnection.GetTable("rankings");
        //        return rankingsTable;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve the rankings table.", ex);
        //    }
        //}


        //internal static List<Image> GetRankingImages()
        //{
        //    if (rankingImages != null)
        //        return rankingImages;

        //    rankingImages = new List<Image>();

        //    if (rankingsTable == null)
        //        GetRankingTable();

        //    foreach (DataRow row in rankingsTable.Rows)
        //    {
        //        Image image = new Bitmap(row["rankingimagepath"].ToString());
        //        rankingImages.Add(image);
        //    }

        //    return rankingImages;
        //}


        //internal static DataTable GetDocumentStatuses()
        //{
        //    if (documentStatuses == null)
        //        documentStatuses = databaseConnection.GetTable("documentstatus");

        //    return documentStatuses;
        //}


        //internal static void RefreshDocumentNamesTable()
        //{
        //    documentNamesTable = databaseConnection.GetTable("documentnames");
        //}

        //internal static DataTable DocumentNamesTable
        //{
        //    get
        //    {
        //        return documentNamesTable;
        //    }
        //}


        //internal static bool DocumentNameExists(string documentName)
        //{
        //    RefreshDocumentNamesTable();

        //    if (documentNamesTable.Select("documentnametext = " + documentName).Count() > 0)
        //        return true;

        //    return false;
        //}


        //private static void RefreshTemplatesTable()
        //{
        //    string query 
        //        = "select * from templates "
        //        + " inner join "
        //        + " templategroups "
        //        + " on templates.templategroupid = templategroups.templategroupid "
        //        + " inner join "
        //        + " documentnames "
        //        + " on templates.documentnameid = documentnames.documentnameid "
        //        + " order by templates.templategroupid";

        //    try
        //    {
        //        templatesTable = databaseConnection.Query(query);
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw ex;
        //    }
        //}


        //internal static DataTable TemplatesTable
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (templatesTable == null)
        //                RefreshTemplatesTable();

        //            return templatesTable;
        //        }
        //        catch (QueryFailedException ex)
        //        {
        //            throw new DataLoadFailedException("An error occurred attempting to load the templates", ex);
        //        }
        //    }
        //}


        //private static void RefreshTemplateGroupsTable()
        //{
        //    try
        //    {
        //        templateGroupsTable = databaseConnection.GetTable("templategroups");
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw ex;
        //    }
        //}


        //internal static DataTable TemplateGroups
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (templateGroupsTable == null)
        //                RefreshTemplateGroupsTable();

        //            return templateGroupsTable;
        //        }
        //        catch (QueryFailedException ex)
        //        {
        //            throw new DataLoadFailedException("An error occured attempting to load the templates", ex);
        //        }
        //    }
        //}


        //private static void RefreshCounselingChecklists()
        //{
        //    try
        //    {
        //        counselingChecklists = databaseConnection.GetTable("counselingchecklists");
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public static DataTable CounselingChecklists 
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (counselingChecklists == null)
        //                RefreshCounselingChecklists();

        //            return counselingChecklists;
        //        }
        //        catch (QueryFailedException ex)
        //        {
        //            throw new DataLoadFailedException("Error retrieveing the General, Specific, and Extract data for counselings.", ex);
        //        }
        //    }
        //}


        //internal static DataTable GetTemplateValuesByTemplateID(int templateID)
        //{
        //    string query = "select * from templates "
        //                    + " inner join "
        //                    + " templatevalues "
        //                    + " on templates.tempateid = templatevalues.templateid "
        //                    + " inner join "
        //                    + " formfields "
        //                    + " on templatevalues.formfieldid = formfields.formfieldid "
        //                    + " where templates.tempateid = \"" + templateID + "\"";

        //    try
        //    {
        //        DataTable results = databaseConnection.Query(query);
        //        return results;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Failed to load values for the selected template.", ex);
        //    }
        //}


        //internal static string GetTemplateNameByTemplateID(int templateID)
        //{
        //    string query = "select * from templates "
        //                    + " inner join "
        //                    + " documentnames "
        //                    + " on templates.documentnameid = documentnames.documentnameid "
        //                    + " where templates.tempateid = " + templateID;

        //    try
        //    {
        //        DataTable results = databaseConnection.Query(query);
        //        string templateName = results.Rows[0]["documentnametext"].ToString();
        //        return templateName;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Failed to retrieve document name", ex);
        //    }

        //}


        //internal static List<int> GetTemplatesInTemplatePackage(int headTemplateID)
        //{
        //    List<int> templateIDs = new List<int>();

        //    DataTable counselingPackages = CounselingPackagesForTemplateID(headTemplateID);
        //    //templateIDs.Add(headTemplateID);

        //    foreach (DataRow counselingPackage in counselingPackages.Rows)
        //    {
        //        int templateID = Convert.ToInt32(counselingPackage["additionaltemplateid"]);
        //        templateIDs.Add(templateID);
        //    }

        //    return templateIDs;
        //}


        //internal static DataTable CounselingPackagesForTemplateID(int headTemplateID)
        //{
        //    DataTable counselingPackage = databaseConnection.Query("select * from counselingpackagegroups"
        //                                                            + " where headtemplateid = " + headTemplateID);
            
        //    if ( counselingPackage.Rows.Count == 0 )
        //        return new DataTable();

        //    int counselingPackageID = Convert.ToInt32( counselingPackage.Rows[0]["counselingpackageid"] );

        //    DataTable counselingPackageEntries = databaseConnection.Query("select * from counselingpackagedocuments "
        //                                                                    + " where counselingpackageid = " + counselingPackageID);

        //    return counselingPackageEntries;
        //}


        //internal static List<string> GetNamesForTemplateIDs(List<int> templateIDs)
        //{
        //    List<string> templateNames = new List<string>();
            
        //    //List<string> tables = new List<string>{"templates", "documentnames"};
        //    //List<string> columns = new List<string>{"documentnameid", "documentnameid"};

        //    //DataTable templates = databaseConnection.GetJoinedTables(tables, columns, "inner join");
        //    DataTable templates = databaseConnection.Query("select * from templates inner join documentnames "
        //                             + " on templates.documentnameid = documentnames.documentnameid ");

        //    foreach (DataRow template in templates.Rows)
        //    {
        //        int templateID = Convert.ToInt32(template["tempateid"]);
        //        if ( templateIDs.Contains(templateID) == false )
        //            continue;

        //        string templateName = template["documentnametext"].ToString();
        //        templateNames.Add(templateName);
        //    }

        //    return templateNames;
        //}

        // @TODO
        //internal static void GenerateTemplateInserts(int headDocumentID, List<int> templatesInGroup, int soldierID)
        //{
        //    throw new Exception("Depreciated! - Do not use this function anymore.");

        //    //int primaryDocumentID = -1; // templatesInGroup[0];

        //    //foreach (int templateID in templatesInGroup)
        //    //{
        //    //    string fileTypeStr = databaseConnection.GetSingleValue("templates", "tempateid", templateID, "formid");
        //    //    int fileTypeInt = Convert.ToInt32(fileTypeStr);

        //    //    if (fileTypeInt == 1)
        //    //    {
        //    //        XFDLDocumentModel xfdlDocumentModel = XFDLDocumentModel.GenerateNewFromTemplate(templateID);
        //    //        xfdlDocumentModel.SoldierID = soldierID;
        //    //        xfdlDocumentModel.DateOfAssessment = DateTime.Now + new TimeSpan(0, 0, 90);
        //    //        xfdlDocumentModel.ParentDocumentID = headDocumentID;

        //    //        Model.GenerateXFDLDocument(xfdlDocumentModel/*, primaryDocument*/);
        //    //        Model.SaveXFDLDocument(xfdlDocumentModel);

        //    //    }
        //    //}

        //   // databaseConnection.Update("update usergenerateddocs set parentdocumentid=-1 where generateddocid=" + primaryDocumentID);
        //}

        // @TODO
        //internal static FileUtils.FileType GetTemplateDocumentType(int templateID)
        //{
        //    //List<string> tables = new List<string> { "templates", "forms", "formdatatypes", " };
            
        //    //DocumentModel document = DocumentModel.LoadGeneratedDocument(templateID);
        //    //if (document.FormID == 1)
        //    //    return FileUtils.FileType.XFDL;
        //    //else if (document.FormID == 2)
        //    //    return FileUtils.FileType.DOC;
        //    //else if (document.FormID == 3)
        //    //    return FileUtils.FileType.PDF;
        //    //else if (document.FormID == -1)
        //    //    return FileUtils.FileType.UserGenerated;
        //    //else
        //    //    return FileUtils.FileType.Unknown;

        //    //FileUtils.FileType formID = Model.GetTemplateDocumentType(templateID);
        //    int formID = Convert.ToInt32(databaseConnection.GetSingleValue("templates", "templateid", templateID, "formid"));
        //    FileUtils.FileType fileType = (FileUtils.FileType)formID;
        //}

        //internal static DataTable GetNotesTableForSoldier(int selectedSoldierID)
        //{
        //    try
        //    {
        //        string query = "select * from notes "
        //                        + " inner join "
        //                        + " notevalues "
        //                        + " on notes.noteid = notevalues.noteid "
        //                        + " where soldierid = " + selectedSoldierID + " "
        //                        + " order by soldierid asc";
        //        DataTable notes = databaseConnection.Query(query);

        //        return notes;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve the notes "
        //                                            + "table for the selected soldier.", ex);
        //    }
        //}


        //internal static void InsertUserGeneratedCounseling(int soldierid, DateTime date, string documentTitle,
        //                                                   string filepath, int statusid, int parentDocumentID)
        //{
        //    int typeid = 1;
        //    int formid = -1;
        //    int usergenerated = 1;
        //    int formdatatypename = -1;

        //    int documenttitleid = Model.GetDocumentNameID(documentTitle);

        //    string insertCommand = "insert into usergenerateddocs ("
        //        + " soldierid, date, documentnameid, documenttypeid, formid, "
        //        + " usergenerated, filepath, statusid, formdatatypenameid, parentdocumentid ) "
        //        + " values ( "
        //        + " " + soldierid + ", " + date.Ticks + ", " + documenttitleid + ", " + typeid + ", " + formid + ", "
        //        + " " + usergenerated + ", \"" + filepath + "\" , " + statusid + ", " + formdatatypename + ", " + parentDocumentID
        //        + " ) ";

        //    databaseConnection.Insert(insertCommand);

        //}


        //internal static DataTable GetNoteValuesForNoteID(int noteID)
        //{
        //    try
        //    {
        //        DataTable noteValues = databaseConnection.Query("select * from notes "
        //                                                        + " inner join "
        //                                                        + " notevalues on notes.noteid = notevalues.noteid where notes.noteid=" + noteID);
        //        return noteValues;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Failed to load values for the selected note.", ex);
        //    }
        //}


        //internal static int CreateNewNoteForSoldier(DateTime date, int soldierID)
        //{
        //    databaseConnection.Insert("insert into notes (date, soldierid) values (" 
        //                                + date.Ticks + ", " + soldierID + ")");
        //    int noteID = databaseConnection.GetLastInsertID();

        //    databaseConnection.Insert("insert into notevalues (noteid, value, issubject) "
        //                                + "values (" + noteID + ", \"\", 1)");
        //    databaseConnection.Insert("insert into notevalues (noteid, value, issubject) "
        //                                + "values (" + noteID + ", \"\", 0)");

        //    return noteID;
        //}

        //internal static void UpdateNote(NoteInterface note)
        //{
        //    databaseConnection.Update("update notevalues set value = "
        //                                + "\"" + note.Subject + "\""
        //                                + " where noteid = " + note.NoteID
        //                                + " and issubject = 1");

        //    databaseConnection.Update("update notevalues set value = "
        //                                + "\"" + note.Comment + "\""
        //                                + " where noteid = " + note.NoteID
        //                                + " and issubject = 0");

        //}

        //internal static void DeleteSoldier(int selectedSoldierID)
        //{
        //    databaseConnection.Delete("soldiers", "soldierid", selectedSoldierID.ToString());

        //    DeleteAllDocumentsForSoldier(selectedSoldierID);
        //    DeleteAllNotesForSoldier(selectedSoldierID);
        //}

        //internal static void SendSoldierToRecycleBin(int soldierID)
        //{
        //    databaseConnection.Update("soldiers", "deleted", "1", "soldierid", soldierID.ToString());
        //}

        //internal static void RemoveSoldierFromRecyclingBin(int soldierID)
        //{
        //    databaseConnection.Update("soldiers", "deleted", "0", "soldierid", soldierID.ToString());
        //}

        //internal static void DeleteAllDocumentsForSoldier(int soldierID)
        //{
        //    foreach (DataRow row in databaseConnection.Query("select * from usergenerateddocs where soldierid=" + soldierID).Rows)
        //    {
        //        int generatedDocID = Convert.ToInt32(row["generateddocid"]);
        //        databaseConnection.Delete("usergeneratedvalues", "generateddocid", generatedDocID.ToString());
        //    }
            
        //    databaseConnection.Delete("usergenerateddocs", "soldierid", soldierID.ToString());
        //}

        //internal static void DeleteAllNotesForSoldier(int soldierID)
        //{
        //    foreach (DataRow row in GetNotesTableForSoldier(soldierID).Rows)
        //    {
        //        int noteID = Convert.ToInt32(row["noteid"]);
        //        databaseConnection.Delete("notes", "noteid", noteID.ToString());
        //        databaseConnection.Delete("notevalues", "noteid", noteID.ToString());
        //    }
        //}


        //internal static void RemoveDocumentFromRecycleBin(int documentID)
        //{
        //    databaseConnection.Update("usergenerateddocs", "deleted", "0", "generateddocid", documentID.ToString());
        //}


        //internal static void MoveDocumentToRecycleBin(int documentID)
        //{
        //    databaseConnection.Update("usergenerateddocs", "deleted", "1", "generateddocid", documentID.ToString());            
        //}


        //internal static void DeleteDocumentPermanently(int documentID)
        //{
        //    DocumentModel document = DocumentModel.LoadGeneratedDocument(documentID);
        //    if (document.UserUploaded)
        //        DocumentModel.RemoveUserDocument(document.SoldierID, document.Filepath);

        //    databaseConnection.Delete("usergenerateddocs", "generateddocid", documentID.ToString());
        //    databaseConnection.Delete("usergeneratedvalues", "generateddocid", documentID.ToString());
        //}

        //internal static void MoveDocumentAndChildrenToRecycleBin(int documentID)
        //{
        //    DataTable documents = databaseConnection.Query("select generateddocid from usergenerateddocs " 
        //                                                        + " where parentdocid = " + documentID.ToString());

        //    foreach (DataRow document in documents.Rows)
        //    {
        //        int childDocumentID = Convert.ToInt32(document["generateddocid"]);
        //        MoveDocumentToRecycleBin(childDocumentID);
        //    }

        //    MoveDocumentToRecycleBin(documentID);
        //}


        //internal static void ChangeDocumentParentDocument(int documentID, int newParentDocumentID)
        //{
        //    databaseConnection.Update("usergenerateddocs", "parentdocumentid", newParentDocumentID.ToString(),
        //                                "generateddocid", documentID.ToString());
        //}


        
        internal static void InsertTuple(string tablename, string firstColumn, string firstValue, string secondColumn, string secondValue)
        {
            try
            {
                string insertCommand = "insert into " + tablename + " (" + firstColumn + ", " + secondColumn + ") "
                    + " values (@firstValue, @secondValue)";
                Params paramValues = new Params();
                
                paramValues.Add("@firstValue", firstValue);
                paramValues.Add("@secondValue", secondValue);

                DatabaseConnection.Insert(insertCommand, paramValues);
            }
            catch (NonQueryFailedException)
            {
                throw new DataStoreFailedException("An error occured attempting to save " + firstValue + ", " + secondValue + " to the database ");
            }
        }
        


        //static DataSet dataSet;
        //static DataTable userGeneratedDocumentsTable;
        //static DataTable formattedSoldiersTable;
        ////static DatabaseConnection databaseConnection;
        //static DataView userGeneratedDocumentsDataView;
        //static DataTable rankingsTable;
        //static DataTable templatesTable;
        //static DataTable templateGroupsTable;
        //static List<Image> rankingImages;
        //static DataTable documentStatuses;
        //static DataTable documentNamesTable;
        //static DataTable counselingChecklists;
        //static DataTable soldierStatusTable;
        //static SortedDictionary<int, Image> soldierImages;

        internal static DataTable GetReferenceDocumentsTable()
        {
            DataTable results;

            //try
            //{
            //    string query = "select * from "
            //                 + "referencegroups "
            //                 + "inner join "
            //                 + "referencedocuments "
            //                 + "on referencegroups.referencegroupid = referencedocuments.referencegroupid ";
            //    results = DatabaseConnection.Query(query);
            //}
            //catch (QueryFailedException ex)
            //{
            //    throw ex;
            //}

            try
            {
                results = DatabaseConnection.GetTable("referencedocuments");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return results;
        }

        internal static DataTable GetReferenceGroupsTable()
        {
            DataTable results;

            try
            {
                results = DatabaseConnection.GetTable("referencegroups");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return results;
        }

        internal static DataTable GetHelpDocumentsTable()
        {
            DataTable results;

            try
            {
                results = DatabaseConnection.GetTable("helpdocuments");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return results;
        }

        internal static DataTable GetHelpDocumentGroupsTable()
        {
            DataTable results;

            try
            {
                results = DatabaseConnection.GetTable("helpdocumentgroups");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return results;
        }
    }
}
