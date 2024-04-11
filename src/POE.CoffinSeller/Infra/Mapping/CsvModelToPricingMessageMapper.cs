using POE.CoffinSeller.Infra.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace POE.CoffinSeller.Infra.Mapping
{
    class CsvModelToPricingMessageMapper
    {

        public static void Map(List<ExilenceCeCsvModel> csvData)
        {
            List<string> pricingMsgLines = File.ReadAllLines("./Data/PricingMsg.txt").ToList();

            foreach (KeyValuePair<string, MappingMessageReplacementModel> pair in CoffinNameToShortTextDictionary)
            {
                // See if the csv has this key-value pair in it.
                if (csvData.Exists(dataRow => dataRow.Name == pair.Key))
                {
                    // Double check that the pricing msg contains this key-value matched pair.
                    if (pricingMsgLines.Exists(line => line.StartsWith(pair.Value.StartsWithLine)))
                    {
                        var lineToEdit = pricingMsgLines.FindIndex(line => line.StartsWith(pair.Value.StartsWithLine));

                        pricingMsgLines[lineToEdit] = pair.Value.ReplaceString(pricingMsgLines[lineToEdit], csvData.Single(dataRow => dataRow.Name == pair.Key));
                    }
                }
                // csv does not have data, so blank it out.
                else
                {
                    // Double check that the pricing msg contains this key-value matched pair.
                    if (pricingMsgLines.Exists(line => line.StartsWith(pair.Value.StartsWithLine)))
                    {
                        var lineToEdit = pricingMsgLines.FindIndex(line => line.StartsWith(pair.Value.StartsWithLine));

                        // Set the price to 0 and stack size to 0.
                        pricingMsgLines[lineToEdit] = pair.Value.ReplaceString(pricingMsgLines[lineToEdit],
                            new ExilenceCeCsvModel { Calculated = 0, Name = pair.Key, StackSize = 0 });
                    }
                }
            }

            Directory.CreateDirectory("./Output");
            File.WriteAllLines($"./Output/PricingMsg_{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}.txt", pricingMsgLines);
        }


        // TODO: Prob have to put in a key for the second string to tell if the replace key is first or second stock.
        public static Dictionary<string, MappingMessageReplacementModel> CoffinNameToShortTextDictionary = new Dictionary<string, MappingMessageReplacementModel>()
        {
            // TODO: This is not a full list, add them... or not.

            // Increased & Scarcer
            { "300% increased chance of Attack Modifiers",      new MappingMessageReplacementModel("Attack", true) },
            { "Attack Modifiers are 150% scarcer",              new MappingMessageReplacementModel("Attack", true, true, "{y}") },
            { "500% increased chance of Attribute Modifiers",   new MappingMessageReplacementModel("Attribute", true) },
            { "Attribute Modifiers are 300% scarcer",           new MappingMessageReplacementModel("Attribute", true, true, "{y}") },
            { "300% increased chance of Caster Modifiers",      new MappingMessageReplacementModel("Caster", true) },
            { "Caster Modifiers are 150% scarcer",              new MappingMessageReplacementModel("Caster", true, true, "{y}") },
            { "500% increased chance of Chaos Modifiers",       new MappingMessageReplacementModel("Chaos", true) },
            { "Chaos Modifiers are 300% scarcer",               new MappingMessageReplacementModel("Chaos", true, true, "{y}") },
            { "500% increased chance of Cold Modifiers",        new MappingMessageReplacementModel("Cold", true) },
            { "Cold Modifiers are 300% scarcer",                new MappingMessageReplacementModel("Cold", true, true, "{y}") },
            { "500% increased chance of Critical Modifiers",    new MappingMessageReplacementModel("Critical", true) },
            { "Critical Modifiers are 300% scarcer",            new MappingMessageReplacementModel("Critical", true, true, "{y}") },
            { "500% increased chance of Defence Modifiers",     new MappingMessageReplacementModel("Defence", true) },
            { "Defence Modifiers are 300% scarcer",             new MappingMessageReplacementModel("Defence", true, true, "{y}") },
            { "300% increased chance of Elemental Modifiers",   new MappingMessageReplacementModel("Elemental", true) },
            { "Elemental Modifiers are 150% scarcer",           new MappingMessageReplacementModel("Elemental", true, true, "{y}") },
            { "500% increased chance of Fire Modifiers",        new MappingMessageReplacementModel("Fire", true) },
            { "Fire Modifiers are 300% scarcer",                new MappingMessageReplacementModel("Fire", true, true, "{y}") },
            { "500% increased chance of Gem Modifiers",         new MappingMessageReplacementModel("Gem", true) },
            { "Gem Modifiers are 300% scarcer",                 new MappingMessageReplacementModel("Gem", true, true, "{y}") },
            { "500% increased chance of Life Modifiers",        new MappingMessageReplacementModel("Life", true) },
            { "Life Modifiers are 300% scarcer",                new MappingMessageReplacementModel("Life", true, true, "{y}") },
            { "500% increased chance of Lightning Modifiers",   new MappingMessageReplacementModel("Lightning", true) },
            { "Lightning Modifiers are 300% scarcer",           new MappingMessageReplacementModel("Lightning", true, true, "{y}") },
            { "500% increased chance of Mana Modifiers",        new MappingMessageReplacementModel("Mana", true) },
            { "Mana Modifiers are 100% scarcer",                new MappingMessageReplacementModel("Mana", true, true, "{y}") },
            { "500% increased chance of Minion Modifiers",      new MappingMessageReplacementModel("Minion") }, // TODO: No Minion Scarcer ???
            { "500% increased chance of Physical Modifiers",    new MappingMessageReplacementModel("Physical", true) },
            { "Physical Modifiers are 300% scarcer",            new MappingMessageReplacementModel("Physical", true, true, "{y}") },
            { "500% increased chance of Resistance Modifiers",  new MappingMessageReplacementModel("Resistance", true) },
            { "Resistance Modifiers are 300% scarcer",          new MappingMessageReplacementModel("Resistance", true, true, "{y}") },
            { "500% increased chance of Speed Modifiers",       new MappingMessageReplacementModel("Speed", true) },
            { "Speed Modifiers are 300% scarcer",               new MappingMessageReplacementModel("Speed", true, true, "{y}") },

            // Misc Corpses
            { "+1 Explicit Modifier", new MappingMessageReplacementModel("+1 Explicit") },
            { "-1 Explicit Modifiers", new MappingMessageReplacementModel("-1 Explicit") },
            { "+1 to Item Level", new MappingMessageReplacementModel("+1 to Item Level") },
            { "+5% to Quality, up to 30%", new MappingMessageReplacementModel("+5% to Quality") },
            { "+50 to Modifier Tier Rating", new MappingMessageReplacementModel("+50 Tier") },
            { "+1 to minimum number of Linked Sockets, up to a maximum of 5", new MappingMessageReplacementModel("5-Link") },
            { "+1 to minimum number of Linked Sockets, up to a maximum of 6", new MappingMessageReplacementModel("6-Link") },
            { "Corpses in adjacent Graves and this Corpse have their crafting outcomes randomised when this Corpse is buried", new MappingMessageReplacementModel("Random Craft") },
            { "Reroll Modifier Values of each Explicit Modifier 6 times, keeping the best outcome", new MappingMessageReplacementModel("Reroll Explicit") },
            { "Reroll Implicit Modifier Values 6 times, keeping the best outcome", new MappingMessageReplacementModel("Reroll Implicit") },

            // Meta Craft Corpses
            { "20% chance to craft an additional Item", new MappingMessageReplacementModel("20% Additional Item") },
            { "25% increased Effect of Corpses in this Grave Column", new MappingMessageReplacementModel("25% Column") },
            { "25% chance to Fracture an Explicit Modifier", new MappingMessageReplacementModel("25% Fracture") },
            { "40% increased Effect of Humanoid Corpses adjacent to this Corpse", new MappingMessageReplacementModel("40% Increased Effect(All)") },
            { "40% increased Effect of Beast Corpses adjacent to this Corpse", new MappingMessageReplacementModel("40% Increased Effect(All)") },
            { "40% increased Effect of Undead Corpses adjacent to this Corpse", new MappingMessageReplacementModel("40% Increased Effect(All)") },
            { "40% increased Effect of Construct Corpses adjacent to this Corpse", new MappingMessageReplacementModel("40% Increased Effect(All)") },
            { "40% increased Effect of Demon Corpses adjacent to this Corpse", new MappingMessageReplacementModel("40% Increased Effect(All)") },
            { "25% chance to create a Mirrored Copy", new MappingMessageReplacementModel("25% Mirrored Copy") },
            { "5% chance for Humanoid Corpses to not be consumed when Exorcising, up to 50%", new MappingMessageReplacementModel(" 5% Not Be Consumed(All)") },
            { "5% chance for Beast Corpses to not be consumed when Exorcising, up to 50%", new MappingMessageReplacementModel(" 5% Not Be Consumed(All)") },
            { "5% chance for Undead Corpses to not be consumed when Exorcising, up to 50%", new MappingMessageReplacementModel(" 5% Not Be Consumed(All)") },
            { "5% chance for Construct Corpses to not be consumed when Exorcising, up to 50%", new MappingMessageReplacementModel(" 5% Not Be Consumed(All)") },
            { "5% chance for Demon Corpses to not be consumed when Exorcising, up to 50%", new MappingMessageReplacementModel(" 5% Not Be Consumed(All)") },
            { "25% increased Effect of Corpses in this Grave Row", new MappingMessageReplacementModel("25% Row") },
            { "25% chance to create a Split Copy", new MappingMessageReplacementModel("25% Split Copy") },
        };
    }



    public class MatchedData
    {

    }
}
