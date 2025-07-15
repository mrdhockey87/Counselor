using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class MemoModel : DocumentModel
    {

        internal static void SaveGenericMemoValues(DataTable userGeneratedValues, GenericMemo memo, byte[] IVbytes)
        {
            //Lock();

            int pkid;

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Organization Name");
            SaveDocumentValue(pkid, memo.OrganizationName, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Organization Street Address");
            SaveDocumentValue(pkid, memo.OrganizationStreetAddress, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Organization City State Zip");
            SaveDocumentValue(pkid, memo.OrganizationCityStZip, IVbytes);

            //pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Document Type For");
            //SaveDocumentValue(pkid, memo.DocumentTypeForLine);

            //pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Recipient To Line");
            //SaveDocumentValue(pkid, memo.RecipientToLine);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Memorandum For");
            SaveDocumentValue(pkid, memo.MemorandumForLine, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Subject");
            SaveDocumentValue(pkid, memo.Subject, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Body");
            SaveDocumentValue(pkid, memo.Body, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Sender Name");
            SaveDocumentValue(pkid, memo.SenderName, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Sender Rank");
            //int senderRankInt = (int)memo.SenderRank;
            //SaveDocumentValue(pkid, senderRankInt.ToString());
            SaveDocumentValue(pkid, memo.SenderRank, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Sender Title");
            SaveDocumentValue(pkid, memo.SenderTitle, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Distribution");
            SaveDocumentValue(pkid, memo.Distribution, IVbytes);

            //Unlock();
        }

        internal static void SavePregnancyElectionStatement(DataTable userGeneratedValues, PregnancyElectionStatementMemo memo, byte[] IVbytes)
        {
            //Lock();

            int pkid;

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Organization Name");
            SaveDocumentValue(pkid, memo.OrganizationName, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Organization Street Address");
            SaveDocumentValue(pkid, memo.OrganizationStreetAddress, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Organization City State Zip");
            SaveDocumentValue(pkid, memo.OrganizationCityStZip, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Office Symbol");
            SaveDocumentValue(pkid, memo.OfficeSymbol, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Memorandum For Text");
            SaveDocumentValue(pkid, memo.MemorandumForText, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Date");
            SaveDocumentValue(pkid, memo.Date.ToString("yyyy MM dd"), IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Subject");
            SaveDocumentValue(pkid, memo.Subject, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Body");
            SaveDocumentValue(pkid, memo.Body, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Printed Name of Soldier");
            SaveDocumentValue(pkid, memo.ToPrintedNameOfSoldier, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "From Name of Commander");
            SaveDocumentValue(pkid, memo.FromNameOfCommander, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "From Name of Unit");
            SaveDocumentValue(pkid, memo.FromNameOfUnit, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Post Soldiers Signature");
            SaveDocumentValue(pkid, memo.PostSoldierSignature1, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "First SigBlock Commanders Name");
            SaveDocumentValue(pkid, memo.FirstSignatureBlockFromCommandersName, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "First SigBlock Commander Rank Branch");
            SaveDocumentValue(pkid, memo.FirstSignatureBlockFromRankBranch, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "First SigBlock Commander Title");
            SaveDocumentValue(pkid, memo.FirstSignatureBlockFromCommanderTitle, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "To Name of Commander");
            SaveDocumentValue(pkid, memo.ToCommandersName, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "To Name of Unit");
            SaveDocumentValue(pkid, memo.ToNameOfUnit, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "From Name of Soldier");
            SaveDocumentValue(pkid, memo.FromNameOfSoldier, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Post Soldier Signature 2");
            SaveDocumentValue(pkid, memo.PostSoldierSignature2, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Second SigBlock Soldiers Name");
            SaveDocumentValue(pkid, memo.SecondSignatureBlockSoldiersName, IVbytes);

            pkid = GetPKIDForDocumentEntry(userGeneratedValues, "Second SigBlock Rank, SSN");
            SaveDocumentValue(pkid, memo.SecondSignatureBlockRankSSN, IVbytes);

            //Unlock();
        }
    }
}
