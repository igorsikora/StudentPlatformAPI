using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StudentPlatformAPI.Data;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Models;

namespace StudentPlatformAPI.Services
{
    public class TaskService : ITaskService
    {
        private StudentPlatformContext _context;
        private readonly IMapper _mapper;
        public TaskService(StudentPlatformContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<TaskDto> getTasks(int statusId, Guid userId)
        {
            return _context.Tasks.Where(t => t.StatusId == statusId && t.UserId == userId).Select(t => new TaskDto()
            {
                StatusId = t.StatusId,
                Id = t.Id,
                Title = t.Title
            }).ToList();
        }

        public void updateTask(TaskDto dto, Guid userId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == dto.Id && t.UserId == userId);
            task.StatusId = dto.StatusId;
            task.Title = dto.Title;

            _context.SaveChanges();
        }

        public int createTask(TaskDto dto, Guid userId)
        {
            Task task = _mapper.Map<Task>(dto);
            task.SetUserId(userId);
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task.Id;
        }

        public void deleteTask(int id, Guid userId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();

            }
        }

        public void deleteTasks(List<TaskDto> tasks, Guid userId)
        {
            foreach (var task in tasks)
            {
                var taskInDb = _context.Tasks.FirstOrDefault(t => t.Id == task.Id && t.UserId == userId);

                if (taskInDb != null)
                {
                    _context.Tasks.Remove(taskInDb);

                }
            }
            _context.SaveChanges();
        }
    }
}