using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult AddPresents([FromBody] List<Order> orders)
        {
            if (orders != null)
            {
                foreach (Order order in orders)
                {
                    unitOfWork.Order.Update(order);
                }
                unitOfWork.Save();
            }
            return Ok();
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
