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
    public partial class EditNoteDialog : Form
    {
        public EditNoteDialog(int soldierID)
        {
            InitializeComponent();

            note = new NoteInterface();
            note.SoldierID = soldierID;

            dateTimePicker1.Value = note.Date;
        }

        internal EditNoteDialog(int noteID, int soldierID)
        {
            InitializeComponent();

            note = new NoteInterface(noteID);

            subjectTextbox.Text = note.Subject;
            commentRichTextBox.Text = note.Comment;
            dateTimePicker1.Value = note.Date;
        }

        private void saveNoteButton_Click(object sender, EventArgs e)
        {
            note.Subject = subjectTextbox.Text;
            note.Comment = commentRichTextBox.Text;
            note.Date = dateTimePicker1.Value;

            note.Save();
            SaveLocation();
            this.Dispose();
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            SaveLocation();
            this.Dispose();
        }

        NoteInterface note;

        private void EditNoteDialog_Load(object sender, EventArgs e)
        {
            PutAtSavedLocation();
        }

        private void EditNoteDialog_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.EditNoteDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.EditNoteDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.EditNoteDialogSize = bounds.Size;
            Properties.Settings.Default.EditNoteDialogLocation = bounds.Location;
            Properties.Settings.Default.EditNoteDialogHeight = bounds.Height;
            Properties.Settings.Default.EditNoteDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.EditNoteDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.EditNoteDialogLocation, Properties.Settings.Default.EditNoteDialogSize);
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
