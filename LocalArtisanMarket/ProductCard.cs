using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace LocalArtisanMarket
{
    public partial class ProductCard : UserControl
    {
        private Product _product;

        // This creates a custom event so the main dashboard can listen for clicks
        public event EventHandler<CartItem> OnAddToCart;
        public ProductCard(Product product)
        {
            InitializeComponent();
            _product = product;

            lblTitle.Text = product.Name;
            lblPrice.Text = "$" + product.Price.ToString("0.00");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ProductCard_Load(object sender, EventArgs e)
        {
            int requestedQty = (int)numQuantity.Value;

            if (requestedQty > 0 && requestedQty <= _product.StockAvailable)
            {
                // Package the selection and fire the event
                CartItem newItem = new CartItem { SelectedProduct = _product, Quantity = requestedQty };
                OnAddToCart?.Invoke(this, newItem);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.", "Stock Warning");
            }
        }
    }
}
