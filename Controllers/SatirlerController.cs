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
using System.Threading.Tasks;

namespace HaftalıkRaporu.Controllers
{
    [Authorize(Roles = SD.EmployeeUser)]

    public class SatirlerController : Controller
    {
        private readonly ApplicationDbContext db;
        [BindProperty]
        public UserRaporVM userRaporVM { get; set; }
        public SatirlerController(ApplicationDbContext db)
        {
            this.db = db;

            userRaporVM = new UserRaporVM()
            {

                Rapor = new Rapor(),
                ApplicationUser = new ApplicationUser(),
                Satir=new Satir(),

                UserList = db.ApplicationUsers.ToList(),
                RaportList = db.Rapors.ToList(),
                SatirList=db.Satirs.AsNoTracking().ToList()



            };
        }
      
        public IActionResult Delete(int id)
        {
            var satir = db.Satirs.Find(id);
            db.Satirs.Remove(satir);
            db.SaveChanges();
            return RedirectToAction("Index","Empolyee");
        }
        public  IActionResult edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var satir = db.Satirs.Include(m => m.Rapor).SingleOrDefault(m => m.Id == id);

            if (satir == null)
            {
                return NotFound();
            }
            userRaporVM.Satir = satir;

            return View(userRaporVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("edit")]
        public async Task<IActionResult> editPost()
        {

            if (ModelState.IsValid)
            {
               
               
                db.Satirs.Update(userRaporVM.Satir);
                await db.SaveChangesAsync();
              
                return RedirectToAction("Goruntule", "Empolyee", new { id = userRaporVM.Satir.RaporId});
            }
            return View(userRaporVM);
        }

        public async Task<IActionResult> JustView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var satir = db.Satirs.Find(id);
            if (satir == null)
            {
                return NotFound();
            }
            userRaporVM.Satir = satir;

            var SatirList = await db.Satirs.Include(m => m.Rapor).ToListAsync();
            SatirList = db.Satirs.Where(m => m.Id == id).ToList();

            return View(SatirList);
        }

    }
}
