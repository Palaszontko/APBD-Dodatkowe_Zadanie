using EventAPI.DTOs;
using EventAPI.Exceptions;
using EventAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(IEventService eventService) : ControllerBase {
    [HttpPost]
    public async Task<IActionResult> AddNewEvent([FromBody] PostEventDto postEventDto) {
        try {
            var newEvent = await eventService.CreateEventAsync(postEventDto);
            return Ok(newEvent);
        }
        catch (NotFoundException e) {
            return NotFound(e.Message);
        }
        catch (EventInPastException e) {
            return BadRequest(e.Message);
        }
    }

}