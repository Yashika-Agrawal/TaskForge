using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskForge.Application.DTOs;
using TaskForge.Domain.Entities;
using TaskForge.Infrastructure.Persistence;

namespace TaskForge.API.Controllers
{
    [ApiController] //Attribute - Attach to methods, classes, etc , used to tell this is Api controller
    [Route("api/[controller]")] // URL will be: /api/taskitem
    //route matching in ASP.NET Core is case-insensitive.
    [Authorize]
    public class TaskItemController : ControllerBase //Added ControllerBase to let .NET know that it handles http req
    {
        private readonly IMapper _mapper;
        private readonly TaskForgeDbContext _dbContext; //need to inject db context to do db related changes

        //use to receive dependencies (like services) and store them in fields so other methods can use them.
        public TaskItemController(IMapper mapper, TaskForgeDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTaskAsync(CreateTaskItemDto createTaskItemDto)
        {
            //convert dto to entity
            var taskEntity = _mapper.Map<TaskItem>(createTaskItemDto); //<int>(str) conversion from string to int

            //Update data in entity acc to what client send
            taskEntity.Id = Guid.NewGuid();
            taskEntity.CreatedAt = DateTime.UtcNow;

            /*taskEntity.Title= createTaskItemDto.Title;
            taskEntity.Description= createTaskItemDto.Description;
            taskEntity.DueDate = createTaskItemDto.DueDate;*/  //All these properties are already handled by AutoMapper

            // Save to DB
            _dbContext.TaskItems.Add(taskEntity); //in TaskItems table add taskEntity which user sends ( new task item)
            await _dbContext.SaveChangesAsync(); //save changes in db

            // map back Entity -> DTO
            var taskDto = _mapper.Map<TaskItemDto>(taskEntity);

            return Ok(taskDto); // just returns 200 with the created task

        }

        [HttpGet]
        public async Task<ActionResult<List<TaskItemDto>>> GetAllTasksAsync()
        {
           var tasks = await _dbContext.TaskItems.ToListAsync(); //Getting all the task items from table and converting to list
           var tasksDto = _mapper.Map<List<TaskItemDto>>(tasks);
           return Ok(tasksDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskById([FromRoute] Guid id)
        {
            
            var task = await _dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if(task ==null)
            {
               return NotFound();
            }
            else
            {
                var taskDto = _mapper.Map<TaskItemDto>(task);
                return Ok(taskDto);
            }
        }
        //since we have update endpoint here, we will have id also in body which can be changed by user, better to have separate dto
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemDto>> UpdateTaskById ([FromRoute] Guid id, [FromBody] UpdateTaskItemDto taskDto)
        {
            var task = await _dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if (task==null)
            {
                return NotFound();
            }
            //mapping
            _mapper.Map(taskDto, task); //converts from taskdto to task entity
                                        //since we are updating task not creating new hence this way it is used

            //save changes
            await _dbContext.SaveChangesAsync();
             return  Ok(_mapper.Map<TaskItemDto>(task));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskItemDto>> DeleteTaskById([FromRoute] Guid id)
        {
            //Fetch the task by its id
            var task = await _dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if(task==null)
            {
                return NotFound();
            }
             _dbContext.TaskItems.Remove(task);
            await _dbContext.SaveChangesAsync();
            return Ok(_mapper?.Map<TaskItemDto>(task));
        }

    }

}
