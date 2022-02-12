using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Models.Auth;
using StudentPlatformAPI.Services;

namespace StudentPlatformAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        private readonly ICalendarEventService _service;
        private readonly UserManager<User> _userManager;

        public CalendarEventsController(ICalendarEventService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("MonthlyEvents")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMonthlyEvents(DateTime date)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                return Ok(_service.getMonthlyCalendarEvents(date, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("WeeklyEvents")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetWeeklyCalendarEvents([FromQuery] IEnumerable<DateTime> day)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                return Ok(_service.getWeeklyCalendarEvents(day, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // POST api/<CalendarEventsController>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CalendarEventDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                return Created("", _service.createCalendarEvent(dto, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<CalendarEventsController>/5
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] CalendarEventDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.updateCalendarEvent(dto, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<CalendarEventsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.deleteCalendarEvent(id, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}