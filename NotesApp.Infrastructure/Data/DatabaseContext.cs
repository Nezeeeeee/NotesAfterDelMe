using System;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.Data
{
    public class DatabaseContext : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly string _databasePath;

        public DatabaseContext()
        {
            //Если хочешь чтобы бд была в папке с прогой то:
            // База данных будет в папке с программой
            //var appDataPath = Path.Combine(
            //    AppDomain.CurrentDomain.BaseDirectory,
            //    "Data");

            //if (!Directory.Exists(appDataPath))
            //    Directory.CreateDirectory(appDataPath);

            //_databasePath = Path.Combine(appDataPath, "notes.db");
            //_connection = new SqliteConnection($"Data Source={_databasePath}");

            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "NotesApp");

            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            _databasePath = Path.Combine(appDataPath, "notes.db");
            _connection = new SqliteConnection($"Data Source={_databasePath}");
        }

        public SqliteConnection Connection => _connection;

        public async Task InitializeDatabaseAsync()
        {
            await _connection.OpenAsync();

            var createNotesTable = @"
                CREATE TABLE IF NOT EXISTS Notes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Content TEXT,
                    CreatedAt TEXT NOT NULL,
                    UpdatedAt TEXT NOT NULL
                );";

            var createTagsTable = @"
                CREATE TABLE IF NOT EXISTS Tags (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE
                );";

            var createNoteTagsTable = @"
                CREATE TABLE IF NOT EXISTS NoteTags (
                    NoteId INTEGER,
                    TagId INTEGER,
                    PRIMARY KEY (NoteId, TagId),
                    FOREIGN KEY (NoteId) REFERENCES Notes(Id) ON DELETE CASCADE,
                    FOREIGN KEY (TagId) REFERENCES Tags(Id) ON DELETE CASCADE
                );";

            using var command1 = new SqliteCommand(createNotesTable, _connection);
            await command1.ExecuteNonQueryAsync();

            using var command2 = new SqliteCommand(createTagsTable, _connection);
            await command2.ExecuteNonQueryAsync();

            using var command3 = new SqliteCommand(createNoteTagsTable, _connection);
            await command3.ExecuteNonQueryAsync();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}