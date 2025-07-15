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
    public partial class DebugDialog : Form
    {
        public DebugDialog()
        {
            InitializeComponent();
            fontNameLabel.Text = Properties.Settings.Default.DefaultFont.Name;
            
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultFont = fontDialog1.Font;
        }

        private void defaultForeColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.DefaultForeColor = colorDialog1.Color;
                defaultForeColorText.Text = colorDialog1.Color.ToString();
            }
        }

        private void defaultDialogFontButton_Click(object sender, EventArgs e)
        {
            Font originalFont = Properties.Settings.Default.DefaultFont;

            if (fontDialog1.ShowDialog() == DialogResult.Cancel)
            {
                Properties.Settings.Default.DefaultFont = originalFont;
                fontNameLabel.Text = originalFont.Name;
                return;
            }
            else
            {
                Properties.Settings.Default.DefaultFont = fontDialog1.Font;
                fontNameLabel.Text = fontDialog1.Font.Name;
                return;
            }
        }

        private void defaultBackgroundColorButton_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.DefaultBackColor = colorDialog1.Color;
                defaultBackColorText.Text = colorDialog1.Color.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            Properties.Settings.Default.ButtonColor = colorDialog1.Color;
            buttonBackgroundColor.Text = colorDialog1.Color.ToString();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Font originalFont = Properties.Settings.Default.ButtonFont;

            DialogResult result = fontDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                Properties.Settings.Default.ButtonFont = originalFont;
                return;
            }
            else
            {
                Properties.Settings.Default.ButtonFont = fontDialog1.Font;
                buttonFontLabel.Text = fontDialog1.Font.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            Properties.Settings.Default.ButtonForeColor = colorDialog1.Color;
            buttonFontLabel.Text = colorDialog1.Color.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            Properties.Settings.Default.DataGridViewBackColor = colorDialog1.Color;
            dataGridViewBackColorLabel.Text = colorDialog1.Color.ToString();
        }


    }
}
