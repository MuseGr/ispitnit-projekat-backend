using Microsoft.AspNetCore.Mvc;
using Backend.Filters;

namespace Backend.Controllers
{
    public class PostController : Controller
    {
        [UserFilter]
        [HttpGet("proba")]
        public async Task<IActionResult> Proba()
        {
            
            return Ok();
        }
    }
}
