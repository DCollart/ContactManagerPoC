using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using ContactManagerPoC.Domain.Core;

namespace ContactManagerPoC.Domain
{
    public class Name : ValueObject
    {
        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }

        public static Result<string, Name> Create(string name)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(name)) errors.Add("Firstname should not be null or empty");

            if (errors.Any()) return Result<string, Name>.Fail(errors);

            return Result<string, Name>.Success(new Name(name));
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
