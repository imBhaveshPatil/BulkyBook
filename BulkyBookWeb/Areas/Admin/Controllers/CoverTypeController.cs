using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private IUnitOfWork _db;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> covertypes = _db.CoverType.GetAll();
            return View(covertypes);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType ctgObj)
        {

            if (ModelState.IsValid)
            {
                _db.CoverType.Add(ctgObj);
                _db.Save();
                TempData["success"] = "cover type created successfully.";
                return RedirectToAction("Index");
            }
            return View(ctgObj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id is null or 0)
                return NotFound();

            //var category = _db.Categories.Find(id);
            var ctg = _db.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (ctg == null)
                return NotFound();

            return View(ctg);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType ctgObj)
        {

            if (ModelState.IsValid)
            {
                _db.CoverType.Update(ctgObj);
                _db.Save();
                TempData["success"] = "cover type updated successfully.";
                return RedirectToAction("Index");
            }
            return View(ctgObj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
                return NotFound();

            //var category = _db.Categories.Find(id);
            var ctg = _db.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (ctg == null)
                return NotFound();

            return View(ctg);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var Obj = _db.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (Obj is null)
                return NotFound();

            _db.CoverType.Remove(Obj);
            _db.Save();
            TempData["success"] = "Cover type deleted successfully.";
            return RedirectToAction("Index");

        }
    }
}
