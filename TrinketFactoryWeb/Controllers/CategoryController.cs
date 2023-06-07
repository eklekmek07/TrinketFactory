using TrinketFactoryWeb.Data;
using TrinketFactoryWeb.Models;

using Microsoft.AspNetCore.Mvc;

namespace TrinketFactoryWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        List<Category> objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
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
        Category categoriesFromDb = _db.Categories.Find(id);
        Category categoriesFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        Category categoriesFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        if (categoriesFromDb == null)
        {
            return NotFound();
        }
        return View(categoriesFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
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
        Category categoriesFromDb = _db.Categories.Find(id);
        Category categoriesFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        Category categoriesFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        if (categoriesFromDb == null)
        {
            return NotFound();
        }
        return View(categoriesFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Category obj = _db.Categories.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(obj);
        
        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View();
    }
}





