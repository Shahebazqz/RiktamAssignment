using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RiktamAssignment.Controllers;
using RiktamAssignment.Dto;
using RiktamAssignment.Models;
using RiktamAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RiktamAssignmentTests
{
    class GroupMessageControllerTests
    {
            private async Task<DataContext> GetDatabaseContext()
            {
                var options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
                var databaseContext = new DataContext(options);
                databaseContext.Database.EnsureCreated();
                if (await databaseContext.GroupMessages.CountAsync() <= 0)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        databaseContext.GroupMessages.Add(
                        new GroupMessage()
                        {
                            Content = "Test Content",
                            GroupId = 1,
                            Sender= null,
                            SenderId=1,
                            SentAt = DateTime.Now
                        });
                        await databaseContext.SaveChangesAsync();
                    }
                }
                return databaseContext;
            }
            [Test]
            public async Task GroupMessageController_CreateGroupMessage_ReturnOKAsync()
            {
                //Arrange
                var dbContext = await GetDatabaseContext();
                var groupMessageRepositoryNew = new GroupMessageRepository(dbContext);
                var groupMessage = new GroupMessageDto { GroupMessageId = 1, Content = "Test Content", GroupId = 1, SenderId = 1, SentAt = DateTime.Now };
                var controller = new GroupMessageController(groupMessageRepositoryNew);

                //Act
                var result = controller.CreateGroupMessage(groupMessage);

                //Assert
                result.Should().NotBeNull();
            }
        [Test]
        public async Task GroupController_GetGroupMessage_ReturnOkAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var groupMessageRepositoryNew = new GroupMessageRepository(dbContext);
            var groupMessage = new GroupMessage { Content = "test1", Group = null, Sender = null, Id = 1,SentAt=DateTime.Now,SenderId=1,GroupId=1 };
            var controller = new GroupMessageController(groupMessageRepositoryNew);

            //Act
            var result = controller.GetGroupMessage(groupMessage.Id);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task GroupController_GetGroupMessages_ReturnOkAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var groupMessageRepositoryNew = new GroupMessageRepository(dbContext);
            var groupMessage = new GroupMessage { Content = "test1", Group = null, Sender = null, Id = 1, SentAt = DateTime.Now, SenderId = 1, GroupId = 1 };
            var controller = new GroupMessageController(groupMessageRepositoryNew);

            //Act
            var result = controller.GetGroupMessages();

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task GroupController_UpdateGroupMessage_ReturnBadRequestAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var groupMessageRepositoryNew = new GroupMessageRepository(dbContext);
            int groupId = 1;
            var groupMessage = new GroupMessageDto { GroupMessageId = 1,Content="Test Content",GroupId=1,SenderId=1,SentAt=DateTime.Now };
            var controller = new GroupMessageController(groupMessageRepositoryNew);

            //Act
            var result = controller.UpdateGroupMessage(groupId, groupMessage);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
