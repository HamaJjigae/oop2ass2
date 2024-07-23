using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless.Models
{
    public class FlightsObj
    {
        private string? flightCode;
        private string? airline;
        private string? departure = null;
        private string? arrival = null;
        private string? day = null;
        private string? time = null;
        private int? seats;
        private double? cost = null;

        public string? FlightCode
        {
            get => flightCode;
            set => flightCode = value;
        }
        public string? Airline
        {
            get => airline;
            set => airline = value;
        }

        public string? Departure
        {
            get => departure;
            set => departure = value;
        }

        public string? Arrival
        {
            get => arrival;
            set => arrival = value;
        }

        public string? Day
        {
            get => day;
            set => day = value;
        }

        public string? Time
        {
            get => time;
            set => time = value;
        }

        public int? Seats
        {
            get => seats;
            set => seats = value;
        }

        public double? Cost
        {
            get => cost;
            set => cost = value;
        }

        public FlightsObj() { }

        public FlightsObj(string flightCode, string airline, string departure, string arrival, string day, string time, int? seats, double? cost)
        {
            this.flightCode = flightCode;
            this.airline = airline;
            this.departure = departure;
            this.arrival = arrival;
            this.day = day;
            this.time = time;
            this.seats = seats;
            this.cost = cost;
        }

        public void DecrementSeats()
        {
            if (Seats.HasValue && Seats.Value > 0)
            {
                Seats--;
            }
            else
            {
                throw new InvalidOperationException("No seats available or seats value is invalid.");
            }
        }
        public void IncrementSeats()
        {
            Seats++;
        }
    }
}