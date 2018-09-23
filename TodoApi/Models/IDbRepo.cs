using System.Collections.Generic;

namespace TodoApi1.Models{
    public interface IDbRepo{
        Note GetNote(int Id);

        List<Note> GetAllNotes();

        bool PostNote(Note note);

        bool PutNote(int id, Note note);
        bool DeleteNote(int id);
    }
}