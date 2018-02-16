using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bank_accounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank_accounts.Controllers {
    public class HomeController : Controller {
        private BAContext _context;

        public HomeController ([FromServices] BAContext context) {
            _context = context;
        }

        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            return View ();
        }

        [HttpPost]
        [Route ("register")]
        public IActionResult Register (RegisterViews ruser) {
            if (ModelState.IsValid) {
                List<User> isuser = _context.users.Where (useri => useri.Email == ruser.Email).ToList ();
                if (isuser.Count () > 0) {
                    string message = "There is already another user with this email!Please use other email!";
                    ViewBag.message = message;
                    return View ("Index");
                }
                User NewUser = new User {
                    FirstName = ruser.FirstName,
                    LastName = ruser.LastName,
                    Email = ruser.Email,
                    Password = ruser.Password
                };
                _context.users.Add (NewUser);
                _context.SaveChanges ();
                List<User> user = _context.users.ToList ();
                int id = user[user.Count () - 1].UserId;
                HttpContext.Session.SetInt32 ("session_user", id);
                return RedirectToAction ("AddRecord", new { uid = id });
            }
            return View ("Index");
        }

        [HttpGet]
        [Route ("loginPage")]

        public IActionResult Login () {
            return View ("Login");
        }

        [HttpPost]
        [Route ("login")]
        public IActionResult LoginProcess (string email, string password) {
            List<User> user = _context.users.Where (u => u.Email == email).ToList ();
            if (user.Count > 0) {
                if (user[0].Password == password) {
                    HttpContext.Session.SetInt32 ("session_user", user[0].UserId);
                    return RedirectToAction ("Account");
                } else {
                    string error = "Password you entered does not match what we have in our records!";
                    ViewBag.err = error;
                    return View ("Login");
                }
            } else {
                ViewBag.err = "We could not find your email in our database!";
            }
            return View ("Login");
        }

        [HttpGet]
        [Route ("addrecord")]
        public IActionResult AddRecord (int uid) {
            Record newRecord = new Record (uid);
            _context.records.Add (newRecord);
            _context.SaveChanges ();
            return RedirectToAction ("Account");
        }

        [HttpGet]
        [Route ("account")]
        public IActionResult Account () {
            int? id = HttpContext.Session.GetInt32 ("session_user");
            List<User> session_user = _context.users.Where (useri => useri.UserId == id).ToList ();
            ViewBag.session = session_user[0].FirstName;
            List<User> userr = _context.users
                .Include (user => user.records).Where (u => u.UserId == id)
                .ToList ();
            userr[0].records.RemoveAt (0);
            userr[0].records.Reverse();
            ViewBag.records = userr[0].records.ToList ();
            ViewBag.lastrec = userr[0].records.First ();
            HttpContext.Session.SetInt32 ("amount", userr[0].records.First().balance);
            return View ("Account");
        }

        [HttpPost]
        [Route ("add-record")]
        public IActionResult DepositWithdraw (int Amount) {
            int? CurrentBalance = HttpContext.Session.GetInt32 ("amount");
            int CB = Convert.ToInt32 (CurrentBalance);
            int? id = HttpContext.Session.GetInt32 ("session_user");
            Record newRecord = new Record (id);

            newRecord.balance += Amount + CB;
            newRecord.record = Amount;
            _context.records.Add (newRecord);
            _context.SaveChanges ();
            return RedirectToAction ("Account");
        }

        [HttpGet]
        [Route ("logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }

    }
}