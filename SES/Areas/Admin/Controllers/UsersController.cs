using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SES.Areas.Admin.Models;
using SES.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        //public IActionResult Create() => View();

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = new User { Email = model.Email, UserName = model.Email};
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ChangePasswordViewModel { UserId = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)

                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var error in changePasswordResult.Errors)
                        {
                            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
                            {
                                ModelState.AddModelError(string.Empty, "Старый пароль введен неверно");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }

                        return View(model);
                    }

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
