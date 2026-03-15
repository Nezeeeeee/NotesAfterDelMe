using System;
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

        public NoteForm(INoteService noteService, NoteDto note = null)
        {
            _noteService = noteService;
            _note = note;
            InitializeComponent();

            if (_note != null)
            {
                LoadNote();
                this.Text = "Edit Note";
            }
            else
            {
                this.Text = "New Note";
            }
        }

        private void LoadNote()
        {
            txtTitle.Text = _note.Title;
            txtContent.Text = _note.Content;
            txtTags.Text = string.Join(", ", _note.Tags);
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

            var tags = txtTags.Text
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList();

            if (_note == null)
            {
                var createDto = new CreateNoteDto
                {
                    Title = txtTitle.Text,
                    Content = txtContent.Text,
                    Tags = tags
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
                    Tags = tags
                };
                await _noteService.UpdateNoteAsync(updateDto);
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}