using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLibrary
{
    public class SlaveUserService
    { 
        /// <summary>
        /// List of users.
        /// </summary>
        private List<User> users;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="SlaveUserService"/> class.
        /// </summary>
        public SlaveUserService()
        {
            this.users = new List<User>();
        }

        public void Add(User user)
        {
            throw new NotSupportedException("This operation is not supported.");
        }

        public void Remove(User user)
        {
            throw new NotSupportedException("This operation is not supported.");
        }

        /// <summary>
        /// Searches for elements that matches the conditions defined by <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">
        /// The Predicat delegate that defines the conditions of the element to search for.
        /// </param>
        /// <returns>
        /// Elements that matches the conditions defined by <paramref name="condition"/>, if found; 
        /// otherwise, an empty list.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="condition"/> is null.
        /// </exception>
        public List<User> Search(Predicate<User> condition)
        {

            if (ReferenceEquals(condition, null))
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return this.users.FindAll(condition);
        }
    }
}

