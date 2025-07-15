using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;

namespace CounselQuickPlatinum
{
    enum DocumentFormIDs : int
    {
        UserGenerated = -1,
        DA4856PDF = 1,
        PregnancyElectionStatement = 2,
        ContinuationOfCounselingPDF = 3,
        GenericMemo = 4,
        Letter = 15,
        //DA4856PDF = 17,
        //ContinuationOfCounselingPDF = 18
    }

    enum DocumentSortMode
    {
        DateAsc,
        DateDesc,
        NameAsc,
        NameDesc,
        StatusAsc,
        StatusDesc,
        TypeAsc,
        TypeDesc,
        SoldierLastNameAsc,
        SoldierLastNameDesc,
        SoldierFirstNameAsc,
        SoldierFirstNameDesc,
        RankAsc,
        RankDesc
    }

    internal class DocumentModel
    {
        private static DataTable documentStatuses;
        private static DataTable documentNamesTable;
        private static DataTable parentDocumentsTable;
        private static List<DataTable> childDocumentTables;
        private static DataTable userGeneratedDocumentsTable;
        
        internal delegate void DocumentModelRefreshedEvent();
        internal static event DocumentModelRefreshedEvent documentModelRefreshed;

        private static Mutex documentsLock;
        private static Thread currentThread;
        private static int lockLevels;

        static DocumentModel()
        {
            lockLevels = 0;

            childDocumentTables = new List<DataTable>();

            SoldierModel.SoldierModelRefreshed = Refresh;
            documentsLock = new Mutex();
            
            Refresh();
        }

        protected static void Lock()
        {
            documentsLock.WaitOne();

            if (currentThread != Thread.CurrentThread)
            {
                Logger.Trace("NEW THREAD ACQUIRING DOCUMENTMODEL LOCK: " + ++lockLevels);
            }
            else if (currentThread == Thread.CurrentThread)
            {
                Logger.Trace("THREAD ALREADY HAS LOCK, LOCKING AGAIN: " + ++lockLevels);
                throw new Exception("ERROR:  DEADLOCK CONDITION POTENTIALLY:  DOUBLE LOCK");
            }

            currentThread = Thread.CurrentThread;

            if (lockLevels > 1)
                throw new Exception("ERROR:  SOMEHOW LOCKED WITH > 1 LOCKLEVELS");
        }

        protected static void Unlock()
        {
            if (currentThread != Thread.CurrentThread)
            {
                Logger.Trace("THREAD RELEASING LOCK != CURRENT THREAD..." + --lockLevels);
            }
            else
            {
                Logger.Trace("CURRENT LOCK HOLDER RELEASING LOCK: " + --lockLevels);
            }

            if (lockLevels == 0)
                currentThread = null;
            else
                throw new Exception("ERROR:  DEADLOCK CONDITION POTENTIALLY:  LOCK LEVELS > 0");

            documentsLock.ReleaseMutex();
        }


        internal static void CheckDocumentStatuses()
        {
            long nowTicks = DateTime.Now.Date.Ticks;
            List<string> commands = new List<string>();

            foreach (DataRow row in userGeneratedDocumentsTable.Rows)
            {
                int formID = Convert.ToInt32(row["formid"]);
                if(formID != (int)DocumentFormIDs.DA4856PDF)
                    continue;
                
                int documentID = Convert.ToInt32(row["generateddocid"]);

                DataTable values = GetUserGeneratedValuesForDocument(documentID);

                DA4856Document counseling = new DA4856Document(documentID);
                if (counseling.DateAssessmentDue.Date.Ticks > nowTicks)
                    continue;
                
                int statusID = (int)DocumentStatus.AssessmentDue;
                string command = "update usergenerateddocs set statusid = " + statusID + " where generateddocid = " + documentID;
                commands.Add(command);
            }

            DatabaseConnection.IssueBatchCommands(commands);
            
            Refresh();
        }


        internal static DataTable GetDocumentHeaderInfo(int generatedDocumentID)
        {
            
            Lock();

            string query = "select * from usergenerateddocs "
                + " inner join "
                + " documentnames "
                + " on usergenerateddocs.documentnameid = documentnames.documentnameid "
                + " where usergenerateddocs.generateddocid = " + generatedDocumentID.ToString();

            DataTable documentInfo;

            try
            {
                documentInfo = DatabaseConnection.Query(query);
                
            }
            catch (QueryFailedException ex)
            {
                Unlock();
                throw new DataLoadFailedException("Could not retrieve document information for "
                    + "selected document.\n", ex);
            }

            Unlock();
            return documentInfo;
        }


        protected static void GenerateFormFieldsForFormType(Document document)
        {
            DataTable formFields = DatabaseConnection.Query("select * from formfields "
                                                            + " where formid = " + document.FormID);

            foreach (DataRow row in formFields.Rows)
            {
                string insertCommand 
                    = "insert into usergeneratedvalues "
                    + " (generateddocid, formid, formfieldid, generatedvaluetext) "
                    + " values (@generatedDocID, 1, @formFieldID, \"\")";

                string docID = document.GeneratedDocID.ToString();
                string formFieldID = row["formfieldid"].ToString();

                Params paramValues = new Params();
                paramValues.Add("@generatedDocID", docID);
                paramValues.Add("@formFieldID", formFieldID);

                DatabaseConnection.Insert(insertCommand, paramValues);
            }
        }

        protected static void GenerateFormFieldsForFormType(Document document, BackgroundWorker worker)
        {
            DataTable formFields = DatabaseConnection.Query("select * from formfields "
                                                            + " where formid = " + document.FormID);

            foreach (DataRow row in formFields.Rows)
            {
                string insertText
                    = "insert into usergeneratedvalues "
                    + " (generateddocid, formid, formfieldid, generatedvaluetext) "
                    + " values "
                    + "( @generatedDocID, "
                    + " @formID, "
                    + " @formFieldID, "
                    + " \"\" )";

                string docID = document.GeneratedDocID.ToString();
                string formFieldID = row["formfieldid"].ToString();

                Params paramValues = new Params();
                paramValues.Add("@generatedDocID", docID);
                paramValues.Add("@formID", document.FormID.ToString());
                paramValues.Add("@formFieldID", formFieldID);

                DatabaseConnection.Insert(insertText, paramValues);
            }

        }


        internal static DataTable DocumentNamesTable
        {
            get
            {
                Lock();
                DataTable namesTableCopy = documentNamesTable.Copy();
                Unlock();

                return namesTableCopy;
            }
        }

        protected static bool CopyDocumentToSoldierDirectory(Document document, byte[] IVbytes)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(document.Filepath);
            
            string directoryPath = SoldierModel.GetSoldierDocDirectory(document.SoldierID);
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            
            string filename = info.Name;
            string nextAvailableFilename = FileUtils.GetNextAvailableFilename(directoryPath, filename);

            // if we are just moving the file from one soldier to anohter
            // don't re-encrypt it! just move it!
            if (document.SoldierIDChanged == false)
            {
                Encryption.EncryptFile(document.Filepath, directory.FullName + "\\" + nextAvailableFilename, IVbytes);
            }

            document.Filepath = directory.FullName + "\\" + nextAvailableFilename;

            return true;
        }


        protected static void RemoveUserDocument(string filepath)
        {
            if (System.IO.File.Exists(filepath))
                System.IO.File.Delete(filepath);
        }


        internal static DataTable GetDocumentStatuses()
        {
            Lock();
            if (documentStatuses == null)
                documentStatuses = DatabaseConnection.GetTable("documentstatus");

            DataTable statusesCopy = documentStatuses.Copy();
            Unlock();

            return statusesCopy;
        }

        protected static void RefreshDocumentNamesTable()
        {
            documentNamesTable = DatabaseConnection.GetTable("documentnames");
        }


        internal static string GetFormFilename(int formID)
        {
            try
            {
                Lock();
                string formFilename = DatabaseConnection.GetSingleValue("forms", "formid", formID, "filepath");
                string formsDirectory = SettingsModel.FormsDirectory;

                Logger.Trace(formsDirectory);

                string formFullPath = formsDirectory + formFilename;

                Logger.Trace(formFullPath);

                Unlock();
                return formFullPath;
            }
            catch (QueryFailedException ex)
            {
                Unlock();
                throw new DataLoadFailedException("Could not locate the requested document.", ex);
            }
        }


        internal static DataTable GetFormFieldsForFormID(int formID)
        {
            try
            {
                Lock();
                string query = "select * from formfields where formid = " + formID;
                DataTable resultSet = DatabaseConnection.Query(query);
                Unlock();
                return resultSet;
            }
            catch (QueryFailedException)
            {
                Unlock();
                throw new DataLoadFailedException("An error occurred attempting to get the fields for the document.  (Document: " + formID + ")");
            }
        }

        protected static void SaveDocumentValue(int pkid, string value, byte[] IVbytes)
        {
            try
            {
                //string encrypted = Encryption.EncryptString(value, IVbytes);
                byte[] encrypted = Encryption.EncryptString(value, IVbytes);
                //string base64Encoded = Convert.ToBase64String(Encryption.GetBytes(encrypted));
                string base64Encoded = Convert.ToBase64String(encrypted);
                //string base64Encoded = Convert.ToBase64String(encrypted);
                Model.SaveColumn("usergeneratedvalues", "generatedvalueid", pkid, "generatedvaluetext", base64Encoded);
            }
            catch (NonQueryFailedException ex)
            {
                throw new DataStoreFailedException("An error occurred attempting to save " + value + " for the current document.", ex);
            }
        }


        protected static int GetPKIDForDocumentEntry(DataTable values, string label)
        {
            try
            {
                int pkid = Convert.ToInt32(values.Select("fieldlabel = '" + label + "'")[0]["generatedvalueid"]);
                return pkid;
            }
            catch (Exception ex)
            {
                throw new DataLoadFailedException("An error occured while attempting to save the document.", ex);
            }
        }


        internal static DataTable GetUserValuesForDocument(Document document)
        {
            Lock();
            DataTable values = new DataTable();

            try
            {
                values = GetUserGeneratedValuesForDocument(document);
            }
            catch (Exception ex)
            {
                Unlock();
                throw ex;
            }

            Unlock();

            return values;
        }

        internal static DataTable GetUserValuesForDocument(int docID)
        {
            Lock();
            DataTable values = new DataTable();

            try
            {
                values = GetUserGeneratedValuesForDocument(docID);
            }
            catch (Exception ex)
            {
                Unlock();
                throw ex;
            }

            Unlock();

            return values;
        }


        protected static DataTable GetUserGeneratedValuesForDocument(Document document)
        {
            int docID = document.GeneratedDocID;
            return GetUserGeneratedValuesForDocument(docID);
        }


        protected static DataTable GetUserGeneratedValuesForDocument(int docID)
        {
            DataTable userGeneratedValues = new DataTable();

            try
            {
                string query = "select * from usergeneratedvalues "
                                + " inner join "
                                + " formfields "
                                + " on usergeneratedvalues.formfieldid = formfields.formfieldid "
                                + " where generateddocid = " + docID;
                userGeneratedValues = DatabaseConnection.Query(query);

                string IVencoded = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", docID, "documentIV");
                byte[] IV = Convert.FromBase64String(IVencoded);

                foreach (DataRow row in userGeneratedValues.Rows)
                {
                    string base64encryptedValue = row["generatedvaluetext"].ToString();
                    byte[] encryptedStringBytes = Convert.FromBase64String(base64encryptedValue);
                    //string encryptedString = Encryption.GetString(encryptedStringBytes);

                    //string decryptedValue = Encryption.DecryptString(encryptedString, IV);
                    string decryptedValue = Encryption.DecryptString(encryptedStringBytes, IV);
                    row["generatedvaluetext"] = decryptedValue;
                }
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException("Could not retrieve the data for DocID: " + docID + "\n"
                                                    + "Unable to save document.", ex);
            }
            
            return userGeneratedValues;
        }

        protected static int CreateNewDocumentNameEntry(string documentName)
        {
            List<int> documentNameIDs = documentNamesTable.Rows.Cast<DataRow>().Select(row => Convert.ToInt32(row["documentnameid"])).ToList();

            int newID = documentNameIDs.Max() + 1;
            for (int i = 0; i < documentNameIDs.Count-1; i++)
            {
                if (documentNameIDs[i] + 1 != documentNameIDs[i + 1])
                {
                    newID = documentNameIDs[i] + 1;
                    break;
                }
            }

            string insertText = "insert into documentnames ( documentnameid, documentnametext ) "
                                    + " values ( @newID, @documentName)";
            Params paramValues = new Params();
            paramValues.Add("@newID", newID.ToString());
            paramValues.Add("@documentName", documentName);

            DatabaseConnection.Insert(insertText, paramValues);

            return newID;
        }


        internal static string GetDocumentName(int documentID)
        {
            Lock();

            DataRow[] documentRow = userGeneratedDocumentsTable.Select("generateddocid = " + documentID);
            int documentNameID = Convert.ToInt32(documentRow[0]["documentnameid"].ToString());
            DataRow[] rows = documentNamesTable.Select("documentnameid = " + documentNameID);
            
            if (rows.Length != 1)
            {
                Unlock();
                Logger.Error("Error occurred trying to get a documentname: nameID: " + documentNameID + " number of rows retrieved: " + rows.Length);
                
                throw new DataLoadFailedException("Could not locate document name.");
            }

            string documentName = rows[0]["documentnametext"].ToString();

            Unlock();

            return documentName;
        }


        protected static int GetDocumentNameID(string documentName)
        {
            if (documentNamesTable == null)
                RefreshDocumentNamesTable();

            DataRow[] rows = documentNamesTable.Select("documentnametext = '" + documentName + "'");
            int documentNameID;

            if (rows.Count() < 1)
            {
                documentNameID = CreateNewDocumentNameEntry(documentName);
                RefreshDocumentNamesTable();
            }
            else
            {
                documentNameID = Convert.ToInt32(rows[0]["documentnameid"]);
            }

            return documentNameID;
        }


        protected static void InsertNewDocument(Document document, BackgroundWorker worker)
        {
            InsertNewDocumentHeaderInfo(document);
            InsertNewDocumentFormFields(document, worker);
        }

        protected static int InsertNewDocumentHeaderInfo(Document document)
        {
            DateTime date = document.Date;
            int soldierID = document.SoldierID;
            int parentDocumentID = document.ParentDocumentID;

            int documentID = document.GeneratedDocID;

            int documentNameID = GetDocumentNameID(document.DocumentName);
            int documentTypeID = Convert.ToInt32(document.DocumentType);//1;
            int formID = document.FormID;  //1;
            int userGenerated = document.UserUploaded ? 1 : 0; //0;
            string filepath = document.Filepath;//"";
            int statusID = (int)document.Status;
            int templateID = document.TemplateID;
            string guidID = document.DocumentGUID.ToString();

            byte[] IVbytes = Encryption.GenerateIV();
            string base64IVstring = Convert.ToBase64String(IVbytes);

            string insertCommand =
                "insert into usergenerateddocs "
                + "(" + (documentID == -1 ? "" : "generateddocid, ")
                + "soldierid, date, documentnameid, "
                + "documenttypeid, formid, usergenerated, "
                + "filepath, statusid, parentdocumentid, "
                + "createdfromtemplateid, generateddocguid, documentIV ) "
                + " values "
                + "(" + (documentID == -1 ? "" : "@documentID, ")
                + "@soldierid, @date, @documentnameid, "
                + "@documenttypeid, @formid, @usergenerated, "
                + "@filepath, @statusid, @parentdocumentid, "
                + "@createdfromtemplateid, @generateddocguid, @documentIV ) ";

            Params paramValues = new Params();
            paramValues.Add("@documentID", documentID.ToString());
            paramValues.Add("@soldierID", soldierID.ToString());
            paramValues.Add("@date", date.Ticks.ToString());
            paramValues.Add("@documentnameid", documentNameID.ToString());
            paramValues.Add("@documenttypeid", documentTypeID.ToString());
            paramValues.Add("@formid", formID.ToString());
            paramValues.Add("@usergenerated", userGenerated.ToString());
            paramValues.Add("@filepath", filepath);
            paramValues.Add("@statusid", statusID.ToString());
            paramValues.Add("@parentdocumentid", parentDocumentID.ToString());
            paramValues.Add("@createdfromtemplateid", templateID.ToString());
            paramValues.Add("@generateddocguid", guidID);
            paramValues.Add("@documentIV", base64IVstring);

            int generatedDocID = DatabaseConnection.Insert(insertCommand, paramValues);

            document.GeneratedDocID = generatedDocID;

            return generatedDocID;
        }


        protected static void InsertNewDocumentFormFields(Document document)
        {
            InsertNewDocumentFormFields(document, null);
        }

        protected static void InsertNewDocumentFormFields(Document document, BackgroundWorker worker)
        {
            DocumentModel.GenerateFormFieldsForFormType(document, worker);
        }



        internal static int SaveDocument(Document document)
        {
            return SaveDocument(document, null);
        }

        private static void SaveUserDocumentToLocalStorage(Document document, string oldFilepath, byte[] IV)
        {
            string currentFilePath = document.Filepath;

            //bool documentExists = DocumentModel.GetDocumentExists(document.DocumentGUID);
            int docID = document.GeneratedDocID;

            CopyDocumentToSoldierDirectory(document, IV);

            if (oldFilepath != "")
            {
                File.Delete(oldFilepath);
            }
        }


        internal static int SaveDocument(Document document, BackgroundWorker backgroundWorker)
        {
            DatabaseConnection.BatchUpdateLock();
            DataTable userGeneratedValues = new DataTable();

            try
            {
                Lock();

                bool documentExists = DocumentModel.GetDocumentExists(document.DocumentGUID);
                string oldFilepath = "";

                if (backgroundWorker != null)
                    backgroundWorker.ReportProgress(25);

                if (!documentExists)
                    InsertNewDocument(document, backgroundWorker);
                else
                {
                    oldFilepath = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", document.GeneratedDocID, "filepath");
                    SaveDocumentHeaderInfo(document);
                }

                Unlock();

                string IVencoded = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", document.GeneratedDocID, "documentIV");
                byte[] IVbytes = Convert.FromBase64String(IVencoded);

                if(backgroundWorker != null)
                    backgroundWorker.ReportProgress(50);

                if (document.UserUploaded)
                {
                    if (document.Filepath != oldFilepath)
                    {
                        SaveUserDocumentToLocalStorage(document, oldFilepath, IVbytes);
                        int docID = document.GeneratedDocID;
                        DatabaseConnection.Update("usergenerateddocs", "filepath", document.Filepath, "generateddocid", docID.ToString());
                    }
                }
                else
                {
                    userGeneratedValues = DocumentModel.GetPKIDsForDocument(document);
                    SaveDocumentValues(userGeneratedValues, document, IVbytes);
                }

                if (backgroundWorker != null)
                    backgroundWorker.ReportProgress(75);
            }
            catch (Exception ex)
            {
                DatabaseConnection.BatchUpdateUnlock();
                Unlock();

                Logger.Error("ERROR IN SAVE DOCUMENT: " + ex, ex);
                throw new DataLoadFailedException("An error occurred while trying to save the document.\n"
                    + "Unable to save document.", ex);
            }

            DatabaseConnection.Backup();
            DatabaseConnection.BatchUpdateUnlock();

            Refresh();

            if (backgroundWorker != null)
                backgroundWorker.ReportProgress(100);

            return document.GeneratedDocID;
        }


        private static void SaveDocumentValues(DataTable userGeneratedValues, Document document, byte[] IVbytes)
        {
            DatabaseConnection.BeginTransaction();

            if (document is DA4856Document)
                DA4856DocumentModel.SaveDA4856DocumentValues(userGeneratedValues, (DA4856Document)document, IVbytes);
            else if (document is GenericMemo)
                MemoModel.SaveGenericMemoValues(userGeneratedValues, (GenericMemo)document, IVbytes);
            else if (document is PregnancyElectionStatementMemo)
                MemoModel.SavePregnancyElectionStatement(userGeneratedValues, (PregnancyElectionStatementMemo)document, IVbytes);
            else if (document is LetterInterface)
                LetterModel.SaveLetterValues(userGeneratedValues, (LetterInterface)document, IVbytes);

            DatabaseConnection.EndTransaction();
        }


        protected static void SaveDocumentHeaderInfo(Document document)
        {
            int pkid = document.GeneratedDocID;
            string pkidStr = pkid.ToString();

            string documentNameIDStr = GetDocumentNameID(document.DocumentName).ToString();
            string statusIDStr = ((int)document.Status).ToString();
            string dateString = document.Date.Date.Ticks.ToString();
            string parentDocumentIDString = document.ParentDocumentID.ToString();
            string soldierIDStr = document.SoldierID.ToString();
            string statusStr = ((int)document.Status).ToString();
            string userGeneratedStr = document.UserUploaded ? "1" : "0";

            string base64IVstring = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", pkid, "documentIV");
            byte[] IVbytes = Convert.FromBase64String(base64IVstring);

            try
            {
                DatabaseConnection.Update("usergenerateddocs", "soldierid", soldierIDStr, "generateddocid", pkidStr);
                DatabaseConnection.Update("usergenerateddocs", "statusid", statusIDStr, "generateddocid", pkidStr);
                DatabaseConnection.Update("usergenerateddocs", "date", dateString, "generateddocid", pkidStr);
                DatabaseConnection.Update("usergenerateddocs", "parentdocumentid", parentDocumentIDString, "generateddocid", pkidStr);

                DatabaseConnection.Update("usergenerateddocs", "documentnameid", documentNameIDStr, "generateddocid", pkidStr);
                DatabaseConnection.Update("usergenerateddocs", "filepath", document.Filepath, "generateddocid", pkidStr);

                DatabaseConnection.Update("usergenerateddocs", "usergenerated", userGeneratedStr, "generateddocid", pkidStr);
                DatabaseConnection.Update("usergenerateddocs", "documentIV", base64IVstring, "generateddocid", pkidStr);
            }
            catch (DataStoreFailedException ex)
            {
                Logger.Error("Error saving document header info", ex);
                throw new DataStoreFailedException("An error occurred attempting to save the document.", ex);
            }
        }


        internal static void RemoveDocumentFromRecycleBin(int documentID)
        {
            Lock();

            DatabaseConnection.Update("usergenerateddocs", "deleted", "0", "generateddocid", documentID.ToString());

            DatabaseConnection.Backup();

            Unlock();
            
            Refresh();
        }


        internal static void RecycleDocument(int documentID)
        {
            Lock();
            MoveDocumentToRecycleBin(documentID);
            Unlock();
            Refresh();
        }


        protected static void MoveDocumentToRecycleBin(int documentID)
        {

            DatabaseConnection.Update("usergenerateddocs", "deleted", "1", "generateddocid", documentID.ToString());

            DatabaseConnection.Backup();
        }

        internal static void DeleteDocument(int documentID)
        {
            Document document = new Document(documentID);

            Lock();
            DeleteDocumentPermanently(document.GeneratedDocID, document.Filepath);
            Unlock();
            Refresh();
        }

        protected static void DeleteDocumentPermanently(int documentID, string filepath)
        {

            if(filepath != "")
                DocumentModel.RemoveUserDocument(filepath);

            DatabaseConnection.Delete("usergenerateddocs", "generateddocid", documentID.ToString());
            DatabaseConnection.Delete("usergeneratedvalues", "generateddocid", documentID.ToString());

            DatabaseConnection.Backup();

        }


        internal static void SetDocumentParentID(int documentID, int newParentDocumentID)
        {
            Lock();
            ChangeDocumentParentDocument(documentID, newParentDocumentID);
            Unlock();

            Refresh();
        }


        protected static void ChangeDocumentParentDocument(int documentID, int newParentDocumentID)
        {

            DatabaseConnection.Update("usergenerateddocs", "parentdocumentid", newParentDocumentID.ToString(),
                                        "generateddocid", documentID.ToString());

            DatabaseConnection.Backup();

        }
        

        internal static void Refresh()
        {
            Lock();

            RefreshUserGeneratedDocumentsTable();
            RefreshDocumentNamesTable();
            RefreshParentDocumentsTable();
            RefreshChildDocumentsTable();


            if (DatabaseConnection.PerformingBatchUpdate || DataImporter.performingImport)
            {
                Unlock();
                return;
            }

            Unlock();

            if (documentModelRefreshed != null)
                documentModelRefreshed();

        }


        internal static DocumentModelRefreshedEvent DocumentModelRefreshed
        {
            set
            {
                documentModelRefreshed += value;
            }
        }

        private static void RefreshParentDocumentsTable()
        {
            parentDocumentsTable
                = DatabaseConnection.Query("select * from usergenerateddocs "
                + " inner join documentstatus "
                + "    on usergenerateddocs.statusid = documentstatus.documentstatusid "
                + " inner join documenttypes "
                + "    on usergenerateddocs.documenttypeid = documenttypes.documenttypeid "
                + " inner join documentcategories "
                + "    on documenttypes.documentcategoryid = documentcategories.documentcategoryid "
                + " inner join documentnames "
                + "    on usergenerateddocs.documentnameid = documentnames.documentnameid "
                + " inner join (select soldierid, lastname, firstname, rankingid, soldierIV from soldiers) as s "
                + "   on usergenerateddocs.soldierid = s.soldierid "
                + " inner join rankings "
                + "   on s.rankingid = rankings.rankingid "
                + " where parentdocumentid = -1 ");

            foreach (DataRow row in parentDocumentsTable.Rows)
            {
                byte[] base64IVbytes = Encryption.GetBytes(row["soldierIV"].ToString());

                if (base64IVbytes.Count() == 0)
                    continue;

                byte[] IV = Convert.FromBase64String(Encryption.GetString(base64IVbytes));

                byte[] lastNameBytes = Convert.FromBase64String(row["lastname"].ToString());
                //string lastNameEncrypted = Encryption.GetString(lastNameBytes);
                //string lastname = Encryption.DecryptString(lastNameEncrypted, IV);
                string lastname = Encryption.DecryptString(lastNameBytes, IV);

                byte[] firstNameBytes = Convert.FromBase64String(row["firstname"].ToString());
                //string firstnameEncrypted = Encryption.GetString(firstNameBytes);
                //string firstname = Encryption.DecryptString(firstnameEncrypted, IV);
                string firstname = Encryption.DecryptString(firstNameBytes, IV);

                row["lastname"] = lastname;
                row["firstname"] = firstname;
            }

            parentDocumentsTable.TableName = "documentstable";
        }


        private static void RefreshChildDocumentsTable()
        {
            //Lock();

            childDocumentTables.Clear();

            foreach (DataRow row in parentDocumentsTable.Rows)
            {
                DataTable childDocs
                    = DatabaseConnection.Query("select * from usergenerateddocs "
                    + " inner join documentstatus "
                    + "    on usergenerateddocs.statusid = documentstatus.documentstatusid "
                    + " inner join documenttypes "
                    + "    on usergenerateddocs.documenttypeid = documenttypes.documenttypeid "
                    + " inner join documentcategories "
                    + "    on documenttypes.documentcategoryid = documentcategories.documentcategoryid "
                    + " inner join documentnames "
                    + "    on usergenerateddocs.documentnameid = documentnames.documentnameid "
                    + " inner join (select soldierid, lastname, firstname, rankingid, soldierIV from soldiers) as s "
                    + "   on usergenerateddocs.soldierid = s.soldierid "
                    + " inner join rankings "
                    + "   on s.rankingid = rankings.rankingid "
                + " where parentdocumentid=" + row["generateddocid"].ToString());

                foreach (DataRow childrow in childDocs.Rows)
                {
                    byte[] base64IVbytes = Encryption.GetBytes(childrow["soldierIV"].ToString());

                    if (base64IVbytes.Count() == 0)
                        continue;

                    byte[] IV = Convert.FromBase64String(Encryption.GetString(base64IVbytes));

                    byte[] lastNameBytes = Convert.FromBase64String(childrow["lastname"].ToString());
                    string lastNameEncrypted = Encryption.GetString(lastNameBytes);
                    //string lastname = Encryption.DecryptString(lastNameEncrypted, IV);
                    string lastname = Encryption.DecryptString(lastNameBytes, IV);

                    byte[] firstNameBytes = Convert.FromBase64String(childrow["firstname"].ToString());
                    //string firstnameEncrypted = Encryption.GetString(firstNameBytes);
                    string firstname = Encryption.DecryptString(firstNameBytes, IV);

                    childrow["lastname"] = lastname;
                    childrow["firstname"] = firstname;
                }

                childDocumentTables.Add(childDocs);
            }
        }


        internal static DataView GetUserGeneratedDocumentView(DocumentSortMode sortModeEnum)
        {
            Lock();

            string sortMode = GetSortModeString(sortModeEnum);

            DataView parentsView = parentDocumentsTable.Copy().DefaultView;
            parentsView.Sort = sortMode;

            DataTable sortedParentsTable = parentsView.ToTable();
            DataColumn[] primaryKeyColumn = new DataColumn[] { sortedParentsTable.Columns["generateddocid"] };
            sortedParentsTable.PrimaryKey = primaryKeyColumn;

            foreach (DataTable childDataTable in childDocumentTables)
            {
                DataView childView = childDataTable.AsDataView();
                childView.Sort = sortMode;

                if (childView.ToTable().Rows.Count == 0)
                    continue;

                int parentDocID = Convert.ToInt32(childView.ToTable().Rows[0]["parentdocumentid"]);

                DataRow row = sortedParentsTable.Rows.Find(parentDocID);
                int index = sortedParentsTable.Rows.IndexOf(row);

                foreach (DataRow childRow in childView.ToTable().Rows)
                {
                    DataRow newRow = sortedParentsTable.NewRow();
                    newRow.ItemArray = childRow.ItemArray;
                    string name = newRow["documentnametext"].ToString();
                    newRow["documentnametext"] = "        " + name;
                    sortedParentsTable.Rows.InsertAt(newRow, index + 1);
                    index++;
                }
            }

            parentsView = sortedParentsTable.Copy().AsDataView();

            Unlock();

            return parentsView;
        }


        private static string GetSortModeString(DocumentSortMode sortModeEnum)
        {
            switch (sortModeEnum)
            {
                case(DocumentSortMode.DateAsc):
                    return "date asc";
                case(DocumentSortMode.DateDesc):
                    return "date desc";
                case(DocumentSortMode.NameAsc):
                    return "documentnametext asc";
                case(DocumentSortMode.NameDesc):
                    return "documentnametext desc";
                case(DocumentSortMode.StatusAsc):
                    return "documentstatustext asc";
                case(DocumentSortMode.StatusDesc):
                    return "documentstatustext desc";
                case(DocumentSortMode.TypeAsc):
                    return "documenttypename asc";
                case (DocumentSortMode.TypeDesc):
                    return "documenttypename desc";
                case(DocumentSortMode.SoldierLastNameAsc):
                    return "lastname asc";
                case (DocumentSortMode.SoldierLastNameDesc):
                    return "lastname desc";
                case (DocumentSortMode.SoldierFirstNameAsc):
                    return "firstname asc";
                case (DocumentSortMode.SoldierFirstNameDesc):
                    return "firstname desc";
                case(DocumentSortMode.RankAsc):
                    return "rankingabbreviation asc";
                case (DocumentSortMode.RankDesc):
                    return "rankingabbreviation desc";
                default:
                    return "date asc";
            }
        }


        internal static void RefreshUserGeneratedDocumentsTable()
        {
            userGeneratedDocumentsTable = DatabaseConnection.Query("select * from usergenerateddocs");
        }

        internal static int GetNumberOfChildDocuments(int documentID)
        {
            Lock();
            int numChildDocuments = userGeneratedDocumentsTable.Select("parentdocumentid = " + documentID).Count();
            Unlock();

            return numChildDocuments;
        }


        internal static List<Document> GetChildDocuments(Document document)
        {
            Lock();
            DataRow[] childDocs = userGeneratedDocumentsTable.Select("parentdocumentid = " + document.GeneratedDocID);
            Unlock();

            List<Document> childDocuments = new List<Document>();

            foreach (DataRow childDoc in childDocs)
            {
                int childDocID = Convert.ToInt32(childDoc["generateddocid"]);
                Document childDocument = new Document(childDocID);

                childDocuments.Add(childDocument);
            }

            return childDocuments;
        }


        private static List<int> GetChildDocumentIDs(int generatedDocumentID)
        {
            DataRow[] childDocs = userGeneratedDocumentsTable.Select("parentdocumentid = " + generatedDocumentID);

            List<int> childDocuments = new List<int>();

            foreach (DataRow childDoc in childDocs)
            {
                int childDocID = Convert.ToInt32(childDoc["generateddocid"]);
                childDocuments.Add(childDocID);
            }

            return childDocuments;
        }





        internal static void DetachAllChildDocuments(int generatedDocumentID)
        {
            Lock();

            List<int> childDocumentIDs = GetChildDocumentIDs(generatedDocumentID);

            foreach (int childDocumentID in childDocumentIDs)
            {
                ChangeDocumentParentDocument(childDocumentID, -1);
            }

            Unlock();

            Refresh();
           
        }


        internal static void RecycleAllChildDocuments(int generatedDocumentID)
        {
            Lock();
            
            List<int> childDocumentIDs = GetChildDocumentIDs(generatedDocumentID);
            
            foreach(int childDocumentID in childDocumentIDs)
            {
                MoveDocumentToRecycleBin(childDocumentID);
            }

            Unlock();

            Refresh();
        }


        internal static bool DocumentIsInRecyclingBin(int documentID)
        {
            Lock();

            DataRow[] rows = userGeneratedDocumentsTable.Select("generateddocid = " + documentID + " and deleted = 1");
            bool inRecycleBin = rows.Count() > 0;

            Unlock();

            return inRecycleBin;
        }


        internal static int NumberOfDocuments 
        {
            get
            {
                Lock();
                int count = userGeneratedDocumentsTable.Rows.Count;
                Unlock();

                return count;
            }
        }


        internal static void DeleteAllDocumentsForSoldier(int soldierID)
        {
            Lock();
            DataRow []documents = userGeneratedDocumentsTable.Select("soldierid = " + soldierID);
            Unlock();

            foreach (DataRow documentRow in documents)
            {
                int documentID = Convert.ToInt32(documentRow["generateddocid"]);
                Document document = new Document(documentID);
                DeleteDocumentPermanently(documentID, document.Filepath);
            }

            Refresh();
        }


        internal static int GetNumDocumentsForSoldierID(int soldierID, bool includeDeleted)
        {
            Lock();

            string showDeletedString = includeDeleted ? "" : " and deleted = 0";
            DataRow[] docs = userGeneratedDocumentsTable.Select("soldierid = " + soldierID + showDeletedString);
            int count = 0;

            if (docs == null)
            {
                count = 0;
            }
            else
                count = docs.Count();

            Unlock();

            return count;
        }


        internal static List<Document> GetAllDocumentsForSoldier(int soldierID)
        {
            Lock();

            List<Document> documents = new List<Document>();
            DataRow[] docs = userGeneratedDocumentsTable.Select("soldierid = " + soldierID + " and deleted = 0");

            Unlock();

            foreach (DataRow doc in docs)
            {
                int documentID = Convert.ToInt32(doc["generateddocid"]);
                Document document = new Document(documentID);
                documents.Add(document);
            }

            return documents;
        }


        internal static DataTable GetDocumentTypesTable()
        {
            Lock();
            DataTable types = DatabaseConnection.Query("select * from documenttypes where documenttypeid <> -1");
            Unlock();

            return types;
        }

        internal static int GetDocumentID(Guid guid)
        {
            Lock();

            int documentID = -1;
            List<DataRow> parentRows = parentDocumentsTable.Select("generateddocguid = '" + guid.ToString() + "'").ToList();
            List<DataRow> childRows = new List<DataRow>();

            foreach (DataTable childTable in childDocumentTables)
            {
                childRows.AddRange(childTable.Select("generateddocguid = '" + guid.ToString() + "'"));
            }

            if (parentRows.Count() > 0)
            {
                documentID = Convert.ToInt32(parentRows[0]["generateddocid"]);
            }
            else if (childRows.Count() > 0)
            {
                documentID = Convert.ToInt32(childRows[0]["generateddocid"]);
            }

            Unlock();

            return documentID;
        }

        internal static bool DocumentExists(Guid guid)
        {
            Lock();
            bool documentExists = false;

            try
            {
                documentExists = GetDocumentExists(guid);
            }
            catch (Exception ex)
            {
                Unlock();
                throw ex;
            }

            Unlock();

            return documentExists;
        }

        protected static bool GetDocumentExists(Guid guid)
        {
            List<DataRow> parentRows = parentDocumentsTable.Select("generateddocguid = '" + guid.ToString() + "'").ToList();
            List<DataRow> childRows = new List<DataRow>();

            bool documentExists = false;

            foreach (DataTable childTable in childDocumentTables)
            {
                childRows.AddRange(childTable.Select("generateddocguid = '" + guid.ToString() + "'"));
            }

            if (parentRows.Count() > 0)
            {
                documentExists = true;
            }
            else if (childRows.Count() > 0)
            {
                documentExists = true;
            }

            return documentExists;
        }

        protected static DataTable GetPKIDsForDocument(int documentID)
        {
            DataTable pkidsTable 
                = DatabaseConnection.Query(   "select * from usergeneratedvalues "
                                            + "inner join "
                                            + "formfields "
                                            + "on usergeneratedvalues.formfieldid = formfields.formfieldid "
                                            + "where usergeneratedvalues.generateddocid = " + documentID);
            
            return pkidsTable;
        }


        protected static DataTable GetPKIDsForDocument(Document document)
        {
            return GetPKIDsForDocument(document.GeneratedDocID);
        }


        internal static void StartDecryptedUserDocumentEditing(Document document)
        {
            string encryptedFilepath = document.Filepath;
            FileInfo encryptedFileInfo = new FileInfo(encryptedFilepath);
            string encryptedExtension = encryptedFileInfo.Extension;

            string decryptedDocumentPath = Path.GetTempFileName();
            FileInfo decryptedFileInfo = new FileInfo(decryptedDocumentPath);
            int decryptedExtensionLen = decryptedFileInfo.Extension.Length;
            string decryptedFilename = decryptedFileInfo.Name;
            string decryptedNameMinusExt = decryptedFilename.Substring(0, decryptedFilename.Length - decryptedExtensionLen);

            decryptedDocumentPath = decryptedFileInfo.Directory.FullName;
            decryptedDocumentPath += "\\" + decryptedNameMinusExt;
            decryptedDocumentPath += encryptedFileInfo.Extension;

            string IV = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", document.GeneratedDocID, "documentIV");
            byte[] IVencoded = Convert.FromBase64String(IV);
            
            Encryption.DecryptFile(document.Filepath, decryptedDocumentPath, IVencoded);
            OpenDocumentMonitor.OpenDocumentAsProcess(document, decryptedDocumentPath);
        }

        public static string GetFieldName(DataRow[] rows, string label)
        {
            try
            {
                string fieldName;
                //DataTable results = rows.Where(row => row["fieldlabel"] == label).CopyToDataTable();

                foreach (DataRow row in rows)
                {
                    if (row["fieldlabel"].ToString() == label)
                    {
                        fieldName = row["fieldname"].ToString();
                        return fieldName;
                    }
                }

                throw new DataLoadFailedException("No rows matching " + label);

            }
            catch (Exception ex)
            {
                throw new DataLoadFailedException("Could not retrieve document value for " + label, ex);
            }
        }
    }
}
