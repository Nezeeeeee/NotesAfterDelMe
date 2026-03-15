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

            lblDate.Text = $"Создано: {_note.CreatedAt:dd.MM.yyyy HH:mm}\nОбновлено: {_note.UpdatedAt:dd.MM.yyyy HH:mm}";

            txtContent.Text = _note.Content;

            // Очищаем панель тегов
            flpTags.Controls.Clear();

            // Добавляем заголовок для тегов (увеличенный)
            var lblTagsHeader = new Label
            {
                Text = "Теги:",
                Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(5),
                ForeColor = Color.DimGray,
                Margin = new Padding(3, 3, 10, 3)
            };
            flpTags.Controls.Add(lblTagsHeader);

            // Отображаем теги в виде красивых кнопок/меток
            if (_note.Tags != null && _note.Tags.Any())
            {
                foreach (var tag in _note.Tags)
                {
                    var lblTag = new Label
                    {
                        Text = $"  {tag}  ", // Добавляем пробелы для отступов
                        AutoSize = true,
                        Padding = new Padding(8, 5, 8, 5),
                        Margin = new Padding(5, 3, 5, 3),
                        BackColor = Color.LightSteelBlue,
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font("Microsoft Sans Serif", 10),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // Добавляем всплывающую подсказку
                    var toolTip = new ToolTip();
                    toolTip.SetToolTip(lblTag, $"Тег: {tag}");

                    flpTags.Controls.Add(lblTag);
                }

                // Добавляем информацию о количестве тегов
                var lblTagCount = new Label
                {
                    Text = $"Всего тегов: {_note.Tags.Count}",
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic),
                    AutoSize = true,
                    Padding = new Padding(5),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10, 3, 3, 3)
                };
                flpTags.Controls.Add(lblTagCount);
            }
            else
            {
                var lblNoTags = new Label
                {
                    Text = "Нет тегов",
                    AutoSize = true,
                    Padding = new Padding(10, 8, 10, 8),
                    ForeColor = Color.Gray,
                    Font = new Font("Microsoft Sans Serif", 10),
                    BackColor = Color.LightGray,
                    BorderStyle = BorderStyle.FixedSingle
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