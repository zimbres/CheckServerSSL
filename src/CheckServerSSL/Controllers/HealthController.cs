namespace CheckServerSSL.Controllers;

[ApiController]
[Route("/")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<Health> Get()
    {
        var health = new Health();
        return Ok(health);
    }
}
