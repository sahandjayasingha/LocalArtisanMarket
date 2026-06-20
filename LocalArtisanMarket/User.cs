using System;

namespace LocalArtisanMarket
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }

    public class Artisan : User
    {
        public string VillageLocation { get; set; }
        public string CraftSpecialization { get; set; }
    }

    public class Customer : User
    {
        public string DeliveryAddress { get; set; }
    }
}