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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            dataGridView1.DataSource = DocumentModel.GetUserGeneratedDocumentView(DocumentSortMode.StatusAsc);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
 
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.Form2SavedWindowState;
            if (winState == "none")
            {
                centerFormPrimary();
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.Form2Location, Properties.Settings.Default.Form2Size);
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
                centerFormPrimary();
            }


        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.Form2SavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.Form2SavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.Form2Size = bounds.Size;
            Properties.Settings.Default.Form2Location = bounds.Location;
            Properties.Settings.Default.Form2Height = bounds.Height;
            Properties.Settings.Default.Form2Width = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();


        }
        //Centers Form2 on the primary screen mdail 8-15-19
        private void centerFormPrimary()
        {
            int height = this.ClientSize.Height; //this.MaximumSize.Height; 
            int width = this.ClientSize.Width; // this.MaximumSize.Width;
            Size size = new Size(width, height);
            int x = (Screen.PrimaryScreen.WorkingArea.Width - width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - height) / 2;
            Point location = new Point(x, y);
            this.DesktopBounds = new Rectangle(location, size);
            this.WindowState = FormWindowState.Normal;
        }
    }
}
