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
    public partial class PromptToSubmitCrashReport : Form
    {

        private int minWidth = 420;
        private int minHeigth = 254;

        public PromptToSubmitCrashReport()
        {
            InitializeComponent();           
        }

        private void rememberMySelectionCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SaveCheckboxStatus()
        {
            if (DatabaseConnection.IsValid == false)
                return;

            if (rememberMySelectionCheckbox.Checked)
                OptionsModel.SetOptionValue("prompttosubmitcrashreport", "0");
            else
                OptionsModel.SetOptionValue("prompttosubmitcrashreport", "1");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SaveCheckboxStatus();

            if(DatabaseConnection.IsValid == true)
                OptionsModel.SetOptionValue("submitcrashreport", "0");

            DialogResult = DialogResult.No;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SaveCheckboxStatus();

            if (DatabaseConnection.IsValid == true)
                OptionsModel.SetOptionValue("submitcrashreport", "1");

            DialogResult = DialogResult.Yes;
        }

        private void PromptToSubmitCrashReport_ForeColorChanged(object sender, EventArgs e)
        {
            //Added to save the forms location so it can be restored mdail 8-19-19
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
                    Properties.Settings.Default.PromptToSubmitCrashReportSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.PromptToSubmitCrashReportSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.PromptToSubmitCrashReportSize = bounds.Size;
            Properties.Settings.Default.PromptToSubmitCrashReportLocation = bounds.Location;
            if (bounds.Height < minHeigth)
            {
                Properties.Settings.Default.PromptToSubmitCrashReportHeight = minHeigth;
            }
            else
            {
                Properties.Settings.Default.PromptToSubmitCrashReportHeight = bounds.Height;
            }
            if (bounds.Width < 420)
            {
                Properties.Settings.Default.PromptToSubmitCrashReportWidth = minWidth;
            }
            else
            {
                Properties.Settings.Default.PromptToSubmitCrashReportWidth = bounds.Width;
            }
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.PromptToSubmitCrashReportSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
                System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
                if (bounds.Height < minHeigth)
                {
                    bounds.Height = minHeigth;
                }
                if (bounds.Width < minWidth)
                {
                    bounds.Width = minWidth;
                }
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.PromptToSubmitCrashReportLocation, bounds.Size);
            }
            else
            {
                Size size = Properties.Settings.Default.PromptToSubmitCrashReportSize;
                if (size.Height < minHeigth)
                {
                    size.Height = minHeigth;
                }
                if (size.Width < minWidth)
                {
                    size.Width = minWidth;
                }
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.PromptToSubmitCrashReportLocation, size);
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

        private void PromptToSubmitCrashReport_Load(object sender, EventArgs e)
        {
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }
    }
}
