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
    public partial class CQPGraphicsCheckbox : CQPGraphicsButton
    {
        bool Checked { get; set; }

        Image activeImage;

        Image hover;
        Image click;
        Image normal;
        Image disabled;

        public CQPGraphicsCheckbox()
        {
            InitializeComponent();
            Checked = false;
        }


        [Description("The image to show when active."),
        Category("Appearance"),
            //DefaultValue(""),
        Browsable(true)]
        public Image ActiveImage
        {
            get { return activeImage; }
            set { activeImage = value; }
        }


        private void CQPGraphicsCheckbox_Click(object sender, EventArgs e)
        {
            if (Checked)
            {
                hoverIcon = hover;
                clickIcon = click;
                normalIcon = normal;
                disabledIcon = disabled;

                Checked = false;
            }
            else
            {
                // back up the icons so we can reload them later
                hover = hoverIcon;
                click = clickIcon;
                normal = normalIcon;
                disabled = disabledIcon;

                hoverIcon = activeImage;
                clickIcon = activeImage;
                normalIcon = activeImage;
                disabledIcon = activeImage;

                Checked = true;
            }
        }


    }
}
