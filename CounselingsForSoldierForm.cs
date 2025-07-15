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
    public partial class CounselingsForSoldierForm : Form
    {
        private CounselingsForSoldierForm(int soldierID)
        {
            this.soldierID = soldierID;

            InitializeComponent();
            LoadTable();
        }


        public CounselingsForSoldierForm(int soldierID, int documentIDToHide)
        {
            this.soldierID = soldierID;

            InitializeComponent();
            LoadTable();
        }


        private void LoadTable()
        {            
            string filter = "soldierid = " + soldierID + " and parentdocumentid = -1";
            documentsDataGridView.Filter = filter;
        }


        internal int SelectedDocumentID
        {
            get
            {
                return selectedDocumentID;
            }
        }


        private void attachToDocumentButton_Click(object sender, EventArgs e)
        {
            selectedDocumentID = documentsDataGridView.SelectedDocumentID;
        }


        int selectedDocumentID;
        int soldierID;

        private void CounselingsForSoldierForm_Load(object sender, EventArgs e)
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.CounselingsForSoldierSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.CounselingsForSoldierLocation, Properties.Settings.Default.CounselingsForSoldierSize);
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

        private void CounselingsForSoldierForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.CounselingsForSoldierSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.CounselingsForSoldierSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.CounselingsForSoldierSize = bounds.Size;
            Properties.Settings.Default.CounselingsForSoldierLocation = bounds.Location;
            Properties.Settings.Default.CounselingsForSoldierHeight = bounds.Height;
            Properties.Settings.Default.CounselingsForSoldierWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
    }
}
