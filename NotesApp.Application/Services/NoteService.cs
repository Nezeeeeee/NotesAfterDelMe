using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces;

namespace NotesApp.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly ITagRepository _tagRepository;

        public NoteService(INoteRepository noteRepository, ITagRepository tagRepository)
        {
            _noteRepository = noteRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllAsync();
            return notes.Select(n => MapToDto(n));
        }

        public async Task<NoteDto> GetNoteByIdAsync(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            return note != null ? MapToDto(note) : null;
        }

        public async Task<NoteDto> CreateNoteAsync(CreateNoteDto createDto)
        {
            var note = new Note
            {
                Title = createDto.Title,
                Content = createDto.Content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Добавляем теги
            foreach (var tagName in createDto.Tags.Distinct())
            {
                var tag = await _tagRepository.GetOrCreateTagAsync(tagName);
                note.Tags.Add(tag);
            }

            var createdNote = await _noteRepository.AddAsync(note);
            return MapToDto(createdNote);
        }

        public async Task UpdateNoteAsync(UpdateNoteDto updateDto)
        {
            var note = await _noteRepository.GetByIdAsync(updateDto.Id);
            if (note == null) throw new Exception("Note not found");

            note.Title = updateDto.Title;
            note.Content = updateDto.Content;
            note.UpdatedAt = DateTime.Now;

            // Обновляем теги
            note.Tags.Clear();
            foreach (var tagName in updateDto.Tags.Distinct())
            {
                var tag = await _tagRepository.GetOrCreateTagAsync(tagName);
                note.Tags.Add(tag);
            }

            await _noteRepository.UpdateAsync(note);
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note != null)
            {
                await _noteRepository.DeleteAsync(note);
            }
        }

        public async Task<IEnumerable<NoteDto>> SearchNotesAsync(string searchTerm, List<string> tags)
        {
            var notes = await _noteRepository.SearchAsync(searchTerm, tags);
            return notes.Select(n => MapToDto(n));
        }

        private NoteDto MapToDto(Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt,
                Tags = note.Tags.Select(t => t.Name).ToList()
            };
        }
    }
}