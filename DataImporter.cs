using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    class DataImporter
    {
        private List<UnitHierarchyModel.Battalion> battalionsToImport;
        private List<UnitHierarchyModel.UnitHierarchy> unitHierarciesToImport;
        private List<Soldier> soldiersToImport;
        private List<Document> documentsToImport;
        private SortedDictionary<int, SortedDictionary<int, string>> formFieldValuesToImport;
        private List<NoteInterface> notesToImport;

        private SortedDictionary<int, int> battalionIDMappings;
        private SortedDictionary<int, int> unitHierarchyIDMappings;
        private SortedDictionary<int, int> soldierIDMappings;
        private SortedDictionary<int, int> documentIDMappings;
        private SortedDictionary<int, int> noteIDMappings;

        DirectoryInfo importDirectory;
        //Control parent;
        Point parentCenter;

        internal static bool performingImport = false;

        private class VersionException : Exception
        {
            internal VersionException(string message)
                : base(message)
            { }
        }


        private class VersionMissingException : Exception
        {
            internal VersionMissingException(string message)
                : base(message)
            { }
        }


        internal void ReadExportFile(FileInfo cqpxFile, BackgroundWorker worker, Point location)
        {
            try
            {
                this.parentCenter = location;
                performingImport = true;

                DatabaseConnection.SetCheckPoint();
                DatabaseConnection.BatchUpdateLock();

                string tempPath = Path.GetTempPath() + "//" + DateTime.Now.Ticks.ToString();
                importDirectory = Directory.CreateDirectory(tempPath);

                Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile(cqpxFile.FullName);
                zipfile.ExtractAll(importDirectory.FullName);

                FileInfo xmlFile = importDirectory.GetFiles("*.xml").First();
                string xmlFilename = xmlFile.FullName;

                XmlDocument document = new XmlDocument();
                document.Load(xmlFilename);

                formFieldValuesToImport = new SortedDictionary<int, SortedDictionary<int, string>>();

                InitializeContainers();

                ReadXmlDocument(document, worker);
                worker.ReportProgress(25);

                InsertImportedItems(worker);
                worker.ReportProgress(98);

                ScrubImportDirectory(importDirectory.FullName);

                //Directory.Delete(tempPath, true);
                performingImport = false;

                DatabaseConnection.ClearCheckpoint();
                DatabaseConnection.BatchUpdateUnlock();

                SoldierModel.Refresh();
                DocumentModel.Refresh();
                NotesModel.Refresh();

                worker.ReportProgress(100);
            }
            catch (VersionException ex)
            {
                Logger.Error(ex);

                DatabaseConnection.RestoreCheckpoint();
                DatabaseConnection.BatchUpdateUnlock();

                string error = "This export file was generated using a newer version of Counselor than you are using."
                                + "Export version: " + ex.Message + "\n"
                                + "Current version: " + SettingsModel.DatabaseVersion + "\n"
                                + "The import cannot be performed.  Update Counselor to the newest version, then try again.";

                throw new Exception(error);

                //string caption = "Import Error";

                //CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                //CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                //CQPMessageBox.ShowDialog(error, caption, buttons, icon);

                //throw ex;

            }
            catch (VersionMissingException ex)
            {
                Logger.Error(ex);

                DatabaseConnection.RestoreCheckpoint();
                DatabaseConnection.BatchUpdateUnlock();

                string error = "An error occurred while reading the import file.\n" + ex.Message
                                + "The file will not be imported.";

                throw new VersionMissingException(error);

                //string caption = "Import Error";

                //CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                //CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                //CQPMessageBox.ShowDialog(error, caption, buttons, icon);

                //throw ex;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                DatabaseConnection.RestoreCheckpoint();
                DatabaseConnection.BatchUpdateUnlock();

                string error = "An error occurred while importing the file.  The data will not be imported.";

                throw new Exception(error);

                //string caption = "Import Error";
                //CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                //CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                //CQPMessageBox.ShowDialog(error, caption, buttons, icon);

                //throw ex;
            }
            finally
            {
                performingImport = false;
            }
        }


        private void ScrubImportDirectory(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();

            foreach (FileInfo file in files)
            {
                FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Write);
                long bytes = file.Length;
                byte[] zero = new byte[1] { 0 };
                for (int i = 0; i < bytes; i++)
                    fileStream.Write(zero, 0, 1);
                fileStream.Close();
                File.Delete(file.FullName);
            }

            Directory.Delete(path, true);
        }


        private void ReadXmlDocument(XmlDocument document, BackgroundWorker worker)
        {
            ReadVersionNumber(document);

            battalionsToImport = ReadBattalions(document);
            worker.ReportProgress(5);
            
            unitHierarciesToImport = ReadUnitHierarchies(document);
            worker.ReportProgress(10);
            
            soldiersToImport = ReadSoldiers(document);
            worker.ReportProgress(15);
            
            documentsToImport = ReadDocuments(document);
            worker.ReportProgress(20);

            notesToImport = ReadNotes(document);
            worker.ReportProgress(25);
        }


        private void InitializeContainers()
        {
            battalionIDMappings = new SortedDictionary<int,int>();
            unitHierarchyIDMappings = new SortedDictionary<int,int>();
            soldierIDMappings = new SortedDictionary<int,int>();
            documentIDMappings = new SortedDictionary<int,int>();
            noteIDMappings = new SortedDictionary<int,int>();
        }


        private void ReadVersionNumber(XmlDocument document)
        {
            XmlNode node = document.SelectSingleNode("Counselor/DatabaseVersion");
            
            if (node == null)
                throw new VersionMissingException("The document is missing a version number.");

            int version;

            try
            {
                string versionNumber = node.Attributes["number"].Value;
                version = Convert.ToInt32(versionNumber);
            }
            catch (Exception)
            {
                throw new VersionMissingException("The document's version number is missing or corrupt.");
            }

            if (version > SettingsModel.DatabaseVersion)
                throw new VersionException(version.ToString());
        }


        private List<UnitHierarchyModel.Battalion> ReadBattalions(XmlDocument document)
        {
            List<UnitHierarchyModel.Battalion> battalionsList = new List<UnitHierarchyModel.Battalion>();

            XmlNodeList battalionsXMLList = document.SelectNodes("Counselor/Battalions/Battalion");

            try
            {
                foreach (XmlNode battalionNode in battalionsXMLList)
                {
                    UnitHierarchyModel.Battalion battalion = new UnitHierarchyModel.Battalion();
                    battalion.BattalionID = Convert.ToInt32(battalionNode.Attributes["id"].Value);
                    battalion.BattalionName = battalionNode.Attributes["name"].Value;

                    battalionsList.Add(battalion);
                }
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred while importing one of the soldiers.  "
                                                    + "Unable to continue.\n\nThe data will not be imported.");
            }

            return battalionsList;
        }


        private List<UnitHierarchyModel.UnitHierarchy> ReadUnitHierarchies(XmlDocument document)
        {
            List<UnitHierarchyModel.UnitHierarchy> unitHierarchyList = new List<UnitHierarchyModel.UnitHierarchy>();

            XmlNodeList unitHierarchyXMLList = document.SelectNodes("Counselor/UnitHierarchies/UnitHierarchy");

            try
            {
                foreach (XmlNode unitHierarchyNode in unitHierarchyXMLList)
                {
                    UnitHierarchyModel.UnitHierarchy unitHierarchy = GetUnitHierarchyFromXMLNode(unitHierarchyNode);
                    unitHierarchyList.Add(unitHierarchy);
                }
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred while importing one of the soldiers.  "
                                                    + "Unable to continue.\n\nThe data will not be imported.");
            }

            return unitHierarchyList;
        }


        private UnitHierarchyModel.UnitHierarchy GetUnitHierarchyFromXMLNode(XmlNode unitHierarchyNode)
        {
            UnitHierarchyModel.UnitHierarchy unitHierarchy = new UnitHierarchyModel.UnitHierarchy();

            unitHierarchy.unitHierarchyID = Convert.ToInt32(unitHierarchyNode.Attributes["id"].Value);
            unitHierarchy.battalionID = Convert.ToInt32(unitHierarchyNode.Attributes["battalionid"].Value);

            UnitHierarchyModel.Battalion battalion 
                = battalionsToImport.Where(b => b.BattalionID == unitHierarchy.battalionID).First();

            unitHierarchy.battalionName = battalion.BattalionName;
            unitHierarchy.unitID = Convert.ToInt32(unitHierarchyNode.Attributes["unitid"].Value);
            unitHierarchy.unitDesignatorID = Convert.ToInt32(unitHierarchyNode.Attributes["unitdesignatorid"].Value);
            unitHierarchy.platoonID = Convert.ToInt32(unitHierarchyNode.Attributes["platoonid"].Value);
            unitHierarchy.squadID = Convert.ToInt32(unitHierarchyNode.Attributes["squadid"].Value);

            return unitHierarchy;
        }


        private List<Soldier> ReadSoldiers(XmlDocument document)
        {
            List<Soldier> soldiers = new List<Soldier>();

            XmlNodeList soldierXMLNodes = document.SelectNodes("Counselor/Soldiers/Soldier");

            try
            {
                foreach (XmlNode soldierNode in soldierXMLNodes)
                {
                    Soldier soldier = ReadSoldierNode(soldierNode);
                    soldiers.Add(soldier);
                }
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred while importing one of the Soldiers.  Unable to continue.\n\nThe data will not be imported.");
            }

            return soldiers;
        }


        private Soldier ReadSoldierNode(XmlNode soldierNode)
        {
            Soldier soldier = new Soldier();

            string soldierLastNameBase64encrypted = soldierNode.Attributes["lastname"].Value;
            string soldierFirstNameBase64encrypted = soldierNode.Attributes["firstname"].Value;
            string dateOfRankBase64encrypted = soldierNode.Attributes["dateofrank"].Value;
            string dateOfBirthBase64encrypted = soldierNode.Attributes["dateofbirth"].Value;
            string otherStatusBase64encrypted = soldierNode.Attributes["otherstatus"].Value;
            string unitHierarchyBase64encrypted = soldierNode.Attributes["unithierarchy"].Value;
            string middleInitialBase64encrypted = soldierNode.Attributes["middleinitial"].Value;
            string IV = soldierNode.Attributes["soldierIV"].Value;
            byte[] IVbytes = Convert.FromBase64String(IV);

            string lastName = Encryption.Base64DecryptString(soldierLastNameBase64encrypted, IVbytes);
            string firstName = Encryption.Base64DecryptString(soldierFirstNameBase64encrypted, IVbytes);
            string dateofrank = Encryption.Base64DecryptString(dateOfRankBase64encrypted, IVbytes);
            string dateofbirth = Encryption.Base64DecryptString(dateOfBirthBase64encrypted, IVbytes);
            string otherstatus = Encryption.Base64DecryptString(otherStatusBase64encrypted, IVbytes);
            string unithierarchy = Encryption.Base64DecryptString(unitHierarchyBase64encrypted, IVbytes);
            string middleInitial = Encryption.Base64DecryptString(middleInitialBase64encrypted, IVbytes);

            soldier.SoldierID = Convert.ToInt32(soldierNode.Attributes["id"].Value);
            soldier.Rank = (Ranking)Convert.ToInt32(soldierNode.Attributes["rankingid"].Value);
            soldier.LastName = lastName;
            soldier.FirstName = firstName;
            soldier.DateOfBirth = new DateTime( Convert.ToInt64(dateofbirth) );
            soldier.DateOfRank = new DateTime( Convert.ToInt64(dateofrank) );
            soldier.OtherStatus = otherstatus;
            soldier.UnitHierarchy = new UnitHierarchyModel.UnitHierarchy();
            soldier.UnitHierarchy.unitHierarchyID = Convert.ToInt32(unithierarchy);
            soldier.MiddleInitial = Convert.ToChar(middleInitial);
            
            /*
            soldier.LastName = soldierNode.Attributes["lastname"].Value;
            soldier.FirstName = soldierNode.Attributes["firstname"].Value;
            soldier.DateOfRank = new DateTime( Convert.ToInt64(soldierNode.Attributes["dateofrank"].Value));
            soldier.DateOfBirth = new DateTime( Convert.ToInt64(soldierNode.Attributes["dateofbirth"].Value));
            soldier.OtherStatus = soldierNode.Attributes["otherstatus"].Value;
            soldier.UnitHierarchy = new UnitHierarchyModel.UnitHierarchy();
            soldier.UnitHierarchy.unitHierarchyID = Convert.ToInt32(soldierNode.Attributes["unithierarchy"].Value);
            soldier.MiddleInitial = Convert.ToChar(soldierNode.Attributes["middleinitial"].Value);
             * */

            soldier.soldierGUID = new Guid(soldierNode.Attributes["guid"].Value);

            //soldier.Statuses = new List<SoldierStatus>();

            XmlNodeList statusNodes = soldierNode.SelectNodes("Statuses/Status");
            foreach(XmlNode statusNode in statusNodes)
            {
                int statusID = Convert.ToInt32(statusNode.Attributes["id"].Value);
                string statusname = statusNode.Attributes["statusname"].Value;
                bool applies = statusNode.Attributes["applies"].Value == "1" ? true : false;

                SoldierStatus status = new SoldierStatus(statusID, statusname, applies);
                soldier.Statuses[statusID] = status;
                //soldier.Statuses.Add(status);
            }

            //soldier.PictureFilename = soldierNode.Attributes["imagefilepath"].Value;
            //soldier.PictureNeedsSaved = true;
            string expectedImagePath = importDirectory.FullName + "\\" + soldier.soldierGUID;
            if (File.Exists(expectedImagePath))
            {
                soldier.hasCustomImage = true;
                soldier.NewPictureFilename = expectedImagePath;
                //soldier.Picture = Image.FromFile(expectedImagePath);
            }

            return soldier;
        }


        private List<Document> ReadDocuments(XmlDocument xmlDocument)
        {
            List<Document> documents = new List<Document>();

            XmlNodeList documentsXMLList = xmlDocument.SelectNodes("Counselor/UserGeneratedDocs/UserGeneratedDoc");

            try
            {
                foreach (XmlNode documentNode in documentsXMLList)
                {
                    Document document = GetDocumentFromXMLNode(documentNode);
                    int documentID = document.GeneratedDocID;

                    formFieldValuesToImport[documentID] = GetFormFieldValuesForDocumentNode(documentNode);
                    
                    documents.Add(document);
                }
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred while importing one of the documents.  Unable to continue.\n\nThe data will not be imported.");
            }

            return documents;
        }


        private Document GetDocumentFromXMLNode(XmlNode documentNode)
        {
            Document document = new Document();

            string IVstring = documentNode.Attributes["documentIV"].Value;
            byte[] IV = Convert.FromBase64String(IVstring);

            string dateBase64Encoded = documentNode.Attributes["date"].Value;
            string date = Encryption.Base64DecryptString(dateBase64Encoded, IV);

            string documentNameBase64Encoded = documentNode.Attributes["documentname"].Value;
            string documentname = Encryption.Base64DecryptString(documentNameBase64Encoded, IV);

            string documentTypeBase64Encoded = documentNode.Attributes["documenttypeid"].Value;
            string documenttype = Encryption.Base64DecryptString(documentTypeBase64Encoded, IV);

            string formIDBase64Encoded = documentNode.Attributes["formid"].Value;
            string formid = Encryption.Base64DecryptString(formIDBase64Encoded, IV);

            string usergeneratedBase64Encoded = documentNode.Attributes["usergenerated"].Value;
            string usergenerated = Encryption.Base64DecryptString(usergeneratedBase64Encoded, IV);

            string statusIDBase64Encoded = documentNode.Attributes["statusid"].Value;
            string statusid = Encryption.Base64DecryptString(statusIDBase64Encoded, IV);

            string parentDocumentIDBase64Encoded = documentNode.Attributes["parentdocumentid"].Value;
            string parentdocumentid = Encryption.Base64DecryptString(parentDocumentIDBase64Encoded, IV);

            string templateIDBase64Encoded = documentNode.Attributes["createdfromtemplate"].Value;
            string templateID = Encryption.Base64DecryptString(templateIDBase64Encoded, IV);


            document.GeneratedDocID = Convert.ToInt32(documentNode.Attributes["id"].Value);
            document.SoldierID = Convert.ToInt32(documentNode.Attributes["soldierid"].Value);
            document.Date = new DateTime(Convert.ToInt64(date));
            document.DocumentName = documentname;
            document.DocumentType = (DocumentType)Convert.ToInt32(documenttype);
            document.FormID = Convert.ToInt32(formid);
            document.UserUploaded = Convert.ToBoolean(Convert.ToInt32(usergenerated));
            document.Filepath = documentNode.Attributes["filepath"].Value;
            document.Status = (DocumentStatus)Convert.ToInt32(statusid);
            document.ParentDocumentID = Convert.ToInt32(parentdocumentid);
            document.TemplateID = Convert.ToInt32(templateID);
            document.DocumentGUID = new Guid(documentNode.Attributes["guid"].Value);

            if (document.Filepath != "")
            {
                string originalFilename = new FileInfo(document.Filepath).Name;
                FileInfo originalFileInfo = new FileInfo(importDirectory.FullName + "\\" + originalFilename);

                string encryptedFileName = importDirectory.FullName + "\\" + originalFilename + ".ENC";
                FileInfo encryptedFileInfo = new FileInfo(encryptedFileName);

                string decryptedFileName = importDirectory.FullName + "\\" + originalFilename;
                FileInfo decryptedFileInfo = new FileInfo(decryptedFileName);

                File.Copy(originalFileInfo.FullName, encryptedFileInfo.FullName);

                File.Delete(originalFileInfo.FullName);
                Encryption.DecryptFile(encryptedFileInfo.FullName, decryptedFileInfo.FullName, IV);
                File.Delete(encryptedFileInfo.FullName);
            }

            return document;
        }

        private SortedDictionary<int, string> GetFormFieldValuesForDocumentNode(XmlNode documentNode)
        {
            SortedDictionary<int, string> formFieldVaues = new SortedDictionary<int,string>();

            XmlNodeList formFieldsList = documentNode.SelectNodes("Values/FormField");

            string IVstring = documentNode.Attributes["documentIV"].Value;
            byte[] IV = Convert.FromBase64String(IVstring);

            try
            {
                foreach (XmlNode formField in formFieldsList)
                {
                    int id = Convert.ToInt32(formField.Attributes["id"].Value);
                    string value = formField.Attributes["value"].Value;

                    string decryptedValue = Encryption.Base64DecryptString(value, IV);

                    formFieldVaues[id] = decryptedValue;
                }
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred while importing one of the documents.  Unable to continue.\n\nThe data will not be imported.");
            }

            return formFieldVaues;
        }


        private List<NoteInterface> ReadNotes(XmlDocument document)
        {
            List<NoteInterface> notes = new List<NoteInterface>();

            XmlNodeList notesXMLList = document.SelectNodes("Counselor/Notes/Note");

            try
            {
                foreach (XmlNode xmlNote in notesXMLList)
                {
                    string IV = xmlNote.Attributes["noteIV"].Value;
                    byte[] IVbytes = Convert.FromBase64String(IV);

                    string dateBase64Encrypted = xmlNote.Attributes["date"].Value;
                    string date = Encryption.Base64DecryptString(dateBase64Encrypted, IVbytes);

                    XmlNode noteValuesNode = xmlNote.FirstChild;

                    string subjectBase64Encrypted = noteValuesNode.Attributes["subject"].Value;
                    string subject = Encryption.Base64DecryptString(subjectBase64Encrypted, IVbytes);

                    string commentBase64Encrypted = noteValuesNode.Attributes["comment"].Value;
                    string comment = Encryption.Base64DecryptString(commentBase64Encrypted, IVbytes);


                    NoteInterface note = new NoteInterface();
                    note.NoteID = Convert.ToInt32(xmlNote.Attributes["id"].Value);
                    note.SoldierID = Convert.ToInt32(xmlNote.Attributes["soldierid"].Value);
                    note.Date = new DateTime(Convert.ToInt64(date));
                    note.NoteGUID = new Guid(xmlNote.Attributes["guid"].Value);

                    note.Subject = subject;
                    note.Comment = comment;

                    notes.Add(note);
                }
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred while importing one of the notes.  Unable to continue.\n\nThe data will not be imported.");
            }

            return notes;
        }


        private void InsertImportedItems(BackgroundWorker worker)
        {
            int numItemsToInsert = battalionsToImport.Count + unitHierarciesToImport.Count
                + soldiersToImport.Count + documentsToImport.Count
                + formFieldValuesToImport.Count + notesToImport.Count;

            float percentPerItem = ((float)(98.0 - 25.0)) / (float)numItemsToInsert;

            InsertBattalionsAndUpdateMappings();
            worker.ReportProgress((int)(25 + percentPerItem * battalionsToImport.Count));

            InsertUnitHierarchiesAndUpdateMappings();
            worker.ReportProgress((int)(25 + percentPerItem * unitHierarciesToImport.Count));
            
            InsertSoldiersAndUpdateMappings();
            worker.ReportProgress((int)(25 + percentPerItem * soldiersToImport.Count));

            Logger.Trace("Import - just insertedsoldiers");

            InsertDocumentsAndUpdateMappings();
            worker.ReportProgress((int)(25 + percentPerItem * documentsToImport.Count));

            Logger.Trace("Import - just insertedsoldiers");

            InsertFormFieldsAndUpdateMappings();
            worker.ReportProgress((int)(25 + percentPerItem * formFieldValuesToImport.Count));

            ImportNotesAndUpdateMappings();
            worker.ReportProgress((int)(25 + percentPerItem * notesToImport.Count));
        }


        private void InsertBattalionsAndUpdateMappings()
        {
            for (int i = 0; i < battalionsToImport.Count; i++)
            {
                int newBattalionID;
                int oldBattalionID = battalionsToImport[i].BattalionID;

                if (!UnitHierarchyModel.BattalionNameExists(battalionsToImport[i].BattalionName))
                    newBattalionID = UnitHierarchyModel.CreateBattalion(battalionsToImport[i].BattalionName);
                else
                    newBattalionID = UnitHierarchyModel.GetBattalionID(battalionsToImport[i].BattalionName);

                battalionsToImport[i].BattalionID = newBattalionID;

                battalionIDMappings.Add(oldBattalionID, newBattalionID);
            }
        }


        private void InsertUnitHierarchiesAndUpdateMappings()
        {
            // first go through and update the unit hierachy . battalionID to the new ID
            // then if a unit hierarchy exists with that set of bID, uID, udID, pID, sID
            //    then use that unit hierarchy ID
            // else
            //    use the updated unit hierarchy ID

            for (int i = 0; i < unitHierarciesToImport.Count; i++)
            {
                int newUnitHierarchyID;
                int oldUnitHierarchyID = unitHierarciesToImport[i].unitHierarchyID;

                if(!battalionIDMappings.Keys.Contains(unitHierarciesToImport[i].battalionID))
                    throw new DataLoadFailedException("An error occurred while importing one of the Soldiers.\n\n"
                                                        +"Unable to continue.\n\nThe data will not be imported.");

                int newBattalionID = battalionIDMappings[unitHierarciesToImport[i].battalionID];
                unitHierarciesToImport[i].battalionID = newBattalionID;

                newUnitHierarchyID = UnitHierarchyModel.CreateUnitHierarchyIfNotExists(unitHierarciesToImport[i]);

                unitHierarciesToImport[i].unitHierarchyID = newUnitHierarchyID;

                unitHierarchyIDMappings.Add(oldUnitHierarchyID, newUnitHierarchyID);
            }
        }


        private bool UnitHierarchyValid(UnitHierarchyModel.UnitHierarchy unitHierarchy)
        {
            int oldUnitHierarchyID = unitHierarchy.unitHierarchyID;

            if (!unitHierarchyIDMappings.Keys.Contains(oldUnitHierarchyID))
                return false;

            return true;
        }


        private UnitHierarchyModel.UnitHierarchy GetNewUnitHierarchy(int oldUnitHierarchyID)
        {
            int newUnitHierarchyID = unitHierarchyIDMappings[oldUnitHierarchyID];

            UnitHierarchyModel.UnitHierarchy unitHierarchy
                = unitHierarciesToImport.Where(unit => unit.unitHierarchyID == newUnitHierarchyID).First();

            int newBattalionID = unitHierarchy.battalionID;

            return unitHierarchy;
        }


        private bool SoldiersEqual(Soldier soldier)
        {
            bool soldiersEqual = true;

            Soldier mySoldier = new Soldier(soldier.soldierGUID);

            soldiersEqual &= mySoldier.DateOfBirth == soldier.DateOfBirth;
            soldiersEqual &= mySoldier.DateOfRank == soldier.DateOfRank;
            soldiersEqual &= mySoldier.FirstName == soldier.FirstName;
            soldiersEqual &= mySoldier.LastName == soldier.LastName;
            soldiersEqual &= mySoldier.MiddleInitial == soldier.MiddleInitial;
            soldiersEqual &= mySoldier.OtherStatus == soldier.OtherStatus;

            //if (soldier.PictureFilename != mySoldier.PictureFilename)
            //    return false;

            /*if (soldier.PictureFilename != "" && mySoldier.PictureFilename != "")
            {
                FileInfo newFile = new FileInfo(soldier.PictureFilename);
                FileInfo myFile = new FileInfo(mySoldier.PictureFilename);
                soldiersEqual &= newFile.Name == myFile.Name;
            }*/

            Image mySoldiersImage = mySoldier.Picture;
            Image newSoldierImage = soldier.Picture;
            if (mySoldiersImage.Size != newSoldierImage.Size)
                return false;

            Bitmap b1 = new Bitmap(mySoldiersImage);
            Bitmap b2 = new Bitmap(newSoldierImage);

            for (int x = 0; x < b1.Width; x++)
                for (int y = 0; y < b2.Height; y++)
                    if (b1.GetPixel(x,y) != b2.GetPixel(x,y))
                        return false;

            //soldiersEqual &= mySoldier.PictureFilename == soldier.PictureFilename;
            soldiersEqual &= mySoldier.Rank == soldier.Rank;
            //soldiersEqual &= mySoldier.Statuses.SequenceEqual(soldier.Statuses);

            soldiersEqual &= Utilities.EqualSortedDict(mySoldier.Statuses, soldier.Statuses);

            ////soldiersEqual &= mySoldier.Statuses == soldier.Statuses;
            //soldiersEqual &= mySoldier.Statuses.Count == soldier.Statuses.Count;
            //if (!soldiersEqual)
            //    return soldiersEqual;

            //foreach (int statusID in mySoldier.Statuses.Keys)
            //{
            //    if(
            //}

            soldiersEqual &= mySoldier.UnitHierarchy == soldier.UnitHierarchy;

            return soldiersEqual;
        }

        private void InsertSoldiersAndUpdateMappings()
        {
            DialogResult result = DialogResult.Yes;
            bool showPrompt = true;

            foreach (Soldier soldier in soldiersToImport)
            {
                UpdateSoldierUnitHierarchyMapping(soldier);
                //UpdateSoldierImagePath(soldier);

                bool soldierExists = SoldierModel.SoldierExists(soldier.soldierGUID);

                if (soldierExists && SoldiersEqual(soldier))
                {
                    int oldSoldierID = soldier.SoldierID;

                    Soldier mySoldier = new Soldier(soldier.soldierGUID);
                    SoldierModel.RemoveSoldierFromRecyclingBin(mySoldier.SoldierID);

                    soldierIDMappings[oldSoldierID] = mySoldier.SoldierID;
                    continue;
                }
                else if (soldierExists)
                {
                    if (showPrompt)
                    {
                        string formattedSoldierName = RankingModel.RankingAbbreviationFromEnum(soldier.Rank)
                                + " " + soldier.LastName + ", " + soldier.FirstName + " " + soldier.MiddleInitial;

                        string soldierConflictMessage = "Your record of " + formattedSoldierName + " differs from the copy you're importing!"
                                                        + "\n\nWould you like to keep your copy, overwrite with the import copy, or keep both (a new record with be created.)";
                        ImportConflictDialog dialog = new ImportConflictDialog(soldierConflictMessage);

                        Point position = parentCenter;
                        position.X -= dialog.Size.Width / 2;
                        position.Y -= dialog.Size.Height / 2;

                        dialog.StartPosition = FormStartPosition.Manual;
                        dialog.Location = position;

                        result = dialog.ShowDialog();
                        showPrompt = !dialog.ApplyToAll;
                    }

                    // Use Mine - update the ID to point at my soldier
                    // but don't update any values
                    if (result == DialogResult.Yes)
                    {
                        Soldier mySoldier = new Soldier(soldier.soldierGUID);
                        int oldSoldierID = soldier.SoldierID;
                        int newSoldierID = mySoldier.SoldierID;
                        //SoldierModel.RemoveSoldierFromRecyclingBin(mySoldier.SoldierID);
                        soldierIDMappings[oldSoldierID] = newSoldierID;
                    }
                    // Use Theirs - update the id to point at my soldier
                    // and update the values
                    else if (result == DialogResult.No)
                    {
                        //UpdateSoldierUnitHierarchyMapping(soldier);
                        //UpdateSoldierImagePath(soldier);

                        Soldier mySoldier = new Soldier(soldier.soldierGUID);

                        int oldSoldierID = soldier.SoldierID;
                        int newSoldierID = mySoldier.SoldierID;

                        //SoldierModel.DeleteSoldier(newSoldierID);

                        //soldier.SoldierID = -1;
                        //soldier.Save();

                        soldier.SoldierID = newSoldierID;
                        SoldierModel.RemoveSoldierFromRecyclingBin(soldier.SoldierID);
                        soldier.Save();

                        soldierIDMappings[oldSoldierID] = newSoldierID;
                    }
                    // Keep Both - update their soldier's GUID, otherwise treat as normal
                    else if(result == DialogResult.Cancel)
                    {
                        //UpdateSoldierUnitHierarchyMapping(soldier);
                        //UpdateSoldierImagePath(soldier);

                        int oldSoldierID = soldier.SoldierID;

                        soldier.SoldierID = -1;
                        soldier.soldierGUID = Guid.NewGuid();

                        int newSoldierID = soldier.Save();
                        soldier.SoldierID = newSoldierID;

                        soldierIDMappings[oldSoldierID] = newSoldierID;
                    }
                }
                else
                {
                    //UpdateSoldierUnitHierarchyMapping(soldier);
                    //UpdateSoldierImagePath(soldier);

                    int oldSoldierID = soldier.SoldierID;
                    soldier.SoldierID = -1;

                    int newSoldierID = soldier.Save();
                    soldier.SoldierID = newSoldierID;

                    soldierIDMappings[oldSoldierID] = newSoldierID;
                }
            }
        }


        private void UpdateSoldierUnitHierarchyMapping(Soldier soldier)
        {
            if (!UnitHierarchyValid(soldier.UnitHierarchy))
                throw new DataLoadFailedException("An error occurred while importing one of the Soldiers.\n\n"
                                                    + "Unable to continue.\n\nThe data will not be imported.");

            int oldUnitHierarchyID = soldier.UnitHierarchy.unitHierarchyID;

            UnitHierarchyModel.UnitHierarchy unitHierarchy = GetNewUnitHierarchy(oldUnitHierarchyID);
            soldier.UnitHierarchy = unitHierarchy;
        }

        /*
        private void UpdateSoldierImagePath(Soldier soldier)
        {
            if (soldier.PictureFilename != "")
                soldier.PictureFilename = importDirectory.FullName + "\\" + soldier.PictureFilename;
        }*/


        private bool DocumentsMatch(Document document)
        {
            Document myDoc = new Document(document.DocumentGUID);

            if(myDoc.Date != document.Date
                || myDoc.DocumentName != document.DocumentName
                || myDoc.DocumentType != document.DocumentType
                || myDoc.FormID != document.FormID
                || myDoc.ParentDocumentID != document.ParentDocumentID
                || myDoc.SoldierID != document.SoldierID
                || myDoc.Status != document.Status
                || myDoc.TemplateID != document.TemplateID
                || myDoc.UserUploaded != document.UserUploaded)
            {
                return false;
            }


            if (myDoc.Filepath != "" && document.Filepath != "")
            {
                FileInfo myfile = new FileInfo(myDoc.Filepath);
                FileInfo newfile = new FileInfo(document.Filepath);

                if (myfile.Name != newfile.Name)
                    return false;
            }


            DataTable valuesForMyDoc = DocumentModel.GetUserValuesForDocument(myDoc);
            SortedDictionary<int, string> newFormFieldValues = formFieldValuesToImport[document.GeneratedDocID];

            if (valuesForMyDoc.Rows.Count != newFormFieldValues.Count)
                return false;

            foreach (int formFieldID in newFormFieldValues.Keys)
            {
                DataRow[] rows = valuesForMyDoc.Select("formfieldid = " + formFieldID);
                if (rows.Count() == 0)
                    return false;

                string newValue = newFormFieldValues[formFieldID];
                string myValue = rows[0]["generatedvaluetext"].ToString();

                if (newValue != myValue)
                    return false;
            }

            return true;
        }


        private void InsertDocumentsAndUpdateMappings()
        {
            DialogResult result = DialogResult.Yes;
            bool showPrompt = true;

            Logger.Frame();
            Logger.Trace("Importer - InsertDocumentAndUpdateMappings start");

            //foreach(Document document in documentsToImport)
            for(int i = 0; i < documentsToImport.Count; i++)
            {
                Logger.Trace("Importer - InsertDocumentsAndUpdateMappings document " + i);

                Document document = documentsToImport[i];

                UpdateDocumentMappings(document);

                bool documentExists = DocumentModel.DocumentExists(document.DocumentGUID);

                int newDocumentID;
                int oldDocumentID = document.GeneratedDocID;
                //document.GeneratedDocID = -1;

                if (documentExists && DocumentsMatch(document))
                {
                    oldDocumentID = document.GeneratedDocID;

                    Document myDoc = new Document(document.DocumentGUID);

                    documentIDMappings[oldDocumentID] = myDoc.GeneratedDocID;
                    continue;
                }
                else if (documentExists)
                {
                    string documentName = document.DocumentName;

                    string documentConflictMessage = "Your record of " + documentName + " differs from the copy you're importing!"
                                                    + "\n\nWould you like to keep your copy, overwrite with the import copy, or keep both (a new record with be created.)";
                    ImportConflictDialog dialog = new ImportConflictDialog(documentConflictMessage);

                    Point position = parentCenter;
                    position.X -= dialog.Size.Width / 2;
                    position.Y -= dialog.Size.Height / 2;

                    dialog.StartPosition = FormStartPosition.Manual;
                    dialog.Location = position;

                    result = dialog.ShowDialog();
                    showPrompt = !dialog.ApplyToAll;

                    // Use Mine - update the ID to point at my document
                    // but don't update any values
                    if (result == DialogResult.Yes)
                    {
                        Document myDocument = new Document(document.DocumentGUID);
                        oldDocumentID = document.GeneratedDocID;
                        newDocumentID = myDocument.GeneratedDocID;
                        documentIDMappings[oldDocumentID] = newDocumentID;

                        
                        formFieldValuesToImport.Remove(oldDocumentID);
                    }
                    // Use Theirs - update the ID to point at my document
                    // and update the values
                    else if (result == DialogResult.No)
                    {
                        Document myDocument = new Document(document.DocumentGUID);
                        oldDocumentID = document.GeneratedDocID;
                        newDocumentID = myDocument.GeneratedDocID;

                        //DocumentModel.DeleteDocumentPermanently(newDocumentID);
                        DocumentModel.DeleteDocument(newDocumentID);

                        document.GeneratedDocID = newDocumentID;
                        document.Save();

                        documentIDMappings[oldDocumentID] = newDocumentID;
                    }
                    // Keep both
                    else
                    {
                        oldDocumentID = document.GeneratedDocID;

                        document.GeneratedDocID = -1;
                        document.DocumentGUID = Guid.NewGuid();

                        newDocumentID = document.Save();
                        document.GeneratedDocID = newDocumentID;

                        documentIDMappings[oldDocumentID] = newDocumentID;
                    }

                }
                else
                {
                    //UpdateSoldierUnitHierarchyMapping(soldier);
                    //UpdateSoldierImagePath(soldier);

                    oldDocumentID = document.GeneratedDocID;
                    document.GeneratedDocID = -1;

                    Logger.Trace("Importer - InsertDocumentsAndUpdateMappings SAVE document " + i);

                    newDocumentID = document.Save();
                    document.GeneratedDocID = newDocumentID;

                    Logger.Trace("Importer - InsertDocumentsAndUpdateMappings SAVED document " + i);


                    documentIDMappings[oldDocumentID] = newDocumentID;
                }
                //newDocumentID = document.Save();
                //document.GeneratedDocID = newDocumentID;

                //documentIDMappings[oldDocumentID] = newDocumentID;
            }
        }


        private void UpdateDocumentMappings(Document document)
        {
            UpdateDocumentSoldierMapping(document);
            UpdateDocumentParentIDMapping(document);
            UpdateDocumentFilepath(document);
        }


        private void UpdateDocumentSoldierMapping(Document document)
        {
            int oldSoldierID = document.SoldierID;

            if (!soldierIDMappings.Keys.Contains(oldSoldierID))
                throw new DataLoadFailedException("An error occurred while importing one of the documents.\n\n"
                                                    + "Unable to continue.\n\nThe data will not be imported.");

            document.SoldierID = soldierIDMappings[oldSoldierID];
        }


        private void UpdateDocumentParentIDMapping(Document document)
        {
            int oldParentID = document.ParentDocumentID;

            if (oldParentID != -1)
                document.ParentDocumentID = documentIDMappings[oldParentID];
        }


        private void UpdateDocumentFilepath(Document document)
        {
            if (document.Filepath != "")
            {
                document.Filepath = importDirectory.FullName + "\\" + new FileInfo(document.Filepath).Name;
                document.FilepathChanged = true;
            }
        }


        private void InsertFormFieldsAndUpdateMappings()
        {

// POSSIBLE ISSUE HERE ************
// POSSIBLE ISSUE HERE ************
// POSSIBLE ISSUE HERE ************
            DatabaseConnection.BeginTransaction();

            foreach (int oldDocumentID in formFieldValuesToImport.Keys)
            {
                SortedDictionary<int, string> formFieldValues = formFieldValuesToImport[oldDocumentID];
                int newDocumentID = documentIDMappings[oldDocumentID];
                //List<Document> docs = documentsToImport.Where(doc => doc.GeneratedDocID == newDocumentID).ToList();
                
                // potential huge error here!
                //if (docs.Count < 1)
                //    throw new DataLoadFailedException("An error occurred while importing one of the documents.  Unable to continue.\n\nThe data will not be imported.");

                //Document doc = new Document(newDocumentID);

                string tableName = "usergeneratedvalues";

                Document document = new Document(newDocumentID);
                int formID = document.FormID; //docs[0].FormID;

                DataTable userGeneratedValues = DocumentModel.GetUserValuesForDocument(document);

                foreach (int formFieldID in formFieldValues.Keys)
                {
                    string value = formFieldValues[formFieldID];

                    string IV = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", newDocumentID, "documentIV");
                    byte[] IVbytes = Convert.FromBase64String(IV);

                    string encryptedValue = Encryption.Base64EncryptString(value, IVbytes);

                    DataRow row = userGeneratedValues.Select("formfieldid = " + formFieldID.ToString()).First();
                    string pkidStr = row["generatedvalueid"].ToString();

                    DatabaseConnection.Update(tableName, "generatedvaluetext", encryptedValue, "generatedvalueid", pkidStr);
                }

                //List<string> columns = new List<string>(new string[] { "generateddocid", "formid", "formfieldid", "generatedvaluetext" });
                //List<string> values = new List<string>(new string[] { newDocumentID.ToString(), formID.ToString(), "", "" });

                //foreach (int formFieldID in formFieldValues.Keys)
                //{
                //    string value = formFieldValues[formFieldID];

                //    if(value == null)
                //        throw new DataLoadFailedException("An error occurred while importing one of the documents.  Unable to continue.\n\nThe data will not be imported.");

                //    values[2] = formFieldID.ToString();
                //    values[3] = value.ToString();

                //    //DatabaseConnection.Insert(tableName, columns, values);
                    
                //}
            }

            DatabaseConnection.EndTransaction();
// POSSIBLE ISSUE HERE ************
// POSSIBLE ISSUE HERE ************
// POSSIBLE ISSUE HERE ************
        }


        private bool NotesMatch(NoteInterface note)
        {
            NoteInterface myNote = new NoteInterface(note.NoteGUID);

            if (note.Comment != myNote.Comment)
                return false;

            if (note.Date != myNote.Date)
                return false;

            if (note.SoldierID != myNote.SoldierID)
                return false;

            if (note.Subject != myNote.Subject)
                return false;

            return true;
        }


        private void ImportNotesAndUpdateMappings()
        {
            try
            {
                DialogResult result = DialogResult.Yes;
                bool showPrompt = true;

                foreach (NoteInterface note in notesToImport)
                {
                    int oldSoldierID = note.SoldierID;

                    //note.NoteID = -1;
                    note.SoldierID = soldierIDMappings[oldSoldierID];

                    //note.Save();

                    bool noteExists = NotesModel.NoteExists(note.NoteGUID);

                    int newNoteID;
                    int oldNoteID = note.NoteID;
                    //document.GeneratedDocID = -1;

                    if (noteExists && NotesMatch(note))
                    {
                        oldNoteID = note.NoteID;

                        NoteInterface myNote = new NoteInterface(note.NoteGUID);

                        noteIDMappings[oldNoteID] = myNote.NoteID;
                        continue;
                    }
                    else if (noteExists)
                    {
                        string noteSubject = note.Subject;

                        string noteConflictMessage = "Your record of " + noteSubject + " differs from the copy you're importing!"
                                                        + "\n\nWould you like to keep your copy, overwrite with the import copy, or keep both (a new record with be created.)";
                        ImportConflictDialog dialog = new ImportConflictDialog(noteConflictMessage);

                        Point position = parentCenter;
                        position.X -= dialog.Size.Width / 2;
                        position.Y -= dialog.Size.Height / 2;

                        dialog.StartPosition = FormStartPosition.Manual;
                        dialog.Location = position;

                        result = dialog.ShowDialog();
                        showPrompt = !dialog.ApplyToAll;

                        // Use Mine - update the ID to point at my document
                        // but don't update any values
                        if (result == DialogResult.Yes)
                        {
                            NoteInterface myNote = new NoteInterface(note.NoteID);
                            oldNoteID = note.NoteID;
                            newNoteID = myNote.NoteID;
                            noteIDMappings[oldNoteID] = newNoteID;
                        }
                        // Use Theirs - update the ID to point at my document
                        // and update the values
                        else if (result == DialogResult.No)
                        {
                            NoteInterface myNote = new NoteInterface(note.NoteID);
                            oldNoteID = note.NoteID;
                            newNoteID = myNote.NoteID;

                            NotesModel.DeleteNote(newNoteID);

                            note.NoteID = newNoteID;
                            note.Save();

                            noteIDMappings[oldNoteID] = newNoteID;
                        }
                        // Keep both
                        else
                        {
                            oldNoteID = note.NoteID;

                            note.NoteID = -1;
                            note.NoteGUID = Guid.NewGuid();

                            newNoteID = note.Save();
                            note.NoteID = newNoteID;

                            noteIDMappings[oldNoteID] = newNoteID;
                        }

                    }
                    else
                    {
                        //UpdateSoldierUnitHierarchyMapping(soldier);
                        //UpdateSoldierImagePath(soldier);

                        oldNoteID = note.NoteID;
                        note.NoteID = -1;

                        newNoteID = note.Save();
                        note.NoteID = newNoteID;

                        noteIDMappings[oldNoteID] = newNoteID;
                    }
                }
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred while importing one of the notes.  Unable to continue.\n\nThe data will not be imported.");
            }
        }
    }
}
