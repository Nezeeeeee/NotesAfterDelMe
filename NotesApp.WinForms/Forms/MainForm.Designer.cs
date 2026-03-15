using System.Drawing;
using System.Windows.Forms;

namespace NotesApp.WinForms.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Объявляем все поля класса
        private System.Windows.Forms.ListBox lstNotes;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtTagFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlTags;
        private System.Windows.Forms.FlowLayoutPanel flpTagFilters;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();

            System.Windows.Forms.Panel searchPanel = new System.Windows.Forms.Panel();
            System.Windows.Forms.Label lblSearch = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblTags = new System.Windows.Forms.Label();
            System.Windows.Forms.Panel actionPanel = new System.Windows.Forms.Panel();

            // MainForm
            this.Text = "Notes Manager";
            this.Size = new System.Drawing.Size(1000, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // searchPanel
            searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            searchPanel.Height = 100;
            searchPanel.Padding = new System.Windows.Forms.Padding(10);

            // lblSearch
            lblSearch.Text = "Search:";
            lblSearch.Location = new System.Drawing.Point(10, 15);
            lblSearch.Size = new System.Drawing.Size(50, 25);

            // txtSearch
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtSearch.Location = new System.Drawing.Point(70, 12);
            this.txtSearch.Size = new System.Drawing.Size(300, 25);

            // lblTags
            lblTags.Text = "Tags:";
            lblTags.Location = new System.Drawing.Point(380, 15);
            lblTags.Size = new System.Drawing.Size(40, 25);

            // txtTagFilter
            this.txtTagFilter = new System.Windows.Forms.TextBox();
            this.txtTagFilter.Location = new System.Drawing.Point(430, 12);
            this.txtTagFilter.Size = new System.Drawing.Size(200, 25);
            this.txtTagFilter.PlaceholderText = "tag1, tag2, tag3";

            // btnSearch
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearch.Text = "Search";
            this.btnSearch.Location = new System.Drawing.Point(640, 10);
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            // actionPanel
            actionPanel.Location = new System.Drawing.Point(10, 50);
            actionPanel.Size = new System.Drawing.Size(980, 40);

            // btnAdd
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAdd.Text = "Add Note";
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            // btnEdit
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnEdit.Text = "Edit";
            this.btnEdit.Location = new System.Drawing.Point(110, 0);
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

            // btnDelete
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new System.Drawing.Point(220, 0);
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // Добавляем кнопки в actionPanel
            actionPanel.Controls.Add(this.btnAdd);
            actionPanel.Controls.Add(this.btnEdit);
            actionPanel.Controls.Add(this.btnDelete);

            // Добавляем контролы в searchPanel
            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(this.txtSearch);
            searchPanel.Controls.Add(lblTags);
            searchPanel.Controls.Add(this.txtTagFilter);
            searchPanel.Controls.Add(this.btnSearch);
            searchPanel.Controls.Add(actionPanel);

            // ============ УВЕЛИЧЕННАЯ ПАНЕЛЬ С ТЕГАМИ ============
            this.pnlTags = new System.Windows.Forms.Panel();
            this.pnlTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTags.Height = 120; // Увеличено с 50 до 120
            this.pnlTags.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTags.BackColor = System.Drawing.Color.WhiteSmoke;

            // Заголовок для тегов
            System.Windows.Forms.Label lblTagsTitle = new System.Windows.Forms.Label();
            lblTagsTitle.Text = "Фильтр по тегам (выберите один или несколько):";
            lblTagsTitle.Location = new System.Drawing.Point(10, 5);
            lblTagsTitle.Size = new System.Drawing.Size(400, 20);
            lblTagsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold);
            this.pnlTags.Controls.Add(lblTagsTitle);

            // Кнопка для сброса фильтра
            System.Windows.Forms.Button btnClearTags = new System.Windows.Forms.Button();
            btnClearTags.Text = "Сбросить фильтр";
            btnClearTags.Location = new System.Drawing.Point(830, 3);
            btnClearTags.Size = new System.Drawing.Size(120, 25);
            btnClearTags.Click += new System.EventHandler(this.BtnClearTags_Click);
            this.pnlTags.Controls.Add(btnClearTags);

            // Панель с тегами
            this.flpTagFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.flpTagFilters.Location = new System.Drawing.Point(10, 30);
            this.flpTagFilters.Size = new System.Drawing.Size(960, 80); // Фиксированный размер
            this.flpTagFilters.AutoScroll = true;
            this.flpTagFilters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.flpTagFilters.BackColor = System.Drawing.Color.White;
            this.flpTagFilters.Padding = new System.Windows.Forms.Padding(5);
            this.pnlTags.Controls.Add(this.flpTagFilters);
            // ====================================================

            // lstNotes
            this.lstNotes = new System.Windows.Forms.ListBox();
            this.lstNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNotes.DisplayMember = "Title";
            this.lstNotes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstNotes.IntegralHeight = false;
            this.lstNotes.ItemHeight = 60;
            this.lstNotes.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstNotes_DrawItem);
            this.lstNotes.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.LstNotes_MeasureItem);
            this.lstNotes.SelectedIndexChanged += new System.EventHandler(this.LstNotes_SelectedIndexChanged);

            // Добавляем контролы на форму
            this.Controls.Add(this.lstNotes);
            this.Controls.Add(this.pnlTags);
            this.Controls.Add(searchPanel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}