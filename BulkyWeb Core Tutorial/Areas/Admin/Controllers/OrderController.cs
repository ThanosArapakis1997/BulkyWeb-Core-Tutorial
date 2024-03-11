using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;
using MGTConcerts.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace MGTConcerts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Concerts()
        {
            List<Concert> ConcertList = unitOfWork.Concert.GetAll(u=>u.ConcertDate < DateTime.Today).ToList();
            return View(ConcertList);
        }
        public IActionResult Index(int? id)
        {
            List<Order> OrderList = unitOfWork.Order.GetAll(u => u.ConcertId == id && u.Present, includeProperties: "Concert").ToList();
            ViewBag.ConcertId = id;
            return View(OrderList);
        }
    }
}
