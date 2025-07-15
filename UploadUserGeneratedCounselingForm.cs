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
    public partial class UploadUserGeneratedCounselingForm : Form
    {
        string title;
        string filename;
        DateTime date;
        int parentDocumentID;
        int soldierID;
        DocumentType type;

        bool titleValid;
        bool filenameFilledIn;
        bool filenameExists;
        private bool documentTypeValid;

        public UploadUserGeneratedCounselingForm(int soldierID, int parentDocumentID)
        {
            this.soldierID = soldierID;
            this.parentDocumentID = parentDocumentID;
            type = DocumentType.Unknown;

            Load += new EventHandler(UploadUserGeneratedCounselingForm_Load);
            InitializeComponent();
        }

        void UploadUserGeneratedCounselingForm_Load(object sender, EventArgs e)
        {
            InitializeSoldierValues(soldierID);

            if (parentDocumentID != -1)
            {
                Document parentDoc = new Document(parentDocumentID);
                string docName = parentDoc.DocumentName;
                attachToParentCheckbox.Text = attachToParentCheckbox.Text.Replace("$DOCNAME", docName);
            }
            else
                attachToParentCheckbox.Visible = false;
            

            try
            {
                DataTable types = DocumentModel.GetDocumentTypesTable();
                comboBox1.DisplayMember = "documenttypename";
                comboBox1.ValueMember = "documenttypeid";
                comboBox1.DataSource = types;
                dateOfDocumentDateTimePicker.Value = DateTime.Now;
            }
            catch (DataLoadFailedException ex)
            {
                CQPMessageBox.Show("An unexpected error occurred trying to link these documents.");
                throw new DataLoadFailedException("The user upload could not be completed", ex);
            }

            type = DocumentType.Unknown;
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }

        private void InitializeSoldierValues(int soldierID)
        {
            Soldier soldier = new Soldier(soldierID);

            if (soldierID == -1)
            {
                rankAndNameLabel.Text = "Unassigned Documents";
                return;
            }

            string formattedName = soldier.Rank.ToString() + " " + soldier.LastName + ", " + soldier.FirstName + " " + soldier.MiddleInitial;

            rankAndNameLabel.Text = formattedName;

            pictureBox1.Image = soldier.Picture;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void browseFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            filenameTextbox.Text = openFileDialog.FileName;
        }

        private void uploadDocumentButton_Click(object sender, EventArgs e)
        {
            if (!AllFormFieldsValid())
            {
                AlertInvalidFormFields();
                DialogResult = DialogResult.None;
                return;
            }

            if (attachToParentCheckbox.Checked == false)
                parentDocumentID = -1;

            if (uploadRadioButton.Checked)
            {
                UploadUserGeneratedDocument();
            }
            else
            {
                this.Hide();
                ShowTemplateDialog();
            }

            this.Dispose();
        }

        private void UploadUserGeneratedDocument()
        {
            title = titleTextbox.Text;
            filename = filenameTextbox.Text;
            date = dateOfDocumentDateTimePicker.Value;
            //type = DocumentType.Counseling;
            
            try
            {
                SaveDocument();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveDocument()
        {
            Document document = new Document();
            document.Date = date; //Date;
            document.DocumentName = title; //Title;
            document.DocumentType = type;//DocumentType.Counseling;
            document.Filepath = filename; // Filename;
            document.FilepathChanged = true;
            document.FormID = (int)DocumentFormIDs.UserGenerated;
            document.GeneratedDocID = -1;
            document.ParentDocumentID = parentDocumentID; //document.GeneratedDocID; //ParentDocumentID;
            document.SoldierID = soldierID; // selectedSoldierID;
            document.Status = DocumentStatus.Draft;
            document.UserUploaded = true;

            try
            {
                document.Save();
            }
            catch (DataStoreFailedException)
            {
                CQPMessageBox.Show("An unexpected error occurred attempting to save"
                    + " the document.", "Unknown error.", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
            }
        }

        private bool AllFormFieldsValid()
        {
            if (uploadRadioButton.Checked)
                return UserGeneratedDocValuesValid();
            else
                if (!templateRadioButton.Checked)
                    return false;

            return true;
        }

        private bool UserGeneratedDocValuesValid()
        {
            titleValid = titleTextbox.Text != "";
            filenameFilledIn = filenameTextbox.Text != "";
            filenameExists = System.IO.File.Exists(filenameTextbox.Text);

            if (type == DocumentType.Unknown)
                documentTypeValid = false;
            
            else if (type == DocumentType.Other && otherTextbox.Text == "")
                documentTypeValid = false;
            else
                documentTypeValid = true;

            return titleValid & filenameFilledIn & filenameExists & documentTypeValid;
        }        

        private void AlertInvalidFormFields()
        {
            if (uploadRadioButton.Checked)
            {
                if (!titleValid)
                    titleLabel.Text = "* Title";
                else
                    titleLabel.Text = "Title";

                if (!filenameFilledIn)
                    filenameLabel.Text = "* Document Filename";
                else
                    filenameLabel.Text = "Document Filename";

                if (!filenameExists)
                    filenameLabel.Text = "* Document Filename - filename specified does not exist";
                else
                    filenameLabel.Text = "Document Filename";

                if (!documentTypeValid)
                    documentTypeLabel.Text = "* Document Type";
                else
                    documentTypeLabel.Text = "Document Type";
            }
            else
            {
                templateGroupBox.Text = "* Options ";
            }
        }        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int typeid = Convert.ToInt32(comboBox1.SelectedValue);
            type = (DocumentType)typeid;
        }

        private void standardTypesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = standardTypesRadioButton.Checked;
            type = (DocumentType)Convert.ToInt32(comboBox1.SelectedValue);
        }

        private void otherRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            otherTextbox.Enabled = otherRadioButton.Checked;
            
            if(otherRadioButton.Checked)
                type = DocumentType.Other;
        }

        private void uploadRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RootTable.RowStyles[4].Height = 0;
            RootTable.RowStyles[5].Height = 100;
        }

        private void templateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RootTable.RowStyles[4].Height = 100;
            RootTable.RowStyles[5].Height = 0;
        }

        private void ShowTemplateDialog()
        {
            Form documentForm = new Form();

            documentForm.AutoSize = true;
            documentForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //fix so that if the color is ever changed again it only needs to be changed once mdail 8-27-19
            documentForm.BackColor = DefaultBackColor;
            documentForm.StartPosition = FormStartPosition.CenterParent;
            documentForm.ShowInTaskbar = false;
            documentForm.ShowIcon = false;

            Control control;
            control = new DocumentsTabPage(soldierID, parentDocumentID);
            
            control.Dock = DockStyle.Fill;
            
            documentForm.Controls.Add(control);

            documentForm.ShowDialog(this);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void UploadUserGeneratedCounselingForm_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.UploadUserGeneratedCounselingFormSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.UploadUserGeneratedCounselingFormSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.UploadUserGeneratedCounselingFormSize = bounds.Size;
            Properties.Settings.Default.UploadUserGeneratedCounselingFormLocation = bounds.Location;
            Properties.Settings.Default.UploadUserGeneratedCounselingFormHeight = bounds.Height;
            Properties.Settings.Default.UploadUserGeneratedCounselingFormWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the windows position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.UploadUserGeneratedCounselingFormSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.UploadUserGeneratedCounselingFormLocation, Properties.Settings.Default.UploadUserGeneratedCounselingFormSize);
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
