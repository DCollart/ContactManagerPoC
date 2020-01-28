using ContactManagerPoC.Application.ContactUseCases.AddContact;
using ContactManagerPoC.Application.ContactUseCases.GetActiveContacts;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ContactManagerPoC.WebAPI.Tests
{
    public class ContactsTests : IClassFixture<WebApiFixture>
    {
        private readonly HttpClient _client;

        public ContactsTests(WebApiFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task CanGetAllContacts()
        {
            // Act
            var response = await _client.GetAsync("/contacts");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CanCreateContact()
        {
            // Arrange 
            var content = new StringContent(JsonSerializer.Serialize(new AddContactRequest()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Street = Guid.NewGuid().ToString(),
                City = Guid.NewGuid().ToString(),
                Number = Guid.NewGuid().ToString(),
                ZipCode = Guid.NewGuid().ToString(),
                Country = Guid.NewGuid().ToString()
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/contacts", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CanGetContactById()
        {
            // Act
            var response = await _client.GetAsync("/contacts/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CantGetContactByIdWithUnexistingId()
        {
            // Act
            var response = await _client.GetAsync("/contacts/0");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
