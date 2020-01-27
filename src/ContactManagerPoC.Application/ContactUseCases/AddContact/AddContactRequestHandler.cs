using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Contact;
using ContactManagerPoC.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            var result = Contact.Create(request.FirstName, request.LastName);

            if (result.IsFailure)
            {
                return Result<string, int>.Fail(result.Errors);
            }

            _contactRepository.AddContact(result.Item);
            await _unitOfWork.SaveChangesAsync();

            return Result<string, int>.Success(result.Item.Id);
        }
    }
}
