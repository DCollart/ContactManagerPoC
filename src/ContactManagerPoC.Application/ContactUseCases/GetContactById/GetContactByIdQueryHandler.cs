using ContactManagerPoC.Domain.Core;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUseCases.GetContactById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, GetContactByIdResponse>
    {
        private readonly IGetContactByIdRepository _contactRepository;

        public GetContactByIdQueryHandler(IGetContactByIdRepository contactRepository)
        {
            Contract.Require(() => contactRepository != null);

            _contactRepository = contactRepository;
        }

        public async Task<GetContactByIdResponse> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            return await _contactRepository.GetContactByIdAsync(request.Id);
        }
    }
}
