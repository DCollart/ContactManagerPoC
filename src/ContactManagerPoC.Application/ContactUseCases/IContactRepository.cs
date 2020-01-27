using ContactManagerPoC.Application.ContactUseCases.GetActiveContacts;
using ContactManagerPoC.Domain.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUsesCases
{
    public interface IContactRepository
    {
        void AddContact(Contact contact);
        Task<ActiveContactResponse[]> GetAllActiveContactsAsync();
    }
}
