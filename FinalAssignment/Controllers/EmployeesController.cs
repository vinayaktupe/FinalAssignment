using FinalAssignment.DAL.Data.Models;
using FinalAssignment.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace FinalAssignment.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _context;

        public EmployeesController(IEmployeeService context)
        {
            this._context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var result = await _context.GetAllEmployees();
            return View(result.ToList());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _context.GetEmployeeByID(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,EmployeeType,CreatedAt,UpdatedAt,IsActive")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _context.CreateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.GetEmployeeByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,EmployeeType,CreatedAt,UpdatedAt,IsActive")] Employee receivedEmployee)
        {
            if (id != receivedEmployee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = await _context.GetEmployeeByID(receivedEmployee.ID);
                    employee.Name = receivedEmployee.Name;
                    employee.EmployeeType = receivedEmployee.EmployeeType;
                    employee.UpdatedAt = DateTime.Now;
                    await _context.UpdateEmployee(receivedEmployee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(receivedEmployee.ID))
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
            return View(receivedEmployee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.GetEmployeeByID(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.GetEmployeeByID(id) != null;
        }
    }
}
