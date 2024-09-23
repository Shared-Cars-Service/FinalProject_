using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using Dal.DataObject;

namespace BL.Interfaces
{
    public interface ICarService : IService<CarDTO>
    {
        public Task<bool> ChangeCarModeAsync(int carId);
    }
}
