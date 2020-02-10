using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContactManagerPoC.Domain;
using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.UpdateContactAddress
{
    public class UpdateContactAddressCommandHandler : IRequestHandler<UpdateContactAddressCommand, Result>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateContactAddressCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            Contract.Require(() => contactRepository != null);
            Contract.Require(() => unitOfWork != null);

            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateContactAddressCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetContactByIdAsync(request.Id);
            if (contact == null)
            {
                return Result.Fail(Error.Create(ErrorMessages.AggregateNotFound, errorType: ErrorType.AggregateNotFound));
            }

            var addressResult = Address.Create(request.Street, request.Number, request.City, request.ZipCode, request.Country);

            if (addressResult.IsFailure)
            {
                var errors = new List<Error>();
                errors.AddRange(addressResult.Errors);

                return Result.Fail(errors);
            }

            contact.ChangeAddress(addressResult.Item);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
