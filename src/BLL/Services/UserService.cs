﻿using Common;
using Common.Constants;
using Common.Enums;
using Confluent.Kafka;
using DAL.Data;
using DAL.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models.Users;

namespace ToDoApp.Services.UserService
{
    public class UserService : IUserService
    {
        private IUserRepository _usersRepository;

        public UserService(IUserRepository userRepository)
        {
            _usersRepository = userRepository;
        }

        public async Task<ResultState> Create(User newUserInfoHolder)
        {
            if (await _usersRepository.GetByName(newUserInfoHolder.Username) != null)
            {
                return new ResultState(false, Messages.UserAlreadyExist);
            }

            try
            {
                await _usersRepository.Create(newUserInfoHolder);
                return new ResultState(true, Messages.UserCreationSuccessfull);
            }
            catch (Exception)
            {
                return new ResultState(false, Messages.UnableToCreateUser);
            }
        }

        public async Task<List<User>> GetAll()
        {
            return await _usersRepository.GetAll();
        }

        public async Task<User> GetUserByNameAndPassword(string username, string password)
        {
            return await _usersRepository.GetByNameAndPassword(username, password);
        }

        public async Task<ResultState> EditUser(int userToEditId, User newInfoHolderUser)
        {
            User userToEdit = await _usersRepository.GetById(userToEditId);

            if (userToEdit is null)
            {
                return new ResultState(false, Messages.UserDoesntExist);
            }

            newInfoHolderUser.EditedOn = DateTime.UtcNow;

            ResultState resultstate = new ResultState(true, "");

            try
            {
                await _usersRepository.Edit(userToEditId, newInfoHolderUser);
                resultstate.Message = Messages.UserEditSuccessfull;
            }
            catch (Exception ex)
            {
                resultstate.IsSuccessful = false;
                resultstate.Message = Messages.UnableToEditUser;
                resultstate.ThrownException = ex;
                return resultstate;
            }

            return resultstate;
        }

        public async Task<ResultState> DeleteUser(int userToDeleteId)
        {
            User userToDelete = await _usersRepository.GetById(userToDeleteId);

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
                await _usersRepository.Delete(userToDeleteId);
                return new ResultState(true, Messages.UserDeletedSuccessfull);
            }
            catch (Exception)
            {
                return new ResultState(false, Messages.UnableToDeleteUser);
            }
        }

    }
}
