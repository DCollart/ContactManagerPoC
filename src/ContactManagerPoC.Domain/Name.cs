using System.Collections.Generic;
using System.Linq;
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

        public static Result<Name> Create(string name)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrEmpty(name)) errors.Add(Error.Create(ErrorMessages.ShouldNotBeNullOrEmpty, nameof(name)));

            if (errors.Any()) return Result<Name>.Fail(errors);

            return Result<Name>.Success(new Name(name));
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
