using System.Threading.Tasks;

namespace ContactManagerPoC.Application
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
