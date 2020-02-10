using System;
using System.Net;
using System.Net.Http;
using System.Text;
using ContactManagerPoC.Application.ContactUseCases.AddContact;
using ContactManagerPoC.Application.ContactUseCases.GetContactById;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactAddress;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactNames;
using FluentAssertions;
using TechTalk.SpecFlow;
using Utf8Json;
using Utf8Json.Resolvers;

namespace ContactManagerPoC.WebAPI.Tests.Scenarii
{
    [Binding]
    public class ContactManagementSteps
    {
        private readonly HttpClient _client;
        private string _id;

        public ContactManagementSteps(WebApiFixture fixture)
        {

            _client = fixture.CreateDefaultClient();
        }

        [Given(@"I have created a contact named (.*) (.*)")]
        public void GivenIHaveCreateAContactNamed(string firstName, string lastName)
        {
            CreateContact(firstName, lastName, "MyNumber", "MyStreet", "MyCity", "MyZipCode", "MyCountry");
        }

        [When(@"I change that name for (.*) (.*)")]
        public void WhenIChangeItFor(string firstName, string lastName)
        {
            var request = new UpdateContactNamesCommand()
            {
                FirstName = firstName,
                LastName = lastName
            };

            var httpContent = new StringContent(JsonSerializer.ToJsonString(request), Encoding.UTF8, "application/json");

            var response = _client.PutAsync($"contacts/{_id}/names", httpContent).GetAwaiter().GetResult();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the new name should be (.*) (.*)")]
        public void ThenTheNewNameShouldBe(string firstName, string lastName)
        {
            var response = _client.GetAsync($"contacts/{_id}").GetAwaiter().GetResult();


            var rawContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var content = JsonSerializer.Deserialize<GetContactByIdResponse>(rawContent, StandardResolver.CamelCase);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.FirstName.Should().Be(firstName);
            content.LastName.Should().Be(lastName);
        }

        [Given(@"I have created a contact living in (.*) - (.*) - (.*) - (.*) - (.*)")]
        public void GivenIHaveCreateAContactLivingIn(string number, string street,
            string city, string zipCode, string country)
        {
            CreateContact("MyFirstname", "MyLastname", number, street, city, zipCode, country);
        }

        [When(@"I change it for (.*) - (.*) - (.*) - (.*) - (.*)")]
        public void WhenIChangeItFor(string number, string street,
            string city, string zipCode, string country)
        {
            var request = new UpdateContactAddressCommand()
            {
                Number = number,
                Street = street,
                City = city,
                ZipCode = zipCode,
                Country = country
            };

            var httpContent = new StringContent(JsonSerializer.ToJsonString(request), Encoding.UTF8, "application/json");

            var response = _client.PutAsync($"contacts/{_id}/address", httpContent).GetAwaiter().GetResult();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the new address should be (.*) - (.*) - (.*) - (.*) - (.*)")]
        public void ThenTheNewAddressShouldBe(string number, string street,
            string city, string zipCode, string country)
        {
            var response = _client.GetAsync($"contacts/{_id}").GetAwaiter().GetResult();

            var rawContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var content = JsonSerializer.Deserialize<GetContactByIdResponse>(rawContent, StandardResolver.CamelCase);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Number.Should().Be(number);
            content.Street.Should().Be(street);
            content.City.Should().Be(city);
            content.ZipCode.Should().Be(zipCode);
            content.Country.Should().Be(country);
        }

        [Given(@"I have created a contact")]
        public void GivenIHaveCreatedAContact()
        {
            CreateContact("MyFirstname", "MyLastname", "MyNumber", "MyStreet", "MyCity", "MyZipCode", "MyCountry");
        }

        [When(@"I delete it")]
        public void WhenIDeleteIt()
        {
            var response = _client.DeleteAsync($"contacts/{_id}").GetAwaiter().GetResult();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"it should be marked as deleted")]
        public void ThenItShouldBeMarkedAsDeleted()
        {
            var response = _client.GetAsync($"contacts/{_id}").GetAwaiter().GetResult();

            var rawContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var content = JsonSerializer.Deserialize<GetContactByIdResponse>(rawContent, StandardResolver.CamelCase);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.IsDeleted.Should().BeTrue();
        }

        private void CreateContact(string firstName, string lastName, string number, string street, string city, string zipCode, string country)
        {
            var request = new AddContactCommand()
            {
                FirstName = firstName,
                LastName = lastName,
                City = city,
                Country = country,
                Number = number,
                Street = street,
                ZipCode = zipCode
            };
            var httpContent = new StringContent(JsonSerializer.ToJsonString(request), Encoding.UTF8, "application/json");

            var response = _client.PostAsync("contacts", httpContent).GetAwaiter().GetResult();

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            _id = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

    }
}
