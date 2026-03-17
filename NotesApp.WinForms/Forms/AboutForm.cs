using System;
using System.Drawing;
using System.Windows.Forms;

namespace NotesApp.WinForms.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            ApplyTheme();
            LocalizationManager.ThemeChanged += (s, e) => ApplyTheme();
            LocalizationManager.LanguageChanged += (s, e) => UpdateUILanguage();
            UpdateUILanguage();
        }

        private void UpdateUILanguage()
        {
            this.Text = LocalizationManager.GetString("AboutTitle");
            lblAppName.Text = "Notes Manager";
            lblVersion.Text = LocalizationManager.GetString("Version") + " 1.0.0";
            lblAuthor.Text = LocalizationManager.GetString("Author") + ": " + GetAuthorName();
            lblDescription.Text = LocalizationManager.GetString("AppDescription");
            btnClose.Text = LocalizationManager.GetString("Close");
        }

        private string GetAuthorName()
        {
            // Здесь можно указать ваше имя
            if (LocalizationManager.CurrentLanguage == "ru")
                return "Сергей Князев"; // Замените на ваше имя
            else
                return "Sergey Knyazev"; // Замените на ваше имя
        }

        private void ApplyTheme()
        {
            this.BackColor = LocalizationManager.GetColor("Background");
            this.ForeColor = LocalizationManager.GetColor("Foreground");

            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    panel.BackColor = LocalizationManager.GetColor("PanelBackground");
                }
                else if (control is Button button)
                {
                    button.BackColor = LocalizationManager.GetColor("ButtonBackground");
                    button.ForeColor = LocalizationManager.GetColor("ButtonText");
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = LocalizationManager.GetColor("BorderColor");
                    button.FlatAppearance.MouseOverBackColor = LocalizationManager.GetColor("ButtonHover");
                }
                else if (control is Label label)
                {
                    label.ForeColor = LocalizationManager.GetColor("Foreground");
                }
            }

            // Специальные цвета для заголовка
            lblAppName.ForeColor = LocalizationManager.CurrentTheme == "dark" ?
                Color.FromArgb(100, 150, 255) : Color.FromArgb(0, 102, 204);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}