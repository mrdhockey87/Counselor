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
    public partial class PromptToCompleteCounselingPackageDialog : Form
    {
        public PromptToCompleteCounselingPackageDialog(int soldierID, int templateID, List<string> templateNames)
        {
            InitializeComponent();
            FillInSoldierInfo(soldierID);
            FillInTemplatePlaceHolders(templateID, templateNames);
            createPackageDetailsLabel.MaximumSize = new Size(300, 0);
        }

        internal void FillInTemplatePlaceHolders(int templateID, List<string> templateNames)
        {
            //string mainTemplateName = templateNames[0];
            string mainTemplateName = TemplatesModel.GetTemplateNameByTemplateID(templateID);
            
            mainBodyTextLabel.Text = mainBodyTextLabel.Text.Replace("$COUNSELING", mainTemplateName);

            for (int i = 0; i < templateNames.Count; i++)
                mainBodyTextLabel.Text = mainBodyTextLabel.Text + "\n    - " + templateNames[i];

            int maxWidthOfLabel = GetMaxWidthOfLabel();
            mainBodyTextLabel.MaximumSize = new Size(maxWidthOfLabel, 0);
        }


        private int GetMaxWidthOfLabel()
        {
            int tableWidth = tableLayoutPanel1.Width;
            int maxWidthofLabel = tableWidth - 40;
            return maxWidthofLabel;
        }


        private void FillInSoldierInfo(int soldierID)
        {
            //SoldierModel soldier = SoldierModel.LoadSoldierModel(soldierID);
            Soldier soldier = new Soldier(soldierID);

            if (soldier.SoldierID != -1)
            {
                StringBuilder rankNameString = new StringBuilder();
                rankNameString.Append(soldier.Rank.ToString() + ", ");
                rankNameString.Append(soldier.LastName + ", ");
                rankNameString.Append(soldier.FirstName);

                rankAndNameLabel.Text = rankNameString.ToString();

                /*
                if (soldier.PictureFilename != "")
                    soldierPictureBox.ImageLocation = soldier.PictureFilename;
                else
                    soldierPictureBox.Image = RankingModel.GetRankingImages()[(int)soldier.Rank - 1];
                 * */
                soldierPictureBox.Image = soldier.Picture;
            }
            else
            {
                soliderInfoTable.Visible = false;
            }
        }

        private void PromptToCompleteCounselingPackageDialog_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.PromptToCompleteCounselingPackageDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.PromptToCompleteCounselingPackageDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.PromptToCompleteCounselingPackageDialogSize = bounds.Size;
            Properties.Settings.Default.PromptToCompleteCounselingPackageDialogLocation = bounds.Location;
            Properties.Settings.Default.PromptToCompleteCounselingPackageDialogHeight = bounds.Height;
            Properties.Settings.Default.PromptToCompleteCounselingPackageDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.PromptToCompleteCounselingPackageDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.PromptToCompleteCounselingPackageDialogLocation, Properties.Settings.Default.PromptToCompleteCounselingPackageDialogSize);
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

        private void PromptToCompleteCounselingPackageDialog_Load(object sender, EventArgs e)
        {

            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }
    }
}
