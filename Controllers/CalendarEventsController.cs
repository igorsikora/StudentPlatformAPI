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
using Swashbuckle.AspNetCore.Filters;

namespace StudentPlatformAPI.Controllers
{
    /// <summary>
    ///     CRUD for CalendarEvents
    /// </summary>
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

        /// <summary>
        ///     Get all CalendarEvents from given month
        /// </summary>
        /// <param name="date" example="2022/01/15"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MonthlyEvents")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMonthlyEvents([FromQuery] DateTime date)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                return Ok(_service.GetMonthlyCalendarEvents(date, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Get all CalendarEvents from given days
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
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
                return Ok(_service.GetWeeklyCalendarEvents(day, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Create CalendarEvent
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CalendarEventDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                return Created("", _service.CreateCalendarEvent(dto, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Update CalendarEvent
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] CalendarEventDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.UpdateCalendarEvent(dto, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Delete CalendarEvent
        /// </summary>
        /// <param name="id" example="5"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.DeleteCalendarEvent(id, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #region CalendarEventDtoExample

        public class CalendarEventDtoExample : IExamplesProvider<CalendarEventDto>
        {
            public CalendarEventDto GetExamples()
            {
                return new CalendarEventDto
                {
                    Id = 15,
                    Title = "event",
                    Description = "desc",
                    EndDate = DateTime.Today,
                    StartDate = DateTime.Today.AddHours(2)
                };
            }
        }

        #endregion
    }
}