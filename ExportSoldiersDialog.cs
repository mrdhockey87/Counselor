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
    public partial class ExportSoldiersDialog : Form
    {
        DataTable battalionsTable;
        DataTable unitsTable;
        DataTable unitDesignatorsTable;
        DataTable platoonsTable;
        DataTable squadsectionTable;
        private bool filepathValid;
        private bool selectedSoldiersValid;

        public ExportSoldiersDialog()
        {
            InitializeComponent();

            Load += new EventHandler(ExportSoldiersDialog_Load);
            //Added to resotre the forms saved location from last run mdail 8-19-19
            PutAtSavedLocation();
        }

        void ExportSoldiersDialog_Load(object sender, EventArgs e)
        {
            DataSet unitsDataSet = UnitHierarchyModel.GetAllUnitInfo();
            battalionsTable = unitsDataSet.Tables["battalions"].Copy();
            unitsTable = unitsDataSet.Tables["units"].Copy();
            unitDesignatorsTable = unitsDataSet.Tables["unitdesignators"].Copy();
            platoonsTable = unitsDataSet.Tables["platoons"].Copy();
            squadsectionTable = unitsDataSet.Tables["squadsections"].Copy();
            filepathValid = false;
            selectedSoldiersValid = false;
            BuildUnitBasedSoldiersTreeView();
        }

        private void cqpGrayRectangleButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Counsel Quick Export Files (*.cqpx)|*.cqpx";
            dialog.RestoreDirectory = true;
            
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            filepathTextbox.Text = dialog.FileName;
        }

        private void BuildUnitBasedSoldiersTreeView()
        {
            if(soldiersTreeView.Nodes.Count != 0)
            {
                soldiersTreeView.Nodes.Clear();
            }
            foreach (DataRow battalionRow in battalionsTable.Rows)
            {
                int battalionID = Convert.ToInt32(battalionRow["battalionid"]);
                DataRow[] rows = SoldierModel.GetSoldierRows("deleted = 0");

                DataRow[] soldiersInBattalion
                    = rows.Where(row => Convert.ToInt32(row["battalionid"]) == battalionID).ToArray();

                if (soldiersInBattalion.Length == 0)
                    continue;

                string battalionName = battalionRow["battalionname"].ToString();
                TreeNode battalionNode = GetUnitsForBattalion(battalionID, soldiersInBattalion);
                battalionNode.Text = battalionName;
                battalionNode.ExpandAll();

                soldiersTreeView.Nodes.Add(battalionNode);
            }
        }

        private TreeNode GetUnitsForBattalion(int battalionID, DataRow[] soldiersInBattalion)
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

        private TreeNode GetPlatoonsForUnit(DataRow[] soldiersInUnit)
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

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in soldiersTreeView.Nodes)
                node.Checked = true;
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in soldiersTreeView.Nodes)
                node.Checked = false;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            CheckAllFieldsValid();
            if (!filepathValid)
            {
                errorLabel.Text = "No output file specified.";
                errorLabel.Visible = true;

                DialogResult = DialogResult.None;
                return;
            }

            if (!selectedSoldiersValid)
            {
                errorLabel.Text = "No Soldiers selected for export.";
                errorLabel.Visible = true;

                DialogResult = DialogResult.None;
                return;
            }

            errorLabel.Visible = false;


            List<int> soldierIDs = GetSelectedSoldierIDs(soldiersTreeView.Nodes[0]);

            DataExporter exporter = new DataExporter();
            foreach (int soldierID in soldierIDs)
            {
                List<Document> documents = DocumentModel.GetAllDocumentsForSoldier(soldierID);
                List<NoteInterface> notes = NotesModel.GetAllNotesForSoldier(soldierID);

                foreach (Document doc in documents)
                    exporter.AddDocument(doc);
                foreach (NoteInterface note in notes)
                    exporter.AddNote(note);

                Soldier soldier = new Soldier(soldierID);
                exporter.AddSoldier(soldier);
            }

            System.IO.FileInfo fileinfo = new System.IO.FileInfo(filepathTextbox.Text);
            exporter.WriteExportFile(fileinfo);

            string message = "Export completed!";
            string caption = "Complete";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.OK;
            CQPMessageBox.CQPMessageBoxIcon icons = CQPMessageBox.CQPMessageBoxIcon.Information;

            CQPMessageBox.ShowDialog(message, caption, buttons, icons);
        }

        private void CheckAllFieldsValid()
        {
            CheckFilepathValid();
            CheckSelectedSoldiersValid();
        }

        private void CheckFilepathValid()
        {
            if (filepathTextbox.Text == "")
            {
                filepathLabel.Text = "* Output File";
                filepathValid = false;
            }
            else
            {
                filepathLabel.Text = "Output File";
                filepathValid = true;
            }
        }

        private void CheckSelectedSoldiersValid()
        {
            if (soldiersTreeView.Nodes.Count == 0)
            {
                selectedSoldiersValid = false;
                return;
            }

            List<int> soldierIDs = GetSelectedSoldierIDs(soldiersTreeView.Nodes[0]);
            if (soldierIDs.Count == 0)
                selectedSoldiersValid = false;
            else
                selectedSoldiersValid = true;
        }

        List<int> GetSelectedSoldierIDs(TreeNode node)
        {
            List<int> selectedSoldierIDs = new List<int>();

            while (node != null)
            {
                SoldierReportTag tag = (SoldierReportTag)node.Tag;

                if (tag.levelID == UnitHierarchyTreeLevel.Soldier)
                {
                    if (node.Checked == true)
                        selectedSoldierIDs.Add(tag.id);
                }
                else
                {
                    TreeNode childNode = node.Nodes[0];
                    selectedSoldierIDs.AddRange(GetSelectedSoldierIDs(childNode));
                }

                node = node.NextNode;
            }

            return selectedSoldierIDs;
        }

        private void ExportSoldiersDialog_FormClosing(object sender, FormClosingEventArgs e)
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
                    Properties.Settings.Default.ExportSoldiersDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), this.WindowState);
                    break;
                default:
                    Properties.Settings.Default.ExportSoldiersDialogSavedWindowState = Enum.GetName(typeof(FormWindowState), FormWindowState.Normal);
                    break;
            }
            System.Drawing.Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            Properties.Settings.Default.ExportSoldiersDialogSize = bounds.Size;
            Properties.Settings.Default.ExportSoldiersDialogLocation = bounds.Location;
            Properties.Settings.Default.ExportSoldiersDialogHeight = bounds.Height;
            Properties.Settings.Default.ExportSoldiersDialogWidth = bounds.Width;
            // persist location ,size and window state of the form on the desktop Added to save the winodws position with the Windows Settings class added mdail 8-13-19
            Properties.Settings.Default.Save();
        }
        //put the form to its saved location mdail 8-16-19
        private void PutAtSavedLocation()
        {
            //the line below centers the form on my screen, the line below that is the code that should center it mdail 8-13-19
            // restore location and size of the form on the desktop mdail 8-13-19
            String winState = Properties.Settings.Default.ExportSoldiersDialogSavedWindowState;
            if (winState == "none")
            {
                Utilities.centerFormPrimary(this);
            }
            else
            {
                this.DesktopBounds = new Rectangle(Properties.Settings.Default.ExportSoldiersDialogLocation, Properties.Settings.Default.ExportSoldiersDialogSize);
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
