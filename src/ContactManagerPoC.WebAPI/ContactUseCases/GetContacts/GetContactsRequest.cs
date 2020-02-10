using MediatR;

namespace ContactManagerPoC.WebAPI.ContactUseCases.GetContacts
{
    public class GetContactsRequest : IRequest<GetContactResponse[]>
    {
    }
}
