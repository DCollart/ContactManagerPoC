using ContactManagerPoC.Application.ContactUseCases.UpdateContactNames;

namespace ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactNames
{
    public static class UpdateContactNamesRequestMapper
    {
        public static UpdateContactNamesCommand MapToCommand(this UpdateContactNamesRequest request, int id)
        {
            return new UpdateContactNamesCommand()
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
        }
    }
}
