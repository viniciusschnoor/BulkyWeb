using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }
        #region CRUD
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid && obj.CategoryId == 0)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _context.Categories.Find(categoryId);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //[HttpPost]
        //public IActionResult Edit(Category obj)
        //{
        //    if (obj.Name == obj.DisplayOrder.ToString())
        //    {
        //        ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
        //    }
        //    if (ModelState.IsValid && obj.CategoryId == 0)
        //    {
        //        _context.Categories.Update(obj);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        #endregion
    }
}
