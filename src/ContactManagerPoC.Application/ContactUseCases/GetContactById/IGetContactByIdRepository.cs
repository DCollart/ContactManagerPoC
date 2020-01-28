using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerPoC.Application.ContactUseCases.GetContactById
{
    public interface IGetContactByIdRepository
    {
        Task<GetContactByIdResponse> GetContactByIdAsync(int id);
    }
}
