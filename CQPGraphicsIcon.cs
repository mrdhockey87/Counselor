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
    public partial class CQPGraphicsButton : CQPButton
    {
        public CQPGraphicsButton()
        {
            InitializeComponent();
        }

        [Description("The image to show normally."),
        Category("Appearance"),
        DefaultValue(""),
        Browsable(true)]
        public Image NormalIcon
        {
            get { return normalIcon; }
            set { normalIcon = value; /*UpdateImages();*/ }
        }

        [Description("The image to show on hover."),
        Category("Appearance"),
        DefaultValue(""),
        Browsable(true)]
        public Image HoverIcon
        {
            get { return hoverIcon; }
            set { hoverIcon = value; /*UpdateImages();*/ }
        }


        [Description("The image to show on hover."),
        Category("Appearance"),
        DefaultValue(""),
        Browsable(true)]
        public Image ClickIcon
        {
            get { return clickIcon; }
            set { clickIcon = value; /*UpdateImages();*/ }
        }


        [Description("The image to show on hover."),
        Category("Appearance"),
        DefaultValue(""),
        Browsable(true)]
        public Image DisabledIcon
        {
            get { return disabledIcon; }
            set { disabledIcon = value; /*UpdateImages();*/ }
        }
    }
}
