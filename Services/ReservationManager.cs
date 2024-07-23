using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Traveless.Models;

namespace Traveless.Services
{
    public class ReservationManager
    {
        private readonly List<MakeReservation> _reservations;

        public ReservationManager(List<MakeReservation> reservations)
        {
            _reservations = reservations;
        }
        public List<MakeReservation> Reservations => _reservations;

        public void AddReservation(MakeReservation reservation)
        {
            _reservations.Add(reservation);
        }
        public void ReservationUpdate(MakeReservation existingReservation, string? newName, string? newCitizenship, bool? newStatus)
        {
            var reservation = _reservations.FirstOrDefault(r => r.ReservationCode == existingReservation.ReservationCode);
            if (reservation != null)
            {
                reservation.Name = newName ?? reservation.Name;
                reservation.Citizenship = newCitizenship ?? reservation.Citizenship;
                reservation.Status = newStatus ?? reservation.Status;

                Persist();
            }
            else
            {
                throw new Exception("Reservation not found.");
            }
        }

        public void Persist()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_reservations, options);

                using (var stream = new FileStream("reservations.json", FileMode.Create, FileAccess.Write))
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving reservations: {ex.Message}");
            }
        }
        public void Load()
        {
            try
            {
                if (File.Exists("reservations.json"))
                {
                    using (var stream = new FileStream("reservations.json", FileMode.Open, FileAccess.Read))
                    using (var reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEnd();
                        var reservations = JsonSerializer.Deserialize<List<MakeReservation>>(json);
                        if (reservations != null)
                        {
                            _reservations.Clear();
                            _reservations.AddRange(reservations);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading reservations: {ex.Message}");
            }
        }

            public string GenerateCode()
        {
            var random = new Random();
            string code;
            do
            {
                code = $"{(char)('A' + random.Next(0, 26))}{random.Next(1000, 10000)}";
            } while (_reservations.Any(r => r.ReservationCode == code));

            return code;
        }
    }
}
