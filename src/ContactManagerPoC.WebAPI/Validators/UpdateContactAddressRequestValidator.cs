using ContactManagerPoC.Application.ContactUseCases.AddContact;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactAddress;

namespace ContactManagerPoC.WebAPI.Validators
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
