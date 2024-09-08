using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

public class EventsService(DataContext context) : IEventsService
{
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

    public async Task Register(CreateRegisterDto createRegisterDto)
    {
        var dbEvent = await context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.Id == createRegisterDto.EventId);
        
        if (dbEvent == null)
        {
            throw new ArgumentException("Event doesn't exist!");
        }
        
        var hasRegistration = dbEvent.Registrations.Any(e => e.IdCode == createRegisterDto.IdCode);
        if (hasRegistration)
        {
            throw new ArgumentException("You already have registered to this event!");
        }

        var isFull = dbEvent.MaxParticipants == dbEvent.Registrations.Count;
        if (isFull)
        {
            throw new ArgumentException("Event is full!");
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