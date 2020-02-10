using FluentValidation;

namespace ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactAddress
{
    public class UpdateContactAddressRequestValidator : AbstractValidator<UpdateContactAddressRequest>
    {
        public UpdateContactAddressRequestValidator()
        {
            RuleFor(c => c.Street).NotEmpty();
            RuleFor(c => c.Number).NotEmpty();
            RuleFor(c => c.Country).NotEmpty();
            RuleFor(c => c.ZipCode).NotEmpty();
            RuleFor(c => c.City).NotEmpty();
        }
    }
}
