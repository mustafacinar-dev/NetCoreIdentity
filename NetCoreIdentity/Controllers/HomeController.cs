using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentity.Context;
using NetCoreIdentity.Models;
using System.Threading.Tasks;

namespace NetCoreIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                if(result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Çok fazla başarısız giriş denemesi yaptığınız için hesabınız geçici süreyle kilitlenmiştir");
                    return View("Index", model);
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Giriş yapabilmeniz için, email adresinizi doğrulamanız gerekmektedir.");
                    return View("Index", model);
                }
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                var yanlisGirilme = await _userManager.GetAccessFailedCountAsync(await _userManager.FindByNameAsync(model.UserName));
                ModelState.AddModelError("", $"Kullanıcı adı veya şifre hatalı {5-yanlisGirilme} giriş denemesi hakkınız kaldı");
            }
            return View("Index", model);
        }

        public IActionResult SignUp()
        {
            return View(new UserSignUpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.Surname,
                    UserName = model.UserName,
                    Gender = model.Gender
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
