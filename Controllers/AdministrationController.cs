using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Wellness_USC.ViewModels;
using Wellness_USC.Areas.Identity.Data;
using static SweetAlertBlog.Enums.Enums;


namespace Wellness_USC.Controllers
{
    [Authorize(Roles = "administrador")]
    public class AdminController : BaseController
    {


        public IActionResult Index()
        {
            return View();
        }
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly UserManager<ApplicationUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult CrearRol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(RolViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole { Name = roleViewModel.RoleName };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("roles", "admin");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(roleViewModel);
        }

        [HttpGet]
        public IActionResult Roles()
        {
            var roles = _roleManager.Roles;

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditarRol(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {

                Alert("Este rol no se encuentra en el sistema", NotificationType.error);
                return RedirectToAction("roles", "admin");
            }

            var model = new EditRolViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.Identification);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditarRol(EditRolViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                Alert("Este rol no se encuentra en el sistema", NotificationType.error);
                return RedirectToAction("roles", "admin");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditarRolesUsuarios(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                Alert("Este rol no se encuentra en el sistema", NotificationType.error);
                return RedirectToAction("roles", "admin");
            }
            ViewBag.id = role.Id;
            var model = new List<UserRolViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRolViewModel = new UserRolViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolViewModel.isSelected = true;
                }
                else
                {
                    userRolViewModel.isSelected = false;
                }
                model.Add(userRolViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditarRolesUsuarios(List<UserRolViewModel> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                Alert("Este rol no se encuentra en el sistema", NotificationType.error);
                return RedirectToAction("roles", "admin");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].isSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].isSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditarRol", new { id = id });
                    }
                }
            }
            return RedirectToAction("EditarRol", new { id = id });
        }

    }
}