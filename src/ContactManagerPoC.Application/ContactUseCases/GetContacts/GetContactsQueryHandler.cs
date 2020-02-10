using System.Threading;
using System.Threading.Tasks;
using ContactManagerPoC.Domain.Core;
using MediatR;

namespace ContactManagerPoC.Application.ContactUseCases.GetContacts
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, GetContactResponse[]>
    {
        private readonly IGetContactsRepository _contactRepository;

        public GetContactsQueryHandler(IGetContactsRepository contactRepository)
        {
            Contract.Require(() => contactRepository != null);

            _contactRepository = contactRepository;
        }

        public async Task<GetContactResponse[]> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            return await _contactRepository.GetContactsAsync();
        }
    }
}
