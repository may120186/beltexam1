using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using beltexam1.Models;

namespace beltexam1.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<JoinActivity> JoinActivities {get; set; }

        public User()
        {
            JoinActivities = new List<JoinActivity>();
        }
    }
}