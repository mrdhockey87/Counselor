using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CounselQuickPlatinum
{
    class ContinuationOfCounselingFormFiller
    {

        internal static void Fill(DA4856Document counseling, FormInterface form)
        {
            //const int formid = (int)DocumentFormIDs.DA4856;
            int formid = -1;

            /*if (form is CounselQuickPlatinum.ContinuationOfCounselingXMLForm)
            {
                formid = (int)DocumentFormIDs.ContinuationOfCounselingPDF;
            }
            else*/ if (form is CounselQuickPlatinum.ContinuationOfCounselingPDFForm)
            {
                formid = (int)DocumentFormIDs.ContinuationOfCounselingPDF;
            }

            DataTable formFields = DocumentModel.GetFormFieldsForFormID(formid);
            DataRow[] rows = formFields.Select("formid = " + formid);

            string fieldName;

            string nameAndGradeOfCounselor = counseling.NameGradeCounselor;
            string nameAndGradeOfCounselee = counseling.NameGradeCounselee;
            DateTime date1 = counseling.ContinuationDate1;
            DateTime date2 = counseling.ContinuationDate2;

            fieldName = DocumentModel.GetFieldName(rows, "AdditionalNotation");
            form.SetValue(fieldName, counseling.ContinuationText);

            fieldName = DocumentModel.GetFieldName(rows, "NameGradeCounselee");
            form.SetValue(fieldName, nameAndGradeOfCounselee);

            fieldName = DocumentModel.GetFieldName(rows, "NameGradeCounselor");
            form.SetValue(fieldName, nameAndGradeOfCounselor);

            fieldName = DocumentModel.GetFieldName(rows, "Date1");
            if (date1 != null && date1 != new DateTime(0))
                form.SetValue(fieldName, date1.ToString("yyyy MM dd"));

            fieldName = DocumentModel.GetFieldName(rows, "Date2");
            if (date2 != null && date2 != new DateTime(0))
                form.SetValue(fieldName, date2.ToString("yyyy MM dd"));
        }



    }
}
