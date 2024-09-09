using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        
        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment, ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        [AllowAnonymous]
        public ViewResult Details(int? id) 
        {
            //throw new Exception("Error in details view");

            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");

            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);

            //Employee model = _employeeRepository.GetEmployee(1);
            //ViewBag.PageTitle = "Employee Details";

            //public JsonResult Details() //Note: We want this method to retirve a specific record and display those details on a browser.
            //{
            //    Employee model = _employeeRepository.GetEmployee(1);
            //    return Json(model);
            //}

            //public ObjectResult Details() //Note: We want this method to retirve a specific record and display those details on a browser.
            //{
            //    Employee model = _employeeRepository.GetEmployee(1);
            //    return new ObjectResult(model);

            //Note: Here we are using one of the overloads of View method to pass the model object.
        }

        [HttpGet] //Note: Here we are specifing HttpGet so that the below create method needs to be executed for Get request.
        //[Authorize]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        //[Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExsistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost] //Note: Here we are specifing the below create method needs to be executed for Post request.
        //[Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid) //Note: This checks whether the entered value is valid or not.
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id); //Note: Here the Id value comes from the hidden input field present in Edit View.

                employee.Name = model.Name; //Note: Here we are updating the exsisting employee with the edited employee values.
                employee.Email = model.Email;
                employee.Department = model.Department;

                if(model.Photos != null)
                {
                    if(model.ExsistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "image", model.ExsistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
                
                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model) //Note: Since we are passing the parent type as a parameter we can either pass instance of parent or child type.
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (IFormFile photo in model.Photos)
                {
                    string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "image");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(UploadsFolder, uniqueFileName);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(filestream);
                    }
                        
                }
            }

            return uniqueFileName;
        }

        [HttpPost] //Note: Here we are specifing the below create method needs to be executed for Post request.
        //[Authorize]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid) //Note: This checks whether the entered value is valid or not.
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id }); //Note: This method will redirect to details action method to display the data.
            }
            return View();
        }
    }
}

/*Note: Both ViewResult and RedirectToAction class implement the IActionResult Interface.*/







