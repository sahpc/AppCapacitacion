using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppCapacitacion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            var userRoles = new Dictionary<string, IList<string>>();

            foreach (var usuario in usuarios)
            {
                userRoles[usuario.Id] = await _userManager.GetRolesAsync(usuario);
            }

            ViewBag.UserRoles = userRoles;
            ViewBag.AllRoles = _roleManager.Roles.ToList();

            return View(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> AsignarRol(string userId, string rol)
        {
            var usuario = await _userManager.FindByIdAsync(userId);

            if (usuario == null || string.IsNullOrEmpty(rol))
                return NotFound();

            var rolesActuales = await _userManager.GetRolesAsync(usuario);
            await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);
            await _userManager.AddToRoleAsync(usuario, rol);

            return RedirectToAction("Index");
        }
    }
}
