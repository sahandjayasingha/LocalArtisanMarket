using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class ProductCard : UserControl
    {
        private ProductDTO _product;

        public event EventHandler<ProductDTO> OnAddToCart;

        public ProductCard()
        {
            InitializeComponent();
        }

        public ProductCard(ProductDTO product)
        {
            InitializeComponent();
            _product = product;

            
            lblTitle.Text = product.ProductName;
            lblPrice.Text = "Rs. " + product.Price.ToString("N2");

            
            lblDescription.Text = product.Description;

            
            if (product.Stock > 0)
            {
                numQuantity.Minimum = 1;
                numQuantity.Maximum = product.Stock; 
                numQuantity.Value = 1;
                btnAddToCart.Enabled = true;
            }
            else
            {
                
                numQuantity.Enabled = false;
                btnAddToCart.Enabled = false;
                btnAddToCart.Text = "Out of Stock";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            int requestedQty = (int)numQuantity.Value;

            if (requestedQty > 0 && requestedQty <= _product.Stock)
            {
                OnAddToCart?.Invoke(this, _product);
                MessageBox.Show($"{_product.ProductName} ({requestedQty}) added to cart successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Please select a valid quantity. Available stock: {_product.Stock}", "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }
    }
}