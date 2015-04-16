using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Group_Project.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace MVC_Group_Project.Controllers
{
    public class BiddingItemsController : Controller
    {
        private ApplicationDbContext db = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();

        // GET: BiddingItems
        public ActionResult Index()
        {
            var biddingItems = db.BiddingItems.Include(b => b.Sub).Include(b => b.User);
            return View(biddingItems.ToList());
        }

        // GET: BiddingItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bidItem = db.BiddingItems.Include(bidI => bidI.Sub).Where(bidI => bidI.BiddingItemID == (int)id);
            //BiddingItem biddingItem = db.BiddingItems.Find(id);
            if (bidItem == null)
            {
                return HttpNotFound();
            }
            return View(bidItem.FirstOrDefault());
        }

        // GET: BiddingItems/Create
        public ActionResult Create()
        {
            ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "SubCategoryName");
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Address");
            return View();
        }

        // POST: BiddingItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BiddingItemID,ItemName,ItemDescription,BidStartTime,BidEndTime,BidStartPrice,SubCategoryID")] BiddingItem biddingItem, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                biddingItem.UserId = userID;

                string imagePath = Server.MapPath("~/Images/" + file.FileName);
                file.SaveAs(imagePath);

                biddingItem.ItemImageURL = "Images/" + file.FileName;

                db.BiddingItems.Add(biddingItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "SubCategoryName");
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
            ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "SubCategoryName", biddingItem.SubCategoryID);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Address", biddingItem.UserId);
            return View(biddingItem);
        }

        // POST: BiddingItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BiddingItemID,ItemName,ItemDescription,ItemImageURL,SubCategoryID,BidStartTime,BidEndTime,BidStartPrice,CurrentPrice,UserId")] BiddingItem biddingItem, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string imagePath = Server.MapPath("~/Images/" + file.FileName);
                file.SaveAs(imagePath);

                biddingItem.ItemImageURL = "Images/" + file.FileName;

                db.BiddingItems.Add(biddingItem);
                db.SaveChanges();

                db.Entry(biddingItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "SubCategoryName", biddingItem.SubCategoryID);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Address", biddingItem.UserId);
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
