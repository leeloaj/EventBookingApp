namespace Services.Dtos;

public class CreateEventDto
{
    public required string Name { get; set; }

    public required DateTime Date { get; set; }
    
    public required int MaxParticipants { get; set; }
}