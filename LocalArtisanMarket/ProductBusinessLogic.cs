using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace LocalArtisanMarket
{
    public class ProductBusinessLogic
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=LocalArtisanMarketDB;Integrated Security=True;Connect Timeout=30;";

        public ProductBusinessLogic()
        {
        }

        public List<ProductDTO> GetCatalog()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // FIXED: Changed 'Stock' to 'StockQuantity AS Stock' 
                    string query = "SELECT ProductID, ProductName, Price, Description, StockQuantity AS Stock, OriginHub, CraftTechnique, MoistureMetric, ProcessingStage, ImagePath, StoryText, StoryImagePath FROM Products";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new ProductDTO(
                                    Convert.ToInt32(reader["ProductID"]),
                                    reader["ProductName"]?.ToString(),
                                    Convert.ToDecimal(reader["Price"]),
                                    reader["Description"]?.ToString(),
                                    Convert.ToInt32(reader["Stock"]), // This still works because of 'AS Stock'
                                    reader["OriginHub"]?.ToString(),
                                    reader["CraftTechnique"]?.ToString(),
                                    Convert.ToDecimal(reader["MoistureMetric"]),
                                    reader["ProcessingStage"]?.ToString(),
                                    reader["ImagePath"]?.ToString(),
                                    reader["StoryText"]?.ToString(),
                                    reader["StoryImagePath"]?.ToString()
                                ));
                            }
                        }
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data retrieval error: " + ex.Message, ex);
            }
        }

        public void ProcessProductCreation(ProductDTO product)
        {
            ValidateStateChangeInvariants(product);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // FIXED: Changed 'Stock' to 'StockQuantity' in the INSERT statement
                    string query = "INSERT INTO Products (ProductName, Price, Description, StockQuantity, OriginHub, CraftTechnique, MoistureMetric, ProcessingStage, ImagePath, StoryText, StoryImagePath) " +
                                   "VALUES (@Name, @Price, @Desc, @Stock, @Origin, @Technique, @Moisture, @Stage, @Img, @StoryTxt, @StoryImg)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", product.ProductName);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Desc", product.Description);
                        cmd.Parameters.AddWithValue("@Stock", product.Stock);
                        cmd.Parameters.AddWithValue("@Origin", product.OriginHub);
                        cmd.Parameters.AddWithValue("@Technique", product.CraftTechnique);
                        cmd.Parameters.AddWithValue("@Moisture", product.MoistureMetric);
                        cmd.Parameters.AddWithValue("@Stage", product.ProcessingStage);
                        cmd.Parameters.AddWithValue("@Img", product.ImagePath ?? "");
                        cmd.Parameters.AddWithValue("@StoryTxt", product.StoryText ?? "");
                        cmd.Parameters.AddWithValue("@StoryImg", product.StoryImagePath ?? "");

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database insertion error: " + ex.Message, ex);
            }
        }

        public void ProcessProductUpdate(ProductDTO product)
        {
            if (product.ProductID <= 0)
                throw new ArgumentException("Invalid state transition tracking reference ID.");

            ValidateStateChangeInvariants(product);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // FIXED: Changed 'Stock' to 'StockQuantity' in the UPDATE statement
                    string query = "UPDATE Products SET ProductName = @Name, Price = @Price, Description = @Desc, StockQuantity = @Stock, " +
                                   "OriginHub = @Origin, CraftTechnique = @Technique, MoistureMetric = @Moisture, ProcessingStage = @Stage, " +
                                   "ImagePath = @Img, StoryText = @StoryTxt, StoryImagePath = @StoryImg WHERE ProductID = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", product.ProductID);
                        cmd.Parameters.AddWithValue("@Name", product.ProductName);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Desc", product.Description);
                        cmd.Parameters.AddWithValue("@Stock", product.Stock);
                        cmd.Parameters.AddWithValue("@Origin", product.OriginHub);
                        cmd.Parameters.AddWithValue("@Technique", product.CraftTechnique);
                        cmd.Parameters.AddWithValue("@Moisture", product.MoistureMetric);
                        cmd.Parameters.AddWithValue("@Stage", product.ProcessingStage);
                        cmd.Parameters.AddWithValue("@Img", product.ImagePath ?? "");
                        cmd.Parameters.AddWithValue("@StoryTxt", product.StoryText ?? "");
                        cmd.Parameters.AddWithValue("@StoryImg", product.StoryImagePath ?? "");

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database update error: " + ex.Message, ex);
            }
        }

        public void ProcessProductDeletion(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid state tracking reference ID assigned for inventory removal.");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Products WHERE ProductID = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", productId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database deletion error: " + ex.Message, ex);
            }
        }

        private void ValidateStateChangeInvariants(ProductDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new ArgumentException("State validation failure: ProductName configuration parameter cannot be null or blank.");

            if (product.Stock < 0)
                throw new ArgumentException("State validation failure: Inventory rule violation. Stock parameters cannot accept mathematical negatives below 0.");

            if (product.Price <= 0)
                throw new ArgumentException("State validation failure: Market valuation parameters must strictly resolve to a positive, non-zero asset value.");

            if (decimal.Round(product.Price, 2) != product.Price)
                throw new ArgumentException("State validation failure: Price variable violates precision constraints. Values cannot extend beyond two decimal places.");

            if (product.Price > 999999.99m)
                throw new ArgumentException("State validation failure: Asset boundary threshold overflow. Price value exceeds enterprise storage configuration scale parameters.");
        }
    }
}