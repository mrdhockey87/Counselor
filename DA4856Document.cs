using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class DA4856Document : Document
    {
        const int xfdlFormIDF = (int)DocumentFormIDs.DA4856PDF;
        
        internal string Assessment { get; set; }
        internal string Counselor { get; set; }
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
        internal string IndividualCounseled { get; set; }
        internal string SessionClosing { get; set; }
        internal string LeaderResponsibilities { get; set; }
        
        internal DateTime DateAssessmentDue { get; set; }
        internal DateTime DateAssessmentPerformed { get; set; }

        public bool HasUnsavedContinuationChanges { get; set; }
        internal bool HasUnsavedAssessmentChanges { get; set; }
        internal bool HasUnsavedDocumentChanges { get; set; }

        public string ContinuationText { get; set; }
        public string NameGradeCounselee { get; set; }
        public string NameGradeCounselor { get; set; }
        public DateTime ContinuationDate1 { get; set; }
        public DateTime ContinuationDate2 { get; set; }

        internal DA4856Document() : base()
        {
            this.Assessment = "";
            this.Counselor = "";
            this.ContinuationText = "";
            this.ContinuationDate1 = new DateTime(0);
            this.ContinuationDate2 = new DateTime(0);
            this.DateAssessmentPerformed = new DateTime(0);
            // this.DateAssessmentDue = new DateTime(0);
            int hoursInThirtyDays = 24 * 30;
            this.DateAssessmentDue = DateTime.Now + new TimeSpan(hoursInThirtyDays, 0, 0);
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
            this.NameGradeCounselee = "";
            this.NameGradeCounselor = "";
            this.NameOfOrganization = "";
            this.PlanOfActions = "";
            this.PurposeOfCounseling = "";
            this.Rank = Ranking.PVT;
            this.SessionClosing = "";
        }


        internal DA4856Document(int documentID) : base(documentID)
        {
            this.GeneratedDocID = documentID;
            HasUnsavedDocumentChanges = false;

            LoadGeneratedDocumentValues(documentID);
        }


        private new void LoadGeneratedDocumentValues(int generatedDocumentID)
        {
            if (generatedDocumentID == -1)
                return;

            try
            {
                //DataTable documentValues = DocumentModel.GetUserGeneratedDocumentValuesForForm(generatedDocumentID);
                DataTable documentValues = DocumentModel.GetUserValuesForDocument(generatedDocumentID);

                if (documentValues.Columns.Contains("generatedvaluetext") != true
                    || documentValues.Rows.Count == 0)
                {
                    throw new DataLoadFailedException("No values found for this document.\n");
                }

                LoadDocumentValues(documentValues, "generatedvaluetext");

            }
            catch (DataLoadFailedException ex)
            {
                Logger.Error("Could not retrieve generated doc values", ex);

                throw ex;
            }
            catch (QueryFailedException ex)
            {
                Logger.Error("Could not retrieve generated doc values", ex);

                throw new DataLoadFailedException("Error retrieving values for selected document", ex);
            }
            catch (FormatException ex)
            {
                Logger.Error("Error parsing doc values", ex);

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


        internal static DA4856Document GenerateNewFromTemplate(Template template)
        {
            DA4856Document document = new DA4856Document();

            document.ContinuationText = "";
            document.Date = DateTime.Now;
            document.DocumentName = template.TemplateName;
            document.DocumentType = template.DocumentType;
            //document.FileType = FileUtils.FileType.XFDL;
            document.Filepath = "";
            document.FilepathChanged = false;
            //document.FormID = xfdlFormIDF;
            document.FormID = Convert.ToInt32(template.FormID);
            document.GeneratedDocID = -1;
            document.HasUnsavedAssessmentChanges = false;
            document.HasUnsavedDocumentChanges = true;
            document.HasUnsavedContinuationChanges = false;
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
                DateAssessmentDue = new DateTime(dateOfAssessmentTicks);
            }
            else
            {
                DateAssessmentDue = new DateTime();
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

            ContinuationText = GetDocumentValue(documentValues, "Continuation Of Counseling", column);
            NameGradeCounselor = GetDocumentValue(documentValues, "NameGradeCounselor", column);
            NameGradeCounselee = GetDocumentValue(documentValues, "NameGradeCounselee", column);

            string continuationDate1String = GetDocumentValue(documentValues, "Date1", column);
            if (continuationDate1String != " " && continuationDate1String != ""
                && continuationDate1String != "0")
            {
                long date1Ticks = Convert.ToInt64(continuationDate1String);
                ContinuationDate1 = new DateTime(date1Ticks);
            }

            string continuationDate2String = GetDocumentValue(documentValues, "Date2", column);
            if (continuationDate2String != " " && continuationDate1String != ""
                && continuationDate2String != "0")
            {
                long date2Ticks = Convert.ToInt64(continuationDate2String);
                ContinuationDate2 = new DateTime(date2Ticks);
            }


        }


        //internal static void GenerateFormFieldsForXFDL(DA4856Document document)
        //{
        //    DataTable formFields = DatabaseConnection.Query("select * from formfields "
        //                                                    + " where formid = 1");

        //    foreach (DataRow row in formFields.Rows)
        //    {
        //        DatabaseConnection.Insert("insert into usergeneratedvalues "
        //            + " (generateddocid, formid, formfieldid, generatedvaluetext) "
        //            + " values ("
        //            + document.GeneratedDocID + ", "
        //            + 1 + ", "
        //            + row["formfieldid"].ToString() + ", "
        //            + " \"\" )");
        //    }
        //}

        
        internal void SaveContinuation()
        {
            DA4856DocumentModel.SaveDA4856Continuation(this);
        }

        
        internal void SaveAssessment()
        {
            DA4856DocumentModel.SaveDA4856Assessment(this);
        }
        

        //internal override void Export(string filename, DocumentFormIDs id)
        internal override void Export(string filename, DocumentFormIDs id)
        {
            Logger.Trace("Export: " + filename);

            string templateFilename = DocumentModel.GetFormFilename(Convert.ToInt32(id));

            Logger.Trace("Export: " + templateFilename);

            FormInterface form;
            /*if (id == DocumentFormIDs.DA4856PDF)
            {
                form = new DA4856XMLForm(templateFilename, filename);
            }*/
            if (id == DocumentFormIDs.DA4856PDF)
            {
                form = new DA4856PDFForm(templateFilename, filename);
            }
            else
            {
                throw new Exception("Invalid form id for DA4856!");
            }


            //form.LoadForm(filename);

            //if (form.IsOpen() == false)
            //{
            //    Logger.Error("Could not open XFDL");

            //    throw new FileException("Could not open " + templateFilename + " for writing!");
            //}

            DA4856FormFiller.Fill(this, form);
            form.SaveForm(filename);

            form = null;

                /*
                DA4856XMLForm xfdl = new DA4856XMLForm(templateFilename, filename);
                xfdl.LoadForm(filename);

                if (xfdl.IsOpen() == false)
                {
                    Logger.Error("Could not open XFDL");

                    throw new FileException("Could not open " + templateFilename + " for writing!");
                }

                xfdl.Fill(this);
                xfdl.SaveForm(filename);

                xfdl = null;*/
                //}
        }
    }
}
