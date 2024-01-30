using MGTConcerts.Models;
using MGTConcerts.Repository;
using MGTConcerts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MGTConcerts.Areas.Admin.Controllers
{
    [Area("Admin")]
    []
    public class ConcertController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public ConcertController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
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
            ViewBag.MusicVenues = MusicVenues;
            ViewBag.Artists = Artists;

            return View();            
        }

        [HttpPost]
        public IActionResult Create(Concert obj)
        {
            MusicVenue Selectedmv = unitOfWork.MusicVenue.Get(u => u.Id == obj.MusicVenueId);

            if (obj.ConcertDate > Selectedmv.AvailableTo.Value || obj.ConcertDate < Selectedmv.AvailableFrom.Value)
            {
                ModelState.AddModelError("ConcertDate","Η Ημερομηνία Συναυλίας είναι εκτός των ορίων διαθεσιμότητας του χώρου, τα οποία είναι: " +  Selectedmv.AvailablePeriod);
            }

            if (ModelState.IsValid)
            {
                unitOfWork.Concert.Add(obj);
                unitOfWork.Artist.AddConcert(obj);
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
            ViewBag.MusicVenues = MusicVenues;
            ViewBag.Artists = Artists;
            return View(mv);
        }

        [HttpPost]
        public IActionResult Update(Concert obj)
        {
            MusicVenue Selectedmv = unitOfWork.MusicVenue.Get(u => u.Id == obj.MusicVenueId);

            if (obj.ConcertDate > Selectedmv.AvailableTo.Value || obj.ConcertDate < Selectedmv.AvailableFrom.Value)
            {
                ModelState.AddModelError("ConcertDate", "Η Ημερομηνία Συναυλίας είναι εκτός των ορίων διαθεσιμότητας του χώρου, τα οποία είναι: " + Selectedmv.AvailablePeriod);
            }

            if (ModelState.IsValid)
            {
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
            ViewBag.MusicVenues = MusicVenues;
            ViewBag.Artists = Artists;
            return View();

        }


        public IActionResult Delete(int? id)
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
            return View(mv);
        }

        [HttpPost]
        public IActionResult Delete(Concert obj)
        {
            unitOfWork.Concert.Remove(obj);
            unitOfWork.Artist.RemoveConcert(obj);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
