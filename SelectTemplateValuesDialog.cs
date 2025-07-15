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
    internal partial class SelectTemplateValuesDialog : Form
    {
        List<string> templateValues;

        internal SelectTemplateValuesDialog(List<string> templateValues)
        {
            InitializeComponent();
            this.templateValues = templateValues;
            Load += new EventHandler(SelectTemplateValuesDialog_Load);
            Resize += new EventHandler(SelectTemplateValuesDialog_Resize);
        }

        void SelectTemplateValuesDialog_Resize(object sender, EventArgs e)
        {
            this.SuspendDrawing();

            templateValuesCheckedListBox.Columns.RemoveAt(0);

            ColumnHeader h = new ColumnHeader();
            h.Width = templateValuesCheckedListBox.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            templateValuesCheckedListBox.Columns.Add(h);

            this.ResumeDrawing();
        }

        void SelectTemplateValuesDialog_Load(object sender, EventArgs e)
        {
            FillTemplateListView(templateValues);

            templateValuesCheckedListBox.View = View.Details;
            templateValuesCheckedListBox.HeaderStyle = ColumnHeaderStyle.None; 

            ColumnHeader h = new ColumnHeader();
            h.Width = templateValuesCheckedListBox.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            templateValuesCheckedListBox.Columns.Add(h);
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }

        private void FillTemplateListView(List<string> templateValues)
        {
            if(templateValuesCheckedListBox.Items.Count > 0)
            {
                templateValuesCheckedListBox.Items.Clear();
            }
            foreach (string value in templateValues)
            {
                ListViewItem item = new ListViewItem();
                item.Text = value;
                item.ToolTipText = value;

                templateValuesCheckedListBox.Items.Add(item);
            }
        }

        private List<int> GetSelectedItems()
        {
            ListView.CheckedIndexCollection indicies = templateValuesCheckedListBox.CheckedIndices;
            List<int> selectedIndicies = new List<int>();

            foreach (int index in indicies)
            {
                selectedIndicies.Add(index);
            }

            return selectedIndicies;
        }


        internal List<int> SelectedIndicies
        {
            get
            {
                return GetSelectedItems();
            }
        }

        private void SelectTemplateValuesDialog_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.SelectTemplateValuesDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.SelectTemplateValuesDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.SelectTemplateValuesDialogSize = bounds.Size;
            Properties.Settings.Default.SelectTemplateValuesDialogLocation = bounds.Location;
            Properties.Settings.Default.SelectTemplateValuesDialogHeight = bounds.Height;
            Properties.Settings.Default.SelectTemplateValuesDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.SelectTemplateValuesDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.SelectTemplateValuesDialogLocation, Properties.Settings.Default.SelectTemplateValuesDialogSize);
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
