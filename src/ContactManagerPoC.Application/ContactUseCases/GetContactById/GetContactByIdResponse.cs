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
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool IsDeleted { get; set; }
    }
}
