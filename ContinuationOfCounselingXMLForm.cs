using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal class ContinuationOfCounselingXMLForm : XFDLForm
    {
        internal string AdditionalNotation { get; set; }
        internal string NameGradeCounselee { get; set; }
        internal string NameGradeCounselor { get; set; }
        internal DateTime Date1 { get; set; }
        internal DateTime Date2 { get; set; }

        //DA4856Document associatedCounseling;

        private int FormID { get; set; }


        internal ContinuationOfCounselingXMLForm(string templateName, string newFormFilename) 
            : base(templateName, newFormFilename)
        {
            FormID = (int)DocumentFormIDs.ContinuationOfCounselingPDF;

            AdditionalNotation = "";
            NameGradeCounselee = "";
            NameGradeCounselor = "";
            Date1 = new DateTime();
            Date2 = new DateTime();
        }


        /*internal void Fill(DA4856Document counseling) //: base(documentID)
        {
            const int formid = (int)DocumentFormIDs.ContinuationOfCounselingPDF;

            //DataTable formFields = XFDLDocumentModel.GetUserGeneratedValuesForDocument(counseling);
            DataTable formFields = DocumentModel.GetFormFieldsForFormID(formid);
            DataRow[] rows = formFields.Select("formid = " + formid);
            string fieldName;

            string nameAndGradeOfCounselor = counseling.NameGradeCounselor;
            string nameAndGradeOfCounselee = counseling.NameGradeCounselee;
            DateTime date1 = counseling.ContinuationDate1;
            DateTime date2 = counseling.ContinuationDate2;

            fieldName = DocumentModel.GetFieldName(rows, "AdditionalNotation");
            SetValue(fieldName, counseling.ContinuationText);

            fieldName = DocumentModel.GetFieldName(rows, "NameGradeCounselee");
            SetValue(fieldName, nameAndGradeOfCounselee);

            fieldName = DocumentModel.GetFieldName(rows, "NameGradeCounselor");
            SetValue(fieldName, nameAndGradeOfCounselor);

            fieldName = DocumentModel.GetFieldName(rows, "Date1");
            if (date1 != null && date1 != new DateTime(0))
                SetValue(fieldName, date1.ToString("yyyy MM dd"));

            fieldName = DocumentModel.GetFieldName(rows, "Date2");
            if (date2 != null && date2 != new DateTime(0))
                SetValue(fieldName, date2.ToString("yyyy MM dd"));
        }*/


        //internal new void Export(string filename)
        //{
        //    PrepForExport(filename);

        //    DataTable formFields = DatabaseConnection.Query("select * from formfields where formid=" + FormID);

        //    UpdateBookmarks(formFields);

        //    wordInterop.Close();
        //}


        //void UpdateBookmarks(DataTable formFields)
        //{
        //    try
        //    {
        //        string bookmarkLabel = GetBookmarkLabel(formFields, "AdditionalNotation");
        //        wordInterop.UpdateBookmark(bookmarkLabel, AdditionalNotation);

        //        bookmarkLabel = GetBookmarkLabel(formFields, "NameGradeCounselee");
        //        wordInterop.UpdateBookmark(bookmarkLabel, NameGradeCounselee);

        //        bookmarkLabel = GetBookmarkLabel(formFields, "NameGradeCounselor");
        //        wordInterop.UpdateBookmark(bookmarkLabel, NameGradeCounselor);

        //        bookmarkLabel = GetBookmarkLabel(formFields, "Date1");
        //        wordInterop.UpdateBookmark(bookmarkLabel, Date1.ToString("yyyy MM dd"));

        //        bookmarkLabel = GetBookmarkLabel(formFields, "Date2");
        //        wordInterop.UpdateBookmark(bookmarkLabel, Date2.ToString("yyyy MM dd"));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
