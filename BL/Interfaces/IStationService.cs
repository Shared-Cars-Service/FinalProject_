using BL.DTO;
using Dal.DataObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IStationService : IService<StationDTO>
    {
        public Task<StationDTO> GetNearestStation(StationDTO stationDTO);
        public Task<StationDTO> GetLucrativeStation(int numOfRentalHours, StationDTO stationDTO);
        public Task<bool> ChangeCarInStationMode(int stationId);
    }
}
