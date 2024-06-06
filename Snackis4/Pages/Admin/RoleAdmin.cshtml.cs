using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;

namespace Snackis4.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class RoleAdminModel : PageModel
    {
        public List<Snackis4User> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }


        [BindProperty(SupportsGet = true)]
        public string RoleName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }

        public bool IsUser { get; set; }

        public bool IsAdmin { get; set; }

        public Snackis4User MyUser { get; set; }


        public readonly UserManager<Snackis4User> _userManager;
        private RoleManager<IdentityRole> _roleManager;


        public RoleAdminModel(RoleManager<IdentityRole> roleManager, UserManager<Snackis4User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
            Users = await _userManager.Users.ToListAsync();

            if (AddUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                await _userManager.AddToRoleAsync(alterUser, RoleName);
            }

            if (RemoveUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                await _userManager.RemoveFromRoleAsync(alterUser, RoleName);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                MyUser = currentUser;
                IsUser = await _userManager.IsInRoleAsync(currentUser, "User");
                IsAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (RoleName != null)
            {
                await CreateRole(RoleName);
            }
            return RedirectToPage("./roleadmin");
        }

        public async Task CreateRole(string roleName)
        {
            bool roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var Role = new IdentityRole
                {
                    Name = roleName
                };
                await _roleManager.CreateAsync(Role);
            }
        }

    }
}