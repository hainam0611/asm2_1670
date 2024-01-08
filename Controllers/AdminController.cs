using ASM2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASM2.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _context = db;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult User()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        public IActionResult Order()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }
    }
}
