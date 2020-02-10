using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUseCases.GetContacts
{
    public interface IGetContactsRepository
    {
        Task<GetContactResponse[]> GetContactsAsync();
    }
}
