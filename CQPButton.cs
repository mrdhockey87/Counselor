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
    public partial class CQPButton : Button
    {
        protected Image normalBackground;
        protected Image hoverBackground;
        protected Image clickBackground;
        protected Image disabledBackground;

        protected Image normalIcon;
        protected Image hoverIcon;
        protected Image clickIcon;
        protected Image disabledIcon;

        public CQPButton()
        {
            InitializeComponent();
            DoubleBuffered = true;
            //Move += new EventHandler(CQPButton_Move);
            
            //BackColor = Parent.BackColor;

            //FlatStyle = FlatStyle.Flat;
            //BackColor = Color.Transparent;
            //AutoSize = true;
            //AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //FlatAppearance.BorderSize = 0;

            GotFocus += new EventHandler(CQPButton_GotFocus);
            LostFocus += new EventHandler(CQPButton_LostFocus);
        }

        protected void CQPButton_LostFocus(object sender, EventArgs e)
        {
            CQPButton_MouseLeave(sender, e);
        }

        protected void CQPButton_GotFocus(object sender, EventArgs e)
        {
            CQPButton_MouseEnter(sender, e);
        }

        //void CQPButton_Move(object sender, EventArgs e)
        //{
        //    //RaisePaintEvent(this, new PaintEventArgs(this.CreateGraphics(), this.Size);
        //    Invalidate();
        //}

        //protected void RefreshSelf()
        //{
        //    if (Parent != null)
        //        BackColor = Parent.BackColor;
        //}


        //public override void OnLoad()
        //{
        //    DoubleBuffered = true;
        //    BackgroundImageLayout = ImageLayout.Zoom;
        //}



        protected void CQPButton_EnabledChanged(object sender, EventArgs e)
        {
            if (Enabled)
            {
                if (normalBackground != null)
                    BackgroundImage = normalBackground;
                if (normalIcon != null)
                    Image = normalIcon;
            }
            else
            {
                if (disabledBackground != null)
                    BackgroundImage = disabledBackground;
                if (disabledIcon != null)
                    Image = disabledIcon;
            }
        }

        protected void CQPButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(clickBackground != null)
                    BackgroundImage = clickBackground;
                if (clickIcon != null)
                    Image = clickIcon;
            }
        }

        protected void CQPButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(normalBackground != null)
                    BackgroundImage = normalBackground;
                if (normalIcon != null)
                    Image = normalIcon;
            }
        }

        protected void CQPButton_MouseEnter(object sender, EventArgs e)
        {
            if(hoverBackground != null)
                BackgroundImage = hoverBackground;
            if (hoverIcon != null)
                Image = hoverIcon;
        }

        protected void CQPButton_MouseLeave(object sender, EventArgs e)
        {
            if (Enabled)
            {
                if (normalBackground != null)
                    BackgroundImage = normalBackground;
                if (normalIcon != null)
                    Image = normalIcon;
            }
            else
            {
                if (disabledBackground != null)
                    BackgroundImage = disabledBackground;
                if (disabledIcon != null)
                    Image = disabledIcon;
            }
        }

        protected virtual void CQPButton_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null)
            {

            }
        }


        protected void CQPButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            if (BackgroundImage != null)
                e.Graphics.DrawImage(BackgroundImage, this.ClientRectangle);
                //e.Graphics.DrawImage(BackgroundImage, e.ClipRectangle);

            if (Image != null)
            {
                RectangleF rect = new RectangleF();
                rect.X = (float)((this.ClientSize.Width - Image.Size.Width) / 2.0);
                rect.Y = (float)((this.ClientSize.Height - Image.Size.Height) / 2.0);
                rect.Width = Image.Width;
                rect.Height = Image.Height;
                e.Graphics.DrawImage(Image, rect);
            }

            if (Text != null)
            {
                SizeF size = e.Graphics.MeasureString(this.Text, this.Font);

                // Center the text inside the client area of the PictureButton.
                e.Graphics.DrawString(this.Text,
                    this.Font,
                    new SolidBrush(this.ForeColor),
                    (this.ClientSize.Width - size.Width) / 2,
                    (this.ClientSize.Height - size.Height) / 2);
            }

        }

        //private void CQPButton_Enter(object sender, EventArgs e)
        //{
        //    CQPButton_MouseEnter(sender, e);
        //}

        //private void CQPButton_Leave(object sender, EventArgs e)
        //{
        //    CQPButton_MouseLeave(sender, e);
        //}



        
    }
}
