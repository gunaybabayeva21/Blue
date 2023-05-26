using Blue.DataContext;
using Blue.Models;
using Blue.ViewModels.FeatureVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blue.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController:Controller
    {
        private readonly BlueDbContext _blueDbContext;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public FeatureController(BlueDbContext blueDbContext, IWebHostEnvironment webHostEnvironment )
        {
            _blueDbContext = blueDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            List<Feature>features=await _blueDbContext.Features.ToListAsync();
            return View(features);
        }

        public async Task<IActionResult> Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeatureCreateVM createVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createVM);
            }
            Feature feature = new Feature()
            {
               Title=createVM.Title,
               Description=createVM.Description,
               
            };
            if(createVM.Image.ContentType.Contains("image/")&& createVM.Image.Length / 1024>2028) 
            {
                ModelState.AddModelError("", "error");
                return View(createVM);
            }
            string newFileName = Guid.NewGuid().ToString() + createVM.Image.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "portfolio", newFileName);
            using (FileStream stream =new FileStream(path, FileMode.Create))
            {
                createVM.Image.CopyToAsync(stream);
            }
            feature.ImageName = newFileName;
            await _blueDbContext.Features.AddAsync(feature);
            await _blueDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
           Feature feature= await _blueDbContext.Features.FindAsync(id);

            if (feature == null) { return NotFound(); }
            FeatureEditVM editVM = new FeatureEditVM()
            {
                Description = feature.Description,
                Title = feature.Title,

            }; 
            return View (editVM);  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FeatureEditVM editVM)
        {
            Feature? feature = await _blueDbContext.Features.FindAsync(id);
            if (feature == null) { return NotFound(); }
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View(editVM);
            };
            if (editVM.Image!=null)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "portfolio", feature.ImageName);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string NewFileName = Guid.NewGuid().ToString() + editVM.Image.FileName;
                using (FileStream stream= new FileStream(path, FileMode.CreateNew))
                {
                   await editVM.Image.CopyToAsync(stream);
                }
                feature.ImageName = NewFileName;
            }
            feature.Title = editVM.Title;
            feature.Description = editVM.Description;
             _blueDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            Feature feature = await _blueDbContext.Features.FindAsync(id);
            if (feature == null) { return NotFound();}
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "portfolio", feature.ImageName);
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _blueDbContext.Features.Remove(feature);
             _blueDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Detail(int id)
        {
            Feature feature = await _blueDbContext.Features.FindAsync(id);
            if (feature == null) { return NotFound();}
            return View(feature);
        }
    }
}
