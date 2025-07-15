using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    internal class DataExporter
    {
        private List<Soldier> soldiersToExport;
        private List<Document> documentsToExport;
        private List<NoteInterface> notesToExport;
        private List<UnitHierarchyModel.UnitHierarchy> unitHierarchiesToExport;
        private List<UnitHierarchyModel.Battalion> battalionsToExport;
        private SortedDictionary<int, SortedDictionary<int, string>> formFieldsToExport;

        private SortedDictionary<int, int> battalionIDMappings;
        private SortedDictionary<int, int> unitHierarchyIDMappings;
        private SortedDictionary<int, int> soldierIDMappings;
        private SortedDictionary<int, int> documentIDMappings;
        private SortedDictionary<int, int> noteIDMappings;

        int databaseVersionNumber;

        System.Xml.XmlTextWriter xmlFile;
        DirectoryInfo exportDirectory;

        internal enum CopyStatus
        {
            Success,
            Fail,
            Cancel
        }


        internal class ExportException : Exception
        {
        }


        internal DataExporter()
        {
            battalionsToExport = new List<UnitHierarchyModel.Battalion>();
            unitHierarchiesToExport = new List<UnitHierarchyModel.UnitHierarchy>();
            soldiersToExport = new List<Soldier>();
            documentsToExport = new List<Document>();
            notesToExport = new List<NoteInterface>();
            formFieldsToExport = new SortedDictionary<int, SortedDictionary<int, string>>();

            battalionIDMappings = new SortedDictionary<int, int>();
            unitHierarchyIDMappings = new SortedDictionary<int, int>();
            soldierIDMappings = new SortedDictionary<int,int>();
            documentIDMappings = new SortedDictionary<int, int>();
            noteIDMappings = new SortedDictionary<int, int>();

            databaseVersionNumber = SettingsModel.DatabaseVersion;
        }


        internal void AddSoldier(Soldier soldier)
        {
            int oldSoldierID = soldier.SoldierID;
            int newSoldierID = soldiersToExport.Count();

            soldierIDMappings[oldSoldierID] = newSoldierID;

            soldiersToExport.Add(soldier);
        }


        internal void AddDocument(Document document)
        {
            int oldDocumentID = document.GeneratedDocID;
            int newDocumentID = documentsToExport.Count();

            documentIDMappings[oldDocumentID] = newDocumentID;

            documentsToExport.Add(document);
        }


        internal void AddNote(NoteInterface note)
        {
            int oldNoteID = note.NoteID;
            int newNoteID = notesToExport.Count();

            noteIDMappings[oldNoteID] = newNoteID;

            notesToExport.Add(note);
        }


        private int InsertOrGetExportedBattalionID(int battalionID, string battalionName)
        {
            IEnumerable<UnitHierarchyModel.Battalion> battalions;
            battalions = battalionsToExport.Where(b => b.BattalionName == battalionName);

            if (battalions.Count() > 0)
            {
                UnitHierarchyModel.Battalion battalion;
                battalion = battalions.First();
                return battalion.BattalionID;
            }
            else
            {
                UnitHierarchyModel.Battalion battalion = new UnitHierarchyModel.Battalion();

                int oldBattalionID = battalionID;
                int newBattalionID = battalionsToExport.Count;

                battalion.BattalionID = newBattalionID;
                battalion.BattalionName = battalionName;
                battalionsToExport.Add(battalion);

                battalionIDMappings[oldBattalionID] = newBattalionID;

                return battalion.BattalionID;
            }
        }


        private int InsertOrGetExportedUnitHierarchyID(UnitHierarchyModel.UnitHierarchy unitHierarchy)
        {
            int oldBattalionID = unitHierarchy.battalionID;
            unitHierarchy.battalionID = battalionIDMappings[unitHierarchy.battalionID];

            IEnumerable<UnitHierarchyModel.UnitHierarchy> unitHierarchies;
            unitHierarchies = unitHierarchiesToExport.Where(u => u == unitHierarchy);

            unitHierarchy.battalionID = oldBattalionID;

            if (unitHierarchies.Count() > 0)
            {
                return unitHierarchy.unitHierarchyID;
            }
            else
            {
                int oldUnitHierarchyID = unitHierarchy.unitHierarchyID;
                int newUnitHierarchyID = unitHierarchiesToExport.Count();

                unitHierarchyIDMappings.Add(oldUnitHierarchyID, newUnitHierarchyID);

                unitHierarchiesToExport.Add(unitHierarchy);
                return newUnitHierarchyID;
            }
        }


        internal void WriteExportFile(FileInfo outputFile)
        {
            PrepareLists();
            WriteFile(outputFile);
        }


        internal void PrepareLists()
        {
            foreach (Soldier soldier in soldiersToExport)
            {
                int battalionID = soldier.UnitHierarchy.battalionID;
                string battalionName = soldier.UnitHierarchy.battalionName;

                InsertOrGetExportedBattalionID(battalionID, battalionName);
                InsertOrGetExportedUnitHierarchyID(soldier.UnitHierarchy);
            }
        }


        private void WriteFile(FileInfo outputFile)
        {
            try
            {
                string tempPath = Path.GetTempPath() + DateTime.Now.Ticks + "export";
                //string tempPath = Path.GetTempFileName();
                exportDirectory = Directory.CreateDirectory(tempPath);

                string basename = outputFile.Name.Substring(0, outputFile.Name.Length - 5);

                WriteXMLFile(basename);
                WriteCQPXFile(outputFile);

                ScrubExportDirectory(tempPath);
            }
            catch (ExportException)
            {
                CQPMessageBox.ShowDialog("An error occurred during the export.  Export cancelled.");
                try
                {
                    if (outputFile.Exists)
                        outputFile.Delete();
                }
                catch (Exception)
                { }
            }
        }


        private void ScrubExportDirectory(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();

            foreach (FileInfo file in files)
            {
                FileStream decryptedFileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Write);
                long bytes = file.Length;
                byte[] zero = new byte[1] { 0 };
                for (int i = 0; i < bytes; i++)
                    decryptedFileStream.Write(zero, 0, 1);
                decryptedFileStream.Close();
                File.Delete(file.FullName);
            }

            Directory.Delete(path, true);
        }


        private void WriteXMLFile(string basename)
        {
            string xmlFilename = exportDirectory.FullName + "\\" + basename + ".xml";
            
            //xmlFile = new System.Xml.XmlTextWriter("export\\" + basename + ".xml", Encoding.ASCII);
            xmlFile = new System.Xml.XmlTextWriter(xmlFilename, Encoding.ASCII);
            xmlFile.Formatting = System.Xml.Formatting.Indented;
            xmlFile.Indentation = 4;

            //xmlFile.WriteStartElement("CounselQuickPlatinum");
            xmlFile.WriteStartElement("Counselor");

            WriteVersionNumber();
            WriteBattalions();
            WriteUnitHierarchies();
            WriteSoldiers();
            WriteDocuments();
            WriteNotes();

            xmlFile.WriteEndElement();
            xmlFile.Flush();
            xmlFile.Close();
        }


        private void WriteCQPXFile(FileInfo outputFile)
        {
            //string outputFilename = basename + ".cqpx";
            
            //DirectoryInfo directoryInfo = new DirectoryInfo("export");
            
            FileInfo[] files = exportDirectory.GetFiles();

            Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile();
            zipfile.AddDirectory(exportDirectory.FullName);
            zipfile.Save(outputFile.FullName);
        }


        private void WriteVersionNumber()
        {
            xmlFile.WriteStartElement("DatabaseVersion");

            string version = databaseVersionNumber.ToString();
            xmlFile.WriteAttributeString("number", version);

            xmlFile.WriteEndElement();
        }


        private void WriteBattalions()
        {
            xmlFile.WriteStartElement("Battalions"); // <Battalions>

            foreach(UnitHierarchyModel.Battalion battalion in battalionsToExport)
            {
                xmlFile.WriteStartElement("Battalion"); // [<Battalion] .../>
                
                string id = battalion.BattalionID.ToString();
                string name = battalion.BattalionName;

                xmlFile.WriteAttributeString("id", id);
                xmlFile.WriteAttributeString("name", name);

                xmlFile.WriteEndElement(); // <Battalion ...  [/>]
            }

            xmlFile.WriteEndElement(); // </Battalions>
        }


        private void WriteUnitHierarchies()
        {
            xmlFile.WriteStartElement("UnitHierarchies"); // <UnitHierarchies>

            foreach (UnitHierarchyModel.UnitHierarchy unitHierarchy in unitHierarchiesToExport)
            {
                xmlFile.WriteStartElement("UnitHierarchy"); // [<UnitHierarchy]  ... />

                int oldUnitHierarchyID = unitHierarchy.unitHierarchyID;
                int oldBattalionID = unitHierarchy.battalionID;

                int newBattalionID = battalionIDMappings[oldBattalionID];
                int newUnitHierarchyID = unitHierarchyIDMappings[oldUnitHierarchyID];

                string id = newUnitHierarchyID.ToString(); //unitHierarchy.unitHierarchyID.ToString();
                string battalionID = newBattalionID.ToString(); //unitHierarchy.battalionID.ToString();
                string unitID = unitHierarchy.unitID.ToString();
                string unitDesignatorID = unitHierarchy.unitDesignatorID.ToString();
                string platoonID = unitHierarchy.platoonID.ToString();
                string squadID = unitHierarchy.squadID.ToString();

                xmlFile.WriteAttributeString("id", id);
                xmlFile.WriteAttributeString("battalionid", battalionID);
                xmlFile.WriteAttributeString("unitid", unitID);
                xmlFile.WriteAttributeString("unitdesignatorid", unitDesignatorID);
                xmlFile.WriteAttributeString("platoonid", platoonID);
                xmlFile.WriteAttributeString("squadid", squadID);

                xmlFile.WriteEndElement(); // <UnitHierarchy  ... [/>]
            }

            xmlFile.WriteEndElement(); // </UnitHierarchies>
        }


        private void WriteSoldiers()
        {
            xmlFile.WriteStartElement("Soldiers"); // <Soldiers>

            foreach (Soldier soldier in soldiersToExport)
            {
                if (soldier.Picture != null)
                {
                    string localFullpath = SoldierModel.GetSoldierImagePath(soldier.SoldierID);
                    string exportFullPath = exportDirectory.FullName + "\\" + soldier.soldierGUID.ToString();
                    if (File.Exists(localFullpath))
                        File.Copy(localFullpath, exportFullPath);
                }

                int oldSoldierID = soldier.SoldierID;
                int newSoldierID = soldierIDMappings[oldSoldierID];

                int oldUnitHierarchyID = soldier.UnitHierarchy.unitHierarchyID;
                int newUnitHierarchyID = unitHierarchyIDMappings[oldUnitHierarchyID];

                //byte[] IV = Encryption.GenerateIV();
                //string IVstring = Convert.FromBase64String(IV);

                string IVstring = DatabaseConnection.GetSingleValue("soldiers", "soldierID", oldSoldierID, "soldierIV");
                byte[] IV = Convert.FromBase64String(IVstring);

                string id = newSoldierID.ToString();
                string rankingid = ((int)soldier.Rank).ToString();
                string firstname = Convert.ToBase64String(Encryption.EncryptString(soldier.FirstName, IV));
                string lastname =  Convert.ToBase64String(Encryption.EncryptString(soldier.LastName, IV));
                string dateofrank = Convert.ToBase64String(Encryption.EncryptString(soldier.DateOfRank.Ticks.ToString(), IV));
                string dateofbirth = Convert.ToBase64String(Encryption.EncryptString(soldier.DateOfBirth.Ticks.ToString(), IV));
                string otherstatus = Convert.ToBase64String(Encryption.EncryptString(soldier.OtherStatus, IV));
                //string imagefilepath = soldier.PictureFilename;
                string unithierarchy = Convert.ToBase64String(Encryption.EncryptString(newUnitHierarchyID.ToString(), IV));
                string middleinitial = Convert.ToBase64String(Encryption.EncryptString(soldier.MiddleInitial.ToString(), IV));
                string guid = soldier.soldierGUID.ToString();

                xmlFile.WriteStartElement("Soldier"); // <Soldier>

                xmlFile.WriteAttributeString("id", id);
                xmlFile.WriteAttributeString("rankingid", rankingid);
                xmlFile.WriteAttributeString("lastname", lastname);
                xmlFile.WriteAttributeString("firstname", firstname);
                xmlFile.WriteAttributeString("dateofrank", dateofrank);
                xmlFile.WriteAttributeString("dateofbirth", dateofbirth);
                xmlFile.WriteAttributeString("otherstatus", otherstatus);
                //xmlFile.WriteAttributeString("imagefilepath", imagefilepath);
                xmlFile.WriteAttributeString("unithierarchy", unithierarchy);
                xmlFile.WriteAttributeString("middleinitial", middleinitial);
                xmlFile.WriteAttributeString("soldierIV", IVstring);
                xmlFile.WriteAttributeString("guid", guid);

                WriteSoldierStatuses(soldier);

                xmlFile.WriteEndElement(); // </Soldier>
            }

            xmlFile.WriteEndElement(); // </Soldiers>
        }


        private void WriteSoldierStatuses(Soldier soldier)
        {
            xmlFile.WriteStartElement("Statuses"); // <SoldierStatuses>

            foreach (SoldierStatus status in soldier.Statuses.Values)
            {
                xmlFile.WriteStartElement("Status");
                xmlFile.WriteAttributeString("id", status.statusEnumID.ToString());
                xmlFile.WriteAttributeString("applies", status.applies ? "1" : "0");
                xmlFile.WriteAttributeString("statusname", status.statusString);
                xmlFile.WriteEndElement();
            }

            xmlFile.WriteEndElement(); // </SoldierStatuses>
        }



        private void WriteDocuments()
        {
            xmlFile.WriteStartElement("UserGeneratedDocs");

            foreach (Document document in documentsToExport)
            {
                if (document.UserUploaded)
                {
                    FileInfo fileInfo = new FileInfo(document.Filepath);
                    if (!fileInfo.Exists)
                    {

                        Soldier soldier = new Soldier(document.SoldierID);
                        string soldierName = soldier.Rank.ToString() + " " + soldier.LastName + ", " + soldier.FirstName;
                        string docName = document.DocumentName;

                        string message = "An error occurred while attempting to export user generated document:\n"
                                        + docName + " for soldier:\n"
                                        + soldierName + "\n"
                                        + "\n"
                                        + "Error:  File cannot be located for exporting.\n"
                                        + "Skip this file or abort exporting?";
                        string caption = "Copy error";
                        CQPMessageBox.CQPMessageBoxIcon warning = CQPMessageBox.CQPMessageBoxIcon.Warning;
                        CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.SkipAbort;

                        DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, warning);

                        if (result == DialogResult.Abort)
                            throw new ExportException();
                        else
                            continue;
                    }
                }


                RemapDocumentValues(document);

                WriteDocumentNode(document);

            }

            xmlFile.WriteEndElement();
        }


        private void WriteDocumentNode(Document document)
        {
            xmlFile.WriteStartElement("UserGeneratedDoc");

            int oldDocumentID = document.GeneratedDocID;
            int newDocumentID = documentIDMappings[oldDocumentID];

            string filename = "";

            if (document.Filepath != "")
            {
                FileInfo file = new FileInfo(document.Filepath);
                filename = file.Name;
            }

            string IV = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", oldDocumentID, "documentIV");
            byte[] IVbytes = Convert.FromBase64String(IV);

            //byte[] IVbytes = Encryption.GetBytes(IV);

            string id = newDocumentID.ToString(); //document.GeneratedDocID.ToString();
            string soldierid = document.SoldierID.ToString();
            string date = Encryption.Base64EncryptString(document.Date.Ticks.ToString(), IVbytes);
            string documentname = Encryption.Base64EncryptString(document.DocumentName, IVbytes);
            string documenttype = Encryption.Base64EncryptString(((int)document.DocumentType).ToString(), IVbytes);
            string formid = Encryption.Base64EncryptString(document.FormID.ToString(), IVbytes);
            string usergenerated = Encryption.Base64EncryptString((Convert.ToInt32(document.UserUploaded)).ToString(), IVbytes);
            string filepath = document.Filepath;
            string statusid = Encryption.Base64EncryptString(((int)document.Status).ToString(), IVbytes);
            string formdatatype = "-1";
            string parentdocumentid = Encryption.Base64EncryptString(document.ParentDocumentID.ToString(), IVbytes);
            string createdfromtemplate = Encryption.Base64EncryptString(document.TemplateID.ToString(), IVbytes);
            string guid = document.DocumentGUID.ToString();


            xmlFile.WriteAttributeString("id", id);
            xmlFile.WriteAttributeString("soldierid", soldierid);
            xmlFile.WriteAttributeString("date", date);
            xmlFile.WriteAttributeString("documentname", documentname);
            xmlFile.WriteAttributeString("documenttypeid", documenttype);
            xmlFile.WriteAttributeString("formid", formid);
            xmlFile.WriteAttributeString("usergenerated", usergenerated);
            xmlFile.WriteAttributeString("filepath", filepath);
            xmlFile.WriteAttributeString("statusid", statusid);
            xmlFile.WriteAttributeString("formdatatype", formdatatype);
            xmlFile.WriteAttributeString("parentdocumentid", parentdocumentid);
            xmlFile.WriteAttributeString("createdfromtemplate", createdfromtemplate);
            xmlFile.WriteAttributeString("documentIV", IV);
            xmlFile.WriteAttributeString("guid", guid);

            if (document.UserUploaded == false)
                WriteDocumentFormFields(document, IVbytes);
            else
            {
                CopyStatus status = CopyUserGeneratedDoc(document, IVbytes);
                if (status == CopyStatus.Cancel)
                    throw new DataException();
            }

            xmlFile.WriteEndElement();
        }


        private void RemapDocumentValues(Document document)
        {
            //RemapGeneratedDocumentID(document);
            RemapDocumentSoldierID(document);
            RemapDocumentFilename(document);
            RemapDocumentParentID(document);
        }


        //private void RemapGeneratedDocumentID(Document document)
        //{
        //    int oldDocumentID = document.GeneratedDocID;
        //    int newDocumentID = documentIDMappings[oldDocumentID];

        //    document.GeneratedDocID = newDocumentID;
        //}


        private void RemapDocumentSoldierID(Document document)
        {
            int newSoldierID;

            if (soldierIDMappings.Keys.Contains(document.SoldierID))
                newSoldierID = soldierIDMappings[document.SoldierID];
            else
                newSoldierID = -1;

            document.SoldierID = newSoldierID;
        }


        private void RemapDocumentFilename(Document document)
        {
            string filename;

            if (document.UserUploaded)
            {
                FileInfo localFilepath = new FileInfo(document.Filepath);
                //string localFileName = localFilepath

                filename = localFilepath.FullName; 
            }
            else
            {
                filename = "";
            }

            document.Filepath = filename;
        }


        private void RemapDocumentParentID(Document document)
        {
            int oldParentDocumentID = document.ParentDocumentID;
            int newParentDocumentID;

            if (!documentIDMappings.Keys.Contains(oldParentDocumentID))
                newParentDocumentID = -1;
            else
                newParentDocumentID = documentIDMappings[oldParentDocumentID];

            document.ParentDocumentID = newParentDocumentID;
        }


        private void WriteDocumentFormFields(Document document, byte[] IV)
        {
            xmlFile.WriteStartElement("Values");

            DataTable values = DocumentModel.GetUserValuesForDocument(document);

            foreach (DataRow row in values.Rows)
            {
                xmlFile.WriteStartElement("FormField"); // [<FormField] ....>

                string id = row["formfieldid"].ToString();
                string value = row["generatedvaluetext"].ToString();

                string base64EncryptedValue = Encryption.Base64EncryptString(value, IV);

                xmlFile.WriteAttributeString("id", id);
                //xmlFile.WriteAttributeString("value", value);
                xmlFile.WriteAttributeString("value", base64EncryptedValue);

                xmlFile.WriteEndElement(); // <FormField .... [ />]
            }

            xmlFile.WriteEndElement();  // </Values>
        }


        private CopyStatus CopyUserGeneratedDoc(Document document, byte[] IV)
        {
            //System.IO.File.Copy(filepath, "export\\" + filepath);
            //FileInfo file = new FileInfo(document.Filepath);

            //if (!file.Exists)
            //{
                //Soldier soldier = new Soldier(document.SoldierID);
                //string soldierName = soldier.Rank.ToString() + " " + soldier.LastName + ", " + soldier.FirstName;
                //string docName = document.DocumentName;

                //string message = "An error occurred while attempting to export user generated document:\n"
                //                + docName + " for soldier:\n"
                //                + soldierName + "\n"
                //                +"\n"
                //                + "Error:  File cannot be located for exporting.\n"
                //                + "Skip this file or abort exporting?";
                //string caption = "Copy error";
                //CQPMessageBox.CQPMessageBoxIcon warning = CQPMessageBox.CQPMessageBoxIcon.Warning;
                //CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.SkipAbort;

                //DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, warning);

            //    if (result == DialogResult.Abort)
            //        return CopyStatus.Cancel;
            //    else
            //        return CopyStatus.Fail;
            //}

            FileInfo file = new FileInfo(document.Filepath);
            string filename = file.Name;

            System.IO.File.Copy(document.Filepath, exportDirectory.FullName + "\\" + filename);

            //Encryption.EncryptFile(document.Filepath, exportDirectory.FullName + "\\" + filename, IV);
            return CopyStatus.Success;
        }


        private void WriteNotes()
        {
            xmlFile.WriteStartElement("Notes"); // <Notes>

            foreach (NoteInterface note in notesToExport)
            {
                int newSoldierID = soldierIDMappings[note.SoldierID];

                string id = note.NoteID.ToString();
                string soldierid = newSoldierID.ToString();
                string date = note.Date.Ticks.ToString();
                string guid = note.NoteGUID.ToString();

                string IV = DatabaseConnection.GetSingleValue("notes", "noteid", note.NoteID, "noteIV");
                byte[] IVbytes = Convert.FromBase64String(IV);

                string base64EncryptedDate = Encryption.Base64EncryptString(date, IVbytes);
                string base64EncryptedSubject = Encryption.Base64EncryptString(note.Subject, IVbytes);
                string base64EncryptedValue = Encryption.Base64EncryptString(note.Comment, IVbytes);

                xmlFile.WriteStartElement("Note"); // <Note>
                
                xmlFile.WriteAttributeString("id", id);
                xmlFile.WriteAttributeString("soldierid", soldierid);
                //xmlFile.WriteAttributeString("date", date);
                xmlFile.WriteAttributeString("date", base64EncryptedDate);
                xmlFile.WriteAttributeString("guid", guid);
                xmlFile.WriteAttributeString("noteIV", IV);

                xmlFile.WriteStartElement("Values");

                //xmlFile.WriteAttributeString("subject", note.Subject);
                //xmlFile.WriteAttributeString("comment", note.Comment);
                xmlFile.WriteAttributeString("subject", base64EncryptedSubject);
                xmlFile.WriteAttributeString("comment", base64EncryptedValue);

                xmlFile.WriteEndElement(); // </Values>

                xmlFile.WriteEndElement(); // </Note>
            }

            xmlFile.WriteEndElement(); // </Notes>
        }
    }
}
