using System.Collections.Generic;
using System.Windows.Forms;

namespace NotesApp.WinForms
{
    public static class LocalizationManager
    {
        private static string _currentLanguage = "ru"; // По умолчанию русский

        public static string CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                OnLanguageChanged();
            }
        }

        public static event System.EventHandler LanguageChanged;

        private static void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(null, System.EventArgs.Empty);
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
            else // Русский
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
                    
                    // NoteForm
                    { "EditNote", "Редактирование заметки" },
                    { "NewNote", "Новая заметка" },
                    { "Title", "Заголовок:" },
                    { "Content", "Содержание:" },
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