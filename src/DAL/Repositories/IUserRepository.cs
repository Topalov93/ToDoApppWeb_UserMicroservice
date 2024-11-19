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
        public Task Create(User newInfoHolderUser);

        public Task<List<User>> GetAll();

        public Task<User> GetById(int userId);

        public Task<User> GetByName(string userName);

        public Task<User> GetByNameAndPassword(string username, string password);

        public Task Edit(int userId, User newInfoHolderUser);

        public Task Delete(int userId);

    }
}
