using TrinketFactoryWeb.Data;
using TrinketFactoryWeb.Models;

using Microsoft.AspNetCore.Mvc;

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
        List<Product> objItemsList = _db.Product.ToList();
        return View(objItemsList);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Product obj)
    {
        if (ModelState.IsValid)
        {
            _db.Product.Add(obj);
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
        Product productFromDb = _db.Product.Find(id);
        Product productFromDb1 = _db.Product.FirstOrDefault(u=>.Id==id);
        Product productFromDb2 = _db.Product.Where(u=>.Id==id).FirstOrDefault();
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            _db.Product.Update(obj);
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
        Product productFromDb = _db.Product.Find(id);
        Product productFromDb1 = _db.Product.FirstOrDefault(u=>.Id==id);
        Product productFromDb2 = _db.Product.Where(u=>.Id==id).FirstOrDefault();
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Product obj = _db.Product.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Product.Remove(obj);
        
        if (ModelState.IsValid)
        {
            _db.Product.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View();
    }
}





