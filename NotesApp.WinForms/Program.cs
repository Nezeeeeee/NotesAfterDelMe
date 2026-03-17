using System;
using System.Windows.Forms;
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
            try
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                var context = new DatabaseContext();
                context.InitializeDatabaseAsync().Wait();

                var noteRepository = new NoteRepository(context);
                var tagRepository = new TagRepository(context);

                INoteService noteService = new NoteService(noteRepository, tagRepository);

                System.Windows.Forms.Application.Run(new MainForm(noteService));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка при запуске: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}