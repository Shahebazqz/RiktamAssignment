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
    class LikedMessageControllerTests
    {
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
                        Username = "test1"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            if (await databaseContext.LikedMessages.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.LikedMessages.Add(
                    new LikedMessage()
                    {
                        LikedAt = DateTime.Now,
                        MessageId=1,
                        Username="test1"
                    }) ;
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        [Test]
        public async Task LikedMessageController_LikeMessage_ReturnOKAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var likedMessageRepositoryNew = new LikedMessageRepository(dbContext);
            var likeMessage = new LikedMessage { MessageId = 1, Username = "test1", LikedAt = DateTime.Now };
            var controller = new LikedMessageController(likedMessageRepositoryNew);

            //Act
            var result = controller.LikeMessage(likeMessage);

            //Assert
            result.Should().NotBeNull();
        }
        [Test]
        public async Task LikedMessageController_GetUserLikes_ReturnOKAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var likedMessageRepositoryNew = new LikedMessageRepository(dbContext);
            var likeMessage = new LikedMessage { MessageId = 1, Username = "test1", LikedAt = DateTime.Now };
            var controller = new LikedMessageController(likedMessageRepositoryNew);

            //Act
            controller.LikeMessage(likeMessage);
            var result = controller.GetUserLikes(likeMessage.Username);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);
        }
        [Test]
        public async Task LikedMessageController_GetMessageLikes_ReturnOKAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var likedMessageRepositoryNew = new LikedMessageRepository(dbContext);
            var likeMessage = new LikedMessage { MessageId = 1, Username = "test1", LikedAt = DateTime.Now };
            var controller = new LikedMessageController(likedMessageRepositoryNew);

            //Act
            controller.LikeMessage(likeMessage);
            var result = controller.GetMessageLikes(1);

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task LikedMessageController_DeleteMessageLike_ReturnOKAsync()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var likedMessageRepositoryNew = new LikedMessageRepository(dbContext);
            var likeMessage = new LikedMessage { MessageId = 1, Username = "test1", LikedAt = DateTime.Now };
            var controller = new LikedMessageController(likedMessageRepositoryNew);

            //Act
            controller.LikeMessage(likeMessage);
            var result = controller.DeleteMessageLike(1, "test1");

            //Assert
            result.Should().NotBeNull();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

    }
}
