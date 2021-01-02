using EventsWebApp.Models;
using EventsWebApp.Repositories;
using EventsWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EventsWebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventAttendeeRepository _eventAttendeeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventsController(IEventRepository eventRepository, IEventAttendeeRepository eventAttendeeRepository,
            ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor, 
            IWebHostEnvironment webHostEnvironment)
        {
            _eventRepository = eventRepository;
            _eventAttendeeRepository = eventAttendeeRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-Us");
        }

        // GET: EventsController
        [Authorize]
        public async Task<ActionResult> Index(string city = null, int categoryId = 0, int date = 0)
        {
            List<Event> events;

            DateTime dateTimeInterval = DateTime.Today;
            
            if (date != 0)
            {
                dateTimeInterval = DateTime.Today.AddDays(date);
            }
            
            if (!string.IsNullOrWhiteSpace(city) && categoryId != 0 && date != 0)
            {
                events = await _eventRepository.GetEventsByCityCategoryDate(city, categoryId, dateTimeInterval);
            }
            else if (!string.IsNullOrWhiteSpace(city) && categoryId != 0)
            {
                events = await _eventRepository.GetEventsByCityCategory(city, categoryId);
            } 
            else if (!string.IsNullOrWhiteSpace(city) && date != 0)
            {
                events = await _eventRepository.GetEventsByCityDate(city, dateTimeInterval);
            }
            else if (categoryId != 0 && date != 0)
            {
                events = await _eventRepository.GetEventsByCategoryDate(categoryId, dateTimeInterval);
            }
            else if (!string.IsNullOrWhiteSpace(city))
            {
                events = await _eventRepository.GetEventsByCity(city);
            }
            else if (categoryId != 0)
            {
                events = await _eventRepository.GetEventsByCategory(categoryId);
            }
            else if (date != 0)
            {
                events = await _eventRepository.GetEventsByDate(dateTimeInterval);
            }
            else
            {
                events = await _eventRepository.GetAllEvents();
            }

            var categories = await _categoryRepository.GetAll();

            categories.Insert(0, new Category() { Id = 0, Name = "All" });

            ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            return View(events);
        }

        [Authorize]
        public async Task<ActionResult> IndexUserEvents()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserEventsViewModel userEventsViewModel = new UserEventsViewModel();
            userEventsViewModel.UsersCreatedEvents = await _eventRepository.GetUsersCreatedEvents(userId);
            userEventsViewModel.UsersAttendEvents = await _eventRepository.GetEventsWhichUserWillAttend(userId);

            return View(userEventsViewModel);
        }

        // GET: EventsController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event @event = await _eventRepository.GetEvent((int)id);

            @event.Category = await _categoryRepository.Get(@event.CategoryId);

            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            EventAttendee userAttendEvent = await _eventAttendeeRepository.GetUserAttendEvent(userId, (int)id);

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
            var categories = await _categoryRepository.GetAll();

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

                    using FileStream fileStream = new FileStream(filePath, FileMode.Create);

                    imageFile.CopyTo(fileStream);

                    @event.ImageName = uniqueFileName;
                }

                try
                {
                    await _eventRepository.Add(@event);
                }
                catch
                {
                    var categories = await _categoryRepository.GetAll();

                    ViewData["Categories"] = new SelectList(categories, "Id", "Name");

                    return View(@event);
                }

                return RedirectToAction(nameof(IndexUserEvents));
            }

            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAll(), "Id", "Name");

            return View(@event);
        }

        // GET: EventsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event @event = await _eventRepository.GetEvent((int)id);

            var categories = await _categoryRepository.GetAll();

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

                Event oldEvent = await _eventRepository.GetEvent(id);

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
                    await _eventRepository.Update(@event, oldEvent);
                }
                catch
                {
                    var categories = await _categoryRepository.GetAll();

                    ViewData["Categories"] = new SelectList(categories, "Id", "Name");

                    return View(@event);
                }

                return RedirectToAction(nameof(IndexUserEvents));
            }

            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAll(), "Id", "Name");

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

            Event @event = await _eventRepository.GetEvent((int)id);

            @event.Category = await _categoryRepository.Get(@event.CategoryId);

            return View(@event);
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Event @event = await _eventRepository.GetEvent(id);

            if (@event.ImageName != null)
            {
                string imageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string imagePath = Path.Combine(imageFolder, @event.ImageName);

                FileInfo fileInfo = new FileInfo(imagePath);

                fileInfo.Delete();
            }

            try
            {
                await _eventRepository.Delete(id);

                return RedirectToAction(nameof(IndexUserEvents));
            }
            catch
            {
                return View(id);
            }
        }
    }
}
