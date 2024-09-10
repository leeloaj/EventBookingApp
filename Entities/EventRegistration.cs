using System.ComponentModel.DataAnnotations;

namespace Entities;

/// <summary>
/// Event registrations 
/// </summary>
public class EventRegistration
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Firstname of the participants
    /// </summary>
    [MaxLength(100)] public required string FirstName { get; set; }

    /// <summary>
    /// Lastname of the participant
    /// </summary>
    [MaxLength(100)] public required string LastName { get; set; }

    /// <summary>
    /// Idcode of the participant
    /// </summary>
    [MaxLength(11)] public required string IdCode { get; set; }

    /// <summary>
    /// Event id.
    /// </summary>
    public required int EventId { get; set; }
    
    /// <summary>
    /// Event
    /// </summary>
    public Event Event { get; set; } = default!;
}