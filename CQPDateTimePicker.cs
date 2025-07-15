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
    public partial class CQPDateTimePicker : UserControl
    {
        public CQPDateTimePicker()
        {
            //onValueChanged = new EventHandler(new object, new EventArgs){};
            InitializeComponent();
        }


        [Browsable(true)]
        public event EventHandler ValueChanged;


        public DateTime Value
        {
            get
            {
                return dateTimePicker1.Value;
            }
            set
            {
                dateTimePicker1.Value = value;
            }
        }

        public string CustomFormat 
        {
            get
            {
                return dateTimePicker1.CustomFormat;
            }

            set
            {
                dateTimePicker1.CustomFormat = value;
            }
        }

        public DateTimePickerFormat Format 
        {
            get
            {
                return dateTimePicker1.Format;
            }

            set
            {
                dateTimePicker1.Format = value;
            }
        }

        public System.Drawing.Font CalendarFont 
        {
            get
            {
                return dateTimePicker1.CalendarFont;
            }

            set
            {
                dateTimePicker1.CalendarFont = value;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }

    }
}
