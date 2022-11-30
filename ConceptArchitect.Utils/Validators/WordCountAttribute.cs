using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.Utils.Validators
{
    public  class WordCountAttribute: ValidationAttribute
    {
        public int Minimum { get; set; } = 1;

        public int Maximum { get; set; } = 0;// 0 means no limit

        public override bool IsValid(object? value)
        {

            var text = value as string;

            //I need to check if text is there how many words exists
            //I will not do the job of [Required]
            //I will ignore it
            if (string.IsNullOrEmpty(text))
                return true;


            var words = text.Split(' ');

            if (words.Length < Minimum)
                return false;
            if (Maximum > 0 && words.Length > Maximum)
                return false;

            return true; //valid
            
        }      
    }
}
