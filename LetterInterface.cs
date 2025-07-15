using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class LetterInterface : Document
    {
        const int formID = (int)DocumentFormIDs.Letter;

        internal bool HasUnsavedChanges { get; set; }

        internal string Company { get; set; }
        internal string BattalionSquadron { get; set; }
        internal string CompanyAddressLine1 { get; set; }
        internal string CompanyAddressLine2 { get; set; }

        internal string RecepientName { get; set; }
        internal string ReceipientAddressLine1 { get; set; }
        internal string ReceipientAddressLine2 { get; set; }

        internal string Greeting { get; set; }

        internal string Body { get; set; }

        internal string SoldiersName { get; set; }
        internal string Rank { get; set; }
        internal string Title { get; set; }


        internal LetterInterface()
            : base()
        {
            Company = "";
            BattalionSquadron = "";
            CompanyAddressLine1 = "";
            CompanyAddressLine2 = "";
            
            RecepientName = "";
            ReceipientAddressLine1 = "";
            ReceipientAddressLine2 = "";
            
            Greeting = "";
            
            Body = "";
            
            SoldiersName = "";
            Rank = "";
            Title = "";
        }


        internal LetterInterface(int letterID)
            : base(letterID)
        {
            try
            {
                LoadLetterValues(letterID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LoadLetterValues(int letterID)
        {
            GeneratedDocID = letterID;
            HasUnsavedChanges = false;

            try
            {
                //DataTable documentValues = DocumentModel.GetUserGeneratedDocumentValuesForForm(letterID);
                DataTable documentValues = DocumentModel.GetUserValuesForDocument(letterID);

                if (documentValues.Columns.Contains("generatedvaluetext") != true
                    || documentValues.Rows.Count == 0)
                {
                    throw new DataLoadFailedException("No values found for this document.\n");
                }

                LoadDocumentValues(documentValues);
            }
            catch (DataLoadFailedException ex)
            {
                throw ex;
            }
        }


        private void LoadDocumentValues(DataTable documentValues)
        {
            string column = "generatedvaluetext";

            Company = GetDocumentValue(documentValues, "COMPANY", column);
            BattalionSquadron = GetDocumentValue(documentValues, "BATTALIONSQUADRON", column);
            CompanyAddressLine1 = GetDocumentValue(documentValues, "COMPANYADDRESSLINE1", column);
            CompanyAddressLine2 = GetDocumentValue(documentValues, "COMPANYADDRESSLINE2", column);

            RecepientName = GetDocumentValue(documentValues, "RECEPIENTNAME", column);
            ReceipientAddressLine1 = GetDocumentValue(documentValues, "ADDRESSLINE1", column);
            ReceipientAddressLine2 = GetDocumentValue(documentValues, "ADDRESSLINE2", column);

            Greeting = GetDocumentValue(documentValues, "DEARRECEIPIENT", column);

            Body = GetDocumentValue(documentValues, "BODY", column);

            SoldiersName = GetDocumentValue(documentValues, "SOLDIERSNAME", column);
            Rank = GetDocumentValue(documentValues, "RANK", column);
            Title = GetDocumentValue(documentValues, "TITLE", column);
        }


        internal static LetterInterface GenerateNewFromTemplate(Template template)
        {
            LetterInterface letter = new LetterInterface();

            letter.Date = DateTime.Now;
            letter.DocumentName = template.TemplateName;
            letter.DocumentType = template.DocumentType;
            letter.Filepath = "";
            letter.FilepathChanged = false;
            letter.FormID = formID;
            letter.GeneratedDocID = -1;
            letter.Status = DocumentStatus.Draft;
            letter.TemplateID = template.TemplateID;
            letter.UserUploaded = false;

            letter.LoadSelectedTemplateValues(template);

            return letter;
        }

        private void LoadSelectedTemplateValues(Template template)
        {
            foreach (string formfield in template.TemplateValues.Keys)
            {
                if (template.TemplateValues[formfield].Count == 1)
                {
                    FillFormField(formfield, template.TemplateValues[formfield][0]);
                }
            }
        }


        protected void FillFormField(string formfield, string value)
        {
            if (formfield == "BODY")
                Body = value;
        }

        internal new void Export(string filename)
        {
            string templateFilename = DocumentModel.GetFormFilename(formID);

            System.IO.FileInfo exportFile = new System.IO.FileInfo(filename);

            string exportFilename = filename;
            string tempFilename = System.IO.Path.GetTempPath() + exportFile.Name;
            //string tempFilename = "C:\\temp\\" + exportFile.Name;

            System.IO.FileInfo templateFile = new System.IO.FileInfo(templateFilename);
            System.IO.FileInfo tempFile = new System.IO.FileInfo(tempFilename);

            //FileUtils.BlockingFileCopy(templateFile, tempFile);

            WordDocument letterDoc = new WordDocument(templateFilename, tempFilename);

            letterDoc.PrepForExport(tempFilename);

            DataTable formFields = DatabaseConnection.Query("select * from formfields where formid=" + FormID);

            UpdateBookmarks(letterDoc, formFields);

            letterDoc.Close();

            if (FileUtils.IsFileLocked(exportFile))
            {
                string error = filename + " is locked and cannot be overwritten - close the file and try again.";
                string caption = "Error - File Locked";

                CQPMessageBox.CQPMessageBoxButtons button = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                CQPMessageBox.ShowDialog(error, caption, button, icon);
                return;
            }

            FileUtils.BlockingFileCopy(tempFile, exportFile);

            //wordInterop.Close();
        }


        void UpdateBookmarks(WordDocument letterWordDoc, DataTable fields)
        {
            DataRow[] formFields = fields.Rows.Cast<DataRow>().ToArray();

            try
            {
                string bookmarkLabel = letterWordDoc.GetFieldName(formFields, "COMPANY");
                letterWordDoc.SetValue(bookmarkLabel, Company);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "DATE");
                letterWordDoc.SetValue(bookmarkLabel, Date.ToString("yyyy mm dd"));

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "BATTALIONSQUADRON");
                letterWordDoc.SetValue(bookmarkLabel, BattalionSquadron);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "COMPANYADDRESSLINE1");
                letterWordDoc.SetValue(bookmarkLabel, CompanyAddressLine1);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "COMPANYADDRESSLINE2");
                letterWordDoc.SetValue(bookmarkLabel, CompanyAddressLine2);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "RECEIPIENTNAME");
                letterWordDoc.SetValue(bookmarkLabel, RecepientName);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "ADDRESSLINE1");
                letterWordDoc.SetValue(bookmarkLabel, ReceipientAddressLine1);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "ADDRESSLINE2");
                letterWordDoc.SetValue(bookmarkLabel, ReceipientAddressLine2);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "DEARRECEIPIENT");
                letterWordDoc.SetValue(bookmarkLabel, Greeting);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "BODY");
                letterWordDoc.SetValue(bookmarkLabel, Body);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "SOLDIERSNAME");
                letterWordDoc.SetValue(bookmarkLabel, SoldiersName);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "RANK");
                letterWordDoc.SetValue(bookmarkLabel, Rank);

                bookmarkLabel = letterWordDoc.GetFieldName(formFields, "TITLE");
                letterWordDoc.SetValue(bookmarkLabel, Title);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
