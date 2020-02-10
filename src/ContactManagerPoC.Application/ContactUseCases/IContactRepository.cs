using ContactManagerPoC.Domain.Contact;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUseCases
{
    public interface IContactRepository
    {
        void AddContact(Contact contact);
        Task<Contact> GetContactByIdAsync(int id);
    }
}
