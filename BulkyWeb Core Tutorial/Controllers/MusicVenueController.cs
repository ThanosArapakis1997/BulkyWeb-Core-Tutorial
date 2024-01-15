using BulkyWeb_Core_Tutorial.Data;
using BulkyWeb_Core_Tutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb_Core_Tutorial.Controllers
{
    public class MusicVenueController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MusicVenueController(ApplicationDbContext db) 
        {
            _db= db;
        }
        public IActionResult Index()
        {
            List<MusicVenue> MusicVenueList= _db.Music_Venues.ToList();
            return View(MusicVenueList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MusicVenue obj)
        {
            if (String.IsNullOrEmpty(obj.Name))
            {
                ModelState.AddModelError("name", "Θα πρέπει να συμπληρώσετε Όνομα");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
