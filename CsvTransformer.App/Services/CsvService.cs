using CsvHelper;
using CsvTransformer.App.Contracts;
using CsvTransformer.App.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsvTransformer.App.Services;

public class CsvService : ICsvService
{
    public string? FilePath { get; private set; }

    public string GetFilePath()
    {
        return FilePath!;
    }

    public void SetFilePath(string input)
    {
        FilePath = input;
    }

    public void RemoveFilePath()
    {
        FilePath = default!;
    }

    public List<OutputRow> GetOutputRows()
    {
        var inputRows = GetInputRows();

        var outputRows = new List<OutputRow>();

        foreach (var row in inputRows)
        {
            var end = row.Amount / 10;

            for (int i = 0; i < end; i++)
            {
                var outputRow = new OutputRow
                {
                    UserId = row.UserId
                };

                outputRows.Add(outputRow);
            }
        }

        return outputRows;
    }

    private List<InputRow> GetInputRows()
    {
        using var reader = new StreamReader(FilePath!);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = new List<InputRow>();

        records = csv.GetRecords<InputRow>().ToList();

        foreach (var record in records)
        {
            record.UserId = record.UserId.Remove(0, 3);
            record.Amount /= 5;
        }

        return records;
    }
}
