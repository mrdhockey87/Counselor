using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal class WordDocument : FormInterface
    {
        internal bool HasUnsavedDocumentChanges { get; set; }
        protected WordInterop wordInterop;

        internal WordDocument(string templateName, string newFormFilename)
        {
            FileUtils.CreateNewCopy(templateName, newFormFilename);
        }


        //internal WordDocument(int generatedDocumentID) : base(generatedDocumentID)
        //{
        //    LoadGeneratedDocumentValues(generatedDocumentID);
        //}


        //internal new void LoadGeneratedDocumentValues(int generatedDocumentID)
        //{
        //    //this.GeneratedDocID = generatedDocumentID;
        //    HasUnsavedDocumentChanges = false;
        //}


        //internal new void Export(string filename)
        //{

        //}


        public void LoadForm(string filename)
        {
            PrepForExport(filename);
        }

        public void SaveForm(string filename)
        {
            Close();
        }


        internal void PrepForExport(string filename)
        {
            //string templateName = DocumentModel.GetFormFilename((int)DocumentFormIDs.GenericMemo);

            try
            {
                //File.Copy(templateName, filename, true);
                wordInterop = new WordInterop();
                wordInterop.OpenDoc(filename);
            }
            catch (WordInteropException ex)
            {
                throw ex;
            }
        }


        internal string GetFieldName(DataRow[] rows, string label)
        {
            try
            {
                string fieldName;
                //DataTable results = rows.Where(row => row["fieldlabel"] == label).CopyToDataTable();

                foreach (DataRow row in rows)
                {
                    if (row["fieldlabel"].ToString() == label)
                    {
                        fieldName = row["fieldname"].ToString();
                        return fieldName;
                    }
                }

                throw new DataLoadFailedException("No rows matching " + label);

            }
            catch (Exception ex)
            {
                throw new DataLoadFailedException("Could not retrieve document value for " + label, ex);
            }
        }



        //internal string GetBookmarkLabel(DataTable formFields, string fieldLabel)
        //{
        //    int formFieldID = GetFormFieldID(formFields, fieldLabel);
        //    DataRow row = formFields.Select("formfieldid = " + formFieldID).ToArray()[0];
        //    string bookmarkLabel = row["fieldname"].ToString();

        //    return bookmarkLabel;
        //}

        public void SetValue(string bookmarkname, string value)
        {
            wordInterop.UpdateBookmark(bookmarkname, value);
        }


        public string GetValue(string propertyName)
        {
            return wordInterop.GetBookmarkValue(propertyName);
        }


        internal void Close()
        {
            wordInterop.Close();
        }
    }
}
