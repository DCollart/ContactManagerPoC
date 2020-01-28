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

        public static Result<string, Address> Create(string street, string number, string city, string zipCode, string country)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(street)) errors.Add("Street should not be null or empty");
            if (string.IsNullOrEmpty(number)) errors.Add("Number should not be null or empty");
            if (string.IsNullOrEmpty(city)) errors.Add("City should not be null or empty");
            if (string.IsNullOrEmpty(zipCode)) errors.Add("ZipCode should not be null or empty");
            if (string.IsNullOrEmpty(country)) errors.Add("Country should not be null or empty");

            if (errors.Any()) return Result<string, Address>.Fail(errors);

            return Result<string, Address>.Success(new Address(street, number, city, zipCode, country));
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
