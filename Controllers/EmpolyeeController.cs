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
    [Authorize(Roles = SD.EmployeeUser)]
   
    public class EmpolyeeController : Controller
    {
        private readonly ApplicationDbContext db;
        [BindProperty]
        public UserRaporVM userRaporVM { get; set; }
        public EmpolyeeController(ApplicationDbContext db)
        {
            this.db = db;

            userRaporVM = new UserRaporVM()
            {

                Rapor = new Rapor(),
                ApplicationUser = new ApplicationUser(),
                Satir=new Satir(),

                UserList = db.ApplicationUsers.ToList(),
                RaportList=db.Rapors.AsNoTracking().ToList(),
                SatirList=db.Satirs.ToList()
             
             

            };
        }

        public async Task<IActionResult> Index()    
        {
            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            var claim = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string UserId = claim.Value;

           
            var RaporList = await db.Rapors.Include(m => m.ApplicationUser).ToListAsync();
            RaporList= db.Rapors.Where(m => m.ApplicationUserId == UserId).ToList();
            return View(RaporList);
        }
        public IActionResult Create()
        {

            return View(userRaporVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        
        public async Task<IActionResult> CreatePost()
        {
            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            var claim = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string UserId = claim.Value;

            userRaporVM.Rapor.ApplicationUserId  = UserId;
            userRaporVM.Rapor.DuzenlenmeTarihi  = DateTime.Now;

            db.Rapors.Add(userRaporVM.Rapor);
            await db.SaveChangesAsync();
            TempData["Referrer"] = "SaveRegister";
            
            return RedirectToAction("Goruntule", "Empolyee", new { id = userRaporVM.Rapor.Id });
        }
        public async Task<IActionResult> SendNotsend(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rabor =  db.Rapors.Find(id);
            if (rabor == null)
            {
                return NotFound();
            }
            if (rabor.Durumu==false)
            {
                rabor.Durumu = true;
            }
           
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var rapor = db.Rapors.Find(id);
            db.Rapors.Remove(rapor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var rapor = db.Rapors.Find(id);
            if (rapor == null)
            {
                return NotFound();
            }
            userRaporVM.Rapor = rapor;
            return View(userRaporVM);
        }

        [ValidateAntiForgeryToken]
        [ActionName("edit")]
        [HttpPost]
        
        public async Task<IActionResult> editPost()
        {
            if (ModelState.IsValid)
            {
                var cliamsIdentity = (ClaimsIdentity)User.Identity;
                var claim = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                string UserId = claim.Value;
                userRaporVM.Rapor.ApplicationUserId = UserId;
                userRaporVM.Rapor.DuzenlenmeTarihi = DateTime.Now;

                db.Rapors.Update(userRaporVM.Rapor);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRaporVM);
        }
        public IActionResult SatirEkle(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var satir = db.Rapors.Find(id);
            if (satir == null)
            {
                return NotFound();
            }
            userRaporVM.Rapor = satir;
            return View(userRaporVM);
        }
        [ValidateAntiForgeryToken]
        [ActionName("SatirEkle")]
        [HttpPost]
        public async Task<IActionResult> CreateSatir(int id)
        {
                userRaporVM.Satir.RaporId = id;
                db.Satirs.Add(userRaporVM.Satir);
                await db.SaveChangesAsync();
                return RedirectToAction("Goruntule", "Empolyee", new { id = userRaporVM.Satir.RaporId });


        }
        public async Task<IActionResult> Goruntule(int? id)
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
            userRaporVM.SatirList = SatirList;

            return View(userRaporVM);
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
