using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal class Template
    {
        protected int templateID;
        protected string templateName;
        protected DocumentType documentType;
        public DocumentFormIDs FormID { get; set; }
        protected Dictionary<string, List<string>> templateValues;
        //protected DocumentCategory documentCategory;


        internal Template(int templateID)
        {
            this.templateID = templateID;
            templateValues = new Dictionary<string, List<string>>();

            LoadTemplate(templateID);
        }


        private void LoadTemplate(int templateID)
        {
            templateName = TemplatesModel.GetTemplateNameByTemplateID(templateID);
            documentType = TemplatesModel.GetTemplateTypeByTemplateID(templateID);
            FormID = TemplatesModel.GetTemplateFormIDByTemplateID(templateID);
            //documentCategory = TemplatesModel.GetTemplateCategoryByTemplateID(templateID);

            DataTable values = TemplatesModel.GetTemplateValuesByTemplateID(templateID);
            foreach (DataRow value in values.Rows)
            {
                string formFieldName = value["fieldlabel"].ToString();
                string templateValue = value["templatevaluetext"].ToString();

                if(!templateValues.ContainsKey(formFieldName))
                {
                    templateValues[formFieldName] = new List<string>();
                }

                templateValues[formFieldName].Add(templateValue);
            }
        }


        internal Dictionary<string, List<string>> TemplateValues
        {
            get
            {
                return templateValues;
            }
        }


        internal string TemplateName
        { 
            get
            {
                return templateName;
            }
        }


        internal DocumentType DocumentType
        {
            get
            {
                return documentType;
            }       
        }


        internal int TemplateID 
        {
            get
            {
                return templateID;
            }       
        }



    }
}
