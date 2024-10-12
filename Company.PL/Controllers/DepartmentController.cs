using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var Count =await _departmentRepository.AddAsync(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
            
        }
        public async Task<IActionResult> Details(int? id,string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var department =await _departmentRepository.GetAsync(id);
            if (department is null) return NotFound();
            return View(viewName,department);
           
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest();
            //var department = _departmentRepository.Get(id);
            //if (department is null) return NotFound();
            //return View(department);
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, Department department)
        {
            try
            {
                if (id != department.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var count =await _departmentRepository.DeleteAsync(department);

                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }
            return View(department);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();
            //var department = _departmentRepository.Get(id);
            //if (department is null) return NotFound();
            //return View(department);
            return await Details(id,"Edit"); 

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id ,Department department)
        {
            try
            {
                if (id != department.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var count =await _departmentRepository.UpdateAsync(department);

                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch(Exception Ex)
            {
                ModelState.AddModelError(string.Empty,Ex.Message);
            }
            return View(department);
        }


    }

}
