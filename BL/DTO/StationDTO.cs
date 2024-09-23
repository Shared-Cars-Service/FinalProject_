using Dal.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class StationDTO
    {
        public int StationID { get; set; }
        public int Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        //להוסיף בDAL את NUMBER
        public StationDTO(int stationId, int number, string street, string neighborhood, string city)
        {
            this.StationID = stationId;
            this.Number = number;
            this.Neighborhood = neighborhood ?? "Unknown";
            this.Street = street ?? "Unknown";
            this.City = city ?? "Unknown";
        }
        public StationDTO(int number, string street, string neighborhood, string city)
        {
            this.Number = number;
            this.Neighborhood = neighborhood ?? "Unknown" ;
            this.Street = street ?? "Unknown";
            this.City = city ?? "Unknown";
        }
        //public StationDTO(string street, string neighborhood, string city)
        //{
        //    this.Neighborhood = neighborhood;
        //    this.Street = street;
        //    this.City = city;
        //}
    }
}
