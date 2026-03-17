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
        // ❌ УДАЛЕНО: private System.Windows.Forms.ComboBox cmbTheme; (это поле уже есть в Designer.cs)

        public MainForm(INoteService noteService)
        {
            _noteService = noteService;
            InitializeComponent();

            // Подписываемся на двойной клик
            this.lstNotes.DoubleClick += LstNotes_DoubleClick;

            // Подписываемся на изменение темы
            LocalizationManager.ThemeChanged += OnThemeChanged;

            // Устанавливаем начальный язык
            SetInitialLanguage();

            LoadNotesAsync(); // Может быть проблема, если LocalizationManager не инициализирован
        }

        private void SetInitialLanguage()
        {
            if (cmbLanguage.SelectedIndex == 0)
            {
                LocalizationManager.CurrentLanguage = "ru";
            }
            else
            {
                LocalizationManager.CurrentLanguage = "en";
            }
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

            var lblAllTags = new Label
            {
                Text = LocalizationManager.GetString("AllTags"),
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(5),
                ForeColor = LocalizationManager.GetColor("Foreground")
            };
            flpTagFilters.Controls.Add(lblAllTags);

            foreach (var tag in allTags)
            {
                var chkBox = new CheckBox
                {
                    Text = tag,
                    Tag = tag,
                    AutoSize = true,
                    Padding = new Padding(5),
                    Margin = new Padding(3),
                    BackColor = LocalizationManager.GetColor("TagBackground"),
                    ForeColor = LocalizationManager.GetColor("TagText"),
                    FlatStyle = FlatStyle.Flat
                };

                var contextMenu = new ContextMenuStrip();
                var deleteMenuItem = new ToolStripMenuItem(LocalizationManager.GetString("Delete"));
                deleteMenuItem.Click += (s, e) => DeleteTag(tag);
                contextMenu.Items.Add(deleteMenuItem);
                chkBox.ContextMenuStrip = contextMenu;

                chkBox.CheckedChanged += ChkBox_CheckedChanged;
                flpTagFilters.Controls.Add(chkBox);
            }

            if (!allTags.Any())
            {
                var lblNoTags = new Label
                {
                    Text = LocalizationManager.GetString("NoTags"),
                    AutoSize = true,
                    Padding = new Padding(5),
                    ForeColor = LocalizationManager.GetColor("Foreground")
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

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(LocalizationManager.GetColor("SelectedItem")), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(LocalizationManager.GetColor("Background")), e.Bounds);
            }

            using (var titleFont = new Font(e.Font, FontStyle.Bold))
            {
                e.Graphics.DrawString(note.Title, titleFont, new SolidBrush(LocalizationManager.GetColor("Foreground")),
                    new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 5, e.Bounds.Width - 10, 20));
            }

            var dateStr = note.UpdatedAt.ToString("dd.MM.yyyy HH:mm");
            e.Graphics.DrawString(dateStr, e.Font, new SolidBrush(LocalizationManager.GetColor("DateText")),
                new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 25, e.Bounds.Width - 10, 15));

            string content = note.Content ?? "";
            if (content.Length > 50)
                content = content.Substring(0, 50) + "...";

            e.Graphics.DrawString(content, e.Font, new SolidBrush(LocalizationManager.GetColor("Foreground")),
                new RectangleF(e.Bounds.X + 5, e.Bounds.Y + 40, e.Bounds.Width - 10, 20));

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

                e.Graphics.DrawString(tagsStr, e.Font, new SolidBrush(LocalizationManager.GetColor("TagText")),
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
            _selectedTags.Clear();

            foreach (Control control in flpTagFilters.Controls)
            {
                if (control is CheckBox chkBox)
                {
                    chkBox.Checked = false;
                }
            }

            txtSearch.Text = "";
            txtTagFilter.Text = "";

            LoadNotesAsync();
        }

        private void CmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLanguage.SelectedIndex == 0)
            {
                LocalizationManager.CurrentLanguage = "ru";
            }
            else
            {
                LocalizationManager.CurrentLanguage = "en";
            }

            UpdateUILanguage();
        }

        private void UpdateUILanguage()
        {
            this.Text = LocalizationManager.GetString("MainFormTitle");
            lblSearch.Text = LocalizationManager.GetString("Search");
            lblTags.Text = LocalizationManager.GetString("Tags");
            btnSearch.Text = LocalizationManager.GetString("SearchButton");
            btnAdd.Text = LocalizationManager.GetString("AddNote");
            btnEdit.Text = LocalizationManager.GetString("Edit");
            btnDelete.Text = LocalizationManager.GetString("Delete");
            lblTagsTitle.Text = LocalizationManager.GetString("TagsFilterTitle");
            btnClearTags.Text = LocalizationManager.GetString("ClearFilter");

            // Обновляем меню
            fileMenu.Text = LocalizationManager.GetString("File");
            aboutMenuItem.Text = LocalizationManager.GetString("About");

            if (cmbTheme != null)
            {
                int selectedIndex = cmbTheme.SelectedIndex;
                cmbTheme.Items.Clear();
                cmbTheme.Items.AddRange(new object[] {
            LocalizationManager.GetString("LightTheme"),
            LocalizationManager.GetString("DarkTheme")
        });

                if (selectedIndex >= 0 && selectedIndex < cmbTheme.Items.Count)
                {
                    cmbTheme.SelectedIndex = selectedIndex;
                }
                else
                {
                    cmbTheme.SelectedIndex = 0;
                }
            }

            LoadAllTagsAsync();
        }

        private void CmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTheme.SelectedIndex == 0)
            {
                LocalizationManager.CurrentTheme = "light";
            }
            else
            {
                LocalizationManager.CurrentTheme = "dark";
            }
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            ApplyThemeToAllForms();
        }

        private void ApplyThemeToAllForms()
        {
            // Применяем тему к главной форме
            ApplyTheme(this);

            // Применяем тему ко всем открытым формам
            // Используем полное имя System.Windows.Forms.Application.OpenForms
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
            {
                if (form != this)
                {
                    if (form is NoteForm noteForm)
                    {
                        ApplyTheme(noteForm);
                    }
                    else if (form is ViewNoteForm viewForm)
                    {
                        ApplyTheme(viewForm);
                    }
                }
            }
        }

        private void ApplyTheme(Form form)
        {
            if (form is MainForm mainForm)
            {
                mainForm.BackColor = LocalizationManager.GetColor("Background");
                mainForm.ForeColor = LocalizationManager.GetColor("Foreground");

                foreach (Control control in mainForm.Controls)
                {
                    if (control is Panel panel)
                    {
                        panel.BackColor = LocalizationManager.GetColor("PanelBackground");
                        panel.ForeColor = LocalizationManager.GetColor("Foreground");
                    }
                    else if (control is ListBox listBox)
                    {
                        listBox.BackColor = LocalizationManager.GetColor("InputBackground");
                        listBox.ForeColor = LocalizationManager.GetColor("InputForeground");
                    }
                    else if (control is Button button)
                    {
                        button.BackColor = LocalizationManager.GetColor("ButtonBackground");
                        button.ForeColor = LocalizationManager.GetColor("ButtonText");
                        button.FlatAppearance.MouseOverBackColor = LocalizationManager.GetColor("ButtonHover");
                    }
                    else if (control is TextBox textBox)
                    {
                        textBox.BackColor = LocalizationManager.GetColor("InputBackground");
                        textBox.ForeColor = LocalizationManager.GetColor("InputForeground");
                    }
                }

                if (mainForm.pnlTags != null)
                {
                    mainForm.pnlTags.BackColor = LocalizationManager.GetColor("PanelBackground");
                }

                mainForm.lstNotes.Invalidate();
            }
            else if (form is NoteForm noteForm)
            {
                noteForm.ApplyTheme();
            }
            else if (form is ViewNoteForm viewForm)
            {
                viewForm.ApplyTheme();
            }
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }
    }
}