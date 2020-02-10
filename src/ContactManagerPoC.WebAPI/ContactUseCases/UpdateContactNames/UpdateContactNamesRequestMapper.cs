using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.AddContact;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactNames;
using ContactManagerPoC.WebAPI.ContactUseCases.AddContact;

namespace ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactNames
{
    public static class UpdateContactNamesRequestMapper
    {
        public static UpdateContactNamesCommand MapToCommand(this UpdateContactNamesRequest request, int id)
        {
            return new UpdateContactNamesCommand()
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
        }
    }
}
