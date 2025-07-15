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
    public partial class CQPDocumentsDataGridView : UserControl
    {
        public DataGridView DataGridView { get { return documentsDataGridView; } }
        private bool IsUpdating { get; set; }
        DocumentSortMode sortMode;
        int selectedDocumentID;

        public delegate void SelectedDocumentChangedHandler(int selectedID);
        public event SelectedDocumentChangedHandler selectedDocumentChanged;

        public delegate void CellDoubleClickedHandler();
        public event CellDoubleClickedHandler cellDoubleClicked;

        public delegate void CellRightClickHandler();
        public event CellRightClickHandler cellRightClicked;


        public CQPDocumentsDataGridView()
        {
            Logger.Trace("CQPDocumentsDataGridView - Constructor");

            InitializeComponent();

            documentsDataGridView.AutoGenerateColumns = false;

            checkboxColumn.DisplayIndex = 1;
            dateColumn.DisplayIndex = 2;
            rankingabbreviationColumn.DisplayIndex = 3;
            lastnameColumn.DisplayIndex = 4;
            firstnameColumn.DisplayIndex = 5;
            typeColumn.DisplayIndex = 6;
            nameColumn.DisplayIndex = 7;
            statusColumn.DisplayIndex = 8;

            Load += new EventHandler(CQPDocumentsDataGridView_Load);
        }


        private void RefreshDocumentDataGridView()
        {
            Logger.Trace("CQPDocumentsDataGridView - RefreshDocumentDataGridView");

            if (this.InvokeRequired)
            {

                Logger.Trace("    CQPDocumentsDataGridView - InvokeRequired");

                this.BeginInvoke((MethodInvoker)delegate
                {
                    PerformDocumentDatagridViewRefresh();
                });
            }
            else
            {
                Logger.Trace("    CQPDocumentsDataGridView - Direct call");
                PerformDocumentDatagridViewRefresh();
            }
        }


        private void PerformDocumentDatagridViewRefresh()
        {
            Logger.Trace("CQPDocumentsDataGridView - PerformDocumentDataGridViewRefresh");

            try
            {
                IsUpdating = true;
                selectedDocumentID = -1;

                string oldFilter = "";

                if (documentsDataGridView.DataSource != null)
                    oldFilter = ((DataView)documentsDataGridView.DataSource).RowFilter;

                DataView view = DocumentModel.GetUserGeneratedDocumentView(sortMode);
                view.RowFilter = oldFilter;
                documentsDataGridView.DataSource = view;

                IsUpdating = false;
            }
            catch (Exception ex)
            {
                string error = "Error refreshing the document datagridview.";                
                Logger.Error(error, ex);
            }
        }


        [Description("What to call when the selected document changes."),
        Category("Appearance"),
        //DefaultValue(true),
        Browsable(true)]
        public SelectedDocumentChangedHandler SelectedDocumentChanged
        {
            set
            {
                selectedDocumentChanged += value;
            }
        }


        public string Filter
        {
            set
            {
                if (DesignMode)
                    return;

                Logger.Trace("CQPDocumentsDataGridView - Filter: " + value);

                if (documentsDataGridView.DataSource == null || (documentsDataGridView.DataSource is DataView) == false)
                {
                    //RefreshDocumentDataGridView();
                    return;
                }

                DataView view = (DataView)documentsDataGridView.DataSource;
                view.RowFilter = value;

                //((DataView)documentsDataGridView.DataSource).RowFilter = value;
                //((DataView)documentsDataGridView.DataSource).RowFilter = "asdflkjadsflkjafd";
                
                ForceUpdate();
            }
        }


        public int SelectedDocumentID
        {
            get
            {
                if (DesignMode)
                    return -1;

                if (documentsDataGridView.CurrentRow == null)
                    return -1;

                int rowIndex = documentsDataGridView.CurrentRow.Index;

                string documentIDString
                    = documentsDataGridView.Rows[rowIndex].Cells["generateddocidColumn"].Value.ToString();

                Int32 documentID = -1;
                //Check to make sure the returned string is an int, if not the docunemtId will keep -1 and thats what will get returned
                //Also if the Convert throws any type of error the documentId gets set to -1 and returned mdail 8-27-19
                if (int.TryParse(documentIDString, out int n)){
                    try
                    {
                        documentID = Convert.ToInt32(documentIDString);
                    }
                    catch (OverflowException)
                    {
                        documentID = -1;
                    }
                    catch (FormatException)
                    {
                        documentID = -1;
                    }
                    catch (Exception)
                    {
                        documentID = -1;
                    }
                }

                return documentID;
            }
        }

        

        [Description("Whether to show the checkbox column"),
        Category("Appearance"),
        //DefaultValue(true),
        Browsable(true)]
        public bool CheckboxColumnVisible
        {
            get
            {
                return checkboxColumn.Visible;
            }
            set
            {
                checkboxColumn.Visible = value;
            }
        }



        [Description("Whether to show the date column"),
        Category("Appearance"),
        //DefaultValue(true),
        Browsable(true)]
        public bool DateColumnVisible
        {
            get
            {
                return dateColumn.Visible;
            }
            set
            {
                dateColumn.Visible = value;
            }
        }


        [Description("Whether to show the rank column"),
        Category("Appearance"),
        //DefaultValue(true),
        Browsable(true)]
        public bool RankColumnVisible
        {
            get
            {
                return rankingabbreviationColumn.Visible;
            }
            set
            {
                rankingabbreviationColumn.Visible = value;
            }
        }


        [Description("Whether to show the lastname column"),
        Category("Appearance"),
        //DefaultValue(true),
        Browsable(true)]
        public bool LastNameColumnVisible
        {
            get
            {
                return lastnameColumn.Visible;
            }
            set
            {
                lastnameColumn.Visible = value;
            }
        }


        [Description("Whether to show the firstname column"),
        Category("Appearance"),
        //DefaultValue(true),
        Browsable(true)]
        public bool FirstNameColumnVisible
        {
            get
            {
                return firstnameColumn.Visible;
            }
            set
            {
                firstnameColumn.Visible = value;
            }
        }


        [Description("Whether to show the document type column"),
        Category("Appearance"),
        //DefaultValue(false),
        Browsable(true)]
        public bool TypeColumnVisible
        {
            get
            {
                return typeColumn.Visible;
            }
            set
            {
                typeColumn.Visible = value;
            }
        }


        [Description("Whether to show the document name column"),
        Category("Appearance"),
        //DefaultValue(false),
        Browsable(true)]
        public bool DocumentNameColumnVisible
        {
            get
            {
                return nameColumn.Visible;
            }
            set
            {
                nameColumn.Visible = value;
            }
        }


        [Description("Whether to show the document name column"),
        Category("Appearance"),
            //DefaultValue(false),
        Browsable(true)]
        public bool StatusColumnVisible
        {
            get
            {
                return statusColumn.Visible;
            }
            set
            {
                statusColumn.Visible = value;
            }
        }
        

        private void CQPDocumentsDataGridView_Load(object sender, EventArgs e)
        {
            try
            {
                if (DesignMode)
                    return;

                //documentsDataGridView.AutoGenerateColumns = false;

                //checkboxColumn.DisplayIndex = 1;
                //dateColumn.DisplayIndex = 2;
                //rankingabbreviationColumn.DisplayIndex = 3;
                //lastnameColumn.DisplayIndex = 4;
                //firstnameColumn.DisplayIndex = 5;
                //typeColumn.DisplayIndex = 6;
                //nameColumn.DisplayIndex = 7;
                //statusColumn.DisplayIndex = 8;

                Logger.Trace("CQPDocumentsDataGridView - Load");

                sortMode = DocumentSortMode.DateDesc;

                DocumentModel.DocumentModelRefreshed = RefreshDocumentDataGridView;
                RefreshDocumentDataGridView();

                SetColumnWidths();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while loading the CQPDataGridView!", ex);
            }
        }

        private void SetColumnWidths()
        {
            Logger.Trace("CQPDocumentsDataGridView - SetColumnWidths");

            List<int> widths = new List<int>();

            List<DataGridViewColumn> columns
                = documentsDataGridView.Columns.Cast<DataGridViewColumn>().Where(column => column.Visible == true).ToList();

            for (int i = 0; i < columns.Count; i++)
            {
                if(columns[i] != nameColumn)
                    columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                else
                    columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


            for (int i = 0; i < columns.Count; i++)
            {
                widths.Add(columns[i].Width);
                Logger.Trace("    CQPDocumentsDataGridView - column width: " + widths[i]);
            }



            for (int i = 0; i < columns.Count - 1; i++)
            {
                //if (columns[i] == nameColumn)
                //    continue;

                columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                columns[i].Width = widths[i];
            }

            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            statusColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //columns.Last().AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void documentsDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Logger.Trace("CQPDocumentsDataGridView - ColumnHeaderClicked");

            if (IsUpdating)
            {
                Logger.Trace("    CQPDocumentsDataGridView - ColumnHeaderClicked - IsUpdating = true, returning");
                return;
            }

            IsUpdating = true;

            int index = e.ColumnIndex;
            DataGridViewColumn column = documentsDataGridView.Columns[index];

            ClearAllOtherSortGlyphs(column);
            SortOrder order = GetSortOrder(column);

            string orderStr = (order == SortOrder.Ascending ? "asc" : "desc");

            sortMode = DocumentSortMode.DateDesc;

            Logger.Trace("    CQPDocumentsDataGridView - ColumnHeaderClicked - sortmode: " + sortMode.ToString());

            if (column.Name == dateColumn.Name)
                sortMode = (order == SortOrder.Ascending ? DocumentSortMode.DateAsc : DocumentSortMode.DateDesc);
            else if (column.Name == nameColumn.Name)
                sortMode = (order == SortOrder.Ascending ? DocumentSortMode.NameAsc : DocumentSortMode.NameDesc);
            else if (column.Name == statusColumn.Name)
                sortMode = (order == SortOrder.Ascending ? DocumentSortMode.StatusAsc : DocumentSortMode.StatusDesc);
            else if (column.Name == typeColumn.Name)
                sortMode = (order == SortOrder.Ascending ? DocumentSortMode.TypeAsc : DocumentSortMode.TypeDesc);
            else if (column.Name == rankingabbreviationColumn.Name)
                sortMode = (order == SortOrder.Ascending ? DocumentSortMode.RankAsc : DocumentSortMode.RankDesc);
            else if (column.Name == lastnameColumn.Name)
                sortMode = (order == SortOrder.Ascending ? DocumentSortMode.SoldierLastNameAsc : DocumentSortMode.SoldierLastNameDesc);
            else if (column.Name == firstnameColumn.Name)
                sortMode = (order == SortOrder.Ascending ? DocumentSortMode.SoldierFirstNameAsc : DocumentSortMode.SoldierFirstNameDesc);

            RefreshDocumentDataGridView();

            column.HeaderCell.SortGlyphDirection = order;
            ForceUpdate();

            //IsUpdating = false;
        }


        private void ForceUpdate()
        {
            Logger.Trace("CQPDocumentsDataGridView - ForceUpdate");

            if (documentsDataGridView.CurrentRow == null)
            {
                Logger.Trace("    CQPDocumentsDataGridView - ForceUpdate: selectedDocID = -1");
                selectedDocumentID = -1;
            }
            else
            {
                int rowIndex = documentsDataGridView.CurrentRow.Index;
                selectedDocumentID = Convert.ToInt32(documentsDataGridView["generateddocidColumn", rowIndex].Value);
                Logger.Trace("    CQPDocumentsDataGridView - ForceUpdate: selectedDocID = " + selectedDocumentID);
            }

            if (selectedDocumentChanged != null)
                selectedDocumentChanged(selectedDocumentID);
        }


        private void ClearAllOtherSortGlyphs(DataGridViewColumn column)
        {
            foreach (DataGridViewColumn otherColumn in documentsDataGridView.Columns)
            {
                if (otherColumn.Name == column.Name)
                    continue;

                otherColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }

        private SortOrder GetSortOrder(DataGridViewColumn column)
        {
            SortOrder order = column.HeaderCell.SortGlyphDirection;
            if (order == SortOrder.None)
                order = SortOrder.Descending;
            else if (order == SortOrder.Ascending)
                order = SortOrder.Descending;
            else if (order == SortOrder.Descending)
                order = SortOrder.Ascending;

            return order;
        }

        private void documentsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (IsUpdating)
            //    return;

            //if (documentsDataGridView.CurrentRow == null)
            //{
            //    IsUpdating = true;
            //    return;
            //}

            //int currentRowID = documentsDataGridView.CurrentRow.Index;

            //// if this was a sort operation...
            //if (currentRowID == -1)
            //{
            //    IsUpdating = true;
            //    return;
            //}

            //else
            //{
            //    //if (IsUpdating)
            //    //    return;

            //    if (documentsDataGridView.Rows.Count == 0)
            //        selectedDocumentID = -1;
            //    else
            //    {
            //        int rowIndex = documentsDataGridView.CurrentRow.Index;
            //        selectedDocumentID = Convert.ToInt32(documentsDataGridView["generateddocidColumn", rowIndex].Value);
            //    }


            //    if (selectedDocumentChanged != null)
            //        selectedDocumentChanged(selectedDocumentID);
            //}
        }

        private void documentsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void documentsDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (IsUpdating)
            //    return;

            ////if (documentsDataGridView.CurrentRow == null)
            ////{
            ////    IsUpdating = true;
            ////    return;
            ////}

            ////int currentRowID = documentsDataGridView.CurrentRow.Index;
            ////int currentRowID;// = documentsDataGridView.CurrentRow.Index;

            //// if this was a sort operation...
            //if(documentsDataGridView.CurrentRow == null)
            ////if (currentRowID == -1)
            //{
            //    selectedDocumentID = -1;
            ////    IsUpdating = true;
            //    return;
            //}

            //else
            //{
            //    //if (IsUpdating)
            //    //    return;

            //    if (documentsDataGridView.Rows.Count == 0)
            //        selectedDocumentID = -1;
            //    else
            //    {
            //        int rowIndex = documentsDataGridView.CurrentRow.Index;
            //        selectedDocumentID = Convert.ToInt32(documentsDataGridView["generateddocidColumn", rowIndex].Value);
            //    }


            //    if (selectedDocumentChanged != null)
            //        selectedDocumentChanged(selectedDocumentID);
            //}
        }

        private void documentsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Logger.Trace("CQPDocumentsDataGridView - CellDoubleClicked");

            if (e.RowIndex == -1)
                return;

            if (cellDoubleClicked != null)
                cellDoubleClicked();
        }

        private void documentsDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex == -1)
            //    return;

            //IsUpdating = true;

            //// we are only worried about right click!
            ////if (e.Button == MouseButtons.Left)
            ////    return;

            //int r = e.RowIndex;
            //int c = e.ColumnIndex;

            //DataGridViewCell clickedCell = documentsDataGridView.Rows[r].Cells[c];
            //documentsDataGridView.CurrentCell = clickedCell;

            //IsUpdating = false;

            //ForceUpdate();


            //if (e.Button == MouseButtons.Left)
            //    return;

            //if (cellRightClicked != null)
            //    cellRightClicked();
        }
        

        internal List<Document> CheckedDocuments 
        {
            get
            {
                List<Document> checkedDocuments = new List<Document>();

                foreach (DataGridViewRow row in documentsDataGridView.Rows)
                {
                    bool isChecked = Convert.ToBoolean(row.Cells["checkboxColumn"].Value);

                    if (!isChecked)
                        continue;

                    int documentID = Convert.ToInt32(row.Cells["generateddocidColumn"].Value);
                    Document document = new Document(documentID);

                    checkedDocuments.Add(document);
                }

                return checkedDocuments;
            }
        }

        internal List<Document> Items 
        {
            get
            {
                List<Document> documents = new List<Document>();

                foreach (DataGridViewRow row in documentsDataGridView.Rows)
                {
                    int documentID = Convert.ToInt32(row.Cells["generateddocidColumn"].Value);
                    Document document = new Document(documentID);

                    documents.Add(document);
                }

                return documents;
            }
        
        }

        private void documentsDataGridView_DataSourceChanged(object sender, EventArgs e)
        {

            
        }

        private void documentsDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            Logger.Trace("CQPDocumentsDataGridView - CellMouseClicked");

            int x = e.X;
            int y = e.Y;

            int c = documentsDataGridView.HitTest(x, y).ColumnIndex;
            int r = documentsDataGridView.HitTest(x, y).RowIndex;

            IsUpdating = true;

            if (r != -1 && c != -1)
                documentsDataGridView.CurrentCell = documentsDataGridView.Rows[r].Cells[c];

            IsUpdating = false;

            ForceUpdate();

            if (e.Button == MouseButtons.Left)
                return;

            if (cellRightClicked != null)
                cellRightClicked();
        }
    }
}
