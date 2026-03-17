namespace NotesApp.WinForms.Forms
{
    partial class AboutForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbIcon;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();

            // AboutForm
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";

            // pnlMain
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pbIcon);
            this.pnlMain.Controls.Add(this.lblAppName);
            this.pnlMain.Controls.Add(this.lblVersion);
            this.pnlMain.Controls.Add(this.lblAuthor);
            this.pnlMain.Controls.Add(this.lblDescription);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(400, 200);
            this.pnlMain.TabIndex = 0;

            // pbIcon
            this.pbIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbIcon.Image")));
            this.pbIcon.Location = new System.Drawing.Point(20, 20);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(64, 64);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;

            // lblAppName
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppName.Location = new System.Drawing.Point(100, 20);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(158, 26);
            this.lblAppName.TabIndex = 1;
            this.lblAppName.Text = "Notes Manager";

            // lblVersion
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblVersion.Location = new System.Drawing.Point(101, 55);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(62, 17);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Версия:";

            // lblAuthor
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblAuthor.Location = new System.Drawing.Point(101, 85);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(51, 17);
            this.lblAuthor.TabIndex = 3;
            this.lblAuthor.Text = "Автор:";

            // lblDescription
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblDescription.Location = new System.Drawing.Point(20, 120);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(360, 60);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Приложение для управления заметками с поддержкой тегов, поиска, множественного выбора и смены темы.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(150, 220);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);

            // AboutForm
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnClose);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}