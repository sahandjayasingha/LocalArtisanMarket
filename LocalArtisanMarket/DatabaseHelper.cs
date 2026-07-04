using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public static class DatabaseHelper
    {
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=LocalArtisanMarketDB;Integrated Security=True;Connect Timeout=30;";

        public static SqlConnection GetConnection()
        {
            InitializeDatabase();
            return new SqlConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            string masterConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;";

            using (SqlConnection conn = new SqlConnection(masterConnectionString))
            {
                string checkDBQuery = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LocalArtisanMarketDB') CREATE DATABASE LocalArtisanMarketDB;";
                using (SqlCommand cmd = new SqlCommand(checkDBQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string createUsersTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[Users] (
                                [UserID] INT IDENTITY(1,1) PRIMARY KEY,
                                [Email] VARCHAR(100) NOT NULL UNIQUE,
                                [PasswordHash] VARCHAR(255) NOT NULL,
                                [Role] VARCHAR(20) NOT NULL
                            );
                        END";
                    using (SqlCommand cmd = new SqlCommand(createUsersTable, conn)) { cmd.ExecuteNonQuery(); }

                    string insertCustomer = @"
                        IF NOT EXISTS (SELECT * FROM [dbo].[Users] WHERE Email = 'customer@gmail.com')
                        BEGIN
                            INSERT INTO [dbo].[Users] (Email, PasswordHash, Role) VALUES ('customer@gmail.com', '***', 'Customer');
                        END";
                    using (SqlCommand cmd = new SqlCommand(insertCustomer, conn)) { cmd.ExecuteNonQuery(); }

                    string insertArtisan = @"
                        IF NOT EXISTS (SELECT * FROM [dbo].[Users] WHERE Email = 'artisan@gmail.com')
                        BEGIN
                            INSERT INTO [dbo].[Users] (Email, PasswordHash, Role) VALUES ('artisan@gmail.com', '***', 'Artisan');
                        END";
                    using (SqlCommand cmd = new SqlCommand(insertArtisan, conn)) { cmd.ExecuteNonQuery(); }

                    string createProductsTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[Products] (
                                [ProductID] INT IDENTITY(1,1) PRIMARY KEY,
                                [ProductName] VARCHAR(100) NOT NULL,
                                [Price] DECIMAL(18,2) NOT NULL,
                                [Description] TEXT,
                                [StockQuantity] INT NOT NULL
                            );
                        END";
                    using (SqlCommand cmd = new SqlCommand(createProductsTable, conn)) { cmd.ExecuteNonQuery(); }

                    string createTrackingTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MaterialTracking]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[MaterialTracking] (
                                [TrackingID] INT IDENTITY(1,1) PRIMARY KEY,
                                [ProductID] INT NOT NULL,
                                [MoistureLevel] DECIMAL(18,2) NOT NULL,
                                [ProductionStage] VARCHAR(100) NOT NULL,
                                [SupplierInfo] VARCHAR(255) NOT NULL
                            );
                        END";
                    using (SqlCommand cmd = new SqlCommand(createTrackingTable, conn)) { cmd.ExecuteNonQuery(); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Initialization Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            InitializeDatabase();
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
            InitializeDatabase();
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