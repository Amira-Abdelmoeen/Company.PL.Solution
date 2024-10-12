using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace Company.PL.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeesController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchInput)
        {
            var employees = Enumerable.Empty<Employee>();   
            var employeesViewModels = new Collection<EmployeeViewModel>();   
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees =await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            //Auto Mapping
            var Result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            //string Message = "Hello World";

            //ViewData["Message"] = Message + "From View Data";

            //ViewBag.Message = Message + "From View Bag";  

            //TempData["Message01"] = Message + "From Temp Data";

            return View(Result);
        }
        public async Task<IActionResult> Create()
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();

            ViewData["Departments"]=departments;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image,"images");
                //cast erom EmployeeViewModel to Model
                //Mapping 
                //Manual Mapping
                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    Phone = model.Phone,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    HiringDate = model.HiringDate,
                //    WorkForId = model.WorkForId,
                //    WorkFor = model.WorkFor,

                //};
                //Auto Mapping
                var employee =_mapper.Map<Employee>(model);   
                var Count =await _unitOfWork.EmployeeRepository.AddAsync(employee);
                if (Count > 0)
                {
                   TempData["Message"] ="Employee Is Created Successfully";
                }
                else
                {
                    TempData["Message"] = "Employee Is Not Created Successfully";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetAsync(id);
            if (employee is null) return NotFound();
            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);
            return View(viewName, employeeVM);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var employee = _mapper.Map<Employee>(model);
                    var count =await _unitOfWork.EmployeeRepository.DeleteAsync(employee);

                    if (count > 0)
                    {
                        DocumentSettings.DeleteFile(model.ImageName,"images");
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();

            ViewData["Departments"] = departments;

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName,"images");
                    }
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                    var employee = _mapper.Map<Employee>(model);
                    var count =await _unitOfWork.EmployeeRepository.UpdateAsync(employee);

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
            return View(model);
        }


    }


}
