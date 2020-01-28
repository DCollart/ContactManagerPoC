using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.AddContact;
using ContactManagerPoC.Application.ContactUseCases.DeleteContactContact;
using ContactManagerPoC.Application.ContactUseCases.GetActiveContacts;
using ContactManagerPoC.Application.ContactUseCases.GetContactById;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactAddress;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactNames;
using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Domain.Core;
using ContactManagerPoC.WebAPI.Validators;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerPoC.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            Contract.Require(() => mediator != null);

            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await _mediator.Send(new GetActiveContactsRequest()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var response = await _mediator.Send(new GetContactByIdRequest() { Id = id });
            if (response == null) 
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("{id}/names")]
        public async Task<IActionResult> UpdateContactNames(int id, UpdateContactNamesRequest updateContactRequest)
        {
            updateContactRequest.Id = id;
            var result = await _mediator.Send(updateContactRequest);
            
            if (result.IsFailure)
            {
                result.Errors.ForEach(e => ModelState.AddModelError(string.Empty, e));
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut("{id}/address")]
        public async Task<IActionResult> ChangeContactAddress(int id, UpdateContactAddressRequest updateContactAddressRequest)
        {
            updateContactAddressRequest.Id = id;
            var result = await _mediator.Send(updateContactAddressRequest);

            if (result.IsFailure)
            {
                result.Errors.ForEach(e => ModelState.AddModelError(string.Empty, e));
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var result = await _mediator.Send(addContactRequest);

            if (result.IsFailure)
            {
                result.Errors.ForEach(e => ModelState.AddModelError(string.Empty, e));
                return BadRequest(ModelState);
            }

            return Created("temp", result.Item);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _mediator.Send(new DeleteContactRequest() { Id = id });

            if (result.IsFailure)
            {
                result.Errors.ForEach(e => ModelState.AddModelError(string.Empty, e));
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}