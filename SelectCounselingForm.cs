using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    public partial class SelectCounselingForm : Form
    {
        public SelectCounselingForm(int soldierID)
        {
            InitializeComponent();
            GetAllCounselingsForSoldier(soldierID);
        }

        private void GetAllCounselingsForSoldier(int soldierID)
        {
            //DataTable 
        }

        
    }
}
