using Emp.Data.Data;
using Emp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emp.Core.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        //protected readonly EmpDbContext _db;
        public EmployeeRepository(EmpDbContext repositoryContext) : base(repositoryContext)
        {
            //_db = repositoryContext;
        }

        public void CreateEmployee(Employee employee)
        {
            //_db.Employees.Add(employee);

            //Let RepositoryBase add record to context (in database)
            Create(employee);
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return FindAll();
        }
    }
}
