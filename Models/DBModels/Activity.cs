using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using beltexam1.Models;

namespace beltexam1.Models
{
    public class Activity : BaseEntity
    {
        [Key]
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public string ActivityName { get; set; }
        public DateTime DateOfActivity { get; set; }
        public DateTime Time { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }

        public List<JoinActivity> JoinActivities {get; set; }

        public Activity()
        {
            JoinActivities = new List<JoinActivity>();
        }
    }
}