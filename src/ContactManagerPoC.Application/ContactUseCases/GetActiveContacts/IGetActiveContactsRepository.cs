using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUseCases.GetActiveContacts
{
    public interface IGetActiveContactsRepository
    {
        Task<GetActiveContactResponse[]> GetAllActiveContactsAsync();
    }
}
