using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagerPoC.Application.ContactUseCases.GetContactById
{
    public class GetContactByIdQuery : IRequest<GetContactByIdResponse>
    {
        public int Id { get; set; }
    }
}
