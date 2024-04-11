using POE.CoffinSeller.Infra.Models;
using System;

namespace POE.CoffinSeller.Infra.Mapping
{
    public class MappingMessageReplacementModel
    {
        public MappingMessageReplacementModel(string startsWith, bool isDoubleLine = false, bool isScarcerItem = false, string charsToReplace = "{x}")
        {
            StartsWithLine = startsWith;
            IsDoubleLine = isDoubleLine;
            IsScarcerItem = isScarcerItem;
            CharsToReplace = charsToReplace;
        }

        public string StartsWithLine { get; set; }

        public bool IsDoubleLine { get; set; }

        public bool IsScarcerItem { get; set; }

        public string CharsToReplace { get; set; }

        private const string ReplacementTemplateText = "{price}c\tStock: {quant}";
        private const string ReplacementPriceText = "{price}";
        private const string ReplacementQuantText = "{quant}";

        // This should probably be somewhere else.
        // TODO: This shouldn't be tied to the other model, but its 2am and idc.
        public string ReplaceString(string stringToReplace, ExilenceCeCsvModel data)
        {
            var roundedPrice = Math.Round(data.Calculated, MidpointRounding.ToNegativeInfinity);

            string replacementTextWithPrice = string.Empty;
            if (roundedPrice != 0) // check if price is 0c, if it is then replace with "--".
                replacementTextWithPrice = ReplacementTemplateText.Replace(ReplacementPriceText, roundedPrice.ToString());
            else
                replacementTextWithPrice = ReplacementTemplateText.Replace(ReplacementPriceText, "--");

            string replacementTextWithBoth = string.Empty;
            if (data.StackSize != 0)
                replacementTextWithBoth = replacementTextWithPrice.Replace(ReplacementQuantText, data.StackSize.ToString());
            else
                replacementTextWithBoth = replacementTextWithPrice.Replace(ReplacementQuantText, "-");


            return stringToReplace.Replace(CharsToReplace, replacementTextWithBoth); ;
        }
    }
}
