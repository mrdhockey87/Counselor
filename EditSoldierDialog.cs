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
    public partial class EditSoldierDialog : Form
    {
        bool dateOfBirthValid = true;
        bool dateOfRankValid = true;
        DataSet unitInformation;
        Soldier soldier;
        bool initialized;
        bool soldierImageChanged;
        bool soldierCustomImage;
        List<Image> rankingImages;
                
        internal EditSoldierDialog(Soldier soldier)
        {
            initialized = false;
            this.soldier = soldier;
            InitializeComponent();
            OnInitNew();
            LoadSoldierValues();
            lastNameTextbox.Focus();
            soldier.HasUnsavedChanges = false;
            initialized = true;
        }



        private void OnInitNew()
        {
            soldierImageChanged = false;
            rankingImages = RankingModel.GetRankingImages();
            PopulateRankingComboBox();
            PopulateUnitComboboxes();
        }

        private void LoadSoldierValues()
        {
            if (soldier == null)
                return;

            LoadPersonalValues();
            LoadUnitValues();
            LoadStatusValues();
        }

        private void LoadPersonalValues()
        {

            lastNameTextbox.Text = soldier.LastName;
            firstNameTextbox.Text = soldier.FirstName;
            middleInitialTextbox.Text = soldier.MiddleInitial.ToString();

            if (soldier.DateOfBirth.Ticks != 0)
            {
                dateOfBirthTextBox.Text = soldier.DateOfBirth.ToString("yyyy MM dd");
                
                int age = Utilities.CalculateAge(soldier.DateOfBirth, DateTime.Now);
                formattedAgeLabel.Text = age.ToString();
            }
            else
            {
                ageLabel.Text = "";
            }

            if (soldier.DateOfRank.Ticks != 0)
            {
                dateOfRankTextBox.Text = soldier.DateOfRank.ToString("yyyy MM dd");
            }

            rankingCombobox.SelectedIndex = (int)soldier.Rank - 1;

            if (soldier.hasCustomImage)
                soldierCustomImage = true;

            soldierPictureBox.Image = soldier.Picture;
        }

        private void LoadUnitValues()
        {
            int battalionID = soldier.UnitHierarchy.battalionID;
            int unitID = soldier.UnitHierarchy.unitID;
            int unitDesignatorID = soldier.UnitHierarchy.unitDesignatorID;
            int platoonID = soldier.UnitHierarchy.platoonID;
            int squadID = soldier.UnitHierarchy.squadID;

            battalionCombobox.SelectedValue = battalionID;
            unitNumberCombobox.SelectedValue = unitID;
            unitDesignatorCombobox.SelectedValue = unitDesignatorID;
            platoonNumberCombobox.SelectedValue = platoonID;
            squadSectionNumberCombobox.SelectedValue = squadID;
        }

        private void LoadStatusValues()
        {
            otherTextbox.Text = soldier.OtherStatus;

            foreach (Control control in statusCheckBoxesLayoutTable.Controls)
            {
                string type = control.GetType().ToString();
                if (type != "System.Windows.Forms.CheckBox")
                    continue;

                CheckBox checkBox = (CheckBox) control;

                string statusName = checkBox.Text;

                foreach (SoldierStatus entry in soldier.Statuses.Values)
                {
                    if (entry.applies == false)
                        continue;

                    if (entry.statusString == statusName)
                        checkBox.Checked = true;
                }
            }
        }

        private void PopulateRankingComboBox()
        {
            DataTable rankingTable;

            try
            {
                 rankingTable = RankingModel.GetRankingTable();
            }
            catch (DataLoadFailedException)
            {
                CQPMessageBox.Show("Error retrieving the ranking list.", "Ranking error", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                return;
            }


            if (rankingTable.Columns.Contains("rankingabbreviation") == false
                || rankingTable.Columns.Contains("rankingid") == false)
            {
                CQPMessageBox.Show("There was an error loading the soldier ranking values", "Ranking error",
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                return;
            }

            rankingCombobox.DataSource = rankingTable;
            rankingCombobox.DisplayMember = "rankingabbreviation";
            rankingCombobox.ValueMember = "rankingid";

        }

        private void PopulateUnitComboboxes()
        {
            try
            {
                unitInformation = UnitHierarchyModel.GetAllUnitInfo().Copy();
            }
            catch (DataLoadFailedException ex)
            {
                CQPMessageBox.Show(ex.Message, "Error", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                return;
            }

            battalionCombobox.DataSource = unitInformation.Tables["battalions"];
            battalionCombobox.ValueMember = "battalionid";
            battalionCombobox.DisplayMember = "battalionname";

            unitNumberCombobox.DataSource = unitInformation.Tables["units"];
            unitNumberCombobox.ValueMember = "unitid";
            unitNumberCombobox.DisplayMember = "unitname";

            unitDesignatorCombobox.DataSource = unitInformation.Tables["unitdesignators"];
            unitDesignatorCombobox.ValueMember = "unitdesignatorid";
            unitDesignatorCombobox.DisplayMember = "unitdesignatorname";

            platoonNumberCombobox.DataSource = unitInformation.Tables["platoons"];
            platoonNumberCombobox.ValueMember = "platoonid";
            platoonNumberCombobox.DisplayMember = "platoonname";

            squadSectionNumberCombobox.DataSource = unitInformation.Tables["squadsections"];
            squadSectionNumberCombobox.ValueMember = "squadsectionid";
            squadSectionNumberCombobox.DisplayMember = "squadsectionname";
        }

        private void unitNumberCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }

        private void platoonNumberCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }

        private void dateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            if (!dateTimeMaskedTextBoxValid(dateOfBirthTextBox.Text))
            {
                ageLabel.Text = "";
                formattedAgeLabel.Text = "";
            }
            else
            {
                ageLabel.Text = "Age";
                DateTime dt = DateTime.ParseExact(dateOfBirthTextBox.Text, "yyyy MM dd", 
                                                    System.Globalization.CultureInfo.InvariantCulture);

                formattedAgeLabel.Text = Utilities.CalculateAge(dt, DateTime.Now).ToString();
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            if (initialized == false)
                return;

            soldier.HasUnsavedChanges = true;
        }

        private bool AllRequiredFieldsValid()
        {
            bool valid = true;
            valid &= lastNameTextbox.Text != "";
            valid &= firstNameTextbox.Text != "";
            valid &= middleInitialTextbox.Text != "";
            valid &= dateOfBirthValid;
            valid &= dateOfRankValid;

            return valid;
        }

        private void FlagMissingRequiredFields()
        {
            List<string> requiredFieldsMissing = new List<string>();

            if (rankingCombobox.SelectedIndex < 0)
            {
                rankLabel.ForeColor = Color.Red;
                requiredFieldsMissing.Add("-  Rank");
            }
            else
            {
                rankLabel.ForeColor = Color.Black;
            }

            if (lastNameTextbox.Text == "")
            {
                lastNameLabel.ForeColor = Color.Red;
                requiredFieldsMissing.Add("-  Last Name");
            }
            else
            {
                lastNameLabel.ForeColor = Color.Black;
            }

            if (firstNameTextbox.Text == "")
            {
                firstNameLabel.ForeColor = Color.Red;
                requiredFieldsMissing.Add("-  First Name");
            }
            else
            {
                firstNameLabel.ForeColor = Color.Black;
            }

            if (middleInitialTextbox.Text == "")
            {
                middleInitialLabel.ForeColor = Color.Red;
                requiredFieldsMissing.Add("-  Middle Initial");
            }
            else
            {
                middleInitialLabel.ForeColor = Color.Black;
            }


            if (!dateOfBirthValid)
            {
                dateOfBirthLabel.ForeColor = Color.Red;
                requiredFieldsMissing.Add("-  Date of Birth");
            }
            else
            {
                dateOfBirthLabel.ForeColor = Color.Black;
            }

            if (!dateOfRankValid)
            {
                dateOfRankLabel.ForeColor = Color.Red;
                requiredFieldsMissing.Add("-  Date of Rank");
            }
            else
            {
                dateOfRankLabel.ForeColor = Color.Black;
            }

            if (requiredFieldsMissing.Count == 0)
                return;

            string message = "Please correct the following items:\n";
            foreach (string item in requiredFieldsMissing)
            {
                message += item + "\n";
            }

            string caption = "Missing required fields.";
           CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
           CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

            CQPMessageBox.Show(message, caption, buttons, icon);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool saved = SaveSoldier();
            if (saved == true)
            SaveLocation();
            this.Dispose();
        }

        private bool SaveSoldier()
        {
            if (!soldier.HasUnsavedChanges)
                return true;

            if (AllRequiredFieldsValid() == false)
            {
                FlagMissingRequiredFields();
                return false;
            }

            try
            {
                SaveDialogValuesToSoldier();
                //soldierPictureBox.Image.Dispose();
                soldier.Save();
                soldierPictureBox.Image = soldier.Picture;
                soldier.HasUnsavedChanges = false;
            }
            catch (DataStoreFailedException ex)
            {
                CQPMessageBox.Show("An error occured while attempting to save the Soldier.", "Error",
                CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                throw new Exception("An error occurred attempting to save the soldier.", ex);
            }

            return true;
        }

        private void SaveDialogValuesToSoldier()
        {
            soldier.Rank = (Ranking)(rankingCombobox.SelectedIndex + 1);
            soldier.LastName = lastNameTextbox.Text;
            soldier.FirstName = firstNameTextbox.Text;
            soldier.MiddleInitial = middleInitialTextbox.Text[0];

            if (dateOfBirthValid)
            {
                if(dateOfBirthTextBox.Text != "        ")
                    soldier.DateOfBirth = DateTime.ParseExact(dateOfBirthTextBox.Text, "yyyy MM dd",
                                               System.Globalization.CultureInfo.InvariantCulture);
                else if (dateOfBirthTextBox.Text == "        ")
                    soldier.DateOfBirth = new DateTime(0);
            }

            if (dateOfRankValid)
            {
                if (dateOfRankTextBox.Text != "        ")
                    soldier.DateOfRank = DateTime.ParseExact(dateOfRankTextBox.Text, "yyyy MM dd",
                                                System.Globalization.CultureInfo.InvariantCulture);
                else if (dateOfRankTextBox.Text == "        ")
                    soldier.DateOfRank = new DateTime(0);
            }

            int battalionSelectedIndex = battalionCombobox.SelectedIndex;
            if (battalionSelectedIndex == -1)
                soldier.UnitHierarchy.battalionID = -1;
            else
                soldier.UnitHierarchy.battalionID = Convert.ToInt32(battalionCombobox.SelectedValue);

            soldier.UnitHierarchy.battalionName = battalionCombobox.Text;
            soldier.UnitHierarchy.unitID = Convert.ToInt32(unitNumberCombobox.SelectedValue);
            soldier.UnitHierarchy.unitDesignatorID = Convert.ToInt32(unitDesignatorCombobox.SelectedValue);
            soldier.UnitHierarchy.platoonID = Convert.ToInt32(platoonNumberCombobox.SelectedValue);
            soldier.UnitHierarchy.squadID = Convert.ToInt32(squadSectionNumberCombobox.SelectedValue);

            soldier.OtherStatus = otherTextbox.Text;


            if (soldierImageChanged == true && soldierCustomImage == true)
            {
                soldier.hasCustomImage = true;
                soldier.NewPictureFilename = soldierPictureBox.ImageLocation;
            }
            else if (soldierImageChanged == true && soldierCustomImage == false)
            {
                soldier.hasCustomImage = false;
                soldier.NewPictureFilename = "";
            }

            foreach( Control control in statusCheckBoxesLayoutTable.Controls )
            {
                if ( control.GetType().ToString() != "System.Windows.Forms.CheckBox" )
                    continue;

                CheckBox checkBox = (CheckBox) control;

                foreach (int statusID in soldier.Statuses.Keys)
                {
                    if (soldier.Statuses[statusID].statusString == checkBox.Text)
                    {
                        if (checkBox.Checked == true)
                            soldier.Statuses[statusID].applies = true;
                        else
                            soldier.Statuses[statusID].applies = false;

                        continue;
                    }
                }
            }
        }

        private void soldierPicturebox_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an image file...";
            openFileDialog.Filter = "Image Files|*.jpg; *.jpeg; *.bmp; *.png; *.gif";
            openFileDialog.RestoreDirectory = true;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            string filename = openFileDialog.FileName;
            Image image = new Bitmap(filename);
            soldierPictureBox.Image = image;
            soldierPictureBox.ImageLocation = filename;

            if (image.Width > 75 || image.Height > 75)
                soldierPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            soldierCustomImage = true;
            soldierImageChanged = true;
            ValueChanged(null, null);
        }


        private void rankingCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (soldierCustomImage == true)
            {
                ValueChanged(null, null);
                return;
            }

            int selectedIndex = rankingCombobox.SelectedIndex;
            soldierPictureBox.Image = rankingImages[selectedIndex];

            ValueChanged(null, null);
            
        }


        private void EditSoldierDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (soldier.HasUnsavedChanges)
            {
                DialogResult result = DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);

                if (result == DialogResult.Yes)
                {
                    SaveSoldier();
                    if (!dateOfBirthValid || !dateOfRankValid)
                        e.Cancel = true;
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
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
                    Properties.Settings.Default.EditSoldierDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.EditSoldierDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.EditSoldierDialogSize = bounds.Size;
            Properties.Settings.Default.EditSoldierDialogLocation = bounds.Location;
            Properties.Settings.Default.EditSoldierDialogHeight = bounds.Height;
            Properties.Settings.Default.EditSoldierDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }

        private bool dateTimeMaskedTextBoxValid(string dateTimeString)
        {
            if (dateTimeString == "        ")
                return false;

            DateTime dateTime;

            try
            {
                dateTime = DateTime.ParseExact(dateTimeString, "yyyy MM dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return false;
            }

            if (dateTime.Date >= DateTime.Now.Date)
                return false;

            return true;
        }

        private void dateOfBirthTextBox1_Leave(object sender, EventArgs e)
        {
            string dateTimeString = dateOfBirthTextBox.Text;
            bool isValidDateTime = dateTimeMaskedTextBoxValid(dateTimeString);

            if (isValidDateTime)
            {
                dateOfBirthValid = true;
                dateOfBirthLabel.ForeColor = Color.Black;

                DateTime dt = Convert.ToDateTime(dateTimeString);
                formattedAgeLabel.Text = Utilities.CalculateAge(dt, DateTime.Now).ToString();
            }
            else
            {
                if (dateTimeString == "        ")
                {
                    dateOfBirthValid = true;
                    dateOfBirthLabel.ForeColor = Color.Black;
                    formattedAgeLabel.Text = "";
                }
                else
                {
                    dateOfBirthValid = false;
                    dateOfBirthLabel.ForeColor = Color.Red;
                    formattedAgeLabel.Text = "?";
                }
            }
        }

        private void dateOfRankTextBox_Leave(object sender, EventArgs e)
        {
            string dateTimeString = dateOfRankTextBox.Text;
            bool isValidDateTime = dateTimeMaskedTextBoxValid(dateTimeString);

            if (isValidDateTime)
            {
                dateOfRankValid = true;
                dateOfRankLabel.ForeColor = Color.Black;
                ValueChanged(null, null);
            }
            else
            {
                if (dateTimeString == "        ")
                {
                    dateOfRankValid = true;
                    dateOfRankLabel.ForeColor = Color.Black;
                }
                else
                {
                    dateOfRankValid = false;
                    dateOfRankLabel.ForeColor = Color.Red;
                }
            }
        }

        private void removeImageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "Are you sure you want to remove the soldier's image?";
            string caption = "Remove soldier image?";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Warning;

            List<string> buttonText = new List<string> { "Remove Image", "No" };

            DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icon);

            if (result == DialogResult.No)
                return;

            soldierCustomImage = false;
            soldierImageChanged = true;

            rankingCombobox_SelectedIndexChanged(null, null);
        }

        private void dateOfBirthTextBox_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                dateOfBirthTextBox.Select(0, 0);
            });  
        }

        private void dateOfRankTextBox_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                dateOfRankTextBox.Select(0, 0);
            });  
        }

        private void EditSoldierDialog_Load(object sender, EventArgs e)
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.EditSoldierDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.EditSoldierDialogLocation, Properties.Settings.Default.EditSoldierDialogSize);
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
