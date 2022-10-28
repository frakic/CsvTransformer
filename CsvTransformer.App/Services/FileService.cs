using CsvHelper;
using CsvTransformer.App.Contracts;
using CsvTransformer.App.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace CsvTransformer.App.Services;

public class FileService : IFileService
{
    public void SaveOutputFile(List<OutputRow> outputRows)
    {
        var saveDialog = new SaveFileDialog()
        {
            Title = "Save output CSV",
            RestoreDirectory = true,
            Filter = "CSV UTF-8 (Comma delimited) (*.csv)|*.csv|All files (*.*)|*.*",
            DefaultExt = "csv",
            FileName = "output.csv"

        };

        if (saveDialog.ShowDialog() == true)
        {
            using (var writer = new StreamWriter(saveDialog.OpenFile()))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(outputRows);
            };

            MessageBox.Show($"Izlazna datoteka uspješno spremljena na lokaciju: {saveDialog.FileName}",
            "Spremanje uspješno",
            MessageBoxButton.OK,
            MessageBoxImage.Information,
            MessageBoxResult.OK);
        }
    }
}
