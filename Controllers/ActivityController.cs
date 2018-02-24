using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using beltexam1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace beltexam1.Controllers
{
    public class ActivityController : Controller
    {
        private BeltExam1Context _context;
 
        public ActivityController(BeltExam1Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        private User ActiveUser
        {
            get{return _context.users.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();}
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User user = _context.users.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
            ViewBag.UserInfo = user;
            
            List<Activity> showActivities = _context.Activities.Include(j => j.JoinActivities).ToList();
            ViewBag.AllActivities = showActivities;
            return View("Dashboard");

        }

        [HttpGet]
        [Route("/newactivity")]
        public IActionResult NewActivity()
        {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");

            ViewBag.UserInfo = ActiveUser;
            return View("NewActivity");
        }

        [HttpPost]
        [Route("AddActivity")]
        public IActionResult AddActivity(AddActivity activity)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            
            if(ModelState.IsValid)
            {
                Activity Activity = new Activity
                {
                    UserId = ActiveUser.id,
                    ActivityName = activity.ActivityName,
                    DateOfActivity = activity.DateOfActivity,
                    Time = activity.Time,
                    Duration = activity.Duration,
                    Description = activity.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                };
                _context.Activities.Add(Activity);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ViewBag.UserInfo = ActiveUser;
            return View("NewActivity");
        }

        [HttpGet]
        [Route("activity/{ActivityId}")]
        public IActionResult ShowActivity(int ActivityId)
        {
            if(ActiveUser == null){
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ShowOne = _context.Activities.Where(o => o.ActivityId == ActivityId).Include(g => g.JoinActivities).ThenInclude(h => h.User).SingleOrDefault();
            ViewBag.UserInfo = ActiveUser;
            return View("ShowActivity");

        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int ActivityId)
        {
            if(ActiveUser == null){
                return RedirectToAction("Index", "Home");
            }
            Activity ToDelete = _context.Activities.SingleOrDefault(ShowOne => ShowOne.ActivityId == ActivityId);
            _context.Activities.Remove(ToDelete);
            _context.SaveChanges();

            ViewBag.UserInfo = ActiveUser;
            List<Activity> Activities = _context.Activities.ToList();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [Route("joinActivity")]
        public IActionResult joinActivity(int ActivityId)
        {
            JoinActivity addJoin = new JoinActivity
            {
                UserId = (int)HttpContext.Session.GetInt32("id"),
                ActivityId = ActivityId
            };
            _context.JoinActivities.Add(addJoin);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [Route("UnjoinActivity")]
        public IActionResult UnjoinActivity(int ActivityId)
        {
            JoinActivity RemoveJoin = _context.JoinActivities.SingleOrDefault(x => x.ActivityId == ActivityId && x.UserId == (int)HttpContext.Session.GetInt32("id"));
            _context.JoinActivities.Remove(RemoveJoin);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");

        }
        

    }
}
