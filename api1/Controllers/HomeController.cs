using Microsoft.AspNetCore.Mvc;

namespace api1.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    public HomeController()
    {

    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Hello from ablo");
    }

}