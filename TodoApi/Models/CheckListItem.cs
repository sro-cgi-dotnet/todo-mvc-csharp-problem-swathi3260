using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApi1.Models;

namespace TodoApi1.Models
{
    public class CheckListItem {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int checkId {get; set;}
        public bool Checked {get; set;}
        public string Name {get; set;}
        public int NoteId{get; set;}

    }
}