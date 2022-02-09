using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using StudentPlatformAPI.dto;
using StudentPlatformAPI.services;

namespace StudentPlatformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        private ICalendarEventService _service;
        public CalendarEventsController(ICalendarEventService service)
        {
            _service = service;
        }


        [Route("monthlyEvents/{studentId}")]
        [HttpGet]
        public IActionResult GetMonthlyEvents(int studentId, DateTime date)
        {
            return Ok(_service.getMonthlyCalendarEvents(date, studentId));
        }

        [Route("weeklyEvents/{studentId}")]
        [HttpGet]
        public IActionResult GetWeeklyCalendarEvents(int studentId, [FromQuery] IEnumerable<DateTime> day)
        {
            return Ok(_service.getWeeklyCalendarEvents(day, studentId));
        }



        

        // POST api/<CalendarEventsController>
        [HttpPost]
        public IActionResult Post([FromBody] CalendarEventDto dto)
        {
            try
            {
                return Created("", _service.createCalendarEvent(dto));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<CalendarEventsController>/5
        [HttpPut]
        public IActionResult Put([FromBody]CalendarEventDto dto)
        {
            try
            {
                _service.updateCalendarEvent(dto);
                return NoContent();
            }
            catch(Exception e)
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
                _service.deleteCalendarEvent(id);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
