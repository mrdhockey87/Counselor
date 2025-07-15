using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    public partial class HelpTabPage : UserControl
    {
        DataTable helpDocuments;
        DataTable helpDocumentGroups;
        List<TreeNode> groups;
        List<TreeNode> documents;
        TreeNode previousTreeNode;

        Pen splitterPen;

        struct HelpTag
        {
            internal int id;
            internal bool isGroupNode;
            internal string documentPath;
        }


        public HelpTabPage()
        {
            InitializeComponent();

            groups = new List<TreeNode>();
            documents = new List<TreeNode>();

            helpDocuments = Model.GetHelpDocumentsTable();
            helpDocumentGroups = Model.GetHelpDocumentGroupsTable();

            SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170));
            splitterPen = new Pen(brush, 1);

            InitializeDocumentTreeViewLists();
            //LoadFullGroupedTree();
            RefreshTreeView();
        }


        private void InitializeDocumentTreeViewLists()
        {
            foreach (DataRow referenceGroup in helpDocumentGroups.Rows)
            {
                TreeNode groupNode = GetGroupNode(helpDocuments, referenceGroup);
                groups.Add(groupNode);

                //groupNode.Collapse();
                //referencesTreeView.Nodes.Add(groupNode);
            }
        }


        private TreeNode GetGroupNode(DataTable referenceDocuments, DataRow referenceGroup)
        {
            TreeNode groupNode = new TreeNode();
            HelpTag groupTag = new HelpTag();

            int id = Convert.ToInt32(referenceGroup["helpgroupid"]);
            string name = referenceGroup["helpgroupname"].ToString();

            groupTag.isGroupNode = true;
            groupTag.id = id;

            groupNode.Tag = groupTag;
            groupNode.Text = name;

            DataRow[] documentRows = referenceDocuments.Select("helpgroupid = " + id);

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
            HelpTag documentTag = new HelpTag();

            string filepath = SettingsModel.HelpDirectory + document["helpdocumentfilename"].ToString();
            string name = document["helpdocumentname"].ToString();
            int id = Convert.ToInt32(document["helpdocumentid"]);

            documentNode.Text = name;

            documentTag.id = id;
            documentTag.isGroupNode = false;
            documentTag.documentPath = filepath;

            documentNode.Tag = documentTag;
            return documentNode;
        }


        private void OnHelpNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            HelpTag tag = (HelpTag)node.Tag;

            if (tag.isGroupNode)
                return;

            string filepath = tag.documentPath;
            if (!File.Exists(filepath))
            {
                CQPMessageBox.Show("An error occurred trying to load the preview!", "Error!", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                return;
            }

            try
            {
                string fullpath = new FileInfo(filepath).FullName;
                webBrowser1.Url = new Uri(fullpath);
            }
            catch (Exception)
            {
                CQPMessageBox.Show("An error occurred trying to load the preview!", "Error!", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                return;
            }
        }


        private void cqpSearchBox1_TextChanged(object sender, EventArgs e)
        {
            RefreshTreeView();
        }


        private void RefreshTreeView()
        {
            string query = cqpSearchBox1.Text;

            helpDocsTreeView.BeginUpdate();
            if (query == "")
                LoadFullGroupedTree();
            else
                LoadPDFSearchTreeView(query);
            helpDocsTreeView.EndUpdate();
        }


        private void LoadPDFSearchTreeView(string query)
        {
            this.SuspendDrawing();

            helpDocsTreeView.Nodes.Clear();

            query = query.ToLower();

            // this is an insanely massive LINQ query; basically:
            //   - get all the group nodes that have treenodes with matching names
            //   - get all the group nodes with matching names
            //   - yeah that's basically it; insanely complicated looking but not so bad.
            List<TreeNode> groupsWithMatchingNodes
                = groups.Where(group => group.Nodes.Cast<TreeNode>().Where(
                    node => node.Text.ToLower().Contains(query)).Count() > 0).ToList();

            List<TreeNode> groupsWithMatchingTitle = groups.Where(group => group.Text.ToLower().Contains(query)).ToList();

            foreach (TreeNode node in groupsWithMatchingTitle)
                groupsWithMatchingNodes.Remove(node);

            InsertNodesWithMatchingChildren(groupsWithMatchingNodes, query);
            InsertNodesWithMatchingSubject(groupsWithMatchingTitle, query);

            this.ResumeDrawing();
        }

        private void InsertNodesWithMatchingChildren(List<TreeNode> nodes, string query)
        {
            foreach (TreeNode groupNode in nodes)
            {
                TreeNode groupNodeClone = (TreeNode)groupNode.Clone();

                int id = ((HelpTag)groupNodeClone.Tag).id;
                helpDocsTreeView.Nodes.Insert(id, groupNodeClone);

                foreach (TreeNode node in groupNodeClone.Nodes)
                {
                    if (!node.Text.ToLower().Contains(query))
                        groupNodeClone.Nodes.Remove(node);
                }

                groupNodeClone.ExpandAll();
            }
        }


        private void InsertNodesWithMatchingSubject(List<TreeNode> groupsWithMatchingTitle, string query)
        {
            foreach (TreeNode groupNode in groupsWithMatchingTitle)
            {
                TreeNode groupNodeClone = (TreeNode)groupNode.Clone();
                helpDocsTreeView.Nodes.Insert(((HelpTag)groupNodeClone.Tag).id, groupNodeClone);
                groupNodeClone.ExpandAll();
            }
        }



        private void LoadFullGroupedTree()
        {
            this.SuspendDrawing();

            helpDocsTreeView.Nodes.Clear();

            foreach (TreeNode groupNode in groups)
            {
                TreeNode groupNodeClone = (TreeNode)groupNode.Clone();
                helpDocsTreeView.Nodes.Add(groupNodeClone);
                groupNodeClone.Collapse();
            }


            if (helpDocsTreeView.Nodes.Count != 0)
            {
                helpDocsTreeView.Nodes[0].EnsureVisible();
            }

            this.ResumeDrawing();
        }

        private void helpDocsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (previousTreeNode != null)
            {
                previousTreeNode.BackColor = helpDocsTreeView.BackColor;
                previousTreeNode.ForeColor = helpDocsTreeView.ForeColor;
            }

            previousTreeNode = helpDocsTreeView.SelectedNode;

            if (helpDocsTreeView.SelectedNode == null)
                return;

            helpDocsTreeView.SelectedNode.BackColor = SystemColors.Highlight;
            helpDocsTreeView.SelectedNode.ForeColor = SystemColors.HighlightText;
        }

        private void helpDocsTreeView_Validating(object sender, CancelEventArgs e)
        {
            if (helpDocsTreeView.SelectedNode == null)
                return;

            helpDocsTreeView.SelectedNode.BackColor = SystemColors.Highlight;
            helpDocsTreeView.SelectedNode.ForeColor = SystemColors.HighlightText;
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
    }
}
