using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    public partial class ReferencesTabPage : UserControl
    {
        struct ReferenceTag
        {
            internal int id;
            internal bool isGroupNode;
            internal string documentPath;
        }

        DataTable referenceDocuments;
        DataTable referenceDocumentGroups;
        List<TreeNode> groups;
        List<TreeNode> documents;
        TreeNode previousTreeNode;
        string selectedFilePath;
        string referencesDirectory;

        Pen splitterPen;

        public ReferencesTabPage()
        {
            Load += new EventHandler(ReferencesTabPage_Load);
            InitializeComponent();

            referenceDocuments = Model.GetReferenceDocumentsTable();
            referenceDocumentGroups = Model.GetReferenceGroupsTable();

            groups = new List<TreeNode>();
            documents = new List<TreeNode>();

            SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170));
            splitterPen = new Pen(brush, 1);

            LoadAdobeReader();
            InitializeDocumentTreeViewLists();
            //LoadFullGroupedTree();
            RefreshTreeView();

            webBrowser1.DocumentText = CounselQuickPlatinum.Properties.Resources.blank;
        }

        void ReferencesTabPage_Load(object sender, EventArgs e)
        {
            referencesDirectory = SettingsModel.ReferencesDirectory;
        }


        private void LoadAdobeReader()
        {
            try
            {
                Type applicationType = Type.GetTypeFromProgID("AcroPDF.PDF");
                
                if (applicationType == null)
                {
                    if (OptionsModel.ReferenceOptions.ShowAdobeMissingWarning)
                    {
                        warningLabel.Visible = true;
                        stopShowingAdobeWarningLink.Visible = true;
                    }
                }
                else
                {
                    Controls.Remove(stopShowingAdobeWarningLink);//.Visible = false;
                    Controls.Remove(warningLabel);
                }

            }
            catch (Exception)
            {

            }
        }


        private void InitializeDocumentTreeViewLists()
        {
            foreach (DataRow referenceGroup in referenceDocumentGroups.Rows)
            {
                TreeNode groupNode = GetGroupNode(referenceDocuments, referenceGroup);
                groups.Add(groupNode);
            }
        }


        private TreeNode GetGroupNode(DataTable referenceDocuments, DataRow referenceGroup)
        {
            TreeNode groupNode = new TreeNode();
            ReferenceTag groupTag = new ReferenceTag();

            int id = Convert.ToInt32(referenceGroup["referencegroupid"]);
            string name = referenceGroup["referencegroupname"].ToString();

            groupTag.isGroupNode = true;
            groupTag.id = id;

            groupNode.Tag = groupTag;
            groupNode.Text = name;

            DataRow[] documentRows = referenceDocuments.Select("referencegroupid = " + id);

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
            ReferenceTag documentTag = new ReferenceTag();

            string filepath = document["referencedocumentfilename"].ToString();
            string name = document["referencedocumentname"].ToString();
            int id = Convert.ToInt32(document["referencedocumentid"]);

            documentNode.Text = name;

            documentTag.id = id;
            documentTag.isGroupNode = false;
            documentTag.documentPath = filepath;

            documentNode.Tag = documentTag;
            return documentNode;
        }

        private void TreeNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Logger.Trace("ReferencesTabPage - TreeNodeDoubleClick ");
            
            TreeNode node = e.Node;
            ReferenceTag tag = (ReferenceTag)node.Tag;

            if (tag.isGroupNode)
                return;

            string filepath = referencesDirectory + tag.documentPath;

            Logger.Trace("   ReferencesTabPage - TreeNodeDoubleClick - Opening Preview: " + filepath);

            if (!File.Exists(filepath))
            {
                Logger.Error("   ReferencesTabPage - TreeNodeDoubleClick - Opening Preview FAILED " + filepath + " - failed File.Exists check");

                CQPMessageBox.Show("An error occurred trying to load the preview!", "Error!", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                return;
            }


            try
            {
                string fullpath = new FileInfo(filepath).FullName;
                Logger.Trace("   ReferencesTabPage - TreeNodeDoubleClick - Opening Preview (fullpath): " + fullpath);
                selectedFilePath = fullpath;

                webBrowser1.Url = new Uri(fullpath);

                cqpGrayRectangleButton1.Enabled = true;
                cqpPrintButton1.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Trace("   ReferencesTabPage - TreeNodeDoubleClick - Opening Preview FAILED: " + ex.Message);

                CQPMessageBox.Show("An error occurred trying to load the preview!", "Error!", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                cqpGrayRectangleButton1.Enabled = false;
                cqpPrintButton1.Enabled = false;

                return;
            }

            referencesTreeView.Focus();
        }


        private void SearchTextboxChanged(object sender, EventArgs e)
        {
            RefreshTreeView();
        }


        private void RefreshTreeView()
        {
            referencesTreeView.BeginUpdate();
            
            string query = cqpSearchBox1.Text;

            if (query == "")
                LoadFullGroupedTree();
            else
                LoadPDFSearchTreeView(query);

            referencesTreeView.EndUpdate();
        }


        private void LoadPDFSearchTreeView(string query)
        {
            this.SuspendDrawing();

            referencesTreeView.Nodes.Clear();

            query = query.ToLower();

            // this is an insanely massive LINQ query; basically:
            //   - get all the group nodes that have treenodes with matching names
            //   - get all the group nodes with matching names
            //   - yeah that's basically it; insanely complicated looking but not so bad.
            List<TreeNode> groupsWithMatchingNodes
                = groups.Where(group => group.Nodes.Cast<TreeNode>().Where( 
                    node => node.Text.ToLower().Contains(query)).Count() > 0 
                    || group.Text.ToLower().Contains(query) ).ToList();

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

                int id = ((ReferenceTag)groupNodeClone.Tag).id;
                referencesTreeView.Nodes.Insert(id, groupNodeClone);

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
                referencesTreeView.Nodes.Insert(((ReferenceTag)groupNodeClone.Tag).id, groupNodeClone);
                groupNodeClone.ExpandAll();
            }
        }
        

        private void LoadFullGroupedTree()
        {
            this.SuspendDrawing();

            referencesTreeView.Nodes.Clear();

            foreach (TreeNode groupNode in groups)
            {
                TreeNode groupNodeClone = (TreeNode)groupNode.Clone();
                referencesTreeView.Nodes.Add(groupNodeClone);
                groupNodeClone.Collapse();
            }

            if(referencesTreeView.Nodes.Count != 0)
                referencesTreeView.Nodes[0].EnsureVisible();

            this.ResumeDrawing();
        }


        private void OnOpenButtonClicked(object sender, EventArgs e)
        {
            Logger.Trace("ReferencesTabPage OpenButtonClicked ");

            //string filename = webBrowser1.Url.AbsolutePath;
            string filename = selectedFilePath;

            Logger.Trace("    ReferencesTabPage - opening: " + filename);

            if (filename == "" || filename == null)
                return;

            if (!File.Exists(filename))
            {
                Logger.Error("    ReferencesTabPage -  " + filename + " does not exist.");

                CQPMessageBox.Show("An error occured trying to print the file!", "Error!", 
                    CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);

                return;
            }

            System.Diagnostics.Process.Start(filename);
        }

        private void OnPrintButtonClicked(object sender, EventArgs e)
        {
            //if (!adobePresent)
            //    return;

            //AxAcroPDFLib.AxAcroPDF adobe = (AxAcroPDFLib.AxAcroPDF)adobePanel.Controls[0];
            //string filename = adobe.src;

            //string filename = webBrowser1.Url.AbsolutePath;

            //if (filename == "" || filename == null)
            //    return;
            //if (!File.Exists(filename))
            //{
            //    CQPMessageBox.Show("An error occured trying to print the file!", "Error!", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
            //}

            //adobe.Print();

            webBrowser1.ShowPrintDialog();
        }

        private void StopShowingThisMessageClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OptionsModel.ReferenceOptions.ShowAdobeMissingWarning = false;
            OptionsModel.Save();

            Controls.Remove(stopShowingAdobeWarningLink);//.Visible = false;
            Controls.Remove(warningLabel);//.Visible = false;            
        }

        private void cqpSmallPillButton1_Click(object sender, EventArgs e)
        {
            cqpSearchBox1.Text = "";
        }

        private void referencesTreeView_Validating(object sender, CancelEventArgs e)
        {
            if (referencesTreeView.SelectedNode == null)
                return;

            referencesTreeView.SelectedNode.BackColor = SystemColors.Highlight;
            referencesTreeView.SelectedNode.ForeColor = SystemColors.HighlightText;

            ////referencesTreeView.SelectedNode.ForeColor = Color.White;

            //previousTreeNode = referencesTreeView.SelectedNode;
        }

        private void referencesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (previousTreeNode != null)
            {
                previousTreeNode.BackColor = referencesTreeView.BackColor;
                previousTreeNode.ForeColor = referencesTreeView.ForeColor;
            }

            previousTreeNode = referencesTreeView.SelectedNode;

            if (referencesTreeView.SelectedNode == null)
                return;

            referencesTreeView.SelectedNode.BackColor = SystemColors.Highlight;
            referencesTreeView.SelectedNode.ForeColor = SystemColors.HighlightText;
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
