using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.WebAPI.ContactUseCases.AddContact
{
    public class AddContactRequest : IRequest<Result<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
