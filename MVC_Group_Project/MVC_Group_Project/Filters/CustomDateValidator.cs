using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Group_Project.Filters
{
    public class CustomDateValidator : IValidatableObject
    {
        [Required(ErrorMessage = "Date is Required!")]
        [DataType(DataType.Date)]
        public System.DateTime StartDate { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate >= DateTime.Today)
                yield return new ValidationResult("Date should be one day ahead!", new[] { "StartDate" });
        }
    }
}