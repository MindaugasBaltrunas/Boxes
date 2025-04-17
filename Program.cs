using BoxEntityNamespace;
using Boxes;
using FileReaderNamespace;
using InputHandling;

namespace BoxManagementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));
            string filePath = Path.Combine(projectDirectory, "Duom.txt");

            Console.WriteLine($"Reading file from: {filePath}");

            var fileReader = new FileReader();
            var boxEntityParser = new BoxEntity();

            List<BoxEntity> boxes = fileReader.ReadFile(filePath, parts =>
            {
                if (boxEntityParser.TryCreate(parts, out BoxEntity box))
                {
                    return box;
                }
                else
                {
                    string invalidData = string.Join(" ", parts);
                    throw new FormatException($"Invalid data format in file. Offending data: '{invalidData}'. Please check the file contents.");
                }
            });

            var boxManager = new BoxManager(boxes);
            var boxEntity = new BoxEntity();

            //boxManager.DisplayBoxes();
            //boxManager.SortByWeight();

            //boxManager.FilterByMaterial("Medis");
            //boxManager.SortByMaterialAndWeight();
            //boxManager.FindLargestBox();
            var inputHandler = new InputHandler();
            int leght = inputHandler.GetInt("Įveskite sandelio ilgį cm: ");
            int width = inputHandler.GetInt("Įveskite sandelio plotį cm: ");
            int height = inputHandler.GetInt("Įveskite sandelio aukštį cm: ");

            var result = boxManager.StockSize(leght, width, height);
            Console.WriteLine($"Shelves in stock: {result.RequiredShelves}");
            Console.WriteLine($"Shelves hieght in stock: {result.ShelfHeight}");


        }
    }
}


//1. Sukurkite klasę BoxEntity, kuri aprašytų dėžės duomenis. Klasėje turi būti: medžiaga,
//iš kurios pagaminta dėžė, ilgis, plotis, aukštis, maksimalus produkcijos svoris, kurį gali
//atlaikyti dėžė, bei kiek dėžių galima sukrauti viena ant kitos

//2. Sukurkite klasę FileReader, kuri leistų nuskaityti duomenis iš failo.
//Klasėje turi būti:Parašykite programą, kuri spausdintų dėžių duomenis lentele
//surastų dėžę, į kurią telpa daugiausiai produkcijos ir suskaičiuotų kokio aukščio
//lentynas turi pastatyti sandėlyje (trūkstamus sandėlio duomenis programa paprašo įvesti klaviatūra)

//3. Sukurkite klasę BoxEntity, kuri aprašytų dėžės duomenis. Klasėje turi būti: medžiaga,

//4.Papildykite programą veiksmais, kurie leistų atrinkti nurodytos
//medžiagos dėžes ir šį sąrašą surikiuoti pagal maksimalų produkcijos svorį ir leistiną dėžių sukrovimą
//viena ant kitos mažėjimo tvarka.