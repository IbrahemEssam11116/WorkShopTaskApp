using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkshopTaskApp.Bussniss.Intrfaces;
using WorkshopTaskApp.Entity.Models;

namespace WorkshopTaskApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService,
                                 ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            var fetchedProducts = await _productService.GetProducts();
            var FilteredProducts =await _productService.GetProductsByPagination( 1,5,null);
            return View(FilteredProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int pageIndex, int? categoryId)
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            var fetchedProducts = await _productService.GetProducts(categoryId);
            var FilteredProducts = await _productService.GetProductsByPagination(pageIndex, 5, categoryId);
            FilteredProducts.CategoryId= categoryId;
            return View(FilteredProducts);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }
        public async Task<IActionResult> Edit(int Id)
        {
            Product fetchedProduct = await _productService.GetProductById(Id);
            if (fetchedProduct == null)
                return NotFound();
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View(fetchedProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product productToAdd)
        {
        
                var isSaved = await _productService.AddProduct(productToAdd);
                if (!isSaved)
                    return RedirectToAction(nameof(Create));

                return RedirectToAction(nameof(Index));
          
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product productToAdd)
        {
        

                var isSaved = await _productService.Update(productToAdd);
                if (!isSaved)
                    return RedirectToAction(nameof(Edit),productToAdd.Id);

                return RedirectToAction(nameof(Index));
          
        }

    }
}
