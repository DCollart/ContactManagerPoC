using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactNames
{
    public class UpdateContactNamesRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
