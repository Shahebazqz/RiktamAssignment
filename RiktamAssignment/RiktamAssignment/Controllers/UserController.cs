using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RiktamAssignment.Dto;
using RiktamAssignment.Helper;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;

namespace RiktamAssignment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly AppSettings appSettings;

        public UserController(IUserRepository userRepository, IMapper mapper,
                                IOptions<AppSettings> appSettings)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = userRepository.GetUser().FirstOrDefault(x => x.Username == userParam.Username && x.Password == userParam.Password);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody]UserDto userDto)
        {
            return userRepository.CreateUser(userDto);
        }

        [Route("GetUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var users = mapper.Map<List<UserDto>>(userRepository.GetUser());
            return Ok(users);
        }

        [Route("GetUser/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user= mapper.Map<UserDto>(userRepository.GetUser(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Route("GetUserGroups/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetUserGroups(int id)
        {
            var groups = userRepository.GetUserGroups(id);

            if (groups == null)
                return BadRequest("Please enter valid id");

            return Ok(mapper.Map<List<GroupDto>>(groups));
        }
        [Route("[action]/{id}")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (userRepository.Delete(id))
                return Ok("Deleted Successfully");
            else
                return BadRequest("Record not found");
        }
        [Route("[action]/{id}")]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            if (userRepository.Update(id,updatedUser))
                return Ok("updated Successfully");
            else
                return BadRequest("Record not found");
        }
    }
}