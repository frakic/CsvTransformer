using CsvTransformer.App.Models;
using System.Collections.Generic;

namespace CsvTransformer.App.Contracts;

public interface ICsvService
{
    string GetFilePath();
    void SetFilePath(string input);
    void RemoveFilePath();
    List<OutputRow> GetOutputRows();
}
