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

            // Подписываемся на двойной клик
            this.lstNotes.DoubleClick += LstNotes_DoubleClick;

            // Устанавливаем начальный язык в соответствии с выбранным в комбобоксе
            SetInitialLanguage();

            LoadNotesAsync();
        }

        private void SetInitialLanguage()
        {
            // Устанавливаем язык в соответствии с выбранным в комбобоксе
            if (cmbLanguage.SelectedIndex == 0) // Русский
            {
                LocalizationManager.CurrentLanguage = "ru";
            }
            else // English
            {
                LocalizationManager.CurrentLanguage = "en";
            }

            // Обновляем тексты на форме
            UpdateUILanguage();
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

            // Добавляем заголовок "Все теги:" с учетом языка
            var lblAllTags = new Label
            {
                Text = LocalizationManager.GetString("AllTags"),
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(5),
                ForeColor = Color.Gray
            };
            flpTagFilters.Controls.Add(lblAllTags);

            // Добавляем сами теги
            foreach (var tag in allTags)
            {
                var chkBox = new CheckBox
                {
                    Text = tag,
                    Tag = tag,
                    AutoSize = true,
                    Padding = new Padding(5),
                    Margin = new Padding(3),
                    BackColor = Color.LightGoldenrodYellow,
                    FlatStyle = FlatStyle.Flat
                };

                // Добавляем контекстное меню для удаления тега
                var contextMenu = new ContextMenuStrip();
                var deleteMenuItem = new ToolStripMenuItem(LocalizationManager.GetString("Delete"));
                deleteMenuItem.Click += (s, e) => DeleteTag(tag);
                contextMenu.Items.Add(deleteMenuItem);
                chkBox.ContextMenuStrip = contextMenu;

                chkBox.CheckedChanged += ChkBox_CheckedChanged;
                flpTagFilters.Controls.Add(chkBox);
            }

            // Если тегов нет
            if (!allTags.Any())
            {
                var lblNoTags = new Label
                {
                    Text = LocalizationManager.GetString("NoTags"),
                    AutoSize = true,
                    Padding = new Padding(5),
                    ForeColor = Color.Gray
                };
                flpTagFilters.Controls.Add(lblNoTags);
            }
        }

        private async void DeleteTag(string tagName)
        {
            var result = MessageBox.Show(
                LocalizationManager.GetString("DeleteTagConfirm", tagName),
                LocalizationManager.GetString("ConfirmDelete"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var notesWithTag = _currentNotes.Where(n => n.Tags.Contains(tagName)).ToList();

                    foreach (var note in notesWithTag)
                    {
                        var updateDto = new UpdateNoteDto
                        {
                            Id = note.Id,
                            Title = note.Title,
                            Content = note.Content,
                            Tags = note.Tags.Where(t => t != tagName).ToList()
                        };
                        await _noteService.UpdateNoteAsync(updateDto);
                    }

                    MessageBox.Show(
                        LocalizationManager.GetString("DeleteTagSuccess", tagName, notesWithTag.Count),
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadNotesAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        LocalizationManager.GetString("DeleteTagError", ex.Message),
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
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

            // Проверяем, выбран ли элемент (наведен или выделен)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // Заливаем фон выбранного элемента нужным цветом
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGoldenrodYellow), e.Bounds);
            }
            else
            {
                // Стандартный фон для невыбранных элементов
                e.DrawBackground();
            }

            // Заголовок (жирным шрифтом)
            using (var titleFont = new Font(e.Font, FontStyle.Bold))
            {
                e.Graphics.DrawString(note.Title, titleFont, Brushes.Black,
                    new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 5, e.Bounds.Width - 10, 20));
            }

            // Дата создания/обновления
            var dateStr = note.UpdatedAt.ToString("dd.MM.yyyy HH:mm");
            e.Graphics.DrawString(dateStr, e.Font, Brushes.Brown,
                new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 25, e.Bounds.Width - 10, 15));

            // Контент (содержимое заметки)
            string content = note.Content ?? "";
            if (content.Length > 50)
                content = content.Substring(0, 50) + "...";

            e.Graphics.DrawString(content, e.Font, Brushes.Black,
                new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 40, e.Bounds.Width - 10, 20));

            // Теги
            if (note.Tags.Any())
            {
                string tagsStr;
                if (note.Tags.Count > 5)
                {
                    var firstFive = string.Join(", ", note.Tags.Take(5));
                    tagsStr = $"Tags: {firstFive} ... и еще {note.Tags.Count - 5}";
                }
                else
                {
                    tagsStr = "Tags: " + string.Join(", ", note.Tags);
                }

                e.Graphics.DrawString(tagsStr, e.Font, Brushes.DarkBlue,
                    new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 62, e.Bounds.Width - 10, 15));
            }

            e.DrawFocusRectangle();
        }

        private void LstNotes_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 80;
        }

        private void LstNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = lstNotes.SelectedItem != null;
            btnDelete.Enabled = lstNotes.SelectedItem != null;
        }

        private void LstNotes_DoubleClick(object sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is NoteDto selectedNote)
            {
                using (var viewForm = new ViewNoteForm(selectedNote))
                {
                    viewForm.ShowDialog();
                }
            }
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
                    LocalizationManager.GetString("DeleteNoteConfirm", selectedNote.Title),
                    LocalizationManager.GetString("ConfirmDelete"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await _noteService.DeleteNoteAsync(selectedNote.Id);
                    LoadNotesAsync();
                }
            }
        }

        private void BtnClearTags_Click(object sender, EventArgs e)
        {
            // Сбрасываем все выбранные теги
            _selectedTags.Clear();

            // Снимаем отметки со всех CheckBox
            foreach (Control control in flpTagFilters.Controls)
            {
                if (control is CheckBox chkBox)
                {
                    chkBox.Checked = false;
                }
            }

            // Очищаем поле поиска
            txtSearch.Text = "";
            txtTagFilter.Text = "";

            // Загружаем все заметки
            LoadNotesAsync();
        }

        private void CmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLanguage.SelectedIndex == 0) // Русский
            {
                LocalizationManager.CurrentLanguage = "ru";
            }
            else // English
            {
                LocalizationManager.CurrentLanguage = "en";
            }

            // Обновляем тексты на форме
            UpdateUILanguage();
        }

        private void UpdateUILanguage()
        {
            // MainForm
            this.Text = LocalizationManager.GetString("MainFormTitle");
            lblSearch.Text = LocalizationManager.GetString("Search");
            lblTags.Text = LocalizationManager.GetString("Tags");
            btnSearch.Text = LocalizationManager.GetString("SearchButton");
            btnAdd.Text = LocalizationManager.GetString("AddNote");
            btnEdit.Text = LocalizationManager.GetString("Edit");
            btnDelete.Text = LocalizationManager.GetString("Delete");
            lblTagsTitle.Text = LocalizationManager.GetString("TagsFilterTitle");
            btnClearTags.Text = LocalizationManager.GetString("ClearFilter");

            // Обновляем заголовок "Все теги" в панели фильтров
            LoadAllTagsAsync();
        }
    }
}