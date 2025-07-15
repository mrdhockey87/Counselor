namespace CounselQuickPlatinum
{
    partial class CounselingTab
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.documentDescriptionLabel = new System.Windows.Forms.Label();
            this.documentNameLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.printCheckListButton = new CounselQuickPlatinum.CQPPrintButton();
            this.composeCounselingButton = new CounselQuickPlatinum.CQPGreenButton();
            this.generalTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.eventSpecificTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.extractTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 476);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(13, 13);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(784, 450);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.SplitterWidth = 50;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.splitContainer1_SplitterMoving);
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Paint);
            this.splitContainer1.Move += new System.EventHandler(this.splitContainer1_Move);
            this.splitContainer1.Resize += new System.EventHandler(this.splitContainer1_Resize);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.treeView1, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(260, 450);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 133);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(254, 314);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.Validating += new System.ComponentModel.CancelEventHandler(this.counselingsTreeView_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Counselings";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.documentDescriptionLabel, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.documentNameLabel, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(254, 94);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // documentDescriptionLabel
            // 
            this.documentDescriptionLabel.AutoSize = true;
            this.documentDescriptionLabel.Location = new System.Drawing.Point(3, 32);
            this.documentDescriptionLabel.Name = "documentDescriptionLabel";
            this.documentDescriptionLabel.Size = new System.Drawing.Size(11, 16);
            this.documentDescriptionLabel.TabIndex = 1;
            this.documentDescriptionLabel.Text = " ";
            // 
            // documentNameLabel
            // 
            this.documentNameLabel.AutoSize = true;
            this.documentNameLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentNameLabel.Location = new System.Drawing.Point(3, 0);
            this.documentNameLabel.Name = "documentNameLabel";
            this.documentNameLabel.Size = new System.Drawing.Size(15, 22);
            this.documentNameLabel.TabIndex = 0;
            this.documentNameLabel.Text = " ";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(474, 450);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.printCheckListButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.composeCounselingButton, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 411);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(468, 36);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Counseling Checklists";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.generalTabButton, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.eventSpecificTabButton, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.extractTabButton, 2, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(315, 29);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 61);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 337);
            this.panel1.TabIndex = 4;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(472, 335);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // printCheckListButton
            // 
            this.printCheckListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.printCheckListButton.AutoSize = true;
            this.printCheckListButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.printCheckListButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.printCheckListButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.printCheckListButton.FlatAppearance.BorderSize = 0;
            this.printCheckListButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.printCheckListButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.printCheckListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printCheckListButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printCheckListButton.Image = global::CounselQuickPlatinum.Properties.Resources.print_up;
            this.printCheckListButton.Location = new System.Drawing.Point(419, 3);
            this.printCheckListButton.Name = "printCheckListButton";
            this.printCheckListButton.Size = new System.Drawing.Size(46, 30);
            this.printCheckListButton.TabIndex = 1;
            this.printCheckListButton.Text = " ";
            this.printCheckListButton.UseVisualStyleBackColor = true;
            this.printCheckListButton.Click += new System.EventHandler(this.printCheckListButton_Click);
            // 
            // composeCounselingButton
            // 
            this.composeCounselingButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.composeCounselingButton.AutoSize = true;
            this.composeCounselingButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources.new90_up;
            this.composeCounselingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.composeCounselingButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.composeCounselingButton.FlatAppearance.BorderSize = 0;
            this.composeCounselingButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.composeCounselingButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.composeCounselingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.composeCounselingButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.composeCounselingButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.composeCounselingButton.Location = new System.Drawing.Point(3, 5);
            this.composeCounselingButton.Name = "composeCounselingButton";
            this.composeCounselingButton.Size = new System.Drawing.Size(106, 26);
            this.composeCounselingButton.TabIndex = 0;
            this.composeCounselingButton.Text = "New Counseling";
            this.composeCounselingButton.UseVisualStyleBackColor = false;
            this.composeCounselingButton.Click += new System.EventHandler(this.composeCounselingButton_Click);
            // 
            // generalTabButton
            // 
            this.generalTabButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.generalTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.general_down;
            this.generalTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.general_disabled;
            this.generalTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.generalTabButton.FlatAppearance.BorderSize = 0;
            this.generalTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.generalTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.generalTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generalTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generalTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.general_highlight;
            this.generalTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.general_up;
            this.generalTabButton.Location = new System.Drawing.Point(0, 0);
            this.generalTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.generalTabButton.Name = "generalTabButton";
            this.generalTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.general_up;
            this.generalTabButton.Size = new System.Drawing.Size(100, 29);
            this.generalTabButton.TabIndex = 0;
            this.generalTabButton.UseVisualStyleBackColor = false;
            this.generalTabButton.Click += new System.EventHandler(this.generalTabButton_Click);
            // 
            // eventSpecificTabButton
            // 
            this.eventSpecificTabButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.eventSpecificTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.event_down;
            this.eventSpecificTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.event_disabled1;
            this.eventSpecificTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.eventSpecificTabButton.FlatAppearance.BorderSize = 0;
            this.eventSpecificTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.eventSpecificTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.eventSpecificTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eventSpecificTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventSpecificTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.event_highlight;
            this.eventSpecificTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.event_up;
            this.eventSpecificTabButton.Location = new System.Drawing.Point(100, 0);
            this.eventSpecificTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.eventSpecificTabButton.Name = "eventSpecificTabButton";
            this.eventSpecificTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.event_up;
            this.eventSpecificTabButton.Size = new System.Drawing.Size(115, 29);
            this.eventSpecificTabButton.TabIndex = 1;
            this.eventSpecificTabButton.UseVisualStyleBackColor = false;
            this.eventSpecificTabButton.Click += new System.EventHandler(this.eventSpecificTabButton_Click);
            // 
            // extractTabButton
            // 
            this.extractTabButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.extractTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.extract_down;
            this.extractTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.extract_disabled;
            this.extractTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.extractTabButton.FlatAppearance.BorderSize = 0;
            this.extractTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.extractTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.extractTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.extractTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extractTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.extract_highlight;
            this.extractTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.extract_up;
            this.extractTabButton.Location = new System.Drawing.Point(215, 0);
            this.extractTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.extractTabButton.Name = "extractTabButton";
            this.extractTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.extract_up;
            this.extractTabButton.Size = new System.Drawing.Size(100, 29);
            this.extractTabButton.TabIndex = 2;
            this.extractTabButton.UseVisualStyleBackColor = false;
            //Added line to hide the extracts tab mdail 4-7-2022
            this.extractTabButton.Visible = false;
            this.extractTabButton.Click += new System.EventHandler(this.extractTabButton_Click);
            // 
            // CounselingTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(810, 476);
            this.Name = "CounselingTab";
            this.Size = new System.Drawing.Size(810, 476);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        //private System.Windows.Forms.Button composeCounselingButton;
        //private System.Windows.Forms.Button printCheckListButton;
        //private CQPGrayRectangleButton composeCounselingButton;
        private CQPPrintButton printCheckListButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label documentDescriptionLabel;
        private System.Windows.Forms.Label documentNameLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private CQPGraphicsButton generalTabButton;
        private CQPGraphicsButton eventSpecificTabButton;
        private CQPGraphicsButton extractTabButton;
        private CQPGreenButton composeCounselingButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel1;
    }
}
