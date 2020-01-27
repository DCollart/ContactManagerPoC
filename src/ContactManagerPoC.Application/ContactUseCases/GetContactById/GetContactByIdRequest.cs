using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagerPoC.Application.ContactUseCases.GetContactById
{
    public class GetContactByIdRequest : IRequest<GetContactByIdResponse>
    {
        public int Id { get; set; }
    }
}
