using AutoMapper;
using TaskForge.Domain.Entities;
using TaskForge.Application.DTOs;



namespace TaskForge.Application.Mappings
{
    //Since status is to be converted from enum to string , we need profile class to define rules
    public class TaskItemProfile : Profile //Inherits from AutoMapper’s Profile base class (required for config)
    {
        //constructor to register mappings, which runs only once when app starts
        //AutoMapper uses this behind the scenes when you inject IMapper.
        public TaskItemProfile() 
        {
            CreateMap<TaskItem, TaskItemDto>() //Tells AutoMapper how to convert from TaskItem to TaskItemDto
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            // Adds a special rule: the Status enum should be converted to a string to convert to dto
            // For the Status property in the destination(DTO)
            // dest => dest.Status: This lambda refers to the destination object (i.e., your TaskItemDto)
            //opt => opt.MapFrom(...): This is how you define where and how to get the value from the source (TaskItem).
            //src => src.Status.ToString(): Get the Status from the source object and call .ToString() to convert it into a string.”

            CreateMap<CreateTaskItemDto, TaskItem>(); //since we dont have status property in CreateTaskItemDto
                                                      //we dont need to explicitly map it

            CreateMap<UpdateTaskItemDto, TaskItem>(); //
        }
    }
}
