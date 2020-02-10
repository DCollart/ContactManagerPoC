using System;
using System.Linq;
using ApplicationLayer = ContactManagerPoC.Application.ContactUseCases.GetContacts;

namespace ContactManagerPoC.WebAPI.ContactUseCases.GetContacts
{
    public static class GetContactResponseMapper
    {
        public static GetContactResponse ToWebResponse(this ApplicationLayer.GetContactResponse response, Func<int, string> getContactByIdUrl)
        {
            return new GetContactResponse()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                Url = getContactByIdUrl(response.Id)
            };
        }

        public static GetContactResponse[] ToWebResponse(this ApplicationLayer.GetContactResponse[] response, Func<int, string> getContactByIdUrl)
        {
            return response.Select(c => c.ToWebResponse(getContactByIdUrl)).ToArray();
        }
    }
}
