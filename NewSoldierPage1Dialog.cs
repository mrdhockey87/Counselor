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
        bool isFormattingComboBoxText = false; // Guard variable to prevent recursion


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
                // Use TextInfo.ToTitleCase which capitalizes the first letter of each word
                // and makes the rest lowercase
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                return textInfo.ToTitleCase(text.ToLower());
            }
            
            // For mixed alphanumeric or numeric entries, return as-is
            return text;
        }

        /// <summary>
        /// Formats the text in a ComboBox with proper capitalization
        /// </summary>
        /// <param name="comboBox">The ComboBox to format</param>
        private void FormatComboBoxText(ComboBox comboBox)
        {
            // Guard against recursion
            if (isFormattingComboBoxText)
                return;

            try
            {
                isFormattingComboBoxText = true;
                
                string originalText = comboBox.Text;
                int originalSelectionStart = comboBox.SelectionStart;
                
                string formattedText = FormatUnitHierarchyText(originalText);
                
                if (originalText != formattedText)
                {
                    comboBox.Text = formattedText;
                    // Restore cursor position, accounting for any length changes
                    int newPosition = Math.Min(originalSelectionStart, formattedText.Length);
                    comboBox.SelectionStart = newPosition;
                }
            }
            finally
            {
                isFormattingComboBoxText = false;
            }
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


        private void dateOfRankTextBox_Leave(object sender, EventArgs e)
        {
            string dateTimeString = dateOfRankTextBox.Text;
            if (DateTimeMaskedTextBoxValid(dateTimeString))
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


        private void dateOfBirthTextBox_Leave(object sender, EventArgs e)
        {
            string dateTimeString = dateOfBirthTextBox.Text;
            if (DateTimeMaskedTextBoxValid(dateTimeString))
            {
                dateOfBirthValid = true;
                dateOfBirthLabel.ForeColor = Color.Black;

                if(dateTimeString != "        ")
                    formattedAgeLabel.Text 
                        = ""+ Utilities.CalculateAge(Convert.ToDateTime(dateTimeString), DateTime.Now);
            }
            else
            {
                dateOfBirthValid = false;
                dateOfBirthLabel.ForeColor = Color.Red;
                formattedAgeLabel.Text = "?";
            }
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
            if (!dateOfBirthValid || !dateOfRankValid)
                return;

            soldier.Rank = (Ranking)(rankingCombobox.SelectedIndex + 1);
            soldier.LastName = lastNameTextbox.Text;
            soldier.FirstName = firstNameTextbox.Text;
            soldier.MiddleInitial = middleInitialTextbox.Text[0];

            string dateOfBirthString = dateOfBirthTextBox.Text;
            if (dateOfBirthString != "        ")
            {
                soldier.DateOfBirth = DateTime.ParseExact(dateOfBirthString, "yyyy MM dd",
                                                            System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                soldier.DateOfBirth = new DateTime(0);
            }

            string dateOfRankString = dateOfRankTextBox.Text;
            if (dateOfRankString != "        ")
            {
                soldier.DateOfRank = DateTime.ParseExact(dateOfRankString, "yyyy MM dd",
                                                            System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                soldier.DateOfRank = new DateTime(0);
            }


            // Handle Unit Hierarchy with custom entries
            UnitHierarchyModel.UnitHierarchy newUnitHierarchy = new UnitHierarchyModel.UnitHierarchy();

            // Battalion handling (already works with custom text)
            int battalionComboboxIndex = battalionCombobox.SelectedIndex;
            if (battalionComboboxIndex == -1)
                newUnitHierarchy.battalionID = -1;
            else
                newUnitHierarchy.battalionID = Convert.ToInt32(battalionCombobox.SelectedValue);
            
            newUnitHierarchy.battalionName = battalionCombobox.Text;

            // Handle other hierarchy components
            string customUnitName = null;
            string customUnitDesignatorName = null;
            string customPlatoonName = null;
            string customSquadSectionName = null;

            // Unit handling
            if (unitNumberCombobox.SelectedValue != null)
                newUnitHierarchy.unitID = Convert.ToInt32(unitNumberCombobox.SelectedValue);
            else
            {
                newUnitHierarchy.unitID = -1;
                customUnitName = unitNumberCombobox.Text;
            }

            // Unit Designator handling
            if (unitDesignatorCombobox.SelectedValue != null)
                newUnitHierarchy.unitDesignatorID = Convert.ToInt32(unitDesignatorCombobox.SelectedValue);
            else
            {
                newUnitHierarchy.unitDesignatorID = -1;
                customUnitDesignatorName = unitDesignatorCombobox.Text;
            }

            // Platoon handling
            if (platoonNumberCombobox.SelectedValue != null)
                newUnitHierarchy.platoonID = Convert.ToInt32(platoonNumberCombobox.SelectedValue);
            else
            {
                newUnitHierarchy.platoonID = -1;
                customPlatoonName = platoonNumberCombobox.Text;
            }

            // Squad/Section handling
            if (squadSectionNumberCombobox.SelectedValue != null)
                newUnitHierarchy.squadID = Convert.ToInt32(squadSectionNumberCombobox.SelectedValue);
            else
            {
                newUnitHierarchy.squadID = -1;
                customSquadSectionName = squadSectionNumberCombobox.Text;
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
            if (!dateOfBirthValid)
                return false;
            if (!dateOfRankValid)
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

            if (!dateOfBirthValid)
                dateOfBirthLabel.ForeColor = Color.Red;
            else
                dateOfBirthLabel.ForeColor = Color.Black;

            if (!dateOfRankValid)
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
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && !comboBox.DroppedDown)
            {
                FormatComboBoxText(comboBox);
            }
            ValueChanged(null, null);
        }

        private void unitDesignatorCombobox_TextChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && !comboBox.DroppedDown)
            {
                FormatComboBoxText(comboBox);
            }
            ValueChanged(null, null);
        }

        private void platoonNumberCombobox_TextChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && !comboBox.DroppedDown)
            {
                FormatComboBoxText(comboBox);
            }
            ValueChanged(null, null);
        }

        private void squadSectionNumberCombobox_TextChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && !comboBox.DroppedDown)
            {
                FormatComboBoxText(comboBox);
            }
            ValueChanged(null, null);
        }
    }
}
