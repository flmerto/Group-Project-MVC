using MVC_Group_Project.Filters;
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


        public int SubCategoryID { get; set; }

        [ForeignKey("SubCategoryID")]
        public SubCategory Sub { get; set; }

        [DataType(DataType.Date)]
        [CurrentDateAttibute]
        [Required(ErrorMessage="Date field mandatory!")]
     
        public DateTime BidStartTime { get; set; }
        
        [DataType(DataType.Date)]
        [CurrentDateAttibute]
        [Required(ErrorMessage = "Date field mandatory!")]
     
        public DateTime BidEndTime { get; set; }

        [DataType(DataType.Date)]
        public string StartAndEnd
        {
            get
            {
                return string.Format("{0},{1}", this.BidStartTime, this.BidEndTime);
            }
        }

        [DataType(DataType.Currency)]
        [Required]
        public decimal BidStartPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal HighestBidPrice { get; set; }

        [MaxLength(120), ForeignKey("User")]
        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

    }
}