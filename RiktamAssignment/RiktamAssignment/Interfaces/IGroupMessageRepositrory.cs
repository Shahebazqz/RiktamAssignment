using RiktamAssignment.Dto;
using RiktamAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Interfaces
{
    public interface IGroupMessageRepository
    {
        GroupMessageDto CreateGroupMessage(GroupMessageDto groupMessageDto);
        GroupMessageDto GetGroupMessage(int id);
        bool DeleteGroupMessage(int id);
        GroupMessage UpdateGroupMessage(int id,GroupMessageDto updatedGroupMessageDto);
        IEnumerable<GroupMessage> GetGroupMessages();
    }
}
