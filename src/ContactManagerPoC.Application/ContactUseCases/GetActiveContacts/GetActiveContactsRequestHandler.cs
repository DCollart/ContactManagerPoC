using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUseCases.GetActiveContacts
{
    public class GetActiveContactsRequestHandler : IRequestHandler<GetActiveContactsRequest, GetActiveContactResponse[]>
    {
        private readonly IContactRepository _contactRepository;

        public GetActiveContactsRequestHandler(IContactRepository contactRepository)
        {
            Contract.Require(() => contactRepository != null);

            _contactRepository = contactRepository;
        }

        public async Task<GetActiveContactResponse[]> Handle(GetActiveContactsRequest request, CancellationToken cancellationToken)
        {
            return await _contactRepository.GetAllActiveContactsAsync();
        }
    }
}
