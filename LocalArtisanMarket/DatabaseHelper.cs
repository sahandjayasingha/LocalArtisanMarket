using System;
using System.Data.SqlClient;

namespace LocalArtisanMarket
{
    public sealed class DatabaseHelper
    {
        private static readonly DatabaseHelper instance = new DatabaseHelper();
        private readonly string connectionString = @"Data Source=.;Initial Catalog=LocalArtisanMarketDB;Integrated Security=True;TrustServerCertificate=True;";

        private DatabaseHelper() { }

        public static DatabaseHelper Instance
        {
            get { return instance; }
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}