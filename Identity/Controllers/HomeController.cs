using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Identity.Controllers;

public class HomeController : Controller
{   
    private readonly UserManager<IdentityUser> _userManager;
    private readonly  SignInManager<IdentityUser> _signInManager;
    public HomeController(ILogger<HomeController> logger, 
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager)
    {
       _userManager = userManager;
       _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

   [Authorize]
   public IActionResult Secret()
   {
       return View();
   }
   public IActionResult Login()
   {   
       return View();
   }

    [HttpPost]
    public async  Task<IActionResult> Login(string username, string password)
    {   

        var user = await _userManager.FindByNameAsync(username);

        if(user != null)
        {
          var signInResult =  await _signInManager.PasswordSignInAsync(user, password,false, false);

          if(signInResult.Succeeded)
          {
              return RedirectToAction("Index");
          }
        }

        return View();
    }
    public IActionResult Register()
    {        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string password)
    {        

        var user = new IdentityUser()
        {
            UserName = username,
            Email = "ricardodias169@gmail.com",
        };


       var result = await _userManager.CreateAsync(user, password);


        if(result.Succeeded)
        {
            var code = _userManager.GenerateEmailConfirmationTokenAsync(user);

            var link = Url.Action(nameof(VerifyEmail), "Home", new { userId = user.Id , code});

           // _userManager.ConfirmEmailAsync()
            //gerar token email
            return RedirectToAction("EmailVerification");
        }

        return View();
    }

    public async Task<IActionResult> VerifyEmail(string userId, string code)
    {
        return View();
    }
    public IActionResult EmailVerification() => View();
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index");
    }
}
