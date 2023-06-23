using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiktamAssignment.Dto;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;

namespace RiktamAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        
        private readonly IGroupRepository groupRepository;
        public GroupController(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }
        [HttpPost]
        public ActionResult<Group> CreateGroup(GroupDto groupDto)
        {
            return groupRepository.CreateGroup(groupDto);
        }
        [Route("GetGroup/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetGroup(int id)
        {
            var group = groupRepository.GetGroup(id);
            if (group == null)
                return NotFound();
            return Ok(group);
        }
        [Route("[action]/{id}")]
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            if (groupRepository.DeleteGroup(id) == true)
                return Ok("Record deleted successfully");
            else
                return BadRequest("Record not found");

        }
        [Route("[action]/{id}")]
        [HttpPut("{id}")]
        public IActionResult UpdateGroup(int id,[FromBody]GroupDto updatedGroup)
        {
            if (groupRepository.UpdateGroup(id, updatedGroup))
                return Ok(updatedGroup);
            else
                return BadRequest("Not Found");
        }
        [Route("[action]/{id}")]
        [HttpPut("{id}")]
        public IActionResult AddMemberToGroup(int id, [FromBody]UserDto member)
        {
            var group = groupRepository.AddMemberToGroup(id, member);
            if (group == null)
                return BadRequest("Please provide valid details");
            else
                return Ok(group);
        }
        [Route("GetGroup")]
        [HttpGet]
        public IActionResult GetGroup()
        {
            return Ok(groupRepository.GetGroup());
        }
    }
}