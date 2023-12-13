using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DataObject;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Implemention
{
    public class CarRepository : ICarRepository
    {
        General general;
        public CarRepository(General general)
        {
            this.general = general;
        }
        public async Task<int> CreateAsync(Car item)
        {
            var newCar = general.Cars.Add(item);
            await general.SaveChangesAsync();
            return newCar.Entity.Id;
        }

        public async Task<bool> DeleteAsync(int carId)
        {
            try
            {
                // Step 1: Update the StationToCar table to set CarId to NULL
                var stationToCarEntries = general.StationToCars.Where(stc => stc.CarId == carId);
                foreach (var entry in stationToCarEntries)
                {
                    entry.CarId = null;
                }

                // Step 2: Remove the car from the Cars table
                Car car = general.Cars.Find(carId);
                general.Cars.Remove(car);

                await general.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Car>> ReadAllAsync()
        {
            return await general.Cars.ToListAsync<Car>();
        }
        public async Task<Car> ReadByIdAsync(int code)
        {
            return await general.Cars.FirstAsync(c => c.Id == code);
        }

        public Task<bool> UpdateAsync(Car newItem)
        {
            //details of car will never change
            throw new NotImplementedException();
        }
        public async Task<bool> ChangeTheCarModeAsync(int carId)
        {
            try
            {
                Car car = await general.Cars.FirstOrDefaultAsync(c => c.Id == carId);
                if (car != null)
                {
                    car.IsAvailable = !car.IsAvailable; // Toggle the availability
                    await general.SaveChangesAsync();
                    return true; // Update successful
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the update
                Console.WriteLine($"An error occurred while toggling the car availability: {ex.Message}");
                return false; // Update failed
            }

        }
    }
}
