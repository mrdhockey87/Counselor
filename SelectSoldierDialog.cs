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
    internal partial class SelectSoldierDialog : Form
    {
        internal enum SelectSoldierMode
        {
            ChooseSoldierToCounsel,
            ReassignDocument,
            ImportDocumentUnassigned,
            SelectSoldiersToExport
        }

        SelectSoldierMode mode;
        Document document;

        public SelectSoldierDialog(SelectSoldierMode mode)
        {
            InitializeComponent();            
            OnInit(mode, null);
        }


        public SelectSoldierDialog(SelectSoldierMode mode, Document document)
        {
            this.mode = mode;
            this.document = document;

            Load += new EventHandler(SelectSoldierDialog_Load);
            InitializeComponent();
        }

        void SelectSoldierDialog_Load(object sender, EventArgs e)
        {
            OnInit(mode, document);
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }


        public void OnInit(SelectSoldierMode mode, Document document)
        {
            UpdateTextAndTable(mode, null);
            if (mode != SelectSoldierMode.SelectSoldiersToExport)
            {
                formattedSoldierTable.SoldierDoubleClicked += delegate (Object sender, EventArgs arg) { DialogResult = DialogResult.OK; };
            }
        }


        internal void UpdateTextAndTable(SelectSoldierMode mode, Document document)
        {
            switch (mode)
            {
                case(SelectSoldierMode.ChooseSoldierToCounsel):
                    mainTextLabel.Text = "Select a Soldier to counsel: ";
                    break;
                case(SelectSoldierMode.ImportDocumentUnassigned):
                    {
                        string value;
                        if (document != null && document.DocumentName != null && document.DocumentName != "")
                            value = ", '" + document.DocumentName + "',";
                        else
                            value = "";

                        mainTextLabel.Text = "One of the documents you are importing" + value + " does not have a Soldier assigned to it.\n"
                                        + "Select a Soldier to assign it to, or assign it to UNASSIGNED if you aren't sure and want to come back to it later.";
                        break;
                    }
                case(SelectSoldierMode.ReassignDocument):
                    mainTextLabel.Text = "Select a Soldier to assign this document to: ";
                    break;
                case(SelectSoldierMode.SelectSoldiersToExport):
                    mainTextLabel.Text = "Select the Soldiers you want to export.  (You will be prompted to select the documents and notes for these Soldiers next.)";
                    selectSoldierButton.Text = "Next";
                    formattedSoldierTable.ShowCheckboxes = true;
                    break;
            }
        }

        public int SelectedSoldierID
        {
            get
            {
                return formattedSoldierTable.SelectedSoldierID;
            }
        }


        internal List<int> SelectedSoldierIDs
        {
            get
            {
                return formattedSoldierTable.SelectedSoldierIDs;
            }
        }


        private void SelectedSoldierIndexChanged(int soldierID)
        {
            selectSoldierButton.Enabled = true;
        }

        private void SelectSoldierDialog_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.SelectSoldierDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.SelectSoldierDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.SelectSoldierDialogSize = bounds.Size;
            Properties.Settings.Default.SelectSoldierDialogLocation = bounds.Location;
            Properties.Settings.Default.SelectSoldierDialogHeight = bounds.Height;
            Properties.Settings.Default.SelectSoldierDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.SelectSoldierDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.SelectSoldierDialogLocation, Properties.Settings.Default.SelectSoldierDialogSize);
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
