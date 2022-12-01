using Emp.Core;
using Emp.Core.Repositories;
using Emp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public IUnitOfWork Uow { get; }
        public IEmployeeRepository EmployeeRepository { get; }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork uow, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            Uow = uow;
            EmployeeRepository = employeeRepository;

        }

        [HttpGet]
        public ActionResult<List<Employee>> Get(string filter)
        {
            List<Employee> employeeList;
            if (String.IsNullOrEmpty(filter))
            {
                employeeList = EmployeeRepository.GetAllEmployees().OrderBy(o => o.BornDate).ToList();
            }
            else
            {
                employeeList = EmployeeRepository.GetAllEmployees().Where(f => f.Name.Contains(filter)).OrderBy(o => o.BornDate).ToList();
                if (employeeList == null)
                {
                    return NotFound();
                }
            }
            return Ok(employeeList);
        }



    }
}
