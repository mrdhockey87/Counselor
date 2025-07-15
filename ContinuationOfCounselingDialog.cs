using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    internal partial class ContinuationOfCounselingDialog : Form
    {
        DA4856Document document;

        internal ContinuationOfCounselingDialog(DA4856Document document)
        {
            Logger.Trace("ContinuationOfCounselingDialog - Constructor: " + document.GeneratedDocID);
            
            this.document = document;

            Load += new EventHandler(ContinuationOfCounselingDialog_Load);
            InitializeComponent();
        }


        void ContinuationOfCounselingDialog_Load(object sender, EventArgs e)
        {
            continuationRichTextBox.Text = document.ContinuationText;
            document.HasUnsavedContinuationChanges = false;
            statusLabel.Text = "";

            DialogResult = DialogResult.None;
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.ContinuationOfCounselingDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.ContinuationOfCounselingDialogLocation, Properties.Settings.Default.ContinuationOfCounselingDialogSize);
                switch (winState)
                {
                    case "Normal":
                        this.WindowState = FormWindowState.Normal;
                        break;
                    case "Maximized":
                        this.WindowState = FormWindowState.Maximized;
                        break;
                    default:
                        this.WindowState = FormWindowState.Normal;
                        break;
                }
            }
            //check to see if the form is visible if not move it to the center of the primary screen mdail 8-15-19
            bool visible = Utilities.isWindowVisible(this.DesktopBounds);
            if (!visible)
            {
                Utilities.centerFormPrimary(this);
            }
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("ContinuationOfCounselingDialog - SaveButtonClicked");

            SaveContinuation();
            DialogResult = DialogResult.OK;
            SaveLocation();
            this.Dispose();
        }


        private void SaveContinuation()
        {
            Logger.Trace("ContinuationOfCounselingDialog - SaveContinuation");

            SaveDialogValuesToDocument();

            try
            {
                document.SaveContinuation();
            }
            catch (Exception ex)
            {
                Logger.Error("Error saving the continuation", ex);
                
                string error = "An error occurred while saving the continuation.\n\nCounselor will now close.";
                string caption = "Save Error";
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;
                CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;

                CQPMessageBox.Show(error, caption, buttons, icon);

                throw new CQPException(error, ex);
            }

            document.HasUnsavedContinuationChanges = false;
            statusLabel.Text = "Saved.";
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("ContinuationOfCounselingDialog - CancelButtonClicked");

            if(document.HasUnsavedContinuationChanges)
            {
                PromptToSaveChanges();

                if (DialogResult == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            SaveLocation();
            this.Dispose();
        }


        private void PromptToSaveChanges()
        {
            Logger.Trace("ContinuationOfCounselingDialog - promptosavechanges");

            DialogResult result = DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);

            Logger.Trace("    ContinuationOfCounselingDialog - " + result.ToString());

            if (result == DialogResult.Cancel || result == DialogResult.No)
                DialogResult = result;
            else if (result == DialogResult.Yes)
            {
                SaveContinuation();
                DialogResult = DialogResult.OK;
            }
        }


        private void SaveDialogValuesToDocument()
        {
            Logger.Trace("ContinuationOfCounselingDialog - SaveDialogValuesToDocument");

            document.ContinuationText = continuationRichTextBox.Text;
        }


        private void ContinuationOfCounselingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.Trace("ContinuationOfCounselingDialog - FormClosing: DialogResult: " + DialogResult.ToString());

            if (DialogResult == DialogResult.No)
                return;

            if (document.HasUnsavedContinuationChanges)
            {
                PromptToSaveChanges();
                if (DialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            SaveLocation();
        }
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized: //ContinuationOfCounselingDialogSavedWindowState
                    Properties.Settings.Default.ContinuationOfCounselingDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.ContinuationOfCounselingDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.ContinuationOfCounselingDialogSize = bounds.Size;
            Properties.Settings.Default.ContinuationOfCounselingDialogLocation = bounds.Location;
            Properties.Settings.Default.ContinuationOfCounselingDialogHeight = bounds.Height;
            Properties.Settings.Default.ContinuationOfCounselingDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();

        }

        private void OnExportButtonClicked(object sender, EventArgs e)
        {
            Logger.Trace("ContinuationOfCounselingDialog - OnExportButtonClicked");

            SaveDialogValuesToDocument();

            SaveFileDialog dialog = new SaveFileDialog();
            //dialog.Filter = "Lotus Forms Documents (*.xfdl)|*.xfdl";
            dialog.Filter = "PDF Documents (*.pdf)|*.pdf";
            dialog.SupportMultiDottedExtensions = true;
            dialog.Title = "Select a document...";
            dialog.RestoreDirectory = true;
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            try
            {
                //string templateName = DocumentModel.GetFormFilename((int)DocumentFormIDs.ContinuationOfCounseling);
                string templateName = DocumentModel.GetFormFilename((int)DocumentFormIDs.ContinuationOfCounselingPDF);
                //ContinuationOfCounselingXMLForm form = new ContinuationOfCounselingXMLForm(templateName, dialog.FileName);
                ContinuationOfCounselingPDFForm form = new ContinuationOfCounselingPDFForm(templateName, dialog.FileName);

                //form.LoadForm(dialog.FileName);

                //if (form.IsOpen() == false)
                //   throw new FileException("Could not open " + dialog.FileName + " for writing!");
                ContinuationOfCounselingFormFiller.Fill(document, form);
                form.SaveForm(dialog.FileName);
                form = null;

                System.Diagnostics.Process.Start(dialog.FileName);
            }
            catch (Exception ex)
            {
                Logger.Error("    ContinuationOfCounselingDialog - Caught fileexception : " + ex.Message);
                Logger.Error(ex);

                CQPMessageBox.Show("An error occurred attempting to export the continuation.", "Export Error", 
                                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                return;
                //throw new CQPException("An error occurred attempting to export the continuation.\n\nCounselor will now close.", ex);
            }

            string message = "File exported successfully.";
            string caption = "File exported.";

            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Information;

            CQPMessageBox.Show(message, caption, buttons, icon);
        }

        private void OnDateTimePickerValueChanged(object sender, EventArgs e)
        {
            OnChanges();
        }

        private void OnNameTextboxTextChanged(object sender, EventArgs e)
        {
            OnChanges();
        }

        private void ContinuationTextBoxTextChanged(object sender, EventArgs e)
        {
            OnChanges();
        }

        private void OnChanges()
        {
            document.HasUnsavedContinuationChanges = true;
            statusLabel.Text = "Unsaved Changes";
        }
    }
}
