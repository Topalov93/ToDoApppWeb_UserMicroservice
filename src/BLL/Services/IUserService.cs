using Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models.Users;

namespace ToDoApp.Services.UserService
{
    public interface IUserService
    {
        public Task<ResultState> CreateUser(User newUserInfoHolder);

        public Task<ResultState> EditUser(int userToEditId, User newInfoHolderUser);

        public Task<ResultState> DeleteUser(int userToDeleteId);

        public Task<List<User>> ListUsers();
    }
}
