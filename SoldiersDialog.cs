using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CounselQuickPlatinum
{
    public partial class SoldiersDialog : UserControl
    {
        //int selectedSoldierID;
        Soldier selectedSoldier;
        DataTable notesTable;
        Pen splitterPen;


        ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// 
        /// CONSTRUCTORS
        /// 
        ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public SoldiersDialog()
        {
            Logger.Trace("SoldierDialog - constructor");
            Load += new EventHandler(SoldiersDialog_Load);
            InitializeComponent();
        }

        void SoldiersDialog_Load(object sender, EventArgs e)
        {
            Logger.Trace("SoldierDialog - Load");

            try
            {
                if (DesignMode)
                    return;

                selectedSoldier = null;

                NotesModel.NoteModelRefreshed = UpdateNotesTable;
                InitializeNotesTable();

                formattedSoldierTable.SoldierDoubleClicked += delegate(Object s, EventArgs ea) 
                {
                    int id = formattedSoldierTable.SelectedSoldierID;
                    if (id == -1)
                        return;
                    new SoldierInfoDialog(new Soldier(id)).ShowDialog();
                };

                SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170));
                splitterPen = new Pen(brush, 1);

                int selectedSoldierID = formattedSoldierTable.SelectedSoldierID;
                SelectedSoldierIDChanged(selectedSoldierID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                throw new CQPException("An unexpected error has occurred and Counselor needs to close.", ex);
                
                throw ex;
            }
        }
        ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// 
        /// INITIALIZERS
        /// 
        ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void InitializeNotesTable()
        {
            Logger.Trace("SoldierDialog - InitializeNotesTable");
            notesTable = NotesModel.GetNotesTable().Copy();
            notesDataGridView.DataSource = notesTable;
        }
        ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// 
        ///  UPDATERS
        ///
        ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void SelectedSoldierIDChanged(int soldierID)
        //private void SelectedSoldierIDChanged()
        {
            Logger.Trace("SoldierDialog - SelectedSoldierIDChanged - SoldierID: " + soldierID);
            
            if (InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    Logger.Trace("  SoldierDialog - InvokeRequired");
                    PeformSoldierIDChangedUpdate(soldierID);
                });
            }
            else
            {
                Logger.Trace("  SoldierDialog - Invoke Not Required");
                PeformSoldierIDChangedUpdate(soldierID);
            }
        }

        private void PeformSoldierIDChangedUpdate(int soldierID)
        {
            Logger.Trace("SoldierDialog - PerformIDChangedUpdate");
            selectedSoldier = new Soldier(soldierID);

            ToggleSelectedSoldierDialogControls();

            RefreshDialogValues();
            UpdateDocumentsGridViewFilter();
            UpdateNotesTableFilter();
        }


        private void ToggleSelectedSoldierDialogControls()
        {
            Logger.Trace("SoldierDialog - ToggleSelectDialogControls");
            int selectedSoldierID = selectedSoldier.SoldierID;
            bool soldierIsSelected = selectedSoldierID >= -1;
            bool soldierIsNotUnassignedDocuments = (selectedSoldierID != -1);
            bool documentIsSelected = documentsDataGridView.SelectedDocumentID >= -1;
            bool noteIsSelected = notesDataGridView.SelectedRows.Count >= 1;

            Logger.Trace("    SoldierDialog - selectedSoldierID: " + selectedSoldierID);
            Logger.Trace("    SoldierDialog - soldierIsSelected: " + soldierIsSelected.ToString());
            Logger.Trace("    SoldierDialog - soldierIsNotUnassignedDocuments: " + soldierIsNotUnassignedDocuments.ToString());
            Logger.Trace("    SoldierDialog - noteIsSelected: " + noteIsSelected.ToString());
            
            soldierInfoTable.Visible = soldierIsSelected && soldierIsNotUnassignedDocuments;
            
            deleteSoldierButton.Enabled = soldierIsSelected && soldierIsNotUnassignedDocuments;

            counselSoldierButton.Enabled = soldierIsSelected;
            addDocumentationButton.Enabled = soldierIsSelected;

            deleteDocumentButton.Enabled = documentIsSelected;
            editDocButton.Enabled = documentIsSelected;

            addNoteButton.Enabled = soldierIsSelected;
            editNoteButton.Enabled = noteIsSelected;
            deleteNoteButton.Enabled = noteIsSelected;
        }


        private void RefreshDialogValues()
        {
            Logger.Trace("SoldierDialog - RefreshDialogValues");
            UpdateNameValues();
            UpdateAgeLabel();
            UpdateDateOfRankLabel();
            UpdateSoldierImage();
        }

        internal void RefreshSoldierDataGridView()
        {
            
        }

        private void UpdateDocumentsGridViewFilter()
        {
            int selectedSoldierID = selectedSoldier.SoldierID;
            string rowFilter = "soldierid = " + selectedSoldierID + " and deleted = 0 ";
            Logger.Trace("SoldierDialog - UpdateDocumentsDGVFilter: " + rowFilter);
            documentsDataGridView.Filter = rowFilter;
        }        

        private void UpdateNameValues()
        {
            firstNameLabel.Text = formattedSoldierTable.FirstName;
            lastNameLabel.Text = formattedSoldierTable.LastName;
            middleInitialLabel.Text = formattedSoldierTable.MiddleInitial;
            rankingLabel.Text = formattedSoldierTable.RankingAbbreviation;
        }

        private void UpdateSoldierImage()
        {
            if (selectedSoldier == null)
                return;

            if (selectedSoldier.SoldierID <= -1)
                pictureBox1.Image = null;
            else
            {
                Image img;
                using (var bmpTemp = new Bitmap(selectedSoldier.Picture))
                {
                    img = new Bitmap(bmpTemp);
                }
                //pictureBox1.Image = selectedSoldier.Picture;
                pictureBox1.Image = selectedSoldier.Picture;
            }
        }

        private void UpdateAgeLabel()
        {
            DateTime dob = formattedSoldierTable.SoldierDateOfBirth;
            string ageText = "-";
            Logger.Trace("SoldierDialog - UpdateAgeLabel: " + dob.ToString());
            if (dob.Ticks != 0)
            {
                DateTime now = DateTime.Now;

                int age = Utilities.CalculateAge(dob, now);
                ageText = Convert.ToString(age);
            }

            calculatedAgeLabel.Text = ageText;
        }

        private void UpdateDateOfRankLabel()
        {
            DateTime dateOfRankDateTime = formattedSoldierTable.SoldierDateOfRank;
            Logger.Trace("SoldierDialog - UpdateDateOfRankLabel: " + dateOfRankDateTime.ToString());
            if (dateOfRankDateTime.Ticks == 0)
            {
                dateOfRankFormattedLabel.Text = "-";
                return;
            }

            string formattedDateTimeText = dateOfRankDateTime.ToString("yyyy-MM-dd");
            dateOfRankFormattedLabel.Text = formattedDateTimeText;
        }

        private void UpdateNotesTable()
        {
            Logger.Trace("SoldierDialog - UpdateNotesTable");
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    PerformUpdateNotesTable();
                    Logger.Trace("    SoldierDialog - InvokeRequired");
                });
            }
            else
            {
                PerformUpdateNotesTable();
                Logger.Trace("    SoldierDialog - On GUI Thread");
            }
        }
        
        private void PerformUpdateNotesTable()
        {
            Logger.Trace("SoldierDialog - PerformUpdateNotesTable");
            notesTable = NotesModel.GetNotesTable().Copy();
            UpdateNotesTableFilter();
        }
        private void UpdateNotesTableFilter()
        {
            Logger.Trace("SoldierDialog - UpdateNotesTableFilter");
            if (notesTable == null)
                return;

            int selectedSoldierID = selectedSoldier.SoldierID;
            DataView view = notesTable.AsDataView();
            view.RowFilter = "soldierid = " + selectedSoldierID;
            Logger.Trace("    SoldierDialog - Filter: " + selectedSoldierID + "= selectedSoldierID");
            notesDataGridView.DataSource = view;
        }

        private void OnNotesTableBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Logger.Trace("SoldierDialog = OnNotesTableBindingComplete");
            AddNoteTableExpansionButtons();
        }

        private void AddNoteTableExpansionButtons()
        {
            Logger.Trace("SoldierDialog - AddNotesTableExpansionButtons");
            notesDataGridView.CurrentCell = null;
            Logger.Trace("    SoldierDialog - NotesDataGridView Rows: " + notesDataGridView.Rows.Count);
            for (int i = 0; i < notesDataGridView.Rows.Count; i += 2)
            {
                int noteID = Convert.ToInt32(notesDataGridView["noteid", i].Value);
                int nextNoteID = Convert.ToInt32(notesDataGridView["noteid", i + 1].Value);

                int firstEntryIsSubject = Convert.ToInt32(notesDataGridView["issubject", i].Value);

                if (noteID == nextNoteID && firstEntryIsSubject == 1)
                {
                    notesDataGridView["expandButton", i].Value = "[ + ]";
                    notesDataGridView.Rows[i + 1].Visible = false;
                }
                else if (noteID != nextNoteID && firstEntryIsSubject == 1)
                {
                    notesDataGridView["expandButton", i].Value = "[ - ]";
                }
                else if (noteID != nextNoteID && firstEntryIsSubject != 1)
                {
                    notesDataGridView["expandButton", i].Value = "[ - ]";
                }
            }
        }
        ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// 
        ///  EVENTS
        ///
        ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void onClickDocumentsMenuButton(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - onClickDocumentsMenuButton");
            menuButtonContextMenu.Show(Cursor.Position, ToolStripDropDownDirection.BelowLeft);
            
        }

        private void OnAddSoldierButtonClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - OnAddSoldierButtonClicked");
            NewSoldierPage1Dialog newSoldierPageOneDialog = new NewSoldierPage1Dialog();
            DialogResult result = newSoldierPageOneDialog.ShowDialog(this);
        }

        private void OnEditDocButtonClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - OnEditDocButtonClicked");
            EditSelectedDocument();
        }                       

        private void OnSoldierInfoLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.Trace("SoldiersDialog - OnSoldierInfoLinkClicked");
            try
            {
                Logger.Trace("    SoldiersDialog - OnSoldierInfoLinkClicked: Showing Soldier " + selectedSoldier.SoldierID);
                SoldierInfoDialog.ShowDialog(selectedSoldier);
            }
            catch (DataLoadFailedException ex)
            {
                throw ex;
            }
        }

        private void OnCounselSoldierClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - OnCounselSoldierClicked");
            ShowNewDocumentDialog();
        }

        private void ShowNewDocumentDialog()
        {
            if (selectedSoldier == null)
                return;

            int selectedSoldierID = selectedSoldier.SoldierID;

            if (selectedSoldierID < -1)
                return;
            
            int parentDocumentID = GetParentDocumentIDForSelectedDocument();
            Logger.Trace("    SoldiersDialog - OnCounselSoldierClicked:  SelectedSoldierID: " + selectedSoldierID + " ParentDocumentID: " + parentDocumentID);
            UploadUserGeneratedCounselingForm form = new UploadUserGeneratedCounselingForm(selectedSoldierID, parentDocumentID);
            form.ShowDialog();
        }

        private void OnSoldierSearchTextboxTextChanged(object sender, EventArgs e)
        {
            formattedSoldierTable.Filter = soldierSearchTextbox.Text;
        }

        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - ClearButtonClicked");
            soldierSearchTextbox.Text = "";
        }

        private void NoteCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            Logger.Trace("SoldiersDialog - NoteCellClicked");
            NoteSelectedButtonsEnable();  
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            if (rowIndex == -1)
            {
                SortNotesTable(colIndex);
            }
            else if (RowIsNoteSubject(rowIndex) && colIndex == 0)
            {
                ToggleCommentVisible(rowIndex);
            }
        }

        private void SortNotesTable(int colIndex)
        {
            if (colIndex == notesDataGridView.Columns["date"].Index)
            {
                SortOrder order = notesDataGridView.Columns["date"].HeaderCell.SortGlyphDirection;
                ReverseSortGlyph(ref order);
                DocumentSortMode sortMode;
                if (order == SortOrder.Ascending)
                    sortMode = DocumentSortMode.DateAsc;
                else
                    sortMode = DocumentSortMode.DateDesc;

                NotesModel.SetSortMode(sortMode);
                UpdateNotesTable();
                notesDataGridView.Columns["value"].HeaderCell.SortGlyphDirection = SortOrder.None;
                notesDataGridView.Columns["date"].HeaderCell.SortGlyphDirection = order;
            }
            else if (colIndex == notesDataGridView.Columns["value"].Index)
            {
                SortOrder order = notesDataGridView.Columns["value"].HeaderCell.SortGlyphDirection;
                ReverseSortGlyph(ref order);
                DocumentSortMode sortMode;
                if (order == SortOrder.Ascending)
                    sortMode = DocumentSortMode.DateAsc;
                else
                    sortMode = DocumentSortMode.DateDesc;

                NotesModel.SetSortMode(sortMode);                
                UpdateNotesTable();
                notesDataGridView.Columns["value"].HeaderCell.SortGlyphDirection = order;
                notesDataGridView.Columns["date"].HeaderCell.SortGlyphDirection = SortOrder.None;
            }


        }

        private void ReverseSortGlyph(ref SortOrder order)
        {
            if (order == SortOrder.None)
                order = SortOrder.Ascending;
            else if (order == SortOrder.Ascending)
                order = SortOrder.Descending;
            else if (order == SortOrder.Descending)
                order = SortOrder.Ascending;
        }

        private void NoteSelectedButtonsEnable()
        {
            Logger.Trace("SoldiersDialog - NoteSelectedButtonsEnable");
            editNoteButton.Enabled = true;
            deleteNoteButton.Enabled = true;
        }

        private bool RowIsNoteSubject(int rowIndex)
        {
            Logger.Trace("SoldiersDialog - RowIsSubjectHeader");
            int issubjectInt = Convert.ToInt32(notesDataGridView["issubject", rowIndex].Value);
            if (issubjectInt == 1)
                return true;
            else
                return false;
        }

        private void ToggleCommentVisible(int rowIndex)
        {
            Logger.Trace("SoldiersDialog - ToggleCommentVisible: " + rowIndex);
            if (rowIndex + 1 > notesDataGridView.Rows.Count)
            {
                return;
            }

            if (notesDataGridView.Rows[rowIndex + 1].Visible == true)
            {
                notesDataGridView["expandButton", rowIndex].Value = "[ + ]";
                notesDataGridView.Rows[rowIndex + 1].Visible = false;
            }
            else if (notesDataGridView.Rows[rowIndex + 1].Visible == false)
            {
                notesDataGridView["expandButton", rowIndex].Value = "[ - ]";
                notesDataGridView.Rows[rowIndex + 1].Visible = true;
            }
        }

        private void addDocumentationButton_Click(object sender, EventArgs e)
        {
            CQPMessageBox.Show("Not supported yet.");
        }


        private void addNoteButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - AddNoteButtonClicked");
            ShowAddNoteDialog();
        }


        private void editNoteButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - EditNoteButtonCLicked");
            ShowEditNoteDialog();
        }


        private void ShowEditNoteDialog()
        {
            Logger.Trace("SoldiersDialog - ShowEditNoteDialog");
            if (notesDataGridView.SelectedRows == null || notesDataGridView.SelectedRows.Count == 0)
                return;

            Logger.Trace("    SoldiersDialog - ShowEditNoteDialog: SelectedRowsCount: " + notesDataGridView.SelectedRows.Count);
            int rowID = notesDataGridView.SelectedRows[0].Index;
            int noteID = Convert.ToInt32(notesDataGridView["noteid", rowID].Value);
            Logger.Trace("    SoldiersDialog - ShowEditNoteDialog: rowID: " + rowID + " noteID: " + noteID);
            if (selectedSoldier == null)
                return;

            int selectedSoldierID = selectedSoldier.SoldierID;
            EditNoteDialog editNoteDialog = new EditNoteDialog(noteID, selectedSoldierID);
            DialogResult result = editNoteDialog.ShowDialog();
        }


        private void deleteSoldierButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - DeleteSoldierButton");
            DeleteSoldierPrompt();
        }


        private void deleteDocumentButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - DeleteDocumentButton");
            DeleteDocument();
        }


        private void ShowAddNoteDialog()
        {
            if (selectedSoldier == null)
                return;

            int selectedSoldierID = selectedSoldier.SoldierID;
            Logger.Trace("SoldiersDialog - ShowAddNoteDialog: " + selectedSoldierID);
            EditNoteDialog editNoteDialog = new EditNoteDialog(selectedSoldierID);
            DialogResult result = editNoteDialog.ShowDialog();
            Logger.Trace("    SoldiersDialog - ShowAddNoteDialog: Result: " + result.ToString());
            if (result == DialogResult.Cancel)
                return;
        }

        private void RefreshDocumentButtons()
        {
            Logger.Trace("SoldiersDialog - RefreshDocumentButtons");
            int documentID = documentsDataGridView.SelectedDocumentID;
            Logger.Trace("    SoldiersDialog - RefreshDocumentButtons: documentID: " + documentID);
            if (documentID != -1)
            {
                deleteDocumentButton.Enabled = true;
                editDocButton.Enabled = true;
            }
            else
            {
                deleteDocumentButton.Enabled = false;
                editDocButton.Enabled = false;
            }
        }

        private void DeleteDocument()
        {
            int documentID = documentsDataGridView.SelectedDocumentID;
            Logger.Trace("    SoldiersDialog - DeleteDocument: documentID: " + documentID);
            if (documentID == -1)
            {
                Logger.Trace("    SoldiersDialog - DeleteDocument: NoDocument selected");            
                return;
            }
            Logger.Trace("SoldiersDialog - DeleteDocument");
            string confirmCaption = "Delete document?";
            string confirmMessage = "Are you sure you want to delete this document?";
            List<string> confirmButtonsText = new List<string> { "Delete Document", "Cancel" };
            CQPMessageBox.CQPMessageBoxButtons confirmButtons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            CQPMessageBox.CQPMessageBoxIcon confirmIcon = CQPMessageBox.CQPMessageBoxIcon.Question;
            DialogResult result = CQPMessageBox.ShowDialog(confirmMessage, confirmCaption, confirmButtons, confirmButtonsText, confirmIcon);
            Logger.Trace("    SoldiersDialog - DeleteDocument: Result: " + result.ToString());            
            if (result == DialogResult.No)
                return;

            int numChildDocuments = DocumentModel.GetNumberOfChildDocuments(documentID);
            if (numChildDocuments > 0)
            {
                Logger.Trace("    SoldiersDialog - DeleteDocument: NumChildDocuments > 0");
                DocumentReassignmentReason reason = DocumentReassignmentReason.DeleteingDocument;
                DocumentReassignmentMode reassignmentMode = ReassignChildDocumentsDialog.ShowDialog(reason);
                Logger.Trace("    SoldiersDialog - DeleteDocument: ReassignmentMode: " + reassignmentMode.ToString());
                if(reassignmentMode == DocumentReassignmentMode.NewParent)
                {
                    bool changedDocs = ChangeChildDocumentsParent(documentID, SelectNewParentDocumentDialog.SelectNewParentMode.SelectNewFromChildren);
                    Logger.Trace("    SoldiersDialog - DeleteDocument: ChangedDocs: " + changedDocs.ToString());
                    
                    if (!changedDocs)
                        return;
                }
                else if (reassignmentMode == DocumentReassignmentMode.DeleteAllChildren)
                {
                    DocumentModel.RecycleAllChildDocuments(documentID);
                }
                else if (reassignmentMode == DocumentReassignmentMode.DetachAllChildren)
                {
                    DocumentModel.DetachAllChildDocuments(documentID);
                }
                else if (reassignmentMode == DocumentReassignmentMode.Cancel)
                {
                    return;
                }
                DocumentModel.RecycleDocument(documentID);
            }
            else if (numChildDocuments == 0)
            {
                DocumentModel.RecycleDocument(documentID);
            }
        }

        private bool ChangeChildDocumentsParent(int generatedDocumentID, SelectNewParentDocumentDialog.SelectNewParentMode mode)
        {
            Logger.Trace("SoldiersDialog - ChangeChildDocumentsParent");
            Document document = new Document(generatedDocumentID);
            int parentDocumentID = document.ParentDocumentID;
            SelectNewParentDocumentDialog dialog = new SelectNewParentDocumentDialog(document, mode);
            DialogResult result = dialog.ShowDialog();
            Logger.Trace("    SoldiersDialog - ChangeChildDocumentsParent: " + result.ToString());
            if (result == DialogResult.Cancel)
                return false;

            int newParentDocumentID = dialog.NewParentDocumentID;
            List<Document> childDocuments = DocumentModel.GetChildDocuments(document);
            Logger.Trace("    SoldiersDialog - ChangeChildDocumentsParent: NumChildDocuments: " + childDocuments.Count);
            foreach (Document childDocument in childDocuments)
            {
                int documentID = childDocument.GeneratedDocID;
                if (documentID == generatedDocumentID || documentID == newParentDocumentID)
                {
                    continue;
                }
                DocumentModel.SetDocumentParentID(documentID, newParentDocumentID);
            }            
            DocumentModel.SetDocumentParentID(newParentDocumentID, -1);
            return true;
        }

        private void documentsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
        }

        private void ToggleSelectedDocumentButtons()
        {
            Logger.Trace("SoldiersDialog - ToggleSelectedDocumentButtons");
            if(documentsDataGridView.SelectedDocumentID == -1)
            {
                deleteDocumentButton.Enabled = false;
                editDocButton.Enabled = false;
            }
            else
            {
                deleteDocumentButton.Enabled = true;
                editDocButton.Enabled = true;
            }
        }

        private void documentsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void documentsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Logger.Trace("SoldiersDialog - DocumentsDataGridViewRowsAdded");
            ToggleSelectedDocumentButtons();
        }

        private void deleteNoteButton_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - DeleteNoteButtonClicked");
            DeleteNote();
        }

        private void DeleteNote()
        {
            Logger.Trace("SoldiersDialog - DeleteNote");
            if (notesDataGridView.SelectedRows.Count < 1)
            {
                Logger.Trace("    SoldiersDialog - DeleteNote:  No rows selected");
                return;
            }
            string message = "Are you sure you want to delete this note?";
            string caption = "Delete note?";
            CQPMessageBox.CQPMessageBoxButtons buttons = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Question;
            List<string> buttonsText = new List<string> { "Delete Note", "Cancel" };
            DialogResult result = CQPMessageBox.ShowDialog(message, caption, buttons, buttonsText, icon);
            Logger.Trace("    SoldiersDialog - DeleteNote:  Result" + result.ToString());
            int noteID = Convert.ToInt32(notesDataGridView.SelectedRows[0].Cells["noteid"].Value);
            Logger.Trace("    SoldiersDialog - DeleteNote: NoteID: " + noteID);
            if (result == DialogResult.No)
                return;
            else
                NotesModel.DeleteNote(noteID);
        }
        /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// 
        ///  GETTERS
        ///
        /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!        
        private int GetParentDocumentIDForSelectedDocument()
        {
            Logger.Trace("SoldiersDialog - GetParentDocumentIDForSelectedDocument");
            if (documentsDataGridView.SelectedDocumentID == -1)
            {
                Logger.Trace("    SoldiersDialog - SelectedDocumentID == -1");
                return -1;
            }
            int currentDocumentID = documentsDataGridView.SelectedDocumentID;
            Logger.Trace("    SoldiersDialog - currentDocumentID " + currentDocumentID);
            Document document = new Document(currentDocumentID);
            int parentDocumentID = document.ParentDocumentID;
            Logger.Trace("    SoldiersDialog - " + parentDocumentID);
            //all documents attach to a parent, so we either have the parent or a child,
            //always return a root document node, we don't allow parent->child->child
            bool isRootDocument = (parentDocumentID == -1);

            Logger.Trace("    SoldiersDialog - " + isRootDocument.ToString());

            if (isRootDocument)
                return currentDocumentID;
            else
                return parentDocumentID;
        }
        /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        ///
        /// WINDOW POUPS 
        /// 
        /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void EditSelectedDocument()
        {
            Logger.Trace("SoldiersDialog - EditSelectedDocument");
            int selectedDocumentID = documentsDataGridView.SelectedDocumentID;
            if (selectedSoldier == null)
                return;

            int selectedSoldierID = selectedSoldier.SoldierID;
            Logger.Trace("    SoldiersDialog - EditSelectedDocument: selectedDocumentID: " + selectedSoldierID);
            if (selectedDocumentID == -1)
                return;

            Document document = new Document(selectedDocumentID);
            DialogHelper.ShowEditDocumentDialog(document, this);
        }

        private void ShowDocumentProperties()
        {
            Logger.Trace("SoldiersDialog - ShowDocumentProperties");
            int selectedDocumentID = documentsDataGridView.SelectedDocumentID;
            if (selectedSoldier == null)
                return;

            int selectedSoldierID = selectedSoldier.SoldierID;
            Logger.Trace("    SoldiersDialog - selectedDocumentID: " + selectedSoldierID);
            DocumentPropertiesDialog dialog = new DocumentPropertiesDialog(selectedDocumentID);
            DialogResult result = dialog.ShowDialog();
            Logger.Trace("    SoldiersDialog - " + result.ToString());
            if (result == DialogResult.Cancel)
                return;
        }

        private DialogResult PromptForTemplateOrUserGeneratedDoc()
        {
            Logger.Trace("SoldiersDialog - PromptForTemplateOrUserGeneratedDoc");
            UserGeneratedOrTemplateDocDialog dialog = new UserGeneratedOrTemplateDocDialog();
            DialogResult result = dialog.ShowDialog();
            Logger.Trace("    SoldiersDialog - " + result.ToString());
            return result;
        }
        
        private void ShowCounselingDialog(int parentDocumentID)
        {
            Logger.Trace("SoldiersDialog - ShowCounselingDialog");
            Form counselSoldierForm = new Form();            
            counselSoldierForm.AutoSize = true;
            counselSoldierForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            counselSoldierForm.BackColor = Color.FromArgb(227, 227, 235);
            counselSoldierForm.StartPosition = FormStartPosition.CenterParent;
            counselSoldierForm.ShowInTaskbar = false;
            int selectedSoldierID = selectedSoldier.SoldierID;
            CounselingTab counselingTab = new CounselingTab(selectedSoldierID, parentDocumentID);
            counselingTab.Dock = DockStyle.Fill;
            counselSoldierForm.Controls.Add(counselingTab);
            counselSoldierForm.ShowDialog();
        }


        private void PromptForUserGeneratedCounselingDoc(int parentDocumentID)
        {
            Logger.Trace("SoldiersDialog - PromptForUserGeneratedCousnelingDoc: " + parentDocumentID);
            int selectedSoldierID = selectedSoldier.SoldierID;
            UploadUserGeneratedCounselingForm form = new UploadUserGeneratedCounselingForm(selectedSoldierID, parentDocumentID);
            DialogResult result = form.ShowDialog();
        }


        private void AddSoldierPrompt()
        {
            Logger.Trace("SoldiersDialog - AddSoldierPrompt");
            NewSoldierPage1Dialog newSoldierPageOneDialog = new NewSoldierPage1Dialog();
            DialogResult result = newSoldierPageOneDialog.ShowDialog(this);
        }


        private void DeleteSoldierPrompt()
        {
            Logger.Trace("SoldiersDialog - DeleteSoldierPrompt");
            string message = "Are you sure you want to send this Soldier to the recycling bin?\n\n"
                + "All counseling, documents, and notes associated with this Soldier will be sent to the recycling bin as well.\n\n"
                + "You can restore or delete the soldier and documents later via the Recycling Bin.";
            string caption = "Delete Soldier?";
            CQPMessageBox.CQPMessageBoxButtons responses = CQPMessageBox.CQPMessageBoxButtons.YesNo;
            List<string> buttons = new List<string> { "Delete Soldier", "Cancel" };
            CQPMessageBox.CQPMessageBoxIcon icon = CQPMessageBox.CQPMessageBoxIcon.Question;
            DialogResult result = CQPMessageBox.ShowDialog(message, caption, responses, buttons, icon);
            Logger.Trace("    SoldiersDialog - result: " + result.ToString());
            if (result == DialogResult.No)
                return;

            int selectedSoldierID = selectedSoldier.SoldierID;
            SoldierModel.SendSoldierToRecycleBin(selectedSoldierID);
        }

        private void AttachSelectedDocumentToNewDocument()
        {
            int selectedDocumentID = documentsDataGridView.SelectedDocumentID;
            int selectedSoldierID = selectedSoldier.SoldierID;
            Logger.Trace("SoldiersDialog - AttachtSelectedDocumentToNewDocument: " + selectedSoldierID);
            Document document = new Document(selectedDocumentID);
            List<Document> otherDocumentsToUpdate = new List<Document>();
            ReassignDocumentController controller = new ReassignDocumentController(document);
            DialogResult selectNewParentResult = controller.PromptToSelectNewParentDocument(SelectNewParentDocumentDialog.SelectNewParentMode.SelectNewFromParents);
            Logger.Trace("    SoldiersDialog - AttachtSelectedDocumentToNewDocument: " + selectNewParentResult.ToString());
            if (selectNewParentResult == DialogResult.Cancel)
                return;

            int oldParentDocumentID = document.ParentDocumentID;
            document.ParentDocumentID = controller.NewParentDocumentID;
            Logger.Trace("    SoldiersDialog - AttachtSelectedDocumentToNewDocument:  oldparentdocid: " + oldParentDocumentID);
            List<Document> childDocs = DocumentModel.GetChildDocuments(document);
            Logger.Trace("    SoldiersDialog - AttachtSelectedDocumentToNewDocument: childdocsCount" + childDocs.Count);
            if (childDocs.Count() > 0)
            {
                DialogResult result = controller.PromptToMoveChildDocuments(childDocs);
                Logger.Trace("    SoldiersDialog - AttachtSelectedDocumentToNewDocument: childDocsMove result: " + result.ToString());

                if (result == DialogResult.Cancel)
                {
                    document.ParentDocumentID = oldParentDocumentID;
                    otherDocumentsToUpdate.Clear();
                    return;
                }
                else
                {
                    otherDocumentsToUpdate = controller.OtherDocumentsToUpdate;
                }
            }
            document.Save();
            foreach (Document childDocument in otherDocumentsToUpdate) 
                childDocument.Save();
        }


        private void ExportSelectedDocument()
        {
            Logger.Trace("SoldiersDialog - ExportSelectedDocument");
            if (documentsDataGridView.SelectedDocumentID == -1)
            {
                Logger.Trace("    SoldiersDialog - ExportSelectedDocument: selectedocumentid = -1");
                return;
            }

            int selectedDocumentID = documentsDataGridView.SelectedDocumentID;
            Logger.Trace("SoldiersDialog - selecteddocumentid: " + selectedDocumentID);
            try
            {
                Document document = new Document(selectedDocumentID);
                DocumentExportController.ExportDocument(document);
            }
            catch (Exception ex)
            {
                if (ex is FileException)
                    CQPMessageBox.Show("An error occurred attempting to open the template file to generate the document");
                else if (ex is DataLoadFailedException)
                    CQPMessageBox.Show("An error occurred writing the output file.");
                else
                    CQPMessageBox.Show("An unexpected error occurred.");

                Logger.Error(ex.StackTrace);
                Logger.Error(ex);
                
                throw ex;
            }
        }
        /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        ///
        ///  CONTEXT MENU HANDLERS
        ///
        /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void OnClickOpenDocumentContextMenuItem(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - OpenDocumentMenuItemClick");
            EditSelectedDocument();
        }


        private void OnPropertiesContextMenuItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - properties menuitem click");
            ShowDocumentProperties();
        }

        private void OnNewDocumentContextMenuItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - newdocument menuitem click");
            OnCounselSoldierClicked(null, null);
        }

        private void OnAttachToDocumentToolStripItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - attach to document menuitem click");
            AttachSelectedDocumentToNewDocument();
        }

        private void OnDetachDocumentFromParentContextMenuItemClicked(object sender, EventArgs e)
        {
            int selectedDocumentID = documentsDataGridView.SelectedDocumentID;
            Logger.Trace("SoldiersDialog - DetachDocumentFromParent menu item click: " + selectedDocumentID);
            DialogResult result = DialogHelper.ConfirmDetachDocument(selectedDocumentID);
            Logger.Trace("    SoldiersDialog - DetachDocumentFromParent menu item click: " + result.ToString());
            if (result == DialogResult.No)
                return;
            
            DocumentModel.SetDocumentParentID(selectedDocumentID, -1);
        }

        private void OnExportToolStripMenuItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - export menuitem clicked");
            try
            {
                Document document = new Document(documentsDataGridView.SelectedDocumentID);
                if (document.DocumentType == DocumentType.Memo || document.DocumentType == DocumentType.Letter)
                {
                    // if this is a Word Document and Word does not exist,
                    // don't crash the program trying to export...
                    if (WordInterop.WordExists == false)
                        return;
                }
                ExportSelectedDocument();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnDeleteContextMenuItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - Delete menu item clicked");
            DeleteDocument();
        }

        private void SetSelectedDocumentStatus(DocumentStatus status)
        {
            int documentID = documentsDataGridView.SelectedDocumentID;
            Logger.Trace("    SoldiersDialog - SetSelectedDocumentStatus: " + documentID + ", " + status.ToString());
            Document document = new Document(documentID);
            document.Status = status;
            document.Save();
        }

        private void OnVerbalStatusContextMenuItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - VerbalStatus menu item clicked");
            SetSelectedDocumentStatus(DocumentStatus.Verbal);
        }

        private void OnCompleteStatusContextMenuItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - Complete status menu item clicked");
            SetSelectedDocumentStatus(DocumentStatus.Complete);
        }

        private void DraftStatusContextMenuItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - Draft status menu item clicked");
            SetSelectedDocumentStatus(DocumentStatus.Draft);
        }

        private void AssessmentStatusContextMenuItemClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - Asssessment due status menu item clicked");
            SetSelectedDocumentStatus(DocumentStatus.AssessmentDue);
        }

        private void PendingStatusContextMenuItemsClicked(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - Pending status menu item clicked");
            SetSelectedDocumentStatus(DocumentStatus.Pending);
        }

        private void menuButtonContextMenu_Opening(object sender, CancelEventArgs e)
        {
            Logger.Trace("SoldiersDialog - menu button context menu opening");
            ToggleContextMenuControls();
        }

        private void ToggleContextMenuControls()
        {
            bool value = false;
            bool isWordDocument = false;
            bool allowExport = false;
            int selectedDocumentID = documentsDataGridView.SelectedDocumentID;
            Logger.Trace("SoldiersDialog - ToggleContextMenuControls: " + selectedDocumentID);
            if (selectedDocumentID == -1)
                value = false;
            else
                value = true;

            if (selectedDocumentID != -1)
            {
                Document doc = new Document(selectedDocumentID);
                isWordDocument = doc.DocumentType == DocumentType.Memo | doc.DocumentType == DocumentType.Letter;
                if (isWordDocument && !WordInterop.WordExists)
                    allowExport = false;
                else
                    allowExport = true;
            }
            openDocumentContextMenuItem.Enabled = value;
            attachToDocumentToolStripItem.Enabled = value;
            setStatusContextMenuItem.Enabled = value;
            detachDocumentFromParentContextMenuItem.Enabled = value;
            exportToolStripMenuItem.Enabled = allowExport;
            deleteContextMenuItem.Enabled = value;
            propertiesContextMenuItem.Enabled = value;
            if (value == true)
            {
                Document document = new Document(selectedDocumentID);
                SetStatusContextMenuItemCheckbox(document);
                ToggleDetachMenuItem(document);
            }
        }

        private void SetStatusContextMenuItemCheckbox(Document document)
        {
            UncheckAllStatusItems();
            DocumentStatus status = document.Status;
            Logger.Trace("SoldiersDialog - SetStatusContextMenuItemCheckbox: " + document.GeneratedDocID + ", " + status.ToString());
            switch (status)
            {
                case(DocumentStatus.AssessmentDue):
                    assessmentStatusContextMenuItem.Checked = true;
                    break;
                case(DocumentStatus.Complete):
                    completeStatusContextMenuItem.Checked = true;
                    break;
                case(DocumentStatus.Draft):
                    draftStatusContextMenuItem.Checked = true;
                    break;
                case(DocumentStatus.Pending):
                    pendingStatusContextMenuItems.Checked = true;
                    break;
                case(DocumentStatus.Verbal):
                    verbalStatusContextMenuItem.Checked = true;
                    break;
            }
        }

        private void UncheckAllStatusItems()
        {
            Logger.Trace("SoldiersDialog - UncheckAllStatusItems");
            assessmentStatusContextMenuItem.Checked = false;
            completeStatusContextMenuItem.Checked = false;
            draftStatusContextMenuItem.Checked = false;
            pendingStatusContextMenuItems.Checked = false;
            verbalStatusContextMenuItem.Checked = false;
        }

        private void ToggleDetachMenuItem(Document document)
        {
            bool isChildDocument = (document.ParentDocumentID != -1);
            Logger.Trace("SoldiersDialog - ToggleDetachMenuItem: " + isChildDocument);
            if (isChildDocument)
                detachDocumentFromParentContextMenuItem.Enabled = true;
            else
                detachDocumentFromParentContextMenuItem.Enabled = false;
        }

        private void documentsDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Logger.Trace("SoldiersDialog - DocumentsDGVColumnHeaderMouseClick: " + e.Button.ToString() + ", " + e.RowIndex + ", " + e.ColumnIndex);
        }

        private void SortDocumentsDataGridViewByName()
        {            
        }

        private void SelectedDocumentChanged(int selectedID)
        {
            Logger.Trace("SoldiersDialog - SelectedDocumentChanged: " + selectedID);
            this.BeginInvoke((MethodInvoker)delegate
            {
                Logger.Trace("    SoldiersDialog - BeginInvoke ToggleSelectedDocumentButtons");
                ToggleSelectedDocumentButtons();

            });
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer1.Invalidate();
        }

        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            splitContainer1.Invalidate();
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

        private void FormattedSoldierTableRightClick()
        {
            Logger.Trace("SoldiersDialog - FormattedSoldierTableRightClick");
                RefreshSoldierContextMenu();
                formattedSoldiersTableContextMenuStrip.Show(Cursor.Position, ToolStripDropDownDirection.BelowRight);
        }

        private void RefreshSoldierContextMenu()
        {
            int selectedSoldierID = selectedSoldier.SoldierID;
            bool enabled = selectedSoldierID == -1 ? false : true;
            Logger.Trace("SoldiersDialog - RefreshSoldierContextMenu: " + selectedSoldierID);
            deleteSoldierToolStripMenuItem.Enabled = enabled;
            editSoldierToolStripMenuItem.Enabled = enabled;
            soldierInfoToolStripMenuItem.Enabled = enabled;
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - AddNoteToolStripMenuItem");
            ShowAddNoteDialog();
        }

        private void addDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - AddDocumentMenuItem");
            OnCounselSoldierClicked(null, null);
        }

        private void soldierInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedSoldierID = selectedSoldier.SoldierID;
            Logger.Trace("SoldiersDialog - SoldierInfoMenuItem: " + selectedSoldierID);
            SoldierInfoDialog dialog = new SoldierInfoDialog(selectedSoldier);
            dialog.ShowDialog();
        }

        private void editSoldierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedSoldierID = selectedSoldier.SoldierID;
            Logger.Trace("SoldiersDialog - EditSoldierMenuItem: " + selectedSoldierID);
            EditSoldierDialog dialog = new EditSoldierDialog(selectedSoldier);
            dialog.ShowDialog();
        }

        private void addSoldierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - AddSoldierMenuItem");
            AddSoldierPrompt();
        }

        private void deleteSoldierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - DeleteSoldierMenuItem");
            DeleteSoldierPrompt();
        }

        private void notesDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - NoteDGVCurrentCellChanged");
            bool enabled = (notesDataGridView.CurrentCell == null ? false : true);
            editNoteButton.Enabled = enabled;
            deleteNoteButton.Enabled = enabled;
        }

        private void documentsDataGridView_cellDoubleClicked()
        {
            Logger.Trace("SoldiersDialog - DocumentsDGV CellDoubleClicked");
            EditSelectedDocument();
        }

        private void documentsDataGridView_cellRightClicked()
        {
            Logger.Trace("SoldiersDialog - DocumentsDGV CellRightClicked");
            RefreshDocumentContextMenu();
            documentsTableContextMenu.Show(Cursor.Position, ToolStripDropDownDirection.BelowLeft);
        }

        private void RefreshDocumentContextMenu()
        {
            bool enabled = (documentsDataGridView.SelectedDocumentID != -1);
            Logger.Trace("SoldiersDialog - RefreshDocumentContextMenu: " + enabled.ToString());
            editDocumentToolstripMenuItem.Enabled = enabled;
            deleteDocumentToolstripMenuItem.Enabled = enabled;
            documentPropertiesToolstripMenuItem.Enabled = enabled;
        }

        private void newDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - NewDocumentMenuItem");
            OnCounselSoldierClicked(null, null);
        }

        private void editDocumentToolstripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - EditDocumentMenuItem");
            EditSelectedDocument();
        }

        private void deleteDocumentToolstripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - DeleteDocumentMenuItem");
            DeleteDocument();
        }

        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - EditNoteMenuItem");
            ShowEditNoteDialog();
        }

        private void deleteNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - DeleteNoetMenuItem");
            DeleteNote();
        }

        private void notesDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            // this is JUST to handle popping up the context menu!
            int x = e.X;
            int y = e.Y;
            int c = notesDataGridView.HitTest(x, y).ColumnIndex;
            int r = notesDataGridView.HitTest(x, y).RowIndex;
            Logger.Trace("SoldiersDialog - NotesDGVMouseClick: " + e.Button.ToString() + ", " + r + ", " + c);
            if (r != -1 && c != -1)
                notesDataGridView.CurrentCell = notesDataGridView.Rows[r].Cells[c];
            
            if (e.Button == MouseButtons.Left)
                return;

            bool enabled = (notesDataGridView.CurrentCell == null ? false : true);
            editNoteToolStripMenuItem.Enabled = enabled;
            deleteNoteToolStripMenuItem.Enabled = enabled;
            notesTableContextMenu.Show(Cursor.Position, ToolStripDropDownDirection.AboveLeft);
        }

        private void documentPropertiesToolstripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - documentPropertiesMenuItem");
            ShowDocumentProperties();
        }

        private void newNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace("SoldiersDialog - NewNoteMenuItem");
            ShowAddNoteDialog();
        }

        private void notesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Logger.Trace("SoldiersDialog - NotesDGVDoubleClick: " + e.RowIndex);
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex == 0)
                return;

            Logger.Trace("   SoldiersDialog - NotesDGVDoubleClick: ShowingEditNoteDialog");
            ShowEditNoteDialog();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O))
            {
                EditSelectedDocument();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.E))
            {
                // We need to check a document is actually selected in this case
                // because we need to actually create the document instanace
                // and check what type it is.
                if (documentsDataGridView.SelectedDocumentID == -1)
                    return true;

                Document document = new Document(documentsDataGridView.SelectedDocumentID);
                if(document.DocumentType == DocumentType.Memo || document.DocumentType == DocumentType.Letter)
                {
                    // if this is a Word Document and Word does not exist,
                    // don't crash the program trying to export...
                    if(WordInterop.WordExists == false)
                        return true;
                }
                ExportSelectedDocument();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.N))
            {
                ShowNewDocumentDialog();
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                DeleteDocument();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cqpGrayRectangleButton1_Click(object sender, EventArgs e)
        {
            throw new Exception("Crash button pressed.");
        }

        private void SoldiersDialog_Resize(object sender, EventArgs e)
        {
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //Below for testing the APPs ability to send an error report, the button is invisible unless set to visible for testing mdail 9-30-19
            Logger.SendCrashReport();
        }
    }
}
