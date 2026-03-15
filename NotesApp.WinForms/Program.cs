using System;
using System.Windows.Forms; // Это должно быть первым
using NotesApp.Application.Interfaces;
using NotesApp.Application.Services;
using NotesApp.Infrastructure.Data;
using NotesApp.Infrastructure.Repositories;
using NotesApp.WinForms.Forms;

namespace NotesApp.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Явно указываем, что используем System.Windows.Forms.Application
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Настройка Dependency Injection вручную
            var context = new DatabaseContext();
            context.InitializeDatabaseAsync().Wait();

            var noteRepository = new NoteRepository(context);
            var tagRepository = new TagRepository(context);

            INoteService noteService = new NoteService(noteRepository, tagRepository);

            System.Windows.Forms.Application.Run(new MainForm(noteService));
        }
    }
}