using System;
using System.Configuration;
using ServiceLibrary;

namespace ServiceApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filename = ConfigurationManager.AppSettings["fileName"];

            var service = new MasterUserService();

            // 1. Add a new user to the storage.
            User user = new User()
            {
                FirstName = "Arya",
                LastName = "Stark",
                DateOfBirth = DateTime.Now
            };
            service.Add(user);

            // 2. Remove an user from the storage.
            service.Remove(user);

            // 3. Search for an user by the first name.
            service.Add(user);
            service.Search(x => x.FirstName == "Arya");

            // 4. Search for an user by the last name.
            service.Search(x => x.LastName == "Stark");
        }
    }
}
