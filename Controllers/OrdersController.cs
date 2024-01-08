using ASM2.Data;
using ASM2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public OrdersController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                    Order order = new Order
                    {
                        UserId = user.Id,
                        OrderDate = DateTime.Now,
                        OrderItems = dataCart.Select(cartItem => new OrderItem
                        {
                            BookId = cartItem.Product.Id,
                            Quantity = cartItem.Quantity,
                            Price = cartItem.Product.Price
                        }).ToList()
                    };


                    _dbContext.Orders.Add(order);
                    _dbContext.SaveChanges();

                    HttpContext.Session.Remove("cart");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("ListCart", "Products");
            }
        }

    }
}
