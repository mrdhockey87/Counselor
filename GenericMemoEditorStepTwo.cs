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
    internal partial class GenericMemoEditorStepTwo : Form
    {
        private GenericMemo memo;
        private Template template;
        bool considerSaving;

        BackgroundWorker saveWorker;
        BackgroundWorker postSaveStatusLabelWorker;
        bool isSaving;
        bool closeAfterSaving;

        bool enableExportButton;

        Control lastControl;

        private GenericMemoEditorStepTwo()
        {
            enableExportButton = true;

            Load += new EventHandler(OnLoad);
            InitializeComponent();
            
            considerSaving = true;
        }

        internal GenericMemoEditorStepTwo(GenericMemo memo, Template template)
            : this()
        {
            this.memo = memo;
            this.template = template;

        }

        void OnLoad(object sender, EventArgs e)
        {
            saveWorker = new BackgroundWorker();
            postSaveStatusLabelWorker = new BackgroundWorker();

            DataTable documentStatuses = DocumentModel.GetDocumentStatuses();
            documentStatusCombobox.DataSource = documentStatuses;
            documentStatusCombobox.DisplayMember = "documentstatustext";
            documentStatusCombobox.ValueMember = "documentstatusid";

            documentNameLabel.Text = memo.DocumentName;

            LoadDocumentValuesFromMemo();

            bool enable = (memo.GeneratedDocID == -1 ? true : false);

            memo.HasUnsavedChanges = false;
            saveAndCloseButton.Enabled = enable;
            saveButton.Enabled = enable;
            memo.HasUnsavedChanges = false;

            enableExportButton = WordInterop.WordExists;
            exportFormButton.Enabled = enableExportButton;

            statusLabel.Text = "";

            isSaving = false;


            SetAllTextboxEnterCallbacks(this);

            organizationNameTextbox.Focus();
            lastControl = organizationNameTextbox;
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

        private void LoadDocumentValuesFromMemo()
        {
            dateTimePicker1.Value = memo.Date;
            documentStatusCombobox.SelectedValue = (int)memo.Status;

            organizationNameTextbox.Text = memo.OrganizationName;
            organizationStAddrTextbox.Text = memo.OrganizationStreetAddress;
            organizationCityStZipTextbox.Text = memo.OrganizationCityStZip;

            recipientTextbox.Text = memo.MemorandumForLine;

            subjectTextbox.Text = memo.Subject;

            bodyRichTextBox.Text = memo.Body;

            senderNameTextbox.Text = memo.SenderName;
            senderRankTextbox.Text = memo.SenderRank;
            senderTitleTextbox.Text = memo.SenderTitle;

            distributionRichTextbox.Text = memo.Distribution;
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            memo.HasUnsavedChanges = true;
            RefreshSaveButtons();
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            if (memo.HasUnsavedChanges)
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

        private void SaveDialogValuesToMemo()
        {
            memo.Date = dateTimePicker1.Value;

            memo.Status = (DocumentStatus)(Convert.ToInt32(documentStatusCombobox.SelectedValue));

            memo.OrganizationName = organizationNameTextbox.Text;
            memo.OrganizationStreetAddress = organizationStAddrTextbox.Text;
            memo.OrganizationCityStZip = organizationCityStZipTextbox.Text;

            memo.MemorandumForLine = recipientTextbox.Text;

            memo.Subject = subjectTextbox.Text;

            memo.Body = bodyRichTextBox.Text;

            memo.SenderName = senderNameTextbox.Text;
            memo.SenderRank = senderRankTextbox.Text;
            memo.SenderTitle = senderTitleTextbox.Text;

            memo.Distribution = distributionRichTextbox.Text;

        }

        private void SaveMemo()
        {
            try
            {
                isSaving = true;

                DisableButtons();
                statusLabel.Text = "";

                SaveDialogValuesToMemo();
                memo.Status = (DocumentStatus)Convert.ToInt32(documentStatusCombobox.SelectedValue);

                saveWorker = new BackgroundWorker();
                saveWorker.DoWork += new DoWorkEventHandler(SaveMemoBackgroud);
                saveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostSavedMemo);
                saveWorker.ProgressChanged += new ProgressChangedEventHandler(SaveProgressUpdated);
                saveWorker.WorkerSupportsCancellation = true;
                saveWorker.WorkerReportsProgress = true;

                memo.HasUnsavedChanges = false;

                progressBar1.Show();
                saveWorker.RunWorkerAsync();

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
            if (this.IsDisposed)
                return;

            progressBar1.Value = e.ProgressPercentage;
        }

        void SaveMemoBackgroud(object sender, DoWorkEventArgs e)
        {
            memo.Save(saveWorker);
        }

        void PostSavedMemo(object sender, RunWorkerCompletedEventArgs e)
        {
            isSaving = false;

            if (closeAfterSaving)
            {
                SaveLocation();
                this.Dispose();
            }

            progressBar1.Value = 0;
            progressBar1.Hide();

            EnableButtons();

            if (postSaveStatusLabelWorker != null && postSaveStatusLabelWorker.IsBusy)
                postSaveStatusLabelWorker.CancelAsync();

            postSaveStatusLabelWorker = new BackgroundWorker();
            postSaveStatusLabelWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ClearStatusLabel);
            postSaveStatusLabelWorker.DoWork += new DoWorkEventHandler(SleepThreeSeconds);
            postSaveStatusLabelWorker.WorkerSupportsCancellation = true;
            postSaveStatusLabelWorker.RunWorkerAsync();
        }

        private void DisableButtons()
        {
            saveButton.Enabled = false;
            saveAndCloseButton.Enabled = false;
            exportFormButton.Enabled = false;
        }

        private void EnableButtons()
        {
            exportFormButton.Enabled = enableExportButton;

            if (memo.HasUnsavedChanges)
            {
                saveButton.Enabled = true;
                saveAndCloseButton.Enabled = true;
            }
        }

        private void RefreshSaveButtons()
        {
            if (saveWorker.IsBusy)
                return;

            if (memo.HasUnsavedChanges)
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
            SaveMemo();
            considerSaving = false;
            closeAfterSaving = true;
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
            SaveMemo();

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

        private void GenericMemoEditorStepTwo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaving)
            {
                e.Cancel = true;
                return;
            }

            if (considerSaving == true && memo.HasUnsavedChanges)
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
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.GenericMemoEditorStepTwoSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.GenericMemoEditorStepTwoSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.GenericMemoEditorStepTwoSize = bounds.Size;
            Properties.Settings.Default.GenericMemoEditorStepTwoLocation = bounds.Location;
            Properties.Settings.Default.GenericMemoEditorStepTwoHeight = bounds.Height;
            Properties.Settings.Default.GenericMemoEditorStepTwoWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();

        }

        private void exportFormButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDialogValuesToMemo();
                DocumentExportController.ExportDocument(memo);
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

        private void GenericMemoEditorStepTwo_Load(object sender, EventArgs e)
        {
            PutAtSavedLocation();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.GenericMemoEditorStepTwoSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.GenericMemoEditorStepTwoLocation, Properties.Settings.Default.GenericMemoEditorStepTwoSize);
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
    }
}
