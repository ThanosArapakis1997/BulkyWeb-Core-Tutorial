using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace MGTConcerts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        //define a dictionary for <Concert,scores> to store the defuzzification scores for every concert


        public HomeController(ILogger<HomeController> logger, IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.AskForPref = false;
            if (User.Identity.IsAuthenticated) {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                ApplicationUser user = unitOfWork.ApplicationUser.Get(x=> x.Email.Equals(userEmail), includeProperties: "Preference");
                if(user.Preference == null)
                {
                    ViewBag.AskForPref = true;
                }
            }
            List<Artist> ArtistList = unitOfWork.Artist.GetAll(includeProperties: "Concerts").ToList();
            List<Concert> ConcertList = unitOfWork.Concert.GetAll().ToList();
            ViewBag.ConcertList= ConcertList;
            return View(ArtistList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Recommendations()
        {
            List<Concert> ConcertList = unitOfWork.Concert.GetAll(includeProperties: "MusicVenue").ToList();
            //Dictionary<Concert,Score> myDict
            //
            //
            //

            //Fuzzify and Apply Rules
            //
            //for concert in concerts
            //  genre = getGenre()
            //  userPref = getGenrePreference(genre)
            //
            //  price = getPrice()
            //  userLoc = getUserLocation()
            //  concertLoc = getConcertLocation()
            //  distance = concertLoc-userLoc
            //
            //  score = evaluateScore(userPref, price, distance)
            //  myDict[concert] = score
            //
            //sortedConcerts = SortConcerts(myDict)


            //return View(sortedConcerts)
            return View(ConcertList.OrderBy(x=>x.Price).ToList());
        }

        public IActionResult SetPreference()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetPreference(Preference obj)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            ApplicationUser user = unitOfWork.ApplicationUser.Get(x => x.Email.Equals(userEmail));
            obj.UserId = user.Id;
            unitOfWork.Preference.Add(obj);
            unitOfWork.Save();

            user.PreferenceId = obj.Id;
            unitOfWork.ApplicationUser.Update(user);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }


        public IActionResult Orders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            List<Order> orders = unitOfWork.Order.GetAll(u=> u.Email == userEmail, includeProperties: "Concert").ToList();

            return View(orders);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}