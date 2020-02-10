using ContactManagerPoC.Domain.Core;

namespace ContactManagerPoC.Domain.Contact
{
    public class Contact
    {
        public int Id { get; private set; }
        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }
        public bool IsDeleted { get; private set; }
        public Address Address { get; private set; }

        private Contact()
        {
        }

        private Contact(Name firstName, Name lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            IsDeleted = false;
        }

        public static Contact Create(Name firstName, Name lastName, Address address)
        {
            Contract.Require(() => firstName != null && lastName != null && address != null);

            return new Contact(firstName, lastName, address);
        }

        public bool CanDelete()
        {
            return !IsDeleted;
        }

        public void Delete()
        {
            Contract.Require(() => CanDelete());
            IsDeleted = true;
        }

        public void UpdateNames(Name firstName, Name lastName)
        {
            Contract.Require(() => firstName != null && lastName != null);

            FirstName = firstName;
            LastName = lastName;
        }

        public void ChangeAddress(Address address)
        {
            Contract.Require(() => address != null);

            Address = address;
        }
    }
}
