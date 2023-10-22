using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsTask.Api.Bl.Interfaces;
using NsTask.Api.Domain.Enteties;
using NsTask.Api.Dtos.NsasTask;

namespace NsTask.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NsasTaskController : ControllerBase
    {
        private readonly INsasTaskRepository nsasTaskRepository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment _env;

        public NsasTaskController(INsasTaskRepository nsasTaskRepository, IMapper mapper, IWebHostEnvironment env)
        {
            this.nsasTaskRepository = nsasTaskRepository;
            this.mapper = mapper;
            _env = env;
        }

        [HttpGet(Name = "GetAllTasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NsasTaskDto>))]
        public IActionResult getAllNsasTasks()
        {
            var NsasTaskList = nsasTaskRepository.GetNsasTasks();
            var NsasTaskDtoList = new List<NsasTaskDto>();
            foreach (var item in NsasTaskList)
            {
                NsasTaskDtoList.Add(mapper.Map<NsasTaskDto>(item));
            }

            return Ok(NsasTaskDtoList);
        }

        [HttpGet(Name = "FilterTasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NsasTaskDto>))]
        public IActionResult filterNsasTasks([FromQuery] NsasTaskFilters filters)
        {
            var result = nsasTaskRepository.FilterTasks(filters);
    
            return Ok(mapper.Map<IEnumerable<NsasTaskDto>>(result));
        }


        [HttpGet("{id}", Name = "GetTaskDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NsasTaskDto))]
        public IActionResult getNsasTask(int id)
        {
            var NsasTask = nsasTaskRepository.GetNsasTask(id);
            var NsasTaskDto = mapper.Map<NsasTaskDto>(NsasTask);
            return Ok(NsasTaskDto);
        }

        [HttpPost(Name = "CreateTask")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NsasTaskDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateNsasTask([FromBody] NsasTaskDto objDTO)
        {
            if (objDTO == null)
            {
                return BadRequest();
            }

            if (nsasTaskRepository.CheckNsasTaskExists(objDTO.Title))
            {
                ModelState.AddModelError(string.Empty, $"the NsasTask is exist");
                return StatusCode(404, ModelState);
            }

            var obj = mapper.Map<NsasTask>(objDTO);
            if (!nsasTaskRepository.CreateNsasTask(obj))
            {
                ModelState.AddModelError(string.Empty, $"something went wrong when adding {obj.Title}");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, obj);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut(Name = "UpdateTask")]
        public IActionResult updateNsasTask([FromBody] NsasTaskDto objDTO)
        {
            if (objDTO == null)
            {
                return BadRequest();
            }
            if(objDTO.Title != null)
            {
              var objName = nsasTaskRepository.GetNsasTask(objDTO.Title);
                if (objName != null)
                {
                    ModelState.AddModelError(string.Empty, "NsasTask is Exist");
                    return StatusCode(404, ModelState);
                }
            }

            var obj = mapper.Map<NsasTask>(objDTO);
            var objFromDB = nsasTaskRepository.GetNsasTask(obj.Id);
            objFromDB.Title = obj.Title;
            objFromDB.Description = obj.Description;

            if (!nsasTaskRepository.UpdateNsasTask(objFromDB))
            {
                ModelState.AddModelError(string.Empty, $"something went wrong when Updating {objFromDB.Title}");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, true);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "DeleteTask")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult deleteNsasTask(int id)
        {
            if (!nsasTaskRepository.CheckNsasTaskExists(id))
            {
                return NotFound();
            }

            var obj = nsasTaskRepository.GetNsasTask(id);
            if (!nsasTaskRepository.DeleteNsasTask(obj))
            {
                ModelState.AddModelError(string.Empty, $"something went wrong when deleting {obj.Title}");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, true);
        }

    }
}
