using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiktamAssignment.Dto;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;

namespace RiktamAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMessageController : ControllerBase
    {
        private readonly IGroupMessageRepository groupMessageRepository;
        public GroupMessageController(IGroupMessageRepository groupMessageRepository)
        {
                    this.groupMessageRepository = groupMessageRepository;
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult CreateGroupMessage([FromBody] GroupMessageDto groupMessageDto)
        {
            var groupMessage= groupMessageRepository.CreateGroupMessage(groupMessageDto);

            if (groupMessage == null)
                return BadRequest("Please enter valid group/sender Id");
            else
                return Ok(groupMessage);
        }
        [Route("[action]/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetGroupMessage(int id)
        {
            var groupMessage= groupMessageRepository.GetGroupMessage(id);

            if (groupMessage == null)
                return NotFound();
            else
                return Ok(groupMessage);
        }
        [Route("[action]/{id}")]
        [HttpDelete("{id}")]
        public IActionResult DeleteGroupMessage(int id)
        {
            if (groupMessageRepository.DeleteGroupMessage(id))
                return Ok("Deleted successfully");
            else
                return NotFound();
        }
        [Route("[action]/{id}")]
        [HttpPut("{id}")]
        public IActionResult UpdateGroupMessage(int id, 
                        [FromBody] GroupMessageDto updatedGroupMessageDto)
        {
            var groupMessage= groupMessageRepository.UpdateGroupMessage(id, updatedGroupMessageDto);
            if (groupMessage == null)
                return BadRequest("Enter valid sender,group or message id");
            else
                return Ok(groupMessage);
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetGroupMessages()
        {
            return Ok(groupMessageRepository.GetGroupMessages());
            
        }
    }
}