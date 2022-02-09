using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pos.web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ApplicationBaseController : ControllerBase
    {

    }
}
