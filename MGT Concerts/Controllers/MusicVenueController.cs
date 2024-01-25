using MGTConcerts.Data;
using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MGTConcerts.Controllers
{
    public class MusicVenueController : Controller
    {
        private readonly IMusicVenueRepository musicVenueRepository;
        public MusicVenueController(IMusicVenueRepository db)
        {
            musicVenueRepository = db;
        }
        public IActionResult Index()
        {
            List<MusicVenue> MusicVenueList = musicVenueRepository.GetAll().ToList();
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

            if (obj.AvailableFrom == null || obj.AvailableTo == null)
            {
                ModelState.AddModelError(string.Empty, "Θα πρέπει να συμπληρώσετε Περίοδο Διαθεσιμότητας");
            }
            else
            {
                if(obj.AvailableFrom.Value > obj.AvailableTo.Value)
                {
                    ModelState.AddModelError(string.Empty, "Η Ημερομηνία Εναρξης Διαθεσιμότητας δεν μπορεί να είναι μικρότερη της Ημερομηνίας Λήξης της Διαθεσιμότητας");
                }
            }
                    

            if (ModelState.IsValid)
            {
                musicVenueRepository.Add(obj);
                musicVenueRepository.Save();
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
            MusicVenue mv = musicVenueRepository.Get(u => u.Id == id);
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
                musicVenueRepository.Update(obj);
                musicVenueRepository.Save();
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
            MusicVenue mv = musicVenueRepository.Get(u => u.Id == id);
            if (mv == null)
            {
                return NotFound();
            }
            return View(mv);
        }

        [HttpPost]
        public IActionResult Delete(MusicVenue obj)
        {
            musicVenueRepository.Remove(obj);
            musicVenueRepository.Save();
            return RedirectToAction("Index");         
        }
    }
}
