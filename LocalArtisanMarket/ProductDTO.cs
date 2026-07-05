namespace LocalArtisanMarket
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string OriginHub { get; set; }
        public string CraftTechnique { get; set; }
        public decimal MoistureMetric { get; set; }
        public string ProcessingStage { get; set; }
        public string ImagePath { get; set; }
        public string StoryText { get; set; }
        public string StoryImagePath { get; set; }

        public ProductDTO(int productId, string productName, decimal price, string description, int stock, string originHub, string craftTechnique, decimal moistureMetric, string processingStage, string imagePath = "", string storyText = "", string storyImagePath = "")
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
            ImagePath = imagePath;
            StoryText = storyText;
            StoryImagePath = storyImagePath;
        }
    }
}