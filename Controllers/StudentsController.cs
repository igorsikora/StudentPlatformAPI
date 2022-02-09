using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPlatformAPI.data;
using StudentPlatformAPI.dto;
using StudentPlatformAPI.services;

namespace StudentPlatformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentPlatformContext _context;
        private IStudentService _service;

        public StudentsController(StudentPlatformContext context, IStudentService service)
        {
            _context = context;
            _service = service;
        }

        [Route("student/{id}")]
        [HttpGet]
        public IActionResult GetStudent(int id)
        {
            return Ok(_service.getStudent(id));
        }

        // PUT api/<StudentsController>
        [HttpPut]
        public IActionResult Put([FromBody]StudentDto dto)
        {
            try
            {
                _service.updateStudent(dto);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
