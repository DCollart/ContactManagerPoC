using ContactManagerPoC.Application.ContactUseCases.AddContact;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactNames;

namespace ContactManagerPoC.WebAPI.Validators
{
    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactNamesRequest>
    {
        public UpdateContactRequestValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
        }
    }
}
