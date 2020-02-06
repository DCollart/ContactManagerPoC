using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManagerPoC.Domain.Core;

namespace ContactManagerPoC.Domain
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string Number { get; }
        public string City { get; }
        public string ZipCode { get; }
        public string Country { get; }

        private Address(string street, string number, string city, string zipCode, string country)
        {
            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }

        public static Result<Address> Create(string street, string number, string city, string zipCode, string country)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrEmpty(street)) errors.Add(Error.Create(ErrorMessages.ShouldNotBeNullOrEmpty, nameof(street)));
            if (string.IsNullOrEmpty(number)) errors.Add(Error.Create(ErrorMessages.ShouldNotBeNullOrEmpty, nameof(number)));
            if (string.IsNullOrEmpty(city)) errors.Add(Error.Create(ErrorMessages.ShouldNotBeNullOrEmpty, nameof(city)));
            if (string.IsNullOrEmpty(zipCode)) errors.Add(Error.Create(ErrorMessages.ShouldNotBeNullOrEmpty, nameof(zipCode)));
            if (string.IsNullOrEmpty(country)) errors.Add(Error.Create(ErrorMessages.ShouldNotBeNullOrEmpty, nameof(country)));

            if (errors.Any()) return Result<Address>.Fail(errors);

            return Result<Address>.Success(new Address(street, number, city, zipCode, country));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return City;
            yield return ZipCode;
            yield return Country;
        }
    }
}
