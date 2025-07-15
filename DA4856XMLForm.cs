using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace CounselQuickPlatinum
{
    class DA4856XMLForm : XFDLForm
    {


        internal DA4856XMLForm(string templateName, string newFormFilename) : base(templateName, newFormFilename)
        {
            
        }

        /*
        internal void Fill(DA4856Document da4856Document)
        {
            const int formid = (int)DocumentFormIDs.DA4856;

            DataTable formFields = DocumentModel.GetFormFieldsForFormID(formid);
            DataRow[] rows = formFields.Select("formid = " + formid);

            string fieldName;

            fieldName = DocumentModel.GetFieldName(rows, "Name (Last, First, MI)");
            SetValue(fieldName, da4856Document.Name);

            fieldName = DocumentModel.GetFieldName(rows, "Rank/Grade");
            SetValue(fieldName, RankingModel.RankToString(da4856Document.Rank));

            fieldName = DocumentModel.GetFieldName(rows, "Date of Counseling");
            SetValue(fieldName, da4856Document.Date.ToString("yyyy MM dd"));

            fieldName = DocumentModel.GetFieldName(rows, "Organization");
            SetValue(fieldName, da4856Document.NameOfOrganization);

            fieldName = DocumentModel.GetFieldName(rows, "Name and Title of Counselor");
            SetValue(fieldName, da4856Document.NameAndTitleOfCounselor);

            fieldName = DocumentModel.GetFieldName(rows, "Purpose of Counseling");
            SetValue(fieldName, da4856Document.PurposeOfCounseling);

            fieldName = DocumentModel.GetFieldName(rows, "Key Points of Discussion");
            SetValue(fieldName, da4856Document.KeyPointsOfDiscussion);

            fieldName = DocumentModel.GetFieldName(rows, "Plan of Actions");
            SetValue(fieldName, da4856Document.PlanOfActions);

            fieldName = DocumentModel.GetFieldName(rows, "I agree");
            if (da4856Document.IAgree == true)
                SetValue(fieldName, "on");
            else
                SetValue(fieldName, "off");

            fieldName = DocumentModel.GetFieldName(rows, "I disagree");
            if (da4856Document.IDisagree == true)
                SetValue(fieldName, "on");
            else
                SetValue(fieldName, "off");

            fieldName = DocumentModel.GetFieldName(rows, "Session Closing");
            SetValue(fieldName, da4856Document.SessionClosing);

            fieldName = DocumentModel.GetFieldName(rows, "Leader Responsibilities");
            SetValue(fieldName, da4856Document.LeaderResponsibilities);

            if (da4856Document.Assessment != "" && da4856Document.DateAssessmentPerformed.Ticks != 0)
            {
                fieldName = DocumentModel.GetFieldName(rows, "Assessment");
                SetValue(fieldName, da4856Document.Assessment);

                fieldName = DocumentModel.GetFieldName(rows, "Date of Assessment");
                SetValue(fieldName, da4856Document.DateAssessmentPerformed.ToString("yyyy MM dd"));
            }
            else
            {
                fieldName = GetFieldName(rows, "Date of Assessment");
                SetValue(fieldName, da4856Document.DateAssessmentDue.ToString("yyyy MM dd"));
            }

            fieldName = GetFieldName(rows, "Counselor");
            SetValue(fieldName, da4856Document.Counselor);

            fieldName = GetFieldName(rows, "Individual Counseled");
            SetValue(fieldName, da4856Document.IndividualCounseled);
        }*/


    }
}
