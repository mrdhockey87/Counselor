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
    public partial class CQPPrintButton : CQPButton
    {
        public CQPPrintButton()
        {
            InitializeComponent();

            normalIcon = CounselQuickPlatinum.Properties.Resources.print_up;
            disabledIcon = CounselQuickPlatinum.Properties.Resources.print_disabled;
            clickIcon = CounselQuickPlatinum.Properties.Resources.print_down;
            hoverIcon = CounselQuickPlatinum.Properties.Resources.print_highlight;
        }
    }
}
