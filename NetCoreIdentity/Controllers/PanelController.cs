using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentity.Context;
using NetCoreIdentity.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetCoreIdentity.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class PanelController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public PanelController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel
            {
                Name = user.Name,
                SurName = user.SurName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                PictureUrl = user.PictureUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (model.Picture != null)
                {
                    string dirPath = Directory.GetCurrentDirectory();
                    string pictureName = Guid.NewGuid() + Path.GetExtension(model.Picture.FileName);
                    string path = dirPath + "/wwwroot/img/" + pictureName;
                    using FileStream stream = new FileStream(path, FileMode.Create);
                    await model.Picture.CopyToAsync(stream);
                    user.PictureUrl = pictureName;
                }
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}