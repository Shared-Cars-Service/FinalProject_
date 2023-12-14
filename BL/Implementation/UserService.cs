using AutoMapper;
using BL.DTO;
using BL.Interfaces;
using Dal.DataObject;
using Dal.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        IMapper mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        #region basic-CRUD
        public async Task<int> CreateAsync(UserDTO item)
        {
            User user = mapper.Map<User>(item);
            return await userRepository.CreateAsync(user);
        }
        public async Task<List<UserDTO>> ReadAllAsync()
        {
            List<User> usersLst = await userRepository.ReadAllAsync();
            List<UserDTO> usersDtoLst = mapper.Map<List<UserDTO>>(usersLst);
            return usersDtoLst;
        }
        public async Task<bool> UpdateAsync(UserDTO newItem)
        {
            User user = mapper.Map<User>(newItem);
            return await userRepository.UpdateAsync(user);
        }
        public async Task<bool> DeleteAsync(int code)
        {

            return await userRepository.DeleteAsync(code);
        }
        public async Task<UserDTO> ReadByIdAsync(int code)
        {
            User user = await userRepository.ReadByIdAsync(code);
            UserDTO userDTO = mapper.Map<UserDTO>(user);
            return userDTO;
        }
        #endregion

        public async Task<ActionResult<UserDTO>> ReadByPasswordAsync(string password,string name)
        {
            User user = await userRepository.ReadByPasswordAsync(password);
            UserDTO userDTO = mapper.Map<UserDTO>(user);
            if(userDTO != null && userDTO.Name.Equals(name))
            {
                return userDTO;
            }
            return new StatusCodeResult(StatusCodes.Status417ExpectationFailed);

        }






    }
}
