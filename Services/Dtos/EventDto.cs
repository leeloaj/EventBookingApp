namespace Services.Dtos;

public class EventDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required DateTime Date { get; set; }
    
    public required int MaxParticipants { get; set; }

    public List<EventRegistrationDto> Registrations { get; set; } = [];
}