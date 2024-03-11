using MGTConcerts.Data;
using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;
using MGTConcerts.Utilities;
using Microsoft.AspNetCore.Authorization;


namespace MGTConcerts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MusicVenueController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public MusicVenueController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            List<MusicVenue> MusicVenueList = unitOfWork.MusicVenue.GetAll().ToList();
            return View(MusicVenueList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MusicVenue obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                ModelState.AddModelError("name", "Θα πρέπει να συμπληρώσετε Όνομα");
            }

            if (obj.AvailableFrom == null || obj.AvailableTo == null)
            {
                ModelState.AddModelError(string.Empty, "Θα πρέπει να συμπληρώσετε Περίοδο Διαθεσιμότητας");
            }
            else
            {
                if (obj.AvailableFrom.Value > obj.AvailableTo.Value)
                {
                    ModelState.AddModelError(string.Empty, "Η Ημερομηνία Εναρξης Διαθεσιμότητας δεν μπορεί να είναι μικρότερη της Ημερομηνίας Λήξης της Διαθεσιμότητας");
                }
            }


            if (ModelState.IsValid)
            {
                unitOfWork.MusicVenue.Add(obj);
                unitOfWork.Save();
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
            MusicVenue mv = unitOfWork.MusicVenue.Get(u => u.Id == id);
            if (mv == null)
            {
                return NotFound();
            }
            return View(mv);
        }

        [HttpPost]
        public IActionResult Update(MusicVenue obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                ModelState.AddModelError("name", "Θα πρέπει να συμπληρώσετε Όνομα");
            }

            if (obj.AvailableFrom == null || obj.AvailableTo == null)
            {
                ModelState.AddModelError(string.Empty, "Θα πρέπει να συμπληρώσετε Περίοδο Διαθεσιμότητας");
            }
            else
            {
                if (obj.AvailableFrom.Value > obj.AvailableTo.Value)
                {
                    ModelState.AddModelError(string.Empty, "Η Ημερομηνία Εναρξης Διαθεσιμότητας δεν μπορεί να είναι μικρότερη της Ημερομηνίας Λήξης της Διαθεσιμότητας");
                }
            }

            if (ModelState.IsValid)
            {
                unitOfWork.MusicVenue.Update(obj);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<MusicVenue> VenueList = unitOfWork.MusicVenue.GetAll().ToList();
            return Json(new { data = VenueList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var venueToBeDeleted = unitOfWork.MusicVenue.Get(u => u.Id == id);
            if (venueToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            unitOfWork.MusicVenue.Remove(venueToBeDeleted);
            unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}
