using MVC_Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace MVC_Group_Project.Controllers
{
    public class BidController : Controller
    {
        private ApplicationDbContext db = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();

        // GET: BiddingPage
        public ActionResult BidNow(int? id)
        {
            var model = new ModelsForBiddingPage();

            if (id == null || id == 0)
            {
                Redirect("../Home/Index");
            }
            else
            {
                var totalbidders = db.OnGoingBids.Where(x => x.BiddingItemID == id).Count(); // used lambda expression to count all the bidders in the item
                model.TotalBidders = totalbidders;
                model.bItem = db.BiddingItems.Single(item => item.BiddingItemID == id); // used lambda expression to find the specific item in database to return the values in the model.
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BidNow(ModelsForBiddingPage bid, int id)
        {
            if (bid != null)
            {
                var userID = User.Identity.GetUserId();
                var ifUserHasCreditCard = db.CreditCards.Where(c => c.UserId == userID).ToList();
                if (ifUserHasCreditCard.Count == 0)
                {
                    ViewData["IsApproved"] = "NoCreditCard";
                    bid.bItem = db.BiddingItems.Single(item => item.BiddingItemID == id);
                }
                else
                {
                    var noBidOnItem = db.OnGoingBids.Where(o => o.BiddingItemID == id);
                    if (db.OnGoingBids.Count() == 0 || noBidOnItem.Count() == 0)
                    {
                        if (bid.onGB.BidPrice > db.BiddingItems.Single(a => a.BiddingItemID == id).BidStartPrice)
                        {
                            bid.onGB.BiddingItemID = id;
                            bid.onGB.BidTime = DateTime.Now;
                            bid.onGB.UserId = userID;

                            bid.bItem = db.BiddingItems.Single(item => item.BiddingItemID == id);
                            bid.bItem.HighestBidPrice = bid.onGB.BidPrice;

                            db.OnGoingBids.Add(bid.onGB);
                            db.SaveChanges();

                            ViewData["IsApproved"] = "true";
                        }
                        else
                        {
                            ViewData["IsApproved"] = "LowBid";
                        }
                        //var highestBid = db.OnGoingBids.Where(x => x.BiddingItemID == id).OrderByDescending(y => y.BidPrice);
                        //ViewBag.HighestBid = highestBid.ToList();
                    }
                    else
                    {
                        var oldBidder = db.OnGoingBids.OrderByDescending(l => l.OnGoingBidsID).Where(m => m.BiddingItemID == id);

                        if (bid.onGB.BidPrice > oldBidder.FirstOrDefault().BidPrice)
                        {
                            bid.onGB.BiddingItemID = id;
                            bid.onGB.BidTime = DateTime.Now;
                            bid.onGB.UserId = userID;

                            bid.bItem = db.BiddingItems.Single(item => item.BiddingItemID == id);
                            bid.bItem.HighestBidPrice = bid.onGB.BidPrice;
                            db.OnGoingBids.Add(bid.onGB);
                            db.SaveChanges();

                            ViewData["IsApproved"] = "true";
                        }
                        else
                        {
                            ViewData["IsApproved"] = "LowBid";
                            //bid.bItem.HighestBidPrice = oldBidPrice.FirstOrDefault().BidPrice;
                        }
                    }

                    var totalbidders = db.OnGoingBids.Where(x => x.BiddingItemID == id).Count();
                    bid.TotalBidders = totalbidders;

                    bid.bItem = db.BiddingItems.Single(item => item.BiddingItemID == id);
                }

            }

            else
            {
                Redirect("../Home/Index");
            }
            return View(bid);
        }
    }
}


//try
//{
                
//}
//catch (DbEntityValidationException dbEx)
//{
//    foreach (var validationErrors in dbEx.EntityValidationErrors)
//    {
//        foreach (var validationError in validationErrors.ValidationErrors)
//        {
//            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
//        }
//    }
//    throw;
//}