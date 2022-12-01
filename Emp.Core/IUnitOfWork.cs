using Emp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        void Commit();
    }
}
