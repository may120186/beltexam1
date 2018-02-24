using Microsoft.EntityFrameworkCore;


namespace beltexam1.Models
{
    public class BeltExam1Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltExam1Context(DbContextOptions<BeltExam1Context> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<JoinActivity> JoinActivities { get; set; }
        
    }

 
}
