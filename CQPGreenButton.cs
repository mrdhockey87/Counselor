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
    public partial class CQPGreenButton : CQPButton
    {
        public CQPGreenButton() : base()
        {
            InitializeComponent();

            normalBackground = CounselQuickPlatinum.Properties.Resources.new90_up;
            hoverBackground = CounselQuickPlatinum.Properties.Resources.new90_highlight;
            clickBackground = CounselQuickPlatinum.Properties.Resources.new90_down;
            disabledBackground = CounselQuickPlatinum.Properties.Resources.new90_disabled;
            //Added to make the text color of all new buttons white mdail 1-11-19
            ForeColor = Color.White;
            BackColor = Color.Empty;
            UpdateImages();
        }

        public enum CQPGreenRectButtonSize
        {
            w65,
            w90,
            w120,
            w150,
            w180
        }

        CQPGreenRectButtonSize size;



        [Description("The image to show when the button is clicked."),
        Category("Appearance"),
        DefaultValue(CQPGreenRectButtonSize.w65),
        Browsable(true)]
        public CQPGreenRectButtonSize GreyRectSize
        {
            get { return size; }
            set { size = value; UpdateImages(); }
        }


        void UpdateImages()
        {
            switch (GreyRectSize)
            {
                case (CQPGreenRectButtonSize.w65):
                    SetW65Images();
                    break;
                case (CQPGreenRectButtonSize.w90):
                    SetW90Images();
                    break;
                case (CQPGreenRectButtonSize.w120):
                    SetW120Images();
                    break;
            }
        }

        void SetW65Images()
        {
            //normalBackground = CounselQuickPlatinum.Properties.Resources._65_up;
            //disabledBackground = CounselQuickPlatinum.Properties.Resources._65_disabled;
            //hoverBackground = CounselQuickPlatinum.Properties.Resources._65_highlight;
            //clickBackground = CounselQuickPlatinum.Properties.Resources._65_down;
            
            Size = CounselQuickPlatinum.Properties.Resources._65_up.Size;
            BackgroundImage = normalBackground;
        }

        void SetW90Images()
        {
            //normalBackground = CounselQuickPlatinum.Properties.Resources._90_up;
            //disabledBackground = CounselQuickPlatinum.Properties.Resources._90_disabled;
            //hoverBackground = CounselQuickPlatinum.Properties.Resources._90_highlight;
            //clickBackground = CounselQuickPlatinum.Properties.Resources._90_down;
            
            Size = CounselQuickPlatinum.Properties.Resources._90_up.Size;
            BackgroundImage = normalBackground;
        }


        void SetW120Images()
        {
            //normalBackground = CounselQuickPlatinum.Properties.Resources._120_up;
            //disabledBackground = CounselQuickPlatinum.Properties.Resources._120_disabled;
            //hoverBackground = CounselQuickPlatinum.Properties.Resources._120_highlight;
            //clickBackground = CounselQuickPlatinum.Properties.Resources._120_down;

            Size = CounselQuickPlatinum.Properties.Resources._120_up.Size;
            BackgroundImage = normalBackground;
        }


        
    }
}
