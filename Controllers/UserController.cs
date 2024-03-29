using Microsoft.EntityFrameworkCore;
using products_and_categories.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace products_and_categories.Controllers
{
  public class UserController : Controller
  {
    private MyContext dbContext;
    // here we can "inject" our context service into the constructor
    public UserController(MyContext context)
    {
      dbContext = context;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
      return View();
    }
    [HttpPost("user/register")]
    public IActionResult Register(UserRegistration newUser)
    {
      if (ModelState.IsValid)
      {
        if (dbContext.Users.Any(u => u.Email == newUser.Email))
        {
          ModelState.AddModelError("Email", "Email already in use!");
          return View("Index");
        }
        else
        {
          PasswordHasher<UserRegistration> Hasher = new PasswordHasher<UserRegistration>();
          newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
          User NewUser = new User
          {
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
            Password = newUser.Password,
          };
          dbContext.Users.Add(NewUser);
          dbContext.SaveChanges();

          int uid = NewUser.UserId;
          HttpContext.Session.SetInt32("uid", uid);

          return RedirectToAction("Success");
        }
      }
      else
      {
        return View("Index");
      }
    }
    [HttpPost("user/login")]
    public IActionResult Login(UserLogin currentUser)
    {
      if (ModelState.IsValid)
      {
        User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == currentUser.LoginEmail);
        if (userInDb == null)
        {
          // Add an error to ModelState and return to View!
          ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
          return View("Index");
        }
        // Initialize hasher object
        var hasher = new PasswordHasher<UserLogin>();

        // verify provided password against hash stored in db
        var result = hasher.VerifyHashedPassword(currentUser, userInDb.Password, currentUser.LoginPassword);

        // result can be compared to 0 for failure
        if (result == 0)
        {
          // handle failure (this should be similar to how "existing email" is handled)
          ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
          return View("Index");
        }
        int uid = userInDb.UserId;
        HttpContext.Session.SetInt32("uid", uid);

        return RedirectToAction("Success");
      }
      else
      {
        return View("Index");
      }
    }
    [HttpGet("user/success")]
    public IActionResult Success()
    {
      int? uid = HttpContext.Session.GetInt32("uid");
      if (uid == null)
      {
        return View("Index");
      }
      else
      {
        User retrivedUser = dbContext.Users.FirstOrDefault(u => u.UserId == uid);
        return RedirectToAction("ProductView", "ProductCategory"); ;
      }
    }

    [HttpGet("user/logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Index");
    }

  }
}