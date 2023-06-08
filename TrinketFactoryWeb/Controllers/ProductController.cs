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
    public IActionResult Create(Product obj)
    {
        if (ModelState.IsValid)
        {
            _db.Products.Add(obj);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Product productFromDb = _db.Products.Find(id);
        
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View();
    }
    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            _db.Products.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View();
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Product productFromDb = _db.Products.Find(id);
        
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product obj = _db.Products.Find(id);
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





