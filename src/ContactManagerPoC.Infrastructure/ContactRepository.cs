using ContactManagerPoC.Application.ContactUseCases.GetActiveContacts;
using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Contact;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerPoC.Infrastructure
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _context;

        public ContactRepository(ContactContext context)
        {
            _context = context;
        }

        public void AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
        }

        public async Task<ActiveContactResponse[]> GetAllActiveContactsAsync()
        {
            return await _context.Contacts.Where(c => !c.IsDeleted).Select(c => new ActiveContactResponse()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName
            }).ToArrayAsync();
        }
    }
}
