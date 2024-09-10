using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace WebApi.Controllers;

/// <summary>
/// Controller for getting and creating events
/// </summary>
/// <param name="eventsService"></param>
[ApiController]
[Route("api/[controller]")]
public class EventsController(IEventsService eventsService) : Controller
{
    /// <summary>
    /// Get request to get all created events
    /// </summary>
    /// <returns>all created events</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<EventDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var events = await eventsService.GetAllEvents();
        
        return Ok(events);
    }

    /// <summary>
    /// Post request to create event
    /// </summary>
    /// <param name="eventDto"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> Create(CreateEventDto eventDto)
    {
        await eventsService.CreateEvent(eventDto);

        return NoContent();
    }

    /// <summary>
    /// Post request to register to event
    /// </summary>
    /// <param name="createRegisterDto"></param>
    /// <returns></returns>
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