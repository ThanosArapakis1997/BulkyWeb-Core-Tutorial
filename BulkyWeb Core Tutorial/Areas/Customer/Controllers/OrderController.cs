using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using MGTConcerts.Utilities;
using Stripe.Checkout;

namespace MGTConcerts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        [BindProperty]
        public Order order { get; set; }
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
                var domain = "https://localhost:7007/";
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = domain + $"customer/order/success?id={obj.Id}",
                    CancelUrl = domain + $"customer/order/Index",
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                    Mode = "payment"
                };

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(concertt.Price * 100),
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = concertt.ConcertName,
                        }
                    },
                    Quantity = 1
                };
                options.LineItems.Add(sessionLineItem);                
                var service = new SessionService();
                Session session = service.Create(options);
                unitOfWork.Order.UpdateStripePaymentID(obj.Id, session.Id, session.PaymentIntentId);
                unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            Concert concert = unitOfWork.Concert.Get(u => u.Id == obj.ConcertId, includeProperties: "MusicVenue");
            MusicVenue mv = unitOfWork.MusicVenue.Get(u => u.Id == concert.MusicVenueId);
            ViewBag.Concert = concert;
            ViewBag.Venue = mv;
            return View();
        }

        //public IActionResult Payment(Order obj)
        //{
        //    Concert con = unitOfWork.Concert.Get(u => u.Id == obj.ConcertId);
            

        //}
        public IActionResult Success(int? id)
        {
            Order obj = unitOfWork.Order.Get(u => u.Id == id);
            Concert concert = unitOfWork.Concert.Get(u => u.Id == obj.ConcertId, includeProperties: "MusicVenue");
            MusicVenue mv = unitOfWork.MusicVenue.Get(u => u.Id == concert.MusicVenueId);
            ViewBag.Concert = concert;
            ViewBag.Venue = mv;
            return View(obj);
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
