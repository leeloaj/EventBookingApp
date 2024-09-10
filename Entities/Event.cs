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

    /// <summary>
    /// Date of the event
    /// </summary>
    public required DateTime Date { get; set; }
    
    /// <summary>
    /// Max participants of the event
    /// </summary>
    public required int MaxParticipants { get; set; }

    /// <summary>
    /// Registrations of the event
    /// </summary>
    public List<EventRegistration> Registrations { get; set; } = [];
}