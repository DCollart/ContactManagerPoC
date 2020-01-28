using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactManagerPoC.Application.ContactUseCases.AddContact;
using ContactManagerPoC.Application.ContactUseCases.DeleteContactContact;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactAddress;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactNames;
using FluentAssertions;
using Xunit;

namespace ContactManagerPoC.WebAPI.Tests
{
    public class ContactLifecycleTests : IClassFixture<WebApiFixture>
    {
        private readonly HttpClient _client;

        public ContactLifecycleTests(WebApiFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task ContactLifeCycle()
        {
            var id = await AddContact();
            await GetContact(id);
            await UpdateContactNames(id);
            await UpdateContactAddress(id);
            await DeleteContact(id);
        }

        private async Task<int> AddContact()
        {
            // Arrange 
            var content = new StringContent(JsonSerializer.Serialize(new AddContactRequest()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Street =  Guid.NewGuid().ToString(),
                City = Guid.NewGuid().ToString(),
                Number = Guid.NewGuid().ToString(),
                ZipCode = Guid.NewGuid().ToString(),
                Country = Guid.NewGuid().ToString()
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/contacts", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            return int.Parse(await response.Content.ReadAsStringAsync());
        }

        private async Task GetContact(int id)
        {
            // Act
            var response = await _client.GetAsync($"/contacts/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task UpdateContactNames(int id)
        {
            // Arrange 
            var content = new StringContent(JsonSerializer.Serialize(new UpdateContactNamesRequest()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString()
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/contacts/{id}/names", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task UpdateContactAddress(int id)
        {
            // Arrange 
            var content = new StringContent(JsonSerializer.Serialize(new UpdateContactAddressRequest()
            {
                Street = Guid.NewGuid().ToString(),
                City = Guid.NewGuid().ToString(),
                Number = Guid.NewGuid().ToString(),
                ZipCode = Guid.NewGuid().ToString(),
                Country = Guid.NewGuid().ToString()
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/contacts/{id}/address", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task DeleteContact(int id)
        {
            // Act
            var response = await _client.DeleteAsync($"/contacts/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
