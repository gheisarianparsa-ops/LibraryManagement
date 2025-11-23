using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
