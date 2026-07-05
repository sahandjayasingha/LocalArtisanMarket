using LocalArtisanMarket;

public class CartItem
{
    
    public ProductDTO SelectedProduct { get; set; }


    public string Name
    {
        get
        {
            if (SelectedProduct != null) return SelectedProduct.ProductName;
            return "";
        }
    }

    public int Quantity { get; set; }

    public decimal TotalPrice
    {
        get
        {
            if (SelectedProduct != null)
            {
                return SelectedProduct.Price * Quantity;
            }
            return 0;
        }
    }
}