using System;
using Xunit;
using TodoApi1.Models;
using TodoApi1.Controllers;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TodoTests
{
    public class UnitTest1
    {
        private List<Note> GetMockDatabase(){
            return new List<Note>{
                new Note{
                    NoteId = 1,
                    Title = "Things to do",
                    PlainText= "what to do",
                    CheckList = new List<CheckListItem>{
                        new CheckListItem{
                            checkId = 1,
                            Name = "visit nandi hills",
                            Checked=true,
                            NoteId = 1
                        }
                    },
                },
                new Note{
                    NoteId = 2,
                    Title = "Trial",
                    PlainText="fjhdsfsj",
                    CheckList = new List<CheckListItem>{
                        new CheckListItem{
                            checkId = 2,
                            Name= "If you eat half, other half is left",
                            NoteId=2
                        }
                    },
                    }
                
            };
        }

        [Fact]
        public void GetAll_Positive()
        {
            var mockdata = new Mock<IDbRepo>();
            List<Note> notes = GetMockDatabase();
            mockdata.Setup(d => d.GetAllNotes()).Returns(notes);
            TodoController notescontroller = new TodoController(mockdata.Object);
            var result = notescontroller.Get();
            Assert.NotNull(result);
            Assert.Equal(2 , notes.Count);

        }

        /*[Fact]
        public void GetAll_Negative_DatabaseError()
        {
            var datarepo = new Mock<IDbRepo>();
            List<Note> notes = null;
            datarepo.Setup(d => d.GetAllNotes()).Returns(notes);
            TodoController todoController = new TodoController(datarepo.Object);
            var result = todoController.Get();
            Assert.IsType<NotFoundObjectResult>(result);
        }


     [Fact]
        public void GetById_Negative_ReturnsNullNotFound()
        {
            var datarepo = new Mock<IDbRepo>();
            List<Note> notes = GetMockDatabase();
            int id = 3;
            datarepo.Setup(d => d.GetNote(id)).Returns(notes.Find(n => n.NoteId == id));
            TodoController todoController = new TodoController(datarepo.Object);
            var result = todoController.Get(id);
            Assert.IsType<NotFoundObjectResult>(result);
        }     */   

     public void PostById_Positive_ReturnsCreated()
        {
            var datarepo = new Mock<IDbRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            datarepo.Setup(d => d.PostNote(note)).Returns(true);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Post(note);

            var crObjectResult = actionResult as CreatedResult;
            Assert.NotNull(crObjectResult);

            var model = crObjectResult.Value as Note;
            Assert.Equal(note.NoteId, model.NoteId);
        }

        [Fact]
        public void PostById_Negative_ReturnsBadRequest()
        {
            var datarepo = new Mock<IDbRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            datarepo.Setup(d => d.PostNote(note)).Returns(false);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Post(note);

            var brObjectResult = actionResult as BadRequestObjectResult;
            Assert.NotNull(brObjectResult);
        }

         [Fact]
        public void PutById_Positive_ReturnsCreated()
        {
            var datarepo = new Mock<IDbRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            int id = (int)note.NoteId;
            datarepo.Setup(d => d.PutNote(id, note)).Returns(true);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Put(id, note);

            var crObjectResult = actionResult as CreatedResult;
            Assert.NotNull(crObjectResult);

            var model = crObjectResult.Value as Note;
            Assert.Equal(id, model.NoteId);
        }

        [Fact]
        public void PutById_Negative_ReturnsNotFoundt()
        {
            var datarepo = new Mock<IDbRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            int id = (int)note.NoteId;
            datarepo.Setup(d => d.PostNote(note)).Returns(false);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Put(id, note);

            var nfObjectResult = actionResult as NotFoundObjectResult;
            Assert.NotNull(nfObjectResult);
        }
        

    }

    }