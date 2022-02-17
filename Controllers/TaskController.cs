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
using Task = StudentPlatformAPI.Models.Task;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentPlatformAPI.Controllers
{
    /// <summary>
    ///     CRUD for Tasks
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;
        private readonly UserManager<User> _userManager;

        public TaskController(ITaskService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        /// <summary>
        ///     Get Tasks with given statusId
        /// </summary>
        /// <param name="statusId" example="1"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{statusId}")]
        [ProducesResponseType(typeof(IEnumerable<Task>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int statusId)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value);
            return Ok(_service.GetTasks(statusId, user.Id));
        }

        /// <summary>
        ///     Create new Task
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] TaskDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                return Created("", _service.CreateTask(dto, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Update Task
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] TaskDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.UpdateTask(dto, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Delete Task
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
                _service.DeleteTask(id, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Delete list of Tasks
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(List<TaskDto> tasks)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                _service.DeleteTasks(tasks, user.Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #region TaskDtoExample

        public class TaskDtoExample : IExamplesProvider<TaskDto>
        {
            public TaskDto GetExamples()
            {
                return new TaskDto
                {
                    Id = 1,
                    StatusId = 0,
                    Title = "Task"
                };
            }
        }

        #endregion

        #region TaskListDtoExample

        public class TaskListDtoExample : IExamplesProvider<List<TaskDto>>
        {
            public List<TaskDto> GetExamples()
            {
                return new List<TaskDto>
                {
                    new()
                    {
                        Id = 1,
                        StatusId = 0,
                        Title = "Task"
                    },
                    new()
                    {
                        Id = 2,
                        StatusId = 0,
                        Title = "Task2"
                    }
                };
            }
        }

        #endregion
    }
}