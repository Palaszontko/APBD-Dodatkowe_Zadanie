using EventAPI.DTOs;
using EventAPI.Exceptions;
using EventAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeakerController(ISpeakerService speakerService) : ControllerBase {
    [HttpPost("assign-event")]
    public async Task<IActionResult> AssignSpeakersToEvent([FromBody] AssignSpeakerDto assignSpeakerDto) {
        try {
            var result = await speakerService.AddSpeakersToEventAsync(assignSpeakerDto);
            return Ok(result);
        }
        catch (NotFoundException e) {
            return NotFound(e.Message);
        }
        catch (SpeakerConflictException e) {
            return BadRequest(e.Message);
        }
    }
}