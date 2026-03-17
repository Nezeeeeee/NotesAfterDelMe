using System.Collections.Generic;
using System;
using System.Drawing;

namespace NotesApp.WinForms
{
    public static class LocalizationManager
    {
        private static string _currentLanguage = "ru";
        private static string _currentTheme = "light";

        public static string CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                OnLanguageChanged();
            }
        }

        public static string CurrentTheme
        {
            get { return _currentTheme; }
            set
            {
                _currentTheme = value;
                OnThemeChanged();
            }
        }

        public static event EventHandler LanguageChanged;
        public static event EventHandler ThemeChanged;

        private static void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }

        private static void OnThemeChanged()
        {
            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        public static Color GetColor(string colorName)
        {
            if (_currentTheme == "dark")
            {
                return colorName switch
                {
                    "Background" => Color.FromArgb(32, 32, 32),
                    "Foreground" => Color.FromArgb(220, 220, 220),
                    "PanelBackground" => Color.FromArgb(45, 45, 45),
                    "PanelBorder" => Color.FromArgb(64, 64, 64),
                    "ButtonBackground" => Color.FromArgb(70, 70, 70),
                    "ButtonHover" => Color.FromArgb(90, 90, 90),
                    "ButtonText" => Color.FromArgb(240, 240, 240),
                    "SelectedItem" => Color.FromArgb(75, 110, 175),
                    "TagBackground" => Color.FromArgb(80, 100, 120),
                    "TagText" => Color.FromArgb(240, 240, 240),
                    "DateText" => Color.FromArgb(150, 150, 150),
                    "HeaderBackground" => Color.FromArgb(55, 55, 55),
                    "InputBackground" => Color.FromArgb(40, 40, 40),
                    "InputForeground" => Color.FromArgb(240, 240, 240),
                    "BorderColor" => Color.FromArgb(80, 80, 80),
                    _ => Color.FromArgb(32, 32, 32)
                };
            }
            else
            {
                return colorName switch
                {
                    "Background" => Color.FromArgb(240, 240, 240),
                    "Foreground" => Color.FromArgb(32, 32, 32),
                    "PanelBackground" => Color.FromArgb(250, 250, 250),
                    "PanelBorder" => Color.FromArgb(200, 200, 200),
                    "ButtonBackground" => Color.FromArgb(230, 230, 230),
                    "ButtonHover" => Color.FromArgb(210, 210, 210),
                    "ButtonText" => Color.FromArgb(32, 32, 32),
                    "SelectedItem" => Color.FromArgb(173, 216, 230),
                    "TagBackground" => Color.FromArgb(176, 196, 222),
                    "TagText" => Color.FromArgb(32, 32, 32),
                    "DateText" => Color.FromArgb(105, 105, 105),
                    "HeaderBackground" => Color.FromArgb(230, 230, 250),
                    "InputBackground" => Color.FromArgb(255, 255, 255),
                    "InputForeground" => Color.FromArgb(32, 32, 32),
                    "BorderColor" => Color.FromArgb(200, 200, 200),
                    _ => Color.FromArgb(240, 240, 240)
                };
            }
        }

        public static Dictionary<string, string> GetStrings()
        {
            if (_currentLanguage == "en")
            {
                return new Dictionary<string, string>
                {
                    // MainForm
                    { "MainFormTitle", "Notes Manager" },
                    { "Search", "Search:" },
                    { "Tags", "Tags:" },
                    { "SearchButton", "Search" },
                    { "AddNote", "Add Note" },
                    { "Edit", "Edit" },
                    { "Delete", "Delete" },
                    { "TagsFilterTitle", "Filter by tags (select one or more):" },
                    { "ClearFilter", "Clear filter" },
                    { "AllTags", "All tags:" },
                    { "NoTags", "No tags" },
                    { "DeleteTagConfirm", "Delete tag '{0}' from all notes?" },
                    { "DeleteTagSuccess", "Tag '{0}' successfully deleted from {1} notes" },
                    { "DeleteTagError", "Error deleting tag: {0}" },
                    { "ConfirmDelete", "Confirm Delete" },
                    { "DeleteNoteConfirm", "Delete note '{0}'?" },
                    { "LightTheme", "Light" },
                    { "DarkTheme", "Dark" },
                    { "File", "File" },
                    { "Export", "Export" },
                    { "ExportAllNotes", "Export All Notes" },
                    { "ExportFiltered", "Export Filtered" },
                    { "About", "About" },
                    { "AboutTitle", "About Program" },
                    { "Version", "Version" },
                    { "Author", "Author" },
                    { "AppDescription", "Notes management application with tag support, search, multi-select and theme switching." },
                    { "ExcelExported", "Notes exported to Excel successfully!" },
                    { "ExcelExportError", "Error exporting to Excel: {0}" },
                    { "ExcelFiles", "Excel Files" },
                    { "NoNotesToExport", "No notes to export" },
                    { "NoFilterActive", "No active filters. Showing all notes." },
                    { "Success", "Success" },
                    { "Information", "Information" },
                    { "Question", "Question" },
                    { "OpenFileQuestion", "Open file?" },
                    { "Error", "Error" },
                    { "NoNotesFound", "No notes found" },
                    
                    // NoteForm
                    { "EditNote", "Edit Note" },
                    { "NewNote", "New Note" },
                    { "Title", "Title:" },
                    { "Content", "Content:" },
                    { "Tags_", "Tags:" },
                    { "Save", "Save" },
                    { "Cancel", "Cancel" },
                    { "NewTagPlaceholder", "new tag" },
                    { "TitleRequired", "Title is required" },
                    { "ValidationError", "Validation Error" },
                    
                    // ViewNoteForm
                    { "ViewNote", "View Note" },
                    { "Created", "Created" },
                    { "Updated", "Updated" },
                    { "Close", "Close" },
                    { "Tags_Header", "Tags:" },
                    { "TotalTags", "Total tags: {0}" },
                    { "NoTags_", "No tags" }
                };
            }
            else
            {
                return new Dictionary<string, string>
                {
                    // MainForm
                    { "MainFormTitle", "Менеджер заметок" },
                    { "Search", "Поиск:" },
                    { "Tags", "Теги:" },
                    { "SearchButton", "Поиск" },
                    { "AddNote", "Добавить" },
                    { "Edit", "Изменить" },
                    { "Delete", "Удалить" },
                    { "TagsFilterTitle", "Фильтр по тегам (выберите один или несколько):" },
                    { "ClearFilter", "Сбросить фильтр" },
                    { "AllTags", "Все теги:" },
                    { "NoTags", "Нет тегов" },
                    { "DeleteTagConfirm", "Удалить тег '{0}' из всех заметок?" },
                    { "DeleteTagSuccess", "Тег '{0}' успешно удален из {1} заметок" },
                    { "DeleteTagError", "Ошибка при удалении тега: {0}" },
                    { "ConfirmDelete", "Подтверждение удаления" },
                    { "DeleteNoteConfirm", "Удалить заметку '{0}'?" },
                    { "LightTheme", "Светлая" },
                    { "DarkTheme", "Темная" },
                    { "File", "Файл" },
                    { "Export", "Экспорт" },
                    { "ExportAllNotes", "Экспорт всех заметок" },
                    { "ExportFiltered", "Экспорт отфильтрованных" },
                    { "About", "О программе" },
                    { "AboutTitle", "О программе" },
                    { "Version", "Версия" },
                    { "Author", "Автор" },
                    { "AppDescription", "Приложение для управления заметками с поддержкой тегов, поиска, множественного выбора и смены темы." },
                    { "ExcelExported", "Заметки успешно экспортированы в Excel!" },
                    { "ExcelExportError", "Ошибка при экспорте в Excel: {0}" },
                    { "ExcelFiles", "Файлы Excel" },
                    { "NoNotesToExport", "Нет заметок для экспорта" },
                    { "NoFilterActive", "Нет активных фильтров. Показаны все заметки." },
                    { "Success", "Успех" },
                    { "Information", "Информация" },
                    { "Question", "Вопрос" },
                    { "OpenFileQuestion", "Открыть файл?" },
                    { "Error", "Ошибка" },
                    { "NoNotesFound", "Заметки не найдены" },
                    
                    // NoteForm
                    { "EditNote", "Редактирование" },
                    { "NewNote", "Новая заметка" },
                    { "Title", "Заголовок:" },
                    { "Content", "Текст:" },
                    { "Tags_", "Теги:" },
                    { "Save", "Сохранить" },
                    { "Cancel", "Отмена" },
                    { "NewTagPlaceholder", "новый тег" },
                    { "TitleRequired", "Заголовок не может быть пустым" },
                    { "ValidationError", "Ошибка валидации" },
                    
                    // ViewNoteForm
                    { "ViewNote", "Просмотр заметки" },
                    { "Created", "Создано" },
                    { "Updated", "Обновлено" },
                    { "Close", "Закрыть" },
                    { "Tags_Header", "Теги:" },
                    { "TotalTags", "Всего тегов: {0}" },
                    { "NoTags_", "Нет тегов" }
                };
            }
        }

        public static string GetString(string key, params object[] args)
        {
            var strings = GetStrings();
            if (strings.ContainsKey(key))
            {
                string value = strings[key];
                if (args.Length > 0)
                {
                    return string.Format(value, args);
                }
                return value;
            }
            return key;
        }
    }
}