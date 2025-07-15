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
    public partial class FormattedSoldierTable : UserControl
    {
        public delegate void SelectedSoldierIndexChangedHandler(int selectedID);
        public event SelectedSoldierIndexChangedHandler SelectedSoldierIndexChanged;

        public delegate void OnRightClickHandler();
        public event OnRightClickHandler OnRightClick;

        [Browsable(true)]
        public EventHandler SoldierDoubleClicked;

        string filter;
        int selectedSoldierID;
        bool showDeletedSoldiers;
        DataView formattedSoldiersView;
        private bool alwaysShowUnassignedSoldier = true;

        public bool IsUpdating { get; set; }
        
        [Browsable(true)]
        public bool ShowCheckboxes
        {
            get { return checkboxColumn.Visible; }
            set { checkboxColumn.Visible = value; }
        }


        [Description("Whether to show the unassigned soldier"),
        Category("Appearance"),
        //DefaultValue(true),
        Browsable(true)]
        public bool AlwaysShowUnassignedSoldier
        {
            get { return alwaysShowUnassignedSoldier; }
            
            set
            {
                alwaysShowUnassignedSoldier = value;
            }
        }


        public FormattedSoldierTable()
        {
            try
            {
                Load += new EventHandler(FormattedSoldierTable_Load);
                InitializeComponent();
                if (DesignMode == true)
                    return;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong here: Design mode: " + DesignMode.ToString() + "\n" + ex.StackTrace);
            }

        }


        void FormattedSoldierTable_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            SoldierModel.SoldierModelRefreshed = RefreshSoldierDataGridView;
            DocumentModel.DocumentModelRefreshed = OnDocumentModelRefreshed;

            RefreshSoldierDataGridView();
            ResizeColumns();
        }


        private void OnDocumentModelRefreshed()
        {
            RefreshSoldierDataGridView();
        }


        public string Filter
        {
            set
            {
                filter = "(lastname LIKE '*" + value + "*' "
                                + " or firstname LIKE '*" + value + "*' "
                                + " or rankingabbreviation LIKE '*" + value + "*' )"
                                + " and deleted = " + Convert.ToInt16(ShowDeletedSoldiers);

                formattedSoldiersView.RowFilter = filter;

                soldierListDataGridView.DataSource = formattedSoldiersView;
                soldierListDataGridView.Refresh();
            }
        }


        [Browsable(true)]
        public bool ShowDeletedSoldiers
        {
            get { return showDeletedSoldiers; }

            set
            {
                showDeletedSoldiers = value;

                if (!DesignMode)
                    RefreshSoldierDataGridView();
            }
        }


        private void RefreshSoldierDataGridView()
        {
            if (InvokeRequired)
            {
                //Application.DoEvents();
                //this.Invoke((MethodInvoker)delegate
                this.BeginInvoke((MethodInvoker)delegate
                {
                    PerformRefresh();
                });
            }
            else
                PerformRefresh();
        }


        private void PerformRefresh()
        {
            int index = 0;

            if (soldierListDataGridView.SelectedRows != null && soldierListDataGridView.SelectedRows.Count > 0)
                index = soldierListDataGridView.SelectedRows[0].Index;

            this.SuspendDrawing();
            IsUpdating = true;

            DataTable soldiersTable = SoldierModel.FormattedSoldiersTableCopy;

            if (!alwaysShowUnassignedSoldier)
            {
                if (DocumentModel.GetNumDocumentsForSoldierID(-1, false) == 0)
                {
                    soldiersTable.PrimaryKey = new DataColumn[] { soldiersTable.Columns["soldierid"] };
                    DataRow unassignedSoldierRow = soldiersTable.Rows.Find(SoldierModel.UnassignedSoldierID);
                    soldiersTable.Rows.Remove(unassignedSoldierRow);
                }
            }

            formattedSoldiersView = soldiersTable.DefaultView;
            formattedSoldiersView.RowFilter = "deleted = " + Convert.ToInt16(showDeletedSoldiers);

            soldierListDataGridView.DataSource = formattedSoldiersView;
            soldierListDataGridView.Refresh();

            IsUpdating = false;

            if (soldierListDataGridView.Rows.Count > index)
                soldierListDataGridView.CurrentCell = soldierListDataGridView.Rows[index].Cells["rankingabbreviationColumn"];
            else if (soldierListDataGridView.Rows.Count > 0)
                soldierListDataGridView.CurrentCell = soldierListDataGridView.Rows[0].Cells["rankingabbreviationColumn"]; //.Selected = true;
            else
                soldierListDataGridView.CurrentCell = null;

            ForceUpdate();



            this.ResumeDrawing();
        }


        private string GetSelectedSoldierFieldValue(string fieldName)
        {
            if (soldierListDataGridView.CurrentRow == null)
                return "";

            int rowIndex = soldierListDataGridView.CurrentRow.Index;

            string fieldValue = soldierListDataGridView[fieldName, rowIndex].Value.ToString();
            return fieldValue;
        }


        public int SelectedSoldierID
        {
            get
            {
                string stringID = GetSelectedSoldierFieldValue("soldieridColumn");
                
                if (stringID == "")
                    return Int32.MinValue;
                    //return -1;
                
                int soldierID = Convert.ToInt32(stringID);
                return soldierID;
            }
        }


        internal List<int> SelectedSoldierIDs
        {
            get
            {
                List<int> selectedSoldierIDs = new List<int>();

                foreach (DataGridViewRow row in soldierListDataGridView.Rows)
                {
                    DataGridViewCheckBoxCell cell = row.Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                    bool isChecked = Convert.ToBoolean(cell.Value);

                    if( isChecked )
                        selectedSoldierIDs.Add(Convert.ToInt32(row.Cells["soldieridColumn"].Value));
                }

                return selectedSoldierIDs;
            }
        }


        public string FirstName
        {
            get
            {
                string firstname = GetSelectedSoldierFieldValue("firstnameColumn");
                return firstname;
            }
        }


        public string LastName
        {
            get
            {
                string lastname = GetSelectedSoldierFieldValue("lastnameColumn");
                return lastname;
            }
        }


        public string MiddleInitial
        {
            get
            {
                string middleInitial = GetSelectedSoldierFieldValue("middleinitial");
                return middleInitial;
            }
        }


        public string RankingAbbreviation
        {
            get
            {
                string rankingabbr = GetSelectedSoldierFieldValue("rankingabbreviationColumn");
                return rankingabbr;
            }
        }


        public DateTime SoldierDateOfRank
        {
            get
            {
                string dateOfRank = GetSelectedSoldierFieldValue("dateofrankColumn");
                if (dateOfRank == "")
                    return new DateTime();

                DateTime dt = new DateTime(Convert.ToInt64(dateOfRank));
                return dt;
            }
        }


        public DateTime SoldierDateOfBirth
        {
            get
            {
                string dateOfBirth = GetSelectedSoldierFieldValue("dateofbirthColumn");
                if (dateOfBirth == "")
                    return new DateTime();

                DateTime dt = new DateTime( Convert.ToInt64(dateOfBirth));
                return dt;
            }
        }


        public string SoldierImagePath
        {
            get
            {
                string imagefilepath = GetSelectedSoldierFieldValue("imagefilepathColumn");
                return imagefilepath;
            }
        }


        //public string SoldierRankingImagePath
        //{
        //    get
        //    {
        //        System.Diagnostics.StackFrame frame = new System.Diagnostics.StackFrame(0);
        //        CQPMessageBox.Show("DEPRICATED CALL: " + frame.GetMethod().Name );

        //        string rankingImagePath = SettingsModel.RankingImageDirectory + GetSelectedSoldierFieldValue("rankingimagepathColumn");
        //        return rankingImagePath;
        //    }
        //}


        public string SoldierRankingImage
        {
            get
            {
                string rankingImagePath = /*SettingsModel.RankingImageDirectory +*/ GetSelectedSoldierFieldValue("rankingimagepathColumn");
                return rankingImagePath;
            }
        }


        internal DataGridViewRowCollection Rows 
        {
            get
            {
                return soldierListDataGridView.Rows;
            }
        }

        private void soldierListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ForceUpdate();

            if (e.RowIndex == -1)
                return;

            if (checkboxColumn.Visible == true)
            {
                bool value;

                if (soldierListDataGridView.CurrentRow.Cells["checkboxColumn"].Value == null)
                    value = false;
                else
                    value = (bool)soldierListDataGridView.CurrentRow.Cells["checkboxColumn"].Value;

                soldierListDataGridView.CurrentRow.Cells["checkboxColumn"].Value = !value;
            }

            if (SoldierDoubleClicked != null)
                SoldierDoubleClicked(sender, e);
        }

        //private void soldierListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex == -1)
        //        return;

        //    if (selectedSoldierID == -1)
        //        return;

        //    Soldier soldier = new Soldier(selectedSoldierID);
        //    SoldierInfoDialog infoDialog = new SoldierInfoDialog(soldier);
        //    infoDialog.ShowDialog(this);
        //}


        private void soldierListDataGridView_Sorted(object sender, EventArgs e)
        {
            IsUpdating = false;

            if (soldierListDataGridView.SelectedCells.Count == 0)
                return;

            ForceUpdate();
        }

        private void ForceUpdate()
        {
            if (IsUpdating)
                return;

            if (soldierListDataGridView.CurrentRow == null)
            {
                selectedSoldierID = Int32.MinValue;
//                return;
            }
            else
            {
                int rowIndex = soldierListDataGridView.CurrentRow.Index;
                selectedSoldierID = Convert.ToInt32(soldierListDataGridView["soldieridColumn", rowIndex].Value);       
            }
            
            if (SelectedSoldierIndexChanged != null)
                SelectedSoldierIndexChanged(selectedSoldierID);
        }


        //private void soldierListDataGridView_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (IsUpdating)
        //        return;

        //    if (soldierListDataGridView.CurrentRow == null)
        //    {
        //        IsUpdating = true;
        //        return;
        //    }

        //    int currentRowID = soldierListDataGridView.CurrentRow.Index;

        //    // if this was a sort operation...
        //    if (currentRowID == -1)
        //    {
        //        IsUpdating = true;
        //        return;
        //    }
        //    else
        //    {
        //        //if (IsUpdating)
        //        //    return;

        //        if (soldierListDataGridView.Rows.Count == 0)
        //            selectedSoldierID = -1;

        //        if (soldierListDataGridView.CurrentRow == null)
        //        {
        //            selectedSoldierID = -1;
        //        }
        //        else
        //        {
        //            int rowIndex = soldierListDataGridView.CurrentRow.Index;
        //            selectedSoldierID = Convert.ToInt32(soldierListDataGridView["soldieridColumn", rowIndex].Value);
        //        }

        //        if (SelectedSoldierIndexChanged != null)
        //            SelectedSoldierIndexChanged(selectedSoldierID);
        //    }
        //}



        private void soldierListDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            int c = soldierListDataGridView.HitTest(x, y).ColumnIndex;
            int r = soldierListDataGridView.HitTest(x, y).RowIndex;

            IsUpdating = true;

            //if (r != -1 && c != -1)
            if (c == -1)
                return;

            DataGridViewColumn clickedColumn = soldierListDataGridView.Columns[c];

            if (r == -1 && clickedColumn.Name == "rankingabbreviationColumn")
            {
                ClearAllOtherSortGlyphs(clickedColumn);

                SortOrder sortOrder = clickedColumn.HeaderCell.SortGlyphDirection;
                SortByRank(sortOrder);
                UpdateHeaderCellSortGlyph(clickedColumn.HeaderCell, sortOrder);
                soldierListDataGridView_Sorted(sender, e);
                return;
            }
            else if (r == -1)
                return;

            
            soldierListDataGridView.CurrentCell = soldierListDataGridView.Rows[r].Cells[c];

            IsUpdating = false;

            ForceUpdate();

            if (e.Button == MouseButtons.Left)
                return;

            if (OnRightClick != null)
                OnRightClick();
        }


        private void SortByRank(SortOrder sortOrder)
        {
            string sortString;
            if (sortOrder == SortOrder.None)
                sortString = "rankingid asc";
            else if (sortOrder == SortOrder.Ascending)
                sortString = "rankingid asc";
            else
                sortString = "rankingid desc";

            formattedSoldiersView.Sort = sortString;
        }


        private void ClearAllOtherSortGlyphs(DataGridViewColumn column)
        {
            foreach (DataGridViewColumn otherColumn in soldierListDataGridView.Columns)
            {
                if (otherColumn.Name == column.Name)
                    continue;

                otherColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }


        private void UpdateHeaderCellSortGlyph(DataGridViewColumnHeaderCell headerCell, SortOrder sortOrder)
        {
            SortOrder newSortOrder = SortOrder.Ascending;

            if (sortOrder == SortOrder.None)
                newSortOrder = SortOrder.Ascending;
            else if (sortOrder == SortOrder.Ascending)
                newSortOrder = SortOrder.Descending;
            else if (sortOrder == SortOrder.Descending)
                newSortOrder = SortOrder.Ascending;

            headerCell.SortGlyphDirection = newSortOrder;
        }


        private void ResizeColumns()
        {
            List<int> widths = new List<int>();

            //this..Width = soldierListDataGridView.Width / 2;

            List<DataGridViewColumn> columns = GetVisibleColumnList();

            //for (int i = 0; i < columns.Count; i++)
            foreach(DataGridViewColumn column in columns)
            {
                //if (i == columns.Count - 1)
                if(column == columns.Last())
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                else
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                widths.Add(column.Width);
            }


            for (int i = 0; i < columns.Count; i++)
            {
                //soldierListDataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                columns[i].Width = widths[i];
                //columns[i].Width = Math.Max(widths[i], ((ClientRectangle.Width-10) / columns.Count+1));
            }

            columns.Last().AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private List<DataGridViewColumn> GetVisibleColumnList()
        {
            return soldierListDataGridView.Columns.Cast<DataGridViewColumn>().Where(column => column.Visible == true).OrderBy(column => column.DisplayIndex).ToList();
        }

        private void soldierListDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            ForceUpdate();
        }

        private void soldierListDataGridView_DragLeave(object sender, EventArgs e)
        {
        }

        private void soldierListDataGridView_DragEnter(object sender, DragEventArgs e)
        {
        }

        //int rowIndexFromMouseDown;
        //int rowIndexOfItemUnderMouseToDrop;
        //Rectangle dragBoxFromMouseDown;

        private void soldierListDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            //rowIndexFromMouseDown = soldierListDataGridView.HitTest(e.X, e.Y).RowIndex;
            //if (rowIndexFromMouseDown != -1)
            //{
            //    // Remember the point where the mouse down occurred. 
            //    // The DragSize indicates the size that the mouse can move 
            //    // before a drag event should be started.                
            //    Size dragSize = SystemInformation.DragSize;

            //    // Create a rectangle using the DragSize, with the mouse position being
            //    // at the center of the rectangle.
            //    dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
            //                                                   e.Y - (dragSize.Height / 2)),
            //                                            dragSize);
            //}
            //else
            //    // Reset the rectangle if the mouse is not over an item in the ListBox.
            //    dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void soldierListDataGridView_DragOver(object sender, DragEventArgs e)
        {
            //if(e.KeyState == 
            //e.Effect = DragDropEffects.Move;
        }


        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            //// The mouse locations are relative to the screen, so they must be 
            //// converted to client coordinates.
            //Point clientPoint = soldierListDataGridView.PointToClient(new Point(e.X, e.Y));

            //// Get the row index of the item the mouse is below. 
            //rowIndexOfItemUnderMouseToDrop =
            //    soldierListDataGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            //// If the drag operation was a move then remove and insert the row.
            //if (e.Effect == DragDropEffects.Move)
            //{
            //    //DataGridViewRow rowToMove = e.Data.GetData(
            //    //        typeof(DataGridViewRow)) as DataGridViewRow;
            //    //soldierListDataGridView.Rows.RemoveAt(rowIndexFromMouseDown);
            //    //soldierListDataGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

            //}
        }


        private void soldierListDataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            //if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            //{
            //    // If the mouse moves outside the rectangle, start the drag.
            //    if (dragBoxFromMouseDown != Rectangle.Empty &&
            //        !dragBoxFromMouseDown.Contains(e.X, e.Y))
            //    {

            //        // Proceed with the drag and drop, passing in the list item.                    
            //        DragDropEffects dropEffect = soldierListDataGridView.DoDragDrop(
            //            soldierListDataGridView.Rows[rowIndexFromMouseDown],
            //            DragDropEffects.Move);
            //    }
            //}
        }
    }
}
