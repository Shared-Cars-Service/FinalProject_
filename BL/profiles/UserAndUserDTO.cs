using AutoMapper;
using BL.DTO;
using Dal.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.profiles
{
    internal class UserAndUserDTO : Profile
    {
        public UserAndUserDTO()
        {
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
