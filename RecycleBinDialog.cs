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
    public partial class RecycleBinDialog : Form
    {
        public RecycleBinDialog()
        {
            if (DesignMode == true)
                return;

            InitializeComponent();
            Load += new EventHandler(RecycleBinDialog_Load);
        }

        void RecycleBinDialog_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeSoldierTab();
                InitializeDocumentsTab();                
                RecyclingBinTable.ColumnStyles[1].Width = 0;
                soldiersTabButton.Enabled = false;

                formattedSoldierTable.Select();
                //Added to resotre the forms saved location from last run mdail 8-19-19
                PutAtSavedLocation();
            }
            catch (Exception ex)
            {
                CQPMessageBox.ShowDialog(ex.Message);
            }
        }

        private void InitializeSoldierTab()
        {
        }

        private void InitializeDocumentsTab()
        {
            RefreshDocumentsDataGridView();
        }

        private void restoreSelectedSoldiers_Click(object sender, EventArgs e)
        {
            List<int> soldiersToRestore = new List<int>();

            foreach (DataGridViewRow row in formattedSoldierTable.Rows)
            {
                if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells["checkboxColumn"]).Value) == true)
                {
                    int soldierID = Convert.ToInt32(row.Cells["soldieridColumn"].Value);
                    soldiersToRestore.Add(soldierID);
                }
            }

            foreach (int soldierID in soldiersToRestore)
            {
                SoldierModel.RemoveSoldierFromRecyclingBin(soldierID);
            }

            if (soldiersToRestore.Count > 0)
            {
                string message = soldiersToRestore.Count > 1 ? soldiersToRestore.Count + " Soldiers restored." : "Soldier restored.";

                CQPMessageBox.Show(message, "Restored", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);
            }
        }

        private bool ConfirmDeleteSoldiers(List<int> soldierIDsToDelete)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("You are about to delete the following " + soldierIDsToDelete.Count + " Soldiers.\n\n");

            foreach (int soldierID in soldierIDsToDelete)
            {
                Soldier soldier = new Soldier(soldierID);

                string rank = RankingModel.RankingAbbreviationFromEnum(soldier.Rank);
                string last = soldier.LastName;
                string first = soldier.FirstName;
                string MI = soldier.MiddleInitial.ToString();

                sb.Append("    - " + rank + " " + last + ", " + first + " " + MI + "\n");
            }

            sb.Append("\n");
            sb.Append("All related counselings, letters, memos, notes, and other files will be deleted as well.\n");
            sb.Append("\nThis action cannot be undone.\n");
            sb.Append("\nAre you sure you want to delete these Soldiers?\n");

            string caption = "CONFIRM PERMANENT DELETE";
            List<string> buttonsText = new List<string>() { "Delete Permanently", "Cancel" };
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Warning;
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OKCancel;

            DialogResult result = CQPMessageBox.ShowDialog(sb.ToString(), caption, buttons, buttonsText, icon);

            if (result == DialogResult.Cancel)
                return false;
            else
                return true;
        }

        private void permanentlyDeleteButton_Click(object sender, EventArgs e)
        {           
            List<int> soldiersToDelete = new List<int>();

            foreach (DataGridViewRow row in formattedSoldierTable.Rows)
            {
                if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells["checkboxColumn"]).Value) == true)
                {
                    int soldierID = Convert.ToInt32(row.Cells["soldieridColumn"].Value);
                    soldiersToDelete.Add(soldierID);
                }
            }

            bool deleteConfirmation = ConfirmDeleteSoldiers(soldiersToDelete);
            if (deleteConfirmation == false)
                return;

            foreach (int soldierID in soldiersToDelete)
            {
                SoldierModel.DeleteSoldier(soldierID);
            }

            if (soldiersToDelete.Count > 0)
            {
                CQPMessageBox.Show(soldiersToDelete.Count + " Soldiers Deleted.", "Soldiers Deleted", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);
            }
        }

        private void emptySoldierRecycleBinButton_Click(object sender, EventArgs e)
        {
            List<int> soldiersToDelete = new List<int>();

            foreach (DataGridViewRow row in formattedSoldierTable.Rows)
            {
                int soldierID = Convert.ToInt32(row.Cells["soldieridColumn"].Value);
                soldiersToDelete.Add(soldierID);
            }

            foreach(int soldierID in soldiersToDelete)
                SoldierModel.DeleteSoldier(soldierID);

            if (soldiersToDelete.Count > 0)
            {
                CQPMessageBox.Show(soldiersToDelete.Count + " Soldiers Deleted.", "Soldiers Deleted", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);
            }
        }

        private void restoreSelectedDocumentsButton_Click(object sender, EventArgs e)
        {
            int count = 0;

            List<Document> childDocs = new List<Document>();
            List<Document> checkedDocuments = deletedDocumentsDataGridView.CheckedDocuments;

            foreach(Document document in checkedDocuments)
            {
                if (document.ParentDocumentID != -1)
                    childDocs.Add(document);
                else
                    DocumentModel.RemoveDocumentFromRecycleBin(document.GeneratedDocID);

                count++;
            }

            foreach (Document childDoc in childDocs)
            {
                if (DocumentModel.DocumentIsInRecyclingBin(childDoc.ParentDocumentID))
                {
                    childDoc.ParentDocumentID = -1;
                    childDoc.Save();
                    DocumentModel.RemoveDocumentFromRecycleBin(childDoc.GeneratedDocID);
                }
                else
                {
                    DocumentModel.RemoveDocumentFromRecycleBin(childDoc.GeneratedDocID);
                }
            }
            
            RefreshDocumentsDataGridView();

            if (count > 0)
            {
                string caption = "Restored";
                string message = count > 1 ? count + " Documents Restored." : "Document Restored";

                CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Information;

                CQPMessageBox.Show(message, caption, buttons, icon);
            }
        }


        private void RefreshDocumentsDataGridView()
        {
            deletedDocumentsDataGridView.Filter = "(deleted = 1)";
        }

        private void permanentlyDeleteSelectedDocumentsButton_Click(object sender, EventArgs e)
        {
            List<int> documentsToDelete = new List<int>();

            foreach(Document document in deletedDocumentsDataGridView.CheckedDocuments)
                documentsToDelete.Add(document.GeneratedDocID);

            foreach (int documentID in documentsToDelete)
                DocumentModel.DeleteDocument(documentID);
                //DocumentModel.DeleteDocumentPermanently(documentID);

            if (documentsToDelete.Count > 0)
            {
                CQPMessageBox.Show(documentsToDelete.Count + " documents deleted.", "Documents Deleted", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);

                RefreshDocumentsDataGridView();
            }
        }

        private void emptyDocumentRecycleBinButton_Click(object sender, EventArgs e)
        {
            List<int> documentsToDelete = new List<int>();
            
            foreach(Document document in deletedDocumentsDataGridView.Items)
                documentsToDelete.Add(document.GeneratedDocID);

            foreach (int documentID in documentsToDelete)
                DocumentModel.DeleteDocument(documentID);
                //DocumentModel.DeleteDocumentPermanently(documentID);

            if (documentsToDelete.Count > 0)
            {
                CQPMessageBox.Show(documentsToDelete.Count + " documents deleted.", "Documents Deleted", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);
            }

            RefreshDocumentsDataGridView();
        }

        private void soldiersTabButton_Click(object sender, EventArgs e)
        {
            soldiersTabButton.Enabled = false;
            counselingTabButton.Enabled = true;

            RecyclingBinTable.ColumnStyles[1].SizeType = SizeType.Percent;
            RecyclingBinTable.ColumnStyles[1].Width = 0;

            RecyclingBinTable.ColumnStyles[0].SizeType = SizeType.Percent;
            RecyclingBinTable.ColumnStyles[0].Width = 100;

            RecyclingBinTable.PerformLayout();


            //SuspendLayout();
            //documentsRecyclingPanel.Hide();

            //RecyclingBinTable.Controls.Remove(documentsRecyclingPanel);
            //RecyclingBinTable.Controls.Add(soldierRecyclingBinPanel,0,0);

            //soldierRecyclingBinPanel.Visible = true;
            //documentsRecyclingPanel.Visible = false;
            //soldierRecyclingBinPanel.BringToFront();
            //documentsRecyclingPanel.SendToBack();
            
            //ResumeLayout();
        }

        private void counselingTabButton_Click(object sender, EventArgs e)
        {
            soldiersTabButton.Enabled = true;
            counselingTabButton.Enabled = false;

            RecyclingBinTable.ColumnStyles[1].SizeType = SizeType.Percent;
            RecyclingBinTable.ColumnStyles[1].Width = 100;

            RecyclingBinTable.ColumnStyles[0].SizeType = SizeType.Percent;
            RecyclingBinTable.ColumnStyles[0].Width = 0;

            //SuspendLayout();
            //soldierRecyclingBinPanel.Hide();
            //documentsRecyclingPanel.Show();

            //RecyclingBinTable.Controls.Remove(soldierRecyclingBinPanel);
            //RecyclingBinTable.Controls.Add(documentsRecyclingPanel,0,0);

            ////soldierRecyclingBinPanel.Visible = false;
            ////documentsRecyclingPanel.Visible = true;
            //soldierRecyclingBinPanel.SendToBack();
            //documentsRecyclingPanel.BringToFront();


            RecyclingBinTable.PerformLayout();

            //ResumeLayout();
        }

        private void RecycleBinDialog_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.RecycleBinDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.RecycleBinDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.RecycleBinDialogSize = bounds.Size;
            Properties.Settings.Default.RecycleBinDialogLocation = bounds.Location;
            Properties.Settings.Default.RecycleBinDialogHeight = bounds.Height;
            Properties.Settings.Default.RecycleBinDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.RecycleBinDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.RecycleBinDialogLocation, Properties.Settings.Default.RecycleBinDialogSize);
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
