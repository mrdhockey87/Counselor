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
    public partial class CQPTextButton : Button
    {
        public enum ButtonSize
        {
            w65,
            w90,
            w120,
            w150,
            w180
        }

        //Image defaultImage;
        //Image hoverImage;
        //Image downImage;
        //Image disabledImage;

        public CQPTextButton()
        {
            InitializeComponent();

            SetImages();
        }


        [Description("The image to show when the button is clicked."),
         Category("Appearance"),
         DefaultValue(ButtonSize.w65),
         Browsable(true)]
        public ButtonSize RectButtonSize
        {
            get;
            set;
        }

        public void SetImages()
        {
            switch (RectButtonSize)
            {
                case(ButtonSize.w65):
                    {
                        //defaultImage = 
                        break;
                    }
            }
        }


    }
}
