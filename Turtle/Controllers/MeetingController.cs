﻿using Microsoft.AspNetCore.Mvc;
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
        VoteMailManager VoteMailManager = new VoteMailManager(new EfVoteMailRepository());
        public IActionResult Index()
        {
            var allMeetings = meetingManager.GetMeetingsByUserId(GetCurrentUser().UserID);
            foreach (var meeting in allMeetings)
            {
                meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
            }
            return View(allMeetings);
        }
        public User GetCurrentUser()
        {
            return userManager.GetUserByIdentityName(User.Identity.Name);
        }
        public IActionResult AddMeeting()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMeeting(Meeting newMeeting, string[] SendMails)
        {
            var userValues = GetCurrentUser();
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
                var newMeetingId = meetingManager.AddReturnId(meeting);
                foreach (var item in SendMails)
                {
                    VoteMail mail = new VoteMail();
                    mail.email = item;
                    mail.status = true;
                    mail.MeetingId = newMeetingId;
                    VoteMailManager.Add(mail);
                    //sendToMail(item);
                }
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

        public void sendToMail(string mail)
        {

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
            meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
            return View(meeting);
        }
        [HttpPost]
        public IActionResult EditMeeting(Meeting updatedValues)
        {
            MeetingValidator uv = new MeetingValidator();
            ValidationResult results = uv.Validate(updatedValues);
            var meeting = meetingManager.GetById(updatedValues.MeetingID);
            meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
            if (results.IsValid)
            {
                
                meeting.MeetingDuration = updatedValues.MeetingDuration;
                if (updatedValues.PlanningDate!=null)
                {
                    meeting.PlanningDate = updatedValues.PlanningDate;
                } 
                if (updatedValues.PlanningDate2!=null)
                {
                    meeting.PlanningDate2 = updatedValues.PlanningDate2;

                } 
                if (updatedValues.PlanningDate3!=null)
                {
                    meeting.PlanningDate3 = updatedValues.PlanningDate3;
                }
                meeting.Description = updatedValues.Description;    
                meeting.MeetingName = updatedValues.MeetingName;
                meeting.Location = updatedValues.Location;
                meeting.Mails = updatedValues.Mails;
                meetingManager.Update(meeting);
                ViewBag.MeetingSuccess = "Toplantı düzenlendi.";
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(meeting);
        }

        public IActionResult VoteChoose(int p)
        {
            return View(p);
        }
    }
}
