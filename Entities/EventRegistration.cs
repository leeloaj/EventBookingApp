using System.ComponentModel.DataAnnotations;

namespace Entities;

public class EventRegistration
{
    public int Id { get; set; }

    [MaxLength(100)] public required string FirstName { get; set; }

    [MaxLength(100)] public required string LastName { get; set; }

    [MaxLength(11)] public required string IdCode { get; set; }

    public required int EventId { get; set; }
    public Event Event { get; set; } = default!;
}