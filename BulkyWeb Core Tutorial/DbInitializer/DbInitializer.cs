﻿using MGTConcerts.Data;
using MGTConcerts.Models;
using MGTConcerts.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MGTConcerts.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager; 
            _roleManager = roleManager; 
        }
        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Cashier)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();

            //if roles are not created, then we will create admin user as well
            _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@hotmail.com",
                    Email = "admin@hotmail.com",
                    Name = "Administrator",
                    PhoneNumber = "1112223333"
                }, "Admin123*").GetAwaiter().GetResult();


                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@dotnetmastery.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

            }

            return;




        }
    }
}
