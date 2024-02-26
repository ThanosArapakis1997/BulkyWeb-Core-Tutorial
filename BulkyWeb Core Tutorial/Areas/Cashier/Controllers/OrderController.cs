using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;
using Order = MGTConcerts.Models.Order;

namespace MGTConcerts.Areas.Cashier.Controllers
{
    [Area("Cashier")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Concerts()
        {
            List<Concert> ConcertList = unitOfWork.Concert.GetAll().ToList();
            return View(ConcertList);
        }
        public IActionResult Index(int? id)
        {
            List<Order> OrderList = unitOfWork.Order.GetAll(u=>u.ConcertId == id && !u.Present, includeProperties: "Concert").ToList();
            ViewBag.ConcertId = id;
            return View(OrderList);
        }

        [HttpPost]
        public IActionResult AddPresents(int id)
        {
            Order order = unitOfWork.Order.Get(u => u.Id == id);
            order.Present = true;
            TempData["success"] = "Η Κράτηση ενημερώθηκε επιτυχώς";
            unitOfWork.Order.Update(order);
            unitOfWork.Save();
                                  
            return Redirect($"/cashier/order/index?id={order.ConcertId}");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(int? id)
        {
            List<Order> OrderList = unitOfWork.Order.GetAll(u =>u.ConcertId == id,includeProperties: "Concert").ToList();
            return Json(new { data = OrderList });
        }
        #endregion
    }

}
