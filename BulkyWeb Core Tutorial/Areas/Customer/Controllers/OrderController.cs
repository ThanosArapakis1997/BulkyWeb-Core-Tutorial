using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MGTConcerts.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
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
    }
}
