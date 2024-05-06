using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthLearning.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            ViewBag.users = _userManager.Users;
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string rolename)
        {
            if (string.IsNullOrEmpty(rolename))
            {
                ViewBag.msg = "Role Name must be provided.";
                return View();
            }

            if (await _roleManager.RoleExistsAsync(rolename))
            {
                ViewBag.msg = "Role already exist.";
                return View();
            }

            IdentityRole role = new IdentityRole(rolename);
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                ViewBag.msg = "Role has been created successfully.";
                return View();
            }
            else
            {
                ViewBag.msg = "Sorry ! Could not create this Role.";
                return View();
            }
        }
        public IActionResult AssignRole()
        {
            ViewBag.userlist = _userManager.Users;
            ViewBag.rolelist = _roleManager.Roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string useremail, string userrole)
        {
            ViewBag.userlist = _userManager.Users;
            ViewBag.rolelist = _roleManager.Roles;

            if (string.IsNullOrEmpty(useremail))
            {
                ViewBag.msg = "User is not selected. Please select a user.";
                return View();
            }

            if (string.IsNullOrEmpty(userrole))
            {
                ViewBag.msg = "Role is not selected. Please select a Role.";
                return View();
            }

            var usr = await _userManager.FindByEmailAsync(useremail);
            if (usr == null)
            {
                ViewBag.msg = "Sorry ! User does not exist.";
                return View();
            }

            var role = await _roleManager.FindByNameAsync(userrole);
            if (role == null)
            {
                ViewBag.msg = "Sorry ! Role does not exist.";
                return View();
            }

            if (await _userManager.IsInRoleAsync(usr, userrole))
            {
                ViewBag.msg = "Sorry ! Already Role has been assigned to this user.";
                return View();
            }

            IdentityResult result = await _userManager.AddToRoleAsync(usr, userrole);
            if (result.Succeeded)
            {
                ViewBag.msg = "Done ! Role is assigned to this user successfully.";
                return View();
            }
            else
            {
                ViewBag.msg = "Sorry ! Role could not assign to this user.";
                return View();
            }
        }
    }
}
