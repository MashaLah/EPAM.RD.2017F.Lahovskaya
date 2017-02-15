
using System;
using System.Collections.Generic;
using ServiceLibrary;

namespace ServiceApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IdGenerator idGenerator = new IdGenerator();
            var service = new UserService(idGenerator);

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
            service.Search(x => x.FirstName=="Arya");
            // 4. Search for an user by the last name.
            service.Search(x => x.LastName == "Stark");
        }

        public class IdGenerator : IIdGenerator
        {
            public int GenerateId(List<User> users)
            {
                if (users.Count == 0)
                    return 1;
                int id = getCurrentId(users);
                return id++;
            }

            private int getCurrentId(List<User> users)
            {
                users.Sort((x, y) => x.Id.CompareTo(y.Id));
                return users[0].Id;
            }
        }
    }
}
