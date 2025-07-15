using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CounselQuickPlatinum
{
    internal enum SaveChangesButtons
    {
        DontSaveCancel,
        SaveDontSave,
        SaveDontSaveCancel
    }

    internal static class DialogHelper
    {        
        #region Redraw Suspend/Resume
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void SuspendDrawing(this Control target)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }

        public static void ResumeDrawing(this Control target) { ResumeDrawing(target, true); }
        public static void ResumeDrawing(this Control target, bool redraw)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Refresh();
            }
        }
        #endregion

        internal static string GetNewPictureFilename()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an image file...";
            openFileDialog.Filter = "Image Files|*.jpg; *.jpeg; *.bmp; *.png; *.gif";
            openFileDialog.RestoreDirectory = true;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return "";

            string filename = openFileDialog.FileName;
            return filename;
        }

        internal static bool TextFitsInTextbox(CQPRichTextBox textbox, Size maxSize)
        {
            return TextFitsInTextbox(textbox.Text, maxSize);
        }

        internal static bool TextFitsInTextbox(string text, Size maxSize)
        {
            //Font formFont = textbox.Font;
            Font formFont = new Font("Times New Roman", 10);

            Size sizeOfText = TextRenderer.MeasureText(text, formFont, maxSize, TextFormatFlags.WordBreak);

            if (sizeOfText.Height > maxSize.Height)
                return false;

            return true;
        }

        // 1. Get TextFitsInTextBox false
        // 2. Start parsing sentence by sentence until it fits
        // 3. Return as two split things
        internal static bool SplitText(ref string text, ref string continuationText, Size maxSize)
        {
            StringBuilder leftSide = new StringBuilder();
            StringBuilder rightSide = new StringBuilder();

            rightSide.Append(text);
            string stringToMove = "";

            while (TextFitsInTextbox(leftSide.ToString() + "\n\n*** See Continuation of Counseling ***", maxSize))
            {
                int firstBreak = rightSide.ToString().IndexOfAny(new char[] { '.', '\n' });
                
                if (firstBreak == -1)
                    break;

                stringToMove = rightSide.ToString().Substring(0, firstBreak + 1) ;

                leftSide.Append(stringToMove);
                rightSide.Replace(stringToMove, "",0,stringToMove.Length);
            }

            // now we know it doesn't fit, undo the last move
            if (stringToMove != "")
            {
                leftSide.Replace(stringToMove, "", 0, stringToMove.Length);
                rightSide.Insert(0, stringToMove);
            }

            if (rightSide.ToString() == text)
                return false;

            text = leftSide.ToString() + "\n\n*** See Continuation of Counseling ***";
            continuationText = rightSide.ToString();

            return true;
        }

        internal static void FillTextboxWithSelectedValues(List<string> templateValues, List<int> selectedIndicies, CQPRichTextBox textbox)
        {
            List<string> TempValues = templateValues;
            foreach (int index in selectedIndicies)
            {
                string value = templateValues[index];
                textbox.Text += value + "\n";
            }
        }

        internal static void PromptToChooseTemplateValues(Template template, string fieldname, CQPRichTextBox textbox)
        {
            SelectTemplateValuesDialog dialog = new SelectTemplateValuesDialog(template.TemplateValues[fieldname]);
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            List<int> selectedIndicies = dialog.SelectedIndicies;
            DialogHelper.FillTextboxWithSelectedValues(template.TemplateValues[fieldname], selectedIndicies, textbox);
        }

        internal static void ShowEditDocumentDialog(Document document, Control parent)
        {
            if(document.UserUploaded)
            {
                DocumentModel.StartDecryptedUserDocumentEditing(document);
                return;
            }
            else
            {
                ShowDocumentEditorByFormID(document, parent);
            }
        }

        internal static void ShowDocumentEditorByFormID(Document document, Control parent)
        {
            int documentID = document.GeneratedDocID;
            int formID = document.FormID;

            if (document is DA4856Document || document.FormID == (int)DocumentFormIDs.DA4856PDF )
            {
                DA4856Document da4856Document;

                if (document is DA4856Document)
                    da4856Document = document as DA4856Document;
                else
                    da4856Document = new DA4856Document(document.GeneratedDocID);
                
                Template template = new Template(da4856Document.TemplateID);

                XFDLEditorPage1 editor = new XFDLEditorPage1(da4856Document, template);
                editor.ShowDialog(parent);
            }
            else if (document is GenericMemo || document.FormID == (int)DocumentFormIDs.GenericMemo)
            {
                GenericMemo memo;
                if (document is GenericMemo)
                    memo = document as GenericMemo;
                else
                    memo = new GenericMemo(document.GeneratedDocID);

                Template template = new Template(memo.TemplateID);

                GenericMemoEditorStepTwo editor = new GenericMemoEditorStepTwo(memo, template);
                editor.ShowDialog(parent);
            }
            else if (document.DocumentType == DocumentType.InfoPaper)
            {
                string filepath = DocumentModel.GetFormFilename((int)document.FormID);
                document.Filepath = filepath;
                PDFViewerForm form = new PDFViewerForm(document);
                form.ShowDialog(parent);
            }
            else if (document is PregnancyElectionStatementMemo || document.FormID == (int)DocumentFormIDs.PregnancyElectionStatement)
            {
                PregnancyElectionStatementMemo memo;
                if(document is PregnancyElectionStatementMemo)
                    memo = document as PregnancyElectionStatementMemo;
                else
                    memo = new PregnancyElectionStatementMemo(document.GeneratedDocID);

                Template template = new Template(memo.TemplateID);
                PregnancyElectionStatementEditorStepOne form = new PregnancyElectionStatementEditorStepOne(memo, template);
                form.ShowDialog(parent);
            }
        }

        internal static DialogResult ConfirmDetachDocument(int selectedDocumentID)
        {
            Document document = new Document(selectedDocumentID);
            string documentName = DocumentModel.GetDocumentName(selectedDocumentID);
            string parentName = DocumentModel.GetDocumentName(document.ParentDocumentID);

            string message = "Are you sure you want to detach:\n"
                            + documentName
                            + "from it's parent document:"
                            + parentName;
            string caption = "Detach Document?";

            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Question;

            List<string> buttonText = new List<string> { "Detach Document", "Cancel" };

            return CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icon);
        }

        internal static DialogResult PromptToSaveChanges(SaveChangesButtons savebuttons)
        {
            string message = "";
            string caption = "Save changes?";
            List<string> buttonText = new List<string>();
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;

            switch (savebuttons)
            {
                case (SaveChangesButtons.SaveDontSaveCancel):
                    {
                        message = "Are you sure you want to close without saving?";
                        buttonText = new List<string> { "Save", "Don't Save", "Cancel" };
                        buttons = CQPMessageBox.CQPMessageBoxButtons.YesNoCancel;
                        break;
                    }
                case (SaveChangesButtons.SaveDontSave):
                    {
                        message = "Are you sure you want to close without saving?";
                        buttonText = new List<string> { "Save", "Don't Save" };
                        buttons = CQPMessageBox.CQPMessageBoxButtons.YesNoCancel;
                        break;
                    }
                case (SaveChangesButtons.DontSaveCancel):
                    {
                        message = "Are you sure you want to discard changes?";
                        buttonText = new List<string> { "Don't Save", "Cancel" };
                        buttons = CQPMessageBox.CQPMessageBoxButtons.OKCancel;
                        break;
                    }
            }
            
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Question;
            return CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icon);
        }

        internal static List<int> PromptToCompleteCounselingPackage(int templateID, int soldierID, Control parent)
        {
            List<int> templateIDs = TemplatesModel.GetTemplatesInTemplatePackage(templateID);

            if (templateIDs.Count == 0)
                return templateIDs;

            List<string> templateNames = TemplatesModel.GetNamesForTemplateIDs(templateIDs);

            PromptToCompleteCounselingPackageDialog dialog
                    = new PromptToCompleteCounselingPackageDialog(soldierID, templateID, templateNames);
            DialogResult result = dialog.ShowDialog(parent);

            if (result == DialogResult.No)
                templateIDs.Clear();
            if (result == DialogResult.Cancel)
            {
                templateIDs.Clear();
                templateIDs.Add(-1);
            }

            return templateIDs;
        }
    }
}
