using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces;
using NotesApp.Infrastructure.Data;

namespace NotesApp.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly DatabaseContext _context;

        public NoteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            await _context.Connection.OpenAsync();

            var query = @"
                SELECT n.*, t.Id as TagId, t.Name as TagName
                FROM Notes n
                LEFT JOIN NoteTags nt ON n.Id = nt.NoteId
                LEFT JOIN Tags t ON nt.TagId = t.Id
                WHERE n.Id = @id";

            using var command = new SqliteCommand(query, _context.Connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            Note note = null;

            while (await reader.ReadAsync())
            {
                if (note == null)
                {
                    note = new Note
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Content = reader.IsDBNull(2) ? null : reader.GetString(2),
                        CreatedAt = DateTime.Parse(reader.GetString(3)),
                        UpdatedAt = DateTime.Parse(reader.GetString(4)),
                        Tags = new List<Tag>()
                    };
                }

                if (!reader.IsDBNull(5))
                {
                    note.Tags.Add(new Tag
                    {
                        Id = reader.GetInt32(5),
                        Name = reader.GetString(6)
                    });
                }
            }

            _context.Connection.Close();
            return note;
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            var notes = new List<Note>();
            await _context.Connection.OpenAsync();

            var query = @"
                SELECT n.*, t.Id as TagId, t.Name as TagName
                FROM Notes n
                LEFT JOIN NoteTags nt ON n.Id = nt.NoteId
                LEFT JOIN Tags t ON nt.TagId = t.Id
                ORDER BY n.UpdatedAt DESC";

            using var command = new SqliteCommand(query, _context.Connection);
            using var reader = await command.ExecuteReaderAsync();

            var notesDict = new Dictionary<int, Note>();

            while (await reader.ReadAsync())
            {
                var noteId = reader.GetInt32(0);
                if (!notesDict.ContainsKey(noteId))
                {
                    notesDict[noteId] = new Note
                    {
                        Id = noteId,
                        Title = reader.GetString(1),
                        Content = reader.IsDBNull(2) ? null : reader.GetString(2),
                        CreatedAt = DateTime.Parse(reader.GetString(3)),
                        UpdatedAt = DateTime.Parse(reader.GetString(4)),
                        Tags = new List<Tag>()
                    };
                }

                if (!reader.IsDBNull(5))
                {
                    notesDict[noteId].Tags.Add(new Tag
                    {
                        Id = reader.GetInt32(5),
                        Name = reader.GetString(6)
                    });
                }
            }

            _context.Connection.Close();
            return notesDict.Values;
        }

        public async Task<Note> AddAsync(Note entity)
        {
            await _context.Connection.OpenAsync();
            using var transaction = _context.Connection.BeginTransaction();

            try
            {
                var insertNote = @"
                    INSERT INTO Notes (Title, Content, CreatedAt, UpdatedAt)
                    VALUES (@title, @content, @createdAt, @updatedAt);
                    SELECT last_insert_rowid();";

                using var command = new SqliteCommand(insertNote, _context.Connection, transaction);
                command.Parameters.AddWithValue("@title", entity.Title);
                command.Parameters.AddWithValue("@content", entity.Content ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@createdAt", entity.CreatedAt.ToString("o"));
                command.Parameters.AddWithValue("@updatedAt", entity.UpdatedAt.ToString("o"));

                entity.Id = Convert.ToInt32(await command.ExecuteScalarAsync());

                foreach (var tag in entity.Tags)
                {
                    var insertNoteTag = @"
                        INSERT INTO NoteTags (NoteId, TagId)
                        VALUES (@noteId, @tagId)";

                    using var tagCommand = new SqliteCommand(insertNoteTag, _context.Connection, transaction);
                    tagCommand.Parameters.AddWithValue("@noteId", entity.Id);
                    tagCommand.Parameters.AddWithValue("@tagId", tag.Id);
                    await tagCommand.ExecuteNonQueryAsync();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                _context.Connection.Close();
            }

            return entity;
        }

        public async Task UpdateAsync(Note entity)
        {
            await _context.Connection.OpenAsync();
            using var transaction = _context.Connection.BeginTransaction();

            try
            {
                var updateNote = @"
                    UPDATE Notes 
                    SET Title = @title, Content = @content, UpdatedAt = @updatedAt
                    WHERE Id = @id";

                using var command = new SqliteCommand(updateNote, _context.Connection, transaction);
                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@title", entity.Title);
                command.Parameters.AddWithValue("@content", entity.Content ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@updatedAt", entity.UpdatedAt.ToString("o"));
                await command.ExecuteNonQueryAsync();

                // Удаляем старые связи с тегами
                var deleteTags = "DELETE FROM NoteTags WHERE NoteId = @noteId";
                using var deleteCommand = new SqliteCommand(deleteTags, _context.Connection, transaction);
                deleteCommand.Parameters.AddWithValue("@noteId", entity.Id);
                await deleteCommand.ExecuteNonQueryAsync();

                // Добавляем новые связи
                foreach (var tag in entity.Tags)
                {
                    var insertNoteTag = @"
                        INSERT INTO NoteTags (NoteId, TagId)
                        VALUES (@noteId, @tagId)";

                    using var tagCommand = new SqliteCommand(insertNoteTag, _context.Connection, transaction);
                    tagCommand.Parameters.AddWithValue("@noteId", entity.Id);
                    tagCommand.Parameters.AddWithValue("@tagId", tag.Id);
                    await tagCommand.ExecuteNonQueryAsync();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                _context.Connection.Close();
            }
        }

        public async Task DeleteAsync(Note entity)
        {
            await _context.Connection.OpenAsync();
            var query = "DELETE FROM Notes WHERE Id = @id";

            using var command = new SqliteCommand(query, _context.Connection);
            command.Parameters.AddWithValue("@id", entity.Id);
            await command.ExecuteNonQueryAsync();

            _context.Connection.Close();
        }

        public async Task<IEnumerable<Note>> SearchAsync(string searchTerm, List<string> tags)
        {
            var notes = new List<Note>();
            await _context.Connection.OpenAsync();

            var query = @"
                SELECT DISTINCT n.*, t.Id as TagId, t.Name as TagName
                FROM Notes n
                LEFT JOIN NoteTags nt ON n.Id = nt.NoteId
                LEFT JOIN Tags t ON nt.TagId = t.Id
                WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query += $" AND (n.Title LIKE '%' || @searchTerm || '%' OR n.Content LIKE '%' || @searchTerm || '%')";
            }

            if (tags != null && tags.Any())
            {
                query += @" AND n.Id IN (
                    SELECT nt.NoteId 
                    FROM NoteTags nt
                    INNER JOIN Tags t ON nt.TagId = t.Id
                    WHERE t.Name IN (" + string.Join(",", tags.Select((_, i) => $"@tag{i}")) + @")
                    GROUP BY nt.NoteId
                    HAVING COUNT(DISTINCT t.Name) = " + tags.Count + @"
                )";
            }

            query += " ORDER BY n.UpdatedAt DESC";

            using var command = new SqliteCommand(query, _context.Connection);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                command.Parameters.AddWithValue("@searchTerm", searchTerm);
            }

            if (tags != null && tags.Any())
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    command.Parameters.AddWithValue($"@tag{i}", tags[i]);
                }
            }

            using var reader = await command.ExecuteReaderAsync();
            var notesDict = new Dictionary<int, Note>();

            while (await reader.ReadAsync())
            {
                var noteId = reader.GetInt32(0);
                if (!notesDict.ContainsKey(noteId))
                {
                    notesDict[noteId] = new Note
                    {
                        Id = noteId,
                        Title = reader.GetString(1),
                        Content = reader.IsDBNull(2) ? null : reader.GetString(2),
                        CreatedAt = DateTime.Parse(reader.GetString(3)),
                        UpdatedAt = DateTime.Parse(reader.GetString(4)),
                        Tags = new List<Tag>()
                    };
                }

                if (!reader.IsDBNull(5))
                {
                    notesDict[noteId].Tags.Add(new Tag
                    {
                        Id = reader.GetInt32(5),
                        Name = reader.GetString(6)
                    });
                }
            }

            _context.Connection.Close();
            return notesDict.Values;
        }

        public Task<IEnumerable<Note>> FindAsync(System.Linq.Expressions.Expression<Func<Note, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}