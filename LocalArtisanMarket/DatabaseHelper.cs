using System;
using System.Data.SqlClient;

namespace LocalArtisanMarket
{
    public static class DatabaseHelper
    {

        private static string connectionString = @"Server=DESKTOP-0IHPJNN;Database=LocalArtisanMarketDB;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}