using EventsWebApp.Context;
using EventsWebApp.Models;
using EventsWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventsWebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsWebAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventsController(EventsWebAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: EventsController
        [Authorize]
        public async Task<ActionResult> Index(string city = null, int categoryId = 0, int date = 0)
        {
            List<Event> events = new List<Event>();

            DateTime dateTimeInterval = DateTime.Today;
            
            if (date != 0)
            {
                dateTimeInterval = DateTime.Today.AddDays(date);
            }
            
                
            if (!string.IsNullOrWhiteSpace(city) && categoryId != 0 && date != 0)
            {
                events = await _context.Event.Include(e => e.Category)
                                            .Include(e => e.EventAttendees)
                                            .Where(e => e.City == city && 
                                            e.CategoryId == categoryId && 
                                            e.DateAndTime <= dateTimeInterval)
                                            .ToListAsync();
            }
            else if (!string.IsNullOrWhiteSpace(city) && categoryId != 0)
            {
                events = await _context.Event.Include(e => e.Category)
                                            .Include(e => e.EventAttendees)
                                            .Where(e => e.City == city &&
                                            e.CategoryId == categoryId)
                                            .ToListAsync();
            } 
            else if (!string.IsNullOrWhiteSpace(city) && date != 0)
            {
                events = await _context.Event.Include(e => e.Category)
                                            .Include(e => e.EventAttendees)
                                            .Where(e => e.City == city &&
                                            e.DateAndTime <= dateTimeInterval)
                                            .ToListAsync();
            }
            else if (categoryId != 0 && date != 0)
            {
                events = await _context.Event.Include(e => e.Category)
                                            .Include(e => e.EventAttendees)
                                            .Where(e => e.CategoryId == categoryId &&
                                            e.DateAndTime <= dateTimeInterval)
                                            .ToListAsync();
            }
            else if (!string.IsNullOrWhiteSpace(city))
            {
                events = await _context.Event.Include(e => e.Category)
                                            .Include(e => e.EventAttendees)
                                            .Where(e => e.City == city)
                                            .ToListAsync();
            }
            else if (categoryId != 0)
            {
                events = await _context.Event.Include(e => e.Category)
                                            .Include(e => e.EventAttendees)
                                            .Where(e => e.CategoryId == categoryId)
                                            .ToListAsync();
            }
            else if (date != 0)
            {
                events = await _context.Event.Include(e => e.Category)
                                            .Include(e => e.EventAttendees)
                                            .Where(e => e.DateAndTime <= dateTimeInterval)
                                            .ToListAsync();
            }
            else
            {
                events = await _context.Event.ToListAsync();
            }

            var categories = await _context.Category.ToListAsync();

            categories.Insert(0, new Category() { Id = 0, Name = "All" });

            ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            return View(events);
        }

        [Authorize]
        public async Task<ActionResult> IndexUserEvents()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserEventsViewModel userEventsViewModel = new UserEventsViewModel();
            userEventsViewModel.UsersCreatedEvents = await _context.Event
                                                                    .Include(e => e.Category)
                                                                    .Include(e => e.EventAttendees)
                                                                    .Where(e => e.UserId == userId)
                                                                    .ToListAsync();

            var userAttend = await _context.EventAttendee.Include(e => e.Event)
                                                         .Where(e => e.UserId == userId)
                                                         .ToListAsync();

            userEventsViewModel.UsersAttendEvents = userAttend.Select(e => e.Event).ToList();

            return View(userEventsViewModel);
        }

        // GET: EventsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventsController/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _context.Category.ToListAsync();

            ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            return View();
        }

        // POST: EventsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
