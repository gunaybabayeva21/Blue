using Microsoft.AspNetCore.Mvc;

namespace Blue.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController:Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
