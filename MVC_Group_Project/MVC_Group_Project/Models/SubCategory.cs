using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Models
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string ImagePath { get; set; }        
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category cat;
    }
}