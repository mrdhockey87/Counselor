using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    class ReassignDocumentController
    {
        Document document;
        internal List<Document> OtherDocumentsToUpdate { get; set; }
        public int NewParentDocumentID { get; set; }

        internal ReassignDocumentController(Document document)
        {
            this.document = document;
            OtherDocumentsToUpdate = new List<Document>();
            NewParentDocumentID = -1;
        }

        internal DialogResult PromptToSelectNewParentDocument(SelectNewParentDocumentDialog.SelectNewParentMode mode)
        {
            int soldierID = document.SoldierID;
            int documentID = document.GeneratedDocID;
            int oldParentDocumentID = document.ParentDocumentID;

            SelectNewParentDocumentDialog dialog = //new SelectNewParentDocumentDialog(soldierID, documentID, oldParentDocumentID);
                new SelectNewParentDocumentDialog(document, mode);
            DialogResult dialogResult = dialog.ShowDialog();

            if (dialogResult != DialogResult.Cancel)
                NewParentDocumentID = dialog.NewParentDocumentID;

            return dialogResult;
        }


        //private DialogResult SelectNewParentForChildDocs(DataRow[] childDocs)
        private DialogResult SelectNewParentForChildDocs(List<Document> childDocs)
        {
            SelectNewParentDocumentDialog dialog = new SelectNewParentDocumentDialog(document, SelectNewParentDocumentDialog.SelectNewParentMode.SelectNewFromChildren);
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return result;
            else if (result == DialogResult.OK)
            {
                int newDocumentID = dialog.NewParentDocumentID;
                //foreach (DataRow row in childDocs)
                //foreach(int childDocumentID in childDocs)
                foreach (Document childDocument in childDocs)
                {
                    //int childDocumentID = Convert.ToInt32(row["generateddocid"]);
                    //Document childDocument = new Document(childDocumentID);
                    if (childDocument.GeneratedDocID == newDocumentID)
                        childDocument.ParentDocumentID = -1;
                    else
                        childDocument.ParentDocumentID = newDocumentID;

                    OtherDocumentsToUpdate.Add(childDocument);
                }

                return result;
            }

            return DialogResult.Cancel;
        }


        //private DialogResult DetachAllChildDocs(DataRow[] childDocs)
        private DialogResult DetachAllChildDocs(List<Document> childDocs)
        {
            foreach (Document childDocument in childDocs)
            {
                //int childDocumentID = Convert.ToInt32(row["generateddocid"]);
                //Document childDocument = new Document(childDocumentID);
                childDocument.ParentDocumentID = -1;
                OtherDocumentsToUpdate.Add(childDocument);
            }

            return DialogResult.OK;
        }


        //internal DialogResult PromptToMoveChildDocuments(DataRow[] childDocs)
        internal DialogResult PromptToMoveChildDocuments(List<Document> childDocs)
        {
            int soldierID = document.SoldierID;
            int documentID = document.GeneratedDocID;
            int newParentID = document.ParentDocumentID;

            DocumentReassignmentReason reason = DocumentReassignmentReason.ChangingParent;
            DocumentReassignmentMode reassignmentMode = ReassignChildDocumentsDialog.ShowDialog(reason);

            if (reassignmentMode == DocumentReassignmentMode.Cancel)
            {
                return DialogResult.Cancel;
            }
            else if (reassignmentMode == DocumentReassignmentMode.NewParent)
            {
                return SelectNewParentForChildDocs(childDocs);
            }
            else if (reassignmentMode == DocumentReassignmentMode.DetachAllChildren)
            {
                return DetachAllChildDocs(childDocs);
            }

            return DialogResult.Cancel;
        }
    }
}
