using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLibrary
{
    public class UserService
    {
        private List<User> users;
        private readonly IEqualityComparer<User> equalityComparer;
        private readonly Func<int> idGenerator;

        /// <summary>
        /// Constructor. Creates list of users.
        /// </summary>
        /// <param name="idGenerator">Delegate to generate Id.</param>
        /// <param name="equalityComparer">Determines how to find out if users are the same.</param>
        public UserService(Func<int> idGenerator = null, IEqualityComparer<User> equalityComparer = null)
        {
            if (ReferenceEquals(idGenerator, null))
            {
                int maxId;
                if (users != null)
                {
                    maxId = users.Max(user => user.Id);
                }
                else
                {
                    maxId = 0;
                } 
                this.idGenerator = () => maxId++;
            }
            else
            {
                this.idGenerator = idGenerator;
            }

            users = new List<User>();
            this.equalityComparer = equalityComparer ?? EqualityComparer<User>.Default;
        }

        /// <summary>
        /// Adds <paramref name="user"/> to users.
        /// </summary>
        /// <param name="user">User to add.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="user"/> is null.
        /// </exception>
        /// <exception cref="EmptyLastNameException">
        /// Thrown when <see cref="User.FirstName"/> is empty.
        /// </exception>
        /// <exception cref="AlreadyExistsException">
        /// Thrown when <paramref name="user"/> is already in users.
        /// </exception>
        public void Add(User user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                throw new EmptyLastNameException($"LastName of {nameof(user)} is empty.");
            }

            if (users.Contains(user,equalityComparer))
            {
                throw new AlreadyExistsException("Such user is already exists.");
            }

            user.Id = idGenerator();
            users.Add(user);
        }

        /// <summary>
        /// Removes <paramref name="user"/> from users.
        /// </summary>
        /// <param name="user">User to remove.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="user"/> is null.
        /// </exception>
        /// <exception cref="DoesNotExistsException">
        /// Thrown when <paramref name="user"/> does not exist in users.
        /// </exception>
        public void Remove(User user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!users.Contains(user,equalityComparer))
            {
                throw new DoesNotExistsException("Such user does not exists.");
            }

            users.Remove(user);
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

            return users.FindAll(condition);
        }
    }
}
