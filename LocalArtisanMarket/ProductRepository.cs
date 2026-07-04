using System;
using System.Data;
using System.Data.SqlClient;

namespace LocalArtisanMarket
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
            // This reads the connection path you set up in your dashboard file
            _connectionString = ArtisanDashboard.ConnectionString;
        }

        // 1. READ: Fetches the items to show in your grid view
        public DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();
            string query = "SELECT ProductID, ProductName, Price, Description, Stock, OriginHub, CraftTechnique FROM Products";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        // 2. CREATE: Saves a brand new item to the database
        public void AddProduct(ProductDTO product)
        {
            string query = @"INSERT INTO Products (ProductName, Price, Description, Stock, OriginHub, CraftTechnique) 
                             VALUES (@ProductName, @Price, @Description, @Stock, @OriginHub, @CraftTechnique)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                cmd.Parameters.AddWithValue("@OriginHub", product.OriginHub ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CraftTechnique", product.CraftTechnique ?? (object)DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 3. UPDATE: Saves edits made to an existing item
        public void UpdateProduct(ProductDTO product)
        {
            string query = @"UPDATE Products 
                             SET ProductName = @ProductName, Price = @Price, Description = @Description, 
                                 Stock = @Stock, OriginHub = @OriginHub, CraftTechnique = @CraftTechnique 
                             WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                cmd.Parameters.AddWithValue("@OriginHub", product.OriginHub ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CraftTechnique", product.CraftTechnique ?? (object)DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 4. DELETE: Removes an item from the database completely
        public void DeleteProduct(int productId)
        {
            string query = "DELETE FROM Products WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProductID", productId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
