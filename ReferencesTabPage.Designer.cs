namespace CounselQuickPlatinum
{
    partial class ReferencesTabPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.warningLabel = new System.Windows.Forms.Label();
            this.cqpPrintButton1 = new CounselQuickPlatinum.CQPPrintButton();
            this.cqpGrayRectangleButton1 = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.stopShowingAdobeWarningLink = new CounselQuickPlatinum.CQPLinkLabel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.referencesTreeView = new System.Windows.Forms.TreeView();
            this.referencesLabel = new CounselQuickPlatinum.CQPLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cqpSmallPillButton1 = new CounselQuickPlatinum.CQPSmallPillButton();
            this.cqpSearchBox1 = new CounselQuickPlatinum.CQPSearchBox();
            this.cqpLabel1 = new CounselQuickPlatinum.CQPLabel();
            this.cqpLabel2 = new CounselQuickPlatinum.CQPLabel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.adobePanel = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.adobePanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.warningLabel, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.cqpPrintButton1, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.cqpGrayRectangleButton1, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.stopShowingAdobeWarningLink, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(13, 530);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(763, 52);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // warningLabel
            // 
            this.warningLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warningLabel.Location = new System.Drawing.Point(3, 0);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(345, 35);
            this.warningLabel.TabIndex = 0;
            this.warningLabel.Text = "Adobe Reader was not detected on your system; the preview pane may not be able to" +
    " display the document previews as a result.";
            this.warningLabel.Visible = false;
            // 
            // cqpPrintButton1
            // 
            this.cqpPrintButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cqpPrintButton1.AutoSize = true;
            this.cqpPrintButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cqpPrintButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cqpPrintButton1.Enabled = false;
            this.cqpPrintButton1.FlatAppearance.BorderSize = 0;
            this.cqpPrintButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cqpPrintButton1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpPrintButton1.Image = global::CounselQuickPlatinum.Properties.Resources.print_up;
            this.cqpPrintButton1.Location = new System.Drawing.Point(714, 3);
            this.cqpPrintButton1.Name = "cqpPrintButton1";
            this.cqpPrintButton1.Size = new System.Drawing.Size(46, 30);
            this.cqpPrintButton1.TabIndex = 3;
            this.cqpPrintButton1.Text = " ";
            this.cqpPrintButton1.UseVisualStyleBackColor = false;
            this.cqpPrintButton1.Click += new System.EventHandler(this.OnPrintButtonClicked);
            // 
            // cqpGrayRectangleButton1
            // 
            this.cqpGrayRectangleButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cqpGrayRectangleButton1.BackColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.cqpGrayRectangleButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cqpGrayRectangleButton1.Enabled = false;
            this.cqpGrayRectangleButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatAppearance.BorderSize = 0;
            this.cqpGrayRectangleButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cqpGrayRectangleButton1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpGrayRectangleButton1.Location = new System.Drawing.Point(643, 5);
            this.cqpGrayRectangleButton1.Name = "cqpGrayRectangleButton1";
            this.cqpGrayRectangleButton1.Size = new System.Drawing.Size(65, 25);
            this.cqpGrayRectangleButton1.TabIndex = 2;
            this.cqpGrayRectangleButton1.Text = "Open";
            this.cqpGrayRectangleButton1.UseVisualStyleBackColor = false;
            this.cqpGrayRectangleButton1.Click += new System.EventHandler(this.OnOpenButtonClicked);
            // 
            // stopShowingAdobeWarningLink
            // 
            this.stopShowingAdobeWarningLink.AutoSize = true;
            this.stopShowingAdobeWarningLink.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopShowingAdobeWarningLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.stopShowingAdobeWarningLink.LinkColor = System.Drawing.Color.Teal;
            this.stopShowingAdobeWarningLink.Location = new System.Drawing.Point(3, 36);
            this.stopShowingAdobeWarningLink.Name = "stopShowingAdobeWarningLink";
            this.stopShowingAdobeWarningLink.Size = new System.Drawing.Size(144, 16);
            this.stopShowingAdobeWarningLink.TabIndex = 1;
            this.stopShowingAdobeWarningLink.TabStop = true;
            this.stopShowingAdobeWarningLink.Text = "Stop showing this message.";
            this.stopShowingAdobeWarningLink.Visible = false;
            this.stopShowingAdobeWarningLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StopShowingThisMessageClicked);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.referencesTreeView, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.referencesLabel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cqpLabel2, 0, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(325, 511);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // referencesTreeView
            // 
            this.referencesTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.referencesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.referencesTreeView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.referencesTreeView.Location = new System.Drawing.Point(3, 114);
            this.referencesTreeView.Name = "referencesTreeView";
            this.referencesTreeView.Size = new System.Drawing.Size(319, 374);
            this.referencesTreeView.TabIndex = 1;
            this.referencesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.referencesTreeView_AfterSelect);
            this.referencesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeNodeDoubleClick);
            // 
            // referencesLabel
            // 
            this.referencesLabel.AutoSize = true;
            this.referencesLabel.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.referencesLabel.Location = new System.Drawing.Point(3, 0);
            this.referencesLabel.Name = "referencesLabel";
            this.referencesLabel.Size = new System.Drawing.Size(101, 24);
            this.referencesLabel.TabIndex = 0;
            this.referencesLabel.Text = "References";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 76);
            this.panel1.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.Controls.Add(this.cqpSmallPillButton1, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.cqpSearchBox1, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.cqpLabel1, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(319, 76);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // cqpSmallPillButton1
            // 
            this.cqpSmallPillButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cqpSmallPillButton1.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources.smpill_up;
            this.cqpSmallPillButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cqpSmallPillButton1.FlatAppearance.BorderSize = 0;
            this.cqpSmallPillButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cqpSmallPillButton1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpSmallPillButton1.Location = new System.Drawing.Point(254, 55);
            this.cqpSmallPillButton1.Name = "cqpSmallPillButton1";
            this.cqpSmallPillButton1.Size = new System.Drawing.Size(52, 18);
            this.cqpSmallPillButton1.TabIndex = 2;
            this.cqpSmallPillButton1.Text = "Clear";
            this.cqpSmallPillButton1.UseVisualStyleBackColor = false;
            this.cqpSmallPillButton1.Click += new System.EventHandler(this.cqpSmallPillButton1_Click);
            // 
            // cqpSearchBox1
            // 
            this.cqpSearchBox1.BackColor = System.Drawing.SystemColors.Window;
            this.cqpSearchBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cqpSearchBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cqpSearchBox1.Location = new System.Drawing.Point(13, 29);
            this.cqpSearchBox1.Name = "cqpSearchBox1";
            this.cqpSearchBox1.Size = new System.Drawing.Size(293, 20);
            this.cqpSearchBox1.TabIndex = 1;
            this.cqpSearchBox1.TextChanged += new System.EventHandler(this.SearchTextboxChanged);
            // 
            // cqpLabel1
            // 
            this.cqpLabel1.AutoSize = true;
            this.cqpLabel1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpLabel1.Location = new System.Drawing.Point(13, 10);
            this.cqpLabel1.Name = "cqpLabel1";
            this.cqpLabel1.Size = new System.Drawing.Size(42, 16);
            this.cqpLabel1.TabIndex = 0;
            this.cqpLabel1.Text = "Search";
            // 
            // cqpLabel2
            // 
            this.cqpLabel2.AutoSize = true;
            this.cqpLabel2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpLabel2.Location = new System.Drawing.Point(3, 491);
            this.cqpLabel2.Name = "cqpLabel2";
            this.cqpLabel2.Size = new System.Drawing.Size(254, 16);
            this.cqpLabel2.TabIndex = 2;
            this.cqpLabel2.Text = "Double-click a selection to preview the document";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.adobePanel, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(388, 511);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // adobePanel
            // 
            this.adobePanel.Controls.Add(this.webBrowser1);
            this.adobePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adobePanel.Location = new System.Drawing.Point(3, 3);
            this.adobePanel.Name = "adobePanel";
            this.adobePanel.Size = new System.Drawing.Size(382, 505);
            this.adobePanel.TabIndex = 0;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(382, 505);
            this.webBrowser1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(789, 595);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(13, 13);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel5);
            this.splitContainer1.Size = new System.Drawing.Size(763, 511);
            this.splitContainer1.SplitterDistance = 325;
            this.splitContainer1.SplitterWidth = 50;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Paint);
            // 
            // ReferencesTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ReferencesTabPage";
            this.Size = new System.Drawing.Size(789, 595);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.adobePanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label warningLabel;
        private CQPPrintButton cqpPrintButton1 = new CQPPrintButton();
        private CQPGrayRectangleButton cqpGrayRectangleButton1 = new CQPGrayRectangleButton();
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TreeView referencesTreeView;
        private CQPLabel referencesLabel = new CQPLabel();
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private CQPSmallPillButton cqpSmallPillButton1 = new CQPSmallPillButton();
        private CQPSearchBox cqpSearchBox1 = new CQPSearchBox();
        private CQPLabel cqpLabel1 = new CQPLabel();
        private CQPLabel cqpLabel2 = new CQPLabel();
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel adobePanel;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CQPLinkLabel stopShowingAdobeWarningLink = new CQPLinkLabel();
        private System.Windows.Forms.SplitContainer splitContainer1;

        //private CQPSearchBox cqpSearchBox1;
        //private CQPTextButton clearButton;
        //private CQPLabel searchLabel;
        //private CQPLabel cqpLabel1;
    }
}
