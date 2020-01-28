using ContactManagerPoC.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.DeleteContactContact;
using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Contact;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.DeleteContact
{
    public class DeleteContactRequestHandler : IRequestHandler<DeleteContactRequest, Result<string>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactRequestHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            Contract.Require(() => contactRepository != null);
            Contract.Require(() => unitOfWork != null);

            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteContactRequest request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetContactByIdAsync(request.Id);

            if (contact == null)
            {
                return Result<string>.Fail("The contact does not exist");
            }

            if (!contact.CanDelete())
            {
                return Result<string>.Fail("The contact cannot be deleted");
            }

            contact.Delete();
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success();
        }
    }
}
