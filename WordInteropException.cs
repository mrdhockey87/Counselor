using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal class WordInteropException : Exception
    {
        internal WordInteropException(string message, Exception ex)
        {
            this.message = message;
            this.baseException = ex;
        }

        override public string Message
        {
            get
            {
                return this.message;
            }
        }

        override public Exception GetBaseException()
        {
            return baseException;
        }

        string message;
        Exception baseException;
    }
}
