using System;
using System.Collections.Generic;
using StudentPlatformAPI.Dto;

namespace StudentPlatformAPI.Services
{
    public interface ITaskService
    {
        public IEnumerable<TaskDto> GetTasks(int statusId, Guid userId);
        public void UpdateTask(TaskDto dto, Guid userId);
        public int CreateTask(TaskDto dto, Guid userId);
        public void DeleteTask(int id, Guid userId);
        public void DeleteTasks(List<TaskDto> tasks, Guid userId);
    }
}