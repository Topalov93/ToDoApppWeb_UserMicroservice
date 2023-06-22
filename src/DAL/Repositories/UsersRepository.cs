using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models.Users;

namespace DAL.Data
{
    public class UsersRepository : IUserRepository
    {
        private ToDoAppDbContext _context;

        public UsersRepository(ToDoAppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User newInfoHolderUser)
        {
            await _context.Users.Add(newInfoHolderUser).ReloadAsync();
            _context.SaveChanges();
        }

        public async Task EditUserBy(int userId, User newInfoHolderUser)
        {
            User userToEdit = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            userToEdit.Username = newInfoHolderUser.Username;
            userToEdit.Password = newInfoHolderUser.Password;
            userToEdit.FirstName = newInfoHolderUser.FirstName;
            userToEdit.LastName = newInfoHolderUser.LastName;
            userToEdit.EditedOn = newInfoHolderUser.EditedOn;
            userToEdit.EditedBy = newInfoHolderUser.EditedBy;

            _context.SaveChanges();
        }

        public async Task DeleteUserBy(int userId)
        {
            var userToDelete = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            _context.Users.Remove(userToDelete);

            _context.SaveChanges();
        }

        public Task<List<User>> GetUsers()
        {
            return _context.Users.ToListAsync();
        }

        public Task<User> GetUserById(int userId)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<User> GetUserByName(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
        }

        public Task<User> GetUserByNameAndPassword(string username, string password)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
        public User GetLastUpdatedUser()
        {
            var users = GetUsers().Result.OrderBy(x => x.EditedBy).ToList();

            return users.FirstOrDefault();
        }
    }
}

