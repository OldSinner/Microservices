using Microsoft.AspNetCore.Mvc;

namespace PlatformService.Extensions
{   
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller { }
}