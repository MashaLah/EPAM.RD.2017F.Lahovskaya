using System;

namespace ServiceLibrary
{
    [Serializable]
    public class User : IEquatable<User> 
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }


        public override int GetHashCode()
        {
            return (FirstName.ToUpper().GetHashCode() * 31 +
            LastName.ToUpper().GetHashCode() * 31 +
            DateOfBirth.GetHashCode() * 31) ^ Id;
        }
           

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

            return string.Equals(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(LastName, other.LastName, StringComparison.OrdinalIgnoreCase) &&
                DateOfBirth.Equals(other.DateOfBirth);
        }

    }
}
