using System;

namespace LocalArtisanMarket
{
    // 1. Parent User Class
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }

    // 2. Child Class for Artisan (Inheritance)
    public class Artisan : User
    {
        public string VillageLocation { get; set; }
        public string CraftSpecialization { get; set; }
    }

    // 3. Child Class for Customer (Inheritance)
    public class Customer : User
    {
        public string ShippingAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    // 4. Product Class (For Sathsarani & Alwis)
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductDescription { get; set; }
        public string CraftStory { get; set; }
        public int StockQty { get; set; }
        public int ArtisanID { get; set; }
    }

    // 5. Material Class (For Insara)
    public class Material
    {
        public int MaterialID { get; set; }
        public string MaterialType { get; set; }
        public string BatchID { get; set; }
        public string MoistureStatus { get; set; }
        public string ProcessingStage { get; set; }
        public int DamagedQty { get; set; }
        public int AvailableQty { get; set; }
        public int ArtisanID { get; set; }
    }
}