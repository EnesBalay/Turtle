using Microsoft.AspNetCore.Mvc;
using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using BussinessLayer.ValidationRules;
using FluentValidation.Results;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;

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
        public IActionResult List()
        {
            var allMeetings = meetingManager.GetMeetingsByUserId(GetCurrentUser().UserID);
            foreach (var meeting in allMeetings)
            {
                meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
            }
            return View(allMeetings);
        }

        public IActionResult DisabledMeetings()
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
            meeting.Status = true;
            var newMeetingId = meetingManager.AddReturnId(meeting);
            string htmlBody = "<b>Toplantı Adı:</b>" + meeting.MeetingName + "<br/><b>Toplantı Konumu:</b>" + meeting.Location + "<br />" + "<b>Toplantı Detayı:</b>" + meeting.Description + "<br />Toplantıyı oylamak için <a href='http://localhost:5254/Meeting/VoteMeeting/" + meeting.MeetingID + "'>tıklayınız.</a>";
            foreach (var item in SendMails)
            {
                VoteMail mail = new VoteMail();
                mail.email = item;
                mail.status = true;
                mail.MeetingId = newMeetingId;
                VoteMailManager.Add(mail);
                sendToMail(item, meeting, htmlBody);
            }
            return Json(new { success = "true" });
        }

        public void sendToMail(string mail, Meeting meeting, string htmlBody)
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
            email.Body = new MessageBody(htmlBody);


            // E-postayı gönderin
            email.Send();
        }


        public IActionResult DeleteMeeting(int id)
        {
            var meetingValue = meetingManager.GetById(id);
            meetingValue.Status = false;
            meetingManager.Update(meetingValue);
            return RedirectToAction("List", "Meeting");
        }

        public IActionResult ActivateMeeting(int id)
        {
            var meetingValue = meetingManager.GetById(id);
            meetingValue.Status = true;
            meetingManager.Update(meetingValue);
            return RedirectToAction("DisabledMeetings", "Meeting");
        }



        [AllowAnonymous]
        public IActionResult VoteMeeting(int id = 0)
        {
            ViewBag.VoterMail = " ";
            ViewBag.VoteError = " ";
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
            ViewBag.VoterMail = " ";
            ViewBag.VoteError = " ";
            ViewBag.VoteError2 = " ";
            meeting = meetingManager.GetById(meeting.MeetingID);
            meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
            ViewBag.VoteError = "Mailiniz toplantıda kayıtlı değildir";
            foreach (var item in meeting.Mails)
            {
                if (item.email == voterMail)
                {
                    ViewBag.VoterMail = voterMail;
                    if (item.status == false)
                    {
                        ViewBag.VoteError2 = "Bu toplantıyı daha önce oyladınız. Tekrar oylayamazsınız!";
                        DateTime chosedDate = meeting.PlanningDate;
                        if (item.ChoosedDate == 1)
                        {
                            chosedDate = meeting.PlanningDate;
                        }
                        if (item.ChoosedDate == 2)
                        {
                            chosedDate = meeting.PlanningDate2;
                        }
                        if (item.ChoosedDate == 3)
                        {
                            chosedDate = meeting.PlanningDate3;
                        }
                        ViewBag.VoteDate = chosedDate;
                    }

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
                string htmlBody = "<b>Toplantı Adı:</b>" + meeting.MeetingName + "<br/><b>Toplantı Konumu:</b>" + meeting.Location + "<br />" + "<b>Toplantı Detayı:</b>" + meeting.Description + "<br />Toplantıyı oylamak için <a href='http://localhost:5254/Meeting/VoteMeeting/" + meeting.MeetingID + "'>tıklayınız.</a>";

                meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
                foreach (var m in meeting.Mails) { sendToMail(m.email, meeting, htmlBody); }
                meetingManager.Update(meeting);
                return Json(new { success = "true" });
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    if (item.ErrorMessage != "The value '' is invalid.")
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                    }
                }
                return Json(new { success = "false" });
            }
        }

        public IActionResult Detail(int id)
        {
            var meeting = meetingManager.GetById(id);
            meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
            return View(meeting);
        }
        [HttpGet]
        public IActionResult VoteAMeeting()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult VoteAMeeting(string email, int dateIndex, int meetingID)
        {
            var voteMeeting = VoteMailManager.GetVoteMailByMail(email, meetingID);
            voteMeeting.ChoosedDate = dateIndex;
            voteMeeting.status = false;
            VoteMailManager.Update(voteMeeting);
            return Json(new { success = "true" });
        }

        public IActionResult FinalizeMeeting(int meetingId, string dateTime)
        {
            var meeting = meetingManager.GetById(meetingId);
            meeting.Mails = VoteMailManager.GetVoteMailsByMeetingId(meeting.MeetingID);
            meeting.FinalizeDate = Convert.ToDateTime(dateTime);
            meetingManager.Update(meeting);
            string htmlBody = "<b>Toplantı Adı: </b>" + meeting.MeetingName + "<br/><b>Toplantı Konumu: </b>" + meeting.Location + "<br />" + "<b>Toplantı Detayı: </b>" + meeting.Description + "<br /><h3 style='color:green;'>Toplantı oylaması tamamlanmıştır.</h3><b>Belirlenen tarih ve saat: </b>" + meeting.FinalizeDate;
            foreach (var mail in meeting.Mails)
            {
                sendToMail(mail.email, meeting, htmlBody);
            }
            return Redirect("/Meeting/Detail/" + meetingId);
        }

    }
}
