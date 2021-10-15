using HaftalıkRaporu.Data;
using HaftalıkRaporu.Models;
using HaftalıkRaporu.Models.ViewModels;
using HaftalıkRaporu.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HaftalıkRaporu.Controllers
{
    [Authorize(Roles =SD.AdminUser)]

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db;
        [BindProperty]
        public UserRaporVM userRaporVM { get; set; }
        public UsersController(ApplicationDbContext db)
        {
            this.db = db;

            userRaporVM = new UserRaporVM()
            {

                Rapor = new Rapor(),
                ApplicationUser = new ApplicationUser(),
                Satir = new Satir(),

                UserList = db.ApplicationUsers.ToList(),
                RaportList = db.Rapors.AsNoTracking().ToList(),
                SatirList = db.Satirs.ToList()



            };
        }

       
        public async Task<IActionResult> Index()
        {
            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            var claim = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string UserId = claim.Value;

            return View( await db.ApplicationUsers.Where(m=>m.Id!=UserId).ToListAsync());
        }
        public async Task<IActionResult> LockUnlock(string ? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var user = await db.ApplicationUsers.FindAsync(id);
            if (user==null)
            {
                return NotFound();
            }
            if (user.LockoutEnd==null||user.LockoutEnd<DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            else
            {
                user.LockoutEnd = DateTime.Now;
            }
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GelenRaporlar()
        {
            var RaporList =  db.Rapors.Include(m => m.ApplicationUser).ToList();
            userRaporVM.RaportList = RaporList;
            return View(userRaporVM);
        }
        public IActionResult Delete(int id)
        {
            var rapor = db.Rapors.Find(id);
            db.Rapors.Remove(rapor);
            db.SaveChanges();
            return RedirectToAction("GelenRaporlar");
        }
        public async Task<IActionResult> JustView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rabor = db.Rapors.Find(id);
            if (rabor == null)
            {
                return NotFound();
            }
            userRaporVM.Rapor = rabor;

            var SatirList = await db.Satirs.Include(m => m.Rapor).ToListAsync();
            SatirList = db.Satirs.Where(m => m.RaporId == id).ToList();

            return View(SatirList);
        }
    }
}
