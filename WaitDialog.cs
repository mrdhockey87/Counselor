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
    public partial class WaitDialog : Form
    {
        public WaitDialog()
        {
            InitializeComponent();
        }

        public WaitDialog(string text)
        {
            InitializeComponent();
            cqpLabel1.Text = text;
        }

        public void UpdateProgress(int value)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate 
                {
                    PeformUpdateProgress(value);
                });
            }
            else
                PeformUpdateProgress(value);
        }

        private void PeformUpdateProgress(int value)
        {
            progressBar1.Value = value;
            this.Refresh();
        }
    }
}
