using System;

namespace LocalArtisanMarket
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockAvailable { get; set; }
    }

    public class CartItem
    {
        public Product SelectedProduct { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => SelectedProduct.Price * Quantity;
    }
}