using RiktamAssignment.Dto;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Repository
{
    public class GroupMessageRepository : IGroupMessageRepository
    {
        private readonly DataContext dataContext;
        public GroupMessageRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public GroupMessageDto CreateGroupMessage(GroupMessageDto groupMessageDto)
        {
            var sender = dataContext.Users.Find(groupMessageDto.SenderId);
            if (sender == null)
                return null;

            var group = dataContext.Groups.Find(groupMessageDto.GroupId);
            if (group == null)
                return null;

            var groupMessage = new GroupMessage()
            {
                Content = groupMessageDto.Content,
                SenderId = groupMessageDto.SenderId,
                Group = group,
                GroupId = groupMessageDto.GroupId,
                Sender = sender,
                SentAt = DateTime.Now
            };
            dataContext.GroupMessages.Add(groupMessage);
            dataContext.SaveChanges();
            return groupMessageDto;
        }

        public bool DeleteGroupMessage(int id)
        {
            var groupMessage = dataContext.GroupMessages.Find(id);
            if (groupMessage == null)
                return false;
            dataContext.GroupMessages.Remove(groupMessage);
            dataContext.SaveChanges();
            return true;
        }

        public GroupMessageDto GetGroupMessage(int id)
        {
            var groupMessage = dataContext.GroupMessages.Find(id);
            if (groupMessage == null)
                return null;
            var groupMessageDto = new GroupMessageDto()
            {
                GroupId = groupMessage.GroupId,
                Content = groupMessage.Content,
                GroupMessageId = groupMessage.Id,
                SenderId = groupMessage.SenderId,
                SentAt = groupMessage.SentAt
            };
            return groupMessageDto;
        }

        public IEnumerable<GroupMessage> GetGroupMessages()
        {
            return dataContext.GroupMessages.ToList();
        }

        public GroupMessage UpdateGroupMessage(int id, GroupMessageDto updatedGroupMessageDto)
        {
            var sender = dataContext.Users.Find(updatedGroupMessageDto.SenderId);
            if (sender == null)
                return null;

            var group = dataContext.Groups.Find(updatedGroupMessageDto.GroupId);
            if (group == null)
                return null;

            var groupMessage = dataContext.GroupMessages.Find(id);
            if (groupMessage == null)
                return null;

            groupMessage.Content = updatedGroupMessageDto.Content;
            groupMessage.Group = group;
            groupMessage.GroupId = updatedGroupMessageDto.GroupId;
            groupMessage.Sender = sender;
            groupMessage.SenderId = updatedGroupMessageDto.SenderId;
            groupMessage.SentAt = updatedGroupMessageDto.SentAt;
            dataContext.SaveChanges();
            return groupMessage;
        }
    }
}
