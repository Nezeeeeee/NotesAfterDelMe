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

            // Подписываемся на изменение языка
            LocalizationManager.LanguageChanged += OnLanguageChanged;

            LoadNote();
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            LoadNote();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LocalizationManager.LanguageChanged -= OnLanguageChanged;
            base.OnFormClosed(e);
        }

        private void LoadNote()
        {
            this.Text = LocalizationManager.GetString("ViewNote");
            lblTitle.Text = _note.Title;

            lblDate.Text = string.Format("{0}: {1:dd.MM.yyyy HH:mm}\n{2}: {3:dd.MM.yyyy HH:mm}",
                LocalizationManager.GetString("Created"),
                _note.CreatedAt,
                LocalizationManager.GetString("Updated"),
                _note.UpdatedAt);

            txtContent.Text = _note.Content;

            // Очищаем панель тегов
            flpTags.Controls.Clear();

            // Добавляем заголовок для тегов
            var lblTagsHeader = new Label
            {
                Text = LocalizationManager.GetString("Tags_Header"),
                Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(5),
                ForeColor = Color.DimGray,
                Margin = new Padding(3, 3, 10, 3)
            };
            flpTags.Controls.Add(lblTagsHeader);

            // Отображаем теги
            if (_note.Tags != null && _note.Tags.Any())
            {
                foreach (var tag in _note.Tags)
                {
                    var lblTag = new Label
                    {
                        Text = $"  {tag}  ",
                        AutoSize = true,
                        Padding = new Padding(8, 5, 8, 5),
                        Margin = new Padding(5, 3, 5, 3),
                        BackColor = Color.LightSteelBlue,
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font("Microsoft Sans Serif", 10),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    var toolTip = new ToolTip();
                    toolTip.SetToolTip(lblTag, $"Тег: {tag}");

                    flpTags.Controls.Add(lblTag);
                }

                var lblTagCount = new Label
                {
                    Text = string.Format(LocalizationManager.GetString("TotalTags"), _note.Tags.Count),
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
                    Text = LocalizationManager.GetString("NoTags_"),
                    AutoSize = true,
                    Padding = new Padding(10, 8, 10, 8),
                    ForeColor = Color.Gray,
                    Font = new Font("Microsoft Sans Serif", 10),
                    BackColor = Color.LightGray,
                    BorderStyle = BorderStyle.FixedSingle
                };
                flpTags.Controls.Add(lblNoTags);
            }

            btnClose.Text = LocalizationManager.GetString("Close");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}