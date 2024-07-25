
using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;     
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories;
            return View(categoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category ctgObj)
        {
            if (ctgObj.Name == ctgObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display order cannot exactly match the name.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(ctgObj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            return View(ctgObj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if(id is null or 0)
                return NotFound();

            var category = _db.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category ctgObj)
        {
            if (ctgObj.Name == ctgObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display order cannot exactly match the name.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(ctgObj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
            return View(ctgObj);
        }
        
        //GET
        public IActionResult Delete(int? id)
        {
            if(id is null or 0)
                return NotFound();

            var category = _db.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var ctgObj = _db.Categories.Find(id);
            
            if(ctgObj is null)
                return NotFound();

            _db.Categories.Remove(ctgObj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
         
        }
    }
}
