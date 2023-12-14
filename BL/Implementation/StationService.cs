using AutoMapper;
using BL.DTO;
using BL.Interfaces;
using Dal.DataObject;
using Dal.Interfaces;
using GoogleMaps.LocationServices;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation
{
    public class StationService : IStationService
    {
        IStationRepository stationRepository;
        IMapper mapper;
        public StationService(IStationRepository stationRepository, IMapper mapper)
        {
            this.stationRepository = stationRepository;
            this.mapper = mapper;
        }
        #region basic CRUD

        public async Task<int> CreateAsync(StationDTO item)
        {
            Station station = mapper.Map<Station>(item);
            return await stationRepository.CreateAsync(station);
        }

        public async Task<List<StationDTO>> ReadAllAsync()
        {
           List<Station> lst = await stationRepository.ReadAllAsync();
            return mapper.Map<List<StationDTO>>(lst);
        }

        public async Task<StationDTO> ReadByIdAsync(int id)
        {
            Station station = await stationRepository.ReadByIdAsync(id);
            return mapper.Map<StationDTO>(station);
        }

        public async Task<bool> UpdateAsync(StationDTO stationDTO)
        {
            Station newStation = mapper.Map<Station>(stationDTO);
            return await stationRepository.UpdateAsync(newStation);
        }
        public async Task<bool> DeleteAsync(int code)
        {
            return await stationRepository.DeleteAsync(code);
        }
        #endregion
        public async Task<StationDTO> GetNearestStation(StationDTO stationDTO)
            {
                Point point = convertStationDTOToPoint(stationDTO);
                // I want the nearest station that is:
                // full (fullStation = true)
                // does not matter centeral or not (isMustCenteral = false)
                Station station = await stationRepository.GetNearestStation(true, false, point, stationDTO.Street, stationDTO.Neighborhood, stationDTO.City);
                return mapper.Map<StationDTO>(station);
            }
            public async Task<StationDTO> GetLucrativeStation(int numberOfRentalDays, StationDTO stationDTO)
            {
                double discount = PriceDetermination.Discount();
                double price_per_day = PriceDetermination.Price_per_day();
                double normal_wolking_distance_in_km = PriceDetermination.Normal_wolking_distance_in_km();
                double avg_price_of_taxi_fare_for_km = PriceDetermination.Avg_price_of_taxi_fare_for_km();
                double initial_state_of_counter = PriceDetermination.Initial_state_of_counter();
                Station lucrativeStation = new Station();
                Point point = convertStationDTOToPoint(stationDTO);
                // I want the nearest station that is:
                // empty (fullStation = false)
                // centeral (isMustCenteral = true)
                Station nearestCenteralStation = await stationRepository.GetNearestStation(false, true, point, stationDTO.Street, stationDTO.Neighborhood, stationDTO.City);
                //if it within walking distance - return it
                double distance = (double)GetDistance(point, new Point() { X = nearestCenteralStation.X, Y = nearestCenteralStation.Y });
                if (distance <= normal_wolking_distance_in_km)
                {
                    lucrativeStation = nearestCenteralStation;
                }
                //or if average taxi fare is less than the discount - return it
                else if (initial_state_of_counter + (distance * avg_price_of_taxi_fare_for_km) <= (double)discount * numberOfRentalDays * price_per_day)
                {
                    lucrativeStation = nearestCenteralStation;
                }
                //else - return the nearest station
                //that empty and not centeral
                else
                {
                    lucrativeStation = await stationRepository.GetNearestStation(false, false, point, stationDTO.Street, stationDTO.Neighborhood, stationDTO.City);
                }
                return mapper.Map<StationDTO>(lucrativeStation);
            }
            private static double GetDistance(Point point1, Point point2)
            {
                return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
            }

            const string API_KEY = "AIzaSyBFwHxGY47K0J1ECt99_TZA7aVO62ztUp0";
            const string BASE_URL = "https://maps.googleapis.com/maps/api/geocode/json";
            private Point convertStationDTOToPoint(StationDTO stationDTO)
            {
                StationDTO locationFromGoogleMaps;
                Point point = new Point();
                string country = "IL";
                string url = BASE_URL + "?address=" + stationDTO.Street + "st" + stationDTO.City + "&components==country:" + country + "&key=" + API_KEY;
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseJson = reader.ReadToEnd();
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(responseJson);
                    if (result.status == "OK")
                    {
                        point.X = result.results[0].geometry.location.lat;
                        point.Y = result.results[0].geometry.location.lng;
                        //the next lines will change the object stationDTO, because the function got it as a reference type

                        //stationDTO.Number=result.results[0].geometry.number;
                        stationDTO.Number = 0;
                        stationDTO.Street = result.results[0].address_components[0].short_name;
                        //stationDTO.Neighborhood = result.results[0].address_components[1].short_name;
                        stationDTO.Neighborhood = null;
                        stationDTO.City = result.results[0].address_components[1].short_name;

                        //locationFromGoogleMaps = new StationDTO (result[1]???????????,result[0].?????,???,???);
                    }
                }
                catch (WebException)
                {
                    //until our seminary will open google maps service:)
                    point.X = 3.333;
                    point.Y = 3.333;
                    stationDTO.Number = 51;
                    stationDTO.Street = "Chazon-Ish";
                    stationDTO.Neighborhood = "Ramat-Shlomo";
                    stationDTO.City = "Jerusalem";
                }
                return point;
            }


        }
    } 
