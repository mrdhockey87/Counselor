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
    public partial class CQPGrayRectangleButton : CQPButton
    {
        public enum CQPGreyRectButtonSize
        {
            w65,
            w90,
            w120,
            w150,
            w180
        }

        CQPGreyRectButtonSize size;

        public CQPGrayRectangleButton() : base()
        {
            BackColor = Color.Empty;
            InitializeComponent();
            UpdateImages();

            
        }


        [Description("The image to show when the button is clicked."),
        Category("Appearance"),
        DefaultValue(CQPGreyRectButtonSize.w65),
        Browsable(true)]
        public CQPGreyRectButtonSize GreyRectSize
        {
            get { return size; }
            set { size = value; UpdateImages(); }
        }


        void UpdateImages()
        {
            switch (GreyRectSize)
            {
                case(CQPGreyRectButtonSize.w65):
                    SetW65Images();
                    break;
                case (CQPGreyRectButtonSize.w90):
                    SetW90Images();
                    break;
                case (CQPGreyRectButtonSize.w120):
                    SetW120Images();
                    break;
                case (CQPGreyRectButtonSize.w150):
                    SetW150Images();
                    break;
                case (CQPGreyRectButtonSize.w180):
                    SetW180Images();
                    break;
            }
        }

        void SetW65Images()
        {
            normalBackground = CounselQuickPlatinum.Properties.Resources._65_up;
            disabledBackground = CounselQuickPlatinum.Properties.Resources._65_disabled;
            hoverBackground = CounselQuickPlatinum.Properties.Resources._65_highlight;
            clickBackground = CounselQuickPlatinum.Properties.Resources._65_down;

            BackgroundImage = normalBackground;
            Size = CounselQuickPlatinum.Properties.Resources._65_up.Size;
        }

        void SetW90Images()
        {
            normalBackground = CounselQuickPlatinum.Properties.Resources._90_up;
            disabledBackground = CounselQuickPlatinum.Properties.Resources._90_disabled;
            hoverBackground = CounselQuickPlatinum.Properties.Resources._90_highlight;
            clickBackground = CounselQuickPlatinum.Properties.Resources._90_down;

            BackgroundImage = normalBackground;
            Size = CounselQuickPlatinum.Properties.Resources._90_up.Size;
        }


        void SetW120Images()
        {
            normalBackground = CounselQuickPlatinum.Properties.Resources._120_up;
            disabledBackground = CounselQuickPlatinum.Properties.Resources._120_disabled;
            hoverBackground = CounselQuickPlatinum.Properties.Resources._120_highlight;
            clickBackground = CounselQuickPlatinum.Properties.Resources._120_down;

            BackgroundImage = normalBackground;
            Size = CounselQuickPlatinum.Properties.Resources._120_up.Size;
        }


        void SetW150Images()
        {
            normalBackground = CounselQuickPlatinum.Properties.Resources._150_up;
            disabledBackground = CounselQuickPlatinum.Properties.Resources._150_disabled;
            hoverBackground = CounselQuickPlatinum.Properties.Resources._150_highlight;
            clickBackground = CounselQuickPlatinum.Properties.Resources._150_down;

            BackgroundImage = normalBackground;
            Size = CounselQuickPlatinum.Properties.Resources._150_up.Size;
        }


        void SetW180Images()
        {
            normalBackground = CounselQuickPlatinum.Properties.Resources._180_up;
            disabledBackground = CounselQuickPlatinum.Properties.Resources._180_disabled;
            hoverBackground = CounselQuickPlatinum.Properties.Resources._180_highlight;
            clickBackground = CounselQuickPlatinum.Properties.Resources._180_down;

            BackgroundImage = normalBackground;
            Size = CounselQuickPlatinum.Properties.Resources._180_up.Size;
        }


        private void CQPGrayRectangleButton_ParentChanged(object sender, EventArgs e)
        {
            //if (Parent == null)
            //    return;

            //if (Parent != null)
            //    this.BackColor = Parent.BackColor;

            //if (DesignMode)
            //{
            //    Refresh();
            //    Parent.Refresh();
            //}
        }


    }
}
