using ContactManagerPoC.Domain.Core;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Result>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            Contract.Require(() => contactRepository != null);
            Contract.Require(() => unitOfWork != null);

            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetContactByIdAsync(request.Id);

            if (contact == null)
            {
                return Result.Fail(Error.Create(ErrorMessages.AggregateNotFound, errorType: ErrorType.AggregateNotFound));
            }
             
            if (!contact.CanDelete())
            {
                return Result.Fail(Error.Create(ErrorMessages.InvalidOperation));
            }

            contact.Delete();
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
