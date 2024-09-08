using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(IEventsService eventsService) : Controller
{
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<EventDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var events = await eventsService.GetAllEvents();
        
        return Ok(events);
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(CreateEventDto eventDto)
    {
        await eventsService.CreateEvent(eventDto);

        return NoContent();
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(CreateRegisterDto createRegisterDto)
    {
        try
        {
            await eventsService.Register(createRegisterDto);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        return NoContent();
    }
}