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
    public partial class CQPButtonAddNew : CQPButton
    {
        public CQPButtonAddNew() : base()
        {
            InitializeComponent();

            //normal = CounselQuickPlatinum.Properties.Resources.ad

            //BackgroundImage = CounselQuickPlatinum.Properties.Resources.Image1;
            //MouseDown += OnMouseClicked;
            //MouseUp += OnMouseUpEvent;
        }


        //private void OnMouseClicked(object sender, MouseEventArgs e)
        //{
        //    if(e.Button == MouseButtons.Left)
        //        BackgroundImage = CounselQuickPlatinum.Properties.Resources.PackageIcon;
        //}

        //private void OnMouseUpEvent(object sender, EventArgs e)
        //{
        //    BackgroundImage = CounselQuickPlatinum.Properties.Resources.Image1;
        //}


        //[Description("The image to show when the button is clicked."),
        // Category("Appearance"),
        // DefaultValue("CounselQuickPlatinum.Properties.Resources.Image1"),
        // Browsable(true)]
        //public Image OnMouseDownImage { get; set; }

    }
}
