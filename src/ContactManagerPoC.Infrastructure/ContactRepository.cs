using ContactManagerPoC.Application.ContactUseCases.GetActiveContacts;
using ContactManagerPoC.Application.ContactUseCases.GetContactById;
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
    public class ContactRepository : IContactRepository, IGetActiveContactsRepository, IGetContactByIdRepository
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

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }

        async Task<GetActiveContactResponse[]> IGetActiveContactsRepository.GetAllActiveContactsAsync()
        {
            return await _context.Contacts.Where(c => !c.IsDeleted).Select(c => new GetActiveContactResponse()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName
            }).ToArrayAsync();
        }

        async Task<GetContactByIdResponse> IGetContactByIdRepository.GetContactByIdAsync(int id)
        {
            return await _context.Contacts.Select(c => new GetContactByIdResponse() 
            { 
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                IsDeleted = c.IsDeleted
            }).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
