using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class FormAccessFailedException : Exception
    {
        public FormAccessFailedException(string filename, string propertyName, Exception baseException) : base()
        {
            message = "Error attempting to access value in " + filename + "\n";
            baseExceptionMessage
                = "Source Exception: " + baseException.GetType().ToString() + "\n"
                + "Message: " + baseException.Message + "\n"
                + "Caller: " + baseException.Source + "\n"
                + "Stack Trace: " + baseException.StackTrace + "\n";
        }


        public override string Message
        {
	        get 
	        { 
		         return this.message;
	        }
        }


        public string BaseExceptionMessage
        {
            get
            {
                return this.baseExceptionMessage;
            }
        }


        string message;
        string baseExceptionMessage;
    }
}
