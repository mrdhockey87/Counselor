using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    static class Model
    {
        internal static void SaveColumn(string tablename, string keyColumn, int pkid, string updateColumn, string updateValue)
        {
            try
            {
                DatabaseConnection.Update(tablename, updateColumn, updateValue, keyColumn, pkid.ToString());
            }
            catch (NonQueryFailedException ex)
            {
                throw ex;
            }
        }

        internal static void InsertTuple(string tablename, string firstColumn, string firstValue, string secondColumn, string secondValue)
        {
            try
            {
                string insertCommand = "insert into " + tablename + " (" + firstColumn + ", " + secondColumn + ") "
                    + " values (@firstValue, @secondValue)";
                Params paramValues = new Params();
                
                paramValues.Add("@firstValue", firstValue);
                paramValues.Add("@secondValue", secondValue);

                DatabaseConnection.Insert(insertCommand, paramValues);
            }
            catch (NonQueryFailedException)
            {
                throw new DataStoreFailedException("An error occured attempting to save " + firstValue + ", " + secondValue + " to the database ");
            }
        }        

        internal static DataTable GetReferenceDocumentsTable()
        {
            DataTable results;

            try
            {
                results = DatabaseConnection.GetTable("referencedocuments");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return results;
        }

        internal static DataTable GetReferenceGroupsTable()
        {
            DataTable results;

            try
            {
                results = DatabaseConnection.GetTable("referencegroups");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return results;
        }

        internal static DataTable GetHelpDocumentsTable()
        {
            DataTable results;

            try
            {
                results = DatabaseConnection.GetTable("helpdocuments");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return results;
        }

        internal static DataTable GetHelpDocumentGroupsTable()
        {
            DataTable results;

            try
            {
                results = DatabaseConnection.GetTable("helpdocumentgroups");
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return results;
        }
    }
}
