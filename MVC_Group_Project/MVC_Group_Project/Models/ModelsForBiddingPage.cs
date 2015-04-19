using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Models
{
    public class ModelsForBiddingPage
    {
        public OnGoingBids onGB { get; set; }
        public BiddingItem bItem { get; set; }
        public int TotalBidders { get; set; }
    }
}