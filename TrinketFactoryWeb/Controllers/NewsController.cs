using Microsoft.AspNetCore.Authorization;
using TrinketFactoryWeb.Data;
using TrinketFactoryWeb.Models;

using Microsoft.AspNetCore.Mvc;

namespace TrinketFactoryWeb.Controllers;

public class NewsController : Controller
{
    private readonly ApplicationDbContext _db;
    public NewsController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        List<News> objNewsList = _db.Newss.ToList();
        return View(objNewsList);
    }

    public IActionResult Create()
    {
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Create(News obj)
    {
        if (ModelState.IsValid)
        {
            TempData["success"] = $"Topic {obj.Id} successfully created";
            _db.Newss.Add(obj);
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
        News newsFromDb = _db.Newss.Find(id);
        
        if (newsFromDb == null)
        {
            return NotFound();
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Edit(News obj)
    {
        if (ModelState.IsValid)
        {
            _db.Newss.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        News newsFromDb = _db.Newss.Find(id);
        
        if (newsFromDb == null)
        {
            return NotFound();
        }
        return RedirectToAction("Index");
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        News obj = _db.Newss.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        
        if (ModelState.IsValid)
        {
            _db.Newss.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }
}





