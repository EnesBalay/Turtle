using Microsoft.AspNetCore.Mvc;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Turtle.Controllers

{
    public class MeetingController : Controller
    {
        MeetingManager meetingManager=new MeetingManager(new EfMeetingRepository());
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(Meeting newMeeting)
        {
            Meeting meeting = new Meeting();
            meeting.MeetingName = meeting.MeetingName;
            meeting.MeetingDuration = meeting.MeetingDuration;
            meeting.CreateDate = DateTime.Now;
            meeting.PlanningDate = meeting.PlanningDate;
            meeting.Location = meeting.Location;
            meeting.UserID = 1;
            meetingManager.Add(meeting);
            ViewBag.MeetingSuccess = "Toplantı oluşturuldu.";
            return View();
            
           
        }

 public IActionResult AddMeeting()
        {
            return View();
        }
    }
}
