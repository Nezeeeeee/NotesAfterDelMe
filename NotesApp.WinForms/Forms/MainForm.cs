using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.WinForms.Services;

namespace NotesApp.WinForms.Forms
{
    public partial class MainForm : Form
    {
        private readonly INoteService _noteService;
        private List<NoteDto> _currentNotes = new List<NoteDto>();
        private List<string> _selectedTags = new List<string>();
        private NoteCard _selectedCard;

        public MainForm(INoteService noteService)
        {
            try
            {
                _noteService = noteService;
                InitializeComponent();

                // Подписываемся на изменение темы
                LocalizationManager.ThemeChanged += OnThemeChanged;

                // Устанавливаем начальный язык
                SetInitialLanguage();

                LoadNotesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации: {ex.Message}\n{ex.StackTrace}");
            }
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
            UpdateSearchResultInfo();
            UpdateSelectedTagsDisplay();
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
            if (chkBox == null) return;

            var tag = chkBox.Tag?.ToString();
            if (string.IsNullOrEmpty(tag)) return;

            if (chkBox.Checked)
            {
                if (!_selectedTags.Contains(tag))
                    _selectedTags.Add(tag);
            }
            else
            {
                _selectedTags.Remove(tag);
            }

            UpdateSelectedTagsDisplay();
            FilterNotesAsync();
        }

        private async void FilterNotesAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                List<NoteDto> filteredNotes;

                if (_selectedTags.Any() || !string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    filteredNotes = (await _noteService.SearchNotesAsync(
                        txtSearch.Text,
                        _selectedTags)).ToList();
                }
                else
                {
                    filteredNotes = (await _noteService.GetAllNotesAsync()).ToList();
                }

                _currentNotes = filteredNotes;
                RefreshNotesList();
                UpdateSearchResultInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void RefreshNotesList()
        {
            flpNotes.Controls.Clear();
            _selectedCard = null;
            UpdateButtonsState();

            foreach (var note in _currentNotes)
            {
                var card = new NoteCard(note);
                card.Margin = new Padding(10);
                card.NoteClick += OnNoteCardClick;
                card.NoteDoubleClick += OnNoteCardDoubleClick;
                card.EditClick += OnNoteCardEdit;
                card.DeleteClick += OnNoteCardDelete;
                flpNotes.Controls.Add(card);
            }
        }

        private void OnNoteCardClick(object sender, NoteDto note)
        {
            // Снимаем выделение с предыдущей карточки
            if (_selectedCard != null)
            {
                _selectedCard.IsSelected = false;
            }

            // Выделяем новую карточку
            _selectedCard = sender as NoteCard;
            if (_selectedCard != null)
            {
                _selectedCard.IsSelected = true;
            }

            UpdateButtonsState();
        }

        private void OnNoteCardDoubleClick(object sender, NoteDto note)
        {
            using (var viewForm = new ViewNoteForm(note))
            {
                viewForm.ShowDialog();
            }
        }

        private void OnNoteCardEdit(object sender, NoteDto note)
        {
            using (var form = new NoteForm(_noteService, note))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadNotesAsync();
                }
            }
        }

        private async void OnNoteCardDelete(object sender, NoteDto note)
        {
            var result = MessageBox.Show(
                LocalizationManager.GetString("DeleteNoteConfirm", note.Title),
                LocalizationManager.GetString("ConfirmDelete"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                await _noteService.DeleteNoteAsync(note.Id);
                LoadNotesAsync();
            }
        }

        private void UpdateButtonsState()
        {
            btnEdit.Enabled = _selectedCard != null;
            btnDelete.Enabled = _selectedCard != null;
        }

        private void UpdateSearchResultInfo()
        {
            if (lblSearchInfo == null) return;

            string infoText;

            if (_selectedTags.Any() || !string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                if (_currentNotes.Count == 0)
                {
                    infoText = LocalizationManager.GetString("NoNotesFound");
                }
                else
                {
                    string notesCount = _currentNotes.Count.ToString();
                    string tagsCount = _selectedTags.Count.ToString();

                    if (LocalizationManager.CurrentLanguage == "ru")
                        infoText = $"Найдено заметок: {notesCount} | Выбрано тегов: {tagsCount}";
                    else
                        infoText = $"Found notes: {notesCount} | Selected tags: {tagsCount}";
                }
            }
            else
            {
                string totalNotes = _currentNotes.Count.ToString();

                if (LocalizationManager.CurrentLanguage == "ru")
                    infoText = $"Всего заметок: {totalNotes}";
                else
                    infoText = $"Total notes: {totalNotes}";
            }

            lblSearchInfo.Text = infoText;
        }

        private void UpdateSelectedTagsDisplay()
        {
            if (lblSelectedTags == null) return;

            if (_selectedTags.Any())
            {
                string selectedTagsText = string.Join(", ", _selectedTags);

                if (LocalizationManager.CurrentLanguage == "ru")
                    lblSelectedTags.Text = $"Выбрано: {selectedTagsText}";
                else
                    lblSelectedTags.Text = $"Selected: {selectedTagsText}";

                lblSelectedTags.Visible = true;
                lblSelectedTags.Invalidate();
                lblSelectedTags.Refresh();
            }
            else
            {
                lblSelectedTags.Visible = false;
                lblSelectedTags.Text = "";
            }
        }

        private void FlpNotes_Resize(object sender, EventArgs e)
        {
            // Перестраиваем карточки при изменении размера
            flpNotes.SuspendLayout();
            flpNotes.ResumeLayout();
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            var searchTerm = txtSearch.Text;
            var tags = txtTagFilter.Text
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList();

            _selectedTags = tags;
            UpdateSelectedTagsDisplay();

            _currentNotes = (await _noteService.SearchNotesAsync(searchTerm, tags)).ToList();
            RefreshNotesList();
            UpdateSearchResultInfo();
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
            if (_selectedCard != null)
            {
                OnNoteCardEdit(_selectedCard, _selectedCard.Note);
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCard != null)
            {
                await _noteService.DeleteNoteAsync(_selectedCard.Note.Id);
                LoadNotesAsync();
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
            UpdateSelectedTagsDisplay();
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

            if (fileMenu != null)
                fileMenu.Text = LocalizationManager.GetString("File");
            if (exportMenu != null)
                exportMenu.Text = LocalizationManager.GetString("Export");
            if (exportAllNotesMenuItem != null)
                exportAllNotesMenuItem.Text = LocalizationManager.GetString("ExportAllNotes");
            if (exportFilteredMenuItem != null)
                exportFilteredMenuItem.Text = LocalizationManager.GetString("ExportFiltered");
            if (aboutMenuItem != null)
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

            UpdateSearchResultInfo();
            UpdateSelectedTagsDisplay();
            LoadAllTagsAsync();

            // Обновляем темы карточек
            foreach (Control control in flpNotes.Controls)
            {
                if (control is NoteCard card)
                {
                    card.UpdateTheme();
                }
            }
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
            ApplyTheme(this);

            foreach (Form form in System.Windows.Forms.Application.OpenForms)
            {
                if (form != this)
                {
                    if (form is NoteForm noteForm)
                    {
                        noteForm.ApplyTheme();
                    }
                    else if (form is ViewNoteForm viewForm)
                    {
                        viewForm.ApplyTheme();
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
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.BackColor = LocalizationManager.GetColor("InputBackground");
                        comboBox.ForeColor = LocalizationManager.GetColor("InputForeground");
                    }
                    else if (control is FlowLayoutPanel flp && flp != flpNotes)
                    {
                        flp.BackColor = LocalizationManager.GetColor("PanelBackground");
                    }
                }

                if (mainForm.pnlTags != null)
                {
                    mainForm.pnlTags.BackColor = LocalizationManager.GetColor("PanelBackground");
                }

                // Обновляем карточки
                foreach (Control control in flpNotes.Controls)
                {
                    if (control is NoteCard card)
                    {
                        card.UpdateTheme();
                    }
                }
            }
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        private void ExportAllNotesMenuItem_Click(object sender, EventArgs e)
        {
            ExportNotes(_currentNotes, "all_notes");
        }

        private void ExportFilteredMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedTags.Any() || !string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                ExportNotes(_currentNotes, "filtered_notes");
            }
            else
            {
                MessageBox.Show(
                    LocalizationManager.GetString("NoFilterActive"),
                    LocalizationManager.GetString("Information"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void ExportNotes(List<NoteDto> notes, string defaultName)
        {
            if (notes == null || notes.Count == 0)
            {
                MessageBox.Show(
                    LocalizationManager.GetString("NoNotesToExport"),
                    LocalizationManager.GetString("Information"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = $"{LocalizationManager.GetString("ExcelFiles")} (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.DefaultExt = "xlsx";
                saveDialog.FileName = $"{defaultName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportService.ExportNotesToExcel(notes, saveDialog.FileName);

                        DialogResult result = MessageBox.Show(
                            LocalizationManager.GetString("ExcelExported") + "\n\n" + LocalizationManager.GetString("OpenFileQuestion"),
                            LocalizationManager.GetString("Success"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                using (var process = new System.Diagnostics.Process())
                                {
                                    process.StartInfo.FileName = saveDialog.FileName;
                                    process.StartInfo.UseShellExecute = true;
                                    process.Start();
                                }
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start("explorer.exe", $"\"{saveDialog.FileName}\"");
                                }
                                catch
                                {
                                    MessageBox.Show(
                                        $"Файл сохранен по адресу:\n{saveDialog.FileName}",
                                        LocalizationManager.GetString("Information"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            string.Format(LocalizationManager.GetString("ExcelExportError"), ex.Message),
                            LocalizationManager.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}