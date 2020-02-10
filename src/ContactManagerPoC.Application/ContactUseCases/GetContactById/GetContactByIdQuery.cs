using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.GetContactById
{
    public class GetContactByIdQuery : IRequest<GetContactByIdResponse>
    {
        public int Id { get; set; }
    }
}
