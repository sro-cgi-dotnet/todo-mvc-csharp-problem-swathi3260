using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi1.Models;

namespace TodoApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        IDbRepo dRepo = null ;
        // intialise this repo with a dependency injection
        public TodoController(IDbRepo _Repo){
            this.dRepo = _Repo;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Note>> Get()
        {
           var notes = dRepo.GetAllNotes();
            if(notes.Count > 0){
                return Ok(notes);
            }
            else{
                return Ok("No Entries Available. Database is Empty");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            var noteId = dRepo.GetNote(id);
            if (noteId != null)
            {
                return Ok(noteId);
            }
            else
            {
                return NotFound($"Note with id {id} is not found.");
            }
        }

        public ActionResult<Note> Get(string text,[FromQuery] string type)
        {
            List<Note> listWithText = dRepo.RetrieveNote(text, type);
            if(listWithText == null){
                return BadRequest($"Type : {type} or Text : {text}  is invalid.");
            }
            else if(listWithText.Count == 0){
                return NotFound($"Notes with {type} = {text} not found.");
            }
            else{
                return Ok(listWithText);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            if(ModelState.IsValid){
                bool res = dRepo.PostNote(note);
                if (res)
                {
                    return Created($"/api/todo/{note.NoteId}",note);
                }
                else
                {
                    return BadRequest("Note already exists");
                }
            }
            return BadRequest("Invalid Format");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Note note)
        {
            if(ModelState.IsValid){
                bool res = dRepo.PutNote(id, note);
                if(res){
                    return Created($"/api/todo/{note.NoteId}", note);
                }
                else{
                    return NotFound($"Note with id {id} is not found");
                }
            }
            return BadRequest("Invalid Format");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             bool res = dRepo.DeleteNote(id);
            if(res){
                return Ok($"note with id {id} is deleted");
            }
            else{
                return NotFound($"Note with id {id} is not found");
        }
    }
}
}