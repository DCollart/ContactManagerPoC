using System;
using System.Collections.Generic;
using System.Text;
using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.UpdateContact
{
    public class UpdateContactRequest : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
