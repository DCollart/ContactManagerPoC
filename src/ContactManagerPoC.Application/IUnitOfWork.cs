using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
