
namespace FileReaderNamespace
{
    public class FileReader
    {
        public List<T> ReadFile<T>(string fileName, Func<string[], T> createItem)
        {
            var items = new List<T>();

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        items.Add(createItem(parts));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading data from file '{fileName}': {ex.Message}", ex);
            }

            return items;
        }

        public bool CreateEmptyTextFileIfNotExists(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, string.Empty);
                    Console.WriteLine($"Empty file successfully created: {filePath}");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating empty file '{filePath}': {ex.Message}");
                return false;
            }
        }
    }
}
