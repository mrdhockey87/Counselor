using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    enum CQPExceptionCode : int
    {
        ExportFailed = 1,
        DatabaseReadFailed = 2,
        ImportFailed = 4,
        LoadSoldierFailed = 8,
        ExportTemplateFileMissing = 16
    }


    enum ImportFailedReason
    {

    }


    internal enum ExportFailedReason
    {
        TemplateMissing,
        WriteProtectedDirectory,
        InvalidFilename,
        Other
    }

    internal class CQPException : Exception
    {
        internal CQPException(string message) 
            : base(message)
        {
            codes = new List<CQPExceptionCode>();
        }

        internal CQPException(string message, Exception ex)
            : base(message, ex)
        {
            codes = new List<CQPExceptionCode>();
        }

        internal List<CQPExceptionCode> codes;
    }


    internal class CQPExportFailedException : CQPException
    {


        const CQPExceptionCode code = CQPExceptionCode.ExportFailed;
        internal ExportFailedReason Reason { get; set; }

        internal CQPExportFailedException(ExportFailedReason reason, string message)
            : base(message)
        {
            Reason = reason;
        }

        internal CQPExportFailedException(ExportFailedReason reason, CQPException ex, string message)
            : base(message)
        {
            Reason = reason;
            ex.codes.Add(code);
        }
    }





    internal class CQPImportFailedException : CQPException
    {


        const CQPExceptionCode code = CQPExceptionCode.ImportFailed;
        internal ImportFailedReason Reason { get; set; }

        internal CQPImportFailedException(ImportFailedReason reason, string message) 
            : base(message)
        {
            Reason = reason;
        }

        internal CQPImportFailedException(ImportFailedReason reason, CQPException ex, string message)
            : base(message)
        {
            ex.codes.Add(code);
            Reason = reason;
        }
    }


    internal class CQPLoadSoldierFailedException : CQPException
    {
        const CQPExceptionCode code = CQPExceptionCode.LoadSoldierFailed;

        internal CQPLoadSoldierFailedException(string message) : base(message)
        {
            
        }

        internal CQPLoadSoldierFailedException(CQPException ex, string message)
            : base(message)
        {
            ex.codes.Add(code);
        }
    }


    class DatabaseTransactionException : CQPException
    {
        public DatabaseTransactionException(string message) : base(message)
        {
            Logger.Error("***** DatabaseTransactionException ***** : " + message);
        }

            
        public DatabaseTransactionException(string message, Exception ex)
            : base(message, ex)
        {
            Logger.Error("***** DatabaseTransactionException ***** : " + message);
        }

    }



    class DataStoreFailedException : DatabaseTransactionException
    {
        public DataStoreFailedException(string message)
            : base(message)
        {
            Logger.Error("***** DataStoreFailedException ****** : " + message);
        }


        public DataStoreFailedException(string message, Exception ex)
            : base(message, ex)
        {
            Logger.Error("***** DataStoreFailedException ****** : " + message);
            Logger.Error(ex);
        }
    }


    class DataLoadFailedException : DatabaseTransactionException
    {
        public DataLoadFailedException(string error)
            : base(error)
        {
            Logger.Error("***** DataLoadFailedException ****** : " + error);
        }

        public DataLoadFailedException(string error, Exception ex)
            : base(error, ex)
        {
            Logger.Error("***** DataLoadFailedException ****** : " + error);
            Logger.Error(ex);
        }

    }
}
