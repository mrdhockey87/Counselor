namespace CounselQuickPlatinum
{
    partial class SelectSoldierDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectSoldierDialog));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.formattedSoldierTable = new CounselQuickPlatinum.FormattedSoldierTable();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.selectSoldierButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.mainTextLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.mainTextLabel, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(427, 324);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.formattedSoldierTable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(8, 65);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 202);
            this.panel1.TabIndex = 0;
            // 
            // formattedSoldierTable
            // 
            this.formattedSoldierTable.AlwaysShowUnassignedSoldier = true;
            this.formattedSoldierTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.formattedSoldierTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formattedSoldierTable.IsUpdating = false;
            this.formattedSoldierTable.Location = new System.Drawing.Point(0, 0);
            this.formattedSoldierTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.formattedSoldierTable.Name = "formattedSoldierTable";
            this.formattedSoldierTable.ShowCheckboxes = false;
            this.formattedSoldierTable.ShowDeletedSoldiers = false;
            this.formattedSoldierTable.Size = new System.Drawing.Size(411, 202);
            this.formattedSoldierTable.TabIndex = 0;
            this.formattedSoldierTable.SelectedSoldierIndexChanged += new CounselQuickPlatinum.FormattedSoldierTable.SelectedSoldierIndexChangedHandler(this.SelectedSoldierIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.cancelButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.selectSoldierButton, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(222, 281);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(197, 33);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            this.cancelButton.Location = new System.Drawing.Point(104, 4);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 25);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // selectSoldierButton
            // 
            this.selectSoldierButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectSoldierButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("selectSoldierButton.BackgroundImage")));
            this.selectSoldierButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.selectSoldierButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectSoldierButton.Enabled = false;
            this.selectSoldierButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.selectSoldierButton.FlatAppearance.BorderSize = 0;
            this.selectSoldierButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.selectSoldierButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.selectSoldierButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectSoldierButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectSoldierButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            this.selectSoldierButton.Location = new System.Drawing.Point(3, 4);
            this.selectSoldierButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectSoldierButton.Name = "selectSoldierButton";
            this.selectSoldierButton.Size = new System.Drawing.Size(90, 25);
            this.selectSoldierButton.TabIndex = 1;
            this.selectSoldierButton.Text = "Select Soldier";
            this.selectSoldierButton.UseVisualStyleBackColor = true;
            // 
            // mainTextLabel
            // 
            this.mainTextLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTextLabel.Location = new System.Drawing.Point(8, 6);
            this.mainTextLabel.Name = "mainTextLabel";
            this.mainTextLabel.Size = new System.Drawing.Size(411, 49);
            this.mainTextLabel.TabIndex = 0;
            this.mainTextLabel.Text = "Choose a Soldier...";
            // 
            // SelectSoldierDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(427, 324);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SelectSoldierDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Soldier";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectSoldierDialog_FormClosing);
            this.Load += new System.EventHandler(this.SelectSoldierDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CQPGrayRectangleButton cancelButton;
        private CQPGrayRectangleButton selectSoldierButton;
        private System.Windows.Forms.Label mainTextLabel;
        private FormattedSoldierTable formattedSoldierTable;
    }
}