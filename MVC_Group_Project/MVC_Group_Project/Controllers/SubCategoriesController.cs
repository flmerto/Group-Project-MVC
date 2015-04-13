using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Group_Project.Models;
using MVC_Group_Project.Filters;

namespace MVC_Group_Project.Controllers
{
    [CustomAuthorization(Role = "Admin")] 
    public class SubCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubCategories
        public ActionResult Index()
        {
            var subCategories = db.SubCategories.Include(s => s.cat);
            return View(subCategories.ToList());
        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subcat = db.SubCategories.Include(subc => subc.cat).Where(subc => subc.SubCategoryID == (int)id);
            if (subcat == null)
            {
                return HttpNotFound();
            }
            return View(subcat.FirstOrDefault());
        }

        // GET: SubCategories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCategoryID,SubCategoryName,CategoryID")] SubCategory subCategory, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string imagePath = Server.MapPath("~/Images/" + file.FileName);
                file.SaveAs(imagePath);

                subCategory.ImagePath = "Images/" + file.FileName;

                db.SubCategories.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubCategoryID,SubCategoryName,ImagePath,CategoryID")] SubCategory subCategory, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string imagePath = Server.MapPath("~/Images/" + file.FileName);
                file.SaveAs(imagePath);

                subCategory.ImagePath = "Images/" + file.FileName;

                db.Entry(subCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subCategory);
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
