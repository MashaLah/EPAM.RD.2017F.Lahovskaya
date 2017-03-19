using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    public interface IUserStorage
    {
        void SaveUsers(IEnumerable<User> users);
        IEnumerable<User> LoadUsers();
    }
}
