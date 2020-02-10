using System.Linq;
using ApplicationLayer = ContactManagerPoC.Application.ContactUseCases.GetContacts;

namespace ContactManagerPoC.WebAPI.ContactUseCases.GetContacts
{
    public static class GetContactResponseMapper
    {
        public static GetContactResponse ToWebResponse(this ApplicationLayer.GetContactResponse response)
        {
            return new GetContactResponse()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName
            };
        }

        public static GetContactResponse[] ToWebResponse(this ApplicationLayer.GetContactResponse[] response)
        {
            return response.Select(c => c.ToWebResponse()).ToArray();
        }
    }
}
