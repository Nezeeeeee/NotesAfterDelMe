namespace NotesApp.WinForms.Forms
{
    partial class NoteForm
    {
        private System.ComponentModel.IContainer components = null;

        // Объявляем все поля класса
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.FlowLayoutPanel flpTagsContainer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

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

            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblContent = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblTags = new System.Windows.Forms.Label();

            // NoteForm - УВЕЛИЧИВАЕМ ШИРИНУ
            this.Size = new System.Drawing.Size(900, 620); // Увеличено с 850 до 900
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // lblTitle
            lblTitle.Text = "Title:";
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.Size = new System.Drawing.Size(100, 20); // Увеличено с 60 до 100
            lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
            lblTitle.TextAlign = ContentAlignment.MiddleRight; // Выравнивание по правому краю

            // txtTitle
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtTitle.Location = new System.Drawing.Point(130, 18); // Сдвинуто с 90 на 130
            this.txtTitle.Size = new System.Drawing.Size(740, 25); // Увеличено с 730 до 740
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);

            // lblContent - УВЕЛИЧИВАЕМ МЕТКУ ДЛЯ РУССКОГО ТЕКСТА
            lblContent.Text = "Content:";
            lblContent.Location = new System.Drawing.Point(20, 60);
            lblContent.Size = new System.Drawing.Size(100, 20); // Увеличено с 70 до 100
            lblContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
            lblContent.TextAlign = ContentAlignment.MiddleRight; // Выравнивание по правому краю

            // txtContent
            this.txtContent = new System.Windows.Forms.TextBox();
            this.txtContent.Location = new System.Drawing.Point(130, 58); // Сдвинуто с 90 на 130
            this.txtContent.Size = new System.Drawing.Size(740, 350); // Увеличено с 730 до 740
            this.txtContent.Multiline = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);

            // lblTags
            lblTags.Text = "Tags:";
            lblTags.Location = new System.Drawing.Point(20, 430);
            lblTags.Size = new System.Drawing.Size(100, 20); // Увеличено с 60 до 100
            lblTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
            lblTags.TextAlign = ContentAlignment.MiddleRight;

            // flpTagsContainer - контейнер для тегов
            this.flpTagsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.flpTagsContainer.Location = new System.Drawing.Point(130, 428); // Сдвинуто с 90 на 130
            this.flpTagsContainer.Size = new System.Drawing.Size(740, 100); // Увеличено с 730 до 740
            this.flpTagsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTagsContainer.AutoScroll = true;
            this.flpTagsContainer.Padding = new System.Windows.Forms.Padding(8);
            this.flpTagsContainer.BackColor = System.Drawing.Color.WhiteSmoke;

            // btnSave
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSave.Text = "Save";
            this.btnSave.Location = new System.Drawing.Point(640, 545); // Сдвинуто с 610 на 640
            this.btnSave.Size = new System.Drawing.Size(110, 35); // Увеличено с 100 до 110
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
            this.btnSave.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(760, 545); // Сдвинуто с 720 на 760
            this.btnCancel.Size = new System.Drawing.Size(110, 35); // Увеличено с 100 до 110
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            // Добавляем контролы на форму
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(lblContent);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(lblTags);
            this.Controls.Add(this.flpTagsContainer);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}