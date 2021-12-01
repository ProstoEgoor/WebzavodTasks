using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task_2._1._2.ModelDB;

namespace Task_2._1._2 {
    class Program {

        static readonly string[] planes = { "AirbusA320", "Boeing-747", "ATR 72" };

        static readonly string[] towns = { "Москва", "Курумоч", "Владивосток", "Санкт-Петербург" };

        static readonly string connectionString = new ConnectionStringManager().ConnectionString;

        static void Main(string[] args) {
            AddCompanies(GetMockCompanies());
            AddPassengers(GetMockPassengers());

            var companies = GetCompanies();

            AddTrips(GetMockTrips(companies));

            var passengers = GetPassengers();
            var trips = GetTrips();

            AddPassInTrips(GetMockPassInTrips(trips, passengers));

            //var passInTrips = GetPassInTrips();

            UpdateTown("Курумоч", "Самара");

            DeleteCompanyTrips(companies[0]);


        }

        static void UpdateTown(string oldTown, string newTown) {
            AirlinesContext.UsingDBContext(context => {
                context.Database.ExecuteSqlInterpolated($"UPDATE dbo.Trip SET town_from = {newTown} WHERE town_from = {oldTown};");
                context.Database.ExecuteSqlInterpolated($"UPDATE dbo.Trip SET town_to = {newTown} WHERE town_to = {oldTown};");
                return true;
            }, connectionString);
        }

        static void DeleteCompanyTrips(Company company) {
            AirlinesContext.UsingDBContext(context => {
                context.Database.ExecuteSqlInterpolated($"DELETE FROM dbo.Pass_in_trip WHERE trip_no IN (SELECT trip_no FROM dbo.Trip WHERE ID_comp = {company.IdComp});");
                context.Database.ExecuteSqlInterpolated($"DELETE FROM dbo.Trip WHERE ID_comp = {company.IdComp};");
                return true;
            }, connectionString);
        }

        static void AddCompanies(Company[] companies) {
            AirlinesContext.UsingDBContext(context => {
                foreach (var company in companies) {
                    context.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.Company VALUES({company.Name});");
                }

                return true;
            }, connectionString);
        }

        static Company[] GetCompanies() {
            return AirlinesContext.UsingDBContext(context => {
                var companies = context.Companies.FromSqlRaw("SELECT * FROM dbo.Company;").ToArray();
                return companies;
            }, connectionString);
        }

        static Company[] GetMockCompanies() {
            var companies = new Company[3];

            companies[0] = new Company() { Name = "FlyZia" };
            companies[1] = new Company() { Name = "Jetling" };
            companies[2] = new Company() { Name = "Airbule" };

            return companies;
        }

        static void AddPassengers(Passenger[] passengers) {
            AirlinesContext.UsingDBContext(context => {
                foreach (var passenger in passengers) {
                    context.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.Passenger VALUES({passenger.Name});");
                }
                return true;
            }, connectionString);
        }

        static Passenger[] GetPassengers() {
            return AirlinesContext.UsingDBContext(context => {
                var passengers = context.Passengers.FromSqlRaw("SELECT * FROM dbo.Passenger;").ToArray();
                return passengers;
            }, connectionString);
        }

        static Passenger[] GetMockPassengers() {
            var passengers = new Passenger[5];

            passengers[0] = new Passenger { Name = "Филипп" };
            passengers[1] = new Passenger { Name = "Трофим" };
            passengers[2] = new Passenger { Name = "Прохор" };
            passengers[3] = new Passenger { Name = "Геннадий" };
            passengers[4] = new Passenger { Name = "Матильда" };

            return passengers;
        }

        static void AddTrips(Trip[] trips) {
            AirlinesContext.UsingDBContext(context => {
                foreach (var trip in trips) {
                    context.Database
                        .ExecuteSqlInterpolated($"INSERT INTO dbo.Trip VALUES({trip.IdComp}, {trip.Plane}, {trip.TownFrom}, {trip.TownTo}, {trip.TimeOut}, {trip.TimeIn});");
                }
                return true;
            }, connectionString);
        }

        static Trip[] GetTrips() {
            return AirlinesContext.UsingDBContext(context => {
                var trips = context.Trips.FromSqlRaw("SELECT * FROM dbo.Trip").ToArray();
                return trips;
            }, connectionString);
        }

        static Trip[] GetMockTrips(Company[] companies) {
            var trips = new Trip[7];

            Random random = new Random();
            DateTime start = new DateTime(2000, 1, 1);
            DateTime end = DateTime.Today;

            for (int i = 0; i < trips.Length; i++) {
                int townFromIndex = random.Next(0, 2);
                int townToIndex = random.Next(0, towns.Length);
                while (townFromIndex == townToIndex) townToIndex = random.Next(0, towns.Length);
                DateTime timeOut = start.AddMinutes(random.Next((int)(end - start).TotalMinutes));

                trips[i] = new Trip() {
                    IdComp = companies[random.Next(0, companies.Length)].IdComp,
                    Plane = planes[random.Next(0, planes.Length)],
                    TownFrom = towns[townFromIndex],
                    TownTo = towns[townToIndex],
                    TimeOut = timeOut,
                    TimeIn = timeOut.AddMinutes(random.Next(60, 300))
                };
            }

            return trips;
        }

        static void AddPassInTrips(PassInTrip[] passInTrips) {
            AirlinesContext.UsingDBContext(context => {
                foreach (var passInTrip in passInTrips) {
                    context.Database
                        .ExecuteSqlInterpolated($"INSERT INTO dbo.Pass_in_trip VALUES({passInTrip.TripNo}, {passInTrip.Date}, {passInTrip.IdPsg}, {passInTrip.Place});");
                }
                return true;
            }, connectionString);
        }

        static PassInTrip[] GetPassInTrips() {
            return AirlinesContext.UsingDBContext(context => {
                var passInTrips = context.PassInTrips.FromSqlRaw("SELECT * FROM dbo.Pass_in_trip").ToArray();
                return passInTrips;
            }, connectionString);
        }

        static PassInTrip[] GetMockPassInTrips(Trip[] trips, Passenger[] passengers) {
            var passInTrips = new List<PassInTrip>();

            Random random = new Random();

            foreach (var trip in trips) {
                int countPassengers = random.Next(1, passengers.Length);
                for (int j = 0; j < countPassengers; j++) {
                    passInTrips.Add(new PassInTrip() {
                        TripNo = trip.TripNo,
                        Date = trip.TimeIn.AddMinutes(-random.Next(30 * 24 * 60)),
                        IdPsg = passengers[j].IdPsg,
                        Place = $"{random.Next(1, 100):0#}{(char)('A' + random.Next(10))}",
                    });
                }
            }

            return passInTrips.ToArray();
        }
    }
}
