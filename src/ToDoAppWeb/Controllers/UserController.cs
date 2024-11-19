using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models.DTO.Requests;
using ToDoApp.Models.DTO.Responses;
using ToDoApp.Models.Users;
using ToDoApp.Services.UserService;
using ToDoAppWeb.KafkaProducer;

namespace ToDoAppWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;

        public UserController(IUserService userService) : base()
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserWithRoleRequestDTO user)
        {
            User userToAdd = new User
            {
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
            };

            var resultState = await _userService.Create(userToAdd);

            if (resultState.IsSuccessful)
            {

                return Ok(resultState.Message);
            }
            else
            {
                return BadRequest(resultState.Message);
            }

        }
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllAsAdmin()
        {
            var users = await _userService.GetAll();

            List<UserResponseDTO> usersResponse = new List<UserResponseDTO>();

            foreach (var user in users)
            {
                usersResponse.Add(new UserResponseDTO()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role,
                    AddedOn = user.AddedOn,
                    EditedOn = user.EditedOn,
                });
            }
            return usersResponse;
        }

        [HttpGet]
        [Route("getAllForEvent")]
        public async Task<ActionResult<List<UserEventResponseDTO>>> GetAllForEvent()
        {
            var users = await _userService.GetAll();

            var usersList = new List<UserEventResponseDTO>();

            foreach (var user in users)
            {
                usersList.Add(new UserEventResponseDTO()
                {
                    FullName = user.FirstName + " " + user.LastName,
                    Email = user.Email
                });
            }

            return usersList;
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task<ActionResult> Edit(int userId, UserWithRoleRequestDTO user)
        {
            User userToEdit = new User
            {
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
            };

            var resultState = await _userService.EditUser(userId, userToEdit);

            if (resultState.IsSuccessful)
            {
                try
                {
                    var producer = new EventProducer();
                    await producer.Produce(userId.ToString(), userToEdit, "edit");
                }
                catch (System.Exception)
                {
                    throw;
                }
                return Ok(resultState.Message);
            }
            else
            {
                return BadRequest(resultState.Message);
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<ActionResult> Delete(int userId)
        {
            var resultState = await _userService.DeleteUser(userId);

            if (resultState.IsSuccessful)
            {
                try
                {
                    var producer = new EventProducer();
                    await producer.Produce(userId.ToString(), new User(), "delete");
                }
                catch (System.Exception)
                {
                    throw;
                }
                return Ok(resultState.Message);
            }
            else
            {
                return BadRequest(resultState.Message);
            }
        }
    }
}
