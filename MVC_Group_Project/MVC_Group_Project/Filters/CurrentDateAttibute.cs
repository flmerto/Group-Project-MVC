using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Filters
{
   
        public class CurrentDateAttibute : ValidationAttribute
        {
            public override bool IsValid(object date)
            {
                if (Convert.ToDateTime(date) >= DateTime.Today || Convert.ToDateTime(date) == DateTime.Today)
                {
                    DateTime startdateTime = Convert.ToDateTime(date);
                    return startdateTime >= DateTime.Now.AddDays(-1);    
                }
                else
                {
                    DateTime enddateTime = Convert.ToDateTime(date);
                    return enddateTime <= DateTime.Now.AddDays(-1);

                }
                
            }

        }
    
}