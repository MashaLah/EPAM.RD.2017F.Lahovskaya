using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    public sealed class UserEventArgs : EventArgs
    {
        public UserEventArgs(User user)
        {
            User = user;
        }
        public User User { get; }
    }
}
