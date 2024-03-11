using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MGTConcerts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class InformationController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public InformationController(ILogger<ContactController> logger, IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
