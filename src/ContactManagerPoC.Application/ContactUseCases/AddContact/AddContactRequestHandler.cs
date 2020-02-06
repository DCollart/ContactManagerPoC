using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Contact;
using ContactManagerPoC.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ContactManagerPoC.Domain;

namespace ContactManagerPoC.Application.ContactUseCases.AddContact
{
    public class AddContactRequestHandler : IRequestHandler<AddContactRequest, Result<int>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddContactRequestHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            Contract.Require(() => contactRepository != null);
            Contract.Require(() => unitOfWork != null);

            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(AddContactRequest request, CancellationToken cancellationToken)
        {
            var firstNameResult = Name.Create(request.FirstName);
            var lastNameResult = Name.Create(request.LastName);
            var addressResult = Address.Create(request.Street, request.Number, request.City, request.ZipCode, request.Country);

            if (firstNameResult.IsFailure || lastNameResult.IsFailure || addressResult.IsFailure)
            {
                var errors = new List<Error>();
                errors.AddRange(firstNameResult.Errors);
                errors.AddRange(lastNameResult.Errors);
                errors.AddRange(addressResult.Errors);


                return Result<int>.Fail(errors);
            }

            var contact = Contact.Create(firstNameResult.Item, lastNameResult.Item, addressResult.Item);
            _contactRepository.AddContact(contact);
            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(contact.Id);
        }
    }
}
