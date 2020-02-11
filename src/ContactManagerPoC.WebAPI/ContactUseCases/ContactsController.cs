using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.DeleteContact;
using ContactManagerPoC.Application.ContactUseCases.GetContactById;
using ContactManagerPoC.Application.ContactUseCases.GetContacts;
using ContactManagerPoC.Domain.Core;
using ContactManagerPoC.WebAPI.ContactUseCases.AddContact;
using ContactManagerPoC.WebAPI.ContactUseCases.GetContactById;
using ContactManagerPoC.WebAPI.ContactUseCases.GetContacts;
using ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactAddress;
using ContactManagerPoC.WebAPI.ContactUseCases.UpdateContactNames;
using ContactManagerPoC.WebAPI.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GetContactByIdResponse = ContactManagerPoC.WebAPI.ContactUseCases.GetContactById.GetContactByIdResponse;
using GetContactResponse = ContactManagerPoC.WebAPI.ContactUseCases.GetContacts.GetContactResponse;

namespace ContactManagerPoC.WebAPI.ContactUseCases
{
    [Route("contacts")]
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
        [ProducesResponseType(typeof(GetContactResponse[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllContacts()
        {
            var response = await _mediator.Send(new GetContactsQuery());
            return Ok(response.ToWebResponse(id => Url.Action(nameof(GetContactById), new { Id = id })));
        }

        [HttpGet("{id}", Name = nameof(GetContactById))]
        [ProducesResponseType(typeof(GetContactByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetContactById(int id)
        {
            var response = await _mediator.Send(new GetContactByIdQuery() { Id = id });
            if (response == null) 
            {
                return NotFound();
            }
            return Ok(response.ToWebResponse());
        }

        [HttpPut("{id}/names")]
        public async Task<IActionResult> UpdateContactNames(int id, UpdateContactNamesRequest updateContactRequest)
        {
            var result = await _mediator.Send(updateContactRequest.MapToCommand(id));

            var potentialBadResult = GeneratePotentialBadResult(result);

            return potentialBadResult ?? Ok();
        }

        [HttpPut("{id}/address")]
        public async Task<IActionResult> ChangeContactAddress(int id, UpdateContactAddressRequest updateContactAddressRequest)
        {
            var result = await _mediator.Send(updateContactAddressRequest.MapToCommand(id));

            var potentialBadResult = GeneratePotentialBadResult(result);

            return potentialBadResult ?? Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var result = await _mediator.Send(addContactRequest.MapToCommand());

            var potentialBadResult = GeneratePotentialBadResult(result);

            return potentialBadResult ?? Created(Url.Action(nameof(GetContactById), new { id = result.Item}), result.Item);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _mediator.Send(new DeleteContactCommand() { Id = id });

            var potentialBadResult = GeneratePotentialBadResult(result);

            return potentialBadResult ?? Ok();
        }

        private ActionResult GeneratePotentialBadResult(Result result)
        {
            if (result.IsFailure)
            {
                ModelState.UpdateFromResult(result);
            }

            if (result.Errors.Any(e => e.ErrorType == ErrorType.AggregateNotFound))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return null;
        }
    }
}