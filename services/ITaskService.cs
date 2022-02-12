using System;
using System.Collections.Generic;
using StudentPlatformAPI.Dto;

namespace StudentPlatformAPI.Services
{
    public interface ITaskService
    {
        public IEnumerable<TaskDto> getTasks(int statusId, Guid userId);
        public void updateTask(TaskDto dto, Guid userId);
        public int createTask(TaskDto dto, Guid userId);
        public void deleteTask(int id, Guid userId);
        public void deleteTasks(List<TaskDto> tasks, Guid userId);
    }
}