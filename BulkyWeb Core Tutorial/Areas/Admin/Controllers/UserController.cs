using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MGTConcerts.Utilities;
using Microsoft.AspNetCore.Authorization;
using MGTConcerts.Data;
using Microsoft.EntityFrameworkCore;

namespace MGTConcerts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class UserController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;



        public UserController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }

        /*public UserController(ApplicationDbContext db) {
            _db = db;
        }*/

        public IActionResult Index()
        {
            return View();
        }
        
        #region API CALLS 

        [HttpGet]
        public IActionResult GetAll()
        {
           // List<ApplicationUser> objUserList = _db.ApplicationUsers.Include(u=>u.UserName).ToList();
            List<ApplicationUser> objUserList = unitOfWork.ApplicationUser.GetAll().ToList();   
            return Json(new { data = objUserList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}
