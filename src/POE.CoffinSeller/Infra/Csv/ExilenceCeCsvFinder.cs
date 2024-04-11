using System.IO;
using System.Linq;

namespace POE.CoffinSeller.Infra.Models
{
    public static class ExilenceCeCsvFinder
    {
        private const string ExcilenceCeSearchPattern = "Export_*.csv";

        public static string FindMostRecentFile(string basePath)
        {
            var foundFiles = Directory.GetFiles(basePath, ExcilenceCeSearchPattern);

            return foundFiles?.ToList()?.OrderBy(x => x)?.LastOrDefault();
        }
    }
}
