using ContactManagerPoC.Application.ContactUseCases.AddContact;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactNames;

namespace ContactManagerPoC.WebAPI.Validators
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
