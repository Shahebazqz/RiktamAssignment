using RiktamAssignment.Dto;
using RiktamAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Interfaces
{
    public interface IUserRepository
    {
        User CreateUser(UserDto userDto);
        ICollection<User> GetUser();
        User GetUser(int id);
        ICollection<Group> GetUserGroups(int id);
        bool Delete(int id);
        bool Update(int id, User updatedUser);
    }
}
