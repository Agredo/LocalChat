using AgredoApplication.MVVM.Services.Models;

namespace AgredoApplication.MVVM.Services.Abstractions.Calendar;

public interface ICalendar
{
    Task<IEnumerable<string>> GetCalendarIDs();
    Task<string> AddEvent(Event calendarEvent);
    Task<string> AddEvent(string calendarId, string title, string description, string location, DateTimeOffset startdDate, DateTimeOffset endDate, bool isAllDay = false);
    void DeleteEvent(string eventId);
    void GetEvents();
    void UpdateEvent();
}
