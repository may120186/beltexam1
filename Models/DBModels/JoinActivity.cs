using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using beltexam1.Models;

namespace beltexam1.Models
{
    public class JoinActivity : BaseEntity
    {
        [Key]
        public int JoinId {get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

    }
}