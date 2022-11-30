using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.Utils.Validators
{
    public class PastDateAttribute : ValidationAttribute
    {
        public int DaysInPast { get; set; } = 1;
        public override bool IsValid(object? value)
        {
            if (!(value is DateTime))
                return true; //I don't handle other types

            var date= (DateTime)value;

            

            var today =  DateTime.Now;

            var minDate = today.AddDays(-DaysInPast);

            if(date>minDate)
            {
                ErrorMessage = $"Date Must be on or beore {minDate.ToShortDateString()}";
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
