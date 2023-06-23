using RiktamAssignment.Dto;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext dataContext;

        public GroupRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public Group AddMemberToGroup(int id, UserDto member)
        {
            var user = dataContext.Users.Find(member.Id);
            if (user == null)
                return null;
            var group = dataContext.Groups.Find(id);
            if (group == null)
                return null;
            var groupUser = new GroupUser()
            {
                Group = group,
                GroupId = group.Id,
                User = user,
                UserId = member.Id
            };

            if (dataContext.GroupUsers.Find(groupUser.GroupId, groupUser.UserId) == null)
            {
                if (group.Members == null)
                    group.Members = new List<GroupUser>() { groupUser };
                else
                    group.Members.Add(groupUser);

                if (user.Groups == null)
                    user.Groups = new List<GroupUser>() { groupUser };
                else
                    user.Groups.Add(groupUser);

                dataContext.GroupUsers.Add(groupUser);

                dataContext.SaveChanges();
            }
            return group;
        }

        public Group CreateGroup(GroupDto groupDto)
        {
            var group = new Group()
            {
                Members = null,
                Messages = null,
                Name = groupDto.Name
            };
            dataContext.Groups.Add(group);
            dataContext.SaveChanges();
            return group;
        }

        public bool DeleteGroup(int id)
        {
            var group = dataContext.Groups.Find(id);
            if (group == null)
                return false;
            dataContext.Groups.Remove(group);
            dataContext.SaveChanges();
            return true;
        }

        public Group GetGroup(int id)
        {
            var group = dataContext.Groups.Find(id);
            if (group == null)
                return null;
            return group;
        }

        public IEnumerable<Group> GetGroup()
        {
            return dataContext.Groups.ToList();
        }

        public bool UpdateGroup(int id, GroupDto updatedGroup)
        {
            var group = dataContext.Groups.Find(id);
            if (group == null)
                return false;
            group.Name = updatedGroup.Name;
            dataContext.SaveChanges();
            return true;
        }
    }
}
