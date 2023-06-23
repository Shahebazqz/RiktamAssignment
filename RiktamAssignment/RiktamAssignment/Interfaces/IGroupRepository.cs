using RiktamAssignment.Dto;
using RiktamAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Interfaces
{
    public interface IGroupRepository
    {
        Group CreateGroup(GroupDto groupDto);
        Group GetGroup(int id);
        bool DeleteGroup(int id);
        bool UpdateGroup(int id,GroupDto updatedGroup);
        Group AddMemberToGroup(int id,UserDto member);
        IEnumerable<Group> GetGroup();
    }
}
