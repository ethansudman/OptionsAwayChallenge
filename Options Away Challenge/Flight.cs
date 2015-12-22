using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Options_Away_Challenge
{
    [DataContract]
    public sealed class Flight
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public string FlightNumber { get; set; }

        [DataMember]
        public string Airline { get; set; }

        [DataMember]
        public string Origin { get; set; }

        [DataMember]
        public string Destination { get; set; }

        [DataMember]
        public string cabin_class { get; set; }

        [DataMember]
        public string OptionDuration { get; set; }

        [DataMember]
        public DateTime Departure { get; set; }

        public Flight(string csvData)
        {
            // I don't completely care for this since if we reorder the CSV file it'll break this
            string[] split = csvData.Split(',');
            OrderId = int.Parse(split[0]);
            FlightNumber = split[1];
            Airline = split[2];
            Origin = split[3];
            Destination = split[4];
            cabin_class = split[5];
            OptionDuration = split[6];
            Departure = DateTime.Parse(split[7]);
        }

        public Flight(int OrderId, string FlightNumber, string Airline, string Origin, string Destination, string cabin_class, string OptionDuration, DateTime Departure)
        {
            this.OrderId = OrderId;
            this.FlightNumber = FlightNumber;
            this.Airline = Airline;
            this.Origin = Origin;
            this.Destination = Destination;
            this.cabin_class = cabin_class;
            this.OptionDuration = OptionDuration;
            this.Departure = Departure;
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                                OrderId,
                                FlightNumber,
                                Airline,
                                Origin,
                                Destination,
                                cabin_class,
                                OptionDuration,
                                Departure.ToString("yyyy-MM-dd HH:mm:ss"));
        } // End ToString method
    }
}
