
using TaskForge.Domain.Entities; // to fix this error, we need to give reference of Domain project,
                                 // but domain should not know about application project
                                 //API -> Application -> Domain
using TaskForge.Application.DTOs;
using TaskForge.Domain.Enums;

namespace TaskForge.Application.Mappers; 
public static class TaskItemMapper
{
    public static TaskItemDto ToDto(TaskItem task) //sending dto from backend to frontend
    {
        return new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status.ToString() // Enum to string conversion manually
                                            // because strings work better in api/frontend
                                            // enums are c# specific, and it can have numeric type only Todo: 0 
                                            // frontend have to decode from 0 to Todo again to show in UI

        };
    }
    public static TaskItem FromDto(TaskItemDto dto) //received response as dto (fromDto)
    {
        return new TaskItem
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            // Convert string back to enum
            Status = Enum.TryParse<Status>(dto.Status, out var status) ? status : Status.Todo
        };
    }

}
