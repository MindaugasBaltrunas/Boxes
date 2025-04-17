using BoxEntityNamespace;

namespace Boxes
{
    public class BoxManager
    {
        private List<BoxEntity> _boxes;

        public BoxManager(List<BoxEntity> boxes)
        {
            _boxes = boxes ?? new List<BoxEntity>();
        }

        private double CalculateVolume(BoxEntity box)
        {
            return (box.Length * box.Width * box.Height) / 1000000;
        }

        public void DisplayBoxes()
        {
            if (_boxes.Count == 0)
            {
                Console.WriteLine("No boxes to display.");
                return;
            }

            Console.WriteLine($"Displaying {_boxes.Count} boxes:");
            Console.WriteLine(new string('=', 70));

            foreach (var box in _boxes)
            {
                Console.WriteLine(box.ToString());
                Console.WriteLine($"Box volume: {CalculateVolume(box):F2} m³");
                Console.WriteLine(new string('-', 60));
            }
        }

        public void SortByWeight()
        {
            _boxes.Sort((x, y) => x.MaxWeight.CompareTo(y.MaxWeight));
            DisplayBoxes();
        }

        public void SortByMaterialAndWeight()
        {
            _boxes = _boxes
                .GroupBy(b => b.Material)
                .SelectMany(g => g
                    .OrderBy(b => b.MaxWeight)
                )
                .ToList();

            DisplayBoxes();
        }

        public void FindLargestBox()
        {
            if (_boxes.Count == 0)
            {
                Console.WriteLine("No boxes available to find the biggest one.");
                return; // Added return to prevent further execution
            }

            var biggestBox = _boxes.OrderByDescending(b => CalculateVolume(b)).First();

            Console.WriteLine($"Largest box: {biggestBox}");
            Console.WriteLine($"Volume: {CalculateVolume(biggestBox):F2} m³");
        }

        public void FilterByMaterial(string material)
        {
            var items = _boxes
                 .Where(b => b.Material.Equals(material, StringComparison.OrdinalIgnoreCase))
                 .ToList();
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public (int ShelfHeight, int RequiredShelves) StockSize(int length, int width, int height)
        {
            var widestBox = _boxes.OrderByDescending(b => b.Width).First();
            var longestBox = _boxes.OrderByDescending(b => b.Length).First();
            var tallestBox = _boxes.OrderByDescending(b => b.Height).First();

            if (width <= widestBox.Width)
            {
                throw new ArgumentException($"Width too small for some boxes: {widestBox.Material} ({widestBox.Width})");
            }

            if (length <= longestBox.Length)
            {
                throw new ArgumentException($"Length too small for some boxes: {longestBox.Material} ({longestBox.Length})");
            }

            if (height <= tallestBox.Height)
            {
                throw new ArgumentException($"Height too small for some boxes: {tallestBox.Material} ({tallestBox.Height})");
            }

            var averageLength = _boxes.Average(b => b.Length);
            var averageWidth = _boxes.Average(b => b.Width);
            var averageHeight = _boxes.Average(b => b.Height);

            var boxesPerRow = (int)(width / averageWidth);
            var rowsPerShelf = (int)(length / averageLength);
            var shelvesPerUnit = (int)(height / averageHeight);

            var boxesPerShelf = boxesPerRow * shelvesPerUnit;
            var totalCapacity = boxesPerShelf * rowsPerShelf;

            if (totalCapacity < _boxes.Count)
            {
                throw new ArgumentException($"Not enough space for all boxes: capacity {totalCapacity}, required {_boxes.Count}");
            }

            if (boxesPerShelf == 0)
            {
                return (0, 0);
            }

            var shelfHeight = (int)(shelvesPerUnit * averageHeight);
            var requiredShelves = (int)Math.Ceiling((double)_boxes.Count / boxesPerShelf);

            return (shelfHeight, requiredShelves);
        }
    }
}
