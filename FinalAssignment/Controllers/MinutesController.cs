using FinalAssignment.DAL.Data.Models;
using FinalAssignment.Services.InputModel;
using FinalAssignment.Services.Services;
using FinalAssignment.Services.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalAssignment.Controllers
{
    public class MinutesController : Controller
    {
        private readonly IMinuteService _context;
        private readonly ICrewService _crewService;
        private readonly IEmployeeService _employeeService;

        public MinutesController(IMinuteService context, ICrewService crewService, IEmployeeService employeeService)
        {
            _context = context;
            this._crewService = crewService;
            this._employeeService = employeeService;
        }

        // GET: Minutes
        public async Task<IActionResult> Index()
        {
            ViewData["CrewID"] = new SelectList(await _crewService.GetAllCrews(), "ID", "Name");
            var supervisors = await _employeeService.GetEmployeeByType(EmployeeType.Supervisor);
            ViewData["SupervisorID"] = supervisors;
            var minutes = await _context.GetAllMinutes();
            var res = await _context.GetSupervisorByMinute(8);
            return View(minutes.ToList());
        }

        // GET: Minutes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var minute = await _context.GetMinuteByID(id);

            var employee = new List<Employee>();

            minute.SupervisorID.ToList().ForEach(async el =>
            {
                var res = await _employeeService.GetEmployeeByID(el.SupervisorID);
                if (res != null)
                {
                    employee.Add(res);
                }
            });

            if (minute == null)
            {
                return NotFound();
            }

            var data = new MinuteViewModel()
            {
                ID = minute.ID,
                MinuteType = minute.MinuteType,
                ApprovalStatus = minute.ApprovalStatus,
                ApprovalHistory = minute.ApprovalHistory,
                Crews = minute.Crews,
                Date = minute.Date.ToString("dd/mm/yyyy"),
                Topic = minute.Topic,
                Employee = employee
            };


            return View(data);
        }

        // GET: Minutes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CrewID"] = new SelectList(await _crewService.GetAllCrews(), "ID", "Name");
            var supervisors = await _employeeService.GetEmployeeByType(EmployeeType.Supervisor);
            ViewData["SupervisorID"] = supervisors;
            return View();
        }

        public async Task<IActionResult> Search([Bind("Crew,Supervisor,Type,Month,Year,PageNumber,ResultsPerPage")] SearchModel filters, IFormCollection formData)
        {
            var result = await _context.GetAllMinutes();
            filters.PageNumber = filters.PageNumber == 0 ? 1 : filters.PageNumber;
            filters.ResultsPerPage = filters.ResultsPerPage <= 0 ? 5 : filters.ResultsPerPage;

            var skip = (filters.PageNumber - 1) * filters.ResultsPerPage;
            filters.PageNumber = skip >= result.Count() ? 1 : filters.PageNumber;
            skip = (filters.PageNumber - 1) * filters.ResultsPerPage;

            if (filters.Year >= 1800)
            {
                result = result.Where(min => min.Date.Year == filters.Year);
            }

            if (filters.Crew != null)
            {
                result = result.Where(min => min.CrewID.ToString().Equals(filters.Crew));
            }

            if (filters.Month != Month.Months)
            {
                result = result.Where(min => (int)(min.Date.Month) == (int)(filters.Month));
            }

            if (filters.Type != MinuteType.Minute)
            {
                result = result.Where(min => min.MinuteType == filters.Type);
            }


            if (result == null)
            {
                return NotFound();
            }

            var data = result.Skip(skip).Take(filters.ResultsPerPage).OrderByDescending(min => min.ID).Select(min => new MinuteViewModel
            {
                ID = min.ID,
                Topic = min.Topic,
                MinType = Enum.GetName(typeof(MinuteType), min.MinuteType),
                Date = min.Date.ToString("dd/MM/yyyy"),
                ApprovalStatus = min.ApprovalStatus,
                ApprovalHistory = min.ApprovalHistory,
                Crews = min.Crews,
                Supervisor = min.SupervisorID
            });

            return Ok(new
            {
                status = "Success",
                data
            });
        }

        // POST: Minutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, MinuteType, Topic, Date, CrewID, SupervisorID")] MinutesInputModel minuteReceived, IFormCollection formData)
        {
            if (ModelState.IsValid)
            {
                var super = formData["SupervisorID"];
                var supervisorID = super.ToString().Split(",");

                var minute = new Minute
                {
                    MinuteType = minuteReceived.MinuteType,
                    CrewID = minuteReceived.CrewID,
                    Date = minuteReceived.Date,
                    Topic = minuteReceived.Topic
                };

                foreach (var superID in supervisorID)
                {
                    if (superID != "")
                    {
                        minute.SupervisorID.Add(new Supervisor() { SupervisorID = Convert.ToInt32(superID) });
                    }
                }

                await _context.CreateMinute(minute);

                return RedirectToAction(nameof(Index));
            }
            ViewData["CrewID"] = new SelectList(await _crewService.GetAllCrews(), "ID", "Name", minuteReceived.CrewID);
            ViewData["SupervisorID"] = await _employeeService.GetEmployeeByType(EmployeeType.Supervisor);
            return View(minuteReceived);
        }

        // GET: Minutes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var minute = await _context.GetMinuteByID(id);
            if (minute == null)
            {
                return NotFound();
            }
            ViewData["CrewID"] = new SelectList(await _crewService.GetAllCrews(), "ID", "Name", minute.CrewID);
            ViewData["SupervisorID"] = new SelectList(await _employeeService.GetEmployeeByType(EmployeeType.Supervisor), "ID", "Name");
            return View(minute);
        }

        // POST: Minutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MinuteType,Topic,Date,CrewID,ApprovalStatus,ApprovalHistory,CreatedAt,UpdatedAt,IsActive")] Minute receivedMinute)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var minute = await _context.GetMinuteByID(receivedMinute.ID);
                    minute.MinuteType = receivedMinute.MinuteType;
                    minute.ApprovalHistory = minute.ApprovalStatus;
                    minute.ApprovalStatus = receivedMinute.ApprovalStatus;

                    await _context.UpdateMinute(minute);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MinuteExists(receivedMinute.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CrewID"] = new SelectList(await _crewService.GetAllCrews(), "ID", "Name", receivedMinute.CrewID);
            ViewData["SupervisorID"] = new SelectList(await _employeeService.GetEmployeeByType(EmployeeType.Supervisor), "ID", "Name");
            return View(receivedMinute);
        }

        // GET: Minutes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var minute = await _context.GetMinuteByID(id);
            if (minute == null)
            {
                return NotFound();
            }

            return View(minute);
        }

        // POST: Minutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteMinute(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MinuteExists(int id)
        {
            return _context.GetMinuteByID(id) != null;
        }
    }
}
