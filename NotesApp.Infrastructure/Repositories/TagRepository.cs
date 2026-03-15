using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces;
using NotesApp.Infrastructure.Data;

namespace NotesApp.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;

        public TagRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetOrCreateTagAsync(string tagName)
        {
            await _context.Connection.OpenAsync();

            // Пытаемся найти существующий тег
            var selectQuery = "SELECT Id, Name FROM Tags WHERE Name = @name";
            using var selectCommand = new SqliteCommand(selectQuery, _context.Connection);
            selectCommand.Parameters.AddWithValue("@name", tagName);

            using var reader = await selectCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var tag = new Tag
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                _context.Connection.Close();
                return tag;
            }

            // Если тег не найден, создаем новый
            _context.Connection.Close();
            await _context.Connection.OpenAsync();

            var insertQuery = "INSERT INTO Tags (Name) VALUES (@name); SELECT last_insert_rowid();";
            using var insertCommand = new SqliteCommand(insertQuery, _context.Connection);
            insertCommand.Parameters.AddWithValue("@name", tagName);

            var newId = Convert.ToInt32(await insertCommand.ExecuteScalarAsync());

            _context.Connection.Close();

            return new Tag
            {
                Id = newId,
                Name = tagName
            };
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            var tags = new List<Tag>();
            await _context.Connection.OpenAsync();

            var query = "SELECT Id, Name FROM Tags ORDER BY Name";
            using var command = new SqliteCommand(query, _context.Connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tags.Add(new Tag
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            _context.Connection.Close();
            return tags;
        }

        // Реализация IRepository<Tag>
        public async Task<Tag> GetByIdAsync(int id)
        {
            await _context.Connection.OpenAsync();
            var query = "SELECT Id, Name FROM Tags WHERE Id = @id";

            using var command = new SqliteCommand(query, _context.Connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var tag = new Tag
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                _context.Connection.Close();
                return tag;
            }

            _context.Connection.Close();
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await GetAllTagsAsync();
        }

        public async Task<Tag> AddAsync(Tag entity)
        {
            return await GetOrCreateTagAsync(entity.Name);
        }

        public async Task UpdateAsync(Tag entity)
        {
            await _context.Connection.OpenAsync();
            var query = "UPDATE Tags SET Name = @name WHERE Id = @id";

            using var command = new SqliteCommand(query, _context.Connection);
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@name", entity.Name);
            await command.ExecuteNonQueryAsync();

            _context.Connection.Close();
        }

        public async Task DeleteAsync(Tag entity)
        {
            await _context.Connection.OpenAsync();
            var query = "DELETE FROM Tags WHERE Id = @id";

            using var command = new SqliteCommand(query, _context.Connection);
            command.Parameters.AddWithValue("@id", entity.Id);
            await command.ExecuteNonQueryAsync();

            _context.Connection.Close();
        }

        public Task<IEnumerable<Tag>> FindAsync(System.Linq.Expressions.Expression<System.Func<Tag, bool>> predicate)
        {
            throw new System.NotImplementedException();
        }
    }
}