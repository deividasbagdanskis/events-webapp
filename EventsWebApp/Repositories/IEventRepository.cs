using EventsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsWebApp.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEvents();
        Task<List<Event>> GetEventsByCityCategoryDate(string city, int categoryId, DateTime dateTimeInterval);
        Task<List<Event>> GetEventsByCityCategory(string city, int categoryId);
        Task<List<Event>> GetEventsByCityDate(string city, DateTime dateTimeInterval);
        Task<List<Event>> GetEventsByCategoryDate(int categoryId, DateTime dateTimeInterval);
        Task<List<Event>> GetEventsByCity(string city);
        Task<List<Event>> GetEventsByCategory(int categoryId);
        Task<List<Event>> GetEventsByDate(DateTime dateTimeInterval);
        Task<List<Event>> GetUsersCreatedEvents(string userId);
        Task<Event> GetEvent(int id);
        void AddEvent(Event @event);
        void UpdateEvent(Event newEvent, Event oldEvent);
        void DeleteEvent(int id);
    }
}