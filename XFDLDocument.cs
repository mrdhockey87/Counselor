using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class XFDLDocument : Document
    {
        const int formID = (int)DocumentFormIDs.DA4856;

        internal bool HasUnsavedAssessmentChanges { get; set; }
        internal bool HasUnsavedDocumentChanges { get; set; }

        internal string Name { get; set; }
        internal Ranking Rank { get; set; }
        //public DateTime DateOfCounseling { get; set; }
        internal string NameOfOrganization { get; set; }
        internal string NameAndTitleOfCounselor { get; set; }
        internal string PurposeOfCounseling { get; set; }
        internal string KeyPointsOfDiscussion { get; set; }
        internal string PlanOfActions { get; set; }
        internal bool IAgree { get; set; }
        internal bool IDisagree { get; set; }
        internal string SessionClosing { get; set; }
        internal string LeaderResponsibilities { get; set; }
        internal string Assessment { get; set; }
        internal string Counselor { get; set; }
        internal string IndividualCounseled { get; set; }
        internal DateTime DateOfAssessment { get; set; }
        internal DateTime DateAssessmentPerformed { get; set; }

        public bool HasUnsavedContinuationChanges { get; set; }

        public string Continuation { get; set; }


        internal XFDLDocument()
        {
            this.Assessment = "";
            this.Counselor = "";
            this.Continuation = "";
            this.Date = new DateTime(0);
            this.DateAssessmentPerformed = new DateTime(0);
            this.DateOfAssessment = new DateTime(0);
            this.DocumentName = "";
            this.DocumentType = DocumentType.Counseling;
            this.Filepath = "";
            this.FilepathChanged = false;
            this.FileType = FileUtils.FileType.Unknown;
            this.FormID = (int)(DocumentFormIDs.UserGenerated);
            this.GeneratedDocID = -1;
            this.HasUnsavedAssessmentChanges = false;
            this.HasUnsavedDocumentChanges = false;
            this.HasUnsavedContinuationChanges = false;
            this.IAgree = false;
            this.IDisagree = false;
            this.IndividualCounseled = "";
            this.KeyPointsOfDiscussion = "";
            this.LeaderResponsibilities = "";
            this.Name = "";
            this.NameAndTitleOfCounselor = "";
            this.NameOfOrganization = "";
            this.ParentDocumentID = -1;
            this.PlanOfActions = "";
            this.PurposeOfCounseling = "";
            this.Rank = Ranking.PVT;
            this.SessionClosing = "";
            this.SoldierID = -1;
            this.Status = DocumentStatus.Draft;
            this.TemplateID = -1;
            this.UserUploaded = false;
        }


        internal XFDLDocument(int documentID) : base(documentID)
        {
            LoadGeneratedDocumentValues(documentID);
        }


        private new void LoadGeneratedDocumentValues(int generatedDocumentID)
        {
            //this.generatedDocumentID = generatedDocumentID;
            this.GeneratedDocID = generatedDocumentID;
            HasUnsavedDocumentChanges = false;

            try
            {
                DataTable documentValues = XFDLDocumentModel.GetXFDLValuesForForm(generatedDocumentID);

                if (documentValues.Columns.Contains("generatedvaluetext") != true
                    || documentValues.Rows.Count == 0)
                {
                    throw new DataLoadFailedException("No values found for this document.\n");
                }

                LoadDocumentValues(documentValues, "generatedvaluetext");

            }
            catch (DataLoadFailedException ex)
            {
                throw ex;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException("Error retrieving values for selected document", ex);
            }
            catch (FormatException ex)
            {
                throw new DataLoadFailedException("Error retrieving values for selected document", ex);
            }
        }





        //internal static XFDLDocument GenerateNewFromTemplate(int templateID)
        //{
        //    XFDLDocument template = new XFDLDocument();
        //    string templateName = TemplatesModel.GetTemplateNameByTemplateID(templateID);

        //    template.Date = DateTime.Now;
        //    //template.Date = DateTime.Now;
        //    template.DocumentName = templateName;
        //    template.DocumentType = DocumentType.Counseling;
        //    template.FileType = FileUtils.FileType.XFDL;
        //    template.FormID = 1;
        //    template.GeneratedDocID = -1;
        //    template.HasUnsavedDocumentChanges = false;
        //    template.HasUnsavedAssessmentChanges = false;
        //    template.Status = DocumentStatus.Draft;
        //    template.UserUploaded = false;

        //    DataTable templateValues = TemplatesModel.GetTemplateValuesByTemplateID(templateID);

        //    template.LoadDocumentValues(templateValues, "templatevaluetext");

        //    return template;
        //}


        internal static XFDLDocument GenerateNewFromTemplate(Template template)
        {
            XFDLDocument document = new XFDLDocument();

            document.Continuation = "";
            document.Date = DateTime.Now;
            document.DocumentName = template.TemplateName;
            document.DocumentType = template.DocumentType;
            document.FileType = FileUtils.FileType.XFDL;
            document.Filepath = "";
            document.FilepathChanged = false;
            document.FormID = formID;
            document.GeneratedDocID = -1;
            document.HasUnsavedAssessmentChanges = false;
            document.HasUnsavedDocumentChanges = false;
            document.Status = DocumentStatus.Draft;
            document.TemplateID = template.TemplateID;
            document.UserUploaded = false;

            document.LoadSelectedTemplateValues(template);

            return document;
        }


        protected void LoadSelectedTemplateValues(Template template)
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
            if (formfield == "Purpose of Counseling")
                PurposeOfCounseling = value;
            else if (formfield == "Key Points of Discussion")
                KeyPointsOfDiscussion = value;
            else if (formfield == "Plan of Actions")
                PlanOfActions = value;
            else if (formfield == "Session Closing")
                SessionClosing = value;
            else if (formfield == "Leader Responsibilities")
                LeaderResponsibilities = value;
        }



        private void LoadDocumentValues(DataTable documentValues, string column)
        {
            Name = GetDocumentValue(documentValues, "Name (Last, First, MI)", column);
            NameOfOrganization = GetDocumentValue(documentValues, "Organization", column);
            NameAndTitleOfCounselor = GetDocumentValue(documentValues, "Name and Title of Counselor", column);
            PurposeOfCounseling = GetDocumentValue(documentValues, "Purpose of Counseling", column);
            KeyPointsOfDiscussion = GetDocumentValue(documentValues, "Key Points of Discussion", column);
            PlanOfActions = GetDocumentValue(documentValues, "Plan of Actions", column);

            SessionClosing = GetDocumentValue(documentValues, "Session Closing", column);
            LeaderResponsibilities = GetDocumentValue(documentValues, "Leader Responsibilities", column);
            Assessment = GetDocumentValue(documentValues, "Assessment", column);
            Counselor = GetDocumentValue(documentValues, "Counselor", column);
            IndividualCounseled = GetDocumentValue(documentValues, "Individual Counseled", column);

            string rankingIntString = GetDocumentValue(documentValues, "Rank/Grade", column);

            if (rankingIntString != " " && rankingIntString != "")
            {
                int rankingInt = Convert.ToInt32(rankingIntString);
                Rank = (Ranking)rankingInt;
            }
            else
            {
                Rank = Ranking.CPL;
            }

            string dateOfCounselingTicksString = GetDocumentValue(documentValues, "Date of Counseling", column);

            if (dateOfCounselingTicksString != " " && rankingIntString != "")
            {
                long dateOfCounselingTicks = Convert.ToInt64(dateOfCounselingTicksString);
                Date = new DateTime(dateOfCounselingTicks);
            }
            else
            {
                Date = DateTime.Now;
            }

            string iAgreeText = GetDocumentValue(documentValues, "I agree", column);
            if (iAgreeText != " " && iAgreeText != "")
            {
                IAgree = Convert.ToBoolean(Convert.ToInt32(iAgreeText));
            }
            else
            {
                IAgree = false;
            }

            string iDisagreeText = GetDocumentValue(documentValues, "I disagree", column);
            if (iDisagreeText != " " && iDisagreeText != "")
            {
                IDisagree = Convert.ToBoolean(Convert.ToInt32(iDisagreeText));
            }
            else
            {
                IDisagree = false;
            }

            string dateOfAssessmentTicksString = GetDocumentValue(documentValues, "Date Of Assessment", column);
            if (dateOfAssessmentTicksString != " " && dateOfAssessmentTicksString != "")
            {
                long dateOfAssessmentTicks = Convert.ToInt64(dateOfAssessmentTicksString);
                DateOfAssessment = new DateTime(dateOfAssessmentTicks);
            }
            else
            {
                DateOfAssessment = new DateTime();
            }

            string dateAssessmentPerformedString = GetDocumentValue(documentValues, "Date Assessment Performed", column);
            if (dateAssessmentPerformedString != " " && dateAssessmentPerformedString != "")
            {
                long dateAssessmentPerformedTicks = Convert.ToInt64(dateAssessmentPerformedString);
                if (dateAssessmentPerformedTicks > 0)
                    DateAssessmentPerformed = new DateTime(dateAssessmentPerformedTicks);
            }
            else
            {
                DateAssessmentPerformed = new DateTime();
            }

            Continuation = GetDocumentValue(documentValues, "Continuation Of Counseling", column);
        }


        internal static void GenerateFormFieldsForXFDL(XFDLDocument document)
        {
            DataTable formFields = DatabaseConnection.Query("select * from formfields "
                                                            + " where formid = 1");

            foreach (DataRow row in formFields.Rows)
            {
                DatabaseConnection.Insert("insert into usergeneratedvalues "
                    + " (generateddocid, formid, formfieldid, generatedvaluetext) "
                    + " values ("
                    + document.GeneratedDocID + ", "
                    + 1 + ", "
                    + row["formfieldid"].ToString() + ", "
                    + " \"\" )");
            }
        }


        internal void SaveContinuation()
        {
            XFDLDocumentModel.SaveContinuation(this);
        }



        internal void SaveAssessment()
        {
            XFDLDocumentModel.SaveAssessment(this);
        }


        internal override void Export(string filename)
        {
            string templateFilename = DocumentModel.GetFormFilename(formID);

            try
            {
                XDFLXMLForm xfdl = new XDFLXMLForm(templateFilename, filename);
                xfdl.LoadForm(filename);

                if (xfdl.IsOpen() == false)
                    throw new FileException("Could not open " + templateFilename + " for writing!");

                xfdl.Fill(this);
                xfdl.SaveForm(filename);
            }
            catch (FileException)
            {
                throw new FileException("Could not open " + templateFilename + " for writing.");
            }
            catch (DataLoadFailedException ex)
            {
                throw ex;
            }
        }
    }
}
