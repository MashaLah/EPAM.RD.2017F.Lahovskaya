using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Tests
{
    public class IdGenerator : IIdGenerator 
    {
        public int GenerateId(List<User> users)
        {
            if (users.Count == 0)
            {
                return 1;
            }

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
