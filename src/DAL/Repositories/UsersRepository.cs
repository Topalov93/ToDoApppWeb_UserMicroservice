using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models.Users;

namespace DAL.Data
{
    public class UsersRepository : ToDoAppDbContext
    {
        public async Task CreateUser(User newInfoHolderUser)
        {
            await Users.Add(newInfoHolderUser).ReloadAsync();
            SaveChanges();
        }

        public async Task EditUserBy(int userId, User newInfoHolderUser)
        {
            User userToEdit = await Users.FirstOrDefaultAsync(u => u.Id == userId);

            userToEdit.Username = newInfoHolderUser.Username;
            userToEdit.Password = newInfoHolderUser.Password;
            userToEdit.FirstName = newInfoHolderUser.FirstName;
            userToEdit.LastName = newInfoHolderUser.LastName;
            userToEdit.EditedOn = newInfoHolderUser.EditedOn;
            userToEdit.EditedBy = newInfoHolderUser.EditedBy;

            SaveChanges();
        }

        public async Task DeleteUserBy(int userId)
        {
            var userToDelete = await Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            Users.Remove(userToDelete);

            SaveChanges();
        }

        public Task<List<User>> GetUsers()
        {
            return Users.ToListAsync();
        }

        public Task<User> GetUserById(int userId)
        {
            return Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<User> GetUserByName(string userName)
        {
            return Users.FirstOrDefaultAsync(u => u.Username == userName);
        }

        public Task<User> GetUserByNameAndPassword(string username, string password)
        {
            return Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}

