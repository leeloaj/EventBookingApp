using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

/// <summary>
/// Service for creating and register to an event
/// </summary>
/// <param name="context"></param>
public class EventsService(DataContext context) : IEventsService
{
    /// <summary>
    /// Creating event
    /// </summary>
    /// <param name="eventDto"></param>
    public async Task CreateEvent(CreateEventDto eventDto)
    {
        var newEvent = new Event
        {
            Date = eventDto.Date,
            Name = eventDto.Name,
            MaxParticipants = eventDto.MaxParticipants
        };

        context.Events.Add(newEvent);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Register to event
    /// </summary>
    /// <param name="createRegisterDto"></param>
    /// <exception cref="ArgumentException"></exception>
    public async Task Register(CreateRegisterDto createRegisterDto)
    {
        var dbEvent = await context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.Id == createRegisterDto.EventId);
        
        if (dbEvent == null)
        {
            throw new ArgumentException("Üritust ei leitud!");
        }
        
        var hasRegistration = dbEvent.Registrations.Any(e => e.IdCode == createRegisterDto.IdCode);
        if (hasRegistration)
        {
            throw new ArgumentException("Oled juba registreerunud sellele üritusele!");
        }

        var isFull = dbEvent.MaxParticipants == dbEvent.Registrations.Count;
        if (isFull)
        {
            throw new ArgumentException("Üritus on täis broneeritud!");
        }
        
        var eventRegistration = new EventRegistration
        {
            EventId = createRegisterDto.EventId,
            FirstName = createRegisterDto.FirstName,
            LastName = createRegisterDto.LastName,
            IdCode = createRegisterDto.IdCode
        };

        context.EventRegistrations.Add(eventRegistration);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Get all created events
    /// </summary>
    /// <returns></returns>
    public async Task<List<EventDto>> GetAllEvents()
    {
        var events = await context.Events
            .Include(e => e.Registrations)
            .ToListAsync();

        return events
            .Select(e => new EventDto
            {
                Id = e.Id,
                Date = e.Date,
                Name = e.Name,
                MaxParticipants = e.MaxParticipants,
                Registrations = e.Registrations
                    .Select(r => new EventRegistrationDto
                    {
                        Id = r.Id,
                        FirstName = r.FirstName,
                        IdCode = r.IdCode,
                        LastName = r.LastName,
                        EventId = r.EventId
                    })
                    .ToList()
            })
            .ToList();
    }
}