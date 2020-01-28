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
        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }
        public bool IsDeleted { get; private set; }

        private Contact()
        {
        }

        private Contact(Name firstName, Name lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            IsDeleted = false;
        }

        public static Result<string, Contact> Create(Name firstName, Name lastName)
        {
            List<string> errors = new List<string>();

            if (firstName == null) errors.Add("Firstname should not be null");
            if (lastName == null) errors.Add("Lastname should not be null");

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

        public Result<string> CanUpdateNames(Name firstName, Name lastName)
        {
            var result = Contact.Create(firstName, lastName);
            return result.IsSuccess ? Result<string>.Success() : Result<string>.Fail(result.Errors);
        }

        public void UpdateNames(Name firstName, Name lastName)
        {
            Contract.Require(() => CanUpdateNames(firstName, lastName).IsSuccess);

            FirstName = firstName;
            LastName = lastName;
        }
    }
}
