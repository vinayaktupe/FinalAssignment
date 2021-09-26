using FinalAssignment.DAL.Data.Models;
using FinalAssignment.DAL.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalAssignment.Services.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<Employee> GetEmployeeByID(int id);
        public Task<IEnumerable<Employee>> GetEmployeeByType(EmployeeType employeeType);
        public Task<bool> CreateEmployee(Employee employee);
        public Task<bool> UpdateEmployee(Employee employee);
        public Task<bool> DeleteEmployee(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly GenericRepository<Employee> _context;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger)
        {
            this._context = new GenericRepository<Employee>();
            this._logger = logger;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            try
            {
                _context.Create(employee);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return false;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                var employee = _context.GetById(id);
                if (employee == null)
                {
                    return false;
                }

                employee.IsActive = false;
                employee.UpdatedAt = DateTime.Now;
                _context.Update(employee);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return false;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                var result = _context.GetByCondition(emp => emp.IsActive == true);

                return result.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<Employee> GetEmployeeByID(int id)
        {
            try
            {
                return _context.GetByCondition(emp => emp.IsActive == true && emp.ID == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByType(EmployeeType employeeType)
        {
            try
            {
                return _context.GetByCondition(emp => emp.IsActive == true && emp.EmployeeType == employeeType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                employee.UpdatedAt = DateTime.Now;
                employee.IsActive = true;
                _context.Update(employee);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return false;
        }
    }
}
