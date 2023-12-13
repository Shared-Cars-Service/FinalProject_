using BL.DTO;
using BL.Interfaces;
using BL.profiles;
using Dal.DataObject;
using Microsoft.AspNetCore.Mvc;

namespace MyService.Controllers
{
    public class StationController : ServiceController
    {
        IStationService stationService;
        public StationController(IStationService stationService)
        {
            this.stationService = stationService;
        }
        [HttpGet]
        public async Task<StationDTO> GetNearestStation(int num, string street, string neighborhood, string city)
        {
            StationDTO stationDTO = new StationDTO(num, street, neighborhood, city);
            return await stationService.GetNearestStation(stationDTO);
        }
        [HttpGet]
        [Route("{numOfRentalHours}")]
        public async Task<StationDTO> FindLucrativeStation(int numOfRentalHours, int num, string street, string neighborhood, string city)
        {
            StationDTO stationDTO = new StationDTO(num, street, neighborhood, city);
            return await stationService.GetLucrativeStation(numOfRentalHours, stationDTO);
        }
        [HttpGet]
        [Route("{getAll}, {getAll2}")]
        public async Task<List<StationDTO>> GetAllAsync(string getAll = null, string getAll2 = null)
        {
            return await stationService.ReadAllAsync();
        }
        //can not do the next lines because it is the same route to the function FindLucrativeStation
        
        //[HttpGet]
        //[Route("{id}")]
        //public async Task<Station>GetByIdAsync (int id)
        //{
        //    return await stationService.ReadByIdAsync(id);
        //}

        [HttpPost]
        public async Task<int> CreateAsync(int num , string street , string neighborhood , string city)
        {
            StationDTO stationDTO = new StationDTO(num , street , neighborhood , city);
            return await stationService.CreateAsync(stationDTO);
        }
        [HttpPut]
        public async Task<bool> UpdateAsync (StationDTO stationDTO)
        {
            return await stationService.UpdateAsync(stationDTO);
        }
        [HttpDelete]
        public async Task<bool> DeleteAsync (int id)
        {
            return await stationService.DeleteAsync(id);  
        }
 
    }
}

