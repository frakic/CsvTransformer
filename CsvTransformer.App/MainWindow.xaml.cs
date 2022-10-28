using CsvTransformer.App.Contracts;
using CsvTransformer.App.Models;
using CsvTransformer.App.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace CsvTransformer.App;

public partial class MainWindow : Window
{
    private readonly ICsvService _csvService;
    private readonly IFileService _fileService;
    public MainWindow(CsvService csvService, FileService fileService)
    {
        _csvService = csvService;
        _fileService = fileService;
        InitializeComponent();
    }

    private void FilePanel_PreviewDragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var fileExtension = Path.GetExtension(files[0]);
            if (fileExtension.ToLower() != ".csv")
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }
    }

    private void FilePanel_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            _csvService.SetFilePath(files[0]);

            DisplayFileInfoAndTransformationButton();
        }
    }

    private void FileButton_Click(object sender, RoutedEventArgs e)
    {
        var file = new OpenFileDialog
        {
            Filter = "CSV UTF-8 (Comma delimited) (*.csv)|*.csv"
        };

        if (file.ShowDialog() == true)
        {
            _csvService.SetFilePath(file.FileName);

            DisplayFileInfoAndTransformationButton();
        }
    }

    private void RemoveFile_Click(object sender, RoutedEventArgs e)
    {
        RemoveFile();
    }

    private void RemoveFile()
    {
        _csvService.RemoveFilePath();
        FileAdd.Visibility = Visibility.Visible;
        FileSuccess.Visibility = Visibility.Hidden;
        FilePanel.IsEnabled = true;
        FilePanel.Background = new System.Windows.Media.SolidColorBrush()
        {
            Color = System.Windows.Media.Color.FromRgb(255, 255, 255)
        };
        FileMessage.Visibility = Visibility.Visible;
        UploadedFile.Visibility = Visibility.Hidden;
        FileName.Content = "";
        DoTransformation.Visibility = Visibility.Hidden;
    }

    private void DisplayFileInfoAndTransformationButton()
    {
        FileAdd.Visibility = Visibility.Hidden;
        FileSuccess.Visibility = Visibility.Visible;
        FilePanel.IsEnabled = false;
        FilePanel.Background = new System.Windows.Media.SolidColorBrush()
        {
            Color = System.Windows.Media.Color.FromRgb(242, 242, 242)
        };
        FileMessage.Visibility = Visibility.Hidden;
        UploadedFile.Visibility = Visibility.Visible;

        FileName.Content = Path.GetFileName(_csvService.GetFilePath());
        DoTransformation.Visibility = Visibility.Visible;
    }

    private void DoTransformation_Click(object sender, RoutedEventArgs e)
    {
        List<OutputRow> outputRows;

        try
        {
            outputRows = _csvService.GetOutputRows();
            _fileService.SaveOutputFile(outputRows);
        }

        catch (Exception ex)
        {
            MessageBox.Show("Došlo je do greške prilikom čitanja ulazne datoteke:\n\n" + ex.Message,
            "Čitanje neuspješno",
            MessageBoxButton.OK,
            MessageBoxImage.Error,
            MessageBoxResult.OK);
        }
        finally
        {
            RemoveFile();
        }
    }
}
