using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLibrary
{
    public class MasterUserService
    {
        /// <summary>
        /// Equality comparer to instances of User class.
        /// </summary>
        private readonly IEqualityComparer<User> equalityComparer;

        /// <summary>
        /// Determines how to generate id.
        /// </summary>
        private readonly Func<int> idGenerator;

        /// <summary>
        /// List of users.
        /// </summary>
        private List<User> users;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterUserService"/> class.
        /// </summary>
        /// <param name="idGenerator">Delegate to generate Id.</param>
        /// <param name="equalityComparer">Determines how to find out if users are the same.</param>
        public MasterUserService(Func<int> idGenerator = null, IEqualityComparer<User> equalityComparer = null)
        {
            if (ReferenceEquals(idGenerator, null))
            {
                int maxId;
                if (this.users != null)
                {
                    maxId = this.users.Max(user => user.Id);
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

            this.users = new List<User>();
            this.equalityComparer = equalityComparer ?? EqualityComparer<User>.Default;
        }

        public event EventHandler<UserEventArgs> UserAdded;

        public event EventHandler<UserEventArgs> UserRemoved;

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
                var ex = new ArgumentNullException(nameof(user));
                logger?.Trace(ex);
                throw ex;
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                var ex = new EmptyLastNameException($"LastName of {nameof(user)} is empty.");
                logger?.Trace(ex);
                throw ex;
            }

            if (this.users.Contains(user, this.equalityComparer))
            {
                var ex = new AlreadyExistsException("Such user is already exists.");
                logger?.Trace(ex);
                throw ex;
            }

            user.Id = this.idGenerator();
            this.users.Add(user);
            OnUserAdded(user);
            logger?.Info($"Add user FirstName:{user.FirstName} LastName:{user.LastName} Date of Birth:{user.DateOfBirth}");
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
                var ex = new ArgumentNullException(nameof(user));
                logger?.Trace(ex);
                throw ex;
            }

            if (!this.users.Contains(user, this.equalityComparer))
            {
                var ex = new DoesNotExistsException("Such user does not exists.");
                logger?.Trace(ex);
                throw ex;
            }

            this.users.Remove(user);
            OnUserRemoved(user);
            logger?.Info($"Remove user FirstName:{user.FirstName} LastName:{user.LastName} Date of Birth:{user.DateOfBirth}");
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

        /// <summary>
        /// Saves current state.
        /// </summary>
        /// <param name="userStorage">Instance of IUserStorage.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="userStorage"/> is null.
        /// </exception>
        public void SaveState(IUserStorage userStorage)
        {
            if (ReferenceEquals(userStorage, null))
            {
                var ex = new ArgumentNullException(nameof(userStorage));
                logger?.Trace(ex);
                throw ex;
            }

            userStorage.SaveUsers(users);
        }

        /// <summary>
        /// Loads current state.
        /// </summary>
        /// <param name="userStorage">Instance of IUserStorage.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="userStorage"/> is null.
        /// </exception>
        public void LoadState(IUserStorage userStorage)
        {
            if (ReferenceEquals(userStorage, null))
            {
                var ex = new ArgumentNullException(nameof(userStorage));
                logger?.Trace(ex);
                throw ex;
            }

            users = userStorage.LoadUsers().ToList();
        }


        private void OnUserAdded(User user)
        {
            UserAdded?.Invoke(this, new UserEventArgs(user));
        }

        private void OnUserRemoved(User user)
        {
            UserRemoved?.Invoke(this, new UserEventArgs(user));
        }
    }
}
