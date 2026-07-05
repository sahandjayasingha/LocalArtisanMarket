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

            // This is the correct, safe way to load the image. 
            // I removed the duplicate block that was at the bottom!
            if (!string.IsNullOrWhiteSpace(product.ImagePath))
            {
                string fullPath = System.IO.Path.Combine(Application.StartupPath, product.ImagePath);
                if (System.IO.File.Exists(fullPath))
                {
                    pictureBox1.Image = Image.FromFile(fullPath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }

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

        public int GetSelectedQuantity()
        {
            return (int)numQuantity.Value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (_product != null)
            {
                using (CraftStoryForm storyForm = new CraftStoryForm(_product))
                {
                    storyForm.ShowDialog();
                }
            }
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
    }
}