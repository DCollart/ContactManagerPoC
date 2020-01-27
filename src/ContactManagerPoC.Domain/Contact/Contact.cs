using ContactManagerPoC.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManagerPoC.Domain.Contact
{
    public class Contact : Core.IDeleted
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; private set; }

        private Contact(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            IsDeleted = false;
        }

        public static Result<string, Contact> Create(string firstName, string lastName)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(firstName)) errors.Add("Firstname should not be null or empty");
            if (string.IsNullOrEmpty(lastName)) errors.Add("Lastname should not be null or empty");

            if (errors.Any()) return Result<string, Contact>.Fail(errors);

            return Result<string, Contact>.Success(new Contact(firstName, lastName));
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
    }
}
