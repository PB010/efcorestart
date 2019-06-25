namespace EFCore.Domain
{
    public class PersonFullName
    {
        protected bool Equals(PersonFullName other)
        {
            return string.Equals(Surname, other.Surname) && string.Equals(GivenName, other.GivenName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PersonFullName) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Surname != null ? Surname.GetHashCode() : 0) * 397) ^ (GivenName != null ? GivenName.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PersonFullName left, PersonFullName right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonFullName left, PersonFullName right)
        {
            return !Equals(left, right);
        }

        public string Surname { get; private set; }
        public string GivenName { get; private set; }
        public string FullName => $"{GivenName} {Surname}";
        public string FullNameReverse => $"{Surname}, {GivenName}";

        private PersonFullName() {}

        public PersonFullName(string givenName, string surname)
        {
            Surname = surname;
            GivenName = givenName;
        }
    }
}
