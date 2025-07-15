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
    public partial class CQPTabControl : UserControl
    {
        List<Control> controls;
        int currentControlID;

        public CQPTabControl()
        {
            controls = new List<Control>();

            Load += new EventHandler(OnLoad);

            InitializeComponent();
            currentControlID = -1;
        }

        void OnLoad(object sender, EventArgs e)
        {
            foreach (Control control in controls)
                control.Visible = false;

            //if (controls.Count > 0)
            //{
            //    controls[0].Visible = true;
            //    controls[0].BringToFront();

            //}
        }


        internal void AddControl(Control control)
        {
            controls.Add(control);

            Controls.Add(control);
            control.Visible = false;
            control.Dock = DockStyle.Fill;
            
            //currentControlID = controls.Count - 1;
            //controls[currentControlID].Visible = true;
            //controls
        }


        internal void Initialize()
        {
            //if(controls.Count)
        }


        internal void Switch(int controlID)
        {
            if (currentControlID == controlID)
                return;

            controls[controlID].Visible = true;

            if (currentControlID > 0)
            {
                controls[currentControlID].Visible = false;
                controls[currentControlID].SendToBack();
            }

            controls[controlID].BringToFront();

            currentControlID = controlID;
        }


    }
}
