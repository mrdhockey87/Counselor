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
    public partial class LetterEditor : Form
    {
        private LetterInterface letter;
        private Template template;
        bool considerSaving;

        BackgroundWorker saveWorker;
        BackgroundWorker postSaveStatusLabelWorker;
        bool isSaving;
        bool closeAfterSaving;

        Control lastControl;
        bool enableExportButton;

        public LetterEditor()
        {
            enableExportButton = false;

            Load += new EventHandler(OnLoad);
            InitializeComponent();
            considerSaving = true;
        }

        void OnLoad(object sender, EventArgs e)
        {
            saveWorker = new BackgroundWorker();
            postSaveStatusLabelWorker = new BackgroundWorker();

            DataTable documentStatuses = DocumentModel.GetDocumentStatuses();
            documentStatusCombobox.DataSource = documentStatuses;
            documentStatusCombobox.DisplayMember = "documentstatustext";
            documentStatusCombobox.ValueMember = "documentstatusid";

            documentNameLabel.Text = letter.DocumentName;

            LoadDocumentValuesFromLetter();

            letter.HasUnsavedChanges = false;

            bool enable = (letter.GeneratedDocID == -1 ? true : false);
            saveAndCloseButton.Enabled = enable;
            saveButton.Enabled = enable;
            letter.HasUnsavedChanges = false;

            enableExportButton = WordInterop.WordExists;
            exportFormButton.Enabled = enableExportButton;

            statusLabel.Text = "";

            isSaving = false;

            SetAllTextboxEnterCallbacks(this);

            companyNameTextbox.Focus();
            lastControl = companyNameTextbox;
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }


        internal LetterEditor(LetterInterface letter, Template template)
            : this()
        {
            this.letter = letter;
            this.template = template;
        }


        void SetAllTextboxEnterCallbacks(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Enter += delegate(object s, EventArgs ea)
                    {
                        lastControl = (Control)s;
                    };
                }

                SetAllTextboxEnterCallbacks(c);
            }
        }


        private void LoadDocumentValuesFromLetter()
        {
            dateTimePicker1.Value = letter.Date;
            documentStatusCombobox.SelectedValue = (int)letter.Status;

            companyNameTextbox.Text = letter.Company;
            battalionSquadronTextbox.Text = letter.BattalionSquadron;
            companyStAddrTextbox.Text = letter.CompanyAddressLine1;
            companyCityStZipTextbox.Text = letter.CompanyAddressLine2;

            recepientNameTextbox.Text = letter.RecepientName;
            receipientAddressLine1Textbox.Text = letter.ReceipientAddressLine1;
            receipientAddressLine2Textbox.Text = letter.ReceipientAddressLine2;

            greetingTextbox.Text = letter.Greeting;

            bodyRichTextBox.Text = letter.Body;

            senderNameTextbox.Text = letter.SoldiersName;
            senderRankTextbox.Text = letter.Rank;
            senderTitleTextbox.Text = letter.Title;
        }

        private void SaveDialogValuesToLetter()
        {
            letter.Date = dateTimePicker1.Value;
            letter.Status = (DocumentStatus) Convert.ToInt32(documentStatusCombobox.SelectedValue);
            
            letter.Company = companyNameTextbox.Text;
            letter.BattalionSquadron = battalionSquadronTextbox.Text;
            letter.CompanyAddressLine1 = companyStAddrTextbox.Text;
            letter.CompanyAddressLine2 = companyCityStZipTextbox.Text;

            letter.RecepientName = recepientNameTextbox.Text;
            letter.ReceipientAddressLine1 = receipientAddressLine1Textbox.Text;
            letter.ReceipientAddressLine2 = receipientAddressLine2Textbox.Text;

            letter.Greeting = greetingTextbox.Text;
            
            letter.Body = bodyRichTextBox.Text;
            
            letter.SoldiersName = senderNameTextbox.Text;
            letter.Rank = senderRankTextbox.Text;
            letter.Title = senderTitleTextbox.Text;
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            letter.HasUnsavedChanges = true;
            RefreshSaveButtons();
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            if (letter.HasUnsavedChanges)
            {
                saveAndCloseButton.Enabled = true;
                saveButton.Enabled = true;

                statusLabel.Text = "Unsaved changes";
            }
            else
            {
                saveAndCloseButton.Enabled = false;
                saveButton.Enabled = false;

                statusLabel.Text = "";
            }
        }

        private void SaveLetter()
        {
            try
            {
                isSaving = true;

                DisableButtons();
                statusLabel.Text = "";

                SaveDialogValuesToLetter();
                letter.Status = (DocumentStatus)Convert.ToInt32(documentStatusCombobox.SelectedValue);

                saveWorker = new BackgroundWorker();
                saveWorker.DoWork += new DoWorkEventHandler(SaveLetterBackgroud);
                saveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostSavedLetter);
                saveWorker.ProgressChanged += new ProgressChangedEventHandler(SaveProgressUpdated);
                saveWorker.WorkerSupportsCancellation = true;
                saveWorker.WorkerReportsProgress = true;

                letter.HasUnsavedChanges = false;

                progressBar1.Show();
                saveWorker.RunWorkerAsync();
                SaveLocation();

            }
            catch (DataStoreFailedException ex)
            {
                Logger.Error(ex);

                CQPMessageBox.Show("There was an error while saving the document.", "Error Saving",
                                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                throw ex;
            }
        }


        void SaveProgressUpdated(object sender, ProgressChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Percent: " + e.ProgressPercentage);

            if (this.IsDisposed)
                return;

            progressBar1.Value = e.ProgressPercentage;
        }


        void SaveLetterBackgroud(object sender, DoWorkEventArgs e)
        {
            letter.Save(saveWorker);
        }


        void PostSavedLetter(object sender, RunWorkerCompletedEventArgs e)
        {
            isSaving = false;

            if (closeAfterSaving)
                this.Dispose();

            progressBar1.Hide();
            progressBar1.Value = 0;

            EnableButtons();

            if (postSaveStatusLabelWorker != null && postSaveStatusLabelWorker.IsBusy)
                postSaveStatusLabelWorker.CancelAsync();

            postSaveStatusLabelWorker = new BackgroundWorker();
            postSaveStatusLabelWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ClearStatusLabel);
            postSaveStatusLabelWorker.DoWork += new DoWorkEventHandler(SleepThreeSeconds);
            postSaveStatusLabelWorker.WorkerSupportsCancellation = true;
            postSaveStatusLabelWorker.RunWorkerAsync();
            SaveLocation();
        }


        private void DisableButtons()
        {
            saveButton.Enabled = false;
            saveAndCloseButton.Enabled = false;
            exportFormButton.Enabled = false;
        }


        private void EnableButtons()
        {
            //exportFormButton.Enabled = true;
            exportFormButton.Enabled = enableExportButton;

            if (letter.HasUnsavedChanges)
            {
                saveButton.Enabled = true;
                saveAndCloseButton.Enabled = true;
            }
        }

        private void RefreshSaveButtons()
        {
            if (saveWorker.IsBusy)
                return;

            if (letter.HasUnsavedChanges)
            {
                saveAndCloseButton.Enabled = true;
                saveButton.Enabled = true;
            }
            else
            {
                saveAndCloseButton.Enabled = false;
                saveButton.Enabled = false;
            }
        }

        private void SaveAndClose()
        {
            SaveLetter();
            considerSaving = false;
            closeAfterSaving = true;
            SaveLocation();
        }

        void ClearStatusLabel(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLabel.Text = "";
        }

        void SleepThreeSeconds(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveLetter();

            lastControl.Focus();
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            SaveAndClose();

            lastControl.Focus();
        }

        private void discardChangesButton_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);

            lastControl.Focus();

            if (result == DialogResult.Cancel)
                return;
            else if (result == DialogResult.Yes)
                SaveAndClose();
            else
            {
                considerSaving = false;
                SaveLocation();
                this.Dispose();
            }
        }

        private void LetterEditorStepTwo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaving)
            {
                //System.Diagnostics.Debug.WriteLine("waiting on worker...\n");
                e.Cancel = true;
                return;
            }

            if (considerSaving == true && letter.HasUnsavedChanges)
            {
                DialogResult result
                    = DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    SaveAndClose();
                    e.Cancel = true;
                    return;
                }
                else
                {
                    considerSaving = false;
                    SaveLocation();
                    this.Dispose();
                }
            }
            SaveLocation();
        }

        private void exportFormButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDialogValuesToLetter();
                DocumentExportController.ExportDocument(letter);
            }
            catch (CQPExportFailedException ex)
            {
                string caption = "Export error";
                CQPMessageBox.CQPMessageBoxButtons button = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                if (ex.Reason == ExportFailedReason.InvalidFilename)
                    CQPMessageBox.Show("Export failed: Invalid filename", caption, button, icon);
                else if (ex.Reason == ExportFailedReason.WriteProtectedDirectory)
                    CQPMessageBox.Show("Export failed: The directory is write protected.", caption, button, icon);
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LetterEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLocation();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.Form1SavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.LetterEditorLocation, Properties.Settings.Default.LetterEditorSize);
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
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.Form1SavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.Form1SavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.LetterEditorSize = bounds.Size;
            Properties.Settings.Default.LetterEditorLocation = bounds.Location;
            Properties.Settings.Default.LetterEditorHeight = bounds.Height;
            Properties.Settings.Default.LetterEditorWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
    }
}
