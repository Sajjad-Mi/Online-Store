using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private IStoreRepository repository;

        public CategoryMenuViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            foreach (var s in RouteData.Values)
            {
                Console.WriteLine("Value = {0}", s);
            }
            return View(repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
