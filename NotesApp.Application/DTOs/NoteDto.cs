using System;
using System.Collections.Generic;

namespace NotesApp.Application.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class CreateNoteDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class UpdateNoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}