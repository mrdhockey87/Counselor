using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    interface FormInterface
    {
        //void CreateNewFormFromTemplate(string templateFilename, string newFormFilename);
        string GetValue(string propertyName);
        //bool IsOpen(string filename);
        void LoadForm(string filename);
        void SaveForm(string filename);
        void SetValue(string propertyName, string value);
    }
}
