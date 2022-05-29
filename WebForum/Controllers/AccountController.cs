using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebForum.Data;
using WebForum.Models;
using WebForum.ViweModels;

namespace WebForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager; 
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginVM login)
        {
            if (!ModelState.IsValid) 
                return View(login);
            var user = await _userManager.FindByEmailAsync(login.Email);  
            if (user != null)
            {
                //User jest , password przywierza
                var passwordcheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passwordcheck)
                {
                    // pass ok, logowanie
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Forum");
                    }
                }
                //Zly pass
                TempData["Error"] = "WRONG MOTHERFUCKER!";
                return View(login);
            }
            // Nie znalazlo user 
            TempData["Error"] = "Wrong credenrials, give another try";
            return View(login);
        }

        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM register)
        {
            if(!ModelState.IsValid)
                return View(register);
            var user = await _userManager.FindByEmailAsync(register.Email);
            if(user != null)
            {
                TempData["Error"] = "This email already in use";
                return View(register);
            }
            var newUser = new ApplicationUser()
            {
                Email = register.Email,
                UserName = register.Email
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, register.Password);
            //if(newUserResponse.Succeeded)
            //    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return RedirectToAction("Index","Home");
        }

        [HttpPost]

        public async Task<IActionResult>Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
