using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUseCases.GetContactById
{
    public class GetContactByIdRequestHandler : IRequestHandler<GetContactByIdRequest, GetContactByIdResponse>
    {
        private readonly IContactRepository _contactRepository;

        public GetContactByIdRequestHandler(IContactRepository contactRepository)
        {
            Contract.Require(() => contactRepository != null);

            _contactRepository = contactRepository;
        }

        public async Task<GetContactByIdResponse> Handle(GetContactByIdRequest request, CancellationToken cancellationToken)
        {
            return await _contactRepository.GetContactByIdAsync(request.Id);
        }
    }
}
