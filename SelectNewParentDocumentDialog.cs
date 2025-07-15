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
    internal partial class SelectNewParentDocumentDialog : Form
    {
        internal enum SelectNewParentMode
        {
            SelectNewFromChildren,
            SelectNewFromParents
        }

        private Document currentParentDocument;
        private SelectNewParentMode mode;

        internal SelectNewParentDocumentDialog(Document currentParentDocument, SelectNewParentMode mode)
        {
            InitializeComponent();

            this.currentParentDocument = currentParentDocument;
            this.mode = mode;           
            Load += new EventHandler(SelectNewParentDocumentDialog_Load);
        }

        void SelectNewParentDocumentDialog_Load(object sender, EventArgs e)
        {
            InitializeDocumentsDataGridView(currentParentDocument, mode);
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }


        internal void InitializeDocumentsDataGridView(Document document, SelectNewParentMode mode)
        {
            string rowFilter = "";
            if (mode == SelectNewParentMode.SelectNewFromChildren)
            {
                rowFilter = "parentdocumentid = " + document.GeneratedDocID
                            + " and deleted = 0";
            }
            else if (mode == SelectNewParentMode.SelectNewFromParents)
            {
                rowFilter = "parentdocumentid = -1 "
                            + " and generateddocid <> " + document.GeneratedDocID
                            + " and soldierid = " + document.SoldierID
                            + " and deleted = 0";
            }

            deletedDocumentsDataGridView.Filter = rowFilter;

            if (deletedDocumentsDataGridView.SelectedDocumentID == -1)
                selectAsParentButton.Enabled = false;
        }


        internal int NewParentDocumentID
        {
            get; set;
        }

        private void selectAsParentButton_Click(object sender, EventArgs e)
        {
            if (deletedDocumentsDataGridView.SelectedDocumentID == -1)
            {
                return;
            }
            NewParentDocumentID = deletedDocumentsDataGridView.SelectedDocumentID;
        }

        private void SelectNewParentDocumentDialog_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.SelectNewParentDocumentDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.SelectNewParentDocumentDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.SelectNewParentDocumentDialogSize = bounds.Size;
            Properties.Settings.Default.SelectNewParentDocumentDialogLocation = bounds.Location;
            Properties.Settings.Default.SelectNewParentDocumentDialogHeight = bounds.Height;
            Properties.Settings.Default.SelectNewParentDocumentDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.SelectNewParentDocumentDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.SelectNewParentDocumentDialogLocation, Properties.Settings.Default.SelectNewParentDocumentDialogSize);
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
