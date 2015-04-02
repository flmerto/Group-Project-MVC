using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Models
{
    public class BiddingItem
    {
        public int BiddingItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImageURL { get; set; }

        public DateTime BidStartTime { get; set; }
        public DateTime BidEndTime { get; set; }

        public decimal BidStartPrice { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}