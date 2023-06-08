using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TrinketFactoryWeb.Data;
using TrinketFactoryWeb.Models;
	
using Microsoft.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrinketFactoryWeb.Controllers;

public class CartController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    public CartController(
        ApplicationDbContext db,
        UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }
    // GET
    public IActionResult Index()
    {
        // 1- get User id inside controller or get it from view
        // 2- Select cart id with user id inside carts
        // 3- Select cart items with cart id inside cartitems
        // 4- Return to view the data from third step
        string userId = _userManager.GetUserId(User); // 1
        int cartId = _db.Carts.Where(c => c.ApplicationUserId == userId)
            .Select(c => c.Id)
            .FirstOrDefault(); // 2
        List<CartItem?> cartItems = _db.CartItems
            .Include(p =>p.Product)
            .Where(ci => ci.CartId == cartId)
            .ToList(); // 3
        return View(cartItems); // 4
    }

    public IActionResult Purchase()
    {
        string userId = _userManager.GetUserId(User); // 1
        int cartId = _db.Carts.Where(c => c.ApplicationUserId == userId)
            .Select(c => c.Id)
            .FirstOrDefault(); // 2
        List<CartItem?> cartItems = _db.CartItems
            .Include(p =>p.Product)
            .Where(ci => ci.CartId == cartId)
            .ToList(); // 3
        return View(cartItems);
    }
    
    public IActionResult PurchaseDone()
    {
        string userId = _userManager.GetUserId(User); // 1
        int cartId = _db.Carts.Where(c => c.ApplicationUserId == userId)
            .Select(c => c.Id)
            .FirstOrDefault(); // 2
        List<CartItem?> cartItems = _db.CartItems
            .Include(p =>p.Product)
            .Where(ci => ci.CartId == cartId)
            .ToList(); // 3
        _db.CartItems.RemoveRange(cartItems);
        _db.SaveChanges();
        return View();
    }
    
    public IActionResult Add(int productId)
    {
        
        Product? product = _db.Products.FirstOrDefault(p => p.Id == productId);
        string userId = _userManager.GetUserId(User);
        int cartId = _db.Carts.Where(c => c.ApplicationUserId == userId)
            .Select(c => c.Id)
            .FirstOrDefault(); // 2
        var cartItem = _db.CartItems
            .Where(c => c.CartId == cartId)
            .FirstOrDefault(c => c.Product.Id == productId);
        if (cartItem != null) {
            
            cartItem.Quantity++;
        }
        else {
            cartItem = new CartItem
            {
                CartId = cartId,
                Quantity = 1,
                Product = product
            };
            _db.CartItems.Add(cartItem);
        }
        
        TempData["succes"] = $"{product!.Name} added succesfull to cart!";
        _db.SaveChanges();
        return RedirectToAction(controllerName:"Product", actionName:"Index");
    }

    // POST: Cart/Increment/5
    [HttpPost]
    public IActionResult Increment(int id)
    {
        CartItem cartItem = _db.CartItems.FirstOrDefault(ci => ci.Id == id);
        if (cartItem != null)
        {
            cartItem.Quantity++;
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // POST: Cart/Decrement/5
    [HttpPost]
    public IActionResult Decrement(int id)
    {
        CartItem? cartItem = _db.CartItems.FirstOrDefault(ci => ci.Id == id);
        if (cartItem != null)
        {
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                _db.SaveChanges();
            }
            else
            {
                _db.CartItems.Remove(cartItem);
                _db.SaveChanges();
            }
        }
        return RedirectToAction("Index");
    }

    // POST: Cart/Remove/5
    [HttpPost]
    public IActionResult Remove(int id)
    {
        CartItem? cartItem = _db.CartItems.FirstOrDefault(ci => ci.Id == id);
        if (cartItem != null)
        {
            _db.CartItems.Remove(cartItem);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}





