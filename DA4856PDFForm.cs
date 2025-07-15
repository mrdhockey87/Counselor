using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CounselQuickPlatinum
{
    class DA4856PDFForm : PdfFormInterface
    {
        public DA4856PDFForm(string templateFilename, string newFormFilename) : base(templateFilename, newFormFilename)
        {
            InitValueDictionary();
            //LoadForm(templateFilename);
        }


        private void InitValueDictionary()
        {
            values = new Dictionary<string, string>();
            values["Name"] = "";
            values["Rank/Grade"] = "";
            values["Date of Counseling"] = "";
            values["Organization"] = "";
            values["Name and Title of Counselor"] = "";
            values["Purpose of Counseling"] = "";
            values["Key Points of Discussion"] = "";
            values["Copyright"] = "";
            values["Plan of Action"] = "";
            values["agree"] = "";
            values["disagree"] = "";
            values["Session Closing"] = "";
            values["Indv Signature"] = "";
            values["Date Indv"] = "";
            values["Leader Responsibilities"] = "";
            values["Cnsl Signature"] = "";
            values["Date Cnsl"] = "";
            values["Assessment"] = "";
            values["Cnsl Assess Sig"] = "";
            values["Indv Assess Sig"] = "";
            values["Date Assessment"] = "";

            //foreach (string key in values.Keys)
            string key = "";
            for (int i = 0; i < values.Keys.Count; i++)
            {
                key = values.Keys.ElementAt(i);
                values[key] = reader.AcroFields.GetField(key);
            }
        }

    }
}
