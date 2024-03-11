using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MGTConcerts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public ContactController(ILogger<ContactController> logger, IUnitOfWork _unitOfWork)
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