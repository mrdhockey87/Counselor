using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal class DA4856DocumentModel : DocumentModel
    {
        public DA4856DocumentModel()
        {

        }


        //public static DataTable GetXFDLValuesForForm(int generateddocid)
        //{
        //    string query = "select * from usergenerateddocs "
        //        + " inner join "
        //        + " formfields "
        //        + " on usergenerateddocs.formid = formfields.formid "
        //        + " inner join "
        //        + " usergeneratedvalues "
        //        + " on usergeneratedvalues.formfieldid = "
        //        + "    formfields.formfieldid "
        //        + " inner join "
        //        + " documentnames "
        //        + " on documentnames.documentnameid = usergenerateddocs.documentnameid "
        //        + " where usergenerateddocs.generateddocid=" + generateddocid
        //        + " and usergeneratedvalues.generateddocid = usergenerateddocs.generateddocid";

        //    try
        //    {
        //        DataTable usergeneratedvalues = DatabaseConnection.Query(query);
        //        return usergeneratedvalues;
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve values for selected "
        //            + "document.", ex);
        //    }
        //}


        internal static void SaveDA4856DocumentValues(DataTable userGeneratedValues, DA4856Document document, byte[] IVbytes)
        {
            int pkid;

            Lock();

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Name (Last, First, MI)");
            SaveDocumentValue(pkid, document.Name, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Rank/Grade");
            SaveDocumentValue(pkid, ((int)document.Rank).ToString(), IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date of Counseling");
            SaveDocumentValue(pkid, document.Date.Ticks.ToString(), IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Organization");
            SaveDocumentValue(pkid, document.NameOfOrganization, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Name and Title of Counselor");
            SaveDocumentValue(pkid, document.NameAndTitleOfCounselor, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Purpose of Counseling");
            SaveDocumentValue(pkid, document.PurposeOfCounseling, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Key Points of Discussion");
            SaveDocumentValue(pkid, document.KeyPointsOfDiscussion, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Plan of Actions");
            SaveDocumentValue(pkid, document.PlanOfActions, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "I agree");
            SaveDocumentValue(pkid, Convert.ToInt32(document.IAgree).ToString(), IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "I disagree");
            SaveDocumentValue(pkid, Convert.ToInt32(document.IDisagree).ToString(), IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Session Closing");
            SaveDocumentValue(pkid, document.SessionClosing, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Leader Responsibilities");
            SaveDocumentValue(pkid, document.LeaderResponsibilities, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Assessment");
            SaveDocumentValue(pkid, document.Assessment, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Counselor");
            SaveDocumentValue(pkid, document.Counselor, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Individual Counseled");
            SaveDocumentValue(pkid, document.IndividualCounseled, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date of Assessment");
            SaveDocumentValue(pkid, document.DateAssessmentDue.Date.Ticks.ToString(), IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date Assessment Performed");
            SaveDocumentValue(pkid, document.DateAssessmentPerformed.Date.Ticks.ToString(), IVbytes);

            Unlock();

            //pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Continuation of Counseling");
            //SaveDocumentValue(pkid, document.ContinuationText);

            //pkid = GetPKIDForDocumentEntry(userGeneratedValues, "NameGradeCounselee");
            //SaveDocumentValue(pkid, document.NameGradeCounselee);

            //pkid = GetPKIDForDocumentEntry(userGeneratedValues, "NameGradeCounselor");
            //SaveDocumentValue(pkid, document.NameGradeCounselor);

            //pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date1");
            //SaveDocumentValue(pkid, document.ContinuationDate1.Ticks.ToString());

            //pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date2");
            //SaveDocumentValue(pkid, document.ContinuationDate2.Ticks.ToString());

            DataTable xfdlFormFields = GetPKIDsForDocument(document);

            SaveDA4856ContinuationValues(xfdlFormFields, document, IVbytes);
            SaveDA4856AssessmentValues(xfdlFormFields, document, IVbytes);
        }



        protected static void SaveDA4856AssessmentValues(DataTable xfdlFormFields, DA4856Document document, byte[] IVbytes)
        {
            int pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Assessment");
            SaveDocumentValue(pkid, document.Assessment, IVbytes);

            pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Date Assessment Performed");
            SaveDocumentValue(pkid, document.DateAssessmentPerformed.Date.Ticks.ToString(), IVbytes);
        }


        internal static void SaveDA4856Assessment(DA4856Document document)
        {
            // @CHANGEHERE
            //DataTable xfdlFormFields = new DataTable();
            DataTable xfdlFormFields = new DataTable();

            //try
            //{
                //DatabaseConnection.BeginTransaction();
                DatabaseConnection.BatchUpdateLock();

                if (document.GeneratedDocID == -1)
                    document.Save();

                Lock();

                //xfdlFormFields = GetUserGeneratedValuesForDocument(document);
                xfdlFormFields = GetPKIDsForDocument(document);

                string IVencoded = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", document.GeneratedDocID, "documentIV");
                byte[] IVbytes = Convert.FromBase64String(IVencoded);
                SaveDA4856AssessmentValues(xfdlFormFields, document, IVbytes);

                //int pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Assessment");
                //SaveDocumentValue(pkid, document.Assessment);

                //pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Date Assessment Performed");
                //SaveDocumentValue(pkid, document.DateAssessmentPerformed.Date.Ticks.ToString());

                Unlock();
                DatabaseConnection.BatchUpdateUnlock();

                //DatabaseConnection.EndTransaction();
            //}
            /*catch (DataLoadFailedException ex)
            {
                throw ex;
            }*/
        }


        protected static void SaveDA4856ContinuationValues(DataTable xfdlFormFields, DA4856Document document, byte[] IVbytes)
        {
            //xfdlFormFields = GetUserGeneratedValuesForDocument(document);
            Lock();
            xfdlFormFields = GetPKIDsForDocument(document);

            int pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Continuation of Counseling");
            SaveDocumentValue(pkid, document.ContinuationText, IVbytes);

            pkid = GetPKIDForDocumentEntry(xfdlFormFields, "NameGradeCounselee");
            SaveDocumentValue(pkid, document.NameGradeCounselee, IVbytes);

            pkid = GetPKIDForDocumentEntry(xfdlFormFields, "NameGradeCounselor");
            SaveDocumentValue(pkid, document.NameGradeCounselor, IVbytes);

            pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Date1");
            SaveDocumentValue(pkid, document.ContinuationDate1.Date.Ticks.ToString(), IVbytes);

            pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Date2");
            SaveDocumentValue(pkid, document.ContinuationDate2.Date.Ticks.ToString(), IVbytes);
            Unlock();
        }


        internal static void SaveDA4856Continuation(DA4856Document document)
        {
            // @CHANGEHERE
            //DataTable xfdlFormFields = new DataTable();
            DataTable documentFormFields = new DataTable();

            try
            {
                if (document.GeneratedDocID == -1)
                    document.Save();

                //Lock();

                string IVencoded = DatabaseConnection.GetSingleValue("usergenerateddocs", "generateddocid", document.GeneratedDocID, "documentIV");
                byte[] IVbytes = Convert.FromBase64String(IVencoded);

                //xfdlFormFields = GetUserGeneratedValuesForDocument(document);
                documentFormFields = DocumentModel.GetPKIDsForDocument(document);
                SaveDA4856ContinuationValues(documentFormFields, document, IVbytes);

                ////xfdlFormFields = GetUserGeneratedValuesForDocument(document);
                //xfdlFormFields = GetPKIDsForDocument(document);

                //int pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Continuation of Counseling");
                //SaveDocumentValue(pkid, document.ContinuationText);

                //pkid = GetPKIDForDocumentEntry(xfdlFormFields, "NameGradeCounselee");
                //SaveDocumentValue(pkid, document.NameGradeCounselee);

                //pkid = GetPKIDForDocumentEntry(xfdlFormFields, "NameGradeCounselor");
                //SaveDocumentValue(pkid, document.NameGradeCounselor);

                //pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Date1");
                //SaveDocumentValue(pkid, document.ContinuationDate1.Date.Ticks.ToString());

                //pkid = GetPKIDForDocumentEntry(xfdlFormFields, "Date2");
                //SaveDocumentValue(pkid, document.ContinuationDate2.Date.Ticks.ToString());

                //Unlock();
            }
            catch (DataStoreFailedException ex)
            {
                Unlock();
                throw ex;
            }
        }
    }
}
