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
    public partial class CQPSearchBox : TextBox
    {
        Color defaultColor;

        public CQPSearchBox()
        {
            InitializeComponent();

            defaultColor = BackColor;
            
            //Enter += CQPTextboxEntered;
            //Leave += CQPTextboxLeft;
            TextChanged += CQPTextboxChanged;
        }

        bool HasText()
        {
            if (Text == "")
                return false;
            else
                return true;
        }

        //void CQPTextboxLeft(object sender, EventArgs e)
        //{
        //    if (HasText())
        //        ColorBlue();
        //    else
        //        ColorGray();
        //}

        void CQPTextboxChanged(object sender, EventArgs e)
        {
            if (HasText())
                ColorBlue();
            else
                ColorGray();
        }

        void ColorBlue()
        {
            BackColor = Color.Aqua;
        }

        void ColorGray()
        {
            BackColor = defaultColor;
        }
    }
}
