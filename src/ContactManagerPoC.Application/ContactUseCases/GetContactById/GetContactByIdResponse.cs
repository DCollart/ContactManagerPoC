using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagerPoC.Application.ContactUseCases.GetContactById
{
    public class GetContactByIdResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
