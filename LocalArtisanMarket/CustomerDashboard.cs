using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class CustomerDashboard : UserControl
    {
        private List<CartItem> shoppingCart = new List<CartItem>();

        public CustomerDashboard()
        {
            InitializeComponent();
            ApplyPremiumStyle();
            LoadCatalogToScreen();
        }

        private void ApplyPremiumStyle()
        {
            Control catalog = flowLayoutPanelCatalog;
            Control grid = dgvCart;
            Control total = lblTotal;
            Control checkout = btnCheckout;
            Control status = lblStatus;

            this.Controls.Clear();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(248, 246, 242);

            TableLayoutPanel tlpMain = new TableLayoutPanel();
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.ColumnCount = 2;
            tlpMain.RowCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 370F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.BackColor = Color.FromArgb(248, 246, 242);
            this.Controls.Add(tlpMain);

            Panel pnlRight = new Panel();
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.BackColor = Color.White;
            pnlRight.Padding = new Padding(20);
            tlpMain.Controls.Add(pnlRight, 1, 0);

            Panel pnlLeft = new Panel();
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.BackColor = Color.FromArgb(248, 246, 242);
            pnlLeft.Padding = new Padding(15, 15, 5, 15);
            tlpMain.Controls.Add(pnlLeft, 0, 0);

            if (catalog != null)
            {
                catalog.Dock = DockStyle.Fill;
                ((FlowLayoutPanel)catalog).AutoScroll = true;
                ((FlowLayoutPanel)catalog).WrapContents = true;
                catalog.BackColor = Color.FromArgb(248, 246, 242);
                pnlLeft.Controls.Add(catalog);
            }

            Label lblCartTitle = new Label
            {
                Text = "Shopping Cart",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(48, 36, 26),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlRight.Controls.Add(lblCartTitle);

            if (grid != null)
            {
                grid.Dock = DockStyle.Top;
                grid.Height = 350;
                DataGridView dgv = (DataGridView)grid;
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.None;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 246, 242);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 85, 75);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                dgv.ColumnHeadersHeight = 45;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 238, 235);
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10.5F);
                dgv.RowTemplate.Height = 45;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.ReadOnly = true;
                pnlRight.Controls.Add(grid);
                grid.BringToFront();
            }

            Panel pnlSpacer1 = new Panel { Dock = DockStyle.Top, Height = 20 };
            pnlRight.Controls.Add(pnlSpacer1);
            pnlSpacer1.BringToFront();

            if (total != null)
            {
                total.Dock = DockStyle.Top;
                total.Height = 40;
                total.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                total.ForeColor = Color.FromArgb(145, 95, 50);
                ((Label)total).TextAlign = ContentAlignment.MiddleRight;
                pnlRight.Controls.Add(total);
                total.BringToFront();
            }

            Panel pnlSpacer2 = new Panel { Dock = DockStyle.Top, Height = 20 };
            pnlRight.Controls.Add(pnlSpacer2);
            pnlSpacer2.BringToFront();

            if (checkout != null)
            {
                checkout.Dock = DockStyle.Top;
                checkout.Height = 55;
                checkout.Text = "Proceed to Checkout";
                checkout.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                checkout.BackColor = Color.FromArgb(65, 110, 75);
                checkout.ForeColor = Color.White;
                ((Button)checkout).FlatStyle = FlatStyle.Flat;
                ((Button)checkout).FlatAppearance.BorderSize = 0;
                checkout.Cursor = Cursors.Hand;
                pnlRight.Controls.Add(checkout);
                checkout.BringToFront();
            }

            Panel pnlSpacer3 = new Panel { Dock = DockStyle.Top, Height = 15 };
            pnlRight.Controls.Add(pnlSpacer3);
            pnlSpacer3.BringToFront();

            if (status != null)
            {
                status.Dock = DockStyle.Top;
                status.Height = 50;
                status.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                ((Label)status).TextAlign = ContentAlignment.MiddleCenter;
                status.Visible = false;
                pnlRight.Controls.Add(status);
                status.BringToFront();
            }
        }

        private void LoadCatalogToScreen()
        {
            if (flowLayoutPanelCatalog == null) return;

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
            if (dgvCart == null) return;

            dgvCart.DataSource = null;
            dgvCart.Columns.Clear();

            dgvCart.DataSource = shoppingCart.ToList();
            dgvCart.RowHeadersVisible = false;

            if (dgvCart.Columns["SelectedProduct"] != null)
            {
                dgvCart.Columns["SelectedProduct"].Visible = false;
            }

            DataGridViewButtonColumn removeBtn = new DataGridViewButtonColumn();
            removeBtn.Name = "RemoveBtn";
            removeBtn.HeaderText = "Del";
            removeBtn.Text = "❌";
            removeBtn.UseColumnTextForButtonValue = true;
            removeBtn.Width = 45;
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
                dgvCart.Columns["Quantity"].Width = 45;
                dgvCart.Columns["Quantity"].DisplayIndex = 1;
            }

            if (dgvCart.Columns["TotalPrice"] != null)
            {
                dgvCart.Columns["TotalPrice"].HeaderText = "Price";
                dgvCart.Columns["TotalPrice"].DefaultCellStyle.Format = "N2";
                dgvCart.Columns["TotalPrice"].Width = 85;
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

            if (lblTotal != null)
            {
                lblTotal.Text = "Total: Rs " + grandTotal.ToString("0.00");
            }
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

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (shoppingCart.Count == 0)
            {
                ShowInlineNotification("Cart is empty. Please add items to checkout.", true);
                return;
            }

            string token = DatabaseHelper.ProcessCheckoutBatch(shoppingCart);

            if (!string.IsNullOrEmpty(token))
            {
                shoppingCart.Clear();
                UpdateCartGridView();
                ShowInlineNotification($"Checkout successful! Your Token: {token}", false);
                LoadCatalogToScreen();
            }
            else
            {
                ShowInlineNotification("Checkout failed. Items may be out of stock.", true);
            }
        }

        private void ShowInlineNotification(string message, bool isError)
        {
            if (lblStatus == null) return;

            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? Color.FromArgb(200, 50, 50) : Color.FromArgb(50, 150, 80);
            lblStatus.Visible = true;
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void CustomerDashboard_Load(object sender, EventArgs e) { }
        private void flowLayoutPanelCatalog_Paint(object sender, PaintEventArgs e) { }
    }
}