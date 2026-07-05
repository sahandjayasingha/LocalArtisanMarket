namespace LocalArtisanMarket
{
    // MODULE 3: Extended Telemetry Data Transfer Object
    public class ProductDTO
    {
        public int ProductID { get; }
        public string ProductName { get; }
        public decimal Price { get; }
        public string Description { get; }
        public int Stock { get; }
        public string OriginHub { get; }
        public string CraftTechnique { get; }

        // Extended material vectors for Insara's telemetry subsystem
        public decimal MoistureMetric { get; }
        public string ProcessingStage { get; }

        public ProductDTO(int productId, string productName, decimal price, string description, int stock, string originHub, string craftTechnique, decimal moistureMetric, string processingStage)
        {
            ProductID = productId;
            ProductName = productName;
            Price = price;
            Description = description;
            Stock = stock;
            OriginHub = originHub;
            CraftTechnique = craftTechnique;
            MoistureMetric = moistureMetric;
            ProcessingStage = processingStage;
        }
    }
}
