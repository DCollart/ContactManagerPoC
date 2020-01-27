using ContactManagerPoC.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUseCases.AddContact
{
    public class AddContactRequest : IRequest<Result<string, int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
