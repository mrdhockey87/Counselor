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
    public partial class DocumentsReportFiltersDialog : Form
    {
        private string documentFilters;

        private string counselingIDFilters;
        private string documentationIDFilters;
        private string documentStatusIDFilters;
        private string noteFilters;

        internal List<int> CounselingIDs { get; set; }
        internal List<int> DocumentIDs { get; set; }
        internal List<int> StatusIDs { get; set; }


        public DocumentsReportFiltersDialog()
        {
            InitializeComponent();
            InitializeChecklistBoxes();

            noteFilters = "";

            CounselingIDs = new List<int>();
            DocumentIDs = new List<int>();
            StatusIDs = new List<int>();
        }

        private void InitializeChecklistBoxes()
        {
            DataTable documentNames = DocumentModel.DocumentNamesTable;
            DataTable templatesTable = TemplatesModel.TemplatesTable.Copy();

            //documentNames.AsDataView().Sort = "documentnametext asc";
            DataView view = templatesTable.DefaultView;
            view.Sort = "documentnametext asc";
            templatesTable = view.ToTable();

            counselingsCheckedListBox.DisplayMember = "Text";
            documentationCheckedListBox.DisplayMember = "Text";
            documentStatusCheckedListBox.DisplayMember = "Text";

            InitializeCounselingCheckbox(documentNames, templatesTable);
            InitializeDocumentationCheckbox(documentNames, templatesTable);
            InitializeDocumentStatusCheckbox();
        }


        private void InitializeCounselingCheckbox(DataTable documentNames, DataTable templatesTable)
        {
            foreach (DataRow template in templatesTable.Select("documenttypeid = 1"))
            {
                int documentNameID = Convert.ToInt32(template["documentnameid"]);
                DataRow[] documentNameRow = documentNames.Select("documentnameid = " + documentNameID);
                string documentName = documentNameRow[0]["documentnametext"].ToString();

                ListViewItem item = new ListViewItem();
                item.Tag = documentNameID;
                item.Text = documentName;

                counselingsCheckedListBox.Items.Add(item);
            }
        }


        private void InitializeDocumentationCheckbox(DataTable documentNames, DataTable templatesTable)
        {
            foreach (DataRow template in templatesTable.Select("documenttypeid = 2"))
            {
                int documentNameID = Convert.ToInt32(template["documentnameid"]);
                DataRow[] documentNameRow = documentNames.Select("documentnameid = " + documentNameID);
                string documentName = documentNameRow[0]["documentnametext"].ToString();

                ListViewItem item = new ListViewItem();
                item.Tag = documentNameID;
                item.Text = documentName;

                documentationCheckedListBox.Items.Add(item);
            }
        }


        private void InitializeDocumentStatusCheckbox()
        {
            DataTable documentStatuses = DocumentModel.GetDocumentStatuses();
            foreach (DataRow status in documentStatuses.Rows)
            {
                int statusID = Convert.ToInt32(status["documentstatusid"]);
                string statusString = status["documentstatustext"].ToString();

                ListViewItem item = new ListViewItem();
                item.Tag = statusID;
                item.Text = statusString;

                documentStatusCheckedListBox.Items.Add(item);
            }
        }


        private void doNotShowNotes_Click(object sender, EventArgs e)
        {
            showAllNotesRadioButton.Checked = false;
            showOnlyFilteredNotesRadioButton.Checked = false;
        }


        private void showAllNotesRadioButton_Click(object sender, EventArgs e)
        {
            doNotShowNotes.Checked = false;
            showOnlyFilteredNotesRadioButton.Checked = false;
        }


        private void showOnlyFilteredNotesRadioButton_Click(object sender, EventArgs e)
        {
            showAllNotesRadioButton.Checked = false;
            doNotShowNotes.Checked = false;
        }


        private void selectAllCounselingsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < counselingsCheckedListBox.Items.Count; i++)
            {
                counselingsCheckedListBox.SetItemChecked(i, true);
            }
        }


        private void deselectAllCounselingsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < counselingsCheckedListBox.Items.Count; i++)
            {
                counselingsCheckedListBox.SetItemChecked(i, false);
            }
        }


        private void selectAllDocumentStatuses_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < documentStatusCheckedListBox.Items.Count; i++)
            {
                documentStatusCheckedListBox.SetItemChecked(i, true);
            }
        }


        private void deselectAllDocumentStatuses_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < documentStatusCheckedListBox.Items.Count; i++)
            {
                documentStatusCheckedListBox.SetItemChecked(i, false);
            }
        }


        private void selectAllDocumentsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < documentationCheckedListBox.Items.Count; i++)
            {
                documentationCheckedListBox.SetItemChecked(i, true);
            }
        }


        private void deselectAllDocumentsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < documentationCheckedListBox.Items.Count; i++)
            {
                documentationCheckedListBox.SetItemChecked(i, false);
            }            
        }


        private void BuildCounselingIDFilters()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ListViewItem item in counselingsCheckedListBox.CheckedItems)
            {
                int documentNameID = Convert.ToInt32(item.Tag);
                stringBuilder.Append("documentnameid = " + documentNameID + " or ");

                CounselingIDs.Add(documentNameID);
            }

            counselingIDFilters = stringBuilder.ToString();
            if ( counselingIDFilters.Length > 0 )
            {
                int startIndex = counselingIDFilters.Length - " or ".Length;
                counselingIDFilters = counselingIDFilters.Remove(startIndex, " or ".Length);
            }
        }


        public void BuildDocumentationIDFilters()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ListViewItem item in documentationCheckedListBox.CheckedItems)
            {
                int documentNameID = Convert.ToInt32(item.Tag);
                stringBuilder.Append("documentnameid = " + documentNameID + " or ");

                DocumentIDs.Add(documentNameID);
            }

            documentationIDFilters = stringBuilder.ToString();
            if (documentationIDFilters.Length > 0)
            {
                int startIndex = documentationIDFilters.Length - " or ".Length;
                documentationIDFilters = documentationIDFilters.Remove(startIndex, " or ".Length);
            }
        }


        public void BuildDocumentStatusIDFilters()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ListViewItem item in documentStatusCheckedListBox.CheckedItems)
            {
                int statusID = Convert.ToInt32(item.Tag);
                stringBuilder.Append("documentstatusid = " + statusID + " or ");

                StatusIDs.Add(statusID);
            }

            documentStatusIDFilters = stringBuilder.ToString();
            if (documentStatusIDFilters.Length > 0)
            {
                int startIndex = documentStatusIDFilters.Length - " or ".Length;
                documentStatusIDFilters = documentStatusIDFilters.Remove(startIndex, " or ".Length);
            }
        }


        public void BuildNoteFilters()
        {
            if (doNotShowNotes.Checked == true)
                noteFilters = "documenttypeid != 3";
            else if (showAllNotesRadioButton.Checked == true)
                noteFilters = "documenttypeid = 3";
            //else if (showOnlyFilteredNotesRadioButton.Checked == true)
                //noteFilters = "
        }


        public void BuildDocumentFilters()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if(counselingIDFilters.Length != 0)
                stringBuilder.Append("( " + counselingIDFilters + " )");

            if(stringBuilder.Length != 0 && documentationIDFilters.Length != 0)
                stringBuilder.Append(" and ");

            if(documentationIDFilters.Length != 0)
                stringBuilder.Append("( " + documentationIDFilters + " )");

            if (stringBuilder.Length != 0 && documentStatusIDFilters.Length != 0)
                stringBuilder.Append(" and ");

            if (documentStatusIDFilters.Length != 0)
                stringBuilder.Append("( " + documentStatusIDFilters + " )");

            if (stringBuilder.Length != 0 && noteFilters.Length != 0)
                stringBuilder.Append(" and ");

            if (noteFilters.Length != 0)
                stringBuilder.Append("( " + noteFilters + " )");

            if (stringBuilder.Length != 0)
                stringBuilder.Append(" and ");

            stringBuilder.Append("( deleted = 0 )");

            documentFilters = stringBuilder.ToString();
        }


        public string DocumentFilters
        {
            get
            {
                BuildCounselingIDFilters();
                BuildDocumentationIDFilters();
                BuildDocumentStatusIDFilters();
                //BuildNoteFilters();
                BuildDocumentFilters();

                return documentFilters;
            }
        }


        internal DialogResult ShowDialog(Control parent, DocumentReportFilter filter)
        {
            CheckSelectedItems(counselingsCheckedListBox, filter.CounselingTemplateIDs);
            CheckSelectedItems(documentationCheckedListBox, filter.DocumentTemplateIDs);
            CheckSelectedItems(documentStatusCheckedListBox, filter.DocumentStatusIDs);
            

            return ShowDialog(parent);
        }

        internal DialogResult ShowNewFilterDialog(Control control, DocumentReportFilter newFilter)
        {
            applyButton.Text = "Save New Filter";
            applyButton.GreyRectSize = CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            titleLabel.Text = "Select new filter settings...";

            return ShowDialog(control, newFilter);
        }


        private void CheckSelectedItems(CheckedListBox listview, List<int> ids)
        {
            for(int i = 0; i < listview.Items.Count; i++)
            {
                ListViewItem item = (ListViewItem)listview.Items[i];
                int id = Convert.ToInt32(item.Tag);
                if (ids.Contains(id))
                    listview.SetItemChecked(i, true);
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in counselingsCheckedListBox.CheckedItems)
            {
                int id = Convert.ToInt32(item.Tag);
                CounselingIDs.Add(id);
            }

            foreach (ListViewItem item in documentationCheckedListBox.CheckedItems)
            {
                int id = Convert.ToInt32(item.Tag);
                DocumentIDs.Add(id);
            }

            foreach (ListViewItem item in documentStatusCheckedListBox.CheckedItems)
            {
                int id = Convert.ToInt32(item.Tag);
                StatusIDs.Add(id);
            }
        }

        private void DocumentsReportFiltersDialog_Load(object sender, EventArgs e)
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.DocumentsReportFiltersSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.DocumentsReportFiltersLocation, Properties.Settings.Default.DocumentsReportFiltersSize);
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

        private void DocumentsReportFiltersDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.DocumentsReportFiltersSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.DocumentsReportFiltersSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.DocumentsReportFiltersSize = bounds.Size;
            Properties.Settings.Default.DocumentsReportFiltersLocation = bounds.Location;
            Properties.Settings.Default.DocumentsReportFiltersHeight = bounds.Height;
            Properties.Settings.Default.DocumentsReportFiltersWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
    }
}
