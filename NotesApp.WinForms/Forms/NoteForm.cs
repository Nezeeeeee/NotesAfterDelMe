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

        // ❌ НЕ НУЖНО объявлять поля здесь - они уже есть в Designer.cs
        // УДАЛИТЕ эти строки:
        // private System.Windows.Forms.TextBox txtTitle;
        // private System.Windows.Forms.TextBox txtContent;
        // private System.Windows.Forms.FlowLayoutPanel flpTagsContainer;
        // private System.Windows.Forms.Button btnSave;
        // private System.Windows.Forms.Button btnCancel;

        public NoteForm(INoteService noteService, NoteDto note = null)
        {
            _noteService = noteService;
            _note = note;
            InitializeComponent();

            if (_note != null)
            {
                _currentTags = new List<string>(_note.Tags);
                LoadNote();
                this.Text = "Edit Note";
            }
            else
            {
                this.Text = "New Note";
            }

            RefreshTagsDisplay();
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
                    Height = 30,
                    Width = 150,
                    Margin = new Padding(3),
                    BackColor = Color.LightSteelBlue,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var lblTag = new Label
                {
                    Text = tag,
                    Location = new Point(5, 5),
                    AutoSize = true,
                    MaximumSize = new Size(100, 20)
                };

                var btnRemove = new Button
                {
                    Text = "X",
                    Location = new Point(120, 3),
                    Size = new Size(20, 20),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.IndianRed,
                    ForeColor = Color.White,
                    Tag = tag
                };
                btnRemove.Click += BtnRemoveTag_Click;

                tagPanel.Controls.Add(lblTag);
                tagPanel.Controls.Add(btnRemove);
                flpTagsContainer.Controls.Add(tagPanel);
            }

            // Поле для добавления нового тега
            var addPanel = new Panel
            {
                Height = 30,
                Width = 200,
                Margin = new Padding(3)
            };

            var txtNewTag = new TextBox
            {
                Location = new Point(0, 3),
                Width = 150,
                PlaceholderText = "новый тег"
            };

            var btnAdd = new Button
            {
                Text = "+",
                Location = new Point(155, 3),
                Size = new Size(25, 22)
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
                MessageBox.Show("Title is required", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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