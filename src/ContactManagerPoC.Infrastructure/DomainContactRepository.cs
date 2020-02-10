using System;
using ContactManagerPoC.Application.ContactUseCases.GetContactById;
using ContactManagerPoC.Domain.Contact;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases;
using ContactManagerPoC.Application.ContactUseCases.GetContacts;
using ContactManagerPoC.Domain.Core;

namespace ContactManagerPoC.Infrastructure
{
    public class DomainContactRepository : IContactRepository
    {
        private readonly ContactContext _context;

        public DomainContactRepository(ContactContext context)
        {
            Contract.Require(() => context != null);

            _context = context;
        }

        public void AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
