using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;

namespace Otus.Teaching.Pcf.Administration.Core.Abstractions.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        /// <summary>
        /// Получить данные сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByIdAsync(Guid id);

        /// <summary>
        /// Обновить количество выданных промокодов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task UpdateAppliedPromocodesAsync(Guid id);
    }
}