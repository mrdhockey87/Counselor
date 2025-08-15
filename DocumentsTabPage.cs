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
    internal partial class DocumentsTabPage : UserControl
    {
        enum DocumentationMode
        {
            Popup,
            Tab
        };


        SortedDictionary<int, SortedDictionary<int, string>> documentChecklists;
        SortedDictionary<int, string> documentDescriptions;

        int soldierID;
        int parentDocumentID;
        CQPGraphicsButton previouslyActiveTabButton;
        int selectedTemplateID;

        Pen splitterPen;
        const int noSelectedSoldierID = Int32.MinValue;

        DocumentationMode mode;
        WaitDialog waitDialog;
        BackgroundWorker insertPackageBackgroundWorker;

        TreeNode previousTreeNode;

        List<TreeNode> groups;
        List<TreeNode> documents;

        internal DocumentsTabPage()
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170));
            splitterPen = new Pen(brush, 1);

            soldierID = noSelectedSoldierID;
            parentDocumentID = -1;
            selectedTemplateID = -1;

            Load += new EventHandler(DocumentsTabPage_Load);
            InitializeComponent();

            previouslyActiveTabButton = generalTabButton;
            generalTabButton.Enabled = false;

            groups = new List<TreeNode>();
            documents = new List<TreeNode>();

            mode = DocumentationMode.Tab;
        }


        internal DocumentsTabPage(int soldierID, int parentDocumentID, DocumentFormIDs formid)
            : this()
        {
            this.soldierID = soldierID;
            this.parentDocumentID = parentDocumentID;
            mode = DocumentationMode.Popup;
        }


        internal DocumentsTabPage(int soldierID, int parentDocumentID)
            : this()
        {
            this.soldierID = soldierID;
            this.parentDocumentID = parentDocumentID;
            mode = DocumentationMode.Popup;
        }
        

        void DocumentsTabPage_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable templates = TemplatesModel.TemplatesTable.Copy();
                DataTable templateGroups = TemplatesModel.TemplateGroups.Copy();

                //LoadTreeView(templateGroups, templates);
                LoadTreeViewLists(templateGroups, templates);
                RefreshTreeView();
                //LoadFullGroupedTree();

                LoadDocumentChecklists(templates);
                LoadDocumentDescriptions(templates);

                InitializeBackgroundWorkers();

                composeDocumentButton.Enabled = false;
                printCheckListButton.Enabled = false;

                webBrowser1.Navigate("about:blank");
                HtmlDocument doc = this.webBrowser1.Document;
                doc.Write(String.Empty);

                webBrowser1.Document.Write("<html><body bgcolor=eeeeee></body></html>");
                webBrowser1.Refresh();
            }
            catch (DataLoadFailedException ex)
            {
                CQPMessageBox.Show(ex.Message, "Error", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                throw ex;
            }
        }


        private void InitializeBackgroundWorkers()
        {
            insertPackageBackgroundWorker = new BackgroundWorker();
            insertPackageBackgroundWorker.DoWork += new DoWorkEventHandler(PackageGenerationDoWork);
            insertPackageBackgroundWorker.WorkerReportsProgress = true;
            insertPackageBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(PackageGenerationProgressChanged);
            insertPackageBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PackageGenerationCompleted);
        }


        private void LoadTreeViewLists(DataTable documentGroups, DataTable templates)
        {
            foreach (DataRow group in documentGroups.Rows)
            {
                TreeNode groupNode = GetGroupNode(templates, group);

                if (groupNode.Nodes.Count != 0)
                    groups.Add(groupNode);

                //groupNode.Collapse();
                //referencesTreeView.Nodes.Add(groupNode);
            }
        }


        private TreeNode GetGroupNode(DataTable referenceDocuments, DataRow referenceGroup)
        {
            TreeNode groupNode = new TreeNode();
            
            int id = Convert.ToInt32(referenceGroup["templategroupid"]);
            string name = referenceGroup["templategroupname"].ToString();

            groupNode.Tag = id;
            groupNode.Text = name;

            DataRow[] documentRows = referenceDocuments.Select("templategroupid = " + id);

            foreach (DataRow document in documentRows)
            {
                TreeNode documentNode = GetDocumentNode(document);
                documents.Add(documentNode);

                groupNode.Nodes.Add(documentNode);
            }

            return groupNode;
        }


        private TreeNode GetDocumentNode(DataRow document)
        {
            TreeNode documentNode = new TreeNode();
            //ReferenceTag documentTag = new ReferenceTag();

            //string filepath = document["referencedocumentfilename"].ToString();
            string name = document["documentnametext"].ToString();
            int id = Convert.ToInt32(document["tempateid"]);
            //int id = Convert.ToInt32(document["templateid"]);

            documentNode.Text = name;
            documentNode.Tag = id;

            /*documentTag.id = id;
            documentTag.isGroupNode = false;
            documentTag.documentPath = filepath;*/

            //documentNode.Tag = documentTag;

            return documentNode;
        }



        private void LoadDocumentChecklists(DataTable templates)
        {
            documentChecklists = new SortedDictionary<int, SortedDictionary<int, string>>();
            documentChecklists[0] = new SortedDictionary<int, string>();
            documentChecklists[1] = new SortedDictionary<int, string>();
            documentChecklists[2] = new SortedDictionary<int, string>();

            try
            {
                DataTable counselingChecklistsTable = TemplatesModel.CounselingChecklists;

                foreach (DataRow row in counselingChecklistsTable.Rows)
                {
                    int templateID = Convert.ToInt32(row["templateid"]);

                    string general = row["generaltext"].ToString();
                    string eventspecific = row["eventspecific"].ToString();
                    string extract = row["extract"].ToString();

                    documentChecklists[0][templateID] = general;
                    documentChecklists[1][templateID] = eventspecific;
                    documentChecklists[2][templateID] = extract;
                }
            }
            catch (Exception)
            {
                CQPMessageBox.Show("An error occured while attempting to load the counseling checklists.");
            }
        }


        private void LoadDocumentDescriptions(DataTable templates)
        {
            documentDescriptions = new SortedDictionary<int, string>();

            foreach (DataRow template in templates.Rows)
            {
                int templateID = Convert.ToInt32(template["tempateid"]);
                //int templateID = Convert.ToInt32(template["templateid"]);

                string documentDescription = template["documentdescription"].ToString();

                documentDescriptions[templateID] = documentDescription;
            }
        }


        private int GetSelectedTemplateID()
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null
                || treeView1.SelectedNode.Level == 0)
                return -1;

            int templateID = Convert.ToInt32(treeView1.SelectedNode.Tag);
            return templateID;
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Color the Node and clear the color of the previous node
            if (previousTreeNode != null)
            {
                previousTreeNode.BackColor = treeView1.BackColor;
                previousTreeNode.ForeColor = treeView1.ForeColor;
            }

            previousTreeNode = treeView1.SelectedNode;
            treeView1.SelectedNode.BackColor = SystemColors.Highlight;
            treeView1.SelectedNode.ForeColor = SystemColors.HighlightText;


            selectedTemplateID = GetSelectedTemplateID();

            if (selectedTemplateID == -1)
            {
                composeDocumentButton.Enabled = false;
                printCheckListButton.Enabled = false;

                documentNameLabel.Text = "";
                documentDescriptionLabel.Text = "";
            }
            else
            {
                composeDocumentButton.Enabled = true;
                printCheckListButton.Enabled = true;

                if (documentDescriptions.ContainsKey(selectedTemplateID))
                    documentDescriptionLabel.Text = documentDescriptions[selectedTemplateID];
                else
                    documentDescriptionLabel.Text = "";

                documentNameLabel.Text = treeView1.SelectedNode.Text;
            }

            RefreshChecklistViewer();
        }

        private DialogResult PromptToChooseSoldier()
        {
            SelectSoldierDialog.SelectSoldierMode mode = SelectSoldierDialog.SelectSoldierMode.ChooseSoldierToCounsel;
            SelectSoldierDialog chooseSoldierDialog = new SelectSoldierDialog(mode);
            DialogResult result = chooseSoldierDialog.ShowDialog();

            if (result == DialogResult.OK)
                soldierID = chooseSoldierDialog.SelectedSoldierID;

            return result;
        }


        private void composeDocumentButton_Click(object sender, EventArgs e)
        {
            int templateID = GetSelectedTemplateID();
            if (templateID == -1)
            {
                return;
            }

            if (soldierID == noSelectedSoldierID)
            {
                DialogResult result = PromptToChooseSoldier();
                if (result == DialogResult.Cancel)
                {
                    if(mode == DocumentationMode.Tab)
                        soldierID = noSelectedSoldierID;

                    return;
                }
            }

            List<int> templatesInGroup 
                = DialogHelper.PromptToCompleteCounselingPackage(templateID, soldierID, this);

            //if templatesInGroup contains -1 then the user cancelled and we need to back out
            if (templatesInGroup.Contains(-1))
            {
                if(mode == DocumentationMode.Tab)
                    soldierID = noSelectedSoldierID;

                return;
            }

            Template template = new Template(templateID);
            int documentID = ShowAndGenerateDocument(template);

            bool documentSaved = documentID != -1;

            if (documentSaved)
            {
                if (templatesInGroup.Count > 0)
                {
                    InsertCounselingPackage(documentID, templatesInGroup);
                }
            }
            else
            {
                if (mode == DocumentationMode.Popup)
                    return;
            }

            if (mode == DocumentationMode.Popup && templatesInGroup.Count == 0)
                ParentForm.Dispose();
            else if (mode == DocumentationMode.Popup && templatesInGroup.Count != 0)
                return;
            else
                soldierID = noSelectedSoldierID;
        }


        private void InsertCounselingPackage(int documentID, List<int> templatesInGroup)
        {
            this.Enabled = false;
            this.UseWaitCursor = true;

            int headDocumentID;
            if (parentDocumentID == -1)
                headDocumentID = documentID;
            else
                headDocumentID = parentDocumentID;

            Soldier soldier = new Soldier(soldierID);

            TemplatesModel.PackageGenerationParamters parameters = new TemplatesModel.PackageGenerationParamters();
            parameters.headDocumentID = headDocumentID;
            parameters.documentIDs = templatesInGroup;
            parameters.soldier = soldier;


            waitDialog = new WaitDialog("Saving Counseling Package...");
            waitDialog.Enabled = true;

            insertPackageBackgroundWorker.RunWorkerAsync(parameters);
            waitDialog.ShowDialog(this);
        }


        private int ShowAndGenerateDocument(Template template)
        {
            int documentID = -1;
            DocumentFormIDs form = template.FormID;

            //if (form == DocumentFormIDs.DA4856 || form == DocumentFormIDs.DA4856PDF )
            if (form == DocumentFormIDs.DA4856PDF)
            {
                documentID = HandleCounseling(template);
            }
            else if (form == DocumentFormIDs.GenericMemo)
            {
                documentID = HandleGenericMemo(template);
            }
            else if (template.DocumentType == DocumentType.Letter)
            {
                documentID = HandleLetter(template);
            }
            else if (template.DocumentType == DocumentType.InfoPaper)
            {
                documentID = HandlePDFFile(template);
            }
            else if (form == DocumentFormIDs.PregnancyElectionStatement)
            {
                documentID = HandlePregnancyElectionStatement(template);
            }
            else
            {
                CQPMessageBox.Show("An error occurred attempting to load this template.", "Not supported yet.",
                                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Information);
            }

            return documentID;
        }

        private int HandleLetter(Template template)
        {
            LetterInterface letter = LetterInterface.GenerateNewFromTemplate(template);
            string filepath = DocumentModel.GetFormFilename((int)template.FormID);

            letter.Date = DateTime.Now;
            letter.DocumentName = template.TemplateName;
            letter.DocumentType = template.DocumentType;
            letter.Filepath = filepath;
            letter.FilepathChanged = false;
            letter.FormID = (int)template.FormID;
            letter.GeneratedDocID = -1;
            letter.ParentDocumentID = parentDocumentID;
            letter.Status = DocumentStatus.Draft;
            letter.TemplateID = template.TemplateID;
            letter.UserUploaded = false;

            letter.SoldiersName = OptionsModel.GenericMemoOptions.DefaultSignatureName;
            letter.Rank = OptionsModel.GenericMemoOptions.DefaultRank;
            letter.Title = OptionsModel.GenericMemoOptions.DefaultTitle;

            Soldier soldier = new Soldier(soldierID);
            UnitHierarchyModel.UnitHierarchy unitHierarchy = soldier.UnitHierarchy;

            letter.SoldierID = soldierID;

            if (soldierID != -1)
            {
                letter.Company = UnitHierarchyModel.GetUnitName(unitHierarchy.unitID)
                                    + " " + UnitHierarchyModel.GetUnitDesignatorName(unitHierarchy.unitDesignatorID);

                letter.BattalionSquadron = UnitHierarchyModel.GetBattalionName(unitHierarchy.battalionID);

                letter.SoldiersName = soldier.FirstName + " " + soldier.LastName;
                letter.Rank = RankingModel.RankToString(soldier.Rank);
                letter.Title = OptionsModel.GenericMemoOptions.DefaultTitle;
            }

            LetterEditor form = new LetterEditor(letter, template);
            form.ShowDialog();

            return letter.GeneratedDocID;
        }

        private int HandlePDFFile(Template template)
        {
            Document document = new Document();

            string filepath = DocumentModel.GetFormFilename((int)template.FormID);

            document.Date = DateTime.Now;
            document.DocumentName = template.TemplateName;
            document.DocumentType = template.DocumentType;
            document.Filepath = filepath;
            document.FilepathChanged = false;
            document.FormID = (int) template.FormID;
            document.GeneratedDocID = -1;
            document.Status = DocumentStatus.Draft;
            document.TemplateID = template.TemplateID;
            document.UserUploaded = false;

            document.ParentDocumentID = parentDocumentID;

            document.SoldierID = soldierID;

            PDFViewerForm form = new PDFViewerForm(document);
            form.ShowDialog();

            return document.GeneratedDocID;
        }


        private int HandlePregnancyElectionStatement(Template template)
        {
            Soldier soldier = new Soldier(soldierID);
            PregnancyElectionStatementMemo memo = PregnancyElectionStatementMemo.GenerateNewFromTemplate(template);

            memo.SoldierID = soldier.SoldierID;
            memo.ParentDocumentID = memo.ParentDocumentID;

            if (soldierID != -1)
            {
                string formattedSoldierName = soldier.LastName + ", " + soldier.FirstName + " " + soldier.MiddleInitial;

                memo.ToPrintedNameOfSoldier = formattedSoldierName;
                memo.FromNameOfSoldier = formattedSoldierName;
                memo.SecondSignatureBlockSoldiersName = formattedSoldierName;
            }

            memo.FirstSignatureBlockFromCommandersName = OptionsModel.GenericMemoOptions.DefaultSignatureName;
            memo.FirstSignatureBlockFromRankBranch = OptionsModel.GenericMemoOptions.DefaultRank;
            memo.FirstSignatureBlockFromCommanderTitle = OptionsModel.GenericMemoOptions.DefaultTitle;

            memo.FromNameOfCommander = OptionsModel.GenericMemoOptions.DefaultSignatureName;
            memo.MemorandumForText = "Memorandum for Commander";

            PregnancyElectionStatementEditorStepOne dialog = new PregnancyElectionStatementEditorStepOne(memo, template);
            dialog.ShowDialog();

            return memo.GeneratedDocID;
        }


        private int HandleCounseling(Template template)
        {
            Soldier soldier = new Soldier(soldierID);

            DA4856Document da4856Document = DA4856Document.GenerateNewFromTemplate(template);

            da4856Document.SoldierID = soldierID;

            if (soldier.SoldierID != -1)
                da4856Document.Name = soldier.LastName + ", " + soldier.FirstName + " " + soldier.MiddleInitial;

            da4856Document.Rank = soldier.Rank;
            da4856Document.ParentDocumentID = parentDocumentID;
            da4856Document.NameAndTitleOfCounselor = OptionsModel.CounselingOptions.DefaultCounselorNameAndTitle;
            da4856Document.NameOfOrganization = OptionsModel.CounselingOptions.DefaultOrganizationName;
            da4856Document.NameGradeCounselee = da4856Document.Name + ", " + RankingModel.RankingToGrade(soldier.Rank);

            DialogHelper.ShowDocumentEditorByFormID(da4856Document, this);

            return da4856Document.GeneratedDocID;
        }


        private int HandleGenericMemo(Template template)
        {
            Soldier soldier = new Soldier(soldierID);
            GenericMemo memo = GenericMemo.GenerateNewFromTemplate(template);

            memo.SoldierID = soldier.SoldierID;
            memo.ParentDocumentID = parentDocumentID;

            memo.SenderName = OptionsModel.GenericMemoOptions.DefaultSignatureName;
            memo.SenderRank = OptionsModel.GenericMemoOptions.DefaultRank;
            memo.SenderTitle = OptionsModel.GenericMemoOptions.DefaultTitle;

            memo.SenderName = OptionsModel.GenericMemoOptions.DefaultSignatureName;

            DialogHelper.ShowDocumentEditorByFormID(memo, this);

            return memo.GeneratedDocID;
        }


        private void printCheckListButton_Click(object sender, EventArgs e)
        {
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
        }


        private void eventSpecificTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(eventSpecificTabButton);
            RefreshChecklistViewer();
        }


        private void extractTabButton_Click(object sender, EventArgs e)
        {
            DisableTabButton(extractTabButton);
            RefreshChecklistViewer();
        }

        private void RefreshChecklistViewer()
        {
            if (selectedTemplateID == -1)
            {
                webBrowser1.Document.Write("<html><body bgcolor=eeeeee></body></html>");
                webBrowser1.Select();
                return;
            }

            int tabPageID = GetSelectedTabPage();

            if (documentChecklists[tabPageID].Keys.Contains(selectedTemplateID))
                webBrowser1.DocumentText = documentChecklists[tabPageID][selectedTemplateID];
            else
            {
                webBrowser1.Document.Write("<html><body bgcolor=eeeeee></body></html>");
            }

            webBrowser1.Select();
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
            PointF p2 = new PointF(splitterDistance, splitterHeight - yOffset);

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

            TemplatesModel.PackageGenerationParamters parameters = (TemplatesModel.PackageGenerationParamters)e.Argument;
            parameters.backgroundWorker = insertPackageBackgroundWorker;

            TemplatesModel.GenerateCounselingPackageInserts(parameters);
        }


        void PackageGenerationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            this.UseWaitCursor = false;

            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    waitDialog.Hide();
                    waitDialog.Dispose();

                    if (mode == DocumentationMode.Popup)
                        ParentForm.Dispose();
                });
            }
            else
            {
                waitDialog.Hide();
                waitDialog.Dispose();

                if (mode == DocumentationMode.Popup)
                    ParentForm.Dispose();
            }
        }


        void PackageGenerationProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            waitDialog.UpdateProgress(e.ProgressPercentage);
            System.Diagnostics.Debug.WriteLine("Insert counseling package - percent complete: " + e.ProgressPercentage);
        }


        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            composeDocumentButton_Click(null, null);
        }

        private void treeView1_Validating(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            treeView1.SelectedNode.BackColor = SystemColors.Highlight;
            treeView1.SelectedNode.ForeColor = SystemColors.HighlightText;
        }

        private void splitContainer1_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            float splitterDistance = (float)(splitContainer1.SplitterDistance + (splitContainer1.SplitterWidth / 2.0));
            int splitterHeight = splitContainer1.Height;

            int yOffset = 15;

            PointF p1 = new PointF(splitterDistance, yOffset);
            PointF p2 = new PointF(splitterDistance, splitterHeight - yOffset);

            e.Graphics.DrawLine(splitterPen, p1, p2);
        }


        private void clearButton_Click(object sender, EventArgs e)
        {
            searchBox.Text = "";
        }


        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            RefreshTreeView();
        }


        private void RefreshTreeView()
        {
            string query = searchBox.Text;

            if (query == "")
                LoadFullGroupedTree();
            else
                LoadFilteredTreeView(query);

           // EnsureLastNodeIsVisible();
        }

       /* private void EnsureLastNodeIsVisible()
        {
            int numNodes = treeView1.Nodes.Count;
            if (numNodes > 0)
            {
                treeView1.Nodes[numNodes - 1].EnsureVisible();
                //treeView1.Nodes[0].EnsureVisible();
            }
        }*/

        private void LoadFullGroupedTree()
        {
            this.SuspendDrawing();
            treeView1.BeginUpdate();
            
            treeView1.Nodes.Clear();

            foreach (TreeNode groupNode in groups)
            {
                TreeNode groupNodeClone = (TreeNode)groupNode.Clone();

                if(groupNodeClone.Nodes.Count != 0)
                    treeView1.Nodes.Add(groupNodeClone);

                groupNodeClone.Collapse();
            }

            if (treeView1.Nodes.Count != 0)
                treeView1.Nodes[0].EnsureVisible();

            treeView1.EndUpdate();
            this.ResumeDrawing();
        }


        private void LoadFilteredTreeView(string query)
        {
            this.SuspendDrawing();
            treeView1.BeginUpdate();

            treeView1.Nodes.Clear();

            query = query.ToLower();

            // this is an insanely massive LINQ query; basically:
            //   - get all the group nodes that have treenodes with matching names
            //   - get all the group nodes with matching names
            //   - yeah that's basically it; insanely complicated looking but not so bad.
            List<TreeNode> groupsWithMatchingNodes
                = groups.Where(group => group.Nodes.Cast<TreeNode>().Where(
                    node => node.Text.ToLower().Contains(query)).Count() > 0
                    || group.Text.ToLower().Contains(query)).ToList();

            List<TreeNode> groupsWithMatchingTitle = groups.Where(group => group.Text.ToLower().Contains(query)).ToList();

            foreach (TreeNode node in groupsWithMatchingTitle)
                groupsWithMatchingNodes.Remove(node);

            InsertNodesWithMatchingChildren(groupsWithMatchingNodes, query);
            InsertNodesWithMatchingSubject(groupsWithMatchingTitle, query);

            if (previousTreeNode != null && previousTreeNode.IsVisible)
                previousTreeNode.EnsureVisible();
            else if (treeView1.Nodes.Count > 0)
                treeView1.Nodes[0].EnsureVisible();

            treeView1.EndUpdate();
            this.ResumeDrawing();
        }


        private void InsertNodesWithMatchingChildren(List<TreeNode> nodes, string query)
        {
            foreach (TreeNode groupNode in nodes)
            {
                TreeNode groupNodeClone = (TreeNode)groupNode.Clone();

                //int id = ((ReferenceTag)groupNodeClone.Tag).id;
                int id = Convert.ToInt32(groupNode.Tag);
                treeView1.Nodes.Insert(id, groupNodeClone);
                /*
                foreach (TreeNode node in groupNodeClone.Nodes)
                {
                    if (!node.Text.ToLower().Contains(query))
                        groupNodeClone.Nodes.Remove(node);
                }*/
                //Above for each would cause null pointer exception when a node was removed from the list of nodes 
                //that wasn't the last node because the node list was now shoter than it was originally mdail 4/11/2022
                if (groupNodeClone.Nodes.Count > 0)
                {
                    for (int i = groupNodeClone.Nodes.Count - 1; i >= 0; i--)
                    {
                        TreeNode node = groupNodeClone.Nodes[i];
                        if (!node.Text.ToLower().Contains(query))
                        {
                            groupNodeClone.Nodes.RemoveAt(i);
                        }
                    }
                }

                groupNodeClone.ExpandAll();
            }
        }


        private void InsertNodesWithMatchingSubject(List<TreeNode> groupsWithMatchingTitle, string query)
        {
            foreach (TreeNode groupNode in groupsWithMatchingTitle)
            {
                TreeNode groupNodeClone = (TreeNode)groupNode.Clone();
                treeView1.Nodes.Insert(Convert.ToInt32(groupNodeClone.Tag), groupNodeClone);
                groupNodeClone.ExpandAll();
            }
        }

        private void requestACounselingLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.armycounselingonline.com/request-a-counseling/");
        }

    }
}

