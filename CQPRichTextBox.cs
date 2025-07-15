using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    public partial class CQPRichTextBox : UserControl
    {
        

        public CQPRichTextBox()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        public new event EventHandler TextChanged;
        

        [Category("Appearance"),
        Browsable(true)]
        internal new string Text
        {
            get
            {
                return richTextBox1.Text;
            }

            set
            {
                richTextBox1.Text = value;
            }
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(sender, e);
        }
    }
}
