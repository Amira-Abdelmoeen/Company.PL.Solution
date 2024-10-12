using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _departmentRepository =new DepartmentRepository(context);
            _employeeRepository = new EmployeeRepository(context);
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public IDepartmentRepository DepartmentRepository =>_departmentRepository;

        public AppDbContext Context { get; }
    }
}
