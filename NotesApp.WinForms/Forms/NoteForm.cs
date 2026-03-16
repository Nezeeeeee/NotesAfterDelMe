using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;

namespace NotesApp.WinForms.Forms
{
    public partial class NoteForm : Form
    {
        private readonly INoteService _noteService;
        private readonly NoteDto _note;
        private List<string> _currentTags = new List<string>();

        public NoteForm(INoteService noteService, NoteDto note = null)
        {
            _noteService = noteService;
            _note = note;
            InitializeComponent();

            // Подписываемся на изменение языка
            LocalizationManager.LanguageChanged += OnLanguageChanged;

            if (_note != null)
            {
                _currentTags = new List<string>(_note.Tags);
                LoadNote();
                this.Text = LocalizationManager.GetString("EditNote");
            }
            else
            {
                this.Text = LocalizationManager.GetString("NewNote");
            }

            UpdateUILanguage();
            RefreshTagsDisplay();
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            UpdateUILanguage();
            RefreshTagsDisplay();
        }

        private void UpdateUILanguage()
        {
            // Обновляем тексты контролов
            foreach (Control control in this.Controls)
            {
                if (control is Label label)
                {
                    if (label.Text.Contains("Title") || label.Text.Contains("Заголовок"))
                        label.Text = LocalizationManager.GetString("Title");
                    else if (label.Text.Contains("Content") || label.Text.Contains("Содержание"))
                        label.Text = LocalizationManager.GetString("Content");
                    else if (label.Text.Contains("Tags") || label.Text.Contains("Теги"))
                        label.Text = LocalizationManager.GetString("Tags_");
                }
            }

            btnSave.Text = LocalizationManager.GetString("Save");
            btnCancel.Text = LocalizationManager.GetString("Cancel");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LocalizationManager.LanguageChanged -= OnLanguageChanged;
            base.OnFormClosed(e);
        }

        private void LoadNote()
        {
            txtTitle.Text = _note.Title;
            txtContent.Text = _note.Content;
        }

        private void RefreshTagsDisplay()
        {
            // Очищаем контейнер с тегами
            flpTagsContainer.Controls.Clear();

            // Отображаем каждый тег как отдельный элемент с кнопкой удаления
            foreach (var tag in _currentTags)
            {
                var tagPanel = new Panel
                {
                    Height = 35,
                    Width = 150,
                    Margin = new Padding(3),
                    BackColor = Color.LightSteelBlue,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var lblTag = new Label
                {
                    Text = tag,
                    Location = new Point(5, 8),
                    AutoSize = true,
                    Font = new Font("Microsoft Sans Serif", 9)
                };

                var btnRemove = new Button
                {
                    Text = "X",
                    Location = new Point(115, 5),
                    Size = new Size(25, 25),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.IndianRed,
                    ForeColor = Color.White,
                    Tag = tag,
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold)
                };
                btnRemove.Click += BtnRemoveTag_Click;

                tagPanel.Controls.Add(lblTag);
                tagPanel.Controls.Add(btnRemove);
                flpTagsContainer.Controls.Add(tagPanel);
            }

            // Поле для добавления нового тега
            var addPanel = new Panel
            {
                Height = 35,
                Width = 220,
                Margin = new Padding(3)
            };

            var txtNewTag = new TextBox
            {
                Location = new Point(0, 5),
                Width = 160,
                PlaceholderText = LocalizationManager.GetString("NewTagPlaceholder"),
                Font = new Font("Microsoft Sans Serif", 9)
            };

            var btnAdd = new Button
            {
                Text = "+",
                Location = new Point(165, 5),
                Size = new Size(30, 25),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnAdd.Click += (s, e) => BtnAddTag_Click(txtNewTag.Text);

            addPanel.Controls.Add(txtNewTag);
            addPanel.Controls.Add(btnAdd);
            flpTagsContainer.Controls.Add(addPanel);
        }

        private void BtnRemoveTag_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var tagToRemove = btn.Tag.ToString();

            _currentTags.Remove(tagToRemove);
            RefreshTagsDisplay();
        }

        private void BtnAddTag_Click(string newTag)
        {
            if (!string.IsNullOrWhiteSpace(newTag) && !_currentTags.Contains(newTag.Trim()))
            {
                _currentTags.Add(newTag.Trim());
                RefreshTagsDisplay();
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show(
                    LocalizationManager.GetString("TitleRequired"),
                    LocalizationManager.GetString("ValidationError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            if (_note == null)
            {
                var createDto = new CreateNoteDto
                {
                    Title = txtTitle.Text,
                    Content = txtContent.Text,
                    Tags = _currentTags
                };
                await _noteService.CreateNoteAsync(createDto);
            }
            else
            {
                var updateDto = new UpdateNoteDto
                {
                    Id = _note.Id,
                    Title = txtTitle.Text,
                    Content = txtContent.Text,
                    Tags = _currentTags
                };
                await _noteService.UpdateNoteAsync(updateDto);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}