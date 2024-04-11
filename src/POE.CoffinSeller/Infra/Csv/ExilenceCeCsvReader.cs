using CsvHelper;
using CsvHelper.Configuration;
using POE.CoffinSeller.Infra.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace POE.CoffinSeller.Infra
{
    public static class ExilenceCeCsvReader
    {
        public static List<ExilenceCeCsvModel> ReadCoffinData(string fileToRead)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldSkipRecord = args => args.Row.Parser.Row < 2
            };

            using (var reader = new StreamReader(fileToRead))
            using (var csv = new CsvReader(reader, config))
            {
                return csv.GetRecords<ExilenceCeCsvModel>().Where(x => x.TypeLine == "Filled Coffin" && !x.Name.StartsWith("Haunted by")).ToList();
            }
        }
    }
}
