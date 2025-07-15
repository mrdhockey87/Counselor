using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using CounselQuickPlatinum.Properties;

namespace CounselQuickPlatinum
{
    public partial class Form1 : Form
    {
        SoldiersDialog soldiersTabPage;
        DocumentsTabPage documentsTabPage;
        //CounselingTab counselingTabPage;
        ReportsTab reportsTabPage;
        ResourcesTabPage resourcesTabPage;
        ReferencesTabPage referencesTabPage;
        HelpTabPage helpTabPage;

        CQPGraphicsButton previouslyActiveTabButton;
        WaitDialog importWaitDialog;
        BackgroundWorker importBackgroundWorker;

        public Form1(SplashScreen splashScreen)
        {
            InitializeComponent();
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
            soldiersTabPage = new SoldiersDialog();
            splashScreen.UpdateProgress(45);

            documentsTabPage = new DocumentsTabPage();
            splashScreen.UpdateProgress(65);

            reportsTabPage = new ReportsTab();
            splashScreen.UpdateProgress(75);

            resourcesTabPage = new ResourcesTabPage();
            splashScreen.UpdateProgress(80);

            referencesTabPage = new ReferencesTabPage();
            splashScreen.UpdateProgress(85);

            helpTabPage = new HelpTabPage();
            splashScreen.UpdateProgress(95);

            Load += new EventHandler(OnLoad);
            splashScreen.UpdateProgress(99);

            InitializeTimer();
            DocumentModel.CheckDocumentStatuses();
            splashScreen.UpdateProgress(100);
        }

        void OnLoad(object sender, EventArgs e)
        {
            InitializeTabs();
            Show();
            Activate();
            cqpTabControl1.Focus();
        }

        private void InitializeTimer()
        {
            int millisecondsInADay = 86400*1000;
            timer1.Interval = millisecondsInADay;
            timer1.Start();
        }


        public void InitializeTabs()
        {
            this.VerticalScroll.Enabled = true;
            this.VScroll = true;

            this.HorizontalScroll.Enabled = true;
            this.HScroll = true;

            cqpTabControl1.AddControl(soldiersTabPage);
            cqpTabControl1.AddControl(documentsTabPage);
            cqpTabControl1.AddControl(reportsTabPage);
            cqpTabControl1.AddControl(referencesTabPage); 
            cqpTabControl1.AddControl(resourcesTabPage);
            cqpTabControl1.AddControl(helpTabPage);

            cqpTabControl1.Switch(0);
            soldiersTabButton.Enabled = false;
            previouslyActiveTabButton = soldiersTabButton;
        }

        private void recycleBinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecycleBinDialog recycleBinDialog = new RecycleBinDialog();
            recycleBinDialog.ShowDialog();
        }

        private void colorAndAppearanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugDialog dialog = new DebugDialog();
            dialog.ShowDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectSoldierDialog.SelectSoldierMode mode = SelectSoldierDialog.SelectSoldierMode.SelectSoldiersToExport;
            SelectSoldierDialog selectSoldierDialog = new SelectSoldierDialog(mode);
            selectSoldierDialog.ShowDialog();

            List<int> ids = selectSoldierDialog.SelectedSoldierIDs;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DocumentModel.CheckDocumentStatuses();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Interval = 86400 * 1000;
            timer1.Start();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Interval = 30000;
            timer1.Start();
        }

        private void cqpTabControl1_Load(object sender, EventArgs e)
        {

        }

        private void DisableTabButton(CQPGraphicsButton button)
        {
            previouslyActiveTabButton.Enabled = true;
            previouslyActiveTabButton = button;

            button.Enabled = false;
        }

        private void soldiersTabButton_MouseUp(object sender, MouseEventArgs e)
        {
            DisableTabButton(soldiersTabButton);
            cqpTabControl1.Switch(0);
            cqpTabControl1.Focus();
        }

        private void counselingsTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(counselingsTabButton);
            cqpTabControl1.Switch(1);
            cqpTabControl1.Focus();
        }

        private void documentsTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(documentsTabButton);
            cqpTabControl1.Switch(1);
            cqpTabControl1.Focus();
        }

        private void reportsTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(reportsTabButton);
            cqpTabControl1.Switch(2);
            cqpTabControl1.Focus();
        }

        private void referencesTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(referencesTabButton);
            cqpTabControl1.Switch(3);
            cqpTabControl1.Focus();
        }

        private void resourcesTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(resourcesTabButton);
            cqpTabControl1.Switch(4);
            cqpTabControl1.Focus();
        }

        private void helpTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(helpTabButton);
            cqpTabControl1.Switch(5);
            cqpTabControl1.Focus();
        }

        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            importExportToolStripMenuItem.DropDownDirection = ToolStripDropDownDirection.BelowLeft;
            settingsContextMenu.Show(Cursor.Position, ToolStripDropDownDirection.BelowLeft);
        }

        private void recyclingBinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecycleBinDialog recycleBinDialog = new RecycleBinDialog();
            recycleBinDialog.ShowDialog();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsDialog dialog = new OptionsDialog();
            dialog.ShowDialog(this);
        }

        private void Form1_Move(object sender, EventArgs e)
        {
        }

        private void ExportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExportSoldiersDialog dialog = new ExportSoldiersDialog();
            dialog.ShowDialog(this);
        }

        private void ImportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Counsel Quick Export Files (*.cqpx)|*.cqpx";
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;

            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            string filename = dialog.FileName;
            FileInfo file = new FileInfo(filename);
            if (file.Exists == false)
            {
                CQPMessageBox.ShowDialog(filename + " does not appear to exist or could not be opened.");
                return;
            }

            importWaitDialog = new WaitDialog("Importing...");

            importBackgroundWorker = new BackgroundWorker();
            importBackgroundWorker.DoWork += new DoWorkEventHandler(ImportFileBackground);
            importBackgroundWorker.WorkerReportsProgress = true;
            importBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(ImportFileProgressUpdated);
            importBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ImportFileCompleted);

            importBackgroundWorker.RunWorkerAsync(file);

            importWaitDialog.ShowDialog();
        }

        void ImportFileCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception)
            {
                string error = ((Exception)e.Result).Message;
                string caption = "Import Error";

                CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;

                importWaitDialog.Dispose();
                CQPMessageBox.ShowDialog(this, error, caption, buttons, icon);
            }
            else
            {
                string message = "Import completed successfully!";
                string caption = "Import successful.";
                CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Information;

                importWaitDialog.Dispose();
                CQPMessageBox.ShowDialog(this, message, caption, buttons, icon);
                 
                //this.Focus();
            }

        }

        void ImportFileProgressUpdated(object sender, ProgressChangedEventArgs e)
        {
            importWaitDialog.UpdateProgress(e.ProgressPercentage);
        }

        void ImportFileBackground(object sender, DoWorkEventArgs e)
        {
            try
            {
                FileInfo file = (FileInfo)e.Argument;

                DataImporter importer = new DataImporter();
                Point location = new Point(this.Location.X, this.Location.Y);
                location.X += this.Size.Width / 2;
                location.Y += this.Size.Height / 2;

                importer.ReadExportFile(file, importBackgroundWorker, location);
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void soldiersTabButton_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Added to save the forms location so it can be restored mdail 8-19-19
            SaveLocation();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.Form1SavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.Form1SavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.Form1Size = bounds.Size;
            Properties.Settings.Default.Form1Location = bounds.Location;
            Properties.Settings.Default.Form1Height = bounds.Height;
            Properties.Settings.Default.Form1Width = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the windows position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.Form1SavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.Form1Location, Properties.Settings.Default.Form1Size);
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
