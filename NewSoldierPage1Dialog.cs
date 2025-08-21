using CounselQuickPlatinum.CustomExtensions;
using CounselQuickPlatinum.UnitHierarchyHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            cqpDateOfRank.DateValueChanged += (sender, e) => {
                // This only fires when user changes to a different valid date
                // Won't fire for invalid/incomplete dates or if they select the same original date
                ValueChanged(sender, e);
            };
            cqpDateOfBirth.DateValueChanged += (sender, e) => {
                // This only fires when user changes to a different valid date
                // Won't fire for invalid/incomplete dates or if they select the same original date
                ValueChanged(sender, e);
                string dateTimeString = cqpDateOfBirth.GetDate().ToString();// dateOfBirthTextBox.Text;
                if (dateTimeString != "0000 00 00")
                {
                    dateOfBirthValid = true;
                    dateOfBirthLabel.ForeColor = Color.Black;

                    formattedAgeLabel.Text
                        = "" + Utilities.CalculateAge(Convert.ToDateTime(dateTimeString), DateTime.Now);
                }
                else
                {
                    dateOfBirthValid = false;
                    dateOfBirthLabel.ForeColor = Color.Red;
                    formattedAgeLabel.Text = "?";
                }
            };
            cqpDateOfRank.SetDateString("");
            cqpDateOfBirth.SetDateString("");
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
            battalionCombobox.SelectedIndex = -1;

            unitNumberCombobox.DataSource = unitInformation.Tables["units"];
            unitNumberCombobox.ValueMember = "unitid";
            unitNumberCombobox.DisplayMember = "unitname";
            unitNumberCombobox.SelectedIndex = -1;

            unitDesignatorCombobox.DataSource = unitInformation.Tables["unitdesignators"];
            unitDesignatorCombobox.ValueMember = "unitdesignatorid";
            unitDesignatorCombobox.DisplayMember = "unitdesignatorname";
            unitDesignatorCombobox.SelectedIndex = -1;

            platoonNumberCombobox.DataSource = unitInformation.Tables["platoons"];
            platoonNumberCombobox.ValueMember = "platoonid";
            platoonNumberCombobox.DisplayMember = "platoonname";
            platoonNumberCombobox.SelectedIndex = -1;

            squadSectionNumberCombobox.DataSource = unitInformation.Tables["squadsections"];
            squadSectionNumberCombobox.ValueMember = "squadsectionid";
            squadSectionNumberCombobox.DisplayMember = "squadsectionname";
            squadSectionNumberCombobox.SelectedIndex = -1;
        }


        private void unitNumberCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }


        private void platoonNumberCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
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

            string dateOfBirthString = cqpDateOfBirth.GetDateString();
            if (!string.IsNullOrEmpty(dateOfBirthString))
            {
                soldier.DateOfBirth = DateTime.ParseExact(dateOfBirthString, "yyyy MM dd",
                                                            System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                soldier.DateOfBirth = new DateTime(0);
            }

            string dateOfRankString = cqpDateOfRank.GetDateString();
            if (!string.IsNullOrEmpty(dateOfRankString))
            {
                soldier.DateOfRank = DateTime.ParseExact(dateOfRankString, "yyyy MM dd",
                                                            System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                soldier.DateOfRank = new DateTime(0);
            }


            //soldier.SquadSectionID = Convert.ToInt32(squadSectionNumberCombobox.SelectedValue);
            int battalionComboboxIndex = battalionCombobox.SelectedIndex;

            if (battalionComboboxIndex == -1)
                soldier.UnitHierarchy.battalionID = -1;
            else
                soldier.UnitHierarchy.battalionID = Convert.ToInt32(battalionCombobox.SelectedValue);
            
            soldier.UnitHierarchy.battalionName = battalionCombobox.Text;
            soldier.UnitHierarchy.unitID = Convert.ToInt32(unitNumberCombobox.SelectedValue);
            soldier.UnitHierarchy.unitDesignatorID = Convert.ToInt32(unitDesignatorCombobox.SelectedValue);
            soldier.UnitHierarchy.platoonID = Convert.ToInt32(platoonNumberCombobox.SelectedValue);
            soldier.UnitHierarchy.squadID = Convert.ToInt32(squadSectionNumberCombobox.SelectedValue);

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

                if (result == DialogResult.Cancel)
                    return;
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
            if (unitNumberCombobox.SelectedIndex < 0)
                return false;
            if (unitDesignatorCombobox.SelectedIndex < 0)
                return false;
            if (platoonNumberCombobox.SelectedIndex < 0)
                return false;
            if (squadSectionNumberCombobox.SelectedIndex < 0)
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

            if (unitNumberCombobox.SelectedIndex < 0)
                unitLabel.ForeColor = Color.Red;
            else
                unitLabel.ForeColor = Color.Black;

            if (unitDesignatorCombobox.SelectedIndex < 0)
                unitDesignatorCombobox.ForeColor = Color.Red;
            else
                unitDesignatorCombobox.ForeColor = Color.Black;

            if (platoonNumberCombobox.SelectedIndex < 0)
                platoonLabel.ForeColor = Color.Red;
            else
                platoonLabel.ForeColor = Color.Black;

            if (squadSectionNumberCombobox.SelectedIndex < 0)
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

        private void cqpDateOfBirth_Leave(object sender, EventArgs e)
        {
            string dateTimeString = cqpDateOfBirth.GetDate().ToString();// dateOfBirthTextBox.Text;
            if (dateTimeString != "0000 00 00")
            {
                dateOfBirthValid = true;
                dateOfBirthLabel.ForeColor = Color.Black;

                formattedAgeLabel.Text
                    = "" + Utilities.CalculateAge(Convert.ToDateTime(dateTimeString), DateTime.Now);
            }
            else
            {
                dateOfBirthValid = false;
                dateOfBirthLabel.ForeColor = Color.Red;
                formattedAgeLabel.Text = "?";
            }
        }

        private void unitDesignatorCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }

        private void battalionCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChanged(null, null);
        }
        /// Refreshes all unit ComboBoxes by reloading data from UnitHierarchyModel.
        /// Call this after creating new unit hierarchy items to update the ComboBox lists. mdail 8-19-19
        /// </summary>
        private void RefreshUnitComboboxes()
        {
            // Store current selections
            int battalionSelected = battalionCombobox.SelectedIndex;
            int unitSelected = unitNumberCombobox.SelectedIndex;
            int unitDesignatorSelected = unitDesignatorCombobox.SelectedIndex;
            int platoonSelected = platoonNumberCombobox.SelectedIndex;
            int squadSectionSelected = squadSectionNumberCombobox.SelectedIndex;

            // Reload the data
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

            // Rebind the ComboBoxes
            battalionCombobox.DataSource = unitInformation.Tables["battalions"];
            unitNumberCombobox.DataSource = unitInformation.Tables["units"];
            unitDesignatorCombobox.DataSource = unitInformation.Tables["unitdesignators"];
            platoonNumberCombobox.DataSource = unitInformation.Tables["platoons"];
            squadSectionNumberCombobox.DataSource = unitInformation.Tables["squadsections"];

            // Always restore selections, even if they were -1 (no selection)
            battalionCombobox.SelectedIndex = battalionSelected;
            unitNumberCombobox.SelectedIndex = unitSelected;           // Now -1 will be restored properly
            unitDesignatorCombobox.SelectedIndex = unitDesignatorSelected;
            platoonNumberCombobox.SelectedIndex = platoonSelected;
            squadSectionNumberCombobox.SelectedIndex = squadSectionSelected;
        }

        #region Refactored ComboBox Leave Event Handlers
        private void battalionCombobox_Leave(object sender, EventArgs e)
        {
            string CurrentText = battalionCombobox.Text.Trim() ?? string.Empty;
            CurrentText = CurrentText.ToSelectiveTitleCase();
            battalionCombobox.Text = CurrentText;
            var config = new UnitHierarchyComboBoxConfig(
                "Battalion",
                UnitHierarchyModel.BattalionNameExists,
                UnitHierarchyModel.CreateBattalion,
                battalionCombobox
            );

            UnitHierarchyComboBoxHelper.HandleUnitHierarchyComboBoxLeave(config, RefreshUnitComboboxes);
        }
        private void unitNumberCombobox_Leave(object sender, EventArgs e)
        {
            string CurrentText = unitNumberCombobox.Text.Trim() ?? string.Empty;
            CurrentText = CurrentText.ToSelectiveTitleCase();
            unitNumberCombobox.Text = CurrentText;
            var config = new UnitHierarchyComboBoxConfig(
                "Unit",
                UnitHierarchyModel.UnitNameExists,
                UnitHierarchyModel.CreateUnit,
                unitNumberCombobox
            );

            UnitHierarchyComboBoxHelper.HandleUnitHierarchyComboBoxLeave(config, RefreshUnitComboboxes);
        }

        private void unitDesignatorCombobox_Leave(object sender, EventArgs e)
        {
            string CurrentText = unitNumberCombobox.Text.Trim() ?? string.Empty;
            CurrentText = CurrentText.ToSelectiveTitleCase();
            unitNumberCombobox.Text = CurrentText;
            var config = new UnitHierarchyComboBoxConfig(
                "Unit Designator",
                UnitHierarchyModel.UnitDesignatorNameExists,
                UnitHierarchyModel.CreateUnitDesignator,
                unitDesignatorCombobox
            );

            UnitHierarchyComboBoxHelper.HandleUnitHierarchyComboBoxLeave(config, RefreshUnitComboboxes);
        }

        private void platoonNumberCombobox_Leave(object sender, EventArgs e)
        {
            string CurrentText = platoonNumberCombobox.Text.Trim() ?? string.Empty;
            CurrentText = CurrentText.ToSelectiveTitleCase();
            platoonNumberCombobox.Text = CurrentText;
            var config = new UnitHierarchyComboBoxConfig(
                "Platoon",
                UnitHierarchyModel.PlatoonNameExists,
                UnitHierarchyModel.CreatePlatoon,
                platoonNumberCombobox
            );

            UnitHierarchyComboBoxHelper.HandleUnitHierarchyComboBoxLeave(config, RefreshUnitComboboxes);
        }

        private void squadSectionNumberCombobox_Leave(object sender, EventArgs e)
        {
            //Problem: The squad sections is connected to the platoon, so if the platoon is not selected, it will not work.
            //so it always sets the platoomID to 2 as I can for the life of me see where the platoonID in the squadscetons table is
            //ever used. and in the table it is 1 for the first half and 2 for the second half. mdail 8-19-25
            string CurrentText = squadSectionNumberCombobox.Text.Trim() ?? string.Empty;
            CurrentText = CurrentText.ToSelectiveTitleCase();
            squadSectionNumberCombobox.Text = CurrentText;
            var config = new UnitHierarchyComboBoxConfig(
                "Squad/Section",
                UnitHierarchyModel.SquadSectionNameExists,
                UnitHierarchyModel.CreateSquadSection,
                squadSectionNumberCombobox
            );

            UnitHierarchyComboBoxHelper.HandleUnitHierarchyComboBoxLeave(config, RefreshUnitComboboxes);
        }
        #endregion        
    }
}
