using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NotesApp.Application.DTOs;

namespace NotesApp.WinForms.Forms
{
    public class NoteCard : UserControl
    {
        private readonly NoteDto _note;
        private Panel pnlCard;
        private Label lblTitle;
        private Label lblDate;
        private Label lblContent;
        private FlowLayoutPanel flpTags;
        private bool _isSelected;

        // Кнопки как поля класса
        private Button btnEdit;
        private Button btnDelete;

        public NoteDto Note => _note;

        public event EventHandler<NoteDto> NoteClick;
        public event EventHandler<NoteDto> NoteDoubleClick;
        public event EventHandler<NoteDto> EditClick;
        public event EventHandler<NoteDto> DeleteClick;

        // Добавляем атрибуты для игнорирования дизайнером
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                UpdateSelection();
            }
        }

        public NoteCard(NoteDto note)
        {
            _note = note;
            InitializeCard();
            UpdateTheme();

            // Подписываемся на события
            this.Click += OnCardClick;
            this.DoubleClick += OnCardDoubleClick;
        }

        private void InitializeCard()
        {
            // Размер карточки
            this.Size = new Size(220, 210);
            this.MinimumSize = new Size(200, 190);

            // Основная панель карточки
            this.pnlCard = new Panel();
            this.pnlCard.Dock = DockStyle.Fill;
            this.pnlCard.Padding = new Padding(10);
            this.pnlCard.BorderStyle = BorderStyle.FixedSingle;
            this.pnlCard.Click += OnCardClick;
            this.pnlCard.DoubleClick += OnCardDoubleClick;

            // Заголовок
            this.lblTitle = new Label();
            this.lblTitle.Text = _note.Title;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            this.lblTitle.Location = new Point(10, 10);
            this.lblTitle.Size = new Size(180, 20);
            this.lblTitle.Click += OnCardClick;
            this.lblTitle.DoubleClick += OnCardDoubleClick;

            // Дата
            this.lblDate = new Label();
            this.lblDate.Text = _note.UpdatedAt.ToString("dd.MM.yyyy HH:mm");
            this.lblDate.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Italic);
            this.lblDate.ForeColor = Color.Gray;
            this.lblDate.Location = new Point(10, 35);
            this.lblDate.Size = new Size(180, 15);
            this.lblDate.Click += OnCardClick;
            this.lblDate.DoubleClick += OnCardDoubleClick;

            // Контент
            this.lblContent = new Label();
            string content = _note.Content ?? "";
            if (content.Length > 100)
                content = content.Substring(0, 100) + "...";
            this.lblContent.Text = content;
            this.lblContent.Font = new Font("Microsoft Sans Serif", 9);
            this.lblContent.Location = new Point(10, 55);
            this.lblContent.Size = new Size(180, 40);
            this.lblContent.Click += OnCardClick;
            this.lblContent.DoubleClick += OnCardDoubleClick;

            // Панель тегов
            this.flpTags = new FlowLayoutPanel();
            this.flpTags.AutoScroll = true;
            this.flpTags.Location = new Point(10, 100);
            this.flpTags.Size = new Size(180, 50);
            this.flpTags.Padding = new Padding(2);
            this.flpTags.Click += OnCardClick;
            this.flpTags.DoubleClick += OnCardDoubleClick;

            // Добавляем теги
            foreach (var tag in _note.Tags)
            {
                var lblTag = new Label();
                lblTag.Text = tag;
                lblTag.AutoSize = true;
                lblTag.Padding = new Padding(3);
                lblTag.Margin = new Padding(2);
                lblTag.BackColor = Color.LightSteelBlue;
                lblTag.ForeColor = Color.Black;
                lblTag.BorderStyle = BorderStyle.FixedSingle;
                lblTag.Font = new Font("Microsoft Sans Serif", 7);
                lblTag.Click += OnCardClick;
                lblTag.DoubleClick += OnCardDoubleClick;
                flpTags.Controls.Add(lblTag);
            }

            // Кнопка редактирования
            this.btnEdit = new Button();
            this.btnEdit.Text = "✏️";
            this.btnEdit.Location = new Point(130, 155);
            this.btnEdit.Size = new Size(30, 25);
            this.btnEdit.FlatStyle = FlatStyle.Flat;
            this.btnEdit.Click += (s, e) => EditClick?.Invoke(this, _note);

            // Кнопка удаления
            this.btnDelete = new Button();
            this.btnDelete.Text = "🗑️";
            this.btnDelete.Location = new Point(165, 155);
            this.btnDelete.Size = new Size(30, 25);
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Click += (s, e) => DeleteClick?.Invoke(this, _note);

            // Добавляем все контролы
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblDate);
            pnlCard.Controls.Add(lblContent);
            pnlCard.Controls.Add(flpTags);
            pnlCard.Controls.Add(btnEdit);
            pnlCard.Controls.Add(btnDelete);

            this.Controls.Add(pnlCard);
        }

        private void OnCardClick(object sender, EventArgs e)
        {
            NoteClick?.Invoke(this, _note);
        }

        private void OnCardDoubleClick(object sender, EventArgs e)
        {
            NoteDoubleClick?.Invoke(this, _note);
        }

        private void UpdateSelection()
        {
            if (pnlCard != null)
            {
                if (_isSelected)
                {
                    pnlCard.BackColor = LocalizationManager.GetColor("SelectedItem");
                }
                else
                {
                    pnlCard.BackColor = LocalizationManager.GetColor("PanelBackground");
                }
            }
        }

        public void UpdateTheme()
        {
            if (pnlCard != null)
            {
                this.BackColor = LocalizationManager.GetColor("Background");
                pnlCard.BackColor = _isSelected ?
                    LocalizationManager.GetColor("SelectedItem") :
                    LocalizationManager.GetColor("PanelBackground");

                if (lblTitle != null) lblTitle.ForeColor = LocalizationManager.GetColor("Foreground");
                if (lblContent != null) lblContent.ForeColor = LocalizationManager.GetColor("Foreground");

                if (flpTags != null)
                {
                    foreach (Control control in flpTags.Controls)
                    {
                        if (control is Label lblTag)
                        {
                            lblTag.BackColor = LocalizationManager.GetColor("TagBackground");
                            lblTag.ForeColor = LocalizationManager.GetColor("TagText");
                        }
                    }
                }

                if (btnEdit != null)
                {
                    btnEdit.BackColor = LocalizationManager.GetColor("ButtonBackground");
                    btnEdit.ForeColor = LocalizationManager.GetColor("ButtonText");
                }

                if (btnDelete != null)
                {
                    btnDelete.BackColor = LocalizationManager.GetColor("ButtonBackground");
                    btnDelete.ForeColor = LocalizationManager.GetColor("ButtonText");
                }
            }
        }

        // Защищаем от сериализации дизайнером
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }
    }
}