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
    public partial class CQPSmallPillButton : CQPButton
    {
        public CQPSmallPillButton()
        {
            InitializeComponent();

            normalBackground = CounselQuickPlatinum.Properties.Resources.smpill_up;
            clickBackground = CounselQuickPlatinum.Properties.Resources.smpill_down;
            disabledBackground = CounselQuickPlatinum.Properties.Resources.smpill_disabled;
            hoverBackground = CounselQuickPlatinum.Properties.Resources.smpill_highligh;
        }



    }
}
