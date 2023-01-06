using Microsoft.AspNetCore.Mvc;
using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using BussinessLayer.ValidationRules;
using FluentValidation.Results;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.AspNetCore.Authorization;

namespace Turtle.Controllers

{
    public class MeetingController : Controller
    {
        MeetingManager meetingManager = new MeetingManager(new EfMeetingRepository());
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
                    sendToMail(item, meeting);
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

        public void sendToMail(string mail, Meeting meeting)
        {
            var mailList = mail.Split(';').ToList();
            // Outlook'a erişmek için ExchangeService nesnesi oluşturun ve gerekli bilgileri doldurun
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
            service.Credentials = new WebCredentials("turtledou@outlook.com", "123Turtle123");
            service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

            // E-posta nesnesini oluşturun ve gerekli bilgileri doldurun
            EmailMessage email = new EmailMessage(service);
            foreach (string mailAddress in mailList)
            {
                email.ToRecipients.Add(mailAddress);
            }
            email.Subject = "Turtle";
            email.Body = new MessageBody("<b>Toplantı Adı:</b>" + meeting.MeetingName + "<br/><b>Toplantı Konumu:</b>" + meeting.Location + "<br />" + "<b>Toplantı Detayı:</b>" + meeting.Description + "");


            // E-postayı gönderin
            email.Send();
        }


        public IActionResult DeleteMeeting(int id)
        {
            var meetingValue = meetingManager.GetById(id);
            meetingManager.Remove(meetingValue);
            return RedirectToAction("Index", "Meeting");
        }
        [AllowAnonymous]
        public IActionResult VoteMeeting(int id = 0)
        {
            ViewBag.VoterMail = null;
            ViewBag.VoteError = null;
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
        [AllowAnonymous]
        [HttpPost]
        public IActionResult VoteMeeting(Meeting meeting, string voterMail)
        {

            meeting = meetingManager.GetById(meeting.MeetingID);
            meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
            ViewBag.VoteError = "Mailiniz toplantıda kayıtlı değildir";
            foreach (var item in meeting.Mails)
            {
                if (item.email == voterMail)
                {
                    ViewBag.VoterMail = voterMail;
                    ViewBag.VoteError=null;
                    break;
                }
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
        public IActionResult EditMeeting(Meeting updatedValues, string[] SendMails)
        {
            MeetingValidator uv = new MeetingValidator();
            var meeting = meetingManager.GetById(updatedValues.MeetingID);

            meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);

            meeting.MeetingDuration = updatedValues.MeetingDuration;
            if (updatedValues.PlanningDate.Year != 1)
            {
                meeting.PlanningDate = updatedValues.PlanningDate;
            }

            if (updatedValues.PlanningDate2.Year != 1)
            {
                meeting.PlanningDate2 = updatedValues.PlanningDate2;
            }
            if (updatedValues.PlanningDate3.Year != 1)
            {
                meeting.PlanningDate3 = updatedValues.PlanningDate3;
            }
            meeting.Description = updatedValues.Description;
            meeting.MeetingName = updatedValues.MeetingName;
            meeting.Location = updatedValues.Location;
            ValidationResult results = uv.Validate(meeting);
            if (results.IsValid)
            {
                foreach (var item in meeting.Mails)
                {
                    VoteMailManager.Remove(item);
                }

                foreach (var sendItem in SendMails)
                {
                    VoteMail mail = new VoteMail();
                    mail.email = sendItem;
                    mail.status = true;
                    mail.MeetingId = meeting.MeetingID;

                    VoteMailManager.Add(mail);
                }
                meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
                foreach (var m in meeting.Mails) { sendToMail(m.email, meeting); }
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
