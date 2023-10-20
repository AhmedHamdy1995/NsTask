using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsTask.Api.Bl.Interfaces;
using NsTask.Api.Domain.Enteties;
using NsTask.Api.Dtos.NsasTask;

namespace NsTask.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NsasTaskDto>))]
        [ProducesDefaultResponseType]
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

        [HttpGet("{taskId:int}", Name = "getNsasTask")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NsasTaskDto))]
        [ProducesDefaultResponseType]
        public IActionResult getNsasTask(int taskId)
        {
            var NsasTask = nsasTaskRepository.GetNsasTask(taskId);
            var NsasTaskDto = mapper.Map<NsasTaskDto>(NsasTask);
            return Ok(NsasTaskDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NsasTaskDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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

            // to create and return the created object 
            return CreatedAtRoute("getNsasTask", new { NsasTaskId = obj.Id }, obj);

        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{NsasTaskId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult updateNsasTask(int NsasTaskId, [FromBody] NsasTaskDto objDTO)
        {
            if (objDTO == null || NsasTaskId != objDTO.Id)
            {
                return BadRequest();
            }

            // to check if the new title exist before
            var objName = nsasTaskRepository.GetNsasTask(objDTO.Title);

            if (objName != null)
            {
                if (objName.Id != NsasTaskId)
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

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{taskId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult deleteNsasTask(int taskId)
        {
            if (!nsasTaskRepository.CheckNsasTaskExists(taskId))
            {
                return NotFound();
            }

            var obj = nsasTaskRepository.GetNsasTask(taskId);
            if (!nsasTaskRepository.DeleteNsasTask(obj))
            {
                ModelState.AddModelError(string.Empty, $"something went wrong when deleting {obj.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];

                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }



        }
    }
}
