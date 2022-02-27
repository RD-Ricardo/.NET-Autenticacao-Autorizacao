using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Claims.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Claims.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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

   public IActionResult Authenticate()
   {


       //declarações
       var grandmaClaims = new List<Claim>()
       {
            new Claim(ClaimTypes.Name, "Ricardo"),
            new Claim(ClaimTypes.Email, "ricardodias69@gmail.com"),
            new Claim("Minha Claim", "valor da minhaClaim")
       };


        //criando identidade do usuario 
        var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");


        //associando o usuario principal  ao ambiente 
        var userPrincipal = new ClaimsPrincipal(new []{ grandmaIdentity }); 


        HttpContext.SignInAsync(userPrincipal);


       return View();
   }

}
