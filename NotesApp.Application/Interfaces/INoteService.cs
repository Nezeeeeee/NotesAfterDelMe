using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Application.DTOs;

namespace NotesApp.Application.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllNotesAsync();
        Task<NoteDto> GetNoteByIdAsync(int id);
        Task<NoteDto> CreateNoteAsync(CreateNoteDto createDto);
        Task UpdateNoteAsync(UpdateNoteDto updateDto);
        Task DeleteNoteAsync(int id);
        Task<IEnumerable<NoteDto>> SearchNotesAsync(string searchTerm, List<string> tags);
    }
}