using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using Dal.DataObject;

namespace BL.profiles
{
    internal class CarAndCarDTO : Profile
    {
        public CarAndCarDTO()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
        }
    }
}
