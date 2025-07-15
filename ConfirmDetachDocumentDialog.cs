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
    internal partial class ConfirmDetachDocumentDialog : Form
    {
        //internal ConfirmDetachDocumentDialog()
        //{
        //    InitializeComponent();
        //}

        internal ConfirmDetachDocumentDialog(Document document)
        {
            Logger.Trace("ConfirmDetachDocumentDialog - " + document.GeneratedDocID);

            InitializeComponent();

            DocumentName = document.DocumentName;
            ParentDocumentName = DocumentModel.GetDocumentName(document.ParentDocumentID);
        }

        internal ConfirmDetachDocumentDialog(int documentID)
            : this(new Document(documentID))
        {
            
        }

        public string DocumentName
        {
            set
            {
                string documentName = value;
                mainTextLabel.Text = mainTextLabel.Text.Replace("$DOCNAME", documentName);
            }
        }

        public string ParentDocumentName
        {
            set
            {
                string parentDocumentName = value;
                mainTextLabel.Text = mainTextLabel.Text.Replace("$PARENTDOCNAME", parentDocumentName);
            }
        }

        private void ConfirmDetachDocumentDialog_Load(object sender, EventArgs e)
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.ConfirmDetachDocumentDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.ConfirmDetachDocumentDialogLocation, Properties.Settings.Default.ConfirmDetachDocumentDialogSize);
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

        private void ConfirmDetachDocumentDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.ConfirmDetachDocumentDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.ConfirmDetachDocumentDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.ConfirmDetachDocumentDialogSize = bounds.Size;
            Properties.Settings.Default.ConfirmDetachDocumentDialogLocation = bounds.Location;
            Properties.Settings.Default.ConfirmDetachDocumentDialogHeight = bounds.Height;
            Properties.Settings.Default.ConfirmDetachDocumentDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
    }
}
