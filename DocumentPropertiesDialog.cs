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
    public partial class DocumentPropertiesDialog : Form
    {
        //SelectNewParentDocumentDialog selectNewParentDocumentDialog;
        Document document;
        List<Document> otherDocumentsToUpdate;

        int soldierID;
        int userGeneratedDocID;
        bool isUserUploaded;

        bool hasUnsavedChanges = false;
        bool exitButtonClicked = false;

        string oldFilename = "";



        public DocumentPropertiesDialog(int userGeneratedDocID)
        {
            InitializeComponent();

            DataTable documentStatuses = DocumentModel.GetDocumentStatuses();
            statusComboBox.DataSource = documentStatuses;
            statusComboBox.DisplayMember = "documentstatustext";
            statusComboBox.ValueMember = "documentstatusid";

            this.userGeneratedDocID = userGeneratedDocID;

            //document = DocumentModel.LoadGeneratedDocument(userGeneratedDocID);
            document = new Document(userGeneratedDocID);

            LoadFormValuesForDocument();

            hasUnsavedChanges = false;
            saveChangesButton.Enabled = false;
            otherDocumentsToUpdate = new List<Document>();
        }


        private void LoadFormValuesForDocument()
        {
            isUserUploaded = document.UserUploaded;
            nameTextBox.Text = document.DocumentName;
            dateOfCounselingPicker.Value = document.Date;
            statusComboBox.SelectedValue = Convert.ToInt32(document.Status);
            soldierID = document.SoldierID;

            if (isUserUploaded == true)
            {
                FileInfo file = new FileInfo(document.Filepath);

                documentLabel.Visible = true;
                filepathLabel.Visible = true;
                filepathLabel.Text = file.Name;
                replaceDocumentButton.Visible = true;
            }
            else
            {
                string templateName = TemplatesModel.GetTemplateNameByTemplateID(document.TemplateID);

                replaceDocumentButton.Visible = false;
                createdFromTemplateLabel.Visible = true;

                createdFromTemplateLabel.Text = "Created from template - " + templateName;
            }

            if (document.ParentDocumentID == -1)
                detachDocumentFromParentButton.Visible = false;
            else
                detachDocumentFromParentButton.Visible = true;

        }


        private void OnOpenDocumentButtonClicked(object sender, EventArgs e)
        {
            DialogHelper.ShowEditDocumentDialog(document, this);
            LoadFormValuesForDocument();
            hasUnsavedChanges = false;
        }





        private void OnAttachToAnotherDocumentButtonClicked(object sender, EventArgs e)
        {
            ReassignDocumentController controller = new ReassignDocumentController(document);
            DialogResult selectNewParentResult = controller.PromptToSelectNewParentDocument(SelectNewParentDocumentDialog.SelectNewParentMode.SelectNewFromParents);

            if (selectNewParentResult == DialogResult.Cancel)
                return;

            int oldParentDocumentID = document.ParentDocumentID;
            document.ParentDocumentID = controller.NewParentDocumentID;

            //DataRow[] childDocs = DocumentModel.UserGeneratedDocumentsTable.Select("parentdocumentid = " + document.GeneratedDocID);
            List<Document> childDocs = DocumentModel.GetChildDocuments(document);

            if (childDocs.Count() > 0)
            {
                DialogResult result = controller.PromptToMoveChildDocuments(childDocs);

                if (result == DialogResult.Cancel)
                {
                    document.ParentDocumentID = oldParentDocumentID;
                    otherDocumentsToUpdate.Clear();
                }
                else
                {
                    otherDocumentsToUpdate = controller.OtherDocumentsToUpdate;
                }
            }

            LoadFormValuesForDocument();
            OnChanges();
        }


        private void OnAssignToDifferentSoldierButtonClicked(object sender, EventArgs e)
        {
            SelectSoldierDialog.SelectSoldierMode selectSoldierMode = SelectSoldierDialog.SelectSoldierMode.ReassignDocument;
            SelectSoldierDialog form = new SelectSoldierDialog(selectSoldierMode);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.Cancel)
                return;
            
            int newSoldierID = form.SelectedSoldierID;
            document.SoldierID = newSoldierID;
            document.SoldierIDChanged = true;

            if (document.ParentDocumentID != -1)
            {
                document.ParentDocumentID = -1;
            }
            else
            {
                string question = "Move all documents attached to this one to the new Soldier?";
                string caption = "Move all documents?";
                List<string> buttonText = new List<string> { "Move All", "No" };
                DialogResult moveAllResult = CQPMessageBox.ShowDialog(question, caption, 
                                                CQPMessageBox.CQPMessageBoxButtons.YesNo, buttonText, CQPMessageBox.CQPMessageBoxIcon.Question);

                List<Document> childDocs = DocumentModel.GetChildDocuments(document);

                if (moveAllResult == DialogResult.Yes)
                {
                    foreach (Document childDocument in childDocs)
                    {
                        childDocument.SoldierID = newSoldierID;
                        childDocument.SoldierIDChanged = true;
                    }
                }
                else
                {
                    //ReassignChildDocumentsDialog dialog = new ReassignChildDocumentsDialog(DocumentReassignmentReason.ChangingParent);
                    //DocumentReassignmentMode reassignDocumentMode = ReassignChildDocumentsDialog.ShowDialog(DocumentReassignmentReason.ChangingParent);
                    //if (reassignDocumentMode == DocumentReassignmentMode.DetachAllChildren)
                    //{
                    //    DocumentModel.DetachAllChildDocuments(document.GeneratedDocID);
                    //}
                    //else if (reassignDocumentMode == DocumentReassignmentMode.NewParent)
                    //{
                    //    SelectNewParentDocumentDialog dialog = new SelectNewParentDocumentDialog(soldierID, document.GeneratedDocID, 0);
                    //    DialogResult selectParentResult = dialog.ShowDialog();
                    //    if (selectParentResult == DialogResult.Cancel)
                    //        return;
                    //    else
                    //    {
                    //        int newParentDocumentID = dialog.NewParentDocumentID;
                    //        foreach (Document childDocument in childDocs)
                    //        {
                    //            childDocument.ParentDocumentID = newParentDocumentID;
                    //        }
                    //    }
                    //}
                    //else if (reassignDocumentMode == DocumentReassignmentMode.Cancel)
                    //    return;

                    ReassignDocumentController controller = new ReassignDocumentController(document);
                    DialogResult moveDocumentsResult = controller.PromptToMoveChildDocuments(childDocs);
                    if (moveDocumentsResult == DialogResult.Cancel)
                        return;
                    else
                        childDocs = controller.OtherDocumentsToUpdate;
                }

                otherDocumentsToUpdate.AddRange(childDocs);
            }

            //Model.SaveDocumentHeaderInfo(document);

            OnChanges();
            LoadFormValuesForDocument();
        }


        private void SaveChangesButtonClicked(object sender, EventArgs e)
        {
            //if(document.FilepathChanged)
            //{
            //    DocumentModel.RemoveUserDocument(oldFilename);
            //}

            document.Save();

            foreach (Document otherDocument in otherDocumentsToUpdate)
            {
                otherDocument.Save();
            }
            
            hasUnsavedChanges = false;
            
            saveChangesButton.Enabled = false;
        }


        private void OnStatusComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (document == null)
                return;

            document.Status = (DocumentStatus)Convert.ToInt32(statusComboBox.SelectedValue);
            hasUnsavedChanges = true;
            OnChanges();
        }


        private void OnNameTextBoxTextChanged(object sender, EventArgs e)
        {
            hasUnsavedChanges = true;
            OnChanges();
        }


        private void OnDateOfCounselingPickerValueChanged(object sender, EventArgs e)
        {
            if (document == null)
                return;

            document.Date = dateOfCounselingPicker.Value;
            hasUnsavedChanges = true;
            OnChanges();
        }


        private void OnNameTextBoxLeave(object sender, EventArgs e)
        {
            if (document == null)
                return;

            document.DocumentName = nameTextBox.Text;
            OnChanges();
        }


        private void ReplaceDocumentButtonClicked(object sender, EventArgs e)
        {
            string message = "Are you sure you want to replace document: "
                + document.Filepath + "?  This will remove the copy of "
                + "the document from Counselor.\n\n"
                + "This action will not be performed until you Save.";

            string caption = "Replace Document?";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            CQPMessageBox.CQPMessageBoxIcon icons = CQPMessageBox.CQPMessageBoxIcon.Warning;
            List<string> buttonText = new List<string> { "Replace Document.", "Cancel" };

            DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icons);
            if (result == DialogResult.No)
                return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Title = "Select a file to upload...";
            dialog.RestoreDirectory = true;

            result = dialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            oldFilename = document.Filepath;

            string filepath = dialog.FileName;
            document.Filepath = filepath;
            document.FilepathChanged = true;

            OnChanges();
            LoadFormValuesForDocument();
            hasUnsavedChanges = true;
        }


        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            exitButtonClicked = true;
            SaveLocation();
            this.Dispose();
        }

        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.DocumentPropertiesDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.DocumentPropertiesDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.DocumentPropertiesDialogSize = bounds.Size;
            Properties.Settings.Default.DocumentPropertiesDialogLocation = bounds.Location;
            Properties.Settings.Default.DocumentPropertiesDialogHeight = bounds.Height;
            Properties.Settings.Default.DocumentPropertiesDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }

        private void OnEditCounselingFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (exitButtonClicked == true)
            {
                SaveLocation();
                return;
            }

            if (hasUnsavedChanges == true)
            {
                //SaveChangesDialog dialog = new SaveChangesDialog(SaveChangesDialog.SaveChangesButtons.SaveDontSaveCancel);
                //DialogResult result = dialog.ShowDialog();
                
                DialogResult result = DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);

                if (result == DialogResult.Yes)
                    SaveChangesButtonClicked(null, null);
                else if (result == DialogResult.No)
                    return;
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
            SaveLocation();
        }


        private void OnDetachDocumentFromParentButtonClicked(object sender, EventArgs e)
        {
            //ConfirmDetachDocumentDialog dialog = new ConfirmDetachDocumentDialog(document);
            //DialogResult result = dialog.ShowDialog(this);

            DialogResult result = DialogHelper.ConfirmDetachDocument(document.GeneratedDocID);

            if (result == DialogResult.Yes)
                document.ParentDocumentID = -1;

            OnChanges();
            LoadFormValuesForDocument();
        }


        private void OnChanges()
        {
            saveChangesButton.Enabled = true;
        }

        private void DocumentPropertiesDialog_Load(object sender, EventArgs e)
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.DocumentPropertiesDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.DocumentPropertiesDialogLocation, Properties.Settings.Default.DocumentPropertiesDialogSize);
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
