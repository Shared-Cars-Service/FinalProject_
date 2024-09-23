
using BL.DTO;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MyService.Controllers
{

    public class UserController : ServiceController
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<int> CreateUser([FromBody] string name, string email, string password)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Name = name;
            userDTO.Email = email;
            userDTO.Password = password;
            return await userService.CreateAsync(userDTO);
        }
        [HttpGet]
        public async Task<List<UserDTO>> GetUsers()
        {
            return await userService.ReadAllAsync();
        }
        [HttpPost]
        [Route("{name}")]
        public async Task<ActionResult<UserDTO>> GetUser([FromHeader] string password, string name)
        {

            return await userService.ReadByPasswordAsync(password, name);
        }

        [HttpPut]
        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            return await userService.UpdateAsync(userDTO);
        }
        [HttpDelete]
        public async Task<bool> DeleteUser(int id)
        {
            // UserDTO userDTO = await userService.ReadByIdAsync(id);
            return await userService.DeleteAsync(id);
        }
    }
}
