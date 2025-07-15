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
    public partial class CQPMessageBox : Form
    {
        private CQPMessageBox(string message)
        {
            InitializeComponent();
            cqpLabel1.Text = message;

            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private CQPMessageBox(string message, string caption) : this(message)
        {
            //InitializeComponent();
            cqpLabel1.Text = message;
            Text = caption;
        }


        internal static DialogResult ShowDialog(string message)
        {
            CQPMessageBox messageBox = new CQPMessageBox(message);

            CQPGrayRectangleButton okButton = new CQPGrayRectangleButton();
            okButton.Text = "OK";
            okButton.GreyRectSize = CQPGrayRectangleButton.CQPGreyRectButtonSize.w65;
            okButton.DialogResult = DialogResult.OK;

            messageBox.buttonsTable.Controls.Add(okButton, 0, 0);
            
            return messageBox.ShowDialog();
        }


        private Image GetImageFromIcon(CQPMessageBoxIcon icon)
        {
            if (icon == CQPMessageBoxIcon.Warning)
                return CounselQuickPlatinum.Properties.Resources.warning;
            else if (icon == CQPMessageBoxIcon.Error)
                return CounselQuickPlatinum.Properties.Resources.error;
            else if (icon == CQPMessageBoxIcon.Information)
                return CounselQuickPlatinum.Properties.Resources.info;
            else if (icon == CQPMessageBoxIcon.Question)
                return CounselQuickPlatinum.Properties.Resources.question;

            return CounselQuickPlatinum.Properties.Resources.warning;
        }


        private void AddButtons(CQPMessageBoxButtons buttons)
        {
            CQPGrayRectangleButton button1 = new CQPGrayRectangleButton();
            CQPGrayRectangleButton button2 = new CQPGrayRectangleButton();
            CQPGrayRectangleButton button3 = new CQPGrayRectangleButton();

            switch(buttons)
            {

                case(CQPMessageBoxButtons.OK) :
                    button1.Text = "OK";
                    button1.DialogResult = DialogResult.OK;
                    buttonsTable.Controls.Add(button1, 0, 0);
                    break;

                case (CQPMessageBoxButtons.OKCancel):
                    button1.Text = "OK";
                    button1.DialogResult = DialogResult.OK;
                    buttonsTable.Controls.Add(button1, 0, 0);

                    button2.Text = "Cancel";
                    button2.DialogResult = DialogResult.Cancel;
                    buttonsTable.Controls.Add(button2, 1, 0);
                    break;

                case(CQPMessageBoxButtons.SkipAbort):
                    button1.Text = "Skip";
                    button1.DialogResult = DialogResult.Ignore;
                    buttonsTable.Controls.Add(button1, 0, 0);

                    button2.Text = "Abort";
                    button2.DialogResult = DialogResult.Abort;
                    buttonsTable.Controls.Add(button2, 1, 0);
                    break;

                case(CQPMessageBoxButtons.YesNo):
                    button1.Text = "Yes";
                    button1.DialogResult = DialogResult.Yes;
                    buttonsTable.Controls.Add(button1, 0, 0);

                    button2.Text = "No";
                    button2.DialogResult = DialogResult.No;
                    buttonsTable.Controls.Add(button2, 1, 0);
                    break;

                case(CQPMessageBoxButtons.YesNoCancel):
                    button1.Text = "Yes";
                    button1.DialogResult = DialogResult.Yes;
                    buttonsTable.Controls.Add(button1, 0, 0);

                    button2.Text = "No";
                    button2.DialogResult = DialogResult.No;
                    buttonsTable.Controls.Add(button2, 1, 0);

                    button3.Text = "Cancel";
                    button3.DialogResult = DialogResult.Cancel;
                    buttonsTable.Controls.Add(button3, 2, 0);
                    break;
            }
        }


        private CQPGrayRectangleButton.CQPGreyRectButtonSize GetGrayRectSize(string text)
        {
            Size size = TextRenderer.MeasureText(text, Font);
            if (size.Width < 65)
                return CQPGrayRectangleButton.CQPGreyRectButtonSize.w65;
            else if (size.Width < 90)
                return CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            else if (size.Width < 120)
                return CQPGrayRectangleButton.CQPGreyRectButtonSize.w120;
            else if (size.Width < 150)
                return CQPGrayRectangleButton.CQPGreyRectButtonSize.w150;
            else if (size.Width < 180)
                return CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;

            return CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;
        }


        private void AddButtons(CQPMessageBoxButtons buttons, List<string> buttonText)
        {
            List<CQPGrayRectangleButton> grayButtons = new List<CQPGrayRectangleButton>();
            List<DialogResult> results = new List<DialogResult>();

            foreach (string buttonValue in buttonText)
            {
                CQPGrayRectangleButton button = new CQPGrayRectangleButton();
                button.Text = buttonValue;
                button.GreyRectSize = GetGrayRectSize(buttonValue);
                grayButtons.Add(button);
            }

            switch (buttons)
            {
                case(CQPMessageBoxButtons.OK) :
                    results.Add(DialogResult.OK);
                    break;
                case(CQPMessageBoxButtons.OKCancel):
                    results.Add(DialogResult.OK);
                    results.Add(DialogResult.Cancel);
                    break;
                case(CQPMessageBoxButtons.SkipAbort):
                    results.Add(DialogResult.Ignore);
                    results.Add(DialogResult.Abort);
                    break;
                case(CQPMessageBoxButtons.YesNo):
                    results.Add(DialogResult.Yes);
                    results.Add(DialogResult.No);
                    break;
                case (CQPMessageBoxButtons.YesNoCancel):
                    results.Add(DialogResult.Yes);
                    results.Add(DialogResult.No);
                    results.Add(DialogResult.Cancel);
                    break;
            }

            for (int i = 0; i < grayButtons.Count; i++)
            {
                grayButtons[i].DialogResult = results[i];
                //int index = grayButtons.Count - i-1;
                int index = i;
                buttonsTable.Controls.Add(grayButtons[i], index, 0);
            }

            
        }


        private CQPMessageBox(string message, string caption, CQPMessageBoxIcon icon) : this(message, caption)
        {
            Image image = GetImageFromIcon(icon);
            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.Image = image;

            primaryTable.Controls.Add(pictureBox, 0, 0);
        }


        internal static DialogResult ShowDialog(string message, string caption, CQPMessageBoxButtons buttons, CQPMessageBoxIcon icon)
        {
            CQPMessageBox messageBox = new CQPMessageBox(message, caption, icon);

            messageBox.AddButtons(buttons);

            return messageBox.ShowDialog();
        }

        internal static DialogResult ShowDialog(Control owner, string message, string caption, CQPMessageBoxButtons buttons, CQPMessageBoxIcon icon)
        {
            CQPMessageBox messageBox = new CQPMessageBox(message, caption, icon);

            messageBox.AddButtons(buttons);
            messageBox.StartPosition = FormStartPosition.CenterParent;

            return messageBox.ShowDialog(owner);
        }

        internal static DialogResult Show(string message)
        {
            return ShowDialog(message);
        }


        internal static DialogResult Show(string message, string caption, CQPMessageBoxButtons buttons, CQPMessageBoxIcon icon)
        {
            return ShowDialog(message, caption, buttons, icon);
        }


        internal static DialogResult ShowDialog(string message, string caption, 
                                                CQPMessageBoxButtons buttons, List<string> buttonText, CQPMessageBoxIcon icon)
        {
            CQPMessageBox messageBox = new CQPMessageBox(message, caption, icon);

            messageBox.AddButtons(buttons, buttonText);

            return messageBox.ShowDialog();
        }


        internal enum CQPMessageBoxButtons
        {
            OK,
            OKCancel,
            YesNo,
            YesNoCancel,
            SkipAbort,
            Custom
        }

        internal enum CQPMessageBoxIcon
        {
            Error,
            Information,
            Warning,
            Question
        }

        //internal enum CQPDialogResult
        //{
        //    OK,
        //    Cancel,
        //    Ignore,
        //    Yes,
        //    No,
        //    Skip,
        //    Abort,
        //    Delete
        //}

        //internal bool UseSecondRow
        //{
        //    set
        //    {
        //        if (value == true)
        //        {

        //        }
        //    }
        //}


        //internal string Caption
        //{
        //    set
        //    {
        //        this.Text = value;
        //    }
        //}

        //internal string Message
        //{

        //}


        internal static DialogResult Show(string message, string caption, CQPMessageBoxButtons buttons, List<string> buttonText, CQPMessageBoxIcon icon)
        {
            return ShowDialog(message, caption, buttons, buttonText, icon);
        }
    }
}
