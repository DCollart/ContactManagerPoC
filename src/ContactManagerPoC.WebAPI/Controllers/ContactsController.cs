using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerPoC.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            Contract.Require(() => contactRepository != null);

            _contactRepository = contactRepository;
        }

        [HttpGet]
        public IActionResult GetAllContacts()
        {
            return Ok(_contactRepository.GetAllActiveContacts());
        }

    }
}