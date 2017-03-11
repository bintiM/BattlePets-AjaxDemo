using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BattlePets.Models;

namespace BattlePets.Controllers
{
    public class BattlePetsController : Controller
    {
        private petsEntities db = new petsEntities();

        // GET: BattlePets
        public ActionResult Index()
        {
            return View(db.BattlePet.ToList());
        }
        [HttpPost]
        public JsonResult Index(string searchTerm)
        {
            //Suche Einträge beginnend mit searchTerm mithilfe von LINQ query  
            var PetName = (from pet in db.BattlePet.ToList()
                           where pet.name.StartsWith(searchTerm, StringComparison.CurrentCultureIgnoreCase)
                            select new { pet.name });
            return Json(PetName, JsonRequestBehavior.AllowGet);
        }

        // GET: BattlePets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BattlePet battlePet = db.BattlePet.Find(id);
            if (battlePet == null)
            {
                return HttpNotFound();
            }
            return View(battlePet);
        }

        // GET: BattlePets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BattlePets/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,hp,dmg,speed")] BattlePet battlePet)
        {
            if (ModelState.IsValid)
            {
                db.BattlePet.Add(battlePet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(battlePet);
        }

        // GET: BattlePets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BattlePet battlePet = db.BattlePet.Find(id);
            if (battlePet == null)
            {
                return HttpNotFound();
            }
            return View(battlePet);
        }

        // POST: BattlePets/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,hp,dmg,speed")] BattlePet battlePet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(battlePet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(battlePet);
        }

        // GET: BattlePets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BattlePet battlePet = db.BattlePet.Find(id);
            if (battlePet == null)
            {
                return HttpNotFound();
            }
            return View(battlePet);
        }

        // POST: BattlePets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BattlePet battlePet = db.BattlePet.Find(id);
            db.BattlePet.Remove(battlePet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
