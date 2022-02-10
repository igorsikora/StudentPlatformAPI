using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Services;

namespace StudentPlatformAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        private readonly ICalendarEventService _service;

        public CalendarEventsController(ICalendarEventService service)
        {
            _service = service;
        }


        [Route("MonthlyEvents")]
        [HttpGet]
        public IActionResult GetMonthlyEvents(DateTime date)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                return Ok(_service.getMonthlyCalendarEvents(date, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("WeeklyEvents")]
        [HttpGet]
        public IActionResult GetWeeklyCalendarEvents([FromQuery] IEnumerable<DateTime> day)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                return Ok(_service.getWeeklyCalendarEvents(day, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        // POST api/<CalendarEventsController>
        [HttpPost]
        public IActionResult Post([FromBody] CalendarEventDto dto)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                return Created("", _service.createCalendarEvent(dto, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<CalendarEventsController>/5
        [HttpPut]
        public IActionResult Put([FromBody] CalendarEventDto dto)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                _service.updateCalendarEvent(dto, userId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/<CalendarEventsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                _service.deleteCalendarEvent(id, userId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}