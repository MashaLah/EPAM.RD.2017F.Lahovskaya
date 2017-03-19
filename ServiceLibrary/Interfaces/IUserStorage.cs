using System.Collections.Generic;

namespace ServiceLibrary
{
    public interface IUserStorage
    {
        void SaveUsers(IEnumerable<User> users);

        IEnumerable<User> LoadUsers();
    }
}
