using Dal.DataObject;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Dal.DataObject.Point;

namespace Dal.Implemention
{
    internal class StationRepository : IStationRepository
    {
        General general;
        public StationRepository(General general)
        {
            this.general = general;
        }
        #region basic-CRUD
        //in create station we have to check:
        //if the station street is exsist in the streets table so the station streetId is its id
        //but if not we have to add new street in the streets table
        //in neughbirhood and city is like this
        public async Task<int> CreateAsync(Station station)
        {
            var newStation = general.Stations.Add(station);
            //int id = -1;
            //id = FindStreetAsync(station.Street.Name);
            //if(id == -1)
            //{
            //    general.Streets.Add(station.Street);
            //}
            await general.SaveChangesAsync();
            return newStation.Entity.Id;
        }
        private int FindStreetAsync (string street)
        {
            return 0;
        }
        private int FindNeighborhoodAsync(string street)
        {
            return 0;
        }
        private int FindCityAsync(string street)
        {
            return 0;
        }

        public async Task<bool> DeleteAsync(int stationId)
        {
            try
            {
                Station? station = general.Stations.Find(stationId);
                if (station != null)
                {
                    general.Stations.Remove(station);
                    await general.SaveChangesAsync();
                }
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Station>> ReadAllAsync()
        {
            return await general.Stations.Include(station => station.Street)
                .ThenInclude(street => street.Neigborhood)
                 .ThenInclude(nei => nei.City)
                 .ToListAsync<Station>();

        }

        public async Task<Station?> ReadByIdAsync(int code)
        {
            return await general.Stations.FindAsync(code);
        }

        public async Task<bool> UpdateAsync(Station newItem)
        {
            try
            {
                general.Stations.Update(newItem);
                await general.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        public async Task<Station> GetNearestStation(bool fullStation,bool isMustCenteral, Point point1, string street, string neighbornhood, string city)
        {
            List<Station> stationList;
            //try to check if there are empty or full station (depend at fullStation)
            //not matter centeral or not in the same street
            stationList = await GetStationsByStreet(fullStation, isMustCenteral, street);
            if (stationList == null)
            {
                //if there are no appropriate station in this street,
                //try to find in the same neighborhood
                stationList = await GetStationsByNeighborhood(fullStation, isMustCenteral, neighbornhood);
                if (stationList == null)
                {
                    //if there are no appropriate station in this neighborhood,
                    //try to find in the same city
                    stationList = await GetStationsByCity(fullStation, isMustCenteral, city);
                    if (stationList == null)
                    {
                        //if there are no appropriate station in this city find all 
                        // empty stations or full stations
                        //(depend on fullStation)
                        if (fullStation)
                            stationList = await GetAllFullStations();
                        else
                            stationList = await GetAllEmptyStations();
                    }
                }
            }
            return await FindNearestStationFromList(point1, stationList);
        }
        #region acsess to DB
        
        public async Task<List<Station>> GetStationsByStreet(bool fullStation, bool isMustCenteral, string street)
        {
            Street str = await general.Streets.Where(s => s.Name.Equals(street)).FirstAsync();
            if (str == null)
                return null;
            int streetId = str.Id;
            if (isMustCenteral)
                return await general.Stations.Where(s => (s.StreetId == streetId && s.StationToCars.First().CarId != null) == fullStation && s.IsCenteral == true).ToListAsync();
            return await general.Stations.Where(s => (s.StreetId == streetId && s.StationToCars.First().CarId != null) == fullStation).ToListAsync();
        }
        public async Task<List<Station>> GetStationsByNeighborhood(bool fullStation, bool isMustCenteral, string neighborhood)
        {
            Neighborhood nbr = await general.Neighborhoods.Where(n => n.Name.Equals(neighborhood)).FirstOrDefaultAsync();
            if (nbr == null)
                return null;
            int neighborhoodId = nbr.Id;
            if (isMustCenteral)
                return await general.Stations.Where(s => (s.Street.NeigborhoodId == neighborhoodId && s.StationToCars.First().CarId != null) == fullStation && s.IsCenteral == true).ToListAsync();
            return await general.Stations.Where(s => (s.Street.NeigborhoodId == neighborhoodId && s.StationToCars.First().CarId != null) == fullStation).ToListAsync();
        }
        public async Task<List<Station>> GetStationsByCity(bool fullStation, bool isMustCenteral, string city)
        {
            City ct = await general.Cities.Where(c => c.Name.Equals(city)).FirstOrDefaultAsync();
            if (ct == null)
                return null;
            int cityId = ct.Id;
            if (isMustCenteral)
                return await general.Stations.Where(s => (s.Street.Neigborhood.CityId == cityId && s.StationToCars.First().CarId != null) == fullStation && s.IsCenteral == true).ToListAsync();
            return await general.Stations.Where(s => (s.Street.Neigborhood.CityId == cityId && s.StationToCars.First().CarId != null) == fullStation).ToListAsync();
        }
        public async Task<List<Station>> GetAllFullStations()
        {
            return await general.Stations.Include(station => station.Street).ThenInclude(street => street.Neigborhood).ThenInclude(nei => nei.City).Where(st => st.StationToCars.First().CarId != null).ToListAsync<Station>();
        }
        public async Task<List<Station>> GetAllEmptyStations()
        {
            return await general.Stations.Include(station => station.Street).ThenInclude(street => street.Neigborhood).ThenInclude(nei => nei.City).Where(st => st.StationToCars.First().CarId == null).ToListAsync<Station>();
        }
        #endregion
        private async Task<Station> FindNearestStationFromList(Point point1, List<Station> stationList)
        {
            Station nearestStation = new Station();
            double distance, minDistance = double.MaxValue;
            Point point2;
            foreach (var st in stationList)
            {
                point2 = new Point() { X = st.X, Y = st.Y };
                distance = GetDistance(point1, point2);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    nearestStation = st;
                }
            }
            return nearestStation;
        }
        private static double GetDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }
    }
}
