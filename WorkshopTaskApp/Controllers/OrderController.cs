using Microsoft.AspNetCore.Mvc;
using WorkshopTaskApp.Bussniss.Intrfaces;
using WorkshopTaskApp.Entity.Models;
namespace WorkshopTaskApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderService,
                               IProductService productService,
                               IUserService userService)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
        }


        public async Task<IActionResult> Order(int productId)
        {
            //Check user cookie 
            var fetchedUserId = _userService.FetchUserID();
            if (fetchedUserId == null)
                return RedirectToAction("Login", "User");
            var user = await _userService.GetUserById(fetchedUserId.Value);
            //check Product availability
            var fetchedProduct = await _productService.Find(productId);
            if (fetchedProduct == null)
                return NotFound();
            ViewBag.Product = fetchedProduct;
            UserOrder model = new UserOrder() { Phone = user.Phone, Address = user.Address };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Order(UserOrder userProduct)
        {
            //Check product available quantity and existance
            var fetchedProduct = await _productService.Find(userProduct.ProductId);
            if (fetchedProduct == null || fetchedProduct.Quantity == 0)
                ModelState.AddModelError("", "This product is out of stock");

            if (userProduct.Quantity > fetchedProduct.Quantity)
                ModelState.AddModelError("", "You ordered more than quantity in stock");


            if (!ModelState.IsValid)
            {
                var fetchProduct = await _productService.Find(userProduct.ProductId);
                ViewBag.Product = fetchedProduct;

                return View(userProduct);
            }
            //return RedirectToAction("Order", new { productId = userProduct.ProductId });


            //Fetch user id from cookie
            var fetchedUserId = _userService.FetchUserID();
            if (fetchedUserId != null)
                userProduct.UserId = fetchedUserId.Value;
            else
                return RedirectToAction("Index", "Product");

            //Add oder in DB
            var isAdded = await _orderService.AddOrder(userProduct);
            if (!isAdded)
                return BadRequest();

            //Update Product Quantity
            fetchedProduct.Quantity = fetchedProduct.Quantity - userProduct.Quantity;
            var isUpdated = await _productService.Update(fetchedProduct);
            if (!isUpdated)
                return BadRequest();

            return RedirectToAction("Index", "Product");
        }


    }
}
