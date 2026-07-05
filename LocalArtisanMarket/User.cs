using System;
using System.Collections.Generic;

namespace LocalArtisanMarket
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        private static List<User> _systemUsers = new List<User>()
        {
            new Artisan { UserID = 1, FullName = "Default Artisan", Email = "artisan@gmail.com", Password = "123", UserType = "Artisan", VillageLocation = "Molagoda", CraftSpecialization = "Pottery" },
            new Customer { UserID = 2, FullName = "Default Customer", Email = "customer@gmail.com", Password = "123", UserType = "Customer", DeliveryAddress = "Colombo" }
        };

        public static User Authenticate(string email, string password)
        {
            return _systemUsers.Find(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == password);
        }

        public static bool RegisterNewUser(User newUser)
        {
            if (_systemUsers.Exists(u => u.Email.Equals(newUser.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            newUser.UserID = _systemUsers.Count + 1;
            _systemUsers.Add(newUser);
            return true;
        }

        public static List<User> GetAllUsers()
        {
            return new List<User>(_systemUsers);
        }
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