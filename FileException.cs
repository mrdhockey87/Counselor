using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class FileException : Exception
    {
        public FileException(string filename) : base()
        {
            FileInfo fileinfo = new FileInfo(filename);
            bool readOnly = (fileinfo.Attributes  & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;

            message = "Error accessing file: " + filename + "\n"
                + "Exists: " + fileinfo.Exists.ToString() + "\n"
                + "Read Only: " + readOnly.ToString() + "\n";

            baseExceptionMessage = 
                  "Caller: " + this.Source + "\n"
                + "Stack Trace: " + this.StackTrace + "\n";
        }

        public FileException(string filename, Exception baseException)
            : base(filename)
        {
            FileInfo fileinfo = new FileInfo(filename);
            bool readOnly = (fileinfo.Attributes  & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;

            message = "Error access file: " + filename + "\n"
                + "Exists: " + fileinfo.Exists.ToString() + "\n"
                + "Read Only: " + readOnly.ToString() + "\n";

            this.baseExceptionMessage
                = "Source Exception: " + baseException.GetType().ToString() + "\n"
                + "Message: " + baseException.Message + "\n"
                + "Caller: " + baseException.Source + "\n"
                + "Stack Trace: " + baseException.StackTrace + "\n";
        }

        public string BaseExceptionMessage
        {
            get { return baseExceptionMessage; }
        }

        public override string Message
        {
            get { return this.message; }
        }

        string message;
        string baseExceptionMessage;
    }
}
