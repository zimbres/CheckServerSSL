namespace CheckServerSSL.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckServerController : ControllerBase
{
    private readonly ILogger<CheckServerController> _logger;
    private readonly SslClient _client;

    public CheckServerController(ILogger<CheckServerController> logger, SslClient client)
    {
        _logger = logger;
        _client = client;
    }

    [HttpGet()]
    public async Task<Response> Get(string server, int port, string data)
    {
        var response = await _client.SendCommandAsync(server, port, data);
        return response;
    }
}
