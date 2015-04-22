using MVC_Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;

namespace MVC_Group_Project.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        // GET: Reports
        public ActionResult Index()
        {
            Reports bi = new Reports();

            bi.LiveBids = db.OnGoingBids.GroupBy(o => o.BiddingItemID)
                  .Select(g => g.OrderByDescending(o => o.BidPrice)
                                .FirstOrDefault()
                   );

            //bi.LiveBids = db.OnGoingBids.ToList();
            bi.BidItems = db.BiddingItems.ToList();
            bi.Purchases = db.Transactions.ToList();
            return View(bi);
        }
    }
}