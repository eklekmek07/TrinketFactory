using TrinketFactoryWeb.Data;
using TrinketFactoryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrinketFactoryWeb.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;
    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        List<Product> objItemsList = _db.Products
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .ToList();
        
        return View(objItemsList);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Details(int productId)
    {
        Product product = _db.Products.FirstOrDefault(p => p.Id == productId);
        return View(product);
    }
    
    [HttpPost]
    public IActionResult Create(Product? obj)
    {
        if (ModelState.IsValid)
        {
            _db.Products.Add(obj);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? productId)
    {
        if (productId == null || productId == 0)
        {
            return NotFound();
        }
        Product productFromDb = _db.Products.Find(productId);
        
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb);
    }
    [HttpPost, ActionName("Edit")]
    public IActionResult EditPost(int? productId)
    {
        if (productId == null || productId == 0)
        {
            return NotFound();
        }

        Product obj = _db.Products.Find(productId);
        if (obj == null)
        {
            return NotFound();
        }

        if (TryUpdateModelAsync<Product>(obj, "", p => p.Name, p => p.Price, p => p.CategoryId, p => p.ImagePath).Result)
        {
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    
    public IActionResult Delete(int? productId)
    {
        if (productId == null || productId == 0)
        {
            return NotFound();
        }
        Product productFromDb = _db.Products.Find(productId);
        
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? productId)
    {
        Product? obj = _db.Products.Find(productId);
        if (obj == null)
        {
            return NotFound();
        }
        
        if (ModelState.IsValid)
        {
            _db.Products.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View();
    }
}





