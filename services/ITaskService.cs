using System.Collections.Generic;
using StudentPlatformAPI.dto;

namespace StudentPlatformAPI.services
{
    public interface ITaskService
    {
        public IEnumerable<TaskDto> getTasks(int statusId);
        public void updateTask(TaskDto dto);
        public void createTask(TaskDto dto);
        public void deleteTask(int id);
        public void deleteTasks(List<TaskDto> tasks);
    }
}