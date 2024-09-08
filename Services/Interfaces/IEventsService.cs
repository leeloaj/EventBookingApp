using Services.Dtos;

namespace Services.Interfaces;

/// <summary>
/// Service for events
/// </summary>
public interface IEventsService
{
    /// <summary>
    /// Create event
    /// </summary>
    /// <param name="eventDto">Event dto</param>
    /// <returns></returns>
    Task CreateEvent(CreateEventDto eventDto);

    /// <summary>
    /// Register to event
    /// </summary>
    /// <param name="createRegisterDto">Register dto</param>
    /// <returns></returns>
    Task Register(CreateRegisterDto createRegisterDto);

    /// <summary>
    /// Get all events
    /// </summary>
    /// <returns>List of events</returns>
    Task<List<EventDto>> GetAllEvents();
}