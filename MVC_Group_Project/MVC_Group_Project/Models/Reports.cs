using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Models
{
    public class Reports
    {
        public int ReportsID { get; set; }
        public IEnumerable<BiddingItem> BidItems { get; set; }
        public IEnumerable<OnGoingBids> LiveBids { get; set; }
        public IEnumerable<Transactions> Purchases { get; set; }
    }
    
}