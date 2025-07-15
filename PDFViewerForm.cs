using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    public partial class PDFViewerForm : Form
    {
        Document document;
        //bool adobeExists;

        internal PDFViewerForm(Document document)
        {
            this.document = document;

            InitializeComponent();

            OnLoad();
        }


        private void OnLoad()
        {
            if (document.GeneratedDocID != -1)
                saveAndCloseButton.Visible = false;

            FileInfo file = new FileInfo(document.Filepath);

            Uri uri = new Uri(file.FullName);
            webBrowser1.Url = uri;
        }


        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            saveAndCloseButton.Enabled = false;
            exportButton.Enabled = false;
            document.Filepath = "";
            document.Save();
            SaveLocation();
            this.Dispose();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            DocumentExportController.ExportDocument(document);
        }

        private void PDFViewerForm_Load(object sender, EventArgs e)
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.PDFViewerFormSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.PDFViewerFormLocation, Properties.Settings.Default.PDFViewerFormSize);
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

        private void PDFViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                    Properties.Settings.Default.PDFViewerFormSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.PDFViewerFormSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.PDFViewerFormSize = bounds.Size;
            Properties.Settings.Default.PDFViewerFormLocation = bounds.Location;
            Properties.Settings.Default.PDFViewerFormHeight = bounds.Height;
            Properties.Settings.Default.PDFViewerFormWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
    }
}
