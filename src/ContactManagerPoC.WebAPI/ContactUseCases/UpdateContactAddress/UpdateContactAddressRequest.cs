﻿using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactAddress
{
    public class UpdateContactAddressRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
