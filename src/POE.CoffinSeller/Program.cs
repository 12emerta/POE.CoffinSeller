using POE.CoffinSeller.Infra;
using POE.CoffinSeller.Infra.Mapping;
using POE.CoffinSeller.Infra.Models;

namespace POE.CoffinSeller
{
    class Program
    {
        static void Main(string[] args)
        {
            var mostRecentFile = ExilenceCeCsvFinder.FindMostRecentFile(@"C:\Users\emert\Downloads\");
            var dataRead = ExilenceCeCsvReader.ReadCoffinData(mostRecentFile);

            CsvModelToPricingMessageMapper.Map(dataRead);
        }
    }
}
