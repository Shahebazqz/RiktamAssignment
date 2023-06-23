using RiktamAssignment.Dto;
using RiktamAssignment.Interfaces;
using RiktamAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public User CreateUser(UserDto userDto)
        {
            var user = new User()
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                Token = "",
                Groups = null,
            };
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = dataContext.Users.Find(id);
            if (user == null)
                return false;
            dataContext.Users.Remove(user);
            dataContext.SaveChanges();
            return true;
        }

        public ICollection<User> GetUser()
        {
            var users = dataContext.Users.ToList();
            return users;
        }

        public User GetUser(int id)
        {
            var user = dataContext.Users.Find(id);
           
            return user;
        }

        public ICollection<Group> GetUserGroups(int id)
        {
            var user = dataContext.Users.Find(id);
            if (user == null)
            {
                return null;
            }
            List<Group> groups = new List<Group>();
            foreach (var groupUser in dataContext.GroupUsers.Where(g => g.UserId == id).ToList())
            {
                var group = dataContext.Groups.Find(groupUser.GroupId);
                groups.Add(group);
            }
            return groups;
        }

        public bool Update(int id, User updatedUser)
        {
            var user = dataContext.Users.Find(id);
            if (user == null)
            {
                return false;
            }
            user.Email = updatedUser.Email;
            user.Groups = updatedUser.Groups;
            user.Password = updatedUser.Password;
            user.Username = updatedUser.Username;
            dataContext.SaveChanges();
            return true;
        }
    }
}
