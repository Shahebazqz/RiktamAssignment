using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;

namespace RiktamAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikedMessageController : ControllerBase
    {
        DataContext dataContext;
        private readonly ILikedMessageRepository likedMessageRepository;
        public LikedMessageController(ILikedMessageRepository likedMessageRepository)
        {
            this.likedMessageRepository = likedMessageRepository;
        }
        [Route("[action]/{id}")]
        [HttpPost("{id}")]
        public IActionResult LikeMessage([FromBody] LikedMessage likedMessage)
        {
            return Ok(likedMessageRepository.LikeMessage(likedMessage));
        }
        [Route("GetMessageLikes/{messageId}")]
        [HttpGet("{messageId}")]
        public IActionResult GetMessageLikes(int messageId)
        {
            return Ok(likedMessageRepository.GetMessageLikes(messageId));
        }
        [Route("[action]/{username}")]
        [HttpGet("{username}")]
        public IEnumerable<LikedMessage> GetUserLikes(string username)
        {
            return likedMessageRepository.GetUserLikes(username);
        }
        [Route("[action]/{messageId}/{username}")]
        [HttpDelete("{messageId}/{username}")]
        public IActionResult DeleteMessageLike(int messageId, string username)
        {
            if (likedMessageRepository.DeleteMessageLike(messageId, username))
                return Ok("Deleted successfully");
            else
                return NotFound();

        }
    }
}