using System.Text.Json.Serialization;

namespace AgredoApplication.MVVM.Services.Models;

public record Event
{
    //
    // Zusammenfassung:
    //     Gets the unique identifier for this event.
    public string Id { get; }

    //
    // Zusammenfassung:
    //     Gets the unique identifier for the calendar this event is part of.
    public string CalendarId { get; set; }

    //
    // Zusammenfassung:
    //     Gets the title for this event.
    [JsonPropertyName("Titel")]
    public string Title { get; }

    //
    // Zusammenfassung:
    //     Gets the description for this event.
    [JsonPropertyName("Beschreibung")]
    public string Description { get; set; } = string.Empty;


    //
    // Zusammenfassung:
    //     Gets the location for this event.
    public string Location { get; set; } = string.Empty;


    //
    // Zusammenfassung:
    //     Gets whether this event is marked as an all-day event.
    [JsonPropertyName("Ganztägig")]
    public bool IsAllDay { get; set; }

    //
    // Zusammenfassung:
    //     Gets the start date and time for this event.
    [JsonPropertyName("StartDatum")]
    public DateTime StartDate { get; set; }

    //
    // Zusammenfassung:
    //     Gets the end date and time for this event.
    [JsonPropertyName("EndDatum")]
    public DateTime EndDate { get; set; }

    //
    // Zusammenfassung:
    //     Gets the total duration for this event.
    public TimeSpan Duration => EndDate - StartDate;

    ////
    //// Zusammenfassung:
    ////     Gets the list of attendees for this event.
    //public IEnumerable<CalendarEventAttendee> Attendees { get; internal set; } = new List<CalendarEventAttendee>();


    //
    // Zusammenfassung:
    //     Initializes a new instance of the Plugin.Maui.CalendarStore.CalendarEvent class.
    //
    //
    // Parameter:
    //   id:
    //     The unique identifier for this event.
    //
    //   calendarId:
    //     The unique identifier for the calendar this event is part of.
    //
    //   title:
    //     The title for this event.
    public Event(string id, string calendarId, string title)
    {
        Id = id;
        CalendarId = calendarId;
        Title = title;
    }
}
