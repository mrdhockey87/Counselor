using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace CounselQuickPlatinum
{
    public partial class PrintHTMLForm : Form
    {
        public PrintHTMLForm()
        {
            Load += new EventHandler(PrintHTMLForm_Load);            
            InitializeComponent();
        }

        void PrintHTMLForm_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += SleepOneMs;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.PrintHTMLFormSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.PrintHTMLFormLocation, Properties.Settings.Default.PrintHTMLFormSize);
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

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            webBrowser1.ShowPrintDialog();
            SaveLocation();
            this.Dispose();
        }


        private void SleepOneMs(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
        }

        public string URL
        {
            set
            {
                webBrowser1.Url = new Uri(value, UriKind.RelativeOrAbsolute);
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
                    Properties.Settings.Default.PrintHTMLFormSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.PrintHTMLFormSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.PrintHTMLFormSize = bounds.Size;
            Properties.Settings.Default.PrintHTMLFormLocation = bounds.Location;
            Properties.Settings.Default.PrintHTMLFormHeight = bounds.Height;
            Properties.Settings.Default.PrintHTMLFormWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
    }
}
