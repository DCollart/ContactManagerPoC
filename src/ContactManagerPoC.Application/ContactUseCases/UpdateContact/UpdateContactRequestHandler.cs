using ContactManagerPoC.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain;
using ContactManagerPoC.Domain.Contact;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.UpdateContact
{
    public class UpdateContactRequestHandler : IRequestHandler<UpdateContactRequest, Result<string>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateContactRequestHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            Contract.Require(() => contactRepository != null);
            Contract.Require(() => unitOfWork != null);

            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateContactRequest request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetContactByIdAsync(request.Id);
            if (contact == null)
            {
                return Result<string>.Fail("The contact does not exist");
            }

            var firstNameResult = Name.Create(request.FirstName);
            var lastNameResult = Name.Create(request.LastName);

            if (firstNameResult.IsFailure || lastNameResult.IsFailure)
            {
                var errors = new List<string>();
                errors.AddRange(firstNameResult.Errors);
                errors.AddRange(lastNameResult.Errors);

                return Result<string, int>.Fail(errors);
            }

            var contactResult = Contact.Create(firstNameResult.Item, lastNameResult.Item);

            if (contactResult.IsFailure)
            {
                return Result<string, int>.Fail(contactResult.Errors);
            }

            contact.UpdateNames(firstNameResult.Item, lastNameResult.Item);
            await _unitOfWork.SaveChangesAsync();

            return Result<string, int>.Success(contactResult.Item.Id);
        }
    }
}
