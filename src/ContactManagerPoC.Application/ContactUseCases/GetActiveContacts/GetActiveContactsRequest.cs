using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagerPoC.Application.ContactUseCases.GetActiveContacts
{
    public class GetActiveContactsRequest : IRequest<GetActiveContactResponse[]>
    {
    }
}
