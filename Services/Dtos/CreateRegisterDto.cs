namespace Services.Dtos;

public class CreateRegisterDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string IdCode { get; set; }

    public int EventId { get; set; }
}