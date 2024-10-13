using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using MGTConcerts.FuzzyLogic;

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

        [Authorize]
        public IActionResult Recommendations()
        {
            // Initialize the fuzzy logic system
            FuzzyLogicSystem fuzzyLogicSystem = new FuzzyLogicSystem();
            DistanceCalculationService distanceCalculationService = new DistanceCalculationService();

            //Get all concerts
            List<Concert> ConcertList = unitOfWork.Concert.GetAll(includeProperties: "MusicVenue").ToList();
            Dictionary<int, int> ConcertScoreDict = new Dictionary<int, int>();

            //get current user
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            ApplicationUser user = unitOfWork.ApplicationUser.Get(x => x.Email.Equals(userEmail), includeProperties: "Preference");
            if (user.Preference != null)
            {
                //Fuzzify and Apply Rules
                //
                foreach(Concert concert in ConcertList)
                {
                    Genre? genre = concert.Genre;
                    Preference pref = user.Preference;

                    double userPreference = GetGenrePreference(pref,genre);
                    double price= concert.Price;

                    //User's Location
                    int userLong = user.Longitude.Value;
                    int userLat = user.Latitude.Value;

                    //Concert's Location
                    int concertLong = concert.MusicVenue.Longitude.Value;
                    int concertLat = concert.MusicVenue.Latitude.Value;

                    //Calculate distance with the new service DistanceCalculationService()

                    double distance  = distanceCalculationService.CalculateDistance(userLong,userLat, concertLong, concertLat);
                    double recommendation = fuzzyLogicSystem.GetRecommendation(price, userPreference, distance);
                    concert.FuzzyScore = recommendation;

                }

                return View(ConcertList.OrderBy(x => x.FuzzyScore).ToList());
            }

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


        public double GetGenrePreference(Preference pref, Genre? gen)
        {
            if (gen.HasValue)
            {
                switch (gen.Value)
                {
                    case Genre.Rock:
                        return pref.rockPreference;
                    case Genre.Pop:
                        return pref.popPreference;
                    case Genre.Rap:
                        return pref.rapPreference;
                    case Genre.Electro:
                        return pref.electroPreference;
                    case Genre.Indie:
                        return pref.indiePreference;
                    case Genre.Metal:
                        return pref.metalPreference;
                    default:
                        return 0;
                }
            }
            return 0;
        }
    }
}