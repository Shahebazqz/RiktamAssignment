using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using RiktamAssignment.Controllers;
using RiktamAssignment.Dto;
using RiktamAssignment.Helper;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;
using RiktamAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class UserControllerTests
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IOptions<AppSettings> appSettings;

        public UserControllerTests()
        {
            userRepository = A.Fake<IUserRepository>();
            mapper = A.Fake<IMapper>();
            appSettings = Options.Create<AppSettings>(new AppSettings { Secret = "myproject_secret_key_XXXXXXXXXXXXX" }); // A.Fake<IOptions<AppSettings>>();
        }
        [Test]
        public void UserController_GetUsers_ReturnOk()
        {
            //Arrange
            var users = A.Fake<ICollection<UserDto>>();
            var userList = A.Fake<List<UserDto>>();
            A.CallTo(() => mapper.Map<List<UserDto>>(users)).Returns(userList);
            var controller = new UserController(userRepository,mapper,appSettings);
            
            //Act
            var result = controller.GetUser();
            
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Test]
        public void UserController_GetUserGroups_ReturnOk()
        {
            //Arrange
            var users = A.Fake<ICollection<UserDto>>();
            var userList = A.Fake<List<UserDto>>();
            A.CallTo(() => mapper.Map<List<UserDto>>(users)).Returns(userList);
            var controller = new UserController(userRepository, mapper, appSettings);

            //Act
            var result = controller.GetUserGroups(1);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Test]
        public async Task UserController_GetUser_ReturnOkAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var userRepositoryNew = new UserRepository(dbContext);
            var user = new UserDto { Username = "test1", Password = "password", Email = "test@mail.com",  Token = "", Id = 1 };
            var controller = new UserController(userRepositoryNew, mapper, appSettings);
            
            //Act
            var result = controller.GetUser(user.Id);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task UserController_DeleteUser_ReturnOkAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var userRepositoryNew = new UserRepository(dbContext);
            int userId = 1;
            var controller = new UserController(userRepositoryNew, mapper, appSettings);

            //Act
            var result = controller.DeleteUser(userId);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task UserController_DeleteUser_ReturnBadRequestAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var userRepositoryNew = new UserRepository(dbContext);
            int userId = 225;
            var controller = new UserController(userRepositoryNew, mapper, appSettings);

            //Act
            var result = controller.DeleteUser(userId);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task UserController_UpdateUser_ReturnOkAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var userRepositoryNew = new UserRepository(dbContext);
            int userId = 1;
            var user = new User { Username = "test1", Password = "password", Email = "test@mail.com",Groups=null, Token = "", Id = 1 };
            var controller = new UserController(userRepositoryNew, mapper, appSettings);

            //Act
            var result = controller.UpdateUser(userId,user);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task UserController_UpdateUser_ReturnBadRequestAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var userRepositoryNew = new UserRepository(dbContext);
            int userId = 255;
            var user = new User { Username = "test1", Password = "password", Email = "test@mail.com", Groups = null, Token = "", Id = 1 };
            var controller = new UserController(userRepositoryNew, mapper, appSettings);

            //Act
            var result = controller.UpdateUser(userId, user);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public void UserController_CreateUser_ReturnOK()
        {
            //Arrange
            var userMap = A.Fake<UserDto>();
            var user = A.Fake<User>();
            var userCreate = A.Fake<UserDto>();
            var users = A.Fake<ICollection<UserDto>>();
            var userList = A.Fake<IList<UserDto>>();
            A.CallTo(() => userRepository.GetUser(userCreate.Id)).Returns(user);
            A.CallTo(() => mapper.Map<User>(userCreate)).Returns(user);
            A.CallTo(() => userRepository.CreateUser(userMap)).Returns(user);
            var controller = new UserController(userRepository,mapper,appSettings);

            //Act
            var result = controller.CreateUser(userCreate);

            //Assert
            result.Should().NotBeNull();
        }
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Users.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Users.Add(
                    new User()
                    {
                        Email = "test1@gmail.com",
                        Groups = null,
                        Password = "password",
                        Token = "",
                        Username="test1"
                    }) ;
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        [Test]
        public async Task Authenticate_ReturnsOkResult_WhenUserIsAuthenticatedAsync()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var userRepositoryNew = new UserRepository(dbContext);
            var userParam = new User { Username = "test1", Password = "password", Email = "test@mail.com", Groups = null, Token = "", Id = 1 };
            var controller = new UserController(userRepositoryNew, mapper, appSettings);
           
            // Act
            var result = controller.Authenticate(userParam);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}