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

            // Подписываемся на изменение языка и темы
            LocalizationManager.LanguageChanged += OnLanguageChanged;
            LocalizationManager.ThemeChanged += OnThemeChanged;

            LoadNote();
            ApplyTheme();
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            LoadNote();
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadNote();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LocalizationManager.LanguageChanged -= OnLanguageChanged;
            LocalizationManager.ThemeChanged -= OnThemeChanged;
            base.OnFormClosed(e);
        }

        public void ApplyTheme()
        {
            this.BackColor = LocalizationManager.GetColor("Background");
            this.ForeColor = LocalizationManager.GetColor("Foreground");

            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    panel.BackColor = LocalizationManager.GetColor("HeaderBackground");
                    panel.ForeColor = LocalizationManager.GetColor("Foreground");
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = LocalizationManager.GetColor("InputBackground");
                    textBox.ForeColor = LocalizationManager.GetColor("InputForeground");
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is Button button)
                {
                    button.BackColor = LocalizationManager.GetColor("ButtonBackground");
                    button.ForeColor = LocalizationManager.GetColor("ButtonText");
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = LocalizationManager.GetColor("BorderColor");
                    button.FlatAppearance.MouseOverBackColor = LocalizationManager.GetColor("ButtonHover");
                }
                else if (control is Label label && label != lblTitle)
                {
                    label.ForeColor = LocalizationManager.GetColor("Foreground");
                }
            }

            // Заголовок всегда контрастный
            if (LocalizationManager.CurrentTheme == "dark")
            {
                lblTitle.ForeColor = Color.White;
                pnlHeader.BackColor = Color.FromArgb(70, 70, 70);
            }
            else
            {
                lblTitle.ForeColor = Color.Black;
                pnlHeader.BackColor = Color.LightSteelBlue;
            }
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

            flpTags.Controls.Clear();

            var lblTagsHeader = new Label
            {
                Text = LocalizationManager.GetString("Tags_Header"),
                Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(5),
                ForeColor = LocalizationManager.GetColor("Foreground"),
                Margin = new Padding(3, 3, 10, 3)
            };
            flpTags.Controls.Add(lblTagsHeader);

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
                        BackColor = LocalizationManager.GetColor("TagBackground"),
                        ForeColor = LocalizationManager.GetColor("TagText"),
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
                    ForeColor = LocalizationManager.GetColor("DateText"),
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
                    ForeColor = LocalizationManager.GetColor("DateText"),
                    Font = new Font("Microsoft Sans Serif", 10),
                    BackColor = LocalizationManager.GetColor("PanelBackground"),
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