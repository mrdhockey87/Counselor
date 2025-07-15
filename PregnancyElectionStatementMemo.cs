using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class PregnancyElectionStatementMemo : Document
    {
        internal string OrganizationName { get; set; }
        internal string OrganizationStreetAddress { get; set; }
        internal string OrganizationCityStZip { get; set; }

        internal string OfficeSymbol { get; set; }

        internal string MemorandumForText { get; set; }

        internal string Subject { get; set; }
        internal string Body { get; set; }

        internal string ToPrintedNameOfSoldier { get; set; }
        internal string FromNameOfCommander { get; set; }
        internal string FromNameOfUnit { get; set; }

        internal string PostSoldierSignature1 { get; set; }

        internal string FirstSignatureBlockFromCommandersName { get; set; }
        internal string FirstSignatureBlockFromRankBranch { get; set; }
        internal string FirstSignatureBlockFromCommanderTitle { get; set; }

        internal string ToCommandersName { get; set; }
        internal string ToNameOfUnit { get; set; }

        internal string FromNameOfSoldier { get; set; }

        internal string PostSoldierSignature2 { get; set; }

        internal string SecondSignatureBlockSoldiersName { get; set; }
        internal string SecondSignatureBlockRankSSN { get; set; }

        internal bool HasUnsavedChanges { get; set; }

        internal PregnancyElectionStatementMemo()
        {
            OrganizationName = "";
            OrganizationStreetAddress = "";
            OrganizationCityStZip = "";

            OfficeSymbol = "";

            MemorandumForText = "";

            Subject = "";
            Body = "";

            ToPrintedNameOfSoldier = "";
            FromNameOfCommander = "";
            FromNameOfUnit = "";

            PostSoldierSignature1 = "";

            FirstSignatureBlockFromCommandersName = "";
            FirstSignatureBlockFromRankBranch = "";
            FirstSignatureBlockFromCommanderTitle = "";

            ToCommandersName = "";
            ToNameOfUnit = "";

            FromNameOfSoldier = "";

            PostSoldierSignature2 = "";

            SecondSignatureBlockSoldiersName = "";
            SecondSignatureBlockRankSSN = "";
        }


        internal PregnancyElectionStatementMemo(int documentID) : base(documentID)
        {
            try
            {
                LoadMemoValues(documentID);
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

            OfficeSymbol = GetDocumentValue(documentValues, "Office Symbol", column);

            MemorandumForText = GetDocumentValue(documentValues, "Memorandum For Text", column);

            Subject = GetDocumentValue(documentValues, "Subject", column);
            Body = GetDocumentValue(documentValues, "Body", column);

            ToPrintedNameOfSoldier = GetDocumentValue(documentValues, "Printed Name of Soldier", column);
            FromNameOfCommander = GetDocumentValue(documentValues, "From Name of Commander", column);
            FromNameOfUnit = GetDocumentValue(documentValues, "From Name of Unit", column);

            PostSoldierSignature1 = GetDocumentValue(documentValues, "Post Soldiers Signature", column);

            FirstSignatureBlockFromCommandersName = GetDocumentValue(documentValues, "First SigBlock Commanders Name", column);
            FirstSignatureBlockFromRankBranch = GetDocumentValue(documentValues, "First SigBlock Commander Rank Branch", column);
            FirstSignatureBlockFromCommanderTitle = GetDocumentValue(documentValues, "First SigBlock Commander Title", column);

            ToCommandersName = GetDocumentValue(documentValues, "To Name of Commander", column);
            ToNameOfUnit = GetDocumentValue(documentValues, "To Name of Unit", column);

            FromNameOfSoldier = GetDocumentValue(documentValues, "From Name of Soldier", column);

            PostSoldierSignature2 = GetDocumentValue(documentValues, "Post Soldier Signature 2", column);

            SecondSignatureBlockSoldiersName = GetDocumentValue(documentValues, "Second SigBlock Soldiers Name", column);
            SecondSignatureBlockRankSSN = GetDocumentValue(documentValues, "Second SigBlock Rank, SSN", column);
        }


        internal static PregnancyElectionStatementMemo GenerateNewFromTemplate(Template template)
        {
            PregnancyElectionStatementMemo memo = new PregnancyElectionStatementMemo();

            memo.Date = DateTime.Now;
            memo.DocumentName = template.TemplateName;
            memo.DocumentType = template.DocumentType;
            //memo.FileType = FileUtils.FileType.XFDL;
            memo.Filepath = "";
            memo.FilepathChanged = false;
            memo.FormID = (int)template.FormID;
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
            else if (formfield == "Memorandum For Text")
                MemorandumForText = value;
            else if (formfield == "Post Soldiers Signature")
                PostSoldierSignature1 = value;
            else if (formfield == "Post Soldier Signature 2")
                PostSoldierSignature2 = value;
        }


        internal new void Export(string filename)
        {
            string templateFilename = DocumentModel.GetFormFilename(this.FormID);

            WordDocument memoDoc = new WordDocument(templateFilename, filename);

            memoDoc.PrepForExport(filename);

            DataTable formFields = DatabaseConnection.Query("select * from formfields where formid=" + FormID);

            UpdateBookmarks(memoDoc, formFields);

            memoDoc.Close();
            //wordInterop.Close();
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

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Office Symbol");
                memoWordDoc.SetValue(bookmarkLabel, OfficeSymbol);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Memorandum For Text");
                memoWordDoc.SetValue(bookmarkLabel, MemorandumForText);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Date");
                memoWordDoc.SetValue(bookmarkLabel, Date.ToString("yyyy MM dd"));

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Subject");
                memoWordDoc.SetValue(bookmarkLabel, Subject);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Body");
                memoWordDoc.SetValue(bookmarkLabel, Body);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Printed Name of Soldier");
                memoWordDoc.SetValue(bookmarkLabel, ToPrintedNameOfSoldier);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "From Name of Commander");
                memoWordDoc.SetValue(bookmarkLabel, FromNameOfCommander);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "From Name of Unit");
                memoWordDoc.SetValue(bookmarkLabel, FromNameOfUnit);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Post Soldiers Signature");
                memoWordDoc.SetValue(bookmarkLabel, PostSoldierSignature1);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "First SigBlock Commanders Name");
                memoWordDoc.SetValue(bookmarkLabel, FirstSignatureBlockFromCommandersName);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "First SigBlock Commander Rank Branch");
                memoWordDoc.SetValue(bookmarkLabel, FirstSignatureBlockFromRankBranch);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "First SigBlock Commander Title");
                memoWordDoc.SetValue(bookmarkLabel, FirstSignatureBlockFromCommanderTitle);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "To Name of Commander");
                memoWordDoc.SetValue(bookmarkLabel, ToCommandersName);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "To Name of Unit");
                memoWordDoc.SetValue(bookmarkLabel, ToNameOfUnit);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "From Name of Soldier");
                memoWordDoc.SetValue(bookmarkLabel, FromNameOfSoldier);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Post Soldier Signature 2");
                memoWordDoc.SetValue(bookmarkLabel, PostSoldierSignature2);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Second SigBlock Soldiers Name");
                memoWordDoc.SetValue(bookmarkLabel, SecondSignatureBlockSoldiersName);

                bookmarkLabel = memoWordDoc.GetFieldName(formFields, "Second SigBlock Rank, SSN");
                memoWordDoc.SetValue(bookmarkLabel, SecondSignatureBlockRankSSN);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
