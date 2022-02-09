using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StudentPlatformAPI.data;
using StudentPlatformAPI.dto;
using StudentPlatformAPI.models;

namespace StudentPlatformAPI.services
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

        public IEnumerable<TaskDto> getTasks(int statusId)
        {
            return _context.Tasks.Where(t => t.StatusId == statusId).Select(t => new TaskDto()
            {
                StatusId = t.StatusId,
                StudentId = t.StudentId,
                Id = t.Id,
                Title = t.Title
            }).ToList();
        }

        public void updateTask(TaskDto dto)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == dto.Id);
            task.StatusId = dto.StatusId;
            task.Title = dto.Title;

            _context.SaveChanges();
        }

        public void createTask(TaskDto dto)
        {
            Task task = _mapper.Map<Task>(dto);
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void deleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();

            }
        }

        public void deleteTasks(List<TaskDto> tasks)
        {
            foreach (var task in tasks)
            {
                var taskInDb = _context.Tasks.FirstOrDefault(t => t.Id == task.Id);

                if (taskInDb != null)
                {
                    _context.Tasks.Remove(taskInDb);

                }
            }
            _context.SaveChanges();
        }
    }
}