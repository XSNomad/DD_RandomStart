using Darkest_RandomStart.Darkest_RandomStart;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Darkest_RandomStart
{
    public class Quirk_file
    {
        public string Id { get; set; }
        [JsonProperty("is_positive")]
        public bool IsPositive { get; set; }
        [JsonProperty("is_disease")]
        public bool IsDisease { get; set; }
        [JsonProperty("incompatible_quirks")]
        public List<string> IncompatibleQuirks { get; set; }
        // Add other properties as needed
    }

    public class QuirkLibrary
    {
        public List<Quirk_file> Quirks { get; set; }
    }

    public class QuirkManager
    {
        private Quirk QuirkDefault = new(); // Rename to DefaultQuirk for clarity
        private static readonly Random random = new();

        public Dictionary<string, Quirk> GetRandomQuirks()
        {
            try
            {
                // Read JSON file using FileFunctions
                string fileName = "quirk_library.json";
                string directory = Path.Combine("..", "..", "shared", "quirk");
                QuirkLibrary quirkLibrary = FileFunctions.ReadJsonFile<QuirkLibrary>(fileName, directory);

                if (quirkLibrary == null || quirkLibrary.Quirks == null || quirkLibrary.Quirks.Count == 0)
                {
                    throw new Exception("No quirks found in the JSON file.");
                }

                // Combine positive and negative quirks
                List<Quirk_file> allQuirks = quirkLibrary.Quirks.ToList();

                // Shuffle the list of quirks
                allQuirks.Shuffle(); // Assuming Shuffle is a custom extension method

                // Select up to 2 positive quirk IDs
                List<string> selectedPositiveQuirkIds = allQuirks
                    .Where(q => q.IsPositive)
                    .Take(random.Next(1, 3))
                    .Select(q => q.Id)
                    .ToList();
                allQuirks.RemoveAll(q => q.IncompatibleQuirks.Any(selectedPositiveQuirkIds.Contains));

                // Select up to 2 negative quirk IDs
                List<string> selectedNegativeQuirkIds = allQuirks
                    .Where(q => !q.IsPositive)
                    .Take(random.Next(0, 3))
                    .Select(q => q.Id)
                    .ToList();

                // Select up to 1 disease quirk ID
                List<string> selectedDiseaseQuirkIds = allQuirks
                    .Where(q => q.IsDisease)
                    .Take(random.Next(0, 2)) // Adjusted to take up to 1 disease quirk
                    .Select(q => q.Id)
                    .ToList();

                // Combine all selected quirk IDs into a single list
                var allSelectedQuirkIds = selectedPositiveQuirkIds
                    .Concat(selectedNegativeQuirkIds)
                    .Concat(selectedDiseaseQuirkIds);

                Dictionary<string, Quirk> selectedQuirkIds = new();

                // Add all selected quirk IDs to the dictionary with default Quirk object
                foreach (var id in allSelectedQuirkIds)
                {
                    selectedQuirkIds.Add(id, QuirkDefault);
                }

                return selectedQuirkIds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRandomQuirks: {ex.Message}");
                return new Dictionary<string, Quirk>(); // Return empty dictionary on error
            }
        }
    }
}
