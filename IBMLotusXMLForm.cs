using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace CounselQuickPlatinum
{
    public class IBMLotusXMLForm : FormInterface
    {
        protected string filename;
        protected System.Xml.XmlDocument document;


        public IBMLotusXMLForm(string templateName, string newFormFilename)
        {
            FileUtils.CreateNewCopy(templateName, newFormFilename);
        }


        public void LoadForm(string filename)
        {
            bool fileExists = FileUtils.VerifyFileExists(filename);
            if (!fileExists)
                throw new FileException(filename);

            try
            {
                InitializeNewXFDL(filename);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            this.filename = filename;
        }


        internal void InitializeNewXFDL(string newFormFilename)
        {
            document = new System.Xml.XmlDocument();
            document.Load(newFormFilename);

            this.filename = newFormFilename;
        }


        internal bool IsOpen()
        {
            return (document != null);
        }


        public string GetValue(string propertyName)
        {
            string value = document.SelectSingleNode("XFDL/globalpage/global/xformsmodels/xforms:model/data/"
                + propertyName).Value;

            return value;
        }


        public void SaveForm(string filename)
        {
            bool saveFileDirectoryExists = FileUtils.VerifyDirectoryExists(filename);

            if (!saveFileDirectoryExists)
                throw new FileException(filename);

            if (document == null)
                throw new Exception("No document opent to save!");

            XmlTextWriter writer = new XmlTextWriter(filename, new UTF8Encoding(false));
            document.Save(writer);
            writer.Close();
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


        public void SetValue(string propertyName, string value)
        {
            string[] values = propertyName.Split('/');

            try
            {
                //   settings   XFDL      globalpage   
                System.Xml.XmlNode node = document.FirstChild.NextSibling.FirstChild;
                // global      formid   xformsmodels  xforms:   xforms:    Data
                System.Xml.XmlNode node2 = node.FirstChild.FirstChild.NextSibling.FirstChild.FirstChild.FirstChild;

                System.Xml.XmlNode node3 = node2.SelectSingleNode(values[0]);
                System.Xml.XmlNode node4 = node3.SelectSingleNode(values[1]);

                node4.InnerText = value;

            }
            catch (Exception)
            {
                throw new Exception("Save Failed - The file is malformed or not a proper XFDL.");
            }
        }
    }
}
