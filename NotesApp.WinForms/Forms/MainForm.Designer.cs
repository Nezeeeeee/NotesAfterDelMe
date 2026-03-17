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
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.ComboBox cmbTheme;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exportMenu;
        private System.Windows.Forms.ToolStripMenuItem exportAllNotesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportFilteredMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblTags;
        private System.Windows.Forms.Panel actionPanel;
        private System.Windows.Forms.Label lblTagsTitle;
        private System.Windows.Forms.Button btnClearTags;
        private System.Windows.Forms.Label lblSearchInfo;
        private System.Windows.Forms.Label lblSelectedTags;
        private System.Windows.Forms.TableLayoutPanel tlpInfoPanel; // Новая панель

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

            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllNotesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFilteredMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTags = new System.Windows.Forms.Label();
            this.txtTagFilter = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.cmbTheme = new System.Windows.Forms.ComboBox();
            this.actionPanel = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlTags = new System.Windows.Forms.Panel();
            this.lblTagsTitle = new System.Windows.Forms.Label();
            this.btnClearTags = new System.Windows.Forms.Button();
            this.flpTagFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpInfoPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearchInfo = new System.Windows.Forms.Label();
            this.lblSelectedTags = new System.Windows.Forms.Label();
            this.lstNotes = new System.Windows.Forms.ListBox();

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileMenu
            });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip";

            // fileMenu
            this.fileMenu.Text = "Файл";

            // exportMenu
            this.exportMenu.Text = "Экспорт";

            // exportAllNotesMenuItem
            this.exportAllNotesMenuItem.Text = "Экспорт всех заметок";
            this.exportAllNotesMenuItem.Click += new System.EventHandler(this.ExportAllNotesMenuItem_Click);

            // exportFilteredMenuItem
            this.exportFilteredMenuItem.Text = "Экспорт отфильтрованных";
            this.exportFilteredMenuItem.Click += new System.EventHandler(this.ExportFilteredMenuItem_Click);

            this.exportMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.exportAllNotesMenuItem,
                this.exportFilteredMenuItem
            });

            // aboutMenuItem
            this.aboutMenuItem.Text = "О программе";
            this.aboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);

            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.exportMenu,
                new System.Windows.Forms.ToolStripSeparator(),
                this.aboutMenuItem
            });

            // searchPanel
            this.searchPanel.Controls.Add(this.lblSearch);
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Controls.Add(this.lblTags);
            this.searchPanel.Controls.Add(this.txtTagFilter);
            this.searchPanel.Controls.Add(this.btnSearch);
            this.searchPanel.Controls.Add(this.cmbLanguage);
            this.searchPanel.Controls.Add(this.cmbTheme);
            this.searchPanel.Controls.Add(this.actionPanel);
            this.searchPanel.Controls.Add(this.tlpInfoPanel);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 24);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new System.Windows.Forms.Padding(10);
            this.searchPanel.Size = new System.Drawing.Size(1200, 140);
            this.searchPanel.TabIndex = 2;

            // lblSearch
            this.lblSearch.Location = new System.Drawing.Point(10, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(50, 25);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search:";

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(70, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 23);
            this.txtSearch.TabIndex = 1;

            // lblTags
            this.lblTags.Location = new System.Drawing.Point(380, 15);
            this.lblTags.Name = "lblTags";
            this.lblTags.Size = new System.Drawing.Size(40, 25);
            this.lblTags.TabIndex = 2;
            this.lblTags.Text = "Tags:";

            // txtTagFilter
            this.txtTagFilter.Location = new System.Drawing.Point(430, 12);
            this.txtTagFilter.Name = "txtTagFilter";
            this.txtTagFilter.PlaceholderText = "tag1, tag2, tag3";
            this.txtTagFilter.Size = new System.Drawing.Size(200, 23);
            this.txtTagFilter.TabIndex = 3;

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(640, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            // cmbLanguage
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.Items.AddRange(new object[] { "Русский", "English" });
            this.cmbLanguage.Location = new System.Drawing.Point(860, 12);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(120, 23);
            this.cmbLanguage.TabIndex = 6;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.CmbLanguage_SelectedIndexChanged);

            // cmbTheme
            this.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTheme.Items.AddRange(new object[] { "Светлая", "Темная" });
            this.cmbTheme.Location = new System.Drawing.Point(990, 12);
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(120, 23);
            this.cmbTheme.TabIndex = 7;
            this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.CmbTheme_SelectedIndexChanged);

            // actionPanel
            this.actionPanel.Controls.Add(this.btnAdd);
            this.actionPanel.Controls.Add(this.btnEdit);
            this.actionPanel.Controls.Add(this.btnDelete);
            this.actionPanel.Location = new System.Drawing.Point(10, 50);
            this.actionPanel.Name = "actionPanel";
            this.actionPanel.Size = new System.Drawing.Size(1180, 40);
            this.actionPanel.TabIndex = 5;

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add Note";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(110, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(220, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // pnlTags
            this.pnlTags.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTags.Controls.Add(this.lblTagsTitle);
            this.pnlTags.Controls.Add(this.btnClearTags);
            this.pnlTags.Controls.Add(this.flpTagFilters);
            this.pnlTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTags.Location = new System.Drawing.Point(0, 164);
            this.pnlTags.Name = "pnlTags";
            this.pnlTags.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTags.Size = new System.Drawing.Size(1200, 120);
            this.pnlTags.TabIndex = 1;

            // lblTagsTitle
            this.lblTagsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblTagsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTagsTitle.Name = "lblTagsTitle";
            this.lblTagsTitle.Size = new System.Drawing.Size(400, 20);
            this.lblTagsTitle.TabIndex = 0;
            this.lblTagsTitle.Text = "Фильтр по тегам (выберите один или несколько):";

            // btnClearTags
            this.btnClearTags.Location = new System.Drawing.Point(1050, 3);
            this.btnClearTags.Name = "btnClearTags";
            this.btnClearTags.Size = new System.Drawing.Size(130, 25);
            this.btnClearTags.TabIndex = 1;
            this.btnClearTags.Text = "Сбросить фильтр";
            this.btnClearTags.Click += new System.EventHandler(this.BtnClearTags_Click);

            // flpTagFilters
            this.flpTagFilters.AutoScroll = true;
            this.flpTagFilters.BackColor = System.Drawing.Color.White;
            this.flpTagFilters.Location = new System.Drawing.Point(10, 30);
            this.flpTagFilters.Name = "flpTagFilters";
            this.flpTagFilters.Padding = new System.Windows.Forms.Padding(5);
            this.flpTagFilters.Size = new System.Drawing.Size(1170, 80);
            this.flpTagFilters.TabIndex = 2;

            // tlpInfoPanel - панель для информации
            this.tlpInfoPanel.ColumnCount = 2;
            this.tlpInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tlpInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tlpInfoPanel.Location = new System.Drawing.Point(10, 95);
            this.tlpInfoPanel.Name = "tlpInfoPanel";
            this.tlpInfoPanel.Size = new System.Drawing.Size(1160, 25);
            this.tlpInfoPanel.TabIndex = 10;

            // lblSearchInfo
            this.lblSearchInfo.AutoSize = true;
            this.lblSearchInfo.Dock = DockStyle.Fill;
            this.lblSearchInfo.Location = new System.Drawing.Point(3, 0);
            this.lblSearchInfo.Name = "lblSearchInfo";
            this.lblSearchInfo.Size = new System.Drawing.Size(100, 25);
            this.lblSearchInfo.TabIndex = 8;
            this.lblSearchInfo.Text = "";
            this.lblSearchInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblSearchInfo.TextAlign = ContentAlignment.MiddleLeft;

            // lblSelectedTags
            this.lblSelectedTags.AutoSize = true;
            this.lblSelectedTags.Dock = DockStyle.Fill;
            this.lblSelectedTags.Location = new System.Drawing.Point(109, 0);
            this.lblSelectedTags.Name = "lblSelectedTags";
            this.lblSelectedTags.Size = new System.Drawing.Size(1048, 25);
            this.lblSelectedTags.TabIndex = 9;
            this.lblSelectedTags.Text = "";
            this.lblSelectedTags.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblSelectedTags.Visible = false;
            this.lblSelectedTags.TextAlign = ContentAlignment.MiddleLeft;
            this.lblSelectedTags.AutoEllipsis = true;

            this.tlpInfoPanel.Controls.Add(this.lblSearchInfo, 0, 0);
            this.tlpInfoPanel.Controls.Add(this.lblSelectedTags, 1, 0);

            // lstNotes
            this.lstNotes.DisplayMember = "Title";
            this.lstNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNotes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstNotes.IntegralHeight = false;
            this.lstNotes.ItemHeight = 60;
            this.lstNotes.Location = new System.Drawing.Point(0, 284);
            this.lstNotes.Name = "lstNotes";
            this.lstNotes.Size = new System.Drawing.Size(1200, 377);
            this.lstNotes.TabIndex = 0;
            this.lstNotes.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstNotes_DrawItem);
            this.lstNotes.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.LstNotes_MeasureItem);
            this.lstNotes.SelectedIndexChanged += new System.EventHandler(this.LstNotes_SelectedIndexChanged);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 661);
            this.Controls.Add(this.lstNotes);
            this.Controls.Add(this.pnlTags);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notes Manager";

            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.actionPanel.ResumeLayout(false);
            this.pnlTags.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}