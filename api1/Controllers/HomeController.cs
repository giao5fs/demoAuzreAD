using api1.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace api1.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ApiUtility _apiUtility;

    public HomeController(ApiUtility apiUtility)
    {
        _apiUtility = apiUtility;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _apiUtility.GetData<object>();


        return Ok(response);
    }
}