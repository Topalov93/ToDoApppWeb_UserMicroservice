using DAL.Models;
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

        // Create
        public async Task Create(User newInfoHolderUser)
        {
            await _context.Users.Add(newInfoHolderUser).ReloadAsync();
            _context.SaveChanges();
        }

        // Read
        public Task<List<User>> GetAll()
        {
            return _context.Users.ToListAsync();
        }

        public Task<User> GetById(int userId)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<User> GetByName(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
        }

        public Task<User> GetByNameAndPassword(string username, string password)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        // Update
        public async Task Edit(int userId, User newInfoHolderUser)
        {
            User userToEdit = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            userToEdit.Username = newInfoHolderUser.Username;
            userToEdit.Password = newInfoHolderUser.Password;
            userToEdit.FirstName = newInfoHolderUser.FirstName;
            userToEdit.LastName = newInfoHolderUser.LastName;
            userToEdit.EditedOn = newInfoHolderUser.EditedOn;

            _context.SaveChanges();
        }

        // Delete
        public async Task Delete(int userId)
        {
            var userToDelete = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            _context.Users.Remove(userToDelete);

            _context.SaveChanges();
        }
    }
}

