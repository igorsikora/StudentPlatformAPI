using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentPlatformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _service;
        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [Route("{statusId}")]
        [HttpGet]
        [Authorize]
        public IActionResult Get(int statusId)
        {
            var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            return Ok(_service.getTasks(statusId, (userId))); 
        }
        

        // POST api/<TaskController>
        [HttpPost]
        public IActionResult Post([FromBody] TaskDto dto)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                _service.createTask(dto, userId);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<TaskController>
        [HttpPut]
        public IActionResult Put([FromBody]TaskDto dto)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                _service.updateTask(dto, userId);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                _service.deleteTask(id, userId);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/<TaskController>
        [HttpDelete]
        public IActionResult Delete(List<TaskDto> tasks)
        {
            try
            {
                var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
                _service.deleteTasks(tasks, userId);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
