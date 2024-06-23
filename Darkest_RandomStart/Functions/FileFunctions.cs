using Newtonsoft.Json;

namespace Darkest_RandomStart
{
    namespace Darkest_RandomStart
    {
        public static class FileFunctions
        {
            public static string currnetDirectory = Directory.GetCurrentDirectory();
            public static T ReadJsonFile<T>(string fileName, string directory)
            {
                try
                {
                    string jsonFilePath = Path.Combine(directory, fileName);

                    if (!File.Exists(jsonFilePath))
                    {
                        throw new FileNotFoundException($"JSON file '{fileName}' not found in directory '{directory}'.");
                    }

                    string jsonContent = File.ReadAllText(jsonFilePath);
                    return JsonConvert.DeserializeObject<T>(jsonContent);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Environment.Exit(1); // Terminate the program
                    throw; // Rethrow to ensure termination
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading JSON file '{fileName}' from directory '{directory}': {ex.Message}");
                    Environment.Exit(1); // Terminate the program
                    throw; // Rethrow to ensure termination
                }
            }
            public static void SaveJsonToFile(string jsonContent, string fileName, string directory)
            {
                // Ensure the directory exists
                Directory.CreateDirectory(directory);

                // Save JSON to a file in the specified directory
                string filePath = Path.Combine(directory, fileName);
                File.WriteAllText(filePath, jsonContent);
            }
        }
    }
}