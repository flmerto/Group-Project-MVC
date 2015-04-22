using MVC_Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using MVC_Group_Project.Filters;


namespace MVC_Group_Project.Controllers
{
    [CustomAuthorization(Role = "Admin")]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        
        // GET: Transactions
        public ActionResult Index()
        {
            var expiredBiddingItems = db.BiddingItems.Where(b => b.BidEndTime >= DateTime.Now).ToList();

            if (expiredBiddingItems.Count() == 0)
            {
                ViewData["UpdateStatus"] = "There are no expired items to update.";
                return View();
            }
            else
            {
                for (int i = 0; i < expiredBiddingItems.Count; i++)
                {
                    var expiredBI = expiredBiddingItems[i];
                    var itemThatIsAlreadySold = db.Transactions.Where(k => k.BiddingItemID == expiredBI.BiddingItemID).ToList();
                    if (expiredBI.HighestBidPrice == 0)
                    {
                        // will remove the item from the list if it doesnt have any bids
                        expiredBiddingItems.RemoveAt(i);
                        i--; // will decrease the i count to match the current spot on the expiredBiddingItems
                    }
                    else
	                {
                        if (itemThatIsAlreadySold.Count == 0)
                        {
                            break; // will get out of the loop if the item has not been yet sold
                        }
                        else
                        {
                            expiredBiddingItems.RemoveAt(i);
                            i--;
                        }
	                }
                    
                }

                if (expiredBiddingItems.Count() == 0)
                {
                    ViewData["UpdateStatus"] = "All items has been sold.";
                    return View();
                }
                else
                {
                    for (int a = 0; a < expiredBiddingItems.Count; a++) // a = expiredBiddingItems
                    {
                        var expiredBI = expiredBiddingItems[a];
                        if (expiredBI.HighestBidPrice == 0)
                        {
                            // left blank so the item with no bids cant get past through transaction
                        }
                        else
                        {
                            var itemWithTheHighestBid = db.OnGoingBids.Where(p => p.BidPrice != 0).Where(q => q.BiddingItemID == expiredBI.BiddingItemID).OrderByDescending(r => r.BidPrice).ToList();
                            var userID = itemWithTheHighestBid.FirstOrDefault().UserId;

                            var creditCard = db.CreditCards.Single(cred => cred.UserId == userID);

                            if (itemWithTheHighestBid.Count == 0)
                            {
                                ViewData["UpdateStatus"] = "There are no items that have bids.";
                            }
                            else
                            {
                                Transactions tr = new Transactions();
                                tr.CreditCardID = creditCard.CreditCardID;
                                tr.BiddingItemID = itemWithTheHighestBid.FirstOrDefault().BiddingItemID;
                                tr.Amount = Convert.ToDouble(itemWithTheHighestBid.FirstOrDefault().BidPrice);
                                tr.PurchaseDateTime = expiredBI.BidEndTime;

                                db.Transactions.Add(tr);
                                db.SaveChanges();

                                if (expiredBiddingItems.Count == a)
                                {
                                    ViewData["UpdateStatus"] = "Items Sold/Expired";
                                }
                            }
                        }
                    }
                }
            }

            return View();
        }
    }
}