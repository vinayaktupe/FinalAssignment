using FinalAssignment.DAL.Data;
using FinalAssignment.DAL.Data.Models;
using FinalAssignment.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment.Services.Services
{
    public interface IMinuteService
    {
        public Task<IEnumerable<Minute>> GetAllMinutes();
        public Task<Minute> GetMinuteByID(int id);
        public Task<bool> CreateMinute(Minute minute);
        public Task<bool> UpdateMinute(Minute minute);
        public Task<bool> DeleteMinute(int id);
        public Task<IEnumerable<Employee>> GetAllSupervisor();
        public Task<IEnumerable<Employee>> GetSupervisorByMinute(int id);
    }

    public class MinuteService : IMinuteService
    {
        private readonly DbSet<Minute> _context;
        private readonly ILogger<MinuteService> _logger;

        public MinuteService(ILogger<MinuteService> logger)
        {
            this._context = GenericRepository<Minute>.Inst.Set;
            this._logger = logger;
        }

        public async Task<bool> CreateMinute(Minute minute)
        {
            try
            {
                using (var context = new UserDbContext())
                {
                    await context.Minutes.AddAsync(minute);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return false;
        }

        public async Task<bool> DeleteMinute(int id)
        {
            try
            {
                var minute = _context.Include(m => m.Crews).Where(m => m.IsActive == true && m.ID == id).SingleOrDefault();

                if (minute == null)
                {
                    return false;
                }

                minute.IsActive = false;
                minute.UpdatedAt = DateTime.Now;
                _context.Update(minute);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return false;
        }

        public async Task<IEnumerable<Minute>> GetAllMinutes()
        {
            try
            {
                var result = _context.Include(min => min.Crews).Where(emp => emp.IsActive == true);

                return result.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllSupervisor()
        {
            try
            {
                using (var context = new GenericRepository<Employee>())
                {
                    var result = context.GetByCondition(emp => emp.IsActive == true && emp.EmployeeType == EmployeeType.Supervisor);
                    return result;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<Minute> GetMinuteByID(int id)
        {
            try
            {
                return _context.Include(min => min.Crews).Where(emp => emp.IsActive == true && emp.ID == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetSupervisorByMinute(int id)
        {
            try
            {
                using (var employee = new GenericRepository<Employee>())
                {
                    var minute = await _context.FirstOrDefaultAsync(min => min.ID == id);
                    var supervisor = from emp in employee.GetAll()
                                     join super in minute.SupervisorID
                                     on emp.ID equals super.SupervisorID
                                     select new Employee
                                     {
                                         Name = emp.Name,
                                         ID = emp.ID,
                                         CreatedAt = emp.CreatedAt,
                                         IsActive = emp.IsActive,
                                         EmployeeType = emp.EmployeeType,
                                         UpdatedAt = emp.UpdatedAt
                                     };

                    return supervisor;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<bool> UpdateMinute(Minute minute)
        {
            try
            {
                minute.UpdatedAt = DateTime.Now;
                minute.IsActive = true;
                _context.Update(minute);
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
