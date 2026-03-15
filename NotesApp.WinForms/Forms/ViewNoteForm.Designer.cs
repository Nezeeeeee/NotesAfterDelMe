namespace NotesApp.WinForms.Forms
{
    partial class ViewNoteForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.FlowLayoutPanel flpTags;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlHeader;

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

            // ViewNoteForm
            this.Text = "Просмотр заметки";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

            // pnlHeader - верхняя панель с заголовком (УВЕЛИЧЕНА)
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100; // Увеличено с 80 до 100
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(15);
            this.pnlHeader.BackColor = System.Drawing.Color.LightSteelBlue;

            // lblTitle - заголовок заметки
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16, System.Drawing.FontStyle.Bold); // Увеличил шрифт
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Size = new System.Drawing.Size(750, 35);
            this.lblTitle.Text = "Заголовок";
            this.lblTitle.AutoEllipsis = true; // Добавляет многоточие если текст не помещается
            this.pnlHeader.Controls.Add(this.lblTitle);

            // lblDate - дата создания/обновления
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDate.Location = new System.Drawing.Point(15, 55); // Сдвинуто вниз
            this.lblDate.Size = new System.Drawing.Size(750, 30);
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
            this.lblDate.ForeColor = System.Drawing.Color.DimGray;
            this.pnlHeader.Controls.Add(this.lblDate);

            // flpTags - контейнер для тегов
            this.flpTags = new System.Windows.Forms.FlowLayoutPanel();
            this.flpTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpTags.Height = 60; // Увеличено с 50 до 60
            this.flpTags.Padding = new System.Windows.Forms.Padding(10);
            this.flpTags.AutoScroll = true;
            this.flpTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTags.BackColor = System.Drawing.Color.WhiteSmoke;

            // txtContent - содержимое заметки
            this.txtContent = new System.Windows.Forms.TextBox();
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Multiline = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.ReadOnly = true;
            this.txtContent.BackColor = System.Drawing.Color.White;
            this.txtContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
            this.txtContent.Padding = new System.Windows.Forms.Padding(15);

            // btnClose - кнопка закрытия
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClose.Text = "Закрыть";
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClose.Height = 45; // Увеличено с 40 до 45
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
            this.btnClose.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 1;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);

            // Добавляем контролы на форму в правильном порядке
            this.Controls.Add(this.txtContent);      // Заполняет оставшееся место
            this.Controls.Add(this.flpTags);         // Сверху контента
            this.Controls.Add(this.pnlHeader);       // Самая верхняя
            this.Controls.Add(this.btnClose);        // Самая нижняя

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}