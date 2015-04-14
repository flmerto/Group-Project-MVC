using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC_Group_Project;
using MVC_Group_Project.Models;
using MVC_Group_Project.Filters;

namespace MVC_Group_Project.Controllers
{
  [CustomAuthorization(Role = "Admin")] 
    public class UsersAdminController : Controller
    {
        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /UsersAdmin/
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /UsersAdmin/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /UsersAdmin/ResetPassword/5
        public async Task<ActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            return View(new ResetUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Password = ""
            });
        }

        //
        // POST: /UsersAdmin/ResetPassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetUserViewModel resetUser)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(resetUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                await UserManager.RemovePasswordAsync(user.Id);
                await UserManager.AddPasswordAsync(user.Id, resetUser.Password);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /UsersAdmin/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /UsersAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = userViewModel.Email, Email = userViewModel.Email };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        //var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles); // doesn't find Roles, only Role TODO!
                        var result = await UserManager.AddToRoleAsync(user.Id, selectedRoles[0]); // needs to be fixed.
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();
                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /UsersAdmin/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /UsersAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email, FirstName, LastName, Address, Id")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                if (user.FirstName == "" && user.LastName == "" && user.Address == "")
                {
                    user.UserName = user.UserName;
                    user.Email = user.Email;
                    user.FirstName = user.FirstName;
                    user.LastName = user.LastName;
                    user.Address = user.Address;
                }
                else
                {
                    user.UserName = editUser.Email;
                    user.Email = editUser.Email;
                    user.FirstName = editUser.FirstName;
                    user.LastName = editUser.LastName;
                    user.Address = editUser.Address;
                }

                //await UserManager.RemovePasswordAsync(user.Id);
                //await UserManager.AddPasswordAsync(user.Id, editUser.Password);

                var userRoles = await UserManager.GetRolesAsync(user.Id);
                if (selectedRole[0] == "Admin" || selectedRole[0] == "Member")
                {
                    await UserManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    selectedRole = selectedRole ?? new string[] { };

                    //var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>()); // doesn't find Roles, only Role TODO!
                    var result = await UserManager.AddToRoleAsync(user.Id, selectedRole[0]); // needs to be fixed.
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    //result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>()); // doesn't find Roles, only Role TODO!
                    result = await UserManager.RemoveFromRoleAsync(user.Id, selectedRole[0]);// needs to be fixed.
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /UsersAdmin/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /UsersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
