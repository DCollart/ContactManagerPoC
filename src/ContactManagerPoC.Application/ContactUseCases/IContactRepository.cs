using ContactManagerPoC.Domain.Contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagerPoC.Application.ContactUsesCases
{
    public interface IContactRepository
    {
        IReadOnlyList<Contact> GetAllActiveContacts();
    }
}
