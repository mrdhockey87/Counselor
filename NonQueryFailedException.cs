using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class NonQueryFailedException : Exception
    {
        NonQueryFailedException(string message, Exception baseException)
        {
            this.message = message;
            this.baseException = baseException;
        }

        public override string Message
        {
            get { return message; }
        }

        public Exception BaseException
        {
            get { return baseException; }
        }

        string message;
        Exception baseException;
    }
}
