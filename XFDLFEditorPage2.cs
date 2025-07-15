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
    public partial class XFDLEditorPage2 : Form
    {
        XFDLEditorPage1 page1;
        DA4856Document document;
        Template template;
        bool considerSaving = true;

        BackgroundWorker saveWorker;
        BackgroundWorker postSaveStatusLabelWorker;
        bool isSaving;
        bool closeAfterSaving;

        bool planOfActionOverflow = false;
        bool sessionClosingOverflow = false;
        bool leaderResponsibilitiesOverflow = false;

        internal XFDLEditorPage2(XFDLEditorPage1 page1, DA4856Document document, Template template)
        {
            InitializeComponent();

            this.page1 = page1;
            this.document = document;
            this.template = template;
            XFDLEditorPage2_Load(null, null);
        }

        void XFDLEditorPage2_Load(object sender, EventArgs e)
        {
            bool unsavedChanges = document.HasUnsavedDocumentChanges;

            isSaving = false;

            InitializeLinkLabels();

            saveWorker = new BackgroundWorker();
            postSaveStatusLabelWorker = new BackgroundWorker();
            InitializeForm();

            document.HasUnsavedDocumentChanges = unsavedChanges;
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }

        void InitializeForm()
        {
            if (DesignMode)
                return;

            titleLabel.Text = document.DocumentName;

            planOfActionTextbox.Text = document.PlanOfActions;
            iAgreeCheckbox.Checked = document.IAgree;
            iDisagreeCheckbox.Checked = document.IDisagree;
            sessionClosingTextbox.Text = document.SessionClosing;
            leaderResponsibilitiesTextbox.Text = document.LeaderResponsibilities;
            counselorTextbox.Text = document.Counselor;
            individualCounseledTextbox.Text = document.IndividualCounseled;

            if (document.DateAssessmentDue.Ticks != 0)
                dateOfAssessmentDateTimePicker.Value = document.DateAssessmentDue;

            DataTable statuses = DocumentModel.GetDocumentStatuses();
            statusComboBox.DataSource = statuses;
            statusComboBox.ValueMember = "documentstatusid";
            statusComboBox.DisplayMember = "documentstatustext";
            statusComboBox.SelectedValue = document.Status;

            planOfActionTextbox_Leave(null, null);
            sessionClosingTextbox_Leave(null, null);
            leaderResponsibilitiesTextbox_Leave(null, null);

            if (document.GeneratedDocID == -1)
            {
                saveButton.Enabled = true;
                saveAndCloseButton.Enabled = true;
            }
        }


        private void InitializeLinkLabels()
        {
            if (!template.TemplateValues.Keys.Contains("Plan of Actions")
                || template.TemplateValues["Plan of Actions"].Count < 2)
                planOfActionLink.Enabled = false;
            if (!template.TemplateValues.Keys.Contains("Session Closing")
                || template.TemplateValues["Session Closing"].Count < 2)
                sessionClosingLink.Enabled = false;
            if (!template.TemplateValues.Keys.Contains("Leader Responsibilities")
                || template.TemplateValues["Leader Responsibilities"].Count < 2)
                leaderResponsibilitiesLink.Enabled = false;
        }


        public void LoadFormValues()
        {

        }


        internal void UpdateDocumentStatus()
        {
            statusComboBox.SelectedValue = document.Status;
        }


        private void OnBackButtonClick(object sender, EventArgs e)
        {
            document.Status = (DocumentStatus)Convert.ToInt32(statusComboBox.SelectedValue);
            this.Hide();
            page1.Refresh();
            page1.Show();
        }


        public void OnChangedValue(object sender, EventArgs e)
        {
            document.HasUnsavedDocumentChanges = true;
            RefreshSaveButtons();
            UpdateStatusLabel();
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


        private void UpdateStatusLabel()
        {
            if (document.HasUnsavedDocumentChanges || document.HasUnsavedAssessmentChanges)
                statusLabel.Text = "Unsaved changes.";
            else
                statusLabel.Text = "Saved successfully.";
        }


        private void iAgreeCheckbox_Click(object sender, EventArgs e)
        {
            iDisagreeCheckbox.Checked = false;
            OnChangedValue(null, null);
        }


        private void iDisagreeCheckbox_Click(object sender, EventArgs e)
        {
            iAgreeCheckbox.Checked = false;
            OnChangedValue(null, null);
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            SaveAndClose();
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
                page1.SaveDialogValuesToDocument();
                document.Status = (DocumentStatus)Convert.ToInt32(statusComboBox.SelectedValue);

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
                Logger.Error(ex);

                CQPMessageBox.Show("There was an error while saving the document.", "Error Saving",
                                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                throw ex;
            }
        }


        private void DisableButtons()
        {
            saveButton.Enabled = false;
            saveAndCloseButton.Enabled = false;
            exportToLotusFormsButton.Enabled = false;
        }

        private void EnableButtons()
        {
            exportToLotusFormsButton.Enabled = true;

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
            considerSaving = false;

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

        internal void SaveDialogValuesToDocument()
        {
            document.PlanOfActions = planOfActionTextbox.Text;
            document.IAgree = iAgreeCheckbox.Checked;
            document.IDisagree = iDisagreeCheckbox.Checked;
            document.SessionClosing = sessionClosingTextbox.Text;
            document.LeaderResponsibilities = leaderResponsibilitiesTextbox.Text;
            document.Counselor = counselorTextbox.Text;
            document.IndividualCounseled = individualCounseledTextbox.Text;
            document.DateAssessmentDue = dateOfAssessmentDateTimePicker.Value;
        }

        private void XFDLEditorPage2_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (isSaving)
            {
                System.Diagnostics.Debug.WriteLine("waiting on worker...\n");
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
                    this.Dispose();
                }
            }
            //Added to save the forms location so it can be restored mdail 8-19-19
            SaveLocation();
        }

        private DialogResult PromptToSaveChanges()
        {
            return DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);
        }

        private void createAssessmentButton_Click(object sender, EventArgs e)
        {
            AssessmentDialog assessmentDialog = new AssessmentDialog(document, template);
            assessmentDialog.ShowDialog();
            UpdateStatusLabel();
        }

        private bool ValidateFormFieldsOkay()
        {
            if (FormFieldOverflowExists() || page1.FormFieldOverflowExists())
            {
                List<String> fieldNames = GetOverflowFieldNames();
                fieldNames.AddRange(page1.GetOverflowFieldNames());

                string message = "The following fields contain too much text and will most likely not be completely visible:\n"
                 + "$FIELDS\n"
                    //+ "Would you like the application to attempt to automatically generate a Continuation of Counseling.\n"
                 + "Would you like the application to attempt to automatically generate a Continuation of Counseling for you?";

                string caption = "Overflow Waring";
                CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNoCancel;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Warning;
                List<string> buttonText = new List<string> { "Generate and Export", "Export Anyway", "Cancel" };

                string replacementText = "";
                foreach (string fieldName in fieldNames)
                {
                    replacementText += "-    " + fieldName + "\n";
                }

                message = message.Replace("$FIELDS", replacementText);

                DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icon);
                if (result == DialogResult.Yes)
                {
                    if (AutomaticCounselingPossible() && page1.AutomaticContinuationPossible())
                    {
                        if (document.ContinuationText != "")
                        {
                            string continuationParseQuestion = "Should the current continuation be overwritten?";
                            string continuationParseCaption = "Overwrite existing continuation?";
                            CQPMessageBox.CQPMessageBoxButtons overwriteButtons = CQPMessageBox.CQPMessageBoxButtons.YesNoCancel;
                            List<string> overwriteButtonsText = new List<string>() { "Overwrite Continuation", "Append To Continuation", "Cancel" };
                            CQPMessageBox.CQPMessageBoxIcon continuationParseIcon = CQPMessageBox.CQPMessageBoxIcon.Question;

                            DialogResult overwriteResult
                                = CQPMessageBox.Show(continuationParseQuestion, continuationParseCaption, overwriteButtons, overwriteButtonsText, continuationParseIcon);

                            if (overwriteResult == DialogResult.Cancel)
                                return false;
                            if (overwriteResult == DialogResult.Yes)
                                document.ContinuationText = "";
                        }

                        return page1.AttemptGenerateContinuation() & AttemptGenerateContinuation();
                    }
                }
                if (result == DialogResult.No)
                    return false;

                return true;
            }
            return true;
        }

        internal bool AutomaticCounselingPossible()
        {
            bool success = true;

            string planOfActionMainText = planOfActionTextbox.Text;
            string planOfActionContinuation = "";

            string sessionClosingMainText = sessionClosingTextbox.Text;
            string sessionClosingContinuation = "";

            string leaderResponsibilitiesMainText = leaderResponsibilitiesTextbox.Text;
            string leaderResponsibilitiesContinuation = "";

            string failureTextbox = "";

            if (!DialogHelper.TextFitsInTextbox(planOfActionTextbox, new Size(860, 385)))
            {
                success &= DialogHelper.SplitText(ref planOfActionMainText,
                                                    ref planOfActionContinuation, new Size(860, 385));

                if (!success)
                    failureTextbox = "Purpose of Counseling";
            }

            if (success && !DialogHelper.TextFitsInTextbox(sessionClosingTextbox, new Size(858, 125)))
            {
                success &= DialogHelper.SplitText(ref sessionClosingMainText,
                                                    ref sessionClosingContinuation, new Size(858, 125));

                if (!success)
                    failureTextbox = "Key Points of Discussion";
            }


            if (success && !DialogHelper.TextFitsInTextbox(leaderResponsibilitiesTextbox, new Size(850, 98)))
            {
                success &= DialogHelper.SplitText(ref leaderResponsibilitiesMainText,
                                                    ref leaderResponsibilitiesContinuation, new Size(850, 98));

                if (!success)
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
            string continuationText = "";

            string planOfActionMainText = planOfActionTextbox.Text;
            string planOfActionContinuation = "";

            string sessionClosingMainText = sessionClosingTextbox.Text;
            string sessionClosingContinuation = "";

            string leaderResponsibilitiesMainText = leaderResponsibilitiesTextbox.Text;
            string leaderResponsibilitiesContinuation = "";

            if (!DialogHelper.TextFitsInTextbox(planOfActionTextbox, new Size(860, 385)))
            {
                DialogHelper.SplitText(ref planOfActionMainText,
                                                    ref planOfActionContinuation, new Size(860, 385));
            }

            if (!DialogHelper.TextFitsInTextbox(sessionClosingTextbox, new Size(858, 125)))
            {
                DialogHelper.SplitText(ref sessionClosingMainText, 
                                                    ref sessionClosingContinuation, new Size(858, 125));
            }


            if (!DialogHelper.TextFitsInTextbox(leaderResponsibilitiesTextbox, new Size(850, 98)))
            {
                DialogHelper.SplitText(ref leaderResponsibilitiesMainText, 
                                                    ref leaderResponsibilitiesContinuation, new Size(850, 98));
            }


            planOfActionTextbox.Text = planOfActionMainText;

            if (planOfActionContinuation != "")
                continuationText += "PLAN OF ACTION (Cont.)\n\n" + planOfActionContinuation + "\n\n";

            sessionClosingTextbox.Text = sessionClosingMainText;

            if (sessionClosingContinuation != "")
                continuationText += "SESSION CLOSING (Cont.)\n\n" + sessionClosingContinuation + "\n\n";

            leaderResponsibilitiesTextbox.Text = leaderResponsibilitiesMainText;

            if (leaderResponsibilitiesContinuation != "")
                continuationText += "LEADER RESPONSIBILITIES (Cont.)\n\n" + leaderResponsibilitiesContinuation + "\n\n";

            if (document.ContinuationText.Length > 0)
                document.ContinuationText += "\n\n";

            document.ContinuationText += continuationText;
            return true;
        }

        private void exportToLotusFormsButton_Click(object sender, EventArgs e)
        {
            if (!ValidateFormFieldsOkay())
                return;

            SaveDialogValuesToDocument();
            page1.SaveDialogValuesToDocument();
            DocumentExportController.ExportDocument(document);
        }
        private void discardChangesButton_Click(object sender, EventArgs e)
        {
            considerSaving = false;
            SaveLocation();
            this.Dispose();
        }

        private void XFDLEditorPage2_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
                considerSaving = false;
            else
                considerSaving = true;

            if (Visible)
            {
                this.Size = page1.Size;
                this.WindowState = page1.WindowState;
                this.Location = page1.Location;
            }
            
            if (document.HasUnsavedAssessmentChanges || document.HasUnsavedDocumentChanges)
            {
                statusLabel.Text = "Unsaved changes.";

            }
            else
                statusLabel.Text = "";

            // we must revert the changes flag because toggling the document status dropdown will change it
            bool hasUnsavedChanges = document.HasUnsavedDocumentChanges;
            UpdateDocumentStatus();
            document.HasUnsavedDocumentChanges = hasUnsavedChanges;
        }

        private void planOfActionTextbox_Leave(object sender, EventArgs e)
        {
            if (!DialogHelper.TextFitsInTextbox(planOfActionTextbox, new Size(860, 385)))
            {
                planOfActionOverflow = true;
                planOfActionLabel.ForeColor = Color.Red;
                planOfActionLabel.Text = "Plan of Action - (This field contains too much text - use continuation of counseling.)";
            }
            else
            {
                planOfActionOverflow = false;
                planOfActionLabel.ForeColor = Color.Black;
                planOfActionLabel.Text = "Plan of Action";
            }
            OnChangedValue(null, null);
        }

        private void sessionClosingTextbox_Leave(object sender, EventArgs e)
        {
            if (!DialogHelper.TextFitsInTextbox(sessionClosingTextbox, new Size(858, 125)))
            {
                sessionClosingOverflow = true;
                sessionClosingLabel.ForeColor = Color.Red;
                sessionClosingLabel.Text = "Session Closing - (This field contains too much text - use continuation of counseling.)";
            }
            else
            {
                sessionClosingOverflow = false;
                sessionClosingLabel.ForeColor = Color.Black;
                sessionClosingLabel.Text = "Session Closing ";
            }
            OnChangedValue(null, null);
        }

        private void leaderResponsibilitiesTextbox_Leave(object sender, EventArgs e)
        {
            if (!DialogHelper.TextFitsInTextbox(leaderResponsibilitiesTextbox, new Size(850, 98)))
            {
                leaderResponsibilitiesOverflow = true;
                leaderResponsibilitiesLabel.ForeColor = Color.Red;
                leaderResponsibilitiesLabel.Text = "Leader Responsibilities - (This field contains too much text - use continuation of counseling.)";
            }
            else
            {
                leaderResponsibilitiesOverflow = false;
                leaderResponsibilitiesLabel.ForeColor = Color.Black;
                leaderResponsibilitiesLabel.Text = "Leader Responsibilities";
            }
            OnChangedValue(null, null);
        }

        internal List<string> GetOverflowFieldNames()
        {
            List<string> overflowFieldNames = new List<string>();
            if (planOfActionOverflow)
                overflowFieldNames.Add("Plan of Action");
            if (sessionClosingOverflow)
                overflowFieldNames.Add("Session Closing");
            if (leaderResponsibilitiesOverflow)
                overflowFieldNames.Add("Leader Responsibilities");

            return overflowFieldNames;
        }

        internal bool FormFieldOverflowExists()
        {
            bool formFieldOverflowExists = false;

            formFieldOverflowExists |= planOfActionOverflow;
            formFieldOverflowExists |= sessionClosingOverflow;
            formFieldOverflowExists |= leaderResponsibilitiesOverflow;
            return formFieldOverflowExists;
        }

        private void PlanOfActionLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogHelper.PromptToChooseTemplateValues(template, "Plan of Actions", planOfActionTextbox);
        }

        private void SessionClosingLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogHelper.PromptToChooseTemplateValues(template, "Session Closing", sessionClosingTextbox);
        }

        private void LeaderResponsibiliesLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogHelper.PromptToChooseTemplateValues(template, "Leader Responsibilities", leaderResponsibilitiesTextbox);
        }

        private void continuationButton_Click(object sender, EventArgs e)
        {
            SaveDialogValuesToDocument();
            page1.SaveDialogValuesToDocument();

            ContinuationOfCounselingDialog dialog = new ContinuationOfCounselingDialog(document);
            dialog.ShowDialog(this);
        }

        private void planOfActionTextbox_VisibleChanged(object sender, EventArgs e)
        {
            //planOfActionTextbox.Select(0,0);
            planOfActionTextbox.Focus();
        }
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.XFDLEditorPage2SavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.XFDLEditorPage2SavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.XFDLEditorPage2Size = bounds.Size;
            Properties.Settings.Default.XFDLEditorPage2Location = bounds.Location;
            Properties.Settings.Default.XFDLEditorPage2Height = bounds.Height;
            Properties.Settings.Default.XFDLEditorPage2Width = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.XFDLEditorPage2SavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.XFDLEditorPage2Location, Properties.Settings.Default.XFDLEditorPage2Size);
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
