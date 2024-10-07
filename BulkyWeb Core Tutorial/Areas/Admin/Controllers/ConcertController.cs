using MGTConcerts.Models;
using MGTConcerts.Repository;
using MGTConcerts.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using MGTConcerts.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace MGTConcerts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ConcertController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ConcertController(IUnitOfWork _unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            unitOfWork = _unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Concert> ConcertList = unitOfWork.Concert.GetAll(includeProperties: "Artist,MusicVenue").ToList();
            
            return View(ConcertList);
        }

        public IActionResult Create()
        {
            ConcertVm concertVM = new()
            {
                Artists = unitOfWork.Artist.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                MusicVenues = unitOfWork.MusicVenue.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Concert = new Concert()
            };
            IEnumerable<SelectListItem> Artists = unitOfWork.Artist.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            IEnumerable<SelectListItem> MusicVenues = unitOfWork.MusicVenue.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            IEnumerable<SelectListItem> Genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            ViewBag.MusicVenues = MusicVenues;
            ViewBag.Artists = Artists;
            ViewBag.Genres = Genres;
            return View();            
        }

        [HttpPost]
        public IActionResult Create(Concert obj, IFormFile file)
        {
            MusicVenue Selectedmv = unitOfWork.MusicVenue.Get(u => u.Id == obj.MusicVenueId);

            if (obj.ConcertDate > Selectedmv.AvailableTo.Value || obj.ConcertDate < Selectedmv.AvailableFrom.Value)
            {
                ModelState.AddModelError("ConcertDate","Η Ημερομηνία Συναυλίας είναι εκτός των ορίων διαθεσιμότητας του χώρου, τα οποία είναι: " +  Selectedmv.AvailablePeriod);
            }

            if (String.IsNullOrEmpty(obj.ConcertName))
            {
                ModelState.AddModelError("ConcertName", "Θα πρέπει να συμπληρώσετε Τίτλο Συναυλίας");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string artistpath = Path.Combine(wwwRootPath, @"images/concert");

                    using (var filestream = new FileStream(Path.Combine(artistpath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    };
                    obj.ImageUrl = @"images/concert/" + filename;
                }
                unitOfWork.Concert.Add(obj);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            IEnumerable<SelectListItem> Artists = unitOfWork.Artist.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            IEnumerable<SelectListItem> MusicVenues = unitOfWork.MusicVenue.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            IEnumerable<SelectListItem> Genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            ViewBag.Genres = Genres;
            ViewBag.MusicVenues = MusicVenues;
            ViewBag.Artists = Artists;
            return View();

        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Concert mv = unitOfWork.Concert.Get(u => u.Id == id);
            
            if (mv == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> Artists = unitOfWork.Artist.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            IEnumerable<SelectListItem> MusicVenues = unitOfWork.MusicVenue.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            IEnumerable<SelectListItem> Genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            ViewBag.Genres = Genres;
            ViewBag.MusicVenues = MusicVenues;
            ViewBag.Artists = Artists;
            return View(mv);
        }

        [HttpPost]
        public IActionResult Update(Concert obj, IFormFile file)
        {
            MusicVenue Selectedmv = unitOfWork.MusicVenue.Get(u => u.Id == obj.MusicVenueId);

            if (obj.ConcertDate > Selectedmv.AvailableTo.Value || obj.ConcertDate < Selectedmv.AvailableFrom.Value)
            {
                ModelState.AddModelError("ConcertDate", "Η Ημερομηνία Συναυλίας είναι εκτός των ορίων διαθεσιμότητας του χώρου, τα οποία είναι: " + Selectedmv.AvailablePeriod);
            }

            if (String.IsNullOrEmpty(obj.ConcertName))
            {
                ModelState.AddModelError("ConcertName", "Θα πρέπει να συμπληρώσετε Τίτλο Συναυλίας");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string artistpath = Path.Combine(wwwRootPath, @"images/concert");

                    using (var filestream = new FileStream(Path.Combine(artistpath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    };
                    obj.ImageUrl = @"images/concert/" + filename;
                }
                unitOfWork.Concert.Update(obj);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            IEnumerable<SelectListItem> Artists = unitOfWork.Artist.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            IEnumerable<SelectListItem> MusicVenues = unitOfWork.MusicVenue.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            IEnumerable<SelectListItem> Genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            ViewBag.Genres = Genres;
            ViewBag.MusicVenues = MusicVenues;
            ViewBag.Artists = Artists;
            return View();

        }

        #region API CALLS

        [HttpGet] 
        public IActionResult GetAll() 
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };
            List<Concert> ConcertList = unitOfWork.Concert.GetAll(includeProperties: "Artist,MusicVenue").ToList();
            return Json(new { data = ConcertList },new JsonSerializerOptions(options));
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var concertToBeDeleted = unitOfWork.Concert.Get(u => u.Id == id);
            if (concertToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }                   


            unitOfWork.Concert.Remove(concertToBeDeleted);
            unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
