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
    }
}
