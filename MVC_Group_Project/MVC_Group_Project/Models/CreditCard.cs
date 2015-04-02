﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Models
{
    public class CreditCard
    {
        public int CreditCardID { get; set; }
        public string CardType { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CSC { get; set; }

        //public int UserID { get; set; }
        //[ForeignKey("UserID")]
        //public 
    }
}