using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.Interfaces;
using Dal.DalImplements;
using Dal.DataObject;
using Dal.Interfaces;

namespace BL.Implementation
{
    public class CarService : ICarService
    {
        ICarRepository carRepository;
        //auto mapper
        IMapper mapper;
        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(CarDTO item)
        {
            Car car = mapper.Map<Car>(item);
            return await carRepository.CreateAsync(car);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await carRepository.DeleteAsync(id);
        }

        public async Task<List<CarDTO>> ReadAllAsync()
        {
            List<Car> carsLst = await carRepository.ReadAllAsync();
            List<CarDTO> carsDtoLst = mapper.Map<List<CarDTO>>(carsLst);
            return carsDtoLst;
        }
        public async Task<CarDTO> ReadByIdAsync(int id)
        {
            Car car = await carRepository.ReadByIdAsync(id);
            CarDTO carDTO = mapper.Map<CarDTO>(car);
            return carDTO;
        }

        public Task<bool> UpdateAsync(CarDTO newItem)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> ChangeCarModeAsync(int carId)
        {
            return await carRepository.ChangeCarModeAsync(carId);
        }
    }
}
