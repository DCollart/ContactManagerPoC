using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactAddress;

namespace ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactAddress
{
    public static class UpdateContactAddressRequestMapper
    {
        public static UpdateContactAddressCommand MapToCommand(this UpdateContactAddressRequest request, int id)
        {
            return new UpdateContactAddressCommand()
            {
                Id = id,
                Number = request.Number,
                Street = request.Street,
                City = request.City,
                ZipCode = request.ZipCode,
                Country = request.Country
            };
        }
    }
}
