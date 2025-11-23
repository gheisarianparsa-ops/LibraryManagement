using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
