using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;
        public HomeController(IStoreRepository repository)
        {
            this.repository = repository;
        }
        //public IActionResult Index(int ProductPage = 1) => View(repository.Products.OrderBy(p => p.ProductId).Skip((ProductPage - 1) * PageSize).Take(PageSize));

        public ViewResult Index(string? category, int ProductPage = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => string.IsNullOrEmpty(category) || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((ProductPage - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = ProductPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },

                CurrentCategory = category
            });
        
    }
}
