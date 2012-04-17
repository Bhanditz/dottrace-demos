using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers 
{
    public class SeedDataController : Controller
    {
        public ActionResult Index(int dinnerCount = 100) {
            const string name = "Nerd";
            var membershipService = new AccountMembershipService();
            if(membershipService.ValidateUser(name, "password") == false) {
                membershipService.CreateUser(name, "password", "nerd@example.com");
            }
            var repo = new DinnerRepository();
            foreach(var d in repo.All) {
                repo.Delete(d.DinnerID);
            }
            for (var i = 0; i < dinnerCount; i++) {
                var dinner = new Dinner {Title = "Nerd-Out",
                                         Description = "Nerding out with the nerds",
                                         EventDate = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0)),
                                         ContactPhone = "403-999-9999",
                                         Address = "Calgary, AB", 
                                         Country = "Canada", 
                                         HostedById = name, 
                                         HostedBy = name};
                var rsvp = new RSVP {AttendeeNameId = name, AttendeeName = name};
                dinner.RSVPs = new List<RSVP> {rsvp};
                repo.InsertOrUpdate(dinner);
            }
            try {
                repo.Save();
            }
            catch(DbEntityValidationException e) {
                var error = e.EntityValidationErrors.First().ValidationErrors.First();
                return new ContentResult {Content = string.Format("{0}: {1}", error.PropertyName, error.ErrorMessage)};
            }
            return new ContentResult{Content = "Success"};
        }
    }
}
