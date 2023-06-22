using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models.Users;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUser(User newInfoHolderUser);

        public Task EditUserBy(int userId, User newInfoHolderUser);

        public Task DeleteUserBy(int userId);

        public Task<List<User>> GetUsers();

        public Task<User> GetUserById(int userId);

        public Task<User> GetUserByName(string userName);

        public Task<User> GetUserByNameAndPassword(string username, string password);

        public User GetLastUpdatedUser();
    }
}
