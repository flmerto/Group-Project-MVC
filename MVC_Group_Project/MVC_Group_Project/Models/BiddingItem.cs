using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Group_Project.Models
{
    public class BiddingItem
    {
        
        public int BiddingItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string ItemDescription { get; set; }
        public string ItemImageURL { get; set; }


        [Required]
        public DateTime BidStartTime { get; set; }
        [Required]
        public DateTime BidEndTime { get; set; }

        public string StartAndEnd
        {
            get
            {
                return string.Format("{0},{1}", this.BidStartTime, this.BidEndTime);
            }
        }

        [Required]
        public decimal BidStartPrice { get; set; }
        [Required]
        public decimal CurrentPrice { get; set; }
    }
}