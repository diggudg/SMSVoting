
using SMS.Hubs;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMS.Controllers
{
    public class ValuesController : ApiController
    {
        SMSReader db = new SMSReader();
        JobInfoRepository objRepo = new JobInfoRepository();
        [HttpGet]
        [Route("sms")]
        public IHttpActionResult SMSrecieved(string mobile, string number, string message)
        {
            var smsreader = new MstSM
            {
                Sender = mobile,
                Receiver = number,
                Message = message.Replace("DEMO ","").Replace("demo ","")
            };

            db.MstSMS.Add(smsreader);
            db.SaveChanges();
            FinalVotingHub.Show();
            return Ok();
        }
        [HttpPost]
        [Route("getsms")]
        public void getSMS (SMSprop sms)
        {
            var checkDuplcates = db.MstSMS.Where(m => m.Sender == sms.sender).ToList().Count();
            if(checkDuplcates!=0)
            {

            }
            else
            {
                var smsreader = new MstSM
                {
                    Sender = sms.sender,
                    Receiver = sms.inNumber,
                    Message = sms.content.ToUpper().Replace("VOTE ","").Replace("VOTE","").Trim(),
                    Keyword = sms.keyword.ToUpper()
                };

                db.MstSMS.Add(smsreader);
                db.SaveChanges();
            }
            
            FinalVotingHub.Show();
        }
        [Route("Live")]
        public IEnumerable<FinalVoting> Get(string Band)
        {
           return objRepo.GetData(Band);
           // return objRepo.GetJob();
        }
        [Route("StartVoting")]
        [HttpPost]
        public IHttpActionResult UpdateTime(string StartDate, string EndDate)
        {
            var dat = db.MstSMSSetups.Where(d => d.Id == 1).ToList().FirstOrDefault();
            dat.SMSStartTime =Convert.ToDateTime(StartDate);
            dat.SMSEndTime = Convert.ToDateTime(EndDate);
           db.Entry(dat).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var updated=dat = db.MstSMSSetups.Where(d => d.Id == 1).ToList().FirstOrDefault();
            return Ok(dat);
        }
        [Route("winners")]
        [HttpPost]
        public IHttpActionResult winner(string band)
        {
            var results = db.EmpKeywordMaps.Where(b => b.Band == band).ToList().OrderByDescending(v=>v.Votes);
            var winner = results.Take(3);
            return Ok(winner);
        }
    }

    public class SMSprop
    {
        public string inNumber { get; set; }
        public string sender { get; set; }
        public string keyword { get; set; }
        public string content { get; set; }
        public string email { get; set; }
        public string credits { get; set; }
    }
    public class UpdateTime
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
