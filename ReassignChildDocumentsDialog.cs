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
    internal enum DocumentReassignmentMode
    {
        DetachAllChildren,
        DeleteAllChildren,
        Cancel,
        NewParent
    }

    internal enum DocumentReassignmentReason
    {
        DeleteingDocument,
        ChangingParent
    }

    internal partial class ReassignChildDocumentsDialog : Form
    {
        internal ReassignChildDocumentsDialog(DocumentReassignmentReason reason)
        {
            InitializeComponent();

            UpdateDialog(reason);
        }

        internal void UpdateDialog(DocumentReassignmentReason reason)
        {
            if (reason == DocumentReassignmentReason.ChangingParent)
            {
                moveAllChildDocumentsButton.Visible = false;
                label1.Text = "This document appears to have one or more documents attached to it.\n\n"
                    + "Would you like to detach these documents or select a new head document?";
            }
        }


        internal static DocumentReassignmentMode ShowDialog(DocumentReassignmentReason reason)
        {
            ReassignChildDocumentsDialog dialog = new ReassignChildDocumentsDialog(reason);
            DialogResult result = dialog.ShowTheDialog();

            if (result == DialogResult.OK)
                return DocumentReassignmentMode.NewParent;
            else if (result == DialogResult.No)
                return DocumentReassignmentMode.DetachAllChildren;
            else if (result == DialogResult.Yes)
                return DocumentReassignmentMode.DeleteAllChildren;
            else if (result == DialogResult.Cancel)
                return DocumentReassignmentMode.Cancel;

            return DocumentReassignmentMode.Cancel;
        }

        internal DialogResult ShowTheDialog()
        {
            DialogResult result = base.ShowDialog();
            return result;
        }
    }
}
