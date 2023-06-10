using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IStoreRepository repository;

        public ProductController(IStoreRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(int productId)
        {
            Product product = repository.Products.FirstOrDefault(o => o.ProductID == productId);
            return View(product);
        }
    }
}
