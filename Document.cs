using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal enum DocumentType : int
    {
        Unknown = -1,
        Counseling = 1,
        //Documentation = 2
        ConinuationOfCounseling = 2,
        Memo = 3,
        InfoPaper = 4,
        Rebuttal = 5,
        Letter = 6,
        Other = 7,
    }


    internal enum DocumentCategory : int
    {
        UserGenerated = -1,
        Counseling = 1,
        Documentation = 2
    }


    class Document
    {
        internal DateTime Date { get; set; }
        internal string DocumentName { get; set; }
        internal DocumentType DocumentType { get; set; }
        internal string Filepath { get; set; }
        internal bool FilepathChanged { get; set; }
        //internal FileUtils.FileType FileType { get; set; }
        internal int FormID { get; set; }
        internal int GeneratedDocID { get; set; }
        internal int ParentDocumentID { get; set; }
        internal int SoldierID { get; set; }
        internal bool SoldierIDChanged { get; set; }
        internal DocumentStatus Status { get; set; }
        internal int TemplateID { get; set; }
        internal bool UserUploaded { get; set; }

        internal Guid DocumentGUID { get; set; }

        internal Document()
        {
            this.Date = new DateTime(0);
            this.DocumentName = "";
            this.DocumentType = DocumentType.Counseling;
            this.Filepath = "";
            this.FilepathChanged = false;
            //this.FileType = FileUtils.FileType.Unknown;
            this.FormID = (int)(DocumentFormIDs.UserGenerated);
            this.GeneratedDocID = -1;
            this.ParentDocumentID = -1;
            this.SoldierID = -1;
            this.Status = DocumentStatus.Draft;
            this.TemplateID = -1;
            this.UserUploaded = false;

            DocumentGUID = Guid.NewGuid();
        }

        internal Document(int generatedDocumentID)
        {
            LoadGeneratedDocumentValues(generatedDocumentID);
        }

        public Document(Guid guid)
        {
            int generatedDocumentID = DocumentModel.GetDocumentID(guid);

            LoadGeneratedDocumentValues(generatedDocumentID);
        }


        internal void LoadGeneratedDocument(int generatedDocumentID)
        {
            //DocumentModel documentModel = new DocumentModel();
            //documentModel.LoadGeneratedDocumentValues(generatedDocumentID);
            //LoadGeneratedDocumentValues(generatedDocumentID);
            //return documentModel;
        }


        protected string GetDocumentValue(DataTable values, string fieldLabel, string column)
        {
            DataRow[] rows = values.Select("fieldlabel = '" + fieldLabel + "'");

            if (rows.Length != 1)
            {
                //throw new DataLoadFailedException("Error loading entry for " + fieldLabel + " found in this document.\n"
                //    + "Unable to load value for this field.");
                return " ";
            }

            string value = rows[0][column].ToString();
            return value;
        }


        protected int GetFormFieldID(DataTable values, string fieldLabel)
        {
            DataRow[] rows = values.Select("fieldlabel = '" + fieldLabel + "'");

            if (rows.Length != 1)
            {
                return -1;
            }

            string value = rows[0]["formfieldid"].ToString();
            return Convert.ToInt32(value);
        }


        protected virtual void LoadGeneratedDocumentValues(int generatedDocumentID)
        {
            //DataTable documentInfo = Model.GetDocumentInfo(generatedDocumentID);
            DataTable documentHeaderInfo = DocumentModel.GetDocumentHeaderInfo(generatedDocumentID);
            DataRow infoRow = documentHeaderInfo.Rows[0];

            SoldierID = Convert.ToInt32(infoRow["soldierid"]);

            Date = Convert.ToDateTime(infoRow["date"]);
            DocumentName = infoRow["documentnametext"].ToString();
            DocumentType = (DocumentType)Convert.ToInt32(infoRow["documenttypeid"]);
            Filepath = infoRow["filepath"].ToString();
            FormID = Convert.ToInt32(infoRow["formid"]);
            GeneratedDocID = generatedDocumentID;
            ParentDocumentID = Convert.ToInt32(infoRow["parentdocumentid"]);
            Status = (DocumentStatus)(Convert.ToInt32(infoRow["statusid"]));
            UserUploaded = Convert.ToBoolean(infoRow["usergenerated"]);
            TemplateID = Convert.ToInt32(infoRow["createdfromtemplateid"]);
            DocumentGUID = new Guid(infoRow["generateddocguid"].ToString());
        }

        //internal static bool AddUserGeneratedDocument(Document document)
        //{
        //    bool success = false;
        //    success = DocumentModel.CopyDocumentToSoldierDirectory(document);

        //    if (!success)
        //        return false;

        //    success = DocumentModel.InsertUserGeneratedDocument(document);

        //    return success;
        //}


        internal virtual void Export(string filename, DocumentFormIDs id)
        {

        }


        internal string GetFullFilepath()
        {
            string soldierDocDirectory = SoldierModel.GetSoldierDocDirectory(SoldierID);
            string fullFilePath = soldierDocDirectory + Filepath;

            return fullFilePath;
        }


        internal int Save()
        {
            return DocumentModel.SaveDocument(this);
        }


        internal int Save(BackgroundWorker worker)
        {
            return DocumentModel.SaveDocument(this, worker);
        }

    }
}
