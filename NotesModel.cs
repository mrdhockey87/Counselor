using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class NotesModel
    {
        internal delegate void NoteModelRefreshedEvent();
        internal static event NoteModelRefreshedEvent noteModelRefreshed;

        static DataTable notesTable;

        static DataTable noteSubjects;
        static DataTable noteValues;

        static DocumentSortMode sortMode;

        internal static DataTable GetNotesTable()
        {
            if (notesTable == null)
                RefreshNotesTables();

            return notesTable;
        }


        internal static void RefreshNotesTables()
        {
            //string query = "select * from notes "
            //                + " inner join "
            //                + " notevalues "
            //                + " on notes.noteid = notevalues.noteid "
            //                + " group by notes.noteid "
            //                + " order by notevalues.issubject asc ";

            //notesTable = DatabaseConnection.Query(query);

            //foreach (DataRow row in notesTable.Rows)
            //{
            //    if (Convert.ToInt32(row["issubject"]) == 0)
            //    {
            //        row["date"] = DBNull.Value;
            //    }
            //}

            RefreshNoteSubjectsTable();
            RefreshNoteValuesTable();
            RefreshNotesTable();
        }

        internal static void RefreshNoteSubjectsTable()
        {
            string query = "select * from notes "
                            + " inner join "
                            + " notevalues "
                            + " on notes.noteid = notevalues.noteid "
                            + " where notevalues.issubject = 1 ";

            noteSubjects = DatabaseConnection.Query(query);
        }

        internal static void RefreshNoteValuesTable()
        {
            string query = "select * from notes "
                            + " inner join "
                            + " notevalues "
                            + " on notes.noteid = notevalues.noteid "
                            + " where notevalues.issubject = 0 ";

            noteValues = DatabaseConnection.Query(query);
        }


        internal static void RefreshNotesTable()
        {
            if (notesTable == null)
                notesTable = noteValues.Clone();

            notesTable.Clear();

            DataView subjectView = noteSubjects.AsDataView();
            DataView valuesView = noteValues.AsDataView();

            string sortModeString = "date asc";

            if (sortMode == DocumentSortMode.DateAsc)
                sortModeString = "date asc";
            else if (sortMode == DocumentSortMode.DateDesc)
                sortModeString = "date desc";
            else if (sortMode == DocumentSortMode.NameAsc)
                sortModeString = "value asc";
            else if (sortMode == DocumentSortMode.NameDesc)
                sortModeString = "value desc";

            subjectView.Sort = sortModeString;
            valuesView.Sort = sortModeString;

            int numRows = subjectView.Table.Rows.Count;

            DataRowCollection subjectRows = subjectView.ToTable().Rows;
            DataRowCollection valueRows = valuesView.ToTable().Rows;

            foreach (DataRow subjectRow in subjectRows)
            {
                int noteID = Convert.ToInt32(subjectRow["noteid"]);

                DataRow valueRow = valueRows.Cast<DataRow>().Where( row => Convert.ToInt32(row["noteid"]) == noteID ).First();

                DataRow newSubjectRow = notesTable.NewRow();
                DataRow newValueRow = notesTable.NewRow();

                byte[] noteIV = Convert.FromBase64String(subjectRow["noteIV"].ToString());

                string base64subject = subjectRow["value"].ToString();
                string decryptedSubject = Encryption.Base64DecryptString(base64subject, noteIV);

                string base64value = valueRow["value"].ToString();
                string decryptedValue = Encryption.Base64DecryptString(base64value, noteIV);

                subjectRow["value"] = decryptedSubject;
                valueRow["value"] = decryptedValue;

                newSubjectRow.ItemArray = subjectRow.ItemArray;
                newValueRow.ItemArray = valueRow.ItemArray;

                notesTable.Rows.Add(newSubjectRow);
                notesTable.Rows.Add(newValueRow);
            }

            foreach (DataRow row in notesTable.Rows)
            {
                if (Convert.ToInt32(row["issubject"]) == 0)
                {
                    row["date"] = DBNull.Value;
                }
            }
        }


        internal static void SetSortMode(DocumentSortMode sm)
        {
            sortMode = sm;

            RefreshNotesTable();
        }


        internal static DataTable GetNoteValuesForNoteID(int noteID)
        {
            if(notesTable == null)
                RefreshNotesTables();

            DataTable noteValues = notesTable.Select("noteid = " + noteID).CopyToDataTable();
            
            if (noteValues.Rows.Count < 2)
                throw new DataLoadFailedException("The details for the selected note could not be retrieved.");

            return noteValues;
        }


        private static int InsertNote(int noteID, DateTime date, int soldierID, string guid)
        {
            string insertText = "insert into notes (" + (noteID == -1 ? "" : "noteID, ") + "date, soldierid, noteguid, noteIV) "
                                        + " values (" + (noteID == -1 ? "" : "@noteID, ") + " @date, @soldierID, @noteguid, @noteIV)";

            Params paramValues = new Params();
            if (noteID != -1)
                paramValues.Add("@noteID", noteID.ToString());

            string IV = Convert.ToBase64String(Encryption.GenerateIV());

            paramValues.Add("@date", date.Ticks.ToString());
            paramValues.Add("@soldierID", soldierID.ToString());
            paramValues.Add("@noteguid", guid);
            paramValues.Add("@noteIV", IV);

            noteID = DatabaseConnection.Insert(insertText, paramValues);
            return noteID;
        }


        internal static int CreateNewNoteForSoldier(int noteID, DateTime date, int soldierID, string guid)
        {
            noteID = InsertNote(noteID, date, soldierID, guid);

            Params paramValues = new Params();
            paramValues.Add("@noteID", noteID.ToString());

            string insertText = "insert into notevalues (noteid, value, issubject) "
                                             + " values (@noteID, \"\", 1)";
            
            DatabaseConnection.Insert(insertText, paramValues);

            insertText = "insert into notevalues (noteid, value, issubject) "
                                      + " values (@noteID, \"\", 0)";
            
            DatabaseConnection.Insert(insertText, paramValues);

            Refresh();
            DatabaseConnection.Backup();

            return noteID;
        }


        internal static int SaveNote(NoteInterface note)
        {
            if(notesTable.Select("noteid = " + note.NoteID).Count() == 0 )
                note.NoteID = CreateNewNoteForSoldier(note.NoteID, DateTime.Now, note.SoldierID, note.NoteGUID.ToString());

            string IVstring = DatabaseConnection.GetSingleValue("notes", "noteid", note.NoteID, "noteIV");
            byte[] IVbytes = Convert.FromBase64String(IVstring);
            //byte[] IVbytes = Encryption.GetBytes(IVstring);

            string subjectBase64Encrypted = Encryption.Base64EncryptString(note.Subject, IVbytes);
            string commentBase64Encrypted = Encryption.Base64EncryptString(note.Comment, IVbytes);

            DatabaseConnection.Update("notevalues", 
                                      new List<string>(){"value"},
                                      new List<string>(){subjectBase64Encrypted}, 
                                      new List<string>(){"noteid", "issubject"}, 
                                      new List<string>(){note.NoteID.ToString(), "1"});

            DatabaseConnection.Update("notevalues",
                                        new List<string>(){"value"},
                                        new List<string>(){commentBase64Encrypted},
                                        new List<string>(){"noteid", "issubject"},
                                        new List<string>(){note.NoteID.ToString(), "0"});

            DatabaseConnection.Update("notes", "date", note.Date.Ticks.ToString(),
                                        "noteid", note.NoteID.ToString());

            Refresh();
            DatabaseConnection.Backup();
            
            return note.NoteID;
        }

        /*
        internal static void UpdateNote(NoteInterface note)
        {
            //DatabaseConnection.Update("update notevalues set value = "
            //                            + "\"" + note.Subject + "\""
            //                            + " where noteid = " + note.NoteID
            //                            + " and issubject = 1");

            DatabaseConnection.Update("notevalues",
                          new List<string>() { "value" },
                          new List<string>() { note.Subject },
                          new List<string>() { "noteid", "issubject" },
                          new List<string>() { note.NoteID.ToString(), "1" });

            DatabaseConnection.Update("notevalues",
                                        new List<string>() { "value" },
                                        new List<string>() { note.Comment },
                                        new List<string>() { "noteid", "issubject" },
                                        new List<string>() { note.NoteID.ToString(), "0" });

            DatabaseConnection.Update("notes", "date", note.Date.Ticks.ToString(),
                            "noteid", note.NoteID.ToString());

            Refresh();
            DatabaseConnection.Backup();
        }
        */

        internal static void DeleteNote(int noteID)
        {
            DatabaseConnection.Delete("notes", "noteid", noteID.ToString());
            DatabaseConnection.Delete("notevalues", "noteid", noteID.ToString());

            Refresh();
            DatabaseConnection.Backup();
        }


        internal static void Refresh()
        {
            DatabaseConnection.Backup();

            RefreshNotesTables();

            if (noteModelRefreshed != null)
                noteModelRefreshed();
        }



        internal static NoteModelRefreshedEvent NoteModelRefreshed
        {
            set
            {
                noteModelRefreshed += value;
            }
        }

        internal static List<NoteInterface> GetAllNotesForSoldier(int soldierID)
        {
            List<NoteInterface> notes = new List<NoteInterface>();

            DataRow[] rows = notesTable.Select("soldierid = " + soldierID);
            foreach (DataRow row in rows)
            {
                if (row["issubject"].ToString() != "1")
                    continue;

                int noteID = Convert.ToInt32(row["noteid"]);
                NoteInterface note = new NoteInterface(noteID);

                notes.Add(note);
            }

            return notes;
        }

        internal static int GetNoteID(Guid guid)
        {
            if(notesTable == null)
                RefreshNotesTables();

            int noteID = -1;
            DataRow[] rows = notesTable.Select("noteguid = '" + guid.ToString() + "'");

            if (rows.Count() > 0)
                noteID = Convert.ToInt32(rows[0]["noteid"]);

            return noteID;
        }

        internal static bool NoteExists(Guid guid)
        {
            if (notesTable.Select("noteguid = '" + guid.ToString() + "'").Count() > 0)
                return true;

            return false;
        }
    }
}
