using Microsoft.AspNetCore.Mvc;
using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using BussinessLayer.ValidationRules;
using FluentValidation.Results;

namespace Turtle.Controllers

{
    public class MeetingController : Controller
    {
        MeetingManager meetingManager=new MeetingManager(new EfMeetingRepository());
        UserManager userManager = new UserManager(new EfUserRepository());
        public IActionResult Index()
        {
            var allMeetings = meetingManager.GetListAll();
            return View(allMeetings);
        }
        public IActionResult AddMeeting()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMeeting(Meeting newMeeting)
        {
            var userValues = userManager.GetUserByIdentityName(User.Identity.Name);
            MeetingValidator uv = new MeetingValidator();
            ValidationResult results = uv.Validate(newMeeting);
            if (results.IsValid)
            {
                Meeting meeting = new Meeting();
                meeting.MeetingName = newMeeting.MeetingName;
                meeting.MeetingDuration = newMeeting.MeetingDuration;
                meeting.CreateDate = DateTime.Now;
                meeting.PlanningDate = newMeeting.PlanningDate;
                meeting.PlanningDate2 = newMeeting.PlanningDate2;
                meeting.PlanningDate3 = newMeeting.PlanningDate3;
                meeting.Location = newMeeting.Location;
                meeting.Description = newMeeting.Description;
                meeting.UserID = userValues.UserID;
                meetingManager.Add(meeting);
                ViewBag.MeetingSuccess = "Toplantı oluşturuldu.";
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        public IActionResult DeleteMeeting(int id)
        {
            var meetingValue = meetingManager.GetById(id);
            meetingManager.Remove(meetingValue);
            return RedirectToAction("Index", "Meeting");
        }
        public IActionResult VoteMeeting(int id=0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var meeting = meetingManager.GetById(id);
         
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
         
        }
        public IActionResult EditMeeting(int id)
        {
            var meeting = meetingManager.GetById(id);

            return View(meeting);

        }
        [HttpPost]
        public IActionResult EditMeeting(Meeting updatedValues)
        {
            MeetingValidator uv = new MeetingValidator();
            ValidationResult results = uv.Validate(updatedValues);
            
            if (results.IsValid)
            {
                updatedValues.CreateDate = DateTime.Now;
                meetingManager.Update(updatedValues);
                ViewBag.MeetingSuccess = "Toplantı düzenlendi.";
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();




        }

        public IActionResult VoteChoose(int p)
        {
            

            return View(p);

        }
    }
}
