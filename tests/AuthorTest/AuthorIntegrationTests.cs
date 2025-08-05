using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorTest
{
    public class AuthorIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _fixture;

        public AuthorIntegrationTests(WebApplicationFactory<Program> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Test1()
        {
            HttpClient client = _fixture.CreateClient();
            var response = await client.GetAsync("/author");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(" ", content);
        }
    }
}
