using Microsoft.AspNetCore.Mvc;
using Pokedex.Infrastructure.Persistence.Repositories;

namespace Pokedex.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
       
    }
}
