using Emp.Core;
using Emp.Core.Repositories;
using Emp.Data.Models;
using Emp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Emp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IUnitOfWork Uow { get; }
        public IEmployeeRepository EmployeeRepository { get; }


        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            Uow = uow;
            EmployeeRepository = employeeRepository;
            
        }

        public IActionResult Index(string filter)
        {
            List<Employee> employeeList;
            if (String.IsNullOrEmpty(filter)){
                employeeList = EmployeeRepository.GetAllEmployees().OrderBy(o => o.BornDate).ToList();
            }
            else
            {
                employeeList = EmployeeRepository.GetAllEmployees().Where(f=>f.Name.Contains(filter)).OrderBy(o=>o.BornDate).ToList();
            }

            ViewData["CurrentFilter"] = filter;
            return View(employeeList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            Employee newEmployee = new Employee();
            return View(newEmployee);
        }

        [HttpPost]
        public IActionResult Create(Employee newEmployee)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newEmployee);
            }

            var uniqueRFC = Uow.Employees.FindAll().Where(u => u.RFC == newEmployee.RFC).FirstOrDefault();

            if (uniqueRFC != null)
            {
                ModelState.AddModelError ("RFC", "RFC is taken");
                return View("Create", newEmployee);
            }

            EmployeeRepository.CreateEmployee(newEmployee);
            Uow.Commit();
            return RedirectToAction("Index");
        }
    }
}
