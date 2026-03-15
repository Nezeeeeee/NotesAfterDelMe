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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            searchPanel = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblTags = new Label();
            txtTagFilter = new TextBox();
            btnSearch = new Button();
            actionPanel = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            pnlTags = new Panel();
            lblTagsTitle = new Label();
            btnClearTags = new Button();
            flpTagFilters = new FlowLayoutPanel();
            lstNotes = new ListBox();
            searchPanel.SuspendLayout();
            actionPanel.SuspendLayout();
            pnlTags.SuspendLayout();
            SuspendLayout();
            // 
            // searchPanel
            // 
            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(lblTags);
            searchPanel.Controls.Add(txtTagFilter);
            searchPanel.Controls.Add(btnSearch);
            searchPanel.Controls.Add(actionPanel);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.Padding = new Padding(10);
            searchPanel.Size = new Size(984, 100);
            searchPanel.TabIndex = 2;
            // 
            // lblSearch
            // 
            lblSearch.Location = new Point(10, 15);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(50, 25);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(70, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 23);
            txtSearch.TabIndex = 1;
            // 
            // lblTags
            // 
            lblTags.Location = new Point(380, 15);
            lblTags.Name = "lblTags";
            lblTags.Size = new Size(40, 25);
            lblTags.TabIndex = 2;
            lblTags.Text = "Tags:";
            // 
            // txtTagFilter
            // 
            txtTagFilter.Location = new Point(430, 12);
            txtTagFilter.Name = "txtTagFilter";
            txtTagFilter.PlaceholderText = "tag1, tag2, tag3";
            txtTagFilter.Size = new Size(200, 23);
            txtTagFilter.TabIndex = 3;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(640, 10);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 30);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Search";
            btnSearch.Click += BtnSearch_Click;
            // 
            // actionPanel
            // 
            actionPanel.Controls.Add(btnAdd);
            actionPanel.Controls.Add(btnEdit);
            actionPanel.Controls.Add(btnDelete);
            actionPanel.Location = new Point(10, 50);
            actionPanel.Name = "actionPanel";
            actionPanel.Size = new Size(980, 40);
            actionPanel.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(0, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 30);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add Note";
            btnAdd.Click += BtnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(110, 0);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 30);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edit";
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(220, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 30);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.Click += BtnDelete_Click;
            // 
            // pnlTags
            // 
            pnlTags.BackColor = Color.WhiteSmoke;
            pnlTags.BorderStyle = BorderStyle.FixedSingle;
            pnlTags.Controls.Add(lblTagsTitle);
            pnlTags.Controls.Add(btnClearTags);
            pnlTags.Controls.Add(flpTagFilters);
            pnlTags.Dock = DockStyle.Top;
            pnlTags.Location = new Point(0, 100);
            pnlTags.Name = "pnlTags";
            pnlTags.Padding = new Padding(10);
            pnlTags.Size = new Size(984, 120);
            pnlTags.TabIndex = 1;
            // 
            // lblTagsTitle
            // 
            lblTagsTitle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblTagsTitle.Location = new Point(10, 5);
            lblTagsTitle.Name = "lblTagsTitle";
            lblTagsTitle.Size = new Size(400, 20);
            lblTagsTitle.TabIndex = 0;
            lblTagsTitle.Text = "Фильтр по тегам (выберите один или несколько):";
            // 
            // btnClearTags
            // 
            btnClearTags.Location = new Point(830, 3);
            btnClearTags.Name = "btnClearTags";
            btnClearTags.Size = new Size(120, 25);
            btnClearTags.TabIndex = 1;
            btnClearTags.Text = "Сбросить фильтр";
            btnClearTags.Click += BtnClearTags_Click;
            // 
            // flpTagFilters
            // 
            flpTagFilters.AutoScroll = true;
            flpTagFilters.BackColor = Color.White;
            flpTagFilters.Location = new Point(10, 30);
            flpTagFilters.Name = "flpTagFilters";
            flpTagFilters.Padding = new Padding(5);
            flpTagFilters.Size = new Size(960, 80);
            flpTagFilters.TabIndex = 2;
            // 
            // lstNotes
            // 
            lstNotes.DisplayMember = "Title";
            lstNotes.Dock = DockStyle.Fill;
            lstNotes.DrawMode = DrawMode.OwnerDrawVariable;
            lstNotes.IntegralHeight = false;
            lstNotes.ItemHeight = 60;
            lstNotes.Location = new Point(0, 220);
            lstNotes.Name = "lstNotes";
            lstNotes.Size = new Size(984, 341);
            lstNotes.TabIndex = 0;
            lstNotes.DrawItem += LstNotes_DrawItem;
            lstNotes.MeasureItem += LstNotes_MeasureItem;
            lstNotes.SelectedIndexChanged += LstNotes_SelectedIndexChanged;
            // 
            // MainForm
            // 
            ClientSize = new Size(984, 561);
            Controls.Add(lstNotes);
            Controls.Add(pnlTags);
            Controls.Add(searchPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Notes Manager";
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            actionPanel.ResumeLayout(false);
            pnlTags.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel searchPanel;
        private Label lblSearch;
        private Label lblTags;
        private Panel actionPanel;
        private Label lblTagsTitle;
        private Button btnClearTags;
    }
}