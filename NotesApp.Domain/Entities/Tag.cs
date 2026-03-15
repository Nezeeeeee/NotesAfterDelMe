using System.Collections.Generic;

namespace NotesApp.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}