using ApplicationLayer = ContactManagerPoC.Application.ContactUseCases.GetContactById;

namespace ContactManagerPoC.WebAPI.ContactUseCases.GetContactById
{
    public static class GetContactByIdResponseMapper
    {
        public static GetContactByIdResponse ToWebResponse(this ApplicationLayer.GetContactByIdResponse response)
        {
            return new GetContactByIdResponse()
            {
                FirstName = response.FirstName,
                LastName = response.LastName,
                Number = response.Number,
                Street = response.Street,
                City = response.City,
                ZipCode = response.ZipCode,
                Country = response.Country,
                Id = response.Id,
                IsDeleted = response.IsDeleted
            };
        }
    }
}
