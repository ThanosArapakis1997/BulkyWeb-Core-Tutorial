using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using MGTConcerts.Utilities;

namespace MGTConcerts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OrderController(IUnitOfWork _unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            unitOfWork = _unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int? id)
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
            List<Concert> Concerts = unitOfWork.Concert.GetAll(u => u.ArtistId == id, "Artist,MusicVenue").ToList();
            ViewBag.Concerts = Concerts;

            return View(mv);
        }

        public IActionResult Create(int? id)
        {
            Concert concert= unitOfWork.Concert.Get(u => u.Id == id, includeProperties: "MusicVenue");
            MusicVenue mv = unitOfWork.MusicVenue.Get(u => u.Id == concert.MusicVenueId);
            ViewBag.Concert = concert;
            ViewBag.Venue = mv;
            return View();
        }
         
        [HttpPost]
        public IActionResult Create(Order obj)
        {
            obj.Id = 0;
            if (ModelState.IsValid)
            {                
                unitOfWork.Order.Add(obj);
                unitOfWork.Save();
                TempData["success"] = "Η Κράτηση δημιουργήθηκε επιτυχώς";
                Concert concertt = unitOfWork.Concert.Get(u => u.Id == obj.ConcertId, includeProperties: "MusicVenue");
                MusicVenue mvv = unitOfWork.MusicVenue.Get(u => u.Id == concertt.MusicVenueId);
                ViewBag.Concert = concertt;
                ViewBag.Venue = mvv;
                return View("Success",obj);
            }
            Concert concert = unitOfWork.Concert.Get(u => u.Id == obj.ConcertId, includeProperties: "MusicVenue");
            MusicVenue mv = unitOfWork.MusicVenue.Get(u => u.Id == concert.MusicVenueId);
            ViewBag.Concert = concert;
            ViewBag.Venue = mv;
            return View();

        }

        public IActionResult Success(int? id)
        {           
            return View();
        }
        public IActionResult PrintToPDF(int? id)
        {
            Order mv = unitOfWork.Order.Get(u => u.Id == id);
            Concert con = unitOfWork.Concert.Get(u => u.Id == mv.ConcertId);
            string file;
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            MemoryStream ms = new MemoryStream();
            string templatePath = Path.Combine(wwwRootPath, @"Templates/OrderTemplate");
            bool printOk;
            printOk = Printer.Instance.PrintToPdf(templatePath ,mv,"OrderTemplate",out file, ms, unitOfWork);
            if (printOk)
            {
                ms.Position = 0;
                return File(ms, "application/pdf",con.ConcertName +"_"+ mv.SurName + ".pdf");
            }
            return NotFound();
        }

    }
}
