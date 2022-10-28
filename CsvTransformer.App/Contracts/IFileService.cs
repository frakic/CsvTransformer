using CsvTransformer.App.Models;
using System.Collections.Generic;

namespace CsvTransformer.App.Contracts;

public interface IFileService
{
    void SaveOutputFile(List<OutputRow> outputRows);
}
