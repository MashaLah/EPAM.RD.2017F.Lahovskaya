using System;

namespace ServiceLibrary
{
    [Serializable]
    public class User : IEquatable<User> 
    {
        /// <summary>
        /// Gets and sets user id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets and sets first name of user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets and sets last name of user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets and sets date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Overrides GetHashCode() method.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return (this.FirstName.ToUpper().GetHashCode() * 31 +
            this.LastName.ToUpper().GetHashCode() * 31 +
            this.DateOfBirth.GetHashCode() * 31) ^ Id;
        }
           
        /// <summary>
        /// Overrides object.equals().
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>True if equal, false if not equal.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            } 

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((User)obj);
        }

        /// <summary>
        /// Check if two users are equal.
        /// </summary>
        /// <param name="other"> Instance of User class.</param>
        /// <returns> False if not equal, true if equal.</returns>
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(this.LastName, other.LastName, StringComparison.OrdinalIgnoreCase) &&
                this.DateOfBirth.Equals(other.DateOfBirth);
        }
    }
}
