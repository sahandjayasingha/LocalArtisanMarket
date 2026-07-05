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
                        ProductCard card = new ProductCard(dto);
                        card.OnAddToCart += Card_OnAddToCart;
                        flowLayoutPanelCatalog.Controls.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void UpdateCartGridView()
        {
            // 1. THE NUKE: Erase the data AND completely destroy any ghost columns
            dgvCart.DataSource = null;
            dgvCart.Columns.Clear(); // <--- This wipes out those empty blank columns!

            // 2. Bind the fresh data
            dgvCart.DataSource = shoppingCart.ToList();

            // 3. Hide the default grey row-selector column
            dgvCart.RowHeadersVisible = false;

            // 4. Hide the raw underlying data object
            if (dgvCart.Columns["SelectedProduct"] != null)
            {
                dgvCart.Columns["SelectedProduct"].Visible = false;
            }

            DataGridViewButtonColumn removeBtn = new DataGridViewButtonColumn();
            removeBtn.Name = "RemoveBtn";
            removeBtn.HeaderText = "Action";
            removeBtn.Text = "❌";
            removeBtn.UseColumnTextForButtonValue = true;
            removeBtn.Width = 50;
            removeBtn.FlatStyle = FlatStyle.Flat;
            dgvCart.Columns.Add(removeBtn);



            if (dgvCart.Columns["Name"] != null)
            {
                dgvCart.Columns["Name"].HeaderText = "Name";
                dgvCart.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvCart.Columns["Name"].DisplayIndex = 0; 
            }

            if (dgvCart.Columns["Quantity"] != null)
            {
                dgvCart.Columns["Quantity"].HeaderText = "Qty";
                dgvCart.Columns["Quantity"].Width = 50;
                dgvCart.Columns["Quantity"].DisplayIndex = 1; 
            }

            if (dgvCart.Columns["TotalPrice"] != null)
            {
                dgvCart.Columns["TotalPrice"].HeaderText = "Price";
                dgvCart.Columns["TotalPrice"].DefaultCellStyle.Format = "N2";
                dgvCart.Columns["TotalPrice"].DisplayIndex = 2; 
            }

            if (dgvCart.Columns["RemoveBtn"] != null)
            {
                dgvCart.Columns["RemoveBtn"].DisplayIndex = 3; 
            }

           
            decimal grandTotal = 0;
            foreach (var item in shoppingCart)
            {
                grandTotal += item.TotalPrice;
            }

            lblTotal.Text = "Total: Rs " + grandTotal.ToString("0.00");
        }
        private void Card_OnAddToCart(object sender, ProductDTO itemToAdd)
        {
            ProductCard clickedCard = sender as ProductCard;
            if (clickedCard == null) return;

            var existingItem = shoppingCart.FirstOrDefault(i => i.SelectedProduct.ProductID == itemToAdd.ProductID);
            int currentQtyInCart = existingItem != null ? existingItem.Quantity : 0;

            int quantityToAdd = clickedCard.GetSelectedQuantity();
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
                shoppingCart.Add(new CartItem { SelectedProduct = itemToAdd, Quantity = quantityToAdd });
            }

            UpdateCartGridView();
            ShowInlineNotification($"{itemToAdd.ProductName} added to cart.", false);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void CustomerDashboard_Load(object sender, EventArgs e) { }
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

        // This is the correct, single version of the click event!
        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure they clicked a valid row AND they clicked our specific Remove button
            if (e.RowIndex >= 0 && dgvCart.Columns[e.ColumnIndex].Name == "RemoveBtn")
            {
                string itemName = shoppingCart[e.RowIndex].Name;

                DialogResult dialogResult = MessageBox.Show(
                    $"Are you sure you want to remove '{itemName}' from your cart?",
                    "Remove Item",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    shoppingCart.RemoveAt(e.RowIndex);
                    UpdateCartGridView();
                    ShowInlineNotification($"{itemName} removed from cart.", false);
                }
            }
        }
    }
}