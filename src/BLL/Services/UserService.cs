using Common;
using Common.Constants;
using Common.Enums;
using DAL.Data;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models.Users;
using UserMicroservice_Message_Send.SendMessage;

namespace ToDoApp.Services.UserService
{
    public class UserService : IUserService
    {
        private IUserRepository _usersRepository;
        private IUserUpdateSender _userUpdateSender;

        public UserService(IUserRepository userRepository, IUserUpdateSender updateSender)
        {
            _userUpdateSender = updateSender;
            _usersRepository = userRepository;
        }

        public async Task<ResultState> CreateUser(User newUserInfoHolder)
        {
            if (await _usersRepository.GetUserByName(newUserInfoHolder.Username) != null)
            {
                return new ResultState(false, Messages.UserAlreadyExist);
            }

            try
            {
                await _usersRepository.CreateUser(newUserInfoHolder);
                return new ResultState(true, Messages.UserCreationSuccessfull);
            }
            catch (Exception)
            {
                return new ResultState(false, Messages.UnableToCreateUser);
            }
        }

        public async Task<ResultState> EditUser(int userToEditId, User newInfoHolderUser)
        {
            User userToEdit = await _usersRepository.GetUserById(userToEditId);

            if (userToEdit is null)
            {
                return new ResultState(false, Messages.UserDoesntExist);
            }

            newInfoHolderUser.EditedOn = DateTime.UtcNow;

            ResultState resultstate = new ResultState(true, "");

            try
            {
                await _usersRepository.EditUserBy(userToEditId, newInfoHolderUser);
                resultstate.Message = Messages.UserEditSuccessfull;
            }
            catch (Exception ex)
            {
                resultstate.IsSuccessful = false;
                resultstate.Message = Messages.UnableToEditUser;
                resultstate.ThrownException = ex;
                return resultstate;
            }

            try
            {
                User updatedUser = await _usersRepository.GetUserById(userToEditId);
                _userUpdateSender.Send(updatedUser);
                return resultstate;
            }
            catch (Exception ex)
            {
                resultstate.Message = (Messages.UserEditSuccessfull + "Unsuccessful send to message queue");
                resultstate.ThrownException = ex;
            }
            return resultstate;
        }

        public async Task<ResultState> DeleteUser(int userToDeleteId)
        {
            User userToDelete = await _usersRepository.GetUserById(userToDeleteId);

            if (userToDelete is null)
            {
                return new ResultState(false, Messages.UserDoesntExist);
            }

            if (userToDelete.Id == 1)
            {
                return new ResultState(false, Messages.UserInitialAdminDeletingUnsuccessful);
            }

            try
            {
                await _usersRepository.DeleteUserBy(userToDeleteId);
                return new ResultState(true, Messages.UserDeletedSuccessfull);
            }
            catch (Exception)
            {
                return new ResultState(false, Messages.UnableToDeleteUser);
            }
        }

        public async Task<List<User>> ListUsers()
        {
            return await _usersRepository.GetUsers();
        }

        public async Task<User> GetUserByNameAndPassword(string username, string password)
        {
            return await _usersRepository.GetUserByNameAndPassword(username, password);
        }
    }
}
