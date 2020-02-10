using ContactManagerPoC.Application.ContactUseCases.GetContactById;
using ContactManagerPoC.Domain.Contact;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases;
using ContactManagerPoC.Application.ContactUseCases.GetContacts;

namespace ContactManagerPoC.Infrastructure
{
    public class ContactRepository : IContactRepository, IGetContactsRepository, IGetContactByIdRepository
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

        async Task<GetContactResponse[]> IGetContactsRepository.GetAllActiveContactsAsync()
        {
            return await _context.Contacts.Where(c => !c.IsDeleted).Select(c => new GetContactResponse()
            {
                Id = c.Id,
                FirstName = c.FirstName.Value,
                LastName = c.LastName.Value
            }).ToArrayAsync();
        }

        async Task<GetContactByIdResponse> IGetContactByIdRepository.GetContactByIdAsync(int id)
        {
            return await _context.Contacts.Select(c => new GetContactByIdResponse() 
            { 
                Id = c.Id,
                FirstName = c.FirstName.Value,
                LastName = c.LastName.Value,
                Street = c.Address.Street,
                Number =  c.Address.Number,
                City = c.Address.City,
                ZipCode = c.Address.ZipCode,
                Country = c.Address.Country,
                IsDeleted = c.IsDeleted
            }).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
