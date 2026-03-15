using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<Tag> GetOrCreateTagAsync(string tagName);
        Task<IEnumerable<Tag>> GetAllTagsAsync();
    }
}