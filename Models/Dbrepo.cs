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