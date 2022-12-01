using Emp.Core.Repositories;
using Emp.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emp.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmpDbContext _db;
        private IEmployeeRepository _employeeRepository;

        public UnitOfWork(EmpDbContext context)
        {
            this._db = context;
        }
        public IEmployeeRepository Employees => _employeeRepository = _employeeRepository ?? new EmployeeRepository(_db);

        public void Commit()
        {
            _db.SaveChanges();
        }
        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
