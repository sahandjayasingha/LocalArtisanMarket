using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    // DELIVERABLE 1: Designed as a UserControl for Sahan's main frame panel
    public partial class ArtisanDashboard : UserControl
    {
        private int selectedProductId = -1; // Tracks selected catalog item
        private static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LocalArtisanMarketDb;Integrated Security=True;";


        public ArtisanDashboard()
        {
            InitializeComponent();
        }

        private void ArtisanDashboard_Load(object sender, EventArgs e)
        {
            LoadProductCatalog();
        }

        // DELIVERABLE 4 & 5: Live Product Preview Data Grid using ConnectionString
        private void LoadProductCatalog()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    // DELIVERABLE 3: Relational SQL schema fields mapped directly
                    string query = "SELECT ProductID, ProductName, Price, Description, Stock, OriginHub, CraftTechnique FROM Products";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvProducts.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading catalog: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DELIVERABLE 2: CRUD - CREATE operation to add a new local artisan item
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    string query = @"INSERT INTO Products (ProductName, Price, Description, Stock, OriginHub, CraftTechnique) 
                                     VALUES (@ProductName, @Price, @Description, @Stock, @OriginHub, @CraftTechnique)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@Stock", Convert.ToInt32(txtStock.Text.Trim()));
                        cmd.Parameters.AddWithValue("@OriginHub", txtOriginHub.Text.Trim());
                        cmd.Parameters.AddWithValue("@CraftTechnique", txtCraftTechnique.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Product created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadProductCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DELIVERABLE 6: CRUD - UPDATE operation for inventory management & restocking
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1)
            {
                MessageBox.Show("Please select a product from the preview grid to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    string query = @"UPDATE Products 
                                     SET ProductName = @ProductName, Price = @Price, Description = @Description, 
                                         Stock = @Stock, OriginHub = @OriginHub, CraftTechnique = @CraftTechnique 
                                     WHERE ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", selectedProductId);
                        cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@Stock", Convert.ToInt32(txtStock.Text.Trim()));
                        cmd.Parameters.AddWithValue("@OriginHub", txtOriginHub.Text.Trim());
                        cmd.Parameters.AddWithValue("@CraftTechnique", txtCraftTechnique.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Product inventory updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadProductCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DELIVERABLE 6: CRUD - DELETE operation to delist items from inventory safely
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1)
            {
                MessageBox.Show("Please select a product to delist.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure you want to delist this item from inventory?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", selectedProductId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Product delisted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadProductCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Synchronize selected DataGrid row back into the input forms automatically
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                selectedProductId = Convert.ToInt32(row.Cells["ProductID"].Value);
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                txtStock.Text = row.Cells["Stock"].Value.ToString();
                txtOriginHub.Text = row.Cells["OriginHub"].Value.ToString();
                txtCraftTechnique.Text = row.Cells["CraftTechnique"].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            selectedProductId = -1;
            txtProductName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            txtStock.Clear();
            txtOriginHub.Clear();
            txtCraftTechnique.Clear();
        }
    }
}
