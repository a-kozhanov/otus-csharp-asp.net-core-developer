using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetEmployeeAsync(Expression<Func<Employee, bool>> predicate);

        Task<Employee> GetEmployeeeWithRole(Guid id);
    }
}