using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CounselQuickPlatinum
{
    public class DA4856FormFiller
    {
        internal static void Fill(DA4856Document da4856Document, FormInterface form)
        {
            //const int formid = (int)DocumentFormIDs.DA4856;
            /*int formid = -1;

            if (form is CounselQuickPlatinum.XFDLForm)
            {
                formid = (int)DocumentFormIDs.DA4856PDF;
            }
            else if (form is CounselQuickPlatinum.PdfFormInterface)
            {
                formid = (int)DocumentFormIDs.DA4856PDF;
            }*/

            if (form is DA4856PDFForm)
            {
                FillCounselingForm(da4856Document, form);
            }
            else if (form is ContinuationOfCounselingPDFForm)
            {
                FillContinuationOfCounseling(da4856Document, form);
            }

            
        }



        private static void FillCounselingForm(DA4856Document da4856Document, FormInterface form)
        {
            int formid = (int)DocumentFormIDs.DA4856PDF;

            string fieldName;
            DataTable formFields = DocumentModel.GetFormFieldsForFormID(formid);
            DataRow[] rows = formFields.Select("formid = " + formid);

            fieldName = DocumentModel.GetFieldName(rows, "Name (Last, First, MI)");
            form.SetValue(fieldName, da4856Document.Name);

            fieldName = DocumentModel.GetFieldName(rows, "Rank/Grade");
            form.SetValue(fieldName, RankingModel.RankToString(da4856Document.Rank));

            fieldName = DocumentModel.GetFieldName(rows, "Date of Counseling");
            form.SetValue(fieldName, da4856Document.Date.ToString("yyyy MM dd"));

            fieldName = DocumentModel.GetFieldName(rows, "Organization");
            form.SetValue(fieldName, da4856Document.NameOfOrganization);

            fieldName = DocumentModel.GetFieldName(rows, "Name and Title of Counselor");
            form.SetValue(fieldName, da4856Document.NameAndTitleOfCounselor);

            fieldName = DocumentModel.GetFieldName(rows, "Purpose of Counseling");
            form.SetValue(fieldName, da4856Document.PurposeOfCounseling);

            fieldName = DocumentModel.GetFieldName(rows, "Key Points of Discussion");
            form.SetValue(fieldName, da4856Document.KeyPointsOfDiscussion);

            fieldName = DocumentModel.GetFieldName(rows, "Plan of Actions");
            form.SetValue(fieldName, da4856Document.PlanOfActions);

            fieldName = DocumentModel.GetFieldName(rows, "I agree");
            if (da4856Document.IAgree == true)
                form.SetValue(fieldName, "On");
            else
                form.SetValue(fieldName, "Off");

            fieldName = DocumentModel.GetFieldName(rows, "I disagree");
            if (da4856Document.IDisagree == true)
                form.SetValue(fieldName, "On");
            else
                form.SetValue(fieldName, "Off");

            fieldName = DocumentModel.GetFieldName(rows, "Session Closing");
            form.SetValue(fieldName, da4856Document.SessionClosing);

            fieldName = DocumentModel.GetFieldName(rows, "Leader Responsibilities");
            form.SetValue(fieldName, da4856Document.LeaderResponsibilities);

            if (da4856Document.Assessment != "" && da4856Document.DateAssessmentPerformed.Ticks != 0)
            {
                fieldName = DocumentModel.GetFieldName(rows, "Assessment");
                form.SetValue(fieldName, da4856Document.Assessment);

                fieldName = DocumentModel.GetFieldName(rows, "Date of Assessment");
                form.SetValue(fieldName, da4856Document.DateAssessmentPerformed.ToString("yyyy MM dd"));
            }
            else
            {
                fieldName = DocumentModel.GetFieldName(rows, "Date of Assessment");
                form.SetValue(fieldName, da4856Document.DateAssessmentDue.ToString("yyyy MM dd"));
            }

            fieldName = DocumentModel.GetFieldName(rows, "Counselor");
            form.SetValue(fieldName, da4856Document.Counselor);

            fieldName = DocumentModel.GetFieldName(rows, "Individual Counseled");
            form.SetValue(fieldName, da4856Document.IndividualCounseled);
        }

        private static void FillContinuationOfCounseling(DA4856Document da4856Document, FormInterface form)
        {
            int formid = (int)DocumentFormIDs.ContinuationOfCounselingPDF;

            string fieldName;
            DataTable formFields = DocumentModel.GetFormFieldsForFormID(formid);
            DataRow[] rows = formFields.Select("formid = " + formid);

            string nameAndGradeOfCounselor = da4856Document.NameGradeCounselor;
            string nameAndGradeOfCounselee = da4856Document.NameGradeCounselee;
            DateTime date1 = da4856Document.ContinuationDate1;
            DateTime date2 = da4856Document.ContinuationDate2;

            fieldName = DocumentModel.GetFieldName(rows, "AdditionalNotation");
            form.SetValue(fieldName, da4856Document.ContinuationText);

            fieldName = DocumentModel.GetFieldName(rows, "NameGradeCounselee");
            form.SetValue(fieldName, nameAndGradeOfCounselee);

            fieldName = DocumentModel.GetFieldName(rows, "NameGradeCounselor");

            fieldName = DocumentModel.GetFieldName(rows, "Date1");
            if (date1 != null && date1 != new DateTime(0))
                form.SetValue(fieldName, date1.ToString("yyyy MM dd"));

            fieldName = DocumentModel.GetFieldName(rows, "Date2");
            if (date2 != null && date2 != new DateTime(0))
                form.SetValue(fieldName, date2.ToString("yyyy MM dd"));
        }
    }
}
