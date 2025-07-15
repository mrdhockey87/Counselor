using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class ContinuationOfCounselingPDFForm : PdfFormInterface
    {
        public ContinuationOfCounselingPDFForm(string templateFilename, string exportFilename)
            : base(templateFilename, exportFilename)
        {
            InitValueDictionary();
        }

        private void InitValueDictionary()
        {
            values = new Dictionary<string, string>();
            values["ADDITIONAL NOTATION"] = "";
            values["NAME AND GRADE OF COUNSELEE"] = "";
            values["DATE"] = "";
            values["NAME AND GRADE OF COUNSELOR"] = "";
            values["DATE_2"] = "";
            values["SIGNATURE"] = "";
            values["SIGNATURE_2"] = "";
            values["Copyright"] = "";

            string key = "";
            for (int i = 0; i < values.Keys.Count; i++)
            {
                key = values.Keys.ElementAt(i);
                values[key] = reader.AcroFields.GetField(key);
            }
        }


    }
}
