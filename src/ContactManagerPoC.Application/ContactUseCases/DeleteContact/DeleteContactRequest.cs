using System;
using System.Collections.Generic;
using System.Text;
using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.DeleteContactContact
{
    public class DeleteContactRequest : IRequest<Result>
    {
        public int Id { get; set; }

    }
}
