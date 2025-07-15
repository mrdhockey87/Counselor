using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class LetterModel : DocumentModel
    {

        internal static void SaveLetterValues(DataTable userGeneratedValues, LetterInterface letter, byte[] IVbytes)
        {
            //Lock();

            int pkid;

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "COMPANY");
            SaveDocumentValue(pkid, letter.Company, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "BATTALIONSQUADRON");
            SaveDocumentValue(pkid, letter.BattalionSquadron, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "COMPANYADDRESSLINE1");
            SaveDocumentValue(pkid, letter.CompanyAddressLine1, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "COMPANYADDRESSLINE2");
            SaveDocumentValue(pkid, letter.CompanyAddressLine2, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "GREETING");
            SaveDocumentValue(pkid, letter.Greeting, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "BODY");
            SaveDocumentValue(pkid, letter.Body, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "SOLDIERSNAME");
            SaveDocumentValue(pkid, letter.SoldiersName, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "RANK");
            SaveDocumentValue(pkid, letter.Rank, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "TITLE");
            SaveDocumentValue(pkid, letter.Title, IVbytes);

            //Unlock();
        }

    }

}
