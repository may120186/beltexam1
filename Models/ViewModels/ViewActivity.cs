using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using beltexam1.Models;

namespace beltexam1.Models
{
    public class AddActivity : BaseEntity
    {
        [Display(Name = "Title")]
        [Required]
        [MinLength(2)]
        public string ActivityName { get; set; }

        [Required]
        [Display(Name = "Date")]
        [MyDate(ErrorMessage = "Date must be in the future")]
        public DateTime DateOfActivity { get; set; }

        [Required]
        [Display(Name = "Time")]
        [MyTime(ErrorMessage = "Time must be in the future")]
        public DateTime Time { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MinLength(10)]
        public string Description { get; set; }
    }

    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }

    public class MyTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            
            return d >= DateTime.Now;
        }
    }
}