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
            meeting.MeetingName = newMeeting.MeetingName;
            meeting.MeetingDuration = newMeeting.MeetingDuration;
            meeting.CreateDate = DateTime.Now;
            meeting.PlanningDate = newMeeting.PlanningDate;
            meetingManager.Add(newMeeting);
            ViewBag.MeetingSuccess = "Toplantı oluşturuldu.";
            return View();
            
           
        }


    }
}
