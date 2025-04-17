using System.Globalization;

namespace InputHandling
{
    public class InputHandler
    {
        public int GetInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= min && result <= max)
                    return result;
                Console.WriteLine($"Įveskite skaičių nuo {min} iki {max}.");
            }
        }
    }
}