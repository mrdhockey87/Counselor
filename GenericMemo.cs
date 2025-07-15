using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CounselQuickPlatinum
{
    internal class GenericMemo : Document
    {
        const int formID = (int)DocumentFormIDs.GenericMemo;
        internal bool HasUnsavedChanges { get; set; }


        internal string OrganizationName { get; set; }
        internal string OrganizationStreetAddress { get; set; }
        internal string OrganizationCityStZip { get; set; }

        //internal string DocumentTypeForLine { get; set; }
        //internal string RecipientToLine { get; set; }
        internal string MemorandumForLine { get; set; }

        internal string Subject { get; set; }

        internal string Body { get; set; }

        internal string SenderName { get; set; }
        //internal Ranking SenderRank { get; set; }
        internal string SenderRank { get; set; }
        internal string SenderTitle { get; set; }

        internal string Distribution { get; set; }


        internal GenericMemo() 
            : base()
        {
            OrganizationName = "";
            OrganizationStreetAddress = "";
            OrganizationCityStZip = "";

            MemorandumForLine = "";

            Subject = "";

            Body = "";

            SenderName = "";
            SenderRank = "";
            SenderTitle = "";

            Distribution = "";
        }


        internal GenericMemo(int memoID)
            : base(memoID)
        {
            try
            {
                LoadMemoValues(memoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMemoValues(int memoID)
        {
            GeneratedDocID = memoID;
            HasUnsavedChanges = false;

            try
            {
                //DataTable documentValues = DocumentModel.GetUserGeneratedDocumentValuesForForm(memoID);
                DataTable documentValues = DocumentModel.GetUserValuesForDocument(memoID);

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

            OrganizationName = GetDocumentValue(documentValues, "Organization Name", column);
            OrganizationStreetAddress = GetDocumentValue(documentValues, "Organization Street Address", column);
            OrganizationCityStZip = GetDocumentValue(documentValues, "Organization City State Zip", column);

            MemorandumForLine = GetDocumentValue(documentValues, "Memorandum For", column);

            Subject = GetDocumentValue(documentValues, "Subject", column);
            Body = GetDocumentValue(documentValues, "Body", column);

            SenderName = GetDocumentValue(documentValues, "Sender Name", column);
            SenderRank = GetDocumentValue(documentValues, "Sender Rank", column);
            SenderTitle = GetDocumentValue(documentValues, "Sender Title", column);

            Distribution = GetDocumentValue(documentValues, "Distribution", column);
        }


        internal static GenericMemo GenerateNewFromTemplate(Template template)
        {
            GenericMemo memo = new GenericMemo();

            memo.Date = DateTime.Now;
            memo.DocumentName = template.TemplateName;
            memo.DocumentType = template.DocumentType;
            memo.Filepath = "";
            memo.FilepathChanged = false;
            memo.FormID = formID;
            memo.GeneratedDocID = -1;
            memo.Status = DocumentStatus.Draft;
            memo.TemplateID = template.TemplateID;
            memo.UserUploaded = false;

            memo.LoadSelectedTemplateValues(template);

            return memo;
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
            if (formfield == "Subject")
                Subject = value;
            else if (formfield == "Body")
                Body = value;
            else if (formfield == "Distribution")
                Distribution = value;
            else if (formfield == "Memorandum For")
                MemorandumForLine = value;
        }


        internal new void Export(string filename)
        {
            string templateFilename = DocumentModel.GetFormFilename(formID);

            System.IO.FileInfo exportFile = new System.IO.FileInfo(filename);

            string exportFilename = filename;
            string tempFilename = System.IO.Path.GetTempPath() + exportFile.Name;

            System.IO.FileInfo templateFile = new System.IO.FileInfo(templateFilename);
            System.IO.FileInfo tempFile = new System.IO.FileInfo(tempFilename);

            //FileUtils.BlockingFileCopy(templateFile, tempFile);

            WordDocument memoDoc = new WordDocument(templateFilename, tempFilename);

            memoDoc.PrepForExport(tempFilename);

            DataTable formFields = DatabaseConnection.Query("select * from formfields where formid=" + FormID);

            UpdateBookmarks(memoDoc, formFields);

            memoDoc.Close();

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
        }


        void UpdateBookmarks(WordDocument memoWordDoc, DataTable fields)
        {
            DataRow[] formFields = fields.Rows.Cast<DataRow>().ToArray();

            try
            {
                string bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Organization Name");
                memoWordDoc.SetValue(bookmarkLabel, OrganizationName);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Organization Street Address");
                memoWordDoc.SetValue(bookmarkLabel, OrganizationStreetAddress);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Organization City State Zip");
                memoWordDoc.SetValue(bookmarkLabel, OrganizationCityStZip);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Memorandum For");
                memoWordDoc.SetValue(bookmarkLabel, MemorandumForLine);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Date");
                memoWordDoc.SetValue(bookmarkLabel, Date.ToString("yyyy MM dd"));

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Subject");
                memoWordDoc.SetValue(bookmarkLabel, Subject);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Body");
                memoWordDoc.SetValue(bookmarkLabel, Body);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Sender Name");
                memoWordDoc.SetValue(bookmarkLabel, SenderName);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Sender Rank");
                memoWordDoc.SetValue(bookmarkLabel, SenderRank);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Sender Title");
                memoWordDoc.SetValue(bookmarkLabel, SenderTitle);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Distribution");
                memoWordDoc.SetValue(bookmarkLabel, Distribution);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
