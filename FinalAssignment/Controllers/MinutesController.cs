﻿using FinalAssignment.DAL.Data.Models;
using FinalAssignment.Services.InputModel;
using FinalAssignment.Services.Services;
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
            return View(minutes.ToList());
        }

        // GET: Minutes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var minute = await _context.GetMinuteByID(id);
            if (minute == null)
            {
                return NotFound();
            }

            return View(minute);
        }

        // GET: Minutes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CrewID"] = new SelectList(await _crewService.GetAllCrews(), "ID", "Name");
            var supervisors = await _employeeService.GetEmployeeByType(EmployeeType.Supervisor);
            ViewData["SupervisorID"] = supervisors;
            return View();
        }

        public async Task<IActionResult> Search([Bind("Crew,Supervisor,Type,Month,Year")] SearchModel filters)
        {
            return NotFound();
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