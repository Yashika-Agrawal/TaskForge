using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskForge.Application.DTOs;
using TaskForge.Application.Interfaces.Repositories;
using TaskForge.Domain.Entities;

namespace TaskForge.Application.Services
{
    public class TaskItemService 

    {
        // IMapper is provided by the Automapper 
        // _camelCaseWithUnderscore is a naming convention for private fields.
        private readonly IMapper _mapper; //readonly means value can only be set once — usually in the constructor.
        //var mapper = new IMapper(); // ❌ Error: Cannot create an instance of the interface
        private readonly ITaskItemRepository _taskItemRepository;
        // take ITaskItemRepository as a dependency via constructor.
        public TaskItemService(IMapper mapper, ITaskItemRepository repo)
        {
            _mapper = mapper;
            _taskItemRepository = repo;
        }
        public async Task<TaskItemDto> CreateAsync (TaskItemDto dto)
        {
            var taskEntity = _mapper.Map<TaskItem>(dto); //Converting dto to TaskItem
            taskEntity.Id = Guid.NewGuid();
            taskEntity.CreatedAt = DateTime.Now;

            // 👇 Simulating a logged-in user ID (until we build real auth)
            //taskEntity.CreatedByUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            //saving to db
            var savedEntity = await _taskItemRepository.CreateAsync(taskEntity);

            var taskItemDto = _mapper.Map<TaskItemDto>(savedEntity);
            return taskItemDto;

        }
    }
}
