using System.ComponentModel.DataAnnotations;

namespace Entities;

/// <summary>
/// Event
/// </summary>
public class Event
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the event
    /// </summary>
    [MaxLength(500)] public required string Name { get; set; }

    public required DateTime Date { get; set; }
    
    public required int MaxParticipants { get; set; }

    public List<EventRegistration> Registrations { get; set; } = [];
}