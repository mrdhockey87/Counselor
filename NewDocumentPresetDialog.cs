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
    internal partial class NewDocumentPresetDialog : Form
    {
        DocumentReportFilter newFilter;
        DocumentReportFilter currentFilter;
        private int minWidth = 491;
        private int minHeigth = 257;

        internal NewDocumentPresetDialog(DocumentReportFilter currentFilter)
        {
            this.currentFilter = new DocumentReportFilter();
            DocumentReportFilter.DeepCopyReportFilterToCurrentFilter(this.currentFilter, currentFilter);

            InitializeComponent();
            Load += new EventHandler(AddNewPresetDialog_Load);
        }

        void AddNewPresetDialog_Load(object sender, EventArgs e)
        {
            List<DocumentReportFilter> filters = DocumentReportFilter.GetAllDocumentReportFilters();

            currentFilter.DocumentReportFilterName = "The current filter settings.";
            currentFilter.DocumentReportFilterID = -1;
            filters.Add(currentFilter);

            reportFilterComboBox.DataSource = filters;
            reportFilterComboBox.ValueMember = "DocumentReportFilterID";
            reportFilterComboBox.DisplayMember = "DocumentReportFilterName";

            reportFilterComboBox.SelectedIndex = reportFilterComboBox.Items.Count - 1;

            newFilter = new DocumentReportFilter();
            PutAtSavedLocation();
        }


        private void generateBlankTemplateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = !generateBlankTemplateCheckbox.Checked;
            reportFilterComboBox.Enabled = enabled;
        }


        private void createNewFilterButton_Click(object sender, EventArgs e)
        {
            if (filterNameTextbox.Text == "")
            {
                ShowBlankNameError();
                DialogResult = DialogResult.None;
                return;
            }

            string newFilterName = filterNameTextbox.Text;
            bool overwrite = false;

            List<DocumentReportFilter> filters = reportFilterComboBox.Items.Cast<DocumentReportFilter>().ToList();
            bool filterNameExists = filters.Where(filter => filter.DocumentReportFilterName == newFilterName).Count() > 0;

            if (filterNameExists)
            {
                DialogResult result = AssignNewFilterFromExistingFilter(filters, newFilterName);
                if (result == DialogResult.No)
                {
                    DialogResult = DialogResult.None;
                    return;
                }
                else
                {
                    overwrite = true;
                }
            }

            if (generateBlankTemplateCheckbox.Checked == true)
            {
                newFilter.CounselingTemplateIDs = new List<int>();
                newFilter.DocumentTemplateIDs = new List<int>();
                newFilter.DocumentStatusIDs = new List<int>();
                newFilter.DocumentReportFilterName = newFilterName;
            }
            else
            {
                DocumentReportFilter filter = (DocumentReportFilter)reportFilterComboBox.SelectedItem;
                DocumentReportFilter.DeepCopyReportFilterToCurrentFilter(newFilter, filter);
                
                // undo setting the ID and name
                if(!overwrite)
                    newFilter.DocumentReportFilterID = -1;

                newFilter.DocumentReportFilterName = newFilterName;
            }

            DocumentsReportFiltersDialog dialog = new DocumentsReportFiltersDialog();
            DialogResult createNewResult = dialog.ShowNewFilterDialog(this, newFilter);

            if (createNewResult == DialogResult.Cancel)
            {
                DialogResult = DialogResult.None;
                return;
            }

            newFilter.CounselingTemplateIDs = dialog.CounselingIDs;
            newFilter.DocumentTemplateIDs = dialog.DocumentIDs;
            newFilter.DocumentStatusIDs = dialog.StatusIDs;

            newFilter.Save();
        }


        private DialogResult AssignNewFilterFromExistingFilter(List<DocumentReportFilter> filters, string newFilterName)
        {
            string confirmMessage = "A preset with this name already exists!  Continuing will overwrite the old preset.\n\nAre you sure you want to continue?";
            string confirmCaption = "Preset name exists!";
            CQPMessageBox.CQPMessageBoxButtons confirmButtons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            CQPMessageBox.CQPMessageBoxIcon confirmIcon = CQPMessageBox.CQPMessageBoxIcon.Warning;
            List<string> buttonText = new List<string> { "Overwrite", "Cancel" };

            DialogResult confirmResult = CQPMessageBox.ShowDialog(confirmMessage, confirmCaption, confirmButtons, confirmIcon);

            if (confirmResult == DialogResult.No)
                return confirmResult;

            // this is safe as we wouldn't have gotten this far 
            newFilter = filters.Where(filter => filter.DocumentReportFilterName == newFilterName).First();

            if (!newFilter.Saveable)
            {
                string errorMessage = newFilter.DocumentReportFilterName + " is not overwriteable.";
                string errorCaption = "Cannot overwrite.";
                CQPMessageBox.CQPMessageBoxButtons errorButtons = CQPMessageBox.CQPMessageBoxButtons.OK;
                CQPMessageBox.CQPMessageBoxIcon errorIcon = CQPMessageBox.CQPMessageBoxIcon.Error;

                CQPMessageBox.Show(errorMessage, errorCaption, errorButtons, errorIcon);
                return DialogResult.No;
            }

            return confirmResult;
        }


        private void ShowBlankNameError()
        {
            string message = "Preset name cannot be blank.";
            string caption = "Blank preset name";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Error;
            CQPMessageBox.Show(message, caption, buttons, icon);
        }



        DocumentReportFilter NewFilter
        {
            get
            {
                return newFilter;
            }
        }

        private void NewDocumentPresetDialog_Load(object sender, EventArgs e)
        {

        }

        private void NewDocumentPresetDialog_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.NewDocumentPresetDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.NewDocumentPresetDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.NewDocumentPresetDialogSize = bounds.Size;
            Properties.Settings.Default.NewDocumentPresetDialogLocation = bounds.Location;
            if(bounds.Height < minHeigth)
            {
                Properties.Settings.Default.NewDocumentPresetDialogHeight = minHeigth;
            }
            else
            {
                Properties.Settings.Default.NewDocumentPresetDialogHeight = bounds.Height;
            }
            if (bounds.Width < minWidth)
            {
                Properties.Settings.Default.NewDocumentPresetDialogWidth = minWidth;
            }
            else
            {
                Properties.Settings.Default.NewDocumentPresetDialogWidth = bounds.Width;
            }
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.NewDocumentPresetDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
                System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
                if (bounds.Height < minHeigth)
                {
                    bounds.Height = minHeigth;
                }
                if (bounds.Width < minWidth)
                {
                    bounds.Width = minWidth;
                }
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.NewDocumentPresetDialogLocation, bounds.Size);
            }
            else
            {
                Size size = Properties.Settings.Default.NewDocumentPresetDialogSize;
                if(size.Height < minHeigth)
                {
                    size.Height = minHeigth;
                }
                if(size.Width < minWidth)
                {
                    size.Width = minWidth;
                }
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.NewDocumentPresetDialogLocation, size);
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
