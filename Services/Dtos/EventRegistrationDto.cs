namespace Services.Dtos;

public class EventRegistrationDto
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string IdCode { get; set; }

    public int EventId { get; set; }
}