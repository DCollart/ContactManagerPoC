using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.GetContacts
{
    public class GetContactsQuery : IRequest<GetContactResponse[]>
    {
    }
}
