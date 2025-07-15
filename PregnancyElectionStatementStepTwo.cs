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
    public partial class PregnancyElectionStatementStepTwo : Form
    {
        private PregnancyElectionStatementMemo memo;
        private Template template;
        bool considerSaving;

        BackgroundWorker saveWorker;
        BackgroundWorker postSaveStatusLabelWorker;
        bool isSaving;
        bool closeAfterSaving;

        Control lastControl;

        PregnancyElectionStatementEditorStepOne pageOne;
        
        internal 
            PregnancyElectionStatementStepTwo(PregnancyElectionStatementEditorStepOne pageOne,
                                                PregnancyElectionStatementMemo memo, Template template)
        {
            this.memo = memo;
            this.template = template;

            this.pageOne = pageOne;
            InitializeComponent();
            OnLoad();

            considerSaving = true;
        }

        void OnLoad()
        {
            if (DesignMode)
                return;

            saveWorker = new BackgroundWorker();
            postSaveStatusLabelWorker = new BackgroundWorker();

            LoadDocumentValuesFromMemo();

            bool enable = (memo.GeneratedDocID == -1 ? true : false);

            saveAndCloseButton.Enabled = enable;
            saveButton.Enabled = enable;
            memo.HasUnsavedChanges = false;

            statusLabel.Text = "";

            isSaving = false;

            SetAllTextboxEnterCallbacks(this);

            toNameOfSoldierTextbox.Focus();
            lastControl = toNameOfSoldierTextbox;

            ElectionResponsePanel.Visible = false;
            ElectionRequestPanel.Visible = true;

            cqpGraphicsButton1.Enabled = false;
            
            tableLayoutPanel3.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel3.RowStyles[1].Height = 0;

            tableLayoutPanel3.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel3.RowStyles[0].Height = 100;

            tableLayoutPanel3.PerformLayout();
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.PregnancyElectionStatementStepTwoSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.PregnancyElectionStatementStepTwoLocation, Properties.Settings.Default.PregnancyElectionStatementStepTwoSize);
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
            toNameOfSoldierTextbox.Text = memo.ToPrintedNameOfSoldier;
            fromNameOfCommanderTextbox.Text = memo.FromNameOfCommander;
            fromNameOfUnitTextbox.Text = memo.FromNameOfUnit;

            electionRequestTextbox.Text = memo.PostSoldierSignature1;

            firstSignatureCommanderNameTextbox.Text = memo.FirstSignatureBlockFromCommandersName;
            firstSignatureCommanderRankBranch.Text = memo.FirstSignatureBlockFromRankBranch;
            firstSignatureCommanderTitle.Text = memo.FirstSignatureBlockFromCommanderTitle;

            toNameOfCommander.Text = memo.ToCommandersName;
            toNameOfUnit.Text = memo.ToNameOfUnit;
            fromNameOfSoldier.Text = memo.FromNameOfSoldier;

            electionDecisionTextbox.Text = memo.PostSoldierSignature2;

            secondSignatureSoldiersNameTextbox.Text = memo.SecondSignatureBlockSoldiersName;
            secondSignatureRankSSNTextbox.Text = memo.SecondSignatureBlockRankSSN;
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
                statusLabel.Text = "Unsaved changes";
            }
            else
            {
                statusLabel.Text = "";
            }
        }

        internal void SaveDialogValuesToMemo()
        {
            memo.ToPrintedNameOfSoldier = toNameOfSoldierTextbox.Text;
            memo.FromNameOfCommander = fromNameOfCommanderTextbox.Text;
            memo.FromNameOfUnit = fromNameOfUnitTextbox.Text;

            memo.PostSoldierSignature1 = electionRequestTextbox.Text;

            memo.FirstSignatureBlockFromCommandersName = firstSignatureCommanderNameTextbox.Text;
            memo.FirstSignatureBlockFromRankBranch = firstSignatureCommanderRankBranch.Text;
            memo.FirstSignatureBlockFromCommanderTitle = firstSignatureCommanderTitle.Text;

            memo.ToCommandersName = toNameOfCommander.Text;
            memo.ToNameOfUnit = toNameOfUnit.Text;
            memo.FromNameOfSoldier = fromNameOfSoldier.Text;

            memo.PostSoldierSignature2 = electionDecisionTextbox.Text;

            memo.SecondSignatureBlockSoldiersName = secondSignatureSoldiersNameTextbox.Text;
            memo.SecondSignatureBlockRankSSN = secondSignatureRankSSNTextbox.Text;
        }

        private void SaveMemo()
        {
            try
            {
                isSaving = true;

                DisableButtons();
                statusLabel.Text = "";

                SaveDialogValuesToMemo();
                pageOne.SaveDialogValuesToMemo();

                saveWorker = new BackgroundWorker();
                saveWorker.DoWork += new DoWorkEventHandler(SaveMemoBackgroud);
                saveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostSavedMemo);
                saveWorker.ProgressChanged += new ProgressChangedEventHandler(SaveProgressUpdated);
                saveWorker.WorkerSupportsCancellation = true;
                saveWorker.WorkerReportsProgress = true;

                memo.HasUnsavedChanges = false;

                saveWorker.RunWorkerAsync();

            }
            catch (DataStoreFailedException)
            {
                CQPMessageBox.Show("There was an error while saving the document.", "Error Saving",
                                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
            }
        }

        void SaveProgressUpdated(object sender, ProgressChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Percent: " + e.ProgressPercentage);

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
            backButton.Enabled = false;
            saveButton.Enabled = false;
            saveAndCloseButton.Enabled = false;
            exportFormButton.Enabled = false;
        }

        private void EnableButtons()
        {
            backButton.Enabled = true;
            exportFormButton.Enabled = true;

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

        private void PregnancyElectionStatementFormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaving)
            {
                System.Diagnostics.Debug.WriteLine("waiting on worker...\n");
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
            //Added to save the forms location so it can be restored mdail 8-19-19
            SaveLocation();
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

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            pageOne.Show();
        }

        private void PregnancyElectionStatementStepTwoVisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                considerSaving = false;
                return;
            }

            considerSaving = true;
            RefreshSaveButtons();
            RefreshStatus();
        }

        private void cqpGraphicsButton1_Click(object sender, EventArgs e)
        {
            ElectionResponsePanel.Visible = false;
            ElectionRequestPanel.Visible = true;

            cqpGraphicsButton1.Enabled = false;
            cqpGraphicsButton2.Enabled = true;

            tableLayoutPanel3.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel3.RowStyles[1].Height = 0;

            tableLayoutPanel3.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel3.RowStyles[0].Height = 100;

            tableLayoutPanel3.PerformLayout();

            toNameOfSoldierTextbox.Focus();
        }

        private void cqpGraphicsButton2_Click(object sender, EventArgs e)
        {
            ElectionRequestPanel.Visible = false;
            ElectionResponsePanel.Visible = true;

            cqpGraphicsButton1.Enabled = true;
            cqpGraphicsButton2.Enabled = false;

            tableLayoutPanel3.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel3.RowStyles[0].Height = 0;

            tableLayoutPanel3.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel3.RowStyles[1].Height = 100;

            tableLayoutPanel3.PerformLayout();

            toNameOfCommander.Focus();
        }
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.PregnancyElectionStatementStepTwoSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.PregnancyElectionStatementStepTwoSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.PregnancyElectionStatementStepTwoSize = bounds.Size;
            Properties.Settings.Default.PregnancyElectionStatementStepTwoLocation = bounds.Location;
            Properties.Settings.Default.PregnancyElectionStatementStepTwoHeight = bounds.Height;
            Properties.Settings.Default.PregnancyElectionStatementStepTwoWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
    }
}
