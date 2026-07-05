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
    public partial class CustomerDashboard : UserControl
    {
        private List<CartItem> shoppingCart = new List<CartItem>();
        private bool _isCartError = false;

        public CustomerDashboard()
        {
            InitializeComponent();

            btnCheckout.AutoSize = true;
            btnCheckout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCheckout.Padding = new Padding(10, 5, 10, 5);

            LoadCatalogToScreen();
        }
        private void LoadCatalogToScreen()
        {
            flowLayoutPanelCatalog.Controls.Clear();

            try
            {
                ProductBusinessLogic businessLogic = new ProductBusinessLogic();
                List<ProductDTO> dtos = businessLogic.GetCatalog();

                if (dtos != null)
                {
                    foreach (ProductDTO dto in dtos)
                    {
                        // This passes the DTO directly to the card (Fixes CS1503)
                        ProductCard card = new ProductCard(dto);
                        card.OnAddToCart += Card_OnAddToCart;
                        flowLayoutPanelCatalog.Controls.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                // Using 'ex' here fixes the unused variable warning (CS0168)
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }


        private void UpdateCartGridView()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = shoppingCart;

            if (dgvCart.Columns["SelectedProduct"] != null)
            {
                dgvCart.Columns["SelectedProduct"].Visible = false;
            }

            decimal grandTotal = 0;
            foreach (var item in shoppingCart)
            {
                grandTotal += item.TotalPrice;
            }

            lblTotal.Text = "Total: $" + grandTotal.ToString("0.00");
        }

        private void Card_OnAddToCart(object sender, ProductDTO itemToAdd)
        {
            var existingItem = shoppingCart.FirstOrDefault(i => i.SelectedProduct.ProductID == itemToAdd.ProductID);
            int currentQtyInCart = existingItem != null ? existingItem.Quantity : 0;
            int quantityToAdd = 1;
            int requestedTotalQty = currentQtyInCart + quantityToAdd;

            if (requestedTotalQty > itemToAdd.Stock)
            {
                ShowInlineNotification($"Cannot add {itemToAdd.ProductName}. Insufficient stock.", true);
                return;
            }

            if (existingItem != null)
            {
                existingItem.Quantity += quantityToAdd;
            }
            else
            {
                // Line 108 Fix: We pass the itemToAdd (which is a ProductDTO) directly!
                shoppingCart.Add(new CartItem { SelectedProduct = itemToAdd, Quantity = quantityToAdd });
            }

            UpdateCartGridView();
            ShowInlineNotification($"{itemToAdd.ProductName} added to cart.", false);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void CustomerDashboard_Load(object sender, EventArgs e) { }
        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void flowLayoutPanelCatalog_Paint(object sender, PaintEventArgs e) { }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (shoppingCart.Count == 0)
            {
                _isCartError = true;
                this.Invalidate();
                ShowInlineNotification("Cart is empty. Please add items to checkout.", true);
                return;
            }

            _isCartError = false;
            this.Invalidate();


            bool success = DatabaseHelper.ProcessCheckoutBatch(shoppingCart);

            if (success)
            {
                shoppingCart.Clear();
                UpdateCartGridView();
                ShowInlineNotification("Checkout successful! Thank you.", false);
                LoadCatalogToScreen();
            }
            else
            {
                ShowInlineNotification("Checkout failed during database transaction.", true);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_isCartError)
            {
                Rectangle rect = new Rectangle(dgvCart.Location.X - 2, dgvCart.Location.Y - 2, dgvCart.Width + 4, dgvCart.Height + 4);
                ControlPaint.DrawBorder(e.Graphics, rect, Color.Red, ButtonBorderStyle.Solid);
            }
        }

        private void ShowInlineNotification(string message, bool isError)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? Color.Red : Color.Green;
            lblStatus.Visible = true;
        }
    }
}