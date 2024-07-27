
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;     
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.GetAll();
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
                _db.Add(ctgObj);
                _db.Save();
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

            //var category = _db.Categories.Find(id);
            var ctg = _db.GetFirstOrDefault(x => x.Id == id);

            if (ctg == null)
                return NotFound();

            return View(ctg);
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
                _db.Update(ctgObj);
                _db.Save();
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

            //var category = _db.Categories.Find(id);
            var ctg = _db.GetFirstOrDefault(x => x.Id == id);

            if (ctg == null)
                return NotFound();

            return View(ctg);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var ctgObj = _db.GetFirstOrDefault(x => x.Id == id);

            if (ctgObj is null)
                return NotFound();

            _db.Remove(ctgObj);
            _db.Save();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
         
        }
    }
}
