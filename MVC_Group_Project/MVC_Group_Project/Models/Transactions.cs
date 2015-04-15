using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Models
{
    public class Transactions
    {
        public int TransactionsID { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public Double Amount { get; set; }
        public int BiddingItemID { get; set; }
        [ForeignKey("BiddingItemID")]
        public BiddingItem BidItem { get; set; }
        public int CreditCardID { get; set; }
        [ForeignKey("CreditCardID")]
        public CreditCard Credcard { get; set; }
    }
}