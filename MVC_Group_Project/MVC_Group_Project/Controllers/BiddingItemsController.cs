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
    public class BiddingItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BiddingItems
        public ActionResult Index()
        {
            return View(db.BiddingItems.ToList());
        }

        // GET: BiddingItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BiddingItem biddingItem = db.BiddingItems.Find(id);
            if (biddingItem == null)
            {
                return HttpNotFound();
            }
            return View(biddingItem);
        }

        // GET: BiddingItems/Create
   
        public ActionResult Create()
        {
            return View(db.SubCategories.ToList());
           
        }

        // POST: BiddingItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BiddingItemID,ItemName,ItemDescription,BidStartTime,BidEndTime,BidStartPrice")] BiddingItem biddingItem, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string imagePath = Server.MapPath("~/Images/" + file.FileName);
                file.SaveAs(imagePath);

                biddingItem.ItemImageURL = "Images/" + file.FileName;

                db.BiddingItems.Add(biddingItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(biddingItem);
        }

        // GET: BiddingItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BiddingItem biddingItem = db.BiddingItems.Find(id);
            if (biddingItem == null)
            {
                return HttpNotFound();
            }
            return View(biddingItem);
        }

        // POST: BiddingItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BiddingItemID,ItemName,ItemDescription,ItemImageURL,BidStartTime,BidEndTime,BidStartPrice,CurrentPrice")] BiddingItem biddingItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(biddingItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(biddingItem);
        }

        // GET: BiddingItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BiddingItem biddingItem = db.BiddingItems.Find(id);
            if (biddingItem == null)
            {
                return HttpNotFound();
            }
            return View(biddingItem);
        }

        // POST: BiddingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BiddingItem biddingItem = db.BiddingItems.Find(id);
            db.BiddingItems.Remove(biddingItem);
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
