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
            _db = db;
        }
        public IActionResult Index()
        {
            List<MusicVenue> MusicVenueList = _db.Music_Venues.ToList();
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
                _db.Music_Venues.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            MusicVenue mv = _db.Music_Venues.Find(id);
            if (mv == null)
            {
                return NotFound();
            }
            return View(mv);
        }

        [HttpPost]
        public IActionResult Update(MusicVenue obj)
        {
            if (String.IsNullOrEmpty(obj.Name))
            {
                ModelState.AddModelError("name", "Θα πρέπει να συμπληρώσετε Όνομα");
            }

            if (ModelState.IsValid)
            {
                _db.Music_Venues.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            MusicVenue mv = _db.Music_Venues.Find(id);
            if (mv == null)
            {
                return NotFound();
            }
            return View(mv);
        }

        [HttpPost]
        public IActionResult Delete(MusicVenue obj)
        {
            _db.Music_Venues.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");         
        }
    }
}
