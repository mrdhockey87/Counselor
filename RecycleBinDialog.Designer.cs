namespace CounselQuickPlatinum
{
    partial class RecycleBinDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecycleBinDialog));
            this.GlobalTable = new System.Windows.Forms.TableLayoutPanel();
            this.TabsTable = new System.Windows.Forms.TableLayoutPanel();
            this.soldiersTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.counselingTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.RecyclingBinTable = new System.Windows.Forms.TableLayoutPanel();
            this.documentsRecyclingPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.restoreSelectedDocumentsButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.permanentlyDeleteSelectedDocumentsButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.emptyDocumentRecycleBinButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.deletedDocumentsDataGridView = new CounselQuickPlatinum.CQPDocumentsDataGridView();
            this.soldierRecyclingBinPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.permanentlyDeleteButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.emptySoldierRecycleBinButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.restoreSelectedSoldiers = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.formattedSoldierTable = new CounselQuickPlatinum.FormattedSoldierTable();
            this.GlobalTable.SuspendLayout();
            this.TabsTable.SuspendLayout();
            this.RecyclingBinTable.SuspendLayout();
            this.documentsRecyclingPanel.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.soldierRecyclingBinPanel.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GlobalTable
            // 
            this.GlobalTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GlobalTable.ColumnCount = 3;
            this.GlobalTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.GlobalTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GlobalTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.GlobalTable.Controls.Add(this.TabsTable, 1, 1);
            this.GlobalTable.Controls.Add(this.RecyclingBinTable, 1, 2);
            this.GlobalTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GlobalTable.Location = new System.Drawing.Point(0, 0);
            this.GlobalTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GlobalTable.Name = "GlobalTable";
            this.GlobalTable.RowCount = 5;
            this.GlobalTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.GlobalTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GlobalTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GlobalTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GlobalTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.GlobalTable.Size = new System.Drawing.Size(664, 512);
            this.GlobalTable.TabIndex = 0;
            // 
            // TabsTable
            // 
            this.TabsTable.AutoSize = true;
            this.TabsTable.ColumnCount = 2;
            this.TabsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TabsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TabsTable.Controls.Add(this.soldiersTabButton, 0, 0);
            this.TabsTable.Controls.Add(this.counselingTabButton, 1, 0);
            this.TabsTable.Location = new System.Drawing.Point(10, 12);
            this.TabsTable.Margin = new System.Windows.Forms.Padding(0);
            this.TabsTable.Name = "TabsTable";
            this.TabsTable.RowCount = 1;
            this.TabsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TabsTable.Size = new System.Drawing.Size(150, 44);
            this.TabsTable.TabIndex = 3;
            // 
            // soldiersTabButton
            // 
            this.soldiersTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.soldiers_down1;
            this.soldiersTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.soldiers_disabled1;
            this.soldiersTabButton.Enabled = false;
            this.soldiersTabButton.FlatAppearance.BorderSize = 0;
            this.soldiersTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.soldiersTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soldiersTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.soldiers_highlight1;
            this.soldiersTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.soldiers_disabled1;
            this.soldiersTabButton.Location = new System.Drawing.Point(0, 0);
            this.soldiersTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.soldiersTabButton.Name = "soldiersTabButton";
            this.soldiersTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.soldiers_up1;
            this.soldiersTabButton.Size = new System.Drawing.Size(75, 44);
            this.soldiersTabButton.TabIndex = 0;
            this.soldiersTabButton.UseVisualStyleBackColor = false;
            this.soldiersTabButton.Click += new System.EventHandler(this.soldiersTabButton_Click);
            // 
            // counselingTabButton
            // 
            this.counselingTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.documents_down;
            this.counselingTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.documents_disabled;
            this.counselingTabButton.FlatAppearance.BorderSize = 0;
            this.counselingTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.counselingTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.counselingTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.documents_highlight;
            this.counselingTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.documents_up;
            this.counselingTabButton.Location = new System.Drawing.Point(75, 0);
            this.counselingTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.counselingTabButton.Name = "counselingTabButton";
            this.counselingTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.documents_up;
            this.counselingTabButton.Size = new System.Drawing.Size(75, 44);
            this.counselingTabButton.TabIndex = 1;
            this.counselingTabButton.UseVisualStyleBackColor = false;
            this.counselingTabButton.Click += new System.EventHandler(this.counselingTabButton_Click);
            // 
            // RecyclingBinTable
            // 
            this.RecyclingBinTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RecyclingBinTable.ColumnCount = 2;
            this.RecyclingBinTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RecyclingBinTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RecyclingBinTable.Controls.Add(this.documentsRecyclingPanel, 1, 0);
            this.RecyclingBinTable.Controls.Add(this.soldierRecyclingBinPanel, 0, 0);
            this.RecyclingBinTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecyclingBinTable.Location = new System.Drawing.Point(10, 56);
            this.RecyclingBinTable.Margin = new System.Windows.Forms.Padding(0, 0, 3, 4);
            this.RecyclingBinTable.Name = "RecyclingBinTable";
            this.RecyclingBinTable.RowCount = 1;
            this.RecyclingBinTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RecyclingBinTable.Size = new System.Drawing.Size(631, 440);
            this.RecyclingBinTable.TabIndex = 4;
            // 
            // documentsRecyclingPanel
            // 
            this.documentsRecyclingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.documentsRecyclingPanel.Controls.Add(this.tableLayoutPanel6);
            this.documentsRecyclingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentsRecyclingPanel.Location = new System.Drawing.Point(315, 0);
            this.documentsRecyclingPanel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 4);
            this.documentsRecyclingPanel.Name = "documentsRecyclingPanel";
            this.documentsRecyclingPanel.Size = new System.Drawing.Size(313, 436);
            this.documentsRecyclingPanel.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.deletedDocumentsDataGridView, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0, 0, 3, 4);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(313, 436);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.restoreSelectedDocumentsButton, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.permanentlyDeleteSelectedDocumentsButton, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.emptyDocumentRecycleBinButton, 4, 2);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 354);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(307, 78);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // restoreSelectedDocumentsButton
            // 
            this.restoreSelectedDocumentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.restoreSelectedDocumentsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.restoreSelectedDocumentsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("restoreSelectedDocumentsButton.BackgroundImage")));
            this.restoreSelectedDocumentsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.restoreSelectedDocumentsButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.restoreSelectedDocumentsButton.FlatAppearance.BorderSize = 0;
            this.restoreSelectedDocumentsButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.restoreSelectedDocumentsButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.restoreSelectedDocumentsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restoreSelectedDocumentsButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restoreSelectedDocumentsButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;
            this.restoreSelectedDocumentsButton.Location = new System.Drawing.Point(13, 4);
            this.restoreSelectedDocumentsButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.restoreSelectedDocumentsButton.Name = "restoreSelectedDocumentsButton";
            this.restoreSelectedDocumentsButton.Size = new System.Drawing.Size(180, 25);
            this.restoreSelectedDocumentsButton.TabIndex = 0;
            this.restoreSelectedDocumentsButton.Text = "Restore Selected Documents";
            this.restoreSelectedDocumentsButton.UseVisualStyleBackColor = true;
            this.restoreSelectedDocumentsButton.Click += new System.EventHandler(this.restoreSelectedDocumentsButton_Click);
            // 
            // permanentlyDeleteSelectedDocumentsButton
            // 
            this.permanentlyDeleteSelectedDocumentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.permanentlyDeleteSelectedDocumentsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.permanentlyDeleteSelectedDocumentsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("permanentlyDeleteSelectedDocumentsButton.BackgroundImage")));
            this.permanentlyDeleteSelectedDocumentsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.permanentlyDeleteSelectedDocumentsButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.permanentlyDeleteSelectedDocumentsButton.FlatAppearance.BorderSize = 0;
            this.permanentlyDeleteSelectedDocumentsButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.permanentlyDeleteSelectedDocumentsButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.permanentlyDeleteSelectedDocumentsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.permanentlyDeleteSelectedDocumentsButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.permanentlyDeleteSelectedDocumentsButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;
            this.permanentlyDeleteSelectedDocumentsButton.Location = new System.Drawing.Point(209, 4);
            this.permanentlyDeleteSelectedDocumentsButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.permanentlyDeleteSelectedDocumentsButton.Name = "permanentlyDeleteSelectedDocumentsButton";
            this.permanentlyDeleteSelectedDocumentsButton.Size = new System.Drawing.Size(180, 25);
            this.permanentlyDeleteSelectedDocumentsButton.TabIndex = 1;
            this.permanentlyDeleteSelectedDocumentsButton.Text = "Permanently Delete Selections";
            this.permanentlyDeleteSelectedDocumentsButton.UseVisualStyleBackColor = true;
            this.permanentlyDeleteSelectedDocumentsButton.Click += new System.EventHandler(this.permanentlyDeleteSelectedDocumentsButton_Click);
            // 
            // emptyDocumentRecycleBinButton
            // 
            this.emptyDocumentRecycleBinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.emptyDocumentRecycleBinButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.emptyDocumentRecycleBinButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("emptyDocumentRecycleBinButton.BackgroundImage")));
            this.emptyDocumentRecycleBinButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.emptyDocumentRecycleBinButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.emptyDocumentRecycleBinButton.FlatAppearance.BorderSize = 0;
            this.emptyDocumentRecycleBinButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.emptyDocumentRecycleBinButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.emptyDocumentRecycleBinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emptyDocumentRecycleBinButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptyDocumentRecycleBinButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;
            this.emptyDocumentRecycleBinButton.Location = new System.Drawing.Point(209, 49);
            this.emptyDocumentRecycleBinButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.emptyDocumentRecycleBinButton.Name = "emptyDocumentRecycleBinButton";
            this.emptyDocumentRecycleBinButton.Size = new System.Drawing.Size(180, 25);
            this.emptyDocumentRecycleBinButton.TabIndex = 2;
            this.emptyDocumentRecycleBinButton.Text = "Empty Document Recycle Bin";
            this.emptyDocumentRecycleBinButton.UseVisualStyleBackColor = true;
            this.emptyDocumentRecycleBinButton.Click += new System.EventHandler(this.emptyDocumentRecycleBinButton_Click);
            // 
            // deletedDocumentsDataGridView
            // 
            this.deletedDocumentsDataGridView.AutoSize = true;
            this.deletedDocumentsDataGridView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deletedDocumentsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deletedDocumentsDataGridView.CheckboxColumnVisible = true;
            this.deletedDocumentsDataGridView.DateColumnVisible = true;
            this.deletedDocumentsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deletedDocumentsDataGridView.DocumentNameColumnVisible = true;
            this.deletedDocumentsDataGridView.FirstNameColumnVisible = true;
            this.deletedDocumentsDataGridView.LastNameColumnVisible = true;
            this.deletedDocumentsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.deletedDocumentsDataGridView.Margin = new System.Windows.Forms.Padding(0, 0, 3, 4);
            this.deletedDocumentsDataGridView.Name = "deletedDocumentsDataGridView";
            this.deletedDocumentsDataGridView.RankColumnVisible = true;
            this.deletedDocumentsDataGridView.Size = new System.Drawing.Size(310, 334);
            this.deletedDocumentsDataGridView.StatusColumnVisible = true;
            this.deletedDocumentsDataGridView.TabIndex = 0;
            this.deletedDocumentsDataGridView.TypeColumnVisible = true;
            // 
            // soldierRecyclingBinPanel
            // 
            this.soldierRecyclingBinPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.soldierRecyclingBinPanel.Controls.Add(this.tableLayoutPanel5);
            this.soldierRecyclingBinPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soldierRecyclingBinPanel.Location = new System.Drawing.Point(0, 0);
            this.soldierRecyclingBinPanel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 4);
            this.soldierRecyclingBinPanel.Name = "soldierRecyclingBinPanel";
            this.soldierRecyclingBinPanel.Size = new System.Drawing.Size(312, 436);
            this.soldierRecyclingBinPanel.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.formattedSoldierTable, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 3, 4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(312, 436);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 186F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.permanentlyDeleteButton, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.emptySoldierRecycleBinButton, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.restoreSelectedSoldiers, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 354);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(306, 78);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // permanentlyDeleteButton
            // 
            this.permanentlyDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.permanentlyDeleteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.permanentlyDeleteButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("permanentlyDeleteButton.BackgroundImage")));
            this.permanentlyDeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.permanentlyDeleteButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.permanentlyDeleteButton.FlatAppearance.BorderSize = 0;
            this.permanentlyDeleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.permanentlyDeleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.permanentlyDeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.permanentlyDeleteButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.permanentlyDeleteButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;
            this.permanentlyDeleteButton.Location = new System.Drawing.Point(209, 4);
            this.permanentlyDeleteButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.permanentlyDeleteButton.Name = "permanentlyDeleteButton";
            this.permanentlyDeleteButton.Size = new System.Drawing.Size(180, 25);
            this.permanentlyDeleteButton.TabIndex = 1;
            this.permanentlyDeleteButton.Text = "Permanently Delete Selections";
            this.permanentlyDeleteButton.UseVisualStyleBackColor = true;
            this.permanentlyDeleteButton.Click += new System.EventHandler(this.permanentlyDeleteButton_Click);
            // 
            // emptySoldierRecycleBinButton
            // 
            this.emptySoldierRecycleBinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.emptySoldierRecycleBinButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.emptySoldierRecycleBinButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("emptySoldierRecycleBinButton.BackgroundImage")));
            this.emptySoldierRecycleBinButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.emptySoldierRecycleBinButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.emptySoldierRecycleBinButton.FlatAppearance.BorderSize = 0;
            this.emptySoldierRecycleBinButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.emptySoldierRecycleBinButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.emptySoldierRecycleBinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emptySoldierRecycleBinButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptySoldierRecycleBinButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;
            this.emptySoldierRecycleBinButton.Location = new System.Drawing.Point(209, 49);
            this.emptySoldierRecycleBinButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.emptySoldierRecycleBinButton.Name = "emptySoldierRecycleBinButton";
            this.emptySoldierRecycleBinButton.Size = new System.Drawing.Size(180, 25);
            this.emptySoldierRecycleBinButton.TabIndex = 2;
            this.emptySoldierRecycleBinButton.Text = "Empty Soldier Recycle Bin";
            this.emptySoldierRecycleBinButton.UseVisualStyleBackColor = true;
            this.emptySoldierRecycleBinButton.Click += new System.EventHandler(this.emptySoldierRecycleBinButton_Click);
            // 
            // restoreSelectedSoldiers
            // 
            this.restoreSelectedSoldiers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("restoreSelectedSoldiers.BackgroundImage")));
            this.restoreSelectedSoldiers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.restoreSelectedSoldiers.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.restoreSelectedSoldiers.FlatAppearance.BorderSize = 0;
            this.restoreSelectedSoldiers.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.restoreSelectedSoldiers.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.restoreSelectedSoldiers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restoreSelectedSoldiers.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restoreSelectedSoldiers.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;
            this.restoreSelectedSoldiers.Location = new System.Drawing.Point(13, 3);
            this.restoreSelectedSoldiers.Name = "restoreSelectedSoldiers";
            this.restoreSelectedSoldiers.Size = new System.Drawing.Size(180, 25);
            this.restoreSelectedSoldiers.TabIndex = 3;
            this.restoreSelectedSoldiers.Text = "Restore Selected Soldiers";
            this.restoreSelectedSoldiers.UseVisualStyleBackColor = false;
            this.restoreSelectedSoldiers.Click += new System.EventHandler(this.restoreSelectedSoldiers_Click);
            // 
            // formattedSoldierTable
            // 
            this.formattedSoldierTable.AlwaysShowUnassignedSoldier = false;
            this.formattedSoldierTable.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.formattedSoldierTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.formattedSoldierTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formattedSoldierTable.IsUpdating = false;
            this.formattedSoldierTable.Location = new System.Drawing.Point(0, 0);
            this.formattedSoldierTable.Margin = new System.Windows.Forms.Padding(0, 0, 3, 4);
            this.formattedSoldierTable.Name = "formattedSoldierTable";
            this.formattedSoldierTable.ShowCheckboxes = true;
            this.formattedSoldierTable.ShowDeletedSoldiers = true;
            this.formattedSoldierTable.Size = new System.Drawing.Size(309, 334);
            this.formattedSoldierTable.TabIndex = 0;
            // 
            // RecycleBinDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(664, 512);
            this.Controls.Add(this.GlobalTable);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(638, 550);
            this.Name = "RecycleBinDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recycle Bin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecycleBinDialog_FormClosing);
            this.Load += new System.EventHandler(this.RecycleBinDialog_Load);
            this.GlobalTable.ResumeLayout(false);
            this.GlobalTable.PerformLayout();
            this.TabsTable.ResumeLayout(false);
            this.RecyclingBinTable.ResumeLayout(false);
            this.documentsRecyclingPanel.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.soldierRecyclingBinPanel.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel GlobalTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel TabsTable;
        private CQPGraphicsButton soldiersTabButton;
        private CQPGraphicsButton counselingTabButton;
        private System.Windows.Forms.Panel soldierRecyclingBinPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel documentsRecyclingPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel RecyclingBinTable;
        private CQPDocumentsDataGridView deletedDocumentsDataGridView;
        private FormattedSoldierTable formattedSoldierTable;

        private CQPGrayRectangleButton emptySoldierRecycleBinButton;
        private CQPGrayRectangleButton permanentlyDeleteButton;
        private CQPGrayRectangleButton restoreSelectedDocumentsButton;
        private CQPGrayRectangleButton permanentlyDeleteSelectedDocumentsButton;
        private CQPGrayRectangleButton emptyDocumentRecycleBinButton;
        private CQPGrayRectangleButton restoreSelectedSoldiers;
    }
}