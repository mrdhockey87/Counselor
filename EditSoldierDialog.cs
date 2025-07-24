using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

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

        /// <summary>
        /// Formats text with proper capitalization for unit hierarchy entries
        /// </summary>
        /// <param name="text">The text to format</param>
        /// <returns>Formatted text with first letter capitalized and rest lowercase for alphabetic entries</returns>
        private string FormatUnitHierarchyText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            // Check if the text contains only letters (and possibly spaces/hyphens)
            if (text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-'))
            {
                // Split the text into individual words
                string[] words = text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                
                if (words.Length == 0)
                    return text;

                // Format each word individually with proper title case
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                List<string> formattedWords = new List<string>();
                
                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i];
                    
                    // Handle words with hyphens by splitting and formatting each part
                    if (word.Contains("-"))
                    {
                        string[] hyphenParts = word.Split('-');
                        for (int j = 0; j < hyphenParts.Length; j++)
                        {
                            if (!string.IsNullOrWhiteSpace(hyphenParts[j]))
                            {
                                hyphenParts[j] = textInfo.ToTitleCase(hyphenParts[j].ToLower());
                            }
                        }
                        formattedWords.Add(string.Join("-", hyphenParts));
                    }
                    else
                    {
                        formattedWords.Add(textInfo.ToTitleCase(word.ToLower()));
                    }
                }
                
                // Join the formatted words back together with single spaces
                return string.Join(" ", formattedWords);
            }
            
            // For mixed alphanumeric or numeric entries, return as-is
            return text;
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

            // Set date of birth using CQPDatePicker
            if (soldier.DateOfBirth.Ticks != 0)
            {
                dateOfBirthPicker.SetDate(soldier.DateOfBirth);
                
                int age = Utilities.CalculateAge(soldier.DateOfBirth, DateTime.Now);
                formattedAgeLabel.Text = age.ToString();
            }
            else
            {
                dateOfBirthPicker.ClearDate();
                formattedAgeLabel.Text = "";
            }

            // Set date of rank using CQPDatePicker
            if (soldier.DateOfRank.Ticks != 0)
            {
                dateOfRankPicker.SetDate(soldier.DateOfRank);
            }
            else
            {
                dateOfRankPicker.ClearDate();
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

        private void unitNumberCombobox_TextChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }

        private void unitDesignatorCombobox_TextChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }

        private void platoonNumberCombobox_TextChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }

        private void squadSectionNumberCombobox_TextChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }

        private void dateOfBirthPicker_ValueChanged(object sender, EventArgs e)
        {
            if (!initialized)
                return;

            if (dateOfBirthPicker.HasValidDate())
            {
                dateOfBirthValid = true;
                dateOfBirthLabel.ForeColor = Color.Black;

                DateTime dt = dateOfBirthPicker.GetDate();
                int age = Utilities.CalculateAge(dt, DateTime.Now);
                formattedAgeLabel.Text = age.ToString();
                ageLabel.Text = "Age";
            }
            else if (dateOfBirthPicker.IsBlank())
            {
                dateOfBirthValid = true;
                dateOfBirthLabel.ForeColor = Color.Black;
                formattedAgeLabel.Text = "";
                ageLabel.Text = "";
            }
            else
            {
                dateOfBirthValid = false;
                dateOfBirthLabel.ForeColor = Color.Red;
                formattedAgeLabel.Text = "?";
                ageLabel.Text = "Age";
            }

            ValueChanged(sender, e);
        }

        private void dateOfRankPicker_ValueChanged(object sender, EventArgs e)
        {
            if (!initialized)
                return;

            if (dateOfRankPicker.HasValidDate())
            {
                dateOfRankValid = true;
                dateOfRankLabel.ForeColor = Color.Black;
            }
            else if (dateOfRankPicker.IsBlank())
            {
                dateOfRankValid = true;
                dateOfRankLabel.ForeColor = Color.Black;
            }
            else
            {
                dateOfRankValid = false;
                dateOfRankLabel.ForeColor = Color.Red;
            }

            ValueChanged(sender, e);
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
            valid &= IsDateValid(dateOfBirthPicker);
            valid &= IsDateValid(dateOfRankPicker);

            return valid;
        }

        private bool IsDateValid(CQPDatePicker datePicker)
        {
            // Date is valid if it's either blank (allowed) or contains a valid date
            return datePicker.IsBlank() || datePicker.HasValidDate();
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


            if (!IsDateValid(dateOfBirthPicker))
            {
                dateOfBirthLabel.ForeColor = Color.Red;
                requiredFieldsMissing.Add("-  Date of Birth");
            }
            else
            {
                dateOfBirthLabel.ForeColor = Color.Black;
            }

            if (!IsDateValid(dateOfRankPicker))
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

            // Handle date of birth using CQPDatePicker
            if (dateOfBirthPicker.HasValidDate())
            {
                soldier.DateOfBirth = dateOfBirthPicker.GetDate();
            }
            else
            {
                soldier.DateOfBirth = new DateTime(0);
            }

            // Handle date of rank using CQPDatePicker
            if (dateOfRankPicker.HasValidDate())
            {
                soldier.DateOfRank = dateOfRankPicker.GetDate();
            }
            else
            {
                soldier.DateOfRank = new DateTime(0);
            }

            // Format all unit hierarchy text before processing to ensure consistent formatting
            // This applies to both existing entries and new custom entries
            string formattedBattalionText = FormatUnitHierarchyText(battalionCombobox.Text);
            string formattedUnitText = FormatUnitHierarchyText(unitNumberCombobox.Text);
            string formattedUnitDesignatorText = FormatUnitHierarchyText(unitDesignatorCombobox.Text);
            string formattedPlatoonText = FormatUnitHierarchyText(platoonNumberCombobox.Text);
            string formattedSquadSectionText = FormatUnitHierarchyText(squadSectionNumberCombobox.Text);

            // Update ComboBox text with formatted versions for user feedback
            battalionCombobox.Text = formattedBattalionText;
            unitNumberCombobox.Text = formattedUnitText;
            unitDesignatorCombobox.Text = formattedUnitDesignatorText;
            platoonNumberCombobox.Text = formattedPlatoonText;
            squadSectionNumberCombobox.Text = formattedSquadSectionText;

            // Handle Unit Hierarchy with custom entries and similarity checking
            UnitHierarchyModel.UnitHierarchy newUnitHierarchy = new UnitHierarchyModel.UnitHierarchy();

            // Battalion handling
            int battalionSelectedIndex = battalionCombobox.SelectedIndex;
            if (battalionSelectedIndex == -1)
                newUnitHierarchy.battalionID = -1;
            else
                newUnitHierarchy.battalionID = Convert.ToInt32(battalionCombobox.SelectedValue);
            
            newUnitHierarchy.battalionName = formattedBattalionText;

            // Handle other hierarchy components with prompt
            string customUnitName = null;
            string customUnitDesignatorName = null;
            string customPlatoonName = null;
            string customSquadSectionName = null;

            // Unit handling with prompt
            if (unitNumberCombobox.SelectedValue != null)
            {
                newUnitHierarchy.unitID = Convert.ToInt32(unitNumberCombobox.SelectedValue);
            }
            else if (!string.IsNullOrWhiteSpace(formattedUnitText))
            {
                int promptResult = PromptForEntryAction(formattedUnitText, "Unit");
                if (promptResult == 2) return; // User cancelled

                if (promptResult == 1) // Use existing
                {
                    // For simplicity, just create new - could enhance with selection dialog later
                    newUnitHierarchy.unitID = -1;
                    customUnitName = formattedUnitText;
                }
                else // Create new
                {
                    newUnitHierarchy.unitID = -1;
                    customUnitName = formattedUnitText;
                }
            }

            // Unit Designator handling with prompt
            if (unitDesignatorCombobox.SelectedValue != null)
            {
                newUnitHierarchy.unitDesignatorID = Convert.ToInt32(unitDesignatorCombobox.SelectedValue);
            }
            else if (!string.IsNullOrWhiteSpace(formattedUnitDesignatorText))
            {
                int promptResult = PromptForEntryAction(formattedUnitDesignatorText, "Unit Designator");
                if (promptResult == 2) return; // User cancelled

                if (promptResult == 1) // Use existing
                {
                    newUnitHierarchy.unitDesignatorID = -1;
                    customUnitDesignatorName = formattedUnitDesignatorText;
                }
                else // Create new
                {
                    newUnitHierarchy.unitDesignatorID = -1;
                    customUnitDesignatorName = formattedUnitDesignatorText;
                }
            }

            // Platoon handling with prompt
            if (platoonNumberCombobox.SelectedValue != null)
            {
                newUnitHierarchy.platoonID = Convert.ToInt32(platoonNumberCombobox.SelectedValue);
            }
            else if (!string.IsNullOrWhiteSpace(formattedPlatoonText))
            {
                int promptResult = PromptForEntryAction(formattedPlatoonText, "Platoon");
                if (promptResult == 2) return; // User cancelled

                if (promptResult == 1) // Use existing
                {
                    newUnitHierarchy.platoonID = -1;
                    customPlatoonName = formattedPlatoonText;
                }
                else // Create new
                {
                    newUnitHierarchy.platoonID = -1;
                    customPlatoonName = formattedPlatoonText;
                }
            }

            // Squad/Section handling with prompt
            if (squadSectionNumberCombobox.SelectedValue != null)
            {
                newUnitHierarchy.squadID = Convert.ToInt32(squadSectionNumberCombobox.SelectedValue);
            }
            else if (!string.IsNullOrWhiteSpace(formattedSquadSectionText))
            {
                int promptResult = PromptForEntryAction(formattedSquadSectionText, "Squad/Section");
                if (promptResult == 2) return; // User cancelled

                if (promptResult == 1) // Use existing
                {
                    newUnitHierarchy.squadID = -1;
                    customSquadSectionName = formattedSquadSectionText;
                }
                else // Create new
                {
                    newUnitHierarchy.squadID = -1;
                    customSquadSectionName = formattedSquadSectionText;
                }
            }

            // Create or get the unit hierarchy ID using the enhanced method
            int unitHierarchyID = UnitHierarchyModel.CreateUnitHierarchyWithCustomEntries(
                newUnitHierarchy, 
                customUnitName, 
                customUnitDesignatorName, 
                customPlatoonName, 
                customSquadSectionName);

            newUnitHierarchy.unitHierarchyID = unitHierarchyID;
            soldier.UnitHierarchy = newUnitHierarchy;

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

        /// <summary>
        /// Prompts user to create new or use existing entry
        /// </summary>
        /// <param name="entryText">The text entered by user</param>
        /// <param name="entryType">Type of entry for display purposes</param>
        /// <returns>Returns: 0=Create New, 1=Use Existing, 2=Cancel</returns>
        private int PromptForEntryAction(string entryText, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryText))
                return 0; // Create new if empty

            string message = $"You entered: \"{entryText}\"\n\n";
            message += $"Would you like to:\n";
            message += $"• Create a new {entryType} entry\n";
            message += $"• Change to an existing {entryType} entry\n";
            message += $"• Cancel and edit your entry";

            string caption = $"New {entryType} Entry";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNoCancel;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Question;

            List<string> buttonText = new List<string>();
            buttonText.Add("Create New");
            buttonText.Add("Use Existing");  
            buttonText.Add("Cancel");

            DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icon);

            if (result == DialogResult.Yes) return 0; // Create new
            if (result == DialogResult.No) return 1;  // Use existing
            if (result == DialogResult.Cancel) return 2; // Cancel
            
            return 0; // Default to create new
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

        private void removeImageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "Are you sure you want to remove the soldier's image?";
            string caption = "Remove soldier image?";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Warning;

            List<string> buttonText = new List<string>();
            buttonText.Add("Remove Image");
            buttonText.Add("No");

            DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, buttonText, icon);

            if (result == DialogResult.No)
                return;

            soldierCustomImage = false;
            soldierImageChanged = true;

            rankingCombobox_SelectedIndexChanged(null, null);
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
