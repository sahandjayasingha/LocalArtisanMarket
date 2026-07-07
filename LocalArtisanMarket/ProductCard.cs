using System;
using System.ComponentModel;
using System.Drawing;
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
            ApplyPremiumStyle();
        }

        public ProductCard(ProductDTO product)
        {
            InitializeComponent();
            _product = product;
            ApplyPremiumStyle();

            lblTitle.Text = product.ProductName;
            lblPrice.Text = "Rs. " + product.Price.ToString("N2");
            lblDescription.Text = product.Description;

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
                btnAddToCart.Text = "Add to Cart";
                btnAddToCart.BackColor = Color.FromArgb(65, 110, 75);
            }
            else
            {
                numQuantity.Enabled = false;
                btnAddToCart.Enabled = false;
                btnAddToCart.Text = "Out of Stock";
                btnAddToCart.BackColor = Color.Gray;
            }
        }

        private void ApplyPremiumStyle()
        {
            this.Size = new Size(250, 360);
            this.BackColor = Color.White;
            this.Margin = new Padding(15);
            this.Cursor = Cursors.Default;

            this.Paint += (s, e) =>
            {
                using (Pen borderPen = new Pen(Color.FromArgb(220, 210, 195), 1))
                {
                    e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
                }
            };

            if (pictureBox1 != null)
            {
                pictureBox1.Location = new Point(0, 0);
                pictureBox1.Size = new Size(250, 160);
                pictureBox1.BackColor = Color.FromArgb(240, 238, 235);
                pictureBox1.Cursor = Cursors.Hand;
            }

            if (lblTitle != null)
            {
                lblTitle.Location = new Point(15, 175);
                lblTitle.AutoSize = false;
                lblTitle.Size = new Size(220, 45);
                lblTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
                lblTitle.ForeColor = Color.FromArgb(48, 36, 26);
            }

            if (lblDescription != null)
            {
                lblDescription.Location = new Point(15, 220);
                lblDescription.AutoSize = false;
                lblDescription.Size = new Size(220, 40);
                lblDescription.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                lblDescription.ForeColor = Color.FromArgb(120, 110, 100);
            }

            if (lblPrice != null)
            {
                lblPrice.Location = new Point(15, 265);
                lblPrice.AutoSize = true;
                lblPrice.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                lblPrice.ForeColor = Color.FromArgb(145, 95, 50);
            }

            if (numQuantity != null)
            {
                numQuantity.Location = new Point(15, 310);
                numQuantity.Size = new Size(55, 30);
                numQuantity.Font = new Font("Segoe UI", 11F);
                numQuantity.BorderStyle = BorderStyle.FixedSingle;
            }

            if (btnAddToCart != null)
            {
                btnAddToCart.Location = new Point(80, 308);
                btnAddToCart.Size = new Size(155, 34);
                btnAddToCart.FlatStyle = FlatStyle.Flat;
                btnAddToCart.FlatAppearance.BorderSize = 0;
                btnAddToCart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btnAddToCart.ForeColor = Color.White;
                btnAddToCart.Cursor = Cursors.Hand;
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