using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManagerPoC.Infrastructure
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _context;

        public ContactRepository(ContactContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Contact> GetAllActiveContacts()
        {
            return _context.Contacts.Where(c => !c.IsDeleted).ToList().AsReadOnly();
        }
    }
}
