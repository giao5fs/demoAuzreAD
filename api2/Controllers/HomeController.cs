using Microsoft.AspNetCore.Mvc;

namespace api2.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var obj = new { id = 1, name = "AAHAHAHAH" };
        return Ok(obj);
    }
}