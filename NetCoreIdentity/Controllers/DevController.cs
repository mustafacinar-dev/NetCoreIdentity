using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreIdentity.Controllers
{
    [Authorize(Roles = "Admin,Developer")]
    public class DevController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
