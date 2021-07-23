using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SES.Data;
using SES.Data.Entities;

namespace SES.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class LogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Logs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Logs.ToListAsync());
        }

        // GET: Admin/Logs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntity = await _context.Logs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logEntity == null)
            {
                return NotFound();
            }

            return View(logEntity);
        }

        // GET: Admin/Logs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Logs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pin,Date,Status,Message,Method,Description")] LogEntity logEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logEntity);
        }

        // GET: Admin/Logs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntity = await _context.Logs.FindAsync(id);
            if (logEntity == null)
            {
                return NotFound();
            }
            return View(logEntity);
        }

        // POST: Admin/Logs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pin,Date,Status,Message,Method,Description")] LogEntity logEntity)
        {
            if (id != logEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogEntityExists(logEntity.Id))
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
            return View(logEntity);
        }

        // GET: Admin/Logs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntity = await _context.Logs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logEntity == null)
            {
                return NotFound();
            }

            return View(logEntity);
        }

        // POST: Admin/Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logEntity = await _context.Logs.FindAsync(id);
            _context.Logs.Remove(logEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogEntityExists(int id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}
