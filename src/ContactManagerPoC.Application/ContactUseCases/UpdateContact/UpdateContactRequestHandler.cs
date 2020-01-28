using ContactManagerPoC.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUsesCases;
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

            var result = contact.CanUpdate(request.FirstName, request.LastName);

            if (result.IsFailure)
            {
                return Result<string>.Fail(result.Errors);
            }

            contact.Update(request.FirstName, request.LastName);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success();
        }
    }
}
