using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain;
using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.UpdateContactNames
{
    public class UpdateContactNamesRequestHandler : IRequestHandler<UpdateContactNamesRequest, Result>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateContactNamesRequestHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            Contract.Require(() => contactRepository != null);
            Contract.Require(() => unitOfWork != null);

            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateContactNamesRequest request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetContactByIdAsync(request.Id);
            if (contact == null)
            {
                return Result.Fail(Error.Create(ErrorMessages.AggregateNotFound, errorType: ErrorType.AggregateNotFound));
            }

            var firstNameResult = Name.Create(request.FirstName);
            var lastNameResult = Name.Create(request.LastName);

            if (firstNameResult.IsFailure || lastNameResult.IsFailure)
            {
                var errors = new List<Error>();
                errors.AddRange(firstNameResult.Errors);
                errors.AddRange(lastNameResult.Errors);

                return Result.Fail(errors);
            }

            contact.UpdateNames(firstNameResult.Item, lastNameResult.Item);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
