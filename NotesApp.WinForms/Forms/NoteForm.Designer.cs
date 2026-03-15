namespace NotesApp.WinForms.Forms
{
    partial class NoteForm
    {
        private System.ComponentModel.IContainer components = null;

        // Объявляем все поля класса
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.TextBox txtTags;
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

            // NoteForm
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // lblTitle
            lblTitle.Text = "Title:";
            lblTitle.Location = new System.Drawing.Point(10, 10);
            lblTitle.Size = new System.Drawing.Size(50, 25);

            // txtTitle
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtTitle.Location = new System.Drawing.Point(70, 10);
            this.txtTitle.Size = new System.Drawing.Size(500, 25);

            // lblContent
            lblContent.Text = "Content:";
            lblContent.Location = new System.Drawing.Point(10, 45);
            lblContent.Size = new System.Drawing.Size(50, 25);

            // txtContent
            this.txtContent = new System.Windows.Forms.TextBox();
            this.txtContent.Location = new System.Drawing.Point(70, 45);
            this.txtContent.Size = new System.Drawing.Size(500, 300);
            this.txtContent.Multiline = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // lblTags
            lblTags.Text = "Tags:";
            lblTags.Location = new System.Drawing.Point(10, 355);
            lblTags.Size = new System.Drawing.Size(50, 25);

            // txtTags
            this.txtTags = new System.Windows.Forms.TextBox();
            this.txtTags.Location = new System.Drawing.Point(70, 355);
            this.txtTags.Size = new System.Drawing.Size(500, 25);

            // btnSave
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSave.Text = "Save";
            this.btnSave.Location = new System.Drawing.Point(400, 400);
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(490, 400);
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            // Добавляем контролы на форму
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(lblContent);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(lblTags);
            this.Controls.Add(this.txtTags);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}