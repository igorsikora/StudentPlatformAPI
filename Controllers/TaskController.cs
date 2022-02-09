using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentPlatformAPI.dto;
using StudentPlatformAPI.services;

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

        [Route("tasks/{statusId}")]
        [HttpGet]
        public IActionResult GetToDo(int statusId)
        {
            return Ok(_service.getTasks(statusId));
        }
        

        // POST api/<TaskController>
        [HttpPost]
        public IActionResult Post([FromBody] TaskDto dto)
        {
            try
            {
                _service.createTask(dto);
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
                _service.updateTask(dto);
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
                _service.deleteTask(id);
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
                _service.deleteTasks(tasks);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
