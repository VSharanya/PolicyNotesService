using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using PolicyNotesService.Models;

namespace PolicyNotesService.Tests.Integration;

public class PolicyNotesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PolicyNotesIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task PostNote_ShouldReturnCreated()
    {
        var client = _factory.CreateClient();

        var dto = new PolicyNoteCreateDto
        {
            PolicyNumber = "PN002",
            Note = "Integration test note"
        };

        var response = await client.PostAsJsonAsync("/notes", dto);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetNotes_ShouldReturnOk()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/notes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetNoteById_ReturnsNotFound_WhenMissing()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/notes/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
