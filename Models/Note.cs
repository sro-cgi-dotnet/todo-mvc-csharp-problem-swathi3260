using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace TodoApi1.Models{
    public class Note{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoteId {get; set;}
        [Required]
        public string Title {get; set;}
        public string PlainText {get; set;}

        public string Label {get; set;}
        public bool Pinned {get; set;}

        public List<CheckListItem> CheckList {get; set;}
    }
} 