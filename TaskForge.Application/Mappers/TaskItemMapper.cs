
using TaskForge.Domain.Entities; // to fix this error, we need to give reference of Domain project,
                                 // but domain should not know about application project
                                 //API -> Application -> Domain
using TaskForge.Application.DTOs;

namespace TaskForge.Application.Mappers; 
public static class TaskItemMapper
{
    public static TaskItemDto ToDto(TaskItem task)
    {
        return new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status.ToString() // Enum to string conversion manually
        };
    }
}
