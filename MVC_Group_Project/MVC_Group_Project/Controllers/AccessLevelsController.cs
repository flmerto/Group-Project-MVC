using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Group_Project.Models;

namespace MVC_Group_Project.Controllers
{
    public class AccessLevelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccessLevels
        public ActionResult Index()
        {
            return View(db.AccessLevels.ToList());
        }

        // GET: AccessLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessLevel accessLevel = db.AccessLevels.Find(id);
            if (accessLevel == null)
            {
                return HttpNotFound();
            }
            return View(accessLevel);
        }

        // GET: AccessLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccessLevelID,AccessType")] AccessLevel accessLevel)
        {
            if (ModelState.IsValid)
            {
                db.AccessLevels.Add(accessLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accessLevel);
        }

        // GET: AccessLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessLevel accessLevel = db.AccessLevels.Find(id);
            if (accessLevel == null)
            {
                return HttpNotFound();
            }
            return View(accessLevel);
        }

        // POST: AccessLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccessLevelID,AccessType")] AccessLevel accessLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accessLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accessLevel);
        }

        // GET: AccessLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessLevel accessLevel = db.AccessLevels.Find(id);
            if (accessLevel == null)
            {
                return HttpNotFound();
            }
            return View(accessLevel);
        }

        // POST: AccessLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessLevel accessLevel = db.AccessLevels.Find(id);
            db.AccessLevels.Remove(accessLevel);
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
