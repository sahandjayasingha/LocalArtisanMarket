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
            _connectionString = ArtisanDashboard.ConnectionString;
        }

        public DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();
            // Extended payload columns added to the live catalog mirror matrix
            string query = "SELECT ProductID, ProductName, Price, Description, Stock, OriginHub, CraftTechnique, MoistureMetric, ProcessingStage FROM Products";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public void AddProduct(ProductDTO product)
        {
            string query = @"INSERT INTO Products (ProductName, Price, Description, Stock, OriginHub, CraftTechnique, MoistureMetric, ProcessingStage) 
                             VALUES (@ProductName, @Price, @Description, @Stock, @OriginHub, @CraftTechnique, @MoistureMetric, @ProcessingStage)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                cmd.Parameters.AddWithValue("@OriginHub", product.OriginHub ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CraftTechnique", product.CraftTechnique ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MoistureMetric", product.MoistureMetric);
                cmd.Parameters.AddWithValue("@ProcessingStage", product.ProcessingStage ?? "Raw");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(ProductDTO product)
        {
            // Highly optimized SQL statement to prevent heavy data table locking structures
            string query = @"UPDATE Products 
                             SET ProductName = @ProductName, Price = @Price, Description = @Description, 
                                 Stock = @Stock, OriginHub = @OriginHub, CraftTechnique = @CraftTechnique,
                                 MoistureMetric = @MoistureMetric, ProcessingStage = @ProcessingStage
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
                cmd.Parameters.AddWithValue("@MoistureMetric", product.MoistureMetric);
                cmd.Parameters.AddWithValue("@ProcessingStage", product.ProcessingStage ?? "Processing");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

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
