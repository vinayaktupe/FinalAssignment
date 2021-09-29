using FinalAssignment.DAL.Data.Models;
using FinalAssignment.DAL.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment.Services.Services
{
    public interface ICrewService
    {
        public Task<IEnumerable<Crew>> GetAllCrews();
        public Task<Crew> GetCrewByID(int id);
        public Task<bool> CreateCrew(Crew crew);
        public Task<bool> UpdateCrew(Crew crew);
        public Task<bool> DeleteCrew(int id);
    }

    public class CrewService : ICrewService
    {
        private readonly GenericRepository<Crew> _context;
        private readonly ILogger<CrewService> _logger;

        public CrewService(ILogger<CrewService> logger)
        {
            this._context = new GenericRepository<Crew>();
            this._logger = logger;
        }

        public async Task<bool> CreateCrew(Crew crew)
        {
            try
            {
                _context.Create(crew);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return false;
        }

        public async Task<bool> DeleteCrew(int id)
        {
            try
            {
                var crew = _context.GetById(id);
                if (crew == null)
                {
                    return false;
                }

                crew.IsActive = false;
                crew.UpdatedAt = DateTime.Now;
                _context.Update(crew);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return false;
        }

        public async Task<IEnumerable<Crew>> GetAllCrews()
        {
            try
            {
                var result = _context.GetByCondition(crew => crew.IsActive != false).OrderByDescending(crew=>crew.ID);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<Crew> GetCrewByID(int id)
        {
            try
            {
                return _context.GetByCondition(crew => crew.IsActive == true && crew.ID == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Source} - {ex.Message}");
            }
            return null;
        }

        public async Task<bool> UpdateCrew(Crew crew)
        {
            try
            {
                crew.UpdatedAt = DateTime.Now;
                crew.IsActive = true;
                _context.Update(crew);
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
