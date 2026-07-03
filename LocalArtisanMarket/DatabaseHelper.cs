using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public static class DatabaseHelper
    {
        
        private static string connectionString = @"Server=DESKTOP-0IHPJNN;Database=LocalArtisanMarketDB;Trusted_Connection=True;";

        
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        try
                        {
                            conn.Open();
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return dt;
                    }
                }
            }
        }

        
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
        }

        
        public static void LogTelemetryData(int productId, decimal moistureLevel, string stage, string supplier)
        {
            string query = "INSERT INTO MaterialTracking (ProductID, MoistureLevel, ProductionStage, SupplierInfo) VALUES (@ProductID, @Moisture, @Stage, @Supplier)";
            SqlParameter[] parameters = {
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Moisture", moistureLevel),
                new SqlParameter("@Stage", stage),
                new SqlParameter("@Supplier", supplier)
            };
            ExecuteNonQuery(query, parameters);
        }
    }
}