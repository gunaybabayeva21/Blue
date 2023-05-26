using Blue.DataContext;
using Blue.Models;
using Blue.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blue.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlueDbContext _blueDbContext;
        public HomeController(BlueDbContext blueDbContext)
        {
            _blueDbContext = blueDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Feature> features = await _blueDbContext.Features.ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Fetures = features
            };
            return View (homeVM);
        }

        
    }
}