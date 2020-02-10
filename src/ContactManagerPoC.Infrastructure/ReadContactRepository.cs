using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.GetContactById;
using ContactManagerPoC.Application.ContactUseCases.GetContacts;
using ContactManagerPoC.Domain.Contact;
using ContactManagerPoC.Domain.Core;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerPoC.Infrastructure
{
    public class ReadContactRepository : IGetContactByIdRepository, IGetContactsRepository
    {
        private readonly IDbConnection _dbConnection;

        public ReadContactRepository(IDbConnection dbConnection)
        {
            Contract.Require(() => dbConnection != null);

            _dbConnection = dbConnection;
        }

        public async Task<GetContactByIdResponse> GetContactByIdAsync(int id)
        {
            return (await _dbConnection.QueryAsync<GetContactByIdResponse>("GetContactById", new {Id = id}, commandType: CommandType.StoredProcedure))
                .FirstOrDefault();
        }

        public async Task<GetContactResponse[]> GetContactsAsync()
        {
            return (await _dbConnection.QueryAsync<GetContactResponse>("GetContacts", commandType: CommandType.StoredProcedure)).ToArray();
        }
    }
}