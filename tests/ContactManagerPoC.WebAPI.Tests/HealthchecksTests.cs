using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ContactManagerPoC.WebAPI.Tests
{
    public class HealthchecksTests : IClassFixture<WebApiFixture>
    {
        private readonly HttpClient _client;

        public HealthchecksTests(WebApiFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task WebAPIShouldBeHealthy()
        {
            // Act
            var response = await _client.GetAsync("/healthchecks");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsStringAsync()).Should().Be("Healthy");        
        }
    }
}
