using FluentValidation;

namespace ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactNames
{
    public class UpdateContactNamesRequestValidator : AbstractValidator<UpdateContactNamesRequest>
    {
        public UpdateContactNamesRequestValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
        }
    }
}
