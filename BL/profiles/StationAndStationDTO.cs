using AutoMapper;
using BL.DTO;
using Dal.DataObject;
using Dal.Interfaces;
//using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GoogleMapsApi;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using System.Net;

namespace BL.profiles
{
    public class StationAndStationDTO : Profile
    {
        // {  .ForMember(d => d.Id, o => o.MapFrom(s => s.AppUserId))
        //.ForMember(d => d.UserName, o => o.MapFrom(s => s.AppUser.UserName))
        //.ForMember(d => d.DateJoined, o => o.MapFrom(s => s.DateJoined));

        public StationAndStationDTO()
        {
            //CreateMap<Station, StationDTO>().ForMember(station => station.Street, o => o.MapFrom(s => s.Street.Name))
            //.ForMember(n => n.Neighborhood, o => o.MapFrom(s => s.Street.Neigborhood.Name))
            //.ForMember(c => c.City, o => o.MapFrom(s => s.Street.Neigborhood.City.Name))
            //.ConstructUsing((src, ctx) => new StationDTO(
            //    src.Id,
            //    src.Number.Value,
            //    src.Street.Name,
            //    src.Street.Neigborhood.Name,
            //    src.Street.Neigborhood.City.Name))
            //.ReverseMap();

            CreateMap<Station, StationDTO>()
    .ForMember(station => station.Street, o => o.MapFrom(s => s.Street.Name))
    .ForMember(n => n.Neighborhood, o => o.MapFrom(s => s.Street.Neigborhood.Name)) // שגיאת כתיב
    .ForMember(c => c.City, o => o.MapFrom(s => s.Street.Neigborhood.City.Name))
    .ConstructUsing((src, ctx) => new StationDTO(
        src.Id,
        src.Number.HasValue ? src.Number.Value : 0, // טיפול במקרה ש-Number הוא null
        src.Street.Name,
        src.Street.Neigborhood.Name,
        src.Street.Neigborhood.City.Name))
    .ReverseMap();


            //      CreateMap<Station, StationDTO>()
            //      .ForMember(station => station.Street, o => o.MapFrom(s => s.Street.Name))
            //    .ForMember(n => n.Neighborhood, o => o.MapFrom(s => s.Street.Neigborhood.Name))
            //.ForMember(c => c.City, o => o.MapFrom(s => s.Street.Neigborhood.City.Name))
            //.ConstructUsing((src, ctx) => new StationDTO(
            //    src.Id,
            //    src.Number.HasValue ? src.Number.Value : 0, // או ערך ברירת מחדל אחר
            //    src.Street.Name,
            //    src.Street.Neigborhood.Name,
            //    src.Street.Neigborhood.City.Name));

            //  .ConstructUsing(s => new StationDTO());
        }
    }
}
//ForMember(station => station.Street, opt => opt.MapFrom(src => src.Street.Name)
