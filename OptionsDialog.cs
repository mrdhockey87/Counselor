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
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
            Load += new EventHandler(OptionsDialog_Load);
        }

        void OptionsDialog_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();

            OptionsModel.Refresh();

            string defaultCounselorName = OptionsModel.CounselingOptions.DefaultCounselorNameAndTitle;
            string defaultOrganization = OptionsModel.CounselingOptions.DefaultOrganizationName;

            string memoSignature = OptionsModel.GenericMemoOptions.DefaultSignatureName;
            string memoRank = OptionsModel.GenericMemoOptions.DefaultRank;
            string memoTitle = OptionsModel.GenericMemoOptions.DefaultTitle;

            bool dontWarnAboutAdobe = !OptionsModel.ReferenceOptions.ShowAdobeMissingWarning;

            dontWarnMeAboutAdobe.Checked = dontWarnAboutAdobe;
            defaultNameAndTitleOfCounselorTextbox.Text = defaultCounselorName;
            defaultOrganizationTextbox.Text = defaultOrganization;

            memoDefaultSignature.Text = memoSignature;
            defaultSignatureRank.Text = memoRank;
            defaultSignatureTitle.Text = memoTitle;

            dontAskMeCrashReportCheckbox.Checked = !OptionsModel.AskToSendCrashReport;
            autoSendRadioButton.Checked = OptionsModel.AutoSubmitCrashReport;
            neverSendRadioButton.Checked = !OptionsModel.AutoSubmitCrashReport;

            dontAskMeCrashReportCheckbox_CheckedChanged(null, null);
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            OptionsModel.ReferenceOptions.ShowAdobeMissingWarning = !dontWarnMeAboutAdobe.Checked;
            
            OptionsModel.CounselingOptions.DefaultOrganizationName = defaultOrganizationTextbox.Text;
            OptionsModel.CounselingOptions.DefaultCounselorNameAndTitle = defaultNameAndTitleOfCounselorTextbox.Text;

            OptionsModel.GenericMemoOptions.DefaultSignatureName = memoDefaultSignature.Text;
            OptionsModel.GenericMemoOptions.DefaultRank = defaultSignatureRank.Text;
            OptionsModel.GenericMemoOptions.DefaultTitle = defaultSignatureTitle.Text;

            OptionsModel.AskToSendCrashReport = !dontAskMeCrashReportCheckbox.Checked;
            OptionsModel.AutoSubmitCrashReport = autoSendRadioButton.Checked;

            OptionsModel.Save();
        }


        private void dontAskMeCrashReportCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            bool dontAskChecked = dontAskMeCrashReportCheckbox.Checked;
            
            autoSendRadioButton.Enabled = dontAskChecked;
            neverSendRadioButton.Enabled = dontAskChecked;
        }
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.OptionsDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.OptionsDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.OptionsDialogSize = bounds.Size;
            Properties.Settings.Default.OptionsDialogLocation = bounds.Location;
            if(bounds.Height < 451)
            {
                Properties.Settings.Default.OptionsDialogHeight = 451;
            } else{
                Properties.Settings.Default.OptionsDialogHeight = bounds.Height;
            }
            if (bounds.Width < 518)
            {
                Properties.Settings.Default.OptionsDialogWidth = 518;
            }
            else
            {
                Properties.Settings.Default.OptionsDialogWidth = bounds.Width;
            }
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.OptionsDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                Size size = Properties.Settings.Default.OptionsDialogSize;
                if(size.Height < 451)
                {
                    size.Height = 451;
                }
                if(size.Width < 518)
                {
                    size.Width = 518;
                }
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.OptionsDialogLocation, size);
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
        //Added to save the forms location so it can be restored mdail 8-19-19
        private void OptionsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLocation();
        }
    }
}
