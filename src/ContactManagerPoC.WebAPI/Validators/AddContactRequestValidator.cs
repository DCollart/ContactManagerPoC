using ContactManagerPoC.Application.ContactUseCases.AddContact;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerPoC.WebAPI.Validators
{
    public class AddContactRequestValidator : AbstractValidator<AddContactRequest>
    {
        public AddContactRequestValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
        }
    }
}
