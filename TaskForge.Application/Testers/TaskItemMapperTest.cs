using System;
using TaskForge.Application.DTOs;
using TaskForge.Application.Mappers;
using TaskForge.Domain.Entities;
using TaskForge.Domain.Enums;

namespace TaskForge.Application.Testers
{
    public class TaskItemMapperTester
    {
        public static void Run()
        {
            // Create a sample TaskItem
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Complete the assignment",
                Description = "Write mapping test case for DTO",
                DueDate = DateTime.UtcNow.AddDays(2),
                Status = Status.Progress
            };

            // Use the mapper to convert to DTO
            TaskItemDto dto = TaskItemMapper.ToDto(task);

            // Print results
            Console.WriteLine("Mapped DTO:");
            Console.WriteLine($"Id: {dto.Id}");
            Console.WriteLine($"Title: {dto.Title}");
            Console.WriteLine($"Description: {dto.Description}");
            Console.WriteLine($"DueDate: {dto.DueDate}");
            Console.WriteLine($"Status: {dto.Status}");
        }
    }
}
