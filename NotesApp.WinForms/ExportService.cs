using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using NotesApp.Application.DTOs;

namespace NotesApp.WinForms.Services
{
    public static class ExportService
    {
        public static void ExportNotesToExcel(List<NoteDto> notes, string filePath)
        {
            if (notes == null || notes.Count == 0)
                throw new ArgumentException("No notes to export");

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Заметки");

                    // Заголовки
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = LocalizationManager.GetString("Title");
                    worksheet.Cell(1, 3).Value = LocalizationManager.GetString("Content");
                    worksheet.Cell(1, 4).Value = LocalizationManager.GetString("Tags_");
                    worksheet.Cell(1, 5).Value = LocalizationManager.GetString("Created");
                    worksheet.Cell(1, 6).Value = LocalizationManager.GetString("Updated");

                    // Стиль для заголовков
                    var headerRange = worksheet.Range(1, 1, 1, 6);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // Заполняем данными
                    for (int i = 0; i < notes.Count; i++)
                    {
                        var note = notes[i];
                        int row = i + 2;

                        worksheet.Cell(row, 1).Value = note.Id;
                        worksheet.Cell(row, 2).Value = note.Title;
                        worksheet.Cell(row, 3).Value = note.Content;
                        worksheet.Cell(row, 4).Value = string.Join(", ", note.Tags);
                        worksheet.Cell(row, 5).Value = note.CreatedAt.ToString("dd.MM.yyyy HH:mm");
                        worksheet.Cell(row, 6).Value = note.UpdatedAt.ToString("dd.MM.yyyy HH:mm");

                        // Автоподбор высоты строки для многострочного текста
                        worksheet.Row(row).AdjustToContents();
                    }

                    // Автоподбор ширины колонок
                    worksheet.Columns().AdjustToContents();

                    // Сохраняем файл
                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при создании Excel файла: {ex.Message}", ex);
            }
        }
    }
}