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
    internal partial class NewSoldierPage2Dialog : Form
    {
        NewSoldierPage1Dialog pageOne;
        Soldier soldier;
        bool imageChangedToCustom;
        List<Image> rankingImages;


        internal NewSoldierPage2Dialog()
        {
            InitializeComponent();
        }

        internal NewSoldierPage2Dialog(NewSoldierPage1Dialog pageOne, Soldier soldier, List<Image> rankingImages)
        {
            this.pageOne = pageOne;
            this.soldier = soldier;
            this.rankingImages = rankingImages;

            InitializeComponent();
            PutAtSavedLocation();
            LoadFormValuesFromSoldierModel();

            imageChangedToCustom = false;
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.NewSoldierPage2DialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.NewSoldierPage2DialogLocation, Properties.Settings.Default.NewSoldierPage2DialogSize);
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


        private void LoadFormValuesFromSoldierModel()
        {
            //if (soldier.Picture != null)

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
                //soldierPictureBox.Image = Model.GetRankingImages()[((int)soldierModel.Rank) - 1];



            //soldier.HasUnsavedChanges = true;
            //soldierPictureChangedToCustom = true;

            //soldierPictureBox.Image = soldier.Picture;

            StringBuilder formattedRankName = new StringBuilder();
            //formattedRankName.Append(Utilities.RankingStringFromEnum(soldier.Rank));
            formattedRankName.Append(RankingModel.RankToString(soldier.Rank));
            formattedRankName.Append(" ");
            formattedRankName.Append(soldier.LastName);
            formattedRankName.Append(", ");
            formattedRankName.Append(soldier.FirstName);

            rankNameLabel.Text = formattedRankName.ToString();

            dateOfRankLabel.Text = "DOR: " + soldier.DateOfRank.ToString("yyyy MM dd");

            if (soldier.DateOfRank.Ticks == 0)
                dateOfRankLabel.Visible = false;

            if (soldier.DateOfBirth.Ticks == 0)
                ageLabel.Visible = false;
            else
                ageLabel.Text = "Age: " + Utilities.CalculateAge(soldier.DateOfBirth, DateTime.Now).ToString();

            int battalionID = soldier.UnitHierarchy.battalionID;
            //string battalionName = UnitHierarchyModel.GetBattalionName(battalionID);
            string battalionName = soldier.UnitHierarchy.battalionName;

            //int unitID = UnitHierarchyModel.GetUnitIDForPlatoonID(platoonID);
            int unitID = soldier.UnitHierarchy.unitID;
            string unitName = UnitHierarchyModel.GetUnitName(unitID);

            int unitDesignatorID = soldier.UnitHierarchy.unitDesignatorID;
            string unitDesignatorName = UnitHierarchyModel.GetUnitDesignatorName(unitDesignatorID);

            //int platoonID = UnitHierarchyModel.GetPlatoonIDForSquadSection(squadID);
            int platoonID = soldier.UnitHierarchy.platoonID;
            string platoonName = UnitHierarchyModel.GetPlatoonName(platoonID);

            int squadID = soldier.UnitHierarchy.squadID;
            string squadName = UnitHierarchyModel.GetSectionSquadName(squadID);

            battalionLabel.Text = battalionName;
            unitLabel.Text = unitName + " " + unitDesignatorName;
            platoonLabel.Text = platoonName + " Platoon";
            squadLabel.Text = squadName + " Squad";
        }
        

        private void NewSoldierPage2Dialog_VisibleChanged(object sender, EventArgs e)
        {
            LoadFormValuesFromSoldierModel();
        }


        private void ValueChanged(object sender, EventArgs e)
        {
            // TODO:  is this necessary?  Obviously there are already unsaved changes, 
            // as we came here from page one.  
            soldier.HasUnsavedChanges = true;
        }


        private void backButton_Click(object sender, EventArgs e)
        {
            SaveDialogValuesToSoldier();

            this.Hide();
            //this.Visible = false;
            //pageOne.Visible = true;
            pageOne.Show();
        }


        private void soldierPictureBox_Click(object sender, EventArgs e)
        {
            string filename = DialogHelper.GetNewPictureFilename();
            if (filename == "")
                return;

            Image image = new Bitmap(filename);
            soldierPictureBox.Image = image;
            soldierPictureBox.ImageLocation = filename;

            if (image.Width > 75 || image.Height > 75)
                soldierPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                soldierPictureBox.SizeMode = PictureBoxSizeMode.Normal;

            soldier.HasUnsavedChanges = true;
            imageChangedToCustom = true;
        }


        private void SaveDialogValuesToSoldier()
        {
            if (imageChangedToCustom == true)
            {
                //soldier.Picture = soldierPictureBox.Image;
                soldier.hasCustomImage = true;
                soldier.NewPictureFilename = soldierPictureBox.ImageLocation;
            }

            soldier.OtherStatus = otherTextbox.Text;

            foreach (Control control in statusCheckBoxesLayoutTable.Controls)
            {
                if (control.GetType().ToString() != "System.Windows.Forms.CheckBox")
                    continue;

                CheckBox checkBox = (CheckBox)control;

                //for (int i = 0; i < soldier.Statuses.Count; i++)
                //{
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
                //}
            }
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveSoldier();
        }


        private DialogResult PromptToSaveChanges()
        {
            //SaveChangesDialog saveChangesDialog = new SaveChangesDialog(SaveChangesDialog.SaveChangesButtons.SaveDontSaveCancel);
            //return saveChangesDialog.ShowDialog();
            return DialogHelper.PromptToSaveChanges(SaveChangesButtons.SaveDontSaveCancel);
        }


        private void SaveSoldier()
        {
            try
            {
                SaveDialogValuesToSoldier();
                soldier.Save();
            }
            catch (Exception ex)
            {
                string error = "An error occurred while trying to save the Soldier.";

                CQPMessageBox.Show(error, "Error saving",
                                CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                Logger.Error(error, ex);

                throw new Exception(error, ex);
            }

            pageOne.Dispose();
            this.Dispose();
            SaveLocation();
        }



        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (soldier.HasUnsavedChanges)
            {
                DialogResult result = PromptToSaveChanges();
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.OK)
                {
                    SaveSoldier();
                    this.Dispose();
                    pageOne.Dispose();
                    pageOne.DialogResult = DialogResult.OK;
                }
                else if (result == DialogResult.Ignore)
                {
                    this.Dispose();
                    pageOne.Dispose();
                    pageOne.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void NewSoldierPage2Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                    Properties.Settings.Default.NewSoldierPage2DialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.NewSoldierPage2DialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.NewSoldierPage2DialogSize = bounds.Size;
            Properties.Settings.Default.NewSoldierPage2DialogLocation = bounds.Location;
            Properties.Settings.Default.NewSoldierPage2DialogHeight = bounds.Height;
            Properties.Settings.Default.NewSoldierPage2DialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }

        private void NewSoldierPage2Dialog_Load(object sender, EventArgs e)
        {
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }
    }
}
