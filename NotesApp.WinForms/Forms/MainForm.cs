using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;

namespace NotesApp.WinForms.Forms
{
    public partial class MainForm : Form
    {
        private readonly INoteService _noteService;
        private List<NoteDto> _currentNotes = new List<NoteDto>();
        private List<string> _selectedTags = new List<string>();

        public MainForm(INoteService noteService)
        {
            _noteService = noteService;
            InitializeComponent();
            LoadNotesAsync();
        }

        private async void LoadNotesAsync()
        {
            _currentNotes = (await _noteService.GetAllNotesAsync()).ToList();
            RefreshNotesList();
            await LoadAllTagsAsync();
        }

        private async Task LoadAllTagsAsync()
        {
            var allTags = _currentNotes
                .SelectMany(n => n.Tags)
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            flpTagFilters.Controls.Clear();

            foreach (var tag in allTags)
            {
                var chkBox = new CheckBox
                {
                    Text = tag,
                    Tag = tag,
                    AutoSize = true,
                    Padding = new Padding(5)
                };
                chkBox.CheckedChanged += ChkBox_CheckedChanged;
                flpTagFilters.Controls.Add(chkBox);
            }
        }

        private void ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            var chkBox = sender as CheckBox;
            var tag = chkBox.Tag.ToString();

            if (chkBox.Checked)
            {
                if (!_selectedTags.Contains(tag))
                    _selectedTags.Add(tag);
            }
            else
            {
                _selectedTags.Remove(tag);
            }

            FilterNotesAsync();
        }

        private async void FilterNotesAsync()
        {
            if (_selectedTags.Any() || !string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                _currentNotes = (await _noteService.SearchNotesAsync(
                    txtSearch.Text,
                    _selectedTags)).ToList();
            }
            else
            {
                _currentNotes = (await _noteService.GetAllNotesAsync()).ToList();
            }

            RefreshNotesList();
        }

        private void RefreshNotesList()
        {
            lstNotes.DataSource = null;
            lstNotes.DataSource = _currentNotes;
        }

        private void LstNotes_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var listBox = sender as ListBox;
            var note = listBox.Items[e.Index] as NoteDto;

            e.DrawBackground();

            using (var titleFont = new Font(e.Font, FontStyle.Bold))
            {
                e.Graphics.DrawString(note.Title, titleFont, Brushes.Black,
                    new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 5, e.Bounds.Width - 10, 20));
            }

            var dateStr = note.UpdatedAt.ToString("dd.MM.yyyy HH:mm");
            e.Graphics.DrawString(dateStr, e.Font, Brushes.Yellow,
                new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 25, e.Bounds.Width - 10, 15));

            if (note.Tags.Any())
            {
                var tagsStr = "Tags: " + string.Join(", ", note.Tags);
                e.Graphics.DrawString(tagsStr, e.Font, Brushes.YellowGreen,
                    new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 40, e.Bounds.Width - 10, 15));
            }

            e.DrawFocusRectangle();
        }

        private void LstNotes_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 60;
        }

        private void LstNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = lstNotes.SelectedItem != null;
            btnDelete.Enabled = lstNotes.SelectedItem != null;
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            var searchTerm = txtSearch.Text;
            var tags = txtTagFilter.Text
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList();

            _currentNotes = (await _noteService.SearchNotesAsync(searchTerm, tags)).ToList();
            RefreshNotesList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new NoteForm(_noteService))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadNotesAsync();
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is NoteDto selectedNote)
            {
                using (var form = new NoteForm(_noteService, selectedNote))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadNotesAsync();
                    }
                }
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is NoteDto selectedNote)
            {
                var result = MessageBox.Show(
                    $"Delete note '{selectedNote.Title}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await _noteService.DeleteNoteAsync(selectedNote.Id);
                    LoadNotesAsync();
                }
            }
        }
    }
}