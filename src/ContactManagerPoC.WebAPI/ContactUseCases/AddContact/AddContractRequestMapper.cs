using ContactManagerPoC.Application.ContactUseCases.AddContact;

namespace ContactManagerPoC.WebAPI.ContactUseCases.AddContact
{
    public static class AddContractRequestMapper
    {
        public static AddContactCommand MapToCommand(this AddContactRequest request)
        {
            return new AddContactCommand()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Number = request.Number,
                Street = request.Street,
                City = request.City,
                ZipCode = request.ZipCode,
                Country = request.Country
            };
        }
    }
}
