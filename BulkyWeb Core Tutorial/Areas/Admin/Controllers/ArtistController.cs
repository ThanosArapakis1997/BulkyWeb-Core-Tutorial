using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MGTConcerts.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace MGTConcerts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ArtistController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArtistController(IUnitOfWork _unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            unitOfWork = _unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            List<Artist> ArtistList = unitOfWork.Artist.GetAll(includeProperties: "Concerts").ToList();
            return View(ArtistList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Artist obj, IFormFile file)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                ModelState.AddModelError("name", "Θα πρέπει να συμπληρώσετε Όνομα");
            }
            if (file==null)
            {
                ModelState.AddModelError("ImageUrl", "Θα πρέπει να προσθέσετε εικόνα του καλλιτέχνη");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename= Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string artistpath = Path.Combine(wwwRootPath, @"images/artist");

                    using (var filestream = new FileStream(Path.Combine(artistpath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    };
                    obj.ImageUrl = @"images/artist/" + filename;
                }
                unitOfWork.Artist.Add(obj);
                unitOfWork.Save();
                TempData["success"] = "Ο Καλλιτέχνης δημιουργήθηκε επιτυχώς";
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
            Artist mv = unitOfWork.Artist.Get(u => u.Id == id);
            if (mv == null)
            {
                return NotFound();
            }
            List<Concert> Concerts = unitOfWork.Concert.GetAll(u => u.ArtistId == id,"Artist,MusicVenue").ToList();
            ViewBag.Concerts = Concerts;
            return View(mv);
        }

        [HttpPost]
        public IActionResult Update(Artist obj, IFormFile file)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                ModelState.AddModelError("name", "Θα πρέπει να συμπληρώσετε Όνομα");
            }
            if (file == null)
            {
                ModelState.AddModelError("ImageUrl", "Θα πρέπει να προσθέσετε εικόνα του καλλιτέχνη");
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string artistpath = Path.Combine(wwwRootPath, @"images/artist");

                    using (var filestream = new FileStream(Path.Combine(artistpath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    };
                    obj.ImageUrl = @"images/artist/" + filename;
                }

                TempData["success"] = "Ο Καλλιτέχνης ενημερώθηκε επιτυχώς";
                unitOfWork.Artist.Update(obj);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Artist> ArtistList = unitOfWork.Artist.GetAll(includeProperties: "Concerts").ToList();
            return Json(new { data = ArtistList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var artistToBeDeleted = unitOfWork.Artist.Get(u => u.Id == id);
            if (artistToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            unitOfWork.Artist.Remove(artistToBeDeleted);
            unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}
