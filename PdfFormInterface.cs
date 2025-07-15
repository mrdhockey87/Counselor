using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CounselQuickPlatinum
{
    class PdfFormInterface : FormInterface
    {
        protected iTextSharp.text.pdf.PdfReader reader;
        protected iTextSharp.text.pdf.PdfStamper pdfStamper;
        protected Dictionary<string, string> values;
        private string filename;
        private string templateFilename;

        internal PdfFormInterface(string filename)
        {
            this.filename = filename;
            LoadForm(filename);

        }

        internal PdfFormInterface(string templateName, string newFormFilename)
        {
            //FileUtils.CreateNewCopy(templateName, newFormFilename);
            this.filename = newFormFilename;
            this.templateFilename = templateName;
            LoadForm(templateName);
        }


        public void LoadForm(string filename)
        {
            try
            {
                reader = new iTextSharp.text.pdf.PdfReader(filename);
            }
            catch (Exception ex)
            {
                Logger.Trace("PdfFormInterface.LoadForm failed, " + filename + " ex " + ex.Message);
                throw ex;
            }
        }



        public string GetValue(string field)
        {
            if(!values.Keys.Contains(field))
                throw new Exception("PdfFormInterface.SetValue - Error getting field \"" + field );

            return values[field];
        }


        public new void SetValue(string field, string value)
        {
            if(!values.Keys.Contains(field))
                throw new Exception("PdfFormInterface.SetValue - Error writing field \"" + field + "\" with value \"" + value);

            values[field] = value;
        }


        public void SaveForm(string exportFilename)
        {
            FileUtils.CreateNewCopy(templateFilename, exportFilename);

            pdfStamper = new iTextSharp.text.pdf.PdfStamper(reader, new System.IO.FileStream(exportFilename, System.IO.FileMode.Create));
            iTextSharp.text.pdf.AcroFields fields = pdfStamper.AcroFields;

            foreach (string key in values.Keys)
            {
                fields.SetField(key, values[key]);
            }

            pdfStamper.Close();
        }


        internal void Close()
        {
            reader.Close();
        }


    }
}
