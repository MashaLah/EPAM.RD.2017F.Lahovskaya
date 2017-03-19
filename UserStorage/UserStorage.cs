using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorage
{
    public class UserStorage : IUserStorage
    {
        /// <summary>
        /// File to store users.
        /// </summary>
        private string fileName;

        /// <summary>
        /// XML formatter.
        /// </summary>
        private XmlSerializer formatter;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileName">Path to file for storage.</param>
        /// <exception cref="ArgumentException">
        /// Throws when <see cref="fileName"> is null or empty.
        /// </exception>
        public UserStorage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException($"{fileName} is null or empty");
            this.fileName = fileName;
            formatter = new XmlSerializer(typeof(List<User>));
        }

        /// <summary>
        /// Saves <see cref="users"> to <see cref="users">
        /// </summary>
        /// <param name="users">List of User</param>
        public void SaveUsers(IEnumerable<User> users)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, users);
            }
        }

        /// <summary>
        /// Load list of User from <see cref="fileName">.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// Throws when file doesn't exists.
        /// </exception> 
        /// <returns>List of User.</returns>
        public IEnumerable<User> LoadUsers()
        {
            List<User> users = new List<User>();
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File {nameof(fileName)} not found.");
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                users = (List<User>)formatter.Deserialize(fs);
            }
            return users;
        }
    }
}
