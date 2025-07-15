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
    public partial class CounselingTab : UserControl
    {
        enum CounselingMode
        {
            Popup,
            Tab
        };


        SortedDictionary<int, SortedDictionary<int, string>> counselingCheckLists;
        SortedDictionary<int, string> documentDescriptions;

        int soldierID;
        int parentDocumentID;
        CQPGraphicsButton previouslyActiveTabButton;
        int selectedTemplateID;

        Pen splitterPen;
        const int noSelectedSoldierID = Int32.MinValue;

        CounselingMode mode;
        BackgroundWorker insertPackageBackgroundWorker;

        WaitDialog waitDialog;
        TreeNode previousTreeNode;

        public CounselingTab()
        {
            Logger.Trace("CounselingTab - Constructor");

            SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170));
            splitterPen = new Pen(brush, 1);

            soldierID = noSelectedSoldierID;
            parentDocumentID = -1;
            selectedTemplateID = -1;

            Load += OnLoad;
            InitializeComponent();

            previouslyActiveTabButton = generalTabButton;
            generalTabButton.Enabled = false;

            mode = CounselingMode.Tab;
        }


        public CounselingTab(int soldierID, int parentDocumentID)
            : this()
        {
            Logger.Trace("CounselingTab - soldierID: " + soldierID + ", parentDocID: " + parentDocumentID);

            this.soldierID = soldierID;
            this.parentDocumentID = parentDocumentID;
            mode = CounselingMode.Popup;
        }


        void OnLoad(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            Logger.Trace("CounselingTab - OnLoad");

            try
            {
                LoadForm();                
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load the dialog: " + ex.Message, ex);

                string errorMessage = "An error occurred while loading the counseling tab.\n\nCounselor will now close.";

                CQPMessageBox.Show(errorMessage, "Error", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                throw ex;
            }
        }


        private void LoadForm()
        {
            DataTable templates = TemplatesModel.TemplatesTable;
            DataTable templateGroups = TemplatesModel.TemplateGroups;

            LoadTreeView(templateGroups, templates);
            LoadCounselingChecklists(templates);
            LoadCounselingDescriptions(templates);

            InitializeBackgroundWorkers();

            composeCounselingButton.Enabled = false;
            printCheckListButton.Enabled = false;

            webBrowser1.Navigate("about:blank");
            HtmlDocument doc = this.webBrowser1.Document;
            doc.Write(String.Empty);

            webBrowser1.Document.Write("<html><body bgcolor=eeeeee></body></html>");

            webBrowser1.Refresh();
        }


        private void InitializeBackgroundWorkers()
        {
            insertPackageBackgroundWorker = new BackgroundWorker();
            insertPackageBackgroundWorker.DoWork += new DoWorkEventHandler(PackageGenerationDoWork);
            insertPackageBackgroundWorker.WorkerReportsProgress = true;
            insertPackageBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(PackageGenerationProgressChanged);
            insertPackageBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PackageGenerationCompleted);
        }


        private void LoadTreeView(DataTable templateGroups, DataTable templates)
        {
            Logger.Trace("CounselingTab - LoadTreeView");

            foreach (DataRow groupRow in templateGroups.Rows)
            {
                TreeNode groupNode = new TreeNode();

                string groupName = groupRow["templategroupname"].ToString();
                int groupID = Convert.ToInt32(groupRow["templategroupid"]);

                groupNode.Text = groupName;
                groupNode.Tag = groupID;

                Logger.Trace("    CounselingTab - Add group node: " + groupName + ", " + groupID);

                DataRow[] templateRows 
                    = templates.Select("templategroupid = " + groupID
                                        + " and documentcategoryid = " + (int)DocumentCategory.Counseling);

                foreach (DataRow template in templateRows)
                {
                    TreeNode node = new TreeNode();

                    string templateName = template["documentnametext"].ToString();
                    int templateID = Convert.ToInt32(template["tempateid"]);
                    //int templateID = Convert.ToInt32(template["templateid"]);

                    node.Text = templateName;
                    node.Tag = templateID;

                    Logger.Trace("        CounselingTab - Add node: " + templateID + ", " + templateName);

                    groupNode.Nodes.Add(node);
                }

                if (groupNode.Nodes.Count != 0)
                    treeView1.Nodes.Add(groupNode);
            }
        }


        private void LoadCounselingChecklists(DataTable templates)
        {
            counselingCheckLists = new SortedDictionary<int, SortedDictionary<int, string>>();
            counselingCheckLists[0] = new SortedDictionary<int, string>();
            counselingCheckLists[1] = new SortedDictionary<int, string>();
            counselingCheckLists[2] = new SortedDictionary<int, string>();

            Logger.Trace("CounselingTab - LoadCounselingChecklists");

            try
            {
                DataTable counselingChecklistsTable = TemplatesModel.CounselingChecklists;

                foreach (DataRow row in counselingChecklistsTable.Rows)
                {
                    int templateID = Convert.ToInt32(row["templateid"]);
                    
                    string general = row["generaltext"].ToString();
                    string eventspecific = row["eventspecific"].ToString();
                    string extract = row["extract"].ToString();

                    counselingCheckLists[0][templateID] = general;
                    counselingCheckLists[1][templateID] = eventspecific;
                    counselingCheckLists[2][templateID] = extract;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occured while attempting to load the counseling checklists.";

                Logger.Error("CounselingTab - " + errorMessage, ex);

                //throw new CQPException(errorMessage, ex);

                //CQPMessageBox.Show(errorMessage, "", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

            }
        }


        private void LoadCounselingDescriptions(DataTable templates)
        {
            Logger.Trace("CounselingTab - LoadingCounselingDescriptions");

            documentDescriptions = new SortedDictionary<int, string>();

            try
            {
                foreach (DataRow template in templates.Rows)
                {
                    int templateID = Convert.ToInt32(template["tempateid"]);
                    //int templateID = Convert.ToInt32(template["templateid"]);

                    string documentDescription = template["documentdescription"].ToString();

                    documentDescriptions[templateID] = documentDescription;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occured while attempting to load the counseling descriptions.";

                Logger.Error("CounselingTab - " + errorMessage, ex);
            }
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Logger.Trace("CounselingTab - TreeViewAfterSelect");

            if (previousTreeNode != null)
            {
                previousTreeNode.BackColor = treeView1.BackColor;
                previousTreeNode.ForeColor = treeView1.ForeColor;
            }

            previousTreeNode = treeView1.SelectedNode;
            treeView1.SelectedNode.BackColor = SystemColors.Highlight;
            treeView1.SelectedNode.ForeColor = SystemColors.HighlightText;

            // Fill in the document info (general/event specific/etc.)
            selectedTemplateID = GetSelectedTemplateID();

            if (selectedTemplateID == -1)
            {
                composeCounselingButton.Enabled = false;
                printCheckListButton.Enabled = false;

                documentNameLabel.Text = "";
                documentDescriptionLabel.Text = "";
            }
            else
            {
                composeCounselingButton.Enabled = true;
                printCheckListButton.Enabled = true;

                if (documentDescriptions.ContainsKey(selectedTemplateID))
                    documentDescriptionLabel.Text = documentDescriptions[selectedTemplateID];
                else
                    documentDescriptionLabel.Text = "";

                documentNameLabel.Text = treeView1.SelectedNode.Text;
            }

            RefreshChecklistViewer();

        }


        private int GetSelectedTemplateID()
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null
                || treeView1.SelectedNode.Level == 0)
                return -1;

            int templateID = Convert.ToInt32(treeView1.SelectedNode.Tag);

            Logger.Trace("CounselingTab - GetSelectedTemplateID - templateID " + templateID);

            return templateID;
        }


        private DialogResult PromptToChooseSoldier()
        {
            Logger.Trace("CounselingTab - PromptToChooseSoldier");

            SelectSoldierDialog.SelectSoldierMode mode = SelectSoldierDialog.SelectSoldierMode.ChooseSoldierToCounsel;
            SelectSoldierDialog chooseSoldierDialog = new SelectSoldierDialog(mode);
            DialogResult result = chooseSoldierDialog.ShowDialog();

            Logger.Trace("    CounselingTab - GetSelectedTemplateID: " + result.ToString());

            if (result == DialogResult.OK)
            {
                soldierID = chooseSoldierDialog.SelectedSoldierID;
                Logger.Trace("    CounselingTab - GetSelectedTemplateID: " + soldierID);
            }

            return result;
        }


        private void composeCounselingButton_Click(object sender, EventArgs e)
        {
            int templateID = GetSelectedTemplateID();

            Logger.Trace("CounselingTab - ComposeCounselingbuttonClick: " + templateID);

            if (templateID == -1)
                return;

            if (soldierID == noSelectedSoldierID)
            {
                Logger.Trace("    CounselingTab - ComposeCounselingButtonClick: SelectSoldierDialogPopup");

                DialogResult result = PromptToChooseSoldier();
                Logger.Trace("    CounselingTab - ComposeCounselingButtonClick: " + result.ToString());

                if (result == DialogResult.Cancel)
                {
                    if(mode == CounselingMode.Tab)
                        soldierID = noSelectedSoldierID;

                    return;
                }
            }

            Logger.Trace("    CounselingTab - ComposeCounselingButtonClick: " + soldierID);

            List<int> templatesInGroup = DialogHelper.PromptToCompleteCounselingPackage(templateID, soldierID, this);

            Logger.Trace("CounselingTab - ComposeCounselingButtonClick: TemplatesInGroup: " + templatesInGroup.Count);

            if (templatesInGroup.Contains(-1))
            {
                if(mode == CounselingMode.Tab)
                    soldierID = noSelectedSoldierID;

                return;
            }

            Soldier soldier = new Soldier(soldierID);
            Template template = new Template(templateID);
            DA4856Document da4856Document = InitializeDA4856FromTemplate(soldier, template);
            
            int numDocumentsExistBefore = DocumentModel.NumberOfDocuments;

            XFDLEditorPage1 page1 = new XFDLEditorPage1(da4856Document, template);
            page1.ShowDialog();
            
            int numDocumentsAfter = DocumentModel.NumberOfDocuments;

            bool counselingSaved = (numDocumentsAfter - numDocumentsExistBefore) > 0;

            if (counselingSaved)
            {
                if (templatesInGroup.Count > 0)
                {
                    GenerateCounselingPackage(da4856Document, templatesInGroup, soldier);
                }
            }
            else
            {
                if (mode == CounselingMode.Popup)
                    return;
            }

            if (mode == CounselingMode.Popup && templatesInGroup.Count == 0)
                ParentForm.Dispose();
            else if (mode == CounselingMode.Popup && templatesInGroup.Count != 0)
                return;
            else
                soldierID = noSelectedSoldierID;
        }


        private DA4856Document InitializeDA4856FromTemplate(Soldier soldier, Template template)
        {
            Logger.Trace("CounselingTab - InitializeDA4856FromTemplate: " + soldier.SoldierID + ", " + template.TemplateID);

            DA4856Document da4856Document = DA4856Document.GenerateNewFromTemplate(template);
            da4856Document.SoldierID = soldierID;

            if (soldier.SoldierID != -1)
                da4856Document.Name = soldier.LastName + ", " + soldier.FirstName + " " + soldier.MiddleInitial;

            da4856Document.Rank = soldier.Rank;
            da4856Document.ParentDocumentID = parentDocumentID;
            da4856Document.NameAndTitleOfCounselor = OptionsModel.CounselingOptions.DefaultCounselorNameAndTitle;
            da4856Document.NameOfOrganization = OptionsModel.CounselingOptions.DefaultOrganizationName;
            da4856Document.NameGradeCounselee = da4856Document.Name + ", " + RankingModel.RankingToGrade(soldier.Rank);

            return da4856Document;
        }


        private void GenerateCounselingPackage(DA4856Document da4856Document, List<int> templatesInGroup, Soldier soldier)
        {
            Logger.Trace("CounselingTab - GenerateCounselingPackage: " + da4856Document.GeneratedDocID + ", " + templatesInGroup.Count + ", " + soldier.SoldierID);

            this.Enabled = false;
            this.UseWaitCursor = true;

            int headDocumentID;
            if (da4856Document.ParentDocumentID == -1)
                headDocumentID = da4856Document.GeneratedDocID;
            else
                headDocumentID = da4856Document.ParentDocumentID;
            
            TemplatesModel.PackageGenerationParamters parameters = new TemplatesModel.PackageGenerationParamters();
            parameters.headDocumentID = headDocumentID;
            parameters.documentIDs = templatesInGroup;
            parameters.soldier = soldier;

            waitDialog = new WaitDialog("Saving Counseling Package...");
            waitDialog.Enabled = true;

            insertPackageBackgroundWorker.RunWorkerAsync(parameters);
            waitDialog.ShowDialog(this);
        }


        private void printCheckListButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("CounselingTab - PrintCheckListButton");
            webBrowser1.ShowPrintDialog();
        }


        private void DisableTabButton(CQPGraphicsButton button)
        {
            previouslyActiveTabButton.Enabled = true;
            previouslyActiveTabButton = button;

            button.Enabled = false;
        }


        private void generalTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(generalTabButton);
            RefreshChecklistViewer();
            webBrowser1.Focus();
        }


        private void eventSpecificTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(eventSpecificTabButton);
            RefreshChecklistViewer();
            webBrowser1.Focus();
        }


        private void extractTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(extractTabButton);
            RefreshChecklistViewer();
            webBrowser1.Focus();
        }


        private void RefreshChecklistViewer()
        {
            if (selectedTemplateID == -1)
            {
                webBrowser1.Document.Write("<html><body bgcolor=eeeeee></body></html>");
                return;
            }

            int tabPageID = GetSelectedTabPage();

            if (counselingCheckLists[tabPageID].Keys.Contains(selectedTemplateID))
                webBrowser1.DocumentText = counselingCheckLists[tabPageID][selectedTemplateID];
            else
                webBrowser1.Document.Write("<html><body bgcolor=eeeeee></body></html>");
        }


        private int GetSelectedTabPage()
        {
            if (!generalTabButton.Enabled)
                return 0;
            else if (!eventSpecificTabButton.Enabled)
                return 1;
            else if (!extractTabButton.Enabled)
                return 2;

            return 0;
        }


        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            float splitterDistance = (float)(splitContainer1.SplitterDistance + (splitContainer1.SplitterWidth / 2.0));
            int splitterHeight = splitContainer1.Height;

            int yOffset = 15;

            PointF p1 = new PointF(splitterDistance, yOffset);
            PointF p2 = new PointF(splitterDistance, splitterHeight - yOffset );
            
            e.Graphics.DrawLine(splitterPen, p1, p2);
        }


        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            splitContainer1.Invalidate();
            base.OnResize(e);
        }


        private void splitContainer1_Move(object sender, EventArgs e)
        {
            splitContainer1.Invalidate();
            base.OnMove(e);
        }


        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            splitContainer1.Invalidate();
        }


        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer1.Invalidate();
        }


        void PackageGenerationDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Logger.Trace("CounselingTab - PackageGenerationDoWork");

                TemplatesModel.PackageGenerationParamters parameters = (TemplatesModel.PackageGenerationParamters)e.Argument;
                parameters.backgroundWorker = insertPackageBackgroundWorker;

                TemplatesModel.GenerateCounselingPackageInserts(parameters);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while generating the counseling package.\n\nCounselor will now close.";

                Logger.Error(errorMessage, ex);

                throw new CQPException(errorMessage, ex);
            }
        }


        void PackageGenerationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.Trace("CounselingTab - PackageGenerationCompleted");

            this.Enabled = true;
            this.UseWaitCursor = false;

            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    waitDialog.Hide();
                    waitDialog.Dispose();

                    if (mode == CounselingMode.Popup)
                        ParentForm.Dispose();
                });
            }
            else
            {
                waitDialog.Hide();
                waitDialog.Dispose();

                if (mode == CounselingMode.Popup)
                    ParentForm.Dispose();
            }
        }


        void PackageGenerationProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Logger.Trace("CounselingTab - PackageGenerationProgressChanged: " + e.ProgressPercentage);
            
            waitDialog.UpdateProgress(e.ProgressPercentage);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            composeCounselingButton_Click(null, null);
        }


        private void counselingsTreeView_Validating(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            treeView1.SelectedNode.BackColor = SystemColors.Highlight;
            treeView1.SelectedNode.ForeColor = SystemColors.HighlightText;
        }

        private void counselingsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

    }
}
