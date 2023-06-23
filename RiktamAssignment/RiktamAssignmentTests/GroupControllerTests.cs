using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RiktamAssignment.Controllers;
using RiktamAssignment.Dto;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;
using RiktamAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RiktamAssignmentTests
{
    class GroupControllerTests
    {
        private readonly IGroupRepository groupRepository;
        public GroupControllerTests()
        {
            groupRepository = A.Fake<IGroupRepository>();
            
        }
        [Test]
        public void GroupController_GetGroup_ReturnOk()
        {
            //Arrange
            var groups = A.Fake<ICollection<Group>>();
            var groupList = A.Fake<List<Group>>();
            
            var controller = new GroupController(groupRepository);

            //Act
            var result = controller.GetGroup();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Test]
        public async Task GroupController_GetGroup_ReturnOkAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var groupRepositoryNew = new GroupRepository(dbContext);
            var group = new Group { Name = "test1", Members = null, Messages = null, Id = 1 };
            var controller = new GroupController(groupRepositoryNew);

            //Act
            var result = controller.GetGroup(group.Id);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GroupController_DeleteGroup_ReturnOkAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var groupRepositoryNew = new GroupRepository(dbContext);
            int groupId = 1;
            var controller = new GroupController(groupRepositoryNew);

            //Act
            var result = controller.DeleteGroup(groupId);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task GroupController_DeleteGroup_ReturnBadRequestAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var groupRepositoryNew = new GroupRepository(dbContext);
            int groupId = 225;
            var controller = new GroupController(groupRepositoryNew);

            //Act
            var result = controller.DeleteGroup(groupId);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task GroupController_UpdateGroup_ReturnOkAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var groupRepositoryNew = new GroupRepository(dbContext);
            int groupId = 1;
            var group = new GroupDto { Name = "test1", Id = 1 };
            var controller = new GroupController(groupRepositoryNew);

            //Act
            var result = controller.UpdateGroup(groupId, group);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GroupController_UpdateGroup_ReturnBadRequestAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var groupRepositoryNew = new GroupRepository(dbContext);
            int groupId = 255;
            var group = new GroupDto { Name = "test1", Id = 1 };
            var controller = new GroupController(groupRepositoryNew);

            //Act
            var result = controller.UpdateGroup(groupId, group);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public void GroupController_CreateGroup_ReturnOK()
        {
            //Arrange
            var groupMap = A.Fake<GroupDto>();
            var group = A.Fake<Group>();
            var groupCreate = A.Fake<Group>();
            var groups = A.Fake<ICollection<GroupDto>>();
            var groupList = A.Fake<IList<GroupDto>>();
            A.CallTo(() => groupRepository.GetGroup(groupCreate.Id)).Returns(group);
            A.CallTo(() => groupRepository.CreateGroup(groupMap)).Returns(group);
            var controller = new GroupController(groupRepository);

            //Act
            var result = controller.CreateGroup(groupMap);

            //Assert
            result.Should().NotBeNull();
        }
        [Test]
        public void GroupController_AddMemberToGroup_ReturnOK()
        {
            //Arrange
            var member = A.Fake<UserDto>();
            var group = A.Fake<Group>();
            var groupCreate = A.Fake<Group>();
            var groups = A.Fake<ICollection<GroupDto>>();
            var groupList = A.Fake<IList<GroupDto>>();
            A.CallTo(() => groupRepository.GetGroup(groupCreate.Id)).Returns(group);
      
            var controller = new GroupController(groupRepository);

            //Act
            var result = controller.AddMemberToGroup(1,member);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Groups.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Groups.Add(
                    new Group()
                    {
                        Members=null,
                        Messages=null,
                        Name="testGroup"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}
