using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoApi1.Models{
    public class DbRepo : IDbRepo {

        TodoContext db = null;
        public DbRepo(TodoContext _db){
            this.db = _db;
        }

        public Note GetNote(int Id){
            using (db){
                return db.Notes.FirstOrDefault(n => n.NoteId == Id);
            }
        }

        public List<Note> GetAllNotes(){
            using (db){
                return db.Notes.Include(n => n.CheckList).ToList();
                // return db.Notes.ToList();
            }
        }
        public List<Note> RetrieveNote(string text, string type)
        {
            List<Note> notes = new List<Note>();
            
            if(type == "Title")
            {
                notes = db.Notes.Where(n => n.Title == text).
                                Include(n => n.CheckList).
                                Include(n => n.Label).ToList();
            } 
            else if(type == "Pinned")
            {
                if(text == "true")
                {
                    notes = db.Notes.Where(n => n.Pinned == true).
                                    Include(n => n.CheckList).
                                    Include(n => n.Label).ToList();
                }
                else if (text == "false")
                {
                    notes = db.Notes.Where(n => n.Pinned == false).
                                    Include(n => n.CheckList).
                                    Include(n => n.Label).ToList();
                }
                else
                {
                    return null;
                }
            } 
            else
            {
                return null;
            }
            return notes;
        }
        
         public List<Note> GetLabel(string label){
            using (db){
                return db.Notes.Include(n => n.Label == label).ToList();
            }
        }
        public bool PostNote(Note note){
            using (db)
            {
                if(db.Notes.FirstOrDefault(n => n.NoteId == note.NoteId) == null){
                    // PostChecklist(note);
                    db.Notes.Add(note);
                    PostChecklist(note);
                    db.SaveChanges();
                    return true;
                }
                else{
                    return false;
                }
            }
        }

        void PostChecklist(Note note){
            foreach(var cl in note.CheckList){
                db.CheckLists.Add(cl);
            }
            db.SaveChanges();
        }

        public bool PutNote(int id, Note note){
            using (db){
                Note iNote = db.Notes.FirstOrDefault(n => n.NoteId == id);
                if(iNote != null){
                    db.Notes.Remove(iNote);
                    db.Notes.Add(note);
                    db.SaveChanges();
                    return true;
                }
                else{
                    return false;
                }
            }
        }

        public bool DeleteNote(int id){
            using ( db){
                Note iNote = db.Notes.FirstOrDefault(n => n.NoteId == id);
                if(iNote != null){
                    db.Notes.Remove(iNote);
                    db.SaveChanges();
                    return true;
                }
                else{
                    return false;
                }
            }
        }

        ~DbRepo(){
            db.Dispose();
        }
    }
}