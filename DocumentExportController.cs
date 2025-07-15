using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    class DocumentExportController
    {


        internal DocumentExportController()
        {

        }

        internal static void ExportDocument(Document document)
        {
            try
            {
                if (document.UserUploaded == true)
                {
                    ExportFile(document);
                }
                else if (DocumentIsDA4856(document))
                {
                    ExportCounseling(document);
                }
                else if (DocumentIsGenericMemo(document))
                {
                    ExportGenericMemo(document);
                }
                else if (DocumentIsInfoPaper(document))
                {
                    ExportFile(document);
                }
                else if (DocumentIsPregnancyElectionStatement(document))
                {
                    ExportPregnancyElectionStatement(document);
                }
                else if (DocumentIsLetter(document))
                {
                    ExportLetter(document);
                }
            }
            catch (CQPExportFailedException ex)
            {
                string errorMessage = "";

                if (ex.Reason == ExportFailedReason.TemplateMissing)
                {
                    errorMessage = "An error occurred attempting to export the document.\n\n"
                                        + "The template file is missing";
                }
                else if (ex.Reason == ExportFailedReason.InvalidFilename)
                {
                    errorMessage = "An error occurred attempting to export the document.\n\n"
                                        + "The path specified is invalid.";
                }

                Logger.Error(errorMessage, ex);

                CQPMessageBox.Show(errorMessage);
            }
            catch (Exception ex)
            {
                Logger.Error("An unknown error occurred attempting to export the document; most likely a key/value pair mismatch.", ex);
                
                throw new CQPException("An error occurred while attempting to export the document.", ex);
            }
        }


        private static void ExportLetter(Document document)
        {
            string filename = PromptForExportFilename("Microsoft Word Documents (*.doc)|*.doc");
            if (filename == "")
                return;

            LetterInterface letter;
            if (document is LetterInterface)
                letter = document as LetterInterface;
            else
                letter = new LetterInterface(document.GeneratedDocID);

            letter.Export(filename);

            TryOpenExportedDocument(filename);
        }


        private static bool DocumentIsLetter(Document document)
        {
            if (document is LetterInterface ||
                document.FormID == (int)DocumentFormIDs.Letter)
                return true;

            return false;
        }


        private static bool DocumentIsPregnancyElectionStatement(Document document)
        {
            if (document is PregnancyElectionStatementMemo ||
                document.FormID == (int)DocumentFormIDs.PregnancyElectionStatement)
                return true;

            return false;
        }


        private static void ExportPregnancyElectionStatement(Document document)
        {
            string filename = PromptForExportFilename("Microsoft Word Documents (*.doc)|*.doc");
            if (filename == "")
                return;

            PregnancyElectionStatementMemo memo;
            if (document is PregnancyElectionStatementMemo)
                memo = document as PregnancyElectionStatementMemo;
            else
                memo = new PregnancyElectionStatementMemo(document.GeneratedDocID);

            memo.Export(filename);

            TryOpenExportedDocument(filename);
        }


        private static bool DocumentIsInfoPaper(Document document)
        {
            if (document.DocumentType == DocumentType.InfoPaper)
                return true;
            
            return false;
        }


        private static string PromptForExportFilename(string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            dialog.RestoreDirectory = true;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.Cancel)
                return "";
            else
                return dialog.FileName;
        }
        

        private static void ExportCounseling(Document document)
        {

            DA4856Document counseling;
            if (document is DA4856Document)
                counseling = document as DA4856Document;
            else
                counseling = new DA4856Document(document.GeneratedDocID);

            //string filename = PromptForExportFilename("Lotus Form Files (*.xfdl)|*.xfdl");
            string filename = PromptForExportFilename("PDF Files (*.pdf)|*.pdf");
            if (filename == "")
                return;

            //counseling.Export(filename, DocumentFormIDs.DA4856);
            counseling.Export(filename, DocumentFormIDs.DA4856PDF);

            TryExportContinuation(counseling);

            TryOpenExportedDocument(filename);

            //System.Diagnostics.Process p1 = new System.Diagnostics.Process();
            //p1.StartInfo.FileName = filename;
            //p1.Start();

            //while (!p1.Responding) ;
 
        }


        private static void TryExportContinuation(DA4856Document counseling)
        {
            bool exportContinuatoin = false;
            if (counseling.ContinuationText != "")
                exportContinuatoin = PromptToExportContinuation(counseling);

            if (!exportContinuatoin)
                return;

            string templateName = DocumentModel.GetFormFilename((int)DocumentFormIDs.ContinuationOfCounselingPDF);
            string continuationFilename = "";
            continuationFilename = PromptForExportFilename("PDF Documents (*.pdf)|*.pdf");
            if (continuationFilename == "")
                return;

            try
            {
                //ContinuationOfCounselingXMLForm form = new ContinuationOfCounselingXMLForm(templateName, continuationFilename);
                ContinuationOfCounselingPDFForm form = new ContinuationOfCounselingPDFForm(templateName, continuationFilename);

                //form.LoadForm(continuationFilename);

                //if (form.IsOpen() == false)
                //    throw new FileException("Could not open " + continuationFilename + " for writing!");

                //form.Fill(counseling);

                DA4856FormFiller.Fill(counseling, form);

                form.SaveForm(continuationFilename);
                form = null;

                TryOpenExportedDocument(continuationFilename);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private static bool PromptToExportContinuation(DA4856Document counseling)
        {
            //DialogResult result = new PromptToExportContinuationForm().ShowDialog();
            string message = "This counseling has a Continuation of Counsling attached.\n"
                              + "Export the Continuation of Counseling as well?";
            string caption = "Export Continuation?";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Question;
            List<string> buttonText = new List<string> { "Export Continuation", "No" };

            DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icon);

            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }


        internal static bool DocumentIsDA4856(Document document)
        {
            if (document.FormID == (int)DocumentFormIDs.DA4856PDF
                || document is DA4856Document)
                //|| document.FormID == (int)DocumentFormIDs.ContinuationOfCounseling)
                return true;

            return false;
        }


        internal static void TryOpenExportedDocument(string filename)
        {
            try
            {

                /*if (!FileUtils.FileTypeHandlerExists(filename))
                {
                    ShowErrorByFileType(filename);
                    return;
                }*/

                System.Diagnostics.Process.Start(filename);

                /*System.Diagnostics.Process p1 = new System.Diagnostics.Process();
                p1.StartInfo.FileName = filename;
                p1.Start();*/

            //    while (!p1.Responding) ;
            }
            catch (Exception)
            {
                string error = "An error occurred attempting to open " + filename;
                string caption = "Error";
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;
                CQPMessageBox.CQPMessageBoxButtons button = CQPMessageBox.CQPMessageBoxButtons.OK;

                CQPMessageBox.ShowDialog(error, caption, button, icon);
            }
        }

        private static void ShowErrorByFileType(string filename)
        {
            FileInfo info = new FileInfo(filename);
            string extension = info.Extension;

            string error;
            string caption = "Error Opening File";
            //CQPMessageBox.CQPMessageBoxButtons = CQPMessageBox.CQPMessageBoxButtons.OK;
            CQPMessageBox.CQPMessageBoxButtons buttons;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

            if (extension == ".doc" || extension == ".docx")
            {
                error = "Error attempting to open " + filename + "\n"
                    + "Microsoft Word is not installed";
                buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.Show(error, caption, buttons, icon);
            }
            else if (extension == ".xfdl")
            {
                error = "Error attempting to open " + filename + "\n"
                    + "Lotus Forms Viewer is not installed.  Download Lotus Forms Viewer?\n";
                buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
                DialogResult result = CQPMessageBox.Show(error, caption, buttons, icon);

                if (result == DialogResult.Yes)
                    System.Diagnostics.Process.Start("http://www.e-publishing.af.mil/viewerdownload.asp");
            }
            else if (extension == ".pdf")
            {
                if (OptionsModel.ReferenceOptions.ShowAdobeMissingWarning == false)
                    return;

                error = "Error attempting to open " + filename + "\n"
                    + "Adobe Reader is not installed.  Download Adobe Reader?\n";
                buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
                DialogResult result = CQPMessageBox.Show(error, caption, buttons, icon);

                if (result == DialogResult.Yes)
                    System.Diagnostics.Process.Start("http://get.adobe.com/reader/");
            }
        }


        private static void ExportGenericMemo(Document document)
        {
            GenericMemo memo = new GenericMemo();
            if (document is GenericMemo)
                memo = document as GenericMemo;
            else
                memo = new GenericMemo(document.GeneratedDocID);

            string filename = PromptForExportFilename("Microsoft Word Documents (*.doc)|*.doc");
            if (filename == "")
                return;

            memo.Export(filename);
            TryOpenExportedDocument(filename);
        }


        private static bool DocumentIsGenericMemo(Document document)
        {
            if (document is GenericMemo || document.FormID == (int)DocumentFormIDs.GenericMemo)
                return true;

            return false;
        }

        private static void ExportFile(Document document)
        {
            FileInfo file = new FileInfo(document.Filepath);
            
            if(file.Exists == false)
            {
                string errormessage = @"The file '" + document.Filepath + "' does not exist or could not be found.";
                string errorcaption = @"Export error.";
                CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                CQPMessageBox.Show(errormessage, errorcaption, buttons, icon);
                return;
            }

            string extension = "*" + file.Extension;
            string filename = PromptForExportFilename(extension + "|" + extension);
            if (filename == "")
                return;

            FileInfo outfile = new FileInfo(filename);

            if (FileUtils.IsFileLocked(outfile))
            {
                string error = filename + " is locked and cannot be overwritten - close the file and try again.";
                string caption = "Error - File Locked";

                CQPMessageBox.CQPMessageBoxButtons button = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                CQPMessageBox.ShowDialog(error, caption, button, icon);
                return;
            }

            FileUtils.BlockingFileCopy(file, outfile);
            TryOpenExportedDocument(filename);
        }
    }
}
