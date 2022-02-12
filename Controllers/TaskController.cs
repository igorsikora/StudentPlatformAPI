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
using Task = StudentPlatformAPI.Models.Task;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentPlatformAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITaskService _service;

        public TaskController(ITaskService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("{statusId}")]
        [ProducesResponseType(typeof(IEnumerable<Task>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int statusId)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value);
            return Ok(_service.getTasks(statusId, user.Id));
        }


        // POST api/<TaskController>
        [HttpPost]
        [ProducesResponseType(typeof(int),(int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] TaskDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                return Created("", _service.createTask(dto, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<TaskController>
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] TaskDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.updateTask(dto, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.deleteTask(id, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<TaskController>
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(List<TaskDto> tasks)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.deleteTasks(tasks, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}