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
    internal enum UnitHierarchyTreeLevel
    {
        Battalion,
        Unit,
        Platoon,
        SquadSection,
        Soldier
    }

    internal struct SoldierReportTag
    {
        internal UnitHierarchyTreeLevel levelID;
        internal int id;
        internal int designatorid;
    }

    public partial class ReportsTab : UserControl
    {
        List<int> filteredUnitIDs;
        List<int> filteredPlatoonIDs;
        List<int> filteredSquadSectionIDs;
        WebBrowser webBrowser = new WebBrowser();
        DataTable soldiersTable;

        SoldierReportFiltersDialog soldierReportFiltersDialog;

        DataSet unitsDataSet;
        DataTable battalionsTable;
        DataTable unitDesignatorsTable;
        DataTable unitsTable;
        DataTable platoonsTable;
        DataTable squadsectionTable;

        DataTable soldierStatuses;
        DataRow[] filteredSoldiers;

        bool displayUnitHierarchy;

        string soldierFilters;
        string soldierIDFilters;

        string documentFilters;
        SortedDictionary<string, string> filters = new SortedDictionary<string, string>();

        DocumentReportFilter currentFilter;
        Pen splitterPen;

        public ReportsTab()
        {

        }


        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                Logger.Trace("ReportsTab - point 1");

                base.OnLoad(e);

                DoubleBuffered = true;

                Logger.Trace("ReportsTab - point 2");

                InitializeComponent();

                Logger.Trace("ReportsTab - point 3");

                SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170));
                splitterPen = new Pen(brush, 1);
                soldierIDFilters = "";
                soldierFilters = "(deleted = 0) and (soldierid > -1)";
                documentFilters = "(deleted = 0)";

                displayUnitHierarchy = true;

                soldierReportFiltersDialog = new SoldierReportFiltersDialog();

                Logger.Trace("ReportsTab - point 4");

                RefreshTables();

                Logger.Trace("ReportsTab - point 5");

                /*
                soldiersTable = SoldierModel.FormattedSoldiersTableCopy;
                soldierStatuses = SoldierModel.SoldiersStatusTable;
                unitsDataSet = UnitHierarchyModel.GetAllUnitInfo();

                battalionsTable = unitsDataSet.Tables["battalions"].Copy();
                unitsTable = unitsDataSet.Tables["units"].Copy();
                unitDesignatorsTable = unitsDataSet.Tables["unitdesignators"].Copy();
                platoonsTable = unitsDataSet.Tables["platoons"].Copy();
                squadsectionTable = unitsDataSet.Tables["squadsections"].Copy();
                */

                //RefreshSoldierTreeView();
                foreach (TreeNode node in soldiersTreeView.Nodes)
                    node.Checked = true;

                Logger.Trace("ReportsTab - point 6");

                RefreshDocumentsReportTable();
                Logger.Trace("ReportsTab - point 7");
                filteredUnitIDs = new List<int>();
                filteredPlatoonIDs = new List<int>();
                filteredSquadSectionIDs = new List<int>();

                SoldierModel.SoldierModelRefreshed = OnSoldierModelRefreshed;
                DocumentModel.DocumentModelRefreshed = OnDocumentModelRefreshed;
                UnitHierarchyModel.UnitHierarchyModelUpdated = OnUnitHierarchyModelRefreshed;
                Logger.Trace("ReportsTab - point 8");
                currentFilter = new DocumentReportFilter();
                Logger.Trace("ReportsTab - point 9");
                UpdateFilterCombobox();
                Logger.Trace("ReportsTab - point 10");
                currentFilter = (DocumentReportFilter)filterComboBox.SelectedItem;
                Logger.Trace("ReportsTab - point 11");
                RefreshBasicMode();
                Logger.Trace("ReportsTab - point 12");
            }
        }


        private void OnSoldierModelRefreshed()
        {
            RefreshTables();
        }

        private void OnDocumentModelRefreshed()
        {
            RefreshTables();
        }

        private void OnUnitHierarchyModelRefreshed()
        {
            RefreshTables();
        }


        internal void RefreshTables()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    PeformTableUpdate();
                });
            }
            else
                PeformTableUpdate();
        }


        private void PeformTableUpdate()
        {
            soldiersTable = SoldierModel.FormattedSoldiersTableCopy;
            soldierStatuses = SoldierModel.SoldiersStatusTable.Copy();
            unitsDataSet = UnitHierarchyModel.GetAllUnitInfo().Copy();

            battalionsTable = unitsDataSet.Tables["battalions"].Copy();
            unitsTable = unitsDataSet.Tables["units"].Copy();
            unitDesignatorsTable = unitsDataSet.Tables["unitdesignators"].Copy();
            platoonsTable = unitsDataSet.Tables["platoons"].Copy();
            squadsectionTable = unitsDataSet.Tables["squadsections"].Copy();

            RefreshSoldierTreeView();
            RefreshDocumentsReportTable();
        }

        private void RefreshDocumentsReportTable()
        {
            RefreshDocumentDataGridViewFilter();
        }


        private void RefreshSoldierTreeView()
        {
            soldiersTreeView.BeginUpdate();

            if (disableDocumentFiltersCheckBox.Checked)
                filteredSoldiers = soldiersTable.Select("soldierid > -1 and deleted = 0");
            else
                filteredSoldiers = soldiersTable.Select(soldierFilters);

            int numNodes = soldiersTreeView.Nodes.Count;
            for (int i = 0; i < numNodes; i++)
                soldiersTreeView.Nodes.RemoveAt(0);

            if (displayUnitHierarchy)
                BuildUnitBasedSoldiersTreeView();
            else
                BuildUnitlessSoldiersTreeView();

            soldiersTreeView.ExpandAll();

            foreach (TreeNode node in soldiersTreeView.Nodes)
                node.Checked = true;

            if (soldiersTreeView.Nodes.Count > 0)
                soldiersTreeView.Nodes[0].EnsureVisible();

            soldiersTreeView.EndUpdate();
        }


        private void BuildUnitBasedSoldiersTreeView()
        {
            foreach (DataRow battalionRow in battalionsTable.Rows)
            {
                int battalionID = Convert.ToInt32(battalionRow["battalionid"]);
                DataRow[] soldiersInBattalion 
                    = filteredSoldiers.Where(row => Convert.ToInt32(row["battalionid"]) == battalionID).ToArray();

                if (soldiersInBattalion.Length == 0)
                    continue;

                string battalionName = battalionRow["battalionname"].ToString();
                TreeNode battalionNode = GetUnitsForBattalion(battalionID, soldiersInBattalion);
                battalionNode.Text = battalionName;
                soldiersTreeView.Nodes.Add(battalionNode);
            }
        }


        private void BuildUnitlessSoldiersTreeView()
        {
            foreach (DataRow soldier in filteredSoldiers)
            {
                TreeNode soldierNode = new TreeNode();
                SoldierReportTag tag = new SoldierReportTag();
                tag.id = Convert.ToInt32(soldier["soldierid"]);
                tag.levelID = UnitHierarchyTreeLevel.Soldier;

                soldierNode.Text = soldier["lastname"] + ", " + soldier["firstname"];
                soldierNode.Tag = tag;

                soldiersTreeView.Nodes.Add(soldierNode);
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
                RefreshDocumentDataGridViewFilter();
                soldiersTreeView.Enabled = true;
            }
        }


        private void soldiersTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            
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
                        n = n.NextNode;
                    }
                }
            }
            catch (System.Exception)
            {
                // this catches when the soldier nodes try to catch their children
            }


        }



        private void RefreshDocumentDataGridViewFilter()
        {
            RefreshSoldierIDFilters();

            if (disableDocumentFiltersCheckBox.Checked == true)
            {
                //cqpDocumentsDataGridView1.Filter = "soldierid > -1 and soldiers.deleted = 0 and (deleted = 0)";
                if(soldierIDFilters == "")
                    cqpDocumentsDataGridView1.Filter = "soldierid < -1 and (deleted = 0)";
                else
                    cqpDocumentsDataGridView1.Filter = "(deleted = 0)";

                return;
            }

            StringBuilder filters = new StringBuilder();
            filters.Append(soldierIDFilters == "" ? "soldierid < -1" : (soldierIDFilters));
            filters.Append((documentFilters.Length > 0) ? " and " : "");
            filters.Append(documentFilters);

            string filterss = filters.ToString();

            cqpDocumentsDataGridView1.Filter = filters.ToString();
        }



        void RefreshSoldierIDFilters()
        {
            List<int> soldierIDs = new List<int>();
            if (soldiersTreeView.Nodes.Count > 0 )
                soldierIDs = GetSelectedSoldierIDs(soldiersTreeView.Nodes[0]);

            soldierIDFilters = "( ";
            for (int i = 0; i < soldierIDs.Count; i++)
            {
                soldierIDFilters += "soldierid = " + soldierIDs[i]
                    + (i < soldierIDs.Count - 1 ? " or " : " ");
            }
            soldierIDFilters += " )";

            if (soldierIDFilters == "(  )")
            {
                soldierIDFilters = "";
            }
        }


        List<int> GetSelectedSoldierIDs(TreeNode node)
        {
            List<int> selectedSoldierIDs = new List<int>();

            while (node != null)
            {
                SoldierReportTag tag = (SoldierReportTag)node.Tag;

                if (tag.levelID == UnitHierarchyTreeLevel.Soldier)
                {
                    if(node.Checked == true)
                        selectedSoldierIDs.Add(tag.id);
                }
                else
                {
                    TreeNode childNode = node.Nodes[0];
                    selectedSoldierIDs.AddRange( GetSelectedSoldierIDs(childNode) );
                }

                node = node.NextNode;
            }
            
            return selectedSoldierIDs;
        }


        private TreeNode GetUnitsForBattalion(int battalionID, DataRow []soldiersInBattalion)
        {
            TreeNode battalionNode = new TreeNode();

            SoldierReportTag tag = new SoldierReportTag();
            tag.id = battalionID;
            tag.levelID = UnitHierarchyTreeLevel.Battalion;
            battalionNode.Tag = tag;

            foreach (DataRow unitRow in unitsTable.Rows)
            {
                int unitID = Convert.ToInt32(unitRow["unitid"]);
                string unitName = unitRow["unitname"].ToString();

                //DataRow[] unitDesignatorIDs
                //    = unitsDataSet.Tables["unithierarchies"].Select("battalionid = " + battalionID
                //                                                    + " and unitid = " + unitID);
                
                //foreach (DataRow unitDesignatorRow in unitDesignatorIDs)
                foreach (DataRow unitDesignatorRow in unitDesignatorsTable.Rows)
                {
                    int unitDesignatorID = Convert.ToInt32(unitDesignatorRow["unitdesignatorid"]);
                    //string unitDesignatorName = unitDesignatorRow["unitdesignatorname"].ToString();
                    string unitDesignatorName = UnitHierarchyModel.GetUnitDesignatorName(unitDesignatorID);

                    DataRow[] soldiersInUnit
                        = soldiersInBattalion.Where(row => Convert.ToInt32(row["unitid"]) == unitID
                                                    && Convert.ToInt32(row["unitdesignatorid"]) == unitDesignatorID).ToArray();

                    if (soldiersInUnit.Length == 0)
                        continue;

                    TreeNode unitNode = GetPlatoonsForUnit(soldiersInUnit);
                    unitNode.Text = unitDesignatorName + " " + unitName;

                    SoldierReportTag unitTag = new SoldierReportTag();
                    unitTag.id = unitID;
                    unitTag.designatorid = unitDesignatorID;
                    unitTag.levelID = UnitHierarchyTreeLevel.Unit;

                    unitNode.Tag = unitTag;

                    battalionNode.Nodes.Add(unitNode);
                }
            }

            return battalionNode;
        }


        private TreeNode GetPlatoonsForUnit(DataRow []soldiersInUnit)
        {
            TreeNode unitNode = new TreeNode();

            foreach (DataRow platoonRow in platoonsTable.Rows)
            {
                int platoonID = Convert.ToInt32(platoonRow["platoonid"]);
                string platoonName = platoonRow["platoonname"].ToString();

                DataRow[] soldiersInPlatoon 
                    = soldiersInUnit.Where(row => Convert.ToInt32(row["platoonid"]) == platoonID).ToArray();

                if (soldiersInPlatoon.Length == 0)
                    continue;

                TreeNode platoonNode = GetSquadsForPlatoon(soldiersInPlatoon);
                platoonNode.Text = "Platoon " + platoonName;

                SoldierReportTag tag = new SoldierReportTag();
                tag.id = platoonID;
                tag.levelID = UnitHierarchyTreeLevel.Platoon;

                platoonNode.Tag = tag;

                unitNode.Nodes.Add(platoonNode);
            }

            return unitNode;
        }


        private TreeNode GetSquadsForPlatoon(DataRow[] soldiersInPlatoon)
        {
            TreeNode platoonNode = new TreeNode();

            foreach (DataRow squadRow in squadsectionTable.Rows)
            {
                int squadID = Convert.ToInt32(squadRow["squadsectionid"]);
                string squadName = squadRow["squadsectionname"].ToString();

                DataRow[] soldiersInSquad
                    = soldiersInPlatoon.Where(row => Convert.ToInt32(row["squadsectionid"]) == squadID).ToArray();

                if (soldiersInSquad.Length == 0)
                    continue;

                TreeNode squadNode = GetSoldiersForSquad(soldiersInSquad);
                squadNode.Text = "Squad " + squadName;

                SoldierReportTag tag = new SoldierReportTag();
                tag.id = squadID;
                tag.levelID = UnitHierarchyTreeLevel.SquadSection;

                squadNode.Tag = tag;

                platoonNode.Nodes.Add(squadNode);
            }

            return platoonNode;
        }

        private TreeNode GetSoldiersForSquad(DataRow[] soldiersInSquad)
        {
            TreeNode squadNode = new TreeNode();

            foreach (DataRow soldier in soldiersInSquad)
            {
                string soldierName = soldier["lastname"].ToString() + ", " + soldier["firstname"].ToString();
                int soldierID = Convert.ToInt32(soldier["soldierid"]);

                TreeNode soldierNode = new TreeNode(soldierName);

                SoldierReportTag soldierTag = new SoldierReportTag();
                soldierTag.levelID = UnitHierarchyTreeLevel.Soldier;
                soldierTag.id = soldierID;
                soldierNode.Tag = soldierTag;

                squadNode.Nodes.Add(soldierNode);
            }

            return squadNode;
        }


        private void soldierFiltersButton_Click(object sender, EventArgs e)
        {
            //soldierReportFiltersDialog = new SoldierReportFiltersDialog();
            DialogResult result = soldierReportFiltersDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string unitFilters = soldierReportFiltersDialog.UnitIDFilters;
                string rankingFilters = soldierReportFiltersDialog.RankingIDFilters;
                string statusFilters = soldierReportFiltersDialog.StatusIDFilters;
                string otherStatusFilters = soldierReportFiltersDialog.OtherStatusFilters;
                string soldierIDFilters = SoldiersWithMatchingStatusIDs(statusFilters, otherStatusFilters);

                soldierFilters = unitFilters + (unitFilters == "" ? "" : " and ")
                    + rankingFilters + (rankingFilters == "" ? "" : " and " )
                   // + statusFilters + (statusFilters == "" ? "" : " and " )
                    + soldierIDFilters;

                if (!soldierFilters.EndsWith(" and ") && soldierFilters != "")
                    soldierFilters += " and ";

                soldierFilters += "(deleted = 0)";

                RefreshSoldierTreeView();
                RefreshDocumentDataGridViewFilter();
            }
        }


        private string SoldiersWithMatchingStatusIDs(string statusFilters, string otherStatusFilters)
        {
            if (statusFilters == "" && otherStatusFilters == "")
                return "";

            StringBuilder soldierIDsSB = new StringBuilder();
            soldierIDsSB.Append("( ");

            List<DataRow> soldiersWithStatuses = soldierStatuses.Select(statusFilters).ToList();
            //if (soldiersWithStatuses.Count == 0)
            //    return "(soldierid = -1)";

            //List<DataRow> soldiersWithOtherStatuses = soldiersTable.Select(otherStatusFilters).ToList();
            List<DataRow> soldiersWithOtherStatuses = new List<DataRow>();
            if (otherStatusFilters != "")
                soldiersWithOtherStatuses = SoldierModel.GetSoldierRows(otherStatusFilters).ToList();

            //soldiersWithStatuses.AddRange(soldiersWithStatuses);
            soldiersWithStatuses.AddRange(soldiersWithOtherStatuses);

            if (soldiersWithStatuses.Count == 0)
                return "(soldierid = -1)";

            var unique = (from row in soldiersWithStatuses
                          select row["soldierid"]).Distinct();
           
            foreach (var soldierID in unique) 
                soldierIDsSB.Append( "soldierid = " + soldierID + " or ");

            if (soldierIDsSB.Length == 2)
                return "";
            
            // if it doesn't end with "( " we know we added at least one
            // soldier id, remove the last " or ", append a ")", and return str
            soldierIDsSB.Remove(soldierIDsSB.Length-4, 4);
            soldierIDsSB.Append(")");

            string soldierIDs = soldierIDsSB.ToString();

            return soldierIDs;
        }


        private void displayUnitHierarchyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            displayUnitHierarchy = displayUnitHierarchyCheckbox.Checked;
            RefreshSoldierTreeView();
        }

        private void documentFiltersButton_Click(object sender, EventArgs e)
        {
            //DocumentReportFilter filter = (DocumentReportFilter)filterComboBox.SelectedItem;

            DocumentReportFilter filter = new DocumentReportFilter();
            DocumentReportFilter.DeepCopyReportFilterToCurrentFilter(filter, currentFilter);

            DialogResult result;
            DocumentsReportFiltersDialog documentsReportFiltersDialog = new DocumentsReportFiltersDialog();

            if (filter == null)
                result = documentsReportFiltersDialog.ShowDialog(this);
            else
                result = documentsReportFiltersDialog.ShowDialog(this, filter);

            if (result == DialogResult.Cancel)
                return;

            currentFilter.CounselingTemplateIDs = documentsReportFiltersDialog.CounselingIDs;
            currentFilter.DocumentTemplateIDs = documentsReportFiltersDialog.DocumentIDs;
            currentFilter.DocumentStatusIDs = documentsReportFiltersDialog.StatusIDs;
            
            LoadDocumentFilter();

            //documentFilters = documentsReportFiltersDialog.DocumentFilters;
            //RefreshDocumentFilters();
        }

        private void disableDocumentFiltersCheckBox_Click(object sender, EventArgs e)
        {
            RefreshSoldierTreeView();
            RefreshDocumentDataGridViewFilter();
        }

        private void saveFilterButton_Click(object sender, EventArgs e)
        {
            bool isNewFilter = !DocumentReportFilter.DocumentReportFilterExists( currentFilter.DocumentReportFilterID );

            if (isNewFilter)
            {
                DialogResult result = ConfirmOverwriteDocumentFilter();
                if (result == DialogResult.Cancel)
                    return;


            }

            currentFilter.Save();

            string message = "Filter " + currentFilter.DocumentReportFilterName + " saved successfully.";
            string caption = "Saved.";
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Information;
            CQPMessageBox.CQPMessageBoxButtons button = CQPMessageBox.CQPMessageBoxButtons.OK;
            CQPMessageBox.Show(message, caption, button, icon);

            UpdateFilterCombobox();



            //DocumentReportFilter reportFilter = new DocumentReportFilter();

            //int selectedIndex = filterComboBox.SelectedIndex;

            //if (selectedIndex != -1)
            //{
            //    DialogResult result = ConfirmOverwriteDocumentFilter();
            //    if (result == DialogResult.Cancel)
            //        return;

            //    reportFilter.DocumentReportFilterID = (int)filterComboBox.SelectedValue;
            //}
            //else
            //    reportFilter.DocumentReportFilterID = -1;

            //string reportFilterName = filterComboBox.Text;

            //reportFilter.DocumentReportFilterName = reportFilterName;
            //reportFilter.CounselingTemplateIDs = currentFilter.CounselingTemplateIDs;
            //reportFilter.DocumentTemplateIDs = currentFilter.DocumentTemplateIDs;
            //reportFilter.DocumentStatusIDs = currentFilter.DocumentStatusIDs;

            ////reportFilter.CounselingTemplateIDs = documentsReportFiltersDialog.CounselingIDs;
            ////reportFilter.DocumentTemplateIDs = documentsReportFiltersDialog.DocumentIDs;
            ////reportFilter.DocumentStatusIDs = documentsReportFiltersDialog.StatusIDs;

            //reportFilter.Save();

            //UpdateFilterCombobox();
        }


        private void UpdateFilterCombobox()
        {
            int selectedIndex = filterComboBox.SelectedIndex;

            List<DocumentReportFilter> filters = DocumentReportFilter.GetAllDocumentReportFilters();

            //bindingSource1.DataSource = filters;

            filterComboBox.DataSource = filters;
            filterComboBox.DisplayMember = "DocumentReportFilterName";
            filterComboBox.ValueMember = "DocumentReportFilterID";

            if (selectedIndex == -1)
                filterComboBox.SelectedIndex = filterComboBox.Items.Count - 1;
            else if (selectedIndex >= filterComboBox.Items.Count)
                filterComboBox.SelectedIndex = filterComboBox.Items.Count - 1;
            else
                filterComboBox.SelectedIndex = selectedIndex;
        }


        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // - The selected index was changed
            // - Copy the selected item to "Filter"
            // - Copy this filter to "CurrentFilter" so we can make changes to "currentfilter" via the 
            //   without filters dialogs without altering the documentreportfilter in the 
            //   combobox (it's a reference remember.)
            // - Later on save, save using "CurrentFilter", not anything in the combobox
            //   - Don't even look at it!  Just poll the DB for if exists(current filter.ID)
            
                DocumentReportFilter selectedFilter = (DocumentReportFilter)filterComboBox.SelectedItem;
                currentFilter = new DocumentReportFilter();
                DocumentReportFilter.DeepCopyReportFilterToCurrentFilter(currentFilter, selectedFilter);

                bool enable = selectedFilter.Saveable;

                this.BeginInvoke((MethodInvoker) delegate
                {
                    deleteFilterButton.Enabled = enable;
                    saveFilterButton.Enabled = enable;
                });

                LoadDocumentFilter();
        }


        private void filterComboBox_TextChanged(object sender, EventArgs e)
        {
            List<DocumentReportFilter> filters = filterComboBox.Items.Cast<DocumentReportFilter>().ToList();
            filters = filters.Where(filter => filter.DocumentReportFilterName == filterComboBox.Text).ToList();
            
            // there is no filter with this name
            if (filters.Count() == 0)
            {
                // just update the name, update the ID to let it know this is an unID'd
                // filter, but keep all the same properties!
                currentFilter.DocumentReportFilterID = -1;
                currentFilter.DocumentReportFilterName = filterComboBox.Text;
                return;
            }
            else
            {
                // we found a match
                // update the selected item to this one
                DocumentReportFilter filter = filters.First();
                filterComboBox.SelectedItem = filter;
            }
            
        }


        private void LoadDocumentFilter()
        {
            //if (filterComboBox.SelectedItem == null)
            //    return;

            if (currentFilter == null)
                return;

            //DocumentReportFilter reportFilter = (DocumentReportFilter)filterComboBox.SelectedItem;
            StringBuilder sb = new StringBuilder();

            string nameFilters 
                = GetDocumentNameFilters(currentFilter.CounselingTemplateIDs, currentFilter.DocumentTemplateIDs);

            string statusFilters
                = GetDocumentStatusFilters(currentFilter.DocumentStatusIDs);

            string categoryFilters
                = GetDocumentCategoryFilters(currentFilter.DocumentCategories);

            sb.Append(nameFilters);

            if (nameFilters.Length != 0)
                sb.Append(" and ");

            sb.Append(statusFilters);

            if (statusFilters.Length != 0)
                sb.Append(" and ");

            sb.Append(categoryFilters);

            if (categoryFilters.Length != 0)
                sb.Append(" and ");

            sb.Append("(deleted = 0)");

            if (sb.ToString().EndsWith(" and "))
                sb.Remove(sb.Length - 5, 5);

            documentFilters = sb.ToString();
            RefreshDocumentDataGridViewFilter();
        }

        private string GetDocumentCategoryFilters(List<int> categoryIDs)
        {
            StringBuilder categoryStringBuilder = new StringBuilder();
            categoryStringBuilder.Append("(");

            foreach (int categoryID in categoryIDs)
                categoryStringBuilder.Append("documentcategoryid = " + categoryID + " or ");

            if (categoryStringBuilder.ToString().EndsWith(" or "))
                categoryStringBuilder.Remove(categoryStringBuilder.Length - 4, 4);

            if (categoryStringBuilder.ToString().EndsWith("("))
                categoryStringBuilder.Remove(categoryStringBuilder.Length - 1, 1);
            else
                categoryStringBuilder.Append(")");

            return categoryStringBuilder.ToString();
        }


        private string GetDocumentNameFilters(List<int> counselingNames, List<int> documentationNames)
        {
            StringBuilder documentFilterStringBuilder = new StringBuilder();
            documentFilterStringBuilder.Append("(");

            foreach (int documentNameID in counselingNames)
                documentFilterStringBuilder.Append("documentnameid = " + documentNameID + " or ");

            foreach (int documentNameID in documentationNames)
                documentFilterStringBuilder.Append("documentnameid = " + documentNameID + " or ");

            if (documentFilterStringBuilder.ToString().EndsWith(" or "))
                documentFilterStringBuilder.Remove(documentFilterStringBuilder.Length - 4, 4);

            if (documentFilterStringBuilder.ToString().EndsWith("("))
                documentFilterStringBuilder.Remove(documentFilterStringBuilder.Length - 1, 1);
            else
                documentFilterStringBuilder.Append(")");

            return documentFilterStringBuilder.ToString();
        }


        private string GetDocumentStatusFilters(List<int> documentStatusIDs)
        {
            StringBuilder documentStatusStringBuilder = new StringBuilder();
            documentStatusStringBuilder.Append("(");

            foreach (int documentStatusID in documentStatusIDs)
                documentStatusStringBuilder.Append("statusid = " + documentStatusID + " or ");

            if (documentStatusStringBuilder.ToString().EndsWith(" or "))
                documentStatusStringBuilder.Remove(documentStatusStringBuilder.Length - 4, 4);

            if (documentStatusStringBuilder.ToString().EndsWith("("))
                documentStatusStringBuilder.Remove(documentStatusStringBuilder.Length - 1, 1);
            else
                documentStatusStringBuilder.Append(")");

            return documentStatusStringBuilder.ToString();
        }


        private void deleteFilterButton_Click(object sender, EventArgs e)
        {
            if(filterComboBox.SelectedItem == null)
                return;

            DocumentReportFilter documentReportFilter = (DocumentReportFilter) filterComboBox.SelectedItem;

            DialogResult result = ConfirmDeleteDocumentFilter();
            if (result == DialogResult.Cancel)
                return;

            documentReportFilter.Delete();

            CQPMessageBox.Show("Filter deleted.", "", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);

            UpdateFilterCombobox();
            RefreshDocumentDataGridViewFilter();
        }


        DialogResult ConfirmOverwriteDocumentFilter()
        {
            DocumentReportFilter documentReportFilter = (DocumentReportFilter)filterComboBox.SelectedItem;

            string message = "Are you sure you want to overwrite filter \""
                                + documentReportFilter.DocumentReportFilterName + "\"?";
            string caption = "Ovewrite filter?";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OKCancel;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Question;

            List<string> buttonText = new List<string> { "Overwrite filter", "Cancel" };

            DialogResult result = CQPMessageBox.Show(message, caption, buttons, buttonText, icon);
            return result;
        }


        DialogResult ConfirmDeleteDocumentFilter()
        {
            DocumentReportFilter documentReportFilter = (DocumentReportFilter)filterComboBox.SelectedItem;

            string message = "Are you sure you want to delete filter \""
                                + documentReportFilter.DocumentReportFilterName + "\"?";
            string caption = "Delete filter?";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OKCancel;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Question;

            List<string> buttonText = new List<string> { "Delete Filter", "Cancel" };

            DialogResult result = CQPMessageBox.Show(message, caption, buttons, buttonText, icon);
            return result;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            //string html = Utilities.DataGridViewToHTML(cqpDocumentsDataGridView1.DataGridView);

            ////string filename = @"c:\__home\development\projects\CounselQuickPlatinum\trunk\Project\bin\Debug\Continuation_of_Counseling.html";//System.IO.Path.GetTempFileName();
            //string filename = System.IO.Path.GetTempFileName();

            //System.IO.StreamWriter htmlFile = new System.IO.StreamWriter(filename);
            //htmlFile.Write(html);
            //htmlFile.Close();

            string html = Utilities.DataGridViewToHTML(cqpDocumentsDataGridView1.DataGridView);

            System.IO.FileInfo file = new System.IO.FileInfo(System.IO.Path.GetTempFileName());
            System.IO.StreamWriter htmlFile = new System.IO.StreamWriter(file.FullName);
            htmlFile.Write(html);
            htmlFile.Close();

            //FileUtils.BlockingFileCopy(new System.IO.FileInfo(file.FullName), new System.IO.FileInfo("C:\\Users\\cdail\\Desktop\\test.html"));

            System.IO.DirectoryInfo directory = file.Directory;
            string reportHTMLFilename = directory.FullName + "\\report.html";

            if (System.IO.File.Exists(reportHTMLFilename))
                System.IO.File.Delete(reportHTMLFilename);

            System.IO.File.Move(file.FullName, reportHTMLFilename);

            PrintHTMLForm form = new PrintHTMLForm();
            form.URL = reportHTMLFilename;
            form.ShowDialog();
        }

        void f_VisibleChanged(object sender, EventArgs e)
        {
            webBrowser.ShowPrintDialog();
        }


        void f_Load(object sender, EventArgs e)
        {
            webBrowser.Show();
            webBrowser.Visible = true;

            
            
        }



        private void advancedModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (advancedModeCheckbox.Checked)
            {
                advancedModeCheckbox.Image = CounselQuickPlatinum.Properties.Resources.slide_right;
                RefreshAdvancedMode();
            }
            else
            {
                advancedModeCheckbox.Image = CounselQuickPlatinum.Properties.Resources.slide_left;
                RefreshBasicMode();
            }
        }


        private void RefreshAdvancedMode()
        {
            //filterComboBox.SelectedIndex = 0;
            //currentFilter = (DocumentReportFilter)filterComboBox.SelectedItem;

            basicButtonsTable.Hide();
            advancedButtonTable.Show();
        }

        private void RefreshBasicMode()
        {
            basicButtonsTable.Show();
            advancedButtonTable.Hide();
        }

        private void allDocumentsFilterButton_Click(object sender, EventArgs e)
        {
            DocumentReportFilter filter = new DocumentReportFilter();
            filter.DocumentReportFilterID = 0;
            filter.CounselingTemplateIDs = new List<int>();
            filter.DocumentTemplateIDs = new List<int>();
            filter.DocumentStatusIDs = new List<int>();

            currentFilter = filter;
            LoadDocumentFilter();
        }

        private void cqpGraphicsIcon2_Click(object sender, EventArgs e)
        {
            DocumentReportFilter filter = new DocumentReportFilter();
            filter.DocumentReportFilterID = 0;
            filter.CounselingTemplateIDs = new List<int>();
            filter.DocumentTemplateIDs = new List<int>();
            filter.DocumentStatusIDs = new List<int>();
            filter.DocumentStatusIDs.Add(4);
            filter.DocumentStatusIDs.Add(5);

            currentFilter = filter;
            LoadDocumentFilter();
        }

        private void verbalCounselingsButton_Click(object sender, EventArgs e)
        {
            DocumentReportFilter filter = new DocumentReportFilter();
            filter.DocumentReportFilterID = 0;
            filter.CounselingTemplateIDs = new List<int>();
            filter.DocumentTemplateIDs = new List<int>();
            filter.DocumentStatusIDs = new List<int>();
            filter.DocumentStatusIDs.Add(1);
            filter.DocumentCategories.Add(1);

            currentFilter = filter;
            LoadDocumentFilter();
        }


        private void cqpGraphicsButton1_Click(object sender, EventArgs e)
        {
            DocumentReportFilter filter = new DocumentReportFilter();
            filter.DocumentReportFilterID = 0;
            filter.CounselingTemplateIDs = new List<int>();
            filter.DocumentTemplateIDs = new List<int>();
            filter.DocumentStatusIDs = new List<int>();
            filter.DocumentStatusIDs.Add(2);
            filter.DocumentCategories.Add(1);

            currentFilter = filter;
            LoadDocumentFilter();
        }

        private void cqpGraphicsButton2_Click(object sender, EventArgs e)
        {
            DocumentReportFilter filter = new DocumentReportFilter();
            filter.DocumentReportFilterID = 0;
            filter.CounselingTemplateIDs = new List<int>();
            filter.DocumentTemplateIDs = new List<int>();
            filter.DocumentStatusIDs = new List<int>();
            filter.DocumentStatusIDs.Add(3);
            filter.DocumentStatusIDs.Add(5);
            //filter.DocumentCategories.Add(1);

            currentFilter = filter;
            LoadDocumentFilter();
        }



        private void advancedModeCheckbox_MouseDown(object sender, MouseEventArgs e)
        {
            advancedModeCheckbox.Image = CounselQuickPlatinum.Properties.Resources.slide_onclick;
        }

        private void addNewPresetButton_Click(object sender, EventArgs e)
        {
            NewDocumentPresetDialog dialog = new NewDocumentPresetDialog(currentFilter);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                UpdateFilterCombobox();
                filterComboBox.SelectedIndex = filterComboBox.Items.Count - 1;
            }
        }

        private void exportToPDFButton_Click(object sender, EventArgs e)
        {
            string html = Utilities.DataGridViewToHTML(cqpDocumentsDataGridView1.DataGridView);

            //string filename = @"c:\__home\development\projects\CounselQuickPlatinum\trunk\Project\bin\Debug\Continuation_of_Counseling.html";//System.IO.Path.GetTempFileName();
            System.IO.FileInfo file = new System.IO.FileInfo(System.IO.Path.GetTempFileName());
            System.IO.StreamWriter htmlFile = new System.IO.StreamWriter(file.FullName);
            htmlFile.Write(html);
            htmlFile.Close();

            System.IO.DirectoryInfo directory = file.Directory;
            string reportHTMLFilename = directory.FullName + "\\report.html";

            if (System.IO.File.Exists(reportHTMLFilename))
                System.IO.File.Delete(reportHTMLFilename);

            System.IO.File.Move(file.FullName, reportHTMLFilename);


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.SupportMultiDottedExtensions = true;
            saveFileDialog.RestoreDirectory = true;

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            try
            {
                System.IO.FileInfo info = new System.IO.FileInfo(".\\htmltopdf\\htmltopdf.exe");

                System.Diagnostics.ProcessStartInfo exportStartInfo = new System.Diagnostics.ProcessStartInfo();
                exportStartInfo.Arguments = "\"" + reportHTMLFilename + "\" \"" + saveFileDialog.FileName + "\"";
                exportStartInfo.FileName = info.FullName;

                Logger.Trace("ReportHTMLFilename: " + reportHTMLFilename);
                Logger.Trace("Filename: " + info.FullName);

                exportStartInfo.CreateNoWindow = true;
                exportStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

                System.Diagnostics.Process exportProcess = new System.Diagnostics.Process();
                exportProcess.StartInfo = exportStartInfo;
                exportProcess.Start();

                while (!exportProcess.HasExited)
                    ;
            }
            catch (Exception)
            {
                string errorMessage = "An unknown error occurred exporting the document.";
                string errorCaption = "Error.";
                CQPMessageBox.CQPMessageBoxIcon errorIcon = CQPMessageBox.CQPMessageBoxIcon.Error;
                CQPMessageBox.CQPMessageBoxButtons errorButtons = CQPMessageBox.CQPMessageBoxButtons.OK;

                CQPMessageBox.Show(errorMessage, errorCaption, errorButtons, errorIcon);
                return;
            }

            System.Diagnostics.Process.Start(saveFileDialog.FileName);
        }
        

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            float splitterDistance = (float)(splitContainer1.SplitterDistance + (splitContainer1.SplitterWidth / 2.0));
            int splitterHeight = splitContainer1.Height;

            int yOffset = 15;

            PointF p1 = new PointF(splitterDistance, yOffset);
            PointF p2 = new PointF(splitterDistance, splitterHeight - yOffset);

            e.Graphics.DrawLine(splitterPen, p1, p2);
        }

        private void disableDocumentFiltersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = !disableDocumentFiltersCheckBox.Checked;

            soldierFiltersButton.Enabled = enabled;
            documentFiltersButton.Enabled = enabled;
        }

    }
}
