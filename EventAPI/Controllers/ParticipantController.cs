using EventAPI.DTOs;
using EventAPI.Exceptions;
using EventAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ParticipantController(IParticipantService participantService) : ControllerBase {
    [HttpPost("{participantId}/register/{eventId}")]
    public async Task<IActionResult> RegisterParticipantForEvent([FromRoute] int participantId, [FromRoute] int eventId) {
        try {
            var registration = await participantService.AddParticipantToEvent(participantId, eventId);
            return Ok(registration);
        }
        catch (NotFoundException e) {
            return NotFound(e.Message);
        }
        catch (TooManyPeopleException e) {
            return BadRequest(e.Message);
        }
        catch (ParticipantAlreadyRegisteredException e) {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{participantId}/{eventId}")]
    public async Task<IActionResult> CancelRegister([FromRoute] int participantId, [FromRoute] int eventId) {
        try {
            await participantService.CancelParticipantRegister(participantId, eventId);
            return NoContent();
        }
        catch (NotFoundException e) {
            return NotFound(e.Message);
        }
        catch (CancelRegisterImpossibleException e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("raport")]
    public async Task<IActionResult> GetReportForParticipant() {
        return Ok(await participantService.GetReportForParticipant());
    }
}