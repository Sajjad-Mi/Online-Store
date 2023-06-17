using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IStoreRepository repository;
        private UserManager<IdentityUser> userManager;

        public ProductController(IStoreRepository repo, UserManager<IdentityUser> userMgr)
        {
            repository = repo;
            userManager = userMgr;
        }

        public async Task<IActionResult> Index(int productId)
        {
            Product product = await repository.Products
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            return View(new ProductCommentViewModel { Product = product });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewComment(Comment Comment)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
                Product product = await repository.Products
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(o => o.ProductID == Comment.ProductId);

                Comment.UserId = user.Id;
                Comment.User = user;
                Comment.ProductId = product.ProductID;

                Comment.Product = product;

                product.Comments.Add(Comment);
                repository.CreateComment(Comment);
                repository.SaveProduct(product);
            }
            return Redirect("/page1");
        }
    }
}
