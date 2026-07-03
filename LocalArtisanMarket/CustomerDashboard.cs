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
    public partial class CustomerDashboard : UserControl
    {
        // 1. The memory for your shopping cart
        private List<CartItem> shoppingCart = new List<CartItem>();

        public CustomerDashboard()
        {
            InitializeComponent();
            LoadCatalogToScreen(); // FIX 1: This triggers the catalog to load immediately!
        }

        // 2. The method to fetch products from the database
        // 2. The method to fetch products from the database
        private List<Product> GetAvailableProducts()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT ProductID, ProductName, Price, Description, Stock FROM Products";

            try
            {
                // THE FIX: We now use your teammate's Singleton instance to get the connection!
                using (System.Data.SqlClient.SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product
                                {
                                    ProductID = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Price = reader.GetDecimal(2),
                                    Description = reader.GetString(3),
                                    StockAvailable = reader.GetInt32(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
            return products;
        }

        // FIX 2: The actual code to update your visual shopping cart!
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

            // Make sure you named your label lblTotal in the designer!
            lblTotal.Text = "Total: $" + grandTotal.ToString("0.00");
        }

        private void LoadCatalogToScreen()
        {
            List<Product> products = GetAvailableProducts();
            flowLayoutPanelCatalog.Controls.Clear();

            foreach (Product p in products)
            {
                ProductCard card = new ProductCard(p);
                card.OnAddToCart += Card_OnAddToCart;
                flowLayoutPanelCatalog.Controls.Add(card);
            }
        }

        private void Card_OnAddToCart(object sender, CartItem itemToAdd)
        {
            var existingItem = shoppingCart.FirstOrDefault(i => i.SelectedProduct.ProductID == itemToAdd.SelectedProduct.ProductID);

            if (existingItem != null)
            {
                existingItem.Quantity += itemToAdd.Quantity;
            }
            else
            {
                shoppingCart.Add(itemToAdd);
            }

            UpdateCartGridView();
        }

        // Keeping your auto-generated click events so the designer doesn't break
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void CustomerDashboard_Load(object sender, EventArgs e) { }
        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}