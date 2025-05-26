using Microsoft.AspNetCore.Mvc;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Controllers
{
    public class BugController : Controller
    {
        // Temporary in-memory storage for bugs
        private static List<Bug> _bugs = new List<Bug>
        {
            new Bug
            {
                Id = 1,
                Title = "Login page error",
                Description = "Users cannot log in when using Firefox",
                CreatedDate = DateTime.Now.AddDays(-5),
                CreatedBy = "john.doe@example.com",
                IsResolved = false,
                Priority = PriorityLevel.High
            },
            new Bug
            {
                Id = 2,
                Title = "Typo on homepage",
                Description = "There's a typo in the welcome message",
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "jane.smith@example.com",
                IsResolved = true,
                ResolvedDate = DateTime.Now.AddDays(-1),
                Priority = PriorityLevel.Low
            }
        };

        // GET: /Bug
        public IActionResult Index()
        {
            return View(_bugs);
        }

        // GET: /Bug/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Bug/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bug bug)
        {
            if (ModelState.IsValid)
            {
                bug.Id = _bugs.Count > 0 ? _bugs.Max(b => b.Id) + 1 : 1;
                bug.CreatedDate = DateTime.Now;
                bug.CreatedBy = "current.user@example.com"; // In a real app, this would come from authentication
                bug.IsResolved = false;
                
                _bugs.Add(bug);
                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }

        // GET: /Bug/Edit/5
        public IActionResult Edit(int id)
        {
            var bug = _bugs.FirstOrDefault(b => b.Id == id);
            if (bug == null)
            {
                return NotFound();
            }
            return View(bug);
        }

        // POST: /Bug/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Bug bug)
        {
            if (id != bug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingBug = _bugs.FirstOrDefault(b => b.Id == id);
                if (existingBug == null)
                {
                    return NotFound();
                }

                existingBug.Title = bug.Title;
                existingBug.Description = bug.Description;
                existingBug.Priority = bug.Priority;
                
                // If bug is being resolved now
                if (!existingBug.IsResolved && bug.IsResolved)
                {
                    existingBug.ResolvedDate = DateTime.Now;
                }
                // If bug is being reopened
                else if (existingBug.IsResolved && !bug.IsResolved)
                {
                    existingBug.ResolvedDate = null;
                }
                
                existingBug.IsResolved = bug.IsResolved;

                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }
    }
}
