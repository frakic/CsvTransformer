using CsvHelper.Configuration.Attributes;

namespace CsvTransformer.App.Models;

public class InputRow
{
    [Name("User ID")]
    public string UserId { get; set; } = default!;
    [Name("amount")]
    public int Amount { get; set; }
}
