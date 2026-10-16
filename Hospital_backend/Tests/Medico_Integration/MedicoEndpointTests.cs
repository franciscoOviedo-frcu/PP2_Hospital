using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Tests;

public class MedicoEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public MedicoEndpointTests(WebApplicationFactory<Program> factory)
    {
        // Crea un cliente HTTP conectado al servidor en memoria
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetMedicos_DeberiaRetornarOk()
    {
        // Act: llamamos al endpoint
        var response = await _client.GetAsync("/api/Medico/leer-todo");

        // Assert: verificamos código de estado 200 OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
