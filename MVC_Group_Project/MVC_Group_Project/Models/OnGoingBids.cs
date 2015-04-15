using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Models
{
    public class OnGoingBids
    {
        public int OnGoingBidsID { get; set; }

        [MaxLength(120), ForeignKey("User")]
        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

        public int BiddingItemID { get; set; }

        [ForeignKey("BiddingItemID")]
        public BiddingItem SelectedItem { get; set; }

        public Decimal BidPrice { get; set; }

        public DateTime BidTime { get; set; }
    }
}