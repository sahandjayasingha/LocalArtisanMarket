using System;

namespace LocalArtisanMarket
{
   

    public class CartItem
    {
        public ProductDTO SelectedProduct { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => SelectedProduct.Price * Quantity;
    }
}