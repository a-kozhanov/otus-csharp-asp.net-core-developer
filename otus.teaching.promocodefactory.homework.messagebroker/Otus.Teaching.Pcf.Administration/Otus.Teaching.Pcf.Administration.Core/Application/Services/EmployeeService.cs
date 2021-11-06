using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Services;
using Otus.Teaching.Pcf.Administration.Core.Application.Exceptions;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;

namespace Otus.Teaching.Pcf.Administration.Core.Application.Services
{
    public class EmployeeService:IEmployeeService
    {

        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee;
        }

        public async Task UpdateAppliedPromocodesAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                throw new EntityNotFoundException(id);

            employee.AppliedPromocodesCount++;

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}