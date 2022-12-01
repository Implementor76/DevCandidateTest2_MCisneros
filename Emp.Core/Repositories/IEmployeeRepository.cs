using Emp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emp.Core.Repositories
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        IQueryable<Employee> GetAllEmployees();
        void CreateEmployee(Employee employee);
    }
}
