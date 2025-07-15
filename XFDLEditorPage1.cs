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
    public partial class XFDLEditorPage1 : Form
    {
        XFDLEditorPage2 page2;
        DA4856Document document;
        Template template;

        BackgroundWorker saveWorker;
        BackgroundWorker postSaveStatusLabelWorker;
        bool isSaving;
        bool closeAfterSaving;

        bool considerSaving;

        bool purposeOfCounselingOverflow = false;
        bool keyPointsOfDiscussionOverflow = false;

        Control lastControl;

        bool firstShown;

        enum ExportMode
        {
            Export,
            ReduceAndExport,
            Cancel
        }

        internal XFDLEditorPage1(DA4856Document document, Template template)
        {
            this.document = document;
            this.template = template;

            InitializeComponent();

            isSaving = false;

            Load += new EventHandler(XFDLEditorPage1_Load);

            page2 = new XFDLEditorPage2(this, document, template);
        }

        void XFDLEditorPage1_Load(object sender, EventArgs e)
        {
            bool unsavedChanges = document.HasUnsavedDocumentChanges;

            nameTextbox.Focus();
            firstShown = true;
            
            saveWorker = new BackgroundWorker();
            postSaveStatusLabelWorker = new BackgroundWorker();

            InitializeForm();

            document.HasUnsavedDocumentChanges = unsavedChanges;
            PutAtSavedLocation();
        }

        void InitializeForm()
        {
            if (DesignMode)
                return;
            
            LoadComboBoxValues();
            LoadTemplatePopupValues();
            LoadFormValues();
            CheckTextboxesForOverflow();

            statusLabel.Text = "";
            considerSaving = true;

            InitializeSaveButtonsAndStatus();

            SetAllTextboxEnterCallbacks(this);

            lastControl = nameTextbox;
            nameTextbox.Focus();
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

        private void LoadComboBoxValues()
        {
            DataTable rankings = RankingModel.GetRankingTable();
            DataTable documentStatuses = DocumentModel.GetDocumentStatuses();

            rankComboBox.DataSource = rankings;
            rankComboBox.DisplayMember = "rankingabbreviation";
            rankComboBox.ValueMember = "rankingid";

            documentStatusComboBox.DataSource = documentStatuses;
            documentStatusComboBox.DisplayMember = "documentstatustext";
            documentStatusComboBox.ValueMember = "documentstatusid";
        }

        private void InitializeSaveButtonsAndStatus()
        {
            if (document.GeneratedDocID == -1)
            {
                saveButton.Enabled = true;
                saveAndCloseButton.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                saveAndCloseButton.Enabled = false;
            }

            statusLabel.Text = "";
        }

        private void LoadTemplatePopupValues()
        {
            if (!template.TemplateValues.Keys.Contains("Purpose of Counseling")
                || template.TemplateValues["Purpose of Counseling"].Count < 2)
                purposeOfCounselingLink.Enabled = false;
            if (!template.TemplateValues.Keys.Contains("Key Points of Discussion")
                || template.TemplateValues["Key Points of Discussion"].Count < 2)
                keyPointsOfDiscussionLink.Enabled = false;
        }

        private void CheckTextboxesForOverflow()
        {
            purposeOfCounselingUpdated(null, null);
            keyPointsOfDiscussionTextbox_TextChanged(null, null);
        }

        public void OnChangedValue(object sender, EventArgs e)
        {
            document.HasUnsavedDocumentChanges = true;
            RefreshSaveButtons();
            RefreshSaveStatus();
        }

        private void RefreshSaveButtons()
        {
            if (saveWorker.IsBusy)
                return;

            if (document.HasUnsavedDocumentChanges 
                || document.HasUnsavedAssessmentChanges
                || document.HasUnsavedContinuationChanges)
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

        private void RefreshSaveStatus()
        {
            if (document.HasUnsavedDocumentChanges
                || document.HasUnsavedAssessmentChanges
                || document.HasUnsavedContinuationChanges)
            {
                statusLabel.Text = "Unsaved changes";
            }
            else
            {
                statusLabel.Text = "";
            }
        }

        public void LoadFormValues()
        {
            titleLabel.Text = document.DocumentName;
            
            nameTextbox.Text = document.Name;

            rankComboBox.SelectedValue = ((int)document.Rank);

            if (document.Date.Ticks != 0)
                dateOfCounselingDateTimePicker.Value = document.Date;
            else
                dateOfCounselingDateTimePicker.Value = DateTime.Now;

            organizationTextbox.Text = document.NameOfOrganization;
            nameAndTitleOfCounselorTextbox.Text = document.NameAndTitleOfCounselor;

            purposeOfCounselingTextbox.Text = document.PurposeOfCounseling;
            purposeOfCounselingUpdated(null, null);

            keyPointsOfDiscussionTextbox.Text = document.KeyPointsOfDiscussion;

            documentStatusComboBox.SelectedValue = document.Status;
        }

        internal void SaveDialogValuesToDocument()
        {
            titleLabel.Text = document.DocumentName;

            document.Name = nameTextbox.Text;
            int rankingID = Convert.ToInt32(rankComboBox.SelectedValue);

            if (rankingID == 0)
                rankingID = -1;

            document.Rank = (Ranking)rankingID;
            document.Date = dateOfCounselingDateTimePicker.Value;

            document.NameOfOrganization = organizationTextbox.Text;
            document.NameAndTitleOfCounselor = nameAndTitleOfCounselorTextbox.Text;
            
            document.NameGradeCounselor = nameAndTitleOfCounselorTextbox.Text;
            document.NameGradeCounselee = document.Name + ", " + RankingModel.RankingAbbreviationFromEnum(document.Rank);
            document.ContinuationDate1 = document.Date;
            document.ContinuationDate2 = document.Date;

            document.PurposeOfCounseling = purposeOfCounselingTextbox.Text;
            document.KeyPointsOfDiscussion = keyPointsOfDiscussionTextbox.Text;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            document.Status = (DocumentStatus)Convert.ToInt32(documentStatusComboBox.SelectedValue);
            this.Hide();
            page2.ShowDialog(this);
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            SaveAndClose();
            lastControl.Focus();
        }

        private void SaveAndClose()
        {
            SaveDocument();
            considerSaving = false;
            closeAfterSaving = true;
        }

        private void SaveDocument()
        {
            try
            {
                isSaving = true;

                DisableButtons();
                statusLabel.Text = "";

                SaveDialogValuesToDocument();
                page2.SaveDialogValuesToDocument();
                document.Status = (DocumentStatus)Convert.ToInt32(documentStatusComboBox.SelectedValue);

                saveWorker = new BackgroundWorker();
                saveWorker.DoWork += new DoWorkEventHandler(SaveDocumentBackgroud);
                saveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostSavedDocument);
                saveWorker.ProgressChanged += new ProgressChangedEventHandler(SaveProgressUpdated);
                saveWorker.WorkerSupportsCancellation = true;
                saveWorker.WorkerReportsProgress = true;

                document.HasUnsavedDocumentChanges = false;
                document.HasUnsavedAssessmentChanges = false;
                document.HasUnsavedContinuationChanges = false;

                progressBar1.Show();
                saveWorker.RunWorkerAsync();
            }
            catch (DataStoreFailedException ex)
            {
                progressBar1.Show();

                CQPMessageBox.Show("There was an error while saving the document.", "Error Saving", 
                                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                throw ex;
            }
        }

        private void DisableButtons()
        {
            saveButton.Enabled = false;
            saveAndCloseButton.Enabled = false;
            exportFormButton.Enabled = false;
        }

        private void EnableButtons()
        {
            exportFormButton.Enabled = true;

            if (document.HasUnsavedDocumentChanges 
                || document.HasUnsavedContinuationChanges 
                || document.HasUnsavedAssessmentChanges)
            {
                saveButton.Enabled = true;
                saveAndCloseButton.Enabled = true;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveDocument();
            lastControl.Focus();
        }
        void SaveProgressUpdated(object sender, ProgressChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Percent: " + e.ProgressPercentage);

            if (this.IsDisposed)
                return;

            progressBar1.Value = e.ProgressPercentage;
        }

        void SaveDocumentBackgroud(object sender, DoWorkEventArgs e)
        {
            document.Save(saveWorker);
        }
        
        void PostSavedDocument(object sender, RunWorkerCompletedEventArgs e)
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

        void ClearStatusLabel(object sender, RunWorkerCompletedEventArgs e)
        {
            // make sure the request to clear hasn't been cancelled,
            // and/or the document doesn't have unsaved changes:
            // if it does we don't want to clear the status...
            if (!e.Cancelled
                && !document.HasUnsavedDocumentChanges
                && !document.HasUnsavedContinuationChanges
                && !document.HasUnsavedAssessmentChanges)
            {
                statusLabel.Text = "";
            }
        }

        void SleepThreeSeconds(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
        }

        private void continuationButton_Click(object sender, EventArgs e)
        {
            SaveDialogValuesToDocument();
            page2.SaveDialogValuesToDocument();
            ContinuationOfCounselingDialog dialog = new ContinuationOfCounselingDialog(document);
            dialog.ShowDialog(this);
            lastControl.Focus();
        }

        private void purposeOfCounselingUpdated(object sender, EventArgs e)
        {
            if (!DialogHelper.TextFitsInTextbox(purposeOfCounselingTextbox, new Size(860, 176)))
            {
                purposeOfCounselingOverflow = true;
                purposeOfCounselingLabel.ForeColor = Color.Red;
                purposeOfCounselingLabel.Text = "Purpose Of Counseling (This field contains too much text - use continuation of counseling.)";
            }
            else
            {
                purposeOfCounselingOverflow = false;
                purposeOfCounselingLabel.ForeColor = Color.Black;
                purposeOfCounselingLabel.Text = "Purpose Of Counseling";
            }
            OnChangedValue(null, null);
        }
        
        private void keyPointsOfDiscussionTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!DialogHelper.TextFitsInTextbox(keyPointsOfDiscussionTextbox, new Size(860, 543)))
            {
                keyPointsOfDiscussionOverflow = true;
                keyPointsOfDiscussion.ForeColor = Color.Red;
                keyPointsOfDiscussion.Text = "Key Points Of Discussion (This field contains too much text - use continuation of counseling.)";
            }
            else
            {
                keyPointsOfDiscussionOverflow = false;
                keyPointsOfDiscussion.ForeColor = Color.Black;
                keyPointsOfDiscussion.Text = "Key Points Of Discussion";
            }
            OnChangedValue(null, null);
        }

        private DialogResult PromptToSaveChanges()
        {
            return DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);
        }

        private void XFDLEditorPage1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(isSaving)
            {
                e.Cancel = true;
                return;
            }

            if (considerSaving == true && document.HasUnsavedDocumentChanges)
            {
                DialogResult result = PromptToSaveChanges();

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

        internal bool FormFieldOverflowExists()
        {
            bool formFieldOverflowExists = false;

            formFieldOverflowExists |= keyPointsOfDiscussionOverflow;
            formFieldOverflowExists |= purposeOfCounselingOverflow;

            return formFieldOverflowExists;
        }

        internal List<string> GetOverflowFieldNames()
        {
            List<string> overflowFieldNames = new List<string>();
            if (purposeOfCounselingOverflow)
                overflowFieldNames.Add("Purpose of Counseling");
            if (keyPointsOfDiscussionOverflow)
                overflowFieldNames.Add("Key Points Of Discussion");

            return overflowFieldNames;
        }

        private bool ValidateFormFieldsOkay()
        {
            if (FormFieldOverflowExists() || page2.FormFieldOverflowExists())
            {
                List<String> fieldNames = GetOverflowFieldNames();
                fieldNames.AddRange(page2.GetOverflowFieldNames());

                string message = "The following fields contain too much text and will most likely not be completely visible:\n"
                                + "$FIELDS\n"
                                + "Would you like the application to attempt to automatically generate a Continuation of Counseling for you?";

                string caption = "Overflow Waring";
                CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNoCancel;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Warning;
                List<string> buttonText = new List<string>{ "Generate and Export", "Export Anyway", "Cancel" };

                string replacementText = "";
                foreach (string fieldName in fieldNames)
                {
                    replacementText += "-    " + fieldName + "\n";
                }

                message = message.Replace("$FIELDS", replacementText);

                DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icon);
                
                if (result == DialogResult.Yes)
                {
                    if (AutomaticContinuationPossible() && page2.AutomaticCounselingPossible())
                    {
                        if (document.ContinuationText != "")
                        {
                            string continuationParseQuestion = "Should the current continuation be overwritten?";
                            string continuationParseCaption = "Overwrite existing continuation?";
                            CQPMessageBox.CQPMessageBoxButtons overwriteButtons = CQPMessageBox.CQPMessageBoxButtons.YesNoCancel;
                            List<string> overwriteButtonsText = new List<string>(){"Overwrite Continuation", "Append To Continuation", "Cancel"};
                            CQPMessageBox.CQPMessageBoxIcon continuationParseIcon = CQPMessageBox.CQPMessageBoxIcon.Question;

                            DialogResult overwriteResult 
                                = CQPMessageBox.Show(continuationParseQuestion, continuationParseCaption, overwriteButtons, overwriteButtonsText, continuationParseIcon);

                            if (overwriteResult == DialogResult.Cancel)
                                return false;
                            if (overwriteResult == DialogResult.Yes)
                                document.ContinuationText = "";
                        }

                        return AttemptGenerateContinuation() & page2.AttemptGenerateContinuation();
                    }
                }
                if (result == DialogResult.No)
                    return false;
                if (result == DialogResult.Cancel)
                    return false;

                return true;
            }

            return true;
        }

        internal bool AutomaticContinuationPossible()
        {
            bool success = true;

            string failureTextbox = "";

            string purposeMainText = purposeOfCounselingTextbox.Text;
            string purposeContinuation = "";

            string keyPointsMainText = keyPointsOfDiscussionTextbox.Text;
            string keyPointsContinuation = "";

            if (!DialogHelper.TextFitsInTextbox(purposeOfCounselingTextbox, new Size(860, 176)))
            {
                success &= DialogHelper.SplitText(ref purposeMainText, ref purposeContinuation, new Size(860, 176));
                if (!success)
                    failureTextbox = "Purpose of Counseling";
            }


            if (success && !DialogHelper.TextFitsInTextbox(keyPointsOfDiscussionTextbox, new Size(860, 543)))
            {
                success &= DialogHelper.SplitText(ref keyPointsMainText, ref keyPointsContinuation, new Size(860, 543));

                if (success)
                    failureTextbox = "Key Points of Discussion";
            }

            if (!success)
            {
                string error = "A Continuation of Counseling could not be automatically generated because a \n"
                                + "suitable division could not be found for: " + failureTextbox;
                string caption = "Could Not Generate Continuation";
                CQPMessageBox.CQPMessageBoxButtons button = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                CQPMessageBox.Show(error, caption, button, icon);

                return false;
            }

            return true;
        }

        internal bool AttemptGenerateContinuation()
        {
            Logger.Trace("AttemptToGenerateContinuation");

            string continuationText = "";
            
            string purposeMainText = purposeOfCounselingTextbox.Text;
            string purposeContinuation = "";

            string keyPointsMainText = keyPointsOfDiscussionTextbox.Text;
            string keyPointsContinuation = "";

            if(!DialogHelper.TextFitsInTextbox(purposeOfCounselingTextbox, new Size(860, 176)))
            {
                DialogHelper.SplitText(ref purposeMainText, ref purposeContinuation, new Size(860, 176));
            }


            if (!DialogHelper.TextFitsInTextbox(keyPointsOfDiscussionTextbox, new Size(860, 543)))
            {
                DialogHelper.SplitText(ref keyPointsMainText, ref keyPointsContinuation, new Size(860, 543));
            }
            
            purposeOfCounselingTextbox.Text = purposeMainText;

            if (purposeContinuation != "")
                continuationText += "PURPOSE OF COUNSELING (Cont.)\n\n" + purposeContinuation + "\n\n";

            keyPointsOfDiscussionTextbox.Text = keyPointsMainText;

            if (keyPointsContinuation != "")
                continuationText += "KEY POINTS OF DISCUSSION (Cont.)\n\n" + keyPointsContinuation + "\n\n";

            if (document.ContinuationText.Length > 0)
                document.ContinuationText += "\n\n";

            document.ContinuationText += continuationText;
            return true;
        }

        private void exportToLotusFormsButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("");

            if (!ValidateFormFieldsOkay())
                return;

            SaveDialogValuesToDocument();
            page2.SaveDialogValuesToDocument();

            Logger.Trace("XFDLEditorPage1 - ExportToLotusFormsButton - "
                                        + "About to export XFDL, current working directory: "
                                        + System.IO.Directory.GetCurrentDirectory());

            DocumentExportController.ExportDocument(document);
            
            lastControl.Focus();
        }

        private void discardChangesButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("");

            DialogResult result = PromptToSaveChanges();

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

        private void UpdateDocumentStatus()
        {
            Logger.Trace("" + document.Status.ToString());

            documentStatusComboBox.SelectedValue = document.Status;
        }

        internal void RefreshSelf()
        {
            Logger.Trace("HasUnsavedChanges: " + document.HasUnsavedDocumentChanges);

            // we must revert the changes flag because toggling 
            // the document status dropdown will change it
            bool hasUnsavedChanges = document.HasUnsavedDocumentChanges;
            UpdateDocumentStatus();
            document.HasUnsavedDocumentChanges = hasUnsavedChanges;            
            RefreshSaveButtons();
            RefreshSaveStatus();
        }

        private void XFDLEditorPage1_VisibleChanged(object sender, EventArgs e)
        {
            Logger.Trace("Visible: " + this.Visible);

            if (this.Visible == false)
            {
                considerSaving = false;
                return;
            }
            
            considerSaving = true;

            if (Visible)
            {
                if (firstShown)
                {
                    Logger.Trace("FirstShown: " + firstShown);

                    firstShown = false;
                    return;
                }

                this.Size = page2.Size;
                this.WindowState = page2.WindowState;
                this.Location = page2.Location;
            }
        }

        private void purposeOfCounselingAddValuesClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogHelper.PromptToChooseTemplateValues(template, "Purpose of Counseling", purposeOfCounselingTextbox);
        }

        private void KeyPointsOfDiscussionLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogHelper.PromptToChooseTemplateValues(template, "Key Points of Discussion", keyPointsOfDiscussionTextbox);
        }

        private void createAssessmentButton_Click(object sender, EventArgs e)
        {
            AssessmentDialog assessmentDialog = new AssessmentDialog(document, template);
            assessmentDialog.ShowDialog();
            lastControl.Focus();
        }

        private void purposeOfCounselingTextbox_Leave(object sender, EventArgs e)
        {
        }
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.XFDLEditorPage1SavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.XFDLEditorPage1SavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.XFDLEditorPage1Size = bounds.Size;
            Properties.Settings.Default.XFDLEditorPage1Location = bounds.Location;
            Properties.Settings.Default.XFDLEditorPage1Height = bounds.Height;
            Properties.Settings.Default.XFDLEditorPage1Width = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.XFDLEditorPage1SavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.XFDLEditorPage1Location, Properties.Settings.Default.XFDLEditorPage1Size);
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
