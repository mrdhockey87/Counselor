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
    public partial class NewSoldierPage1Dialog : Form
    {
        bool dateOfBirthValid = true;
        bool dateOfRankValid = true;
        Soldier soldier;
        NewSoldierPage2Dialog page2;
        bool soldierPictureChangedToCustom;
        List<Image> rankingImages;


        public NewSoldierPage1Dialog()
        {
            soldier = new Soldier();
            
            InitializeComponent();
            InitializeControls();

            soldier.HasUnsavedChanges = false;
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



        private void InitializeControls()
        {
            rankingImages = RankingModel.GetRankingImages();

            PopulateRankingComboBox();
            InitializeDateControls();
            PopulateUnitComboboxes();

            //soldierPictureBox.ImageLocation = SettingsModel.RankingImageDirectory + "NEW.png";
            soldierPictureBox.Image = (Image)CounselQuickPlatinum.Properties.Resources.NEW;
            soldierPictureChangedToCustom = false;
        }


        private void InitializeDateControls()
        {
            formattedAgeLabel.Text = "";
        }


        private void PopulateRankingComboBox()
        {
            DataTable rankingTable = RankingModel.GetRankingTable();
            rankingCombobox.DataSource = rankingTable;
            rankingCombobox.DisplayMember = "rankingabbreviation";
            rankingCombobox.ValueMember = "rankingid";

            Logger.Trace("About to select \"NEW\"");

            rankingCombobox.SelectedIndex = -1;
        }


        private void PopulateUnitComboboxes()
        {
            DataSet unitInformation;
            try
            {
                unitInformation = UnitHierarchyModel.GetAllUnitInfo();
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


        private void dateOfBirthPicker_ValueChanged(object sender, EventArgs e)
        {
            if (dateOfBirthPicker.HasValidDate())
            {
                dateOfBirthValid = true;
                dateOfBirthLabel.ForeColor = Color.Black;

                DateTime dt = dateOfBirthPicker.GetDate();
                int age = Utilities.CalculateAge(dt, DateTime.Now);
                formattedAgeLabel.Text = age.ToString();
            }
            else if (dateOfBirthPicker.IsBlank())
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

            ValueChanged(sender, e);
        }

        private void dateOfRankPicker_ValueChanged(object sender, EventArgs e)
        {
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

        private bool DateTimeMaskedTextBoxValid(string dateTimeString)
        {
            DateTime dateTime;

            // blank is okay
            if (dateTimeString == "        ")
                return true;

            // if something is there, validate it
            try
            {
                dateTime = DateTime.ParseExact(dateTimeString, "yyyy MM dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return false;
            }

            // make sure it's in the past
            if (dateTime.Date - TimeSpan.FromSeconds(1) >= DateTime.Now.Date)
                return false;

            return true;
        }


        private DialogResult PromptToSaveChanges()
        {
            //SaveChangesDialog.SaveChangesButtons buttons = SaveChangesDialog.SaveChangesButtons.DontSaveCancel;
            //SaveChangesDialog saveChangesDialog = new SaveChangesDialog(buttons);
            //DialogResult result = saveChangesDialog.ShowDialog();
            //return result;

            return DialogHelper.PromptToSaveChanges(SaveChangesButtons.DontSaveCancel);
        }


        private void ValueChanged(object sender, EventArgs e)
        {
            soldier.HasUnsavedChanges = true;
        }


        private void SaveDialogValuesToSoldier()
        {
            if (!IsDateValid(dateOfBirthPicker) || !IsDateValid(dateOfRankPicker))
                return;

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
            int battalionComboboxIndex = battalionCombobox.SelectedIndex;
            if (battalionComboboxIndex == -1)
                newUnitHierarchy.battalionID = -1;
            else
                newUnitHierarchy.battalionID = Convert.ToInt32(battalionCombobox.SelectedValue);
            
            newUnitHierarchy.battalionName = formattedBattalionText;

            // Handle other hierarchy components with similarity checking
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

            if (soldierPictureChangedToCustom == true)
            {
                //soldier.Picture = soldierPictureBox.Image;
                soldier.hasCustomImage = true;
                soldier.NewPictureFilename = soldierPictureBox.ImageLocation;
            }

            soldier.HasUnsavedChanges = false;
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (soldier.HasUnsavedChanges)
            {
                DialogResult result = PromptToSaveChanges();

                // TODO:  ?? SAVE?
                if (result == DialogResult.Cancel)
                    return;
                //else if (result == DialogResult.No)

                //DialogResult = DialogResult.Cancel;
                //this.Dispose();
            }

            DialogResult = DialogResult.Cancel;
            SaveLocation();
            this.Dispose();
        }


        private void NewSoldierPage1Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Visible == false)
                return;
            
            if (soldier.HasUnsavedChanges)
            {
                DialogResult result = PromptToSaveChanges();

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                SaveLocation();
                this.Dispose();
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
                    Properties.Settings.Default.NewSoldierPage1DialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.NewSoldierPage1DialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.NewSoldierPage1DialogSize = bounds.Size;
            Properties.Settings.Default.NewSoldierPage1DialogLocation = bounds.Location;
            Properties.Settings.Default.NewSoldierPage1DialogHeight = bounds.Height;
            Properties.Settings.Default.NewSoldierPage1DialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }


        private bool AllRequiredFieldsValid()
        {
            if (rankingCombobox.SelectedIndex < 0)
                return false;
            if (lastNameTextbox.Text == "")
                return false;
            if (firstNameTextbox.Text == "")
                return false;
            if (middleInitialTextbox.Text == "")
                return false;
            if (battalionCombobox.SelectedIndex < 0 && battalionCombobox.Text == "")
                return false;
            if (unitNumberCombobox.SelectedIndex < 0 && unitNumberCombobox.Text == "")
                return false;
            if (unitDesignatorCombobox.SelectedIndex < 0 && unitDesignatorCombobox.Text == "")
                return false;
            if (platoonNumberCombobox.SelectedIndex < 0 && platoonNumberCombobox.Text == "")
                return false;
            if (squadSectionNumberCombobox.SelectedIndex < 0 && squadSectionNumberCombobox.Text == "")
                return false;
            if (!IsDateValid(dateOfBirthPicker))
                return false;
            if (!IsDateValid(dateOfRankPicker))
                return false;

            return true;
        }


        private void FlagMissingRequiredFields()
        {
            requiredFieldLabel.ForeColor = Color.Red;

            if (rankingCombobox.SelectedIndex < 0)
                rankLabel.ForeColor = Color.Red;
            else
                rankLabel.ForeColor = Color.Black;

            if (lastNameTextbox.Text == "")
                lastNameLabel.ForeColor = Color.Red;
            else
                lastNameLabel.ForeColor = Color.Black;

            if (firstNameTextbox.Text == "")
                firstNameLabel.ForeColor = Color.Red;
            else
                firstNameLabel.ForeColor = Color.Black;

            if (middleInitialTextbox.Text == "")
                middleInitialLabel.ForeColor = Color.Red;
            else
                middleInitialLabel.ForeColor = Color.Black;

            if (battalionCombobox.SelectedIndex < 0
                    && battalionCombobox.Text == "")
                battalionLabel.ForeColor = Color.Red;
            else
                battalionLabel.ForeColor = Color.Black;

            if (unitNumberCombobox.SelectedIndex < 0 && unitNumberCombobox.Text == "")
                unitLabel.ForeColor = Color.Red;
            else
                unitLabel.ForeColor = Color.Black;

            if (unitDesignatorCombobox.SelectedIndex < 0 && unitDesignatorCombobox.Text == "")
                unitDesignatorCombobox.ForeColor = Color.Red;
            else
                unitDesignatorCombobox.ForeColor = Color.Black;

            if (platoonNumberCombobox.SelectedIndex < 0 && platoonNumberCombobox.Text == "")
                platoonLabel.ForeColor = Color.Red;
            else
                platoonLabel.ForeColor = Color.Black;

            if (squadSectionNumberCombobox.SelectedIndex < 0 && squadSectionNumberCombobox.Text == "")
                squadSectionLabel.ForeColor = Color.Red;
            else
                squadSectionLabel.ForeColor = Color.Black;

            if (!IsDateValid(dateOfBirthPicker))
                dateOfBirthLabel.ForeColor = Color.Red;
            else
                dateOfBirthLabel.ForeColor = Color.Black;

            if (!IsDateValid(dateOfRankPicker))
                dateOfRankLabel.ForeColor = Color.Red;
            else
                dateOfRankLabel.ForeColor = Color.Black;

            return;
        }


        private void soldierPictureBox_Click(object sender, EventArgs e)
        {
            string filename = DialogHelper.GetNewPictureFilename();
            if (filename == "")
                return;

            Image image = new Bitmap(filename);
            //soldierPictureBox.Image = image;
            soldierPictureBox.ImageLocation = filename;

            if (image.Width > 75 || image.Height > 75)
                soldierPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                soldierPictureBox.SizeMode = PictureBoxSizeMode.Normal;

            soldier.HasUnsavedChanges = true;
            soldierPictureChangedToCustom = true;
        }


        private void nextButton_Click(object sender, EventArgs e)
        {
            if (AllRequiredFieldsValid() == false)
            {
                FlagMissingRequiredFields();
                return;
            }

            SaveDialogValuesToSoldier();
            FlagMissingRequiredFields();
            requiredFieldLabel.ForeColor = Color.Black;

            //DialogResult = DialogResult.OK;
            
            this.Hide();
            //this.Visible = false;

            page2 = new NewSoldierPage2Dialog(this, soldier, rankingImages);
            page2.ShowDialog(this);
        }


        private void rankingCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (soldierPictureChangedToCustom == true)
            {
                ValueChanged(null, null);
                return;
            }

            int selectedIndex = rankingCombobox.SelectedIndex;

            if(selectedIndex > -1)
                soldierPictureBox.Image = rankingImages[selectedIndex];

            ValueChanged(null, null);
        }

        private void NewSoldierPage1Dialog_VisibleChanged(object sender, EventArgs e)
        {

            if (soldier.NewPictureFilename != "")
            {
                soldierPictureBox.ImageLocation = soldier.NewPictureFilename;
                Image image = new Bitmap(soldier.NewPictureFilename);
                //soldierPictureBox.Image = image;
                //soldierPictureBox.ImageLocation = filename;

                if (image.Width > 75 || image.Height > 75)
                    soldierPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    soldierPictureBox.SizeMode = PictureBoxSizeMode.Normal;
            }
            else
                soldierPictureBox.Image = rankingImages[((int)soldier.Rank) - 1];
        }

        private void NewSoldierPage1Dialog_Load(object sender, EventArgs e)
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.NewSoldierPage1DialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.NewSoldierPage1DialogLocation, Properties.Settings.Default.NewSoldierPage1DialogSize);
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

        private bool IsDateValid(CQPDatePicker datePicker)
        {
            // Date is valid if it's either blank (allowed) or contains a valid date
            return datePicker.IsBlank() || datePicker.HasValidDate();
        }
    }
}
