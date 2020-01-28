using ContactManagerPoC.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManagerPoC.Domain.Contact
{
    public class Contact : IDeleted
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

        private static bool IsValidName(string name) => !string.IsNullOrEmpty(name);

        public static Result<string, Contact> Create(string firstName, string lastName)
        {
            List<string> errors = new List<string>();

            if (!IsValidName(firstName)) errors.Add("Firstname should not be null or empty");
            if (!IsValidName(lastName)) errors.Add("Lastname should not be null or empty");

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

        public Result<string> CanUpdate(string firstName, string lastName)
        {
            var result = Contact.Create(firstName, lastName);
            return result.IsSuccess ? Result<string>.Success() : Result<string>.Fail(result.Errors);
        }

        public void Update(string firstName, string lastName)
        {
            Contract.Require(() => CanUpdate(firstName, lastName).IsSuccess);

            FirstName = firstName;
            LastName = lastName;
        }
    }
}
