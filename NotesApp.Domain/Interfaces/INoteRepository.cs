using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<IEnumerable<Note>> SearchAsync(string searchTerm, List<string> tags);
    }
}