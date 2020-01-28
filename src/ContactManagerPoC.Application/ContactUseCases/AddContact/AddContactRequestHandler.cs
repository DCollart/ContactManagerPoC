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
    public class AddContactRequestHandler : IRequestHandler<AddContactRequest, Result<string, int>>
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

        public async Task<Result<string, int>> Handle(AddContactRequest request, CancellationToken cancellationToken)
        {
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

            _contactRepository.AddContact(contactResult.Item);
            await _unitOfWork.SaveChangesAsync();

            return Result<string, int>.Success(contactResult.Item.Id);
        }
    }
}
