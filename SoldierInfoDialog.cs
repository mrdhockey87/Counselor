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
    internal partial class SoldierInfoDialog : Form
    {
        Soldier soldier;

        internal SoldierInfoDialog(Soldier soldier)
        {
            InitializeComponent();
            this.soldier = soldier;
            PopulateFields();
        }


        static internal void ShowDialog(Soldier soldier)
        {
            SoldierInfoDialog soldierInfoDialog = new SoldierInfoDialog(soldier);
            DialogResult result = soldierInfoDialog.ShowDialog();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.SoldierInfoDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.SoldierInfoDialogLocation, Properties.Settings.Default.SoldierInfoDialogSize);
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


        void PopulateFields()
        {
            FormatStatusString();
            UpdateImage();
            UpdateNameAndRankLabel();
            UpdateDORLabel();
            UpdateAgeLabel();
            UpdateUnitHeirarchyLabels();
        }


        void FormatStatusString()
        {
            string noItemsString = "[No Outstanding Items]";

            Graphics g = this.CreateGraphics();
            Font f = soldierStatusEnumsLabel.Font;

            string fullString = "";
            
            int[] widths = statusLabelTable.GetColumnWidths();
            int width = widths[1];

            foreach ( SoldierStatus entry in soldier.Statuses.Values )
            {
                if (entry.applies == true)
                    fullString += entry.statusString + ", ";
            }

            if (soldier.OtherStatus != "")
                fullString += soldier.OtherStatus;

            if (fullString == "")
                fullString = noItemsString;

            string wordWrappedStatusString = Utilities.WordWrappedString(fullString, f, g, width);

            wordWrappedStatusString = wordWrappedStatusString.TrimEnd(',', ' ');
            

            soldierStatusEnumsLabel.Text = wordWrappedStatusString;
        }


        void UpdateImage()
        {
            /*
            if (soldier.Picture != null)
                pictureBox1.Image = soldier.Picture;
            else
                pictureBox1.Image = RankingModel.GetRankingImages()[((int)soldier.Rank - 1)];
             */

            pictureBox1.Image = soldier.Picture;
        }


        void UpdateNameAndRankLabel()
        {
            string nameAndRankText;
            nameAndRankText = RankingModel.RankToString(soldier.Rank);
            nameAndRankText += " " + soldier.LastName + ", ";
            nameAndRankText += soldier.FirstName + " ";
            nameAndRankText += soldier.MiddleInitial;

            rankNameLabel.Text = nameAndRankText;
        }


        void UpdateDORLabel()
        {
            if (soldier.DateOfRank.Ticks == 0)
                return;

            string dorText = soldier.DateOfRank.ToString("yyyy-MM-dd");
            //dorLabel.Text += dorText;
            formattedDORLabel.Text = dorText;
        }


        void UpdateAgeLabel()
        {
            if (soldier.DateOfBirth.Ticks == 0)
                return;

            int age = Utilities.CalculateAge(soldier.DateOfBirth, DateTime.Now);
            //ageLabel.Text += age.ToString();
            formattedAgeLabel.Text = age.ToString();
        }


        void UpdateUnitHeirarchyLabels()
        {
            int battalionID = soldier.UnitHierarchy.battalionID;
            string battalionName = UnitHierarchyModel.GetBattalionName(battalionID);
            //battalionLabel.Text += battalionName;
            formattedBattalionLabel.Text = battalionName;

            int unitID = soldier.UnitHierarchy.unitID;
            int unitDesignatorID = soldier.UnitHierarchy.unitDesignatorID;
            string unitName = UnitHierarchyModel.GetUnitName(unitID);
            string unitDesignatorName = UnitHierarchyModel.GetUnitDesignatorName(unitDesignatorID);
            //unitLabel.Text += unitName + " " + unitDesignatorName;
            formattedUnitLabel.Text = unitName + " " + unitDesignatorName;

            int platoonID = soldier.UnitHierarchy.platoonID;
            string platoonName = UnitHierarchyModel.GetPlatoonName(platoonID);
            //platoonLabel.Text += platoonName;// +" Platoon";
            formattedPlatoonLabel.Text = platoonName;

            int squadSectionID = soldier.UnitHierarchy.squadID;
            string squadName = UnitHierarchyModel.GetSectionSquadName(squadSectionID);            
            //squadLabel.Text += squadName;
            formattedSquadLabel.Text = squadName;

        }


        private void closeButton_Click(object sender, EventArgs e)
        {
            SaveLocation();
            this.Dispose();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            EditSoldierDialog newSoldierDialogPageOne = new EditSoldierDialog(soldier);
            this.Hide();
            newSoldierDialogPageOne.ShowDialog(this);
            SaveLocation();
            this.Dispose();
        }
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.SoldierInfoDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.SoldierInfoDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.SoldierInfoDialogSize = bounds.Size;
            Properties.Settings.Default.SoldierInfoDialogLocation = bounds.Location;
            Properties.Settings.Default.SoldierInfoDialogHeight = bounds.Height;
            Properties.Settings.Default.SoldierInfoDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }

        private void SoldierInfoDialog_Load(object sender, EventArgs e)
        {
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }
    }
}
