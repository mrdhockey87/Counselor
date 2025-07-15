using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal class NoteInterface
    {
        internal int NoteID { get; set; }
        internal int SoldierID { get; set; }
        internal String Subject { get; set; }
        internal String Comment { get; set; }
        internal DateTime Date { get; set; }

        internal Guid NoteGUID { get; set; }

        internal NoteInterface() 
        {
            NoteID = -1;
            SoldierID = -1;
            Subject = "";
            Comment = "";
            Date = DateTime.Now;

            NoteGUID = Guid.NewGuid();
        }


        internal NoteInterface(int noteID)
        {
            DataTable noteValues = NotesModel.GetNoteValuesForNoteID(noteID);

            NoteID = noteID;
            SoldierID = Convert.ToInt32(noteValues.Rows[0]["soldierid"]);
            Subject = noteValues.Select("issubject = 1")[0]["value"].ToString();
            Comment = noteValues.Select("issubject = 0")[0]["value"].ToString();
            Date = Convert.ToDateTime(noteValues.Rows[0]["date"]);

            NoteGUID = new Guid(noteValues.Rows[0]["noteguid"].ToString());
        }

        public NoteInterface(Guid guid)
        {
            int noteID = NotesModel.GetNoteID(guid);

            DataTable noteValues = NotesModel.GetNoteValuesForNoteID(noteID);

            NoteID = noteID;
            SoldierID = Convert.ToInt32(noteValues.Rows[0]["soldierid"]);
            Subject = noteValues.Select("issubject = 1")[0]["value"].ToString();
            Comment = noteValues.Select("issubject = 0")[0]["value"].ToString();
            Date = Convert.ToDateTime(noteValues.Rows[0]["date"]);

            NoteGUID = guid;
        }


        internal int Save()
        {
            return NotesModel.SaveNote(this);
            //Refresh();
        }

    }
}
