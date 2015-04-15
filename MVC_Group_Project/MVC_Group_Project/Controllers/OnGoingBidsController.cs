﻿using System;
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
    public class OnGoingBidsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OnGoingBids
        public ActionResult Index()
        {
            var onGoingBids = db.OnGoingBids.Include(o => o.ApplicationUser).Include(o => o.SelectedItem);
            return View(onGoingBids.ToList());
        }

        // GET: OnGoingBids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnGoingBids onGoingBids = db.OnGoingBids.Find(id);
            if (onGoingBids == null)
            {
                return HttpNotFound();
            }
            return View(onGoingBids);
        }

        // GET: OnGoingBids/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Address");
            ViewBag.BiddingItemID = new SelectList(db.BiddingItems, "BiddingItemID", "ItemName");
            return View();
        }

        // POST: OnGoingBids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OnGoingBidsID,UserId,BiddingItemID,BidPrice,BidTime")] OnGoingBids onGoingBids)
        {
            if (ModelState.IsValid)
            {
                db.OnGoingBids.Add(onGoingBids);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Address", onGoingBids.UserId);
            ViewBag.BiddingItemID = new SelectList(db.BiddingItems, "BiddingItemID", "ItemName", onGoingBids.BiddingItemID);
            return View(onGoingBids);
        }

        // GET: OnGoingBids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnGoingBids onGoingBids = db.OnGoingBids.Find(id);
            if (onGoingBids == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Address", onGoingBids.UserId);
            ViewBag.BiddingItemID = new SelectList(db.BiddingItems, "BiddingItemID", "ItemName", onGoingBids.BiddingItemID);
            return View(onGoingBids);
        }

        // POST: OnGoingBids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OnGoingBidsID,UserId,BiddingItemID,BidPrice,BidTime")] OnGoingBids onGoingBids)
        {
            if (ModelState.IsValid)
            {
                db.Entry(onGoingBids).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Address", onGoingBids.UserId);
            ViewBag.BiddingItemID = new SelectList(db.BiddingItems, "BiddingItemID", "ItemName", onGoingBids.BiddingItemID);
            return View(onGoingBids);
        }

        // GET: OnGoingBids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnGoingBids onGoingBids = db.OnGoingBids.Find(id);
            if (onGoingBids == null)
            {
                return HttpNotFound();
            }
            return View(onGoingBids);
        }

        // POST: OnGoingBids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OnGoingBids onGoingBids = db.OnGoingBids.Find(id);
            db.OnGoingBids.Remove(onGoingBids);
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
