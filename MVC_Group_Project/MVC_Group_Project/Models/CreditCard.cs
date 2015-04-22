using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Group_Project.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Date)]
        [CurrentDateAttibute]
        [Required(ErrorMessage = "Date Error!")]
        public DateTime ExpiryDate { get; set; }
        public int CSC { get; set; }

        [MaxLength(120), ForeignKey("User")]
        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

    }
}