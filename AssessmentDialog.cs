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
    public partial class AssessmentDialog : Form
    {
        DA4856Document document;
        Template template;

        internal AssessmentDialog(DA4856Document document, Template template)
        {
            Logger.Trace("AssessmentDialog: Entered Assessment Dialog Constructor");

            InitializeComponent();
            Load += new EventHandler(AssessmentDialog_Load);
            this.document = document;
            this.template = template;
        }

        void AssessmentDialog_Load(object sender, EventArgs e)
        {
            LoadFormValues(document);

            document.HasUnsavedAssessmentChanges = false;
            dateOfAssessmentDatetime.ValueChanged += dateOfAssessmentDatetime_ValueChanged;
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }

        internal void LoadFormValues(DA4856Document document)
        {
            Logger.Trace("AssessmentDialog LoadFormValues(): Start");

            titleLabel.Text = document.DocumentName;
            
            assessmentTextbox.Text = document.Assessment;

            if (document.DateAssessmentPerformed != null && document.DateAssessmentPerformed.Ticks != 0)
                dateOfAssessmentDatetime.Value = document.DateAssessmentPerformed;
            else
                dateOfAssessmentDatetime.Value = DateTime.Now;
        }


        private void StoreDialogValuesToDocument(DA4856Document document)
        {
            Logger.Trace("AssessmentDialog: StoreDialogValues(): Start");

            document.Assessment = assessmentTextbox.Text;
            document.DateAssessmentPerformed = dateOfAssessmentDatetime.Value;
        }


        private void TrySaveDocument()
        {
            Logger.Trace("AssessmentDialog: Saving document");

            if (document.HasUnsavedAssessmentChanges == false)
                return;

            try
            {
                StoreDialogValuesToDocument(document);

                document.SaveAssessment();
                document.HasUnsavedAssessmentChanges = false;
            }
            catch (DatabaseTransactionException ex)
            {
                Logger.Error("AsssessmentDialog: Caught exception in TrySaveDocument: details: " + ex.Message, ex);

                CQPMessageBox.Show("An error occured while attempting to save the assessment.\n\nCounselor will now exit.\n\n  Error Code:  01-01");
                throw ex;
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Logger.Trace("AssessmentDialog: Discard changes");

            if (document.HasUnsavedAssessmentChanges == true)
            {
                DialogResult result = PromptToSaveAssessment();

                if (result == DialogResult.Yes)
                    TrySaveDocument();
                else if (result == DialogResult.Cancel)
                    return;
            }

            document.HasUnsavedAssessmentChanges = false;
            SaveLocation();
            this.Dispose();
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("AssessmentDialog: SaveButtonClick");
            TrySaveDocument();
        }        


        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("AssessmentDialog: SaveAndClose");
            TrySaveDocument();
            SaveLocation();
            this.Dispose();
        }


        private void assessmentTextbox_TextChanged(object sender, EventArgs e)
        {
            if (document.HasUnsavedAssessmentChanges == false)
                Logger.Trace("AssessmentDialog: textchanged, setting hasunsavedchanges to true");

            document.HasUnsavedAssessmentChanges = true;
        }


        private void dateOfAssessmentDatetime_ValueChanged(object sender, EventArgs e)
        {
            if (document.HasUnsavedAssessmentChanges == false)
                Logger.Trace("AssessmentDialog: datetime chnaged, setting hasunsavedchanges to true");

            document.HasUnsavedAssessmentChanges = true;
        }

        private void AssessmentDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.Trace("AssessmentDialog:  FormClosing");

            if (document.HasUnsavedAssessmentChanges == false)
            {
                Logger.Trace("AssessmentDialog, FormClosing:  Has no unsaved changes, returning");
                return;
            }

            Logger.Trace("AssessmentDialog:  Prompting to save");

            DialogResult dialogResult;
            dialogResult = PromptToSaveAssessment();

            Logger.Trace("AssessmentDialog: Result: " + dialogResult.ToString());

            switch (dialogResult)
            {
                case(DialogResult.Yes) :
                    TrySaveDocument();
                    break;
                case( DialogResult.Cancel ) :
                    e.Cancel = true;
                    return;
                case(DialogResult.No):
                    break;
            }
        }


        private DialogResult PromptToSaveAssessment()
        {
            Logger.Trace("AssessmentDialog: PromptToSaveAssessment");

            return DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(document.HasUnsavedAssessmentChanges == false)
            {
                Logger.Trace("AssessmentDialog:  dateTimePicker1 changed, setting hasunsavedchanges true");
            }

            document.HasUnsavedAssessmentChanges = true;
        }

        private void assessmentInsertTemplateValuesLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.Trace("AssessmentDialog:  template values link label clicked");

            DialogHelper.PromptToChooseTemplateValues(template, "Assessment", assessmentTextbox);
        }
        //put the form to its saved location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.AssessmentDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.AssessmentDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.AssessmentDialogSize = bounds.Size;
            Properties.Settings.Default.AssessmentDialogLocation = bounds.Location;
            Properties.Settings.Default.AssessmentDialogHeight = bounds.Height;
            Properties.Settings.Default.AssessmentDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.AssessmentDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.AssessmentDialogLocation, Properties.Settings.Default.AssessmentDialogSize);
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
