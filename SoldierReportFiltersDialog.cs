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
    public partial class SoldierReportFiltersDialog : Form
    {
        public class UnitHierarchyCheckState
        {
            public bool battalionChecked;
            public bool unitChecked;
            public bool platoonChecked;
            public bool squadChecked;

            public UnitHierarchyCheckState()
            {
                battalionChecked = false;
                unitChecked = false;
                platoonChecked = false;
                squadChecked = false;
            }
        }

        string unitIDFilters;
        string rankingIDFilters;
        string statusIDFilters;
        string otherStatusFilters;

        int prevSoldiersTableHashCode;

        List<int> selectedUnitHierarchyIDs;

        Dictionary<UnitHierarchyModel.UnitHierarchy, UnitHierarchyCheckState> unitHierarchyCheckStates;
        List<int> checkedRankingIDs;
        List<int> checkedStatusIDs;
        List<string> otherStatusText;

        bool firstTime = true;

        public SoldierReportFiltersDialog()
        {
            prevSoldiersTableHashCode = 0;

            Load += new EventHandler(SoldierReportFiltersDialog_Load);
            InitializeComponent();
            checkedRankingIDs = new List<int>();
            checkedStatusIDs = new List<int>();
            selectedUnitHierarchyIDs = new List<int>();
            otherStatusText = new List<string>();
            unitHierarchyCheckStates = new Dictionary<UnitHierarchyModel.UnitHierarchy, UnitHierarchyCheckState>();
            if (DesignMode)
            {
                Refresh();
            }
        }


        void SoldierReportFiltersDialog_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (firstTime)
                {
                    RefreshRankingCheckListBox();
                    firstTime = false;
                }

                RefreshSoldierTreeView();
                RefreshStatusComboBox();

                rankingCheckListBox.DisplayMember = "Text";
                checkedListBox2.DisplayMember = "Text";
            }
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }


        private void BuildUnitHierarchyTreeNode()
        {
            //DataTable soldiersTable = SoldierModel.FormattedSoldiersTableCopy;
            DataSet unitsDataSet = UnitHierarchyModel.GetAllUnitInfo();
            
            DataTable unitHierarchyTable = unitsDataSet.Tables["unithierarchies"];
            DataColumn []unitHierarchyPrimaryKey = new DataColumn[] { unitHierarchyTable.Columns["unithierarchyid"] };

            unitHierarchyTable.PrimaryKey = unitHierarchyPrimaryKey;

            //DataRow[] filteredUnitHierarchies
            //    = unitHierarchyTable.AsEnumerable().Where(uh => soldiersTable.Select("unithierarchyid = " + Convert.ToInt32(uh["unithierarchyid"])).Count() > 0).ToArray();

            DataRow[] filteredUnitHierarchies = UnitHierarchyModel.GetNonEmptyUnitHierarchies();
            
            List<UnitHierarchyModel.UnitHierarchy> unitHierarchies = new List<UnitHierarchyModel.UnitHierarchy>();
            foreach (DataRow row in filteredUnitHierarchies)
            {
                int uhID = Convert.ToInt32(row["unithierarchyid"]);
                UnitHierarchyModel.UnitHierarchy uh = UnitHierarchyModel.GetUnitHierarchyByID(uhID);
                unitHierarchies.Add(uh);
            }

            foreach (UnitHierarchyModel.UnitHierarchy unitHierarchy in unitHierarchies)
            {
                UnitHierarchyCheckState checkState;
                if(unitHierarchyCheckStates.ContainsKey(unitHierarchy))
                    checkState = unitHierarchyCheckStates[unitHierarchy];
                else
                    checkState = new UnitHierarchyCheckState();

                BuildBattalionTreeNode(unitHierarchy, checkState);
            }
        }


        private void BuildBattalionTreeNode(UnitHierarchyModel.UnitHierarchy unitHierarchy, UnitHierarchyCheckState checkState)
        {
            int battalionID = unitHierarchy.battalionID;
            TreeNode battalionNode;

            List<TreeNode> nodes = soldiersTreeView.Nodes.Cast<TreeNode>().Where(node => ((SoldierReportTag)node.Tag).id == battalionID).ToList();

            if (nodes.Count() > 0)
            {
                battalionNode = nodes[0];
            }
            else
            {
                battalionNode = new TreeNode();

                SoldierReportTag tag = new SoldierReportTag();
                tag.id = unitHierarchy.battalionID;
                tag.levelID = UnitHierarchyTreeLevel.Battalion;

                battalionNode.Tag = tag;
                battalionNode.Text = unitHierarchy.battalionName;
                
                soldiersTreeView.Nodes.Add(battalionNode);
            }

            // is this node checked?
            if (checkState.battalionChecked)
                battalionNode.Checked = true;

            BuildUnitTreeNode(unitHierarchy, battalionNode, checkState);

            battalionNode.ExpandAll();
        }


        private void BuildUnitTreeNode(UnitHierarchyModel.UnitHierarchy unitHierarchy, TreeNode battalionNode, UnitHierarchyCheckState checkState)
        {
            int unitID = unitHierarchy.unitID;
            TreeNode unitNode;

            List<TreeNode> nodes = battalionNode.Nodes.Cast<TreeNode>().Where(node => ((SoldierReportTag)node.Tag).id == unitID).ToList();

            if (nodes.Count() > 0)
            {
                unitNode = nodes[0];
            }
            else
            {
                unitNode = new TreeNode();

                SoldierReportTag tag = new SoldierReportTag();
                tag.id = unitHierarchy.unitID;
                tag.designatorid = unitHierarchy.unitDesignatorID;
                tag.levelID = UnitHierarchyTreeLevel.Unit;

                unitNode.Tag = tag;

                string unitName = UnitHierarchyModel.GetUnitName(unitID);
                string unitDesignatorName = UnitHierarchyModel.GetUnitDesignatorName(unitHierarchy.unitDesignatorID);
                unitNode.Text = unitName + " " + unitDesignatorName;

                battalionNode.Nodes.Add(unitNode);
            }

            if (checkState.unitChecked)
                unitNode.Checked = true;

            BuildPlatoonTreeNode(unitHierarchy, unitNode, checkState);
        }


        private void BuildPlatoonTreeNode(UnitHierarchyModel.UnitHierarchy unitHierarchy, TreeNode unitNode, UnitHierarchyCheckState checkState)
        {
            int platoonID = unitHierarchy.platoonID;
            TreeNode platoonNode;

            List<TreeNode> nodes = unitNode.Nodes.Cast<TreeNode>().Where(node => ((SoldierReportTag)node.Tag).id == platoonID).ToList();

            if (nodes.Count() > 0)
            {
                platoonNode = nodes[0];
            }
            else
            {
                platoonNode = new TreeNode();

                SoldierReportTag tag = new SoldierReportTag();
                tag.id = unitHierarchy.platoonID;
                tag.levelID = UnitHierarchyTreeLevel.Platoon;

                platoonNode.Tag = tag;

                string platoonName = UnitHierarchyModel.GetPlatoonName(platoonID);
                platoonNode.Text = "Platoon " + platoonName;

                unitNode.Nodes.Add(platoonNode);
            }

            if (checkState.platoonChecked)
                platoonNode.Checked = true;

            BuildSquadTreeNode(unitHierarchy, platoonNode, checkState);
        }


        private void BuildSquadTreeNode(UnitHierarchyModel.UnitHierarchy unitHierarchy, TreeNode platoonNode, UnitHierarchyCheckState checkState)
        {
            int squadID = unitHierarchy.squadID;
            TreeNode squadNode;

            List<TreeNode> nodes = platoonNode.Nodes.Cast<TreeNode>().Where(node => ((SoldierReportTag)node.Tag).id == squadID).ToList();


            if (nodes.Count() > 0)
            {
                squadNode = nodes[0];
            }
            else
            {
                squadNode = new TreeNode();

                SoldierReportTag tag = new SoldierReportTag();
                tag.id = unitHierarchy.squadID;
                tag.levelID = UnitHierarchyTreeLevel.SquadSection;

                squadNode.Tag = tag;

                string squadName = UnitHierarchyModel.GetSectionSquadName(squadID);
                squadNode.Text = "Squad " + squadName;
                    
                platoonNode.Nodes.Add(squadNode);
            }

            if (checkState.squadChecked)
                squadNode.Checked = true;
        }


        private void RefreshSoldierTreeView()
        {
            int numberOfNodes = soldiersTreeView.Nodes.Count;
            for (int i = 0; i < numberOfNodes; i++)
            {
                soldiersTreeView.Nodes.RemoveAt(0);
            }

            BuildUnitHierarchyTreeNode();
        }

        void RefreshRankingCheckListBox()
        {
            DataTable rankings = RankingModel.GetRankingTable();
            foreach (DataRow ranking in rankings.Rows)
            {
                string rankingText = ranking["rankingabbreviation"].ToString();
                int rankingID = Convert.ToInt32(ranking["rankingid"]);

                ListViewItem item = new ListViewItem();
                item.Text = rankingText;
                item.Tag = rankingID;

                if (checkedRankingIDs.Contains(rankingID))
                    item.Checked = true;

                rankingCheckListBox.Items.Add(item);
            }
        }


        void RefreshStatusComboBox()
        {
            int soldiersTableHashCode = SoldierModel.GetSoldierTableHashcode(); //SoldierModel.FormattedSoldiersTableCopy.GetHashCode();
            if (prevSoldiersTableHashCode == soldiersTableHashCode)
                return;

            prevSoldiersTableHashCode = soldiersTableHashCode;

            int numberOfNodes = checkedListBox2.Items.Count;
            for (int i = 0; i < numberOfNodes; i++)
                checkedListBox2.Items.RemoveAt(0);

            DataTable statusesEnumsTable = SoldierModel.GetAllStatusEnums();
            foreach(DataRow status in statusesEnumsTable.Rows)
            {
                string name = status["statusenumtext"].ToString();
                int statusID = Convert.ToInt32(status["statusenumid"]);

                ListViewItem item = new ListViewItem();
                item.Text = name;
                item.Tag = statusID;

                if (checkedStatusIDs.Contains(statusID))
                    item.Checked = true;

                checkedListBox2.Items.Add(item);
            }

            DataTable soldiersTable = SoldierModel.FormattedSoldiersTableCopy;
            DataView soldiersView = new DataView(soldiersTable);
            DataTable distinctValues = soldiersView.ToTable(true, "otherstatustext");

            foreach (DataRow otherStatus in distinctValues.Rows)
            {
                if (otherStatus["otherstatustext"].ToString() == "")
                    continue;

                ListViewItem item = new ListViewItem();
                item.Text = otherStatus["otherstatustext"].ToString();
                item.Tag = -1;

                checkedListBox2.Items.Add(item);
            }


            /*
            foreach (DataRow soldierRow in soldiersTable.Rows)
            {
                int id = Convert.ToInt32(soldierRow["soldierid"]);
                Soldier soldier = new Soldier(id);
                
                if (soldier.OtherStatus == "")
                    continue;



                ListViewItem item = new ListViewItem();
                item.Text = soldier.OtherStatus;
                item.Tag = -1;

                checkedListBox2.Items.Add(item);
            }
             * */
        }


        internal string UnitIDFilters
        {
            get
            {
                return unitIDFilters;
            }
        }


        internal string RankingIDFilters
        {
            get
            {
                return rankingIDFilters;
            }
        }

        internal string StatusIDFilters
        {
            get
            {
                return statusIDFilters;
            }
        }

        internal string OtherStatusFilters
        {
            get
            {
                return otherStatusFilters;
            }
        }

        private void soldiersTreeView_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            bool isRootTrigger = (e.Action == TreeViewAction.ByKeyboard
                                    || e.Action == TreeViewAction.ByMouse);

            if (isRootTrigger && soldiersTreeView.Enabled == false)
                return;

            if (isRootTrigger)
            {
                soldiersTreeView.Enabled = false;
            }

            RecursiveTreeSelect(e.Node);

            soldiersTreeView.SelectedNode = e.Node;

            if (isRootTrigger)
            {
                soldiersTreeView.SelectedNode = e.Node;
                soldiersTreeView.Enabled = true;
            }
        }


        private void RecursiveTreeSelect(TreeNode node)
        {
            TreeNode n = new TreeNode();
            try
            {
                bool bCheck = node.Checked;
                soldiersTreeView.SelectedNode = node;

                if (soldiersTreeView.SelectedNode.Nodes.Count == 0)
                    return;

                n = soldiersTreeView.SelectedNode.Nodes[0];

                if (node != n)
                {
                    while (n != null)
                    {
                        n.Checked = bCheck;
                        //RecursiveTreeSelect(n);
                        n = n.NextNode;
                    }
                }
            }
            catch (System.Exception)
            {
                // this catches when the soldier nodes try to catch their children
            }
        }


        private void applyButton_Click(object sender, EventArgs e)
        {
            ClearLists();
            PopulateCheckedItemLists();
            AppendAssignmentIDFilters();
            AppendRankingIDFilters();
            AppendStatusIDFilters();
            ApplyOtherStatusFilters();
        }


        private void ClearLists()
        {
            checkedRankingIDs.Clear();
            checkedStatusIDs.Clear();
            selectedUnitHierarchyIDs.Clear();
            unitHierarchyCheckStates.Clear();
            otherStatusText.Clear();
        }


        private void PopulateCheckedItemLists()
        {
            GetSelectedUnitHierarchies();
            GetSelectedRankingIDs();
            GetSelectedStatusIDs();

        }



        private List<int> GetSelectedUnitHierarchies()
        {
            selectedUnitHierarchyIDs = new List<int>();

            bool battalionChecked;
            bool unitChecked;
            bool platoonChecked;
            bool squadChecked;

            foreach (TreeNode battalionNode in soldiersTreeView.Nodes)
            {
                SoldierReportTag battalionTag = (SoldierReportTag)battalionNode.Tag;
                int battalionID = battalionTag.id;
                battalionChecked = battalionNode.Checked;

                foreach (TreeNode unitNode in battalionNode.Nodes)
                {
                    SoldierReportTag unitTag = (SoldierReportTag)unitNode.Tag;
                    int unitID = unitTag.id;
                    int unitDesignatorID = unitTag.designatorid;

                    unitChecked = unitNode.Checked;

                    foreach (TreeNode platoonNode in unitNode.Nodes)
                    {
                        SoldierReportTag platoonTag = (SoldierReportTag)platoonNode.Tag;
                        int platoonID = platoonTag.id;

                        platoonChecked = platoonNode.Checked;

                        foreach (TreeNode squadSectionNode in platoonNode.Nodes)
                        {
                            SoldierReportTag squadSectionTag = (SoldierReportTag)squadSectionNode.Tag;
                            int squadSectionID = squadSectionTag.id;

                            squadChecked = squadSectionNode.Checked;

                            UnitHierarchyCheckState checkState = new UnitHierarchyCheckState();
                            checkState.battalionChecked = battalionChecked;
                            checkState.unitChecked = unitChecked;
                            checkState.platoonChecked = platoonChecked;
                            checkState.squadChecked = squadChecked;

                            UnitHierarchyModel.UnitHierarchy unitHierarchy = new UnitHierarchyModel.UnitHierarchy();

                            unitHierarchy.battalionID = battalionID;
                            unitHierarchy.battalionName = UnitHierarchyModel.GetBattalionName(battalionID);
                            unitHierarchy.unitID = unitID;
                            unitHierarchy.unitDesignatorID = unitDesignatorID;
                            unitHierarchy.platoonID = platoonID;
                            unitHierarchy.squadID = squadSectionID;
                            int unitHierarchyID = UnitHierarchyModel.GetUnitHierarchyIDIfExists(unitHierarchy);
                            unitHierarchy.unitHierarchyID = unitHierarchyID;

                            unitHierarchyCheckStates.Add(unitHierarchy, checkState);

                            if (!squadChecked)
                                continue;

                            
                            selectedUnitHierarchyIDs.Add(unitHierarchyID);
                        }
                    }
                }
            }

            return selectedUnitHierarchyIDs;
        }


        private void GetSelectedRankingIDs()
        {
            foreach (ListViewItem rankingItem in rankingCheckListBox.CheckedItems)
            {
                int rankingID = Convert.ToInt32(rankingItem.Tag);
                checkedRankingIDs.Add(rankingID);
            }
        }


        private void GetSelectedStatusIDs()
        {
            foreach (ListViewItem statusItem in checkedListBox2.CheckedItems)
            {
                int statusID = Convert.ToInt32(statusItem.Tag);

                if (statusID > -1)
                    checkedStatusIDs.Add(statusID);
                else
                    otherStatusText.Add(statusItem.Text);
            }
        }


        private void AppendAssignmentIDFilters()
        {
            unitIDFilters = "";
            foreach (int unitHierarchyID in selectedUnitHierarchyIDs)
            {
                unitIDFilters += "unithierarchyid = " + unitHierarchyID + " or ";
            }

            if(unitIDFilters.EndsWith(" or "))
            {
                int start = unitIDFilters.Length - 4;
                unitIDFilters = unitIDFilters.Remove(start);
            }

            if (unitIDFilters.Length != 0)
            {
                unitIDFilters = unitIDFilters.Insert(0, "(");
                unitIDFilters += ")";
            }
        }


        void AppendRankingIDFilters()
        {
            rankingIDFilters = "( ";

            foreach(int rankingID in checkedRankingIDs)
            {
                rankingIDFilters += " rankingid = " + rankingID + " or ";
            }

            if (rankingIDFilters.EndsWith(" or "))
                rankingIDFilters = rankingIDFilters.Remove(rankingIDFilters.Length - 4);

            if (rankingIDFilters.EndsWith("and ( "))
                rankingIDFilters = rankingIDFilters.Remove(rankingIDFilters.Length - "and ( ".Length);
            else if (rankingIDFilters.EndsWith("( "))
                rankingIDFilters = rankingIDFilters.Remove(rankingIDFilters.Length - "( ".Length);
            else
                rankingIDFilters += " )";
        }


        void AppendStatusIDFilters()
        {
            statusIDFilters = "( ";

            foreach(int statusID in checkedStatusIDs)
            {
                statusIDFilters += " statusenumid = " + statusID + " or ";
            }

            if (statusIDFilters.EndsWith(" or "))
                statusIDFilters = statusIDFilters.Remove(statusIDFilters.Length - 4);

            if (statusIDFilters.EndsWith("( "))
                statusIDFilters = statusIDFilters.Remove(statusIDFilters.Length - "( ".Length);
            else
                statusIDFilters += " )";

        }


        void ApplyOtherStatusFilters()
        {
            otherStatusFilters = "( ";

            foreach (string status in otherStatusText)
                otherStatusFilters += " otherstatustext = '" + status + "' or ";

            if (otherStatusFilters.EndsWith(" or "))
                otherStatusFilters = otherStatusFilters.Remove(otherStatusFilters.Length - 4);

            if (otherStatusFilters.EndsWith("( "))
                otherStatusFilters = otherStatusFilters.Remove(otherStatusFilters.Length - "( ".Length);
            else
                otherStatusFilters += " )";
        }
        

        private void selectAllRankingsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rankingCheckListBox.Items.Count; i++)
            {
                rankingCheckListBox.SetItemChecked(i, true);
            }
        }

        private void deselectAllRankingsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rankingCheckListBox.Items.Count; i++)
            {
                rankingCheckListBox.SetItemChecked(i, false);
            }
        }

        private void selectAllStatusesButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, true);
            }
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SoldierReportFiltersDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Added to save the forms location so it can be restored mdail 8-19-19
            SaveLocation();
        }
        //save the forms location mdail 8-16-19
        private void SaveLocation()
        {
            // restore form's window state - if the windows state is normal or maximized the same as is if minimized the save it as normal mdail 8-13-19
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    Properties.Settings.Default.SoldierReportFiltersDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.SoldierReportFiltersDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.SoldierReportFiltersDialogSize = bounds.Size;
            Properties.Settings.Default.SoldierReportFiltersDialogLocation = bounds.Location;
            Properties.Settings.Default.SoldierReportFiltersDialogHeight = bounds.Height;
            Properties.Settings.Default.SoldierReportFiltersDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.SoldierReportFiltersDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.SoldierReportFiltersDialogLocation, Properties.Settings.Default.SoldierReportFiltersDialogSize);
                switch (winState)
                {
                    case "Normal":
                        this.WindowState = FormWindowState.Normal;
                        break;
                    case "Maximized":
                        this.WindowState = FormWindowState.Maximized;
                        break;
                    default:
                        this.WindowState = FormWindowState.Normal;
                        break;
                }
            }
            //check to see if the form is visible if not move it to the center of the primary screen mdail 8-15-19
            bool visible = Utilities.isWindowVisible(this.DesktopBounds);
            if (!visible)
            {
                Utilities.centerFormPrimary(this);
            }
        }
    }
}
