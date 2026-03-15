using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NotesApp.Application.DTOs;

namespace NotesApp.WinForms.Forms
{
    public partial class ViewNoteForm : Form
    {
        private readonly NoteDto _note;

        public ViewNoteForm(NoteDto note)
        {
            _note = note;
            InitializeComponent();
            LoadNote();
        }

        private void LoadNote()
        {
            this.Text = _note.Title;
            lblTitle.Text = _note.Title;

            // Форматируем дату в две строки для лучшей читаемости
            lblDate.Text = $"Создано: {_note.CreatedAt:dd.MM.yyyy HH:mm}\nОбновлено: {_note.UpdatedAt:dd.MM.yyyy HH:mm}";

            txtContent.Text = _note.Content;

            // Отображаем теги
            flpTags.Controls.Clear();

            // Добавляем заголовок для тегов
            var lblTagsHeader = new Label
            {
                Text = "Теги:",
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(5),
                ForeColor = Color.DimGray
            };
            flpTags.Controls.Add(lblTagsHeader);

            // Отображаем каждый тег
            if (_note.Tags != null && _note.Tags.Any())
            {
                foreach (var tag in _note.Tags)
                {
                    var lblTag = new Label
                    {
                        Text = tag,
                        AutoSize = true,
                        Padding = new Padding(8, 4, 8, 4),
                        Margin = new Padding(3, 3, 3, 3),
                        BackColor = Color.LightSteelBlue,
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font("Microsoft Sans Serif", 9)
                    };
                    flpTags.Controls.Add(lblTag);
                }
            }
            else
            {
                var lblNoTags = new Label
                {
                    Text = "Нет тегов",
                    AutoSize = true,
                    Padding = new Padding(5),
                    ForeColor = Color.Gray,
                    Font = new Font("Microsoft Sans Serif", 9)
                };
                flpTags.Controls.Add(lblNoTags);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}