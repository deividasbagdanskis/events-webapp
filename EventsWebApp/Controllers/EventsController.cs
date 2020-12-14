using EventsWebApp.Context;
using EventsWebApp.Models;
using EventsWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EventsWebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsWebAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventsController(EventsWebAppContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-Us");
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
                events = await _context.Event.Include(e => e.Category).Include(e => e.EventAttendees).ToListAsync();
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
            userEventsViewModel.UsersCreatedEvents = await _context.Event.Include(e => e.Category)
                                                                         .Include(e => e.EventAttendees)
                                                                         .Where(e => e.UserId == userId)
                                                                         .ToListAsync();

            var userAttend = await _context.EventAttendee.Include(e => e.Event)
                                                         .Include(e => e.Event.Category)
                                                         .Where(e => e.UserId == userId)
                                                         .ToListAsync();

            userEventsViewModel.UsersAttendEvents = userAttend.Select(e => e.Event).ToList();

            return View(userEventsViewModel);
        }

        // GET: EventsController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event @event = await _context.Event.Include(e => e.EventAttendees).Where(e => e.Id == id).FirstOrDefaultAsync();

            @event.Category = await _context.Category.FindAsync(@event.CategoryId);

            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            EventAttendee userAttendEvent = await _context.EventAttendee.Include(e => e.Event)
                                                         .Where(e => e.UserId == userId && e.EventId == id)
                                                         .FirstOrDefaultAsync();

            bool userWillAttend = false;

            if (userAttendEvent != null)
            {
                userWillAttend = true;
            }

            ViewData["UserWillAttend"] = userWillAttend;

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: EventsController/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var categories = await _context.Category.ToListAsync();

            ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            return View();
        }

        // POST: EventsController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event, DateTime date, TimeSpan time,
            [FromForm(Name = "imageFile")] IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                @event.UserId = userId;

                @event.DateAndTime = date + time;

                string uniqueFileName = null;

                if (imageFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    imageFile.CopyTo(new FileStream(filePath, FileMode.Create));

                    @event.ImageName = uniqueFileName;
                }

                try
                {
                    _context.Event.Add(@event);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    var categories = await _context.Category.ToListAsync();

                    ViewData["Categories"] = new SelectList(categories, "Id", "Name");

                    return View(@event);
                }

                return RedirectToAction(nameof(IndexUserEvents));
            }

            ViewData["Categories"] = new SelectList(await _context.Category.ToListAsync(), "Id", "Name");

            return View(@event);
        }

        // GET: EventsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event @event = await _context.Event.Include(e => e.Category).Where(e => e.Id == id).FirstOrDefaultAsync();

            //@event.Category = await _context.Category.FindAsync(@event.CategoryId);

            var categories = await _context.Category.ToListAsync();

            ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            return View(@event);
        }

        // POST: EventsController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Event @event, DateTime date, TimeSpan time,
            [FromForm(Name = "imageFile")] IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                @event.UserId = userId;

                @event.DateAndTime = date + time;

                Event oldEvent = await _context.Event.FindAsync(id);

                string uniqueFileName = null;

                if (imageFile != null)
                {
                    string imageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string imagePath = Path.Combine(imageFolder, oldEvent.ImageName);

                    FileInfo fileInfo = new FileInfo(imagePath);

                    System.IO.File.Delete(imagePath);

                    fileInfo.Delete();

                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    imageFile.CopyTo(new FileStream(filePath, FileMode.Create));

                    @event.ImageName = uniqueFileName;
                }
                else
                {
                    @event.ImageName = oldEvent.ImageName;
                }

                try
                {
                    _context.Entry(oldEvent).State = EntityState.Detached;
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    var categories = await _context.Category.ToListAsync();

                    ViewData["Categories"] = new SelectList(categories, "Id", "Name");

                    return View(@event);
                }

                return RedirectToAction(nameof(IndexUserEvents));
            }

            ViewData["Categories"] = new SelectList(await _context.Category.ToListAsync(), "Id", "Name");

            return View(@event);
        }

        // GET: EventsController/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event @event = await _context.Event.Include(e => e.EventAttendees).Where(e => e.Id == id).FirstOrDefaultAsync();

            @event.Category = await _context.Category.FindAsync(@event.CategoryId);

            return View(@event);
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Event @event = await _context.Event.Include(e => e.EventAttendees).Where(e => e.Id == id).FirstOrDefaultAsync();

            var eventAttendees = await _context.EventAttendee.Where(e => e.EventId == id).ToListAsync();

            if (@event.ImageName != null)
            {
                string imageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string imagePath = Path.Combine(imageFolder, @event.ImageName);

                FileInfo fileInfo = new FileInfo(imagePath);

                fileInfo.Delete();
            }

            try
            {
                _context.EventAttendee.RemoveRange(eventAttendees);
                _context.Event.Remove(@event);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexUserEvents));
            }
            catch
            {
                return View(id);
            }
        }
    }
}
