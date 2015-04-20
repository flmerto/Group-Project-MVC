﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Filters
{
   
        public class CurrentDateAttibute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime dateTime = Convert.ToDateTime(value);
                return dateTime >= DateTime.Now.AddDays(-1);

            }

        }
    
}