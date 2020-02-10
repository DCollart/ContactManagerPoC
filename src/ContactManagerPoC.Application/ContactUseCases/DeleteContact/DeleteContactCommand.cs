using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.DeleteContact
{
    public class DeleteContactCommand : IRequest<Result>
    {
        public int Id { get; set; }

    }
}
