using Microsoft.AspNetCore.Mvc;

namespace Pokedex.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
       
    }
}
