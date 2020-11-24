using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Exam.Models;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("Id") != null)
            {
                ViewBag.Hobbies = dbContext.Hobbies
                .Include(h => h.Hobbists);
                return View();
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("Id") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        [HttpGet("Hobby/{myNum}")]
        public IActionResult Hobby(int myNum)
        {
            ViewBag.hobby = dbContext.Hobbies
            .Include(h => h.Hobbists)
            .FirstOrDefault(h => h.HobbyId == myNum);
            ViewBag.user = dbContext.Users.ToList();
            HttpContext.Session.SetInt32("HId", myNum);
            return View();
        }

        [HttpPost("CreateHobby")]
        public IActionResult CreateHobby(Hobby newHobby)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Hobbies.Any(n => n.HobbyName == newHobby.HobbyName))
                {
                    ModelState.AddModelError("HobbyName", "Hobby Name is already being used!");
                    return View("Create", newHobby);
                }
                else
                {
                    int? loggedin = HttpContext.Session.GetInt32("Id");
                    newHobby.UserId = (int)loggedin;
                    dbContext.Add(newHobby);
                    dbContext.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return View("Create", newHobby);
            }
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View("Home");
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Username == user.Username);
                if (userInDb != null)
                {
                    ModelState.AddModelError("Username", "Username is already being used!");
                    return View("Home");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("Id", user.UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Home");
            }
        }

        [HttpPost("Hobby/AddHobby")]
        public IActionResult AddHobby(Interest newInterest)
        {
            int? back = HttpContext.Session.GetInt32("HId");
            int sendback = (int)back;

            if (dbContext.Interests.Any(n => n.UserId == newInterest.UserId))
            {
                return View("Hobby/" + sendback);
            }
            else
            {
                int? loggedin = HttpContext.Session.GetInt32("Id");
                newInterest.UserId = (int)loggedin;
                dbContext.Add(newInterest);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if (ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Username == userSubmission.LoginUsername);
                if (userInDb == null)
                {
                    ModelState.AddModelError("LoginUsername", "Invalid Username/Password");
                    return View("Home");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Username/Password");
                    return View("Home");
                }
                HttpContext.Session.SetInt32("Id", userInDb.UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Home");
            }
        }
    }
}
