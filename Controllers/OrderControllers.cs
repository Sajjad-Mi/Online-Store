using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        private UserManager<IdentityUser> userManager;

        public OrderController(
            IOrderRepository repoService,
            Cart cartService,
            UserManager<IdentityUser> userMgr
        )
        {
            repository = repoService;
            cart = cartService;
            userManager = userMgr;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public async Task<IActionResult> Checkout([FromForm] Order order)
        {
            foreach (var entry in ModelState.Values)
            {
                Console.WriteLine($"Property: {entry}");

                if (entry.Errors.Any())
                {
                    Console.WriteLine("Errors:");
                    foreach (var error in entry.Errors)
                    {
                        Console.WriteLine($"- {error.ErrorMessage}");
                    }
                }

                if (entry.RawValue != null)
                {
                    Console.WriteLine($"Raw Value: {entry.RawValue}");
                }

                Console.WriteLine();
            }
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
                order.User = user;
                order.UserId = user.Id;
                order.Lines = cart.Lines.ToArray();
                Console.WriteLine(order.UserId);
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            
            IQueryable<Order> userOrders =  repository.Orders
                .Where(o => o.UserId == user.Id);
            return View(userOrders);
        }
    }
}
