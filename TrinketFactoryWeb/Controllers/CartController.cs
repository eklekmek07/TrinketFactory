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
        List<CartItem> cartItems = _db.CartItems
            .Where(ci => ci.CartId == cartId)
            .ToList(); // 3
        return View(cartItems); // 4
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
        CartItem cartItem = _db.CartItems.FirstOrDefault(ci => ci.Id == id);
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
        CartItem cartItem = _db.CartItems.FirstOrDefault(ci => ci.Id == id);
        if (cartItem != null)
        {
            _db.CartItems.Remove(cartItem);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}




