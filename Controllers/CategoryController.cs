using Microsoft.AspNetCore.Mvc;
using TrinketFactoryStore.Data;
using TrinketFactoryStore.Models;

namespace TrinketFactoryStore.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var categoryList = _db.Categories.ToList();
        return View(categoryList);
    }
    
    public IActionResult Create()
    {
        var categoryList = _db.Categories.ToList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (ModelState.IsValid)
        {
            TempData["success"] = "Category created successfully";
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = _db.Categories.Find(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            TempData["success"] = "Category updated successfully";
            _db.Categories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        
        return View();
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = _db.Categories.Find(id);
        return View(categoryFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int id)
    {
        Category? obj = _db.Categories.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            TempData["success"] = "Category deleted successfully";
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        
        return View();
    }
}