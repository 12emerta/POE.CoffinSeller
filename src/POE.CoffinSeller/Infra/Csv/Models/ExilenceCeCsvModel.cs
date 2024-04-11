using CsvHelper.Configuration.Attributes;

namespace POE.CoffinSeller.Infra.Models
{
    [Delimiter(",")]
    public class ExilenceCeCsvModel
    {
        [Name("name")]
        public string Name { get; set; }

        [Name("typeLine")]
        public string TypeLine { get; set; }

        [Name("stackSize")]
        public decimal StackSize { get; set; }

        [Name("calculated")]
        public decimal Calculated { get; set; }
    }
}
