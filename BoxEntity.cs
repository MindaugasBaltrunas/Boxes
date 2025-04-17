namespace BoxEntityNamespace
{
    public class BoxEntity
    {
        public string Material { get; }
        public double Length { get; }
        public double Width { get; }
        public double Height { get; }
        public double MaxWeight { get; }
        public int Quantity { get; }

        public BoxEntity()
        {
        }

        public BoxEntity(string material, double length, double width, double height, double maxWeight, int quantity)
        {
            Material = material ?? throw new ArgumentNullException(nameof(material));

            if (length <= 0) throw new ArgumentException("Length must be positive", nameof(length));
            if (width <= 0) throw new ArgumentException("Width must be positive", nameof(width));
            if (height <= 0) throw new ArgumentException("Height must be positive", nameof(height));
            if (maxWeight <= 0) throw new ArgumentException("MaxWeight must be positive", nameof(maxWeight));
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive", nameof(quantity));

            Length = length;
            Width = width;
            Height = height;
            MaxWeight = maxWeight;
            Quantity = quantity;
        }

        public bool TryCreate(string[] parts, out BoxEntity box)
        {
            box = null;

            if (parts == null || parts.Length < 6)
                return false;

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i]?.Trim() ?? string.Empty;
            }

            if (string.IsNullOrWhiteSpace(parts[0]))
                return false;

            if (double.TryParse(parts[1], out double length) &&
                double.TryParse(parts[2], out double width) &&
                double.TryParse(parts[3], out double height) &&
                double.TryParse(parts[4], out double maxWeight) &&
                int.TryParse(parts[5], out int quantity) &&
                length > 0 && width > 0 && height > 0 && maxWeight > 0 && quantity > 0)
            {
                try
                {
                    box = new BoxEntity(parts[0], length, width, height, maxWeight, quantity);
                    return true;
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }

            return false;
        }
        public override string ToString()
        {
            return $"Material: {Material}, Dimensions: {Length:F2} x {Width:F2} x {Height:F2}, " +
                   $"Max Weight: {MaxWeight:F2}, Quantity: {Quantity}";
        }
    } 
}