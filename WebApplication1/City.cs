using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public class City
    {
        public int Id { get; set; }

        [JsonPropertyName("district")]
        public string District { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("population")]
        public int Population { get; set; }
        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("coords")]
        public Coords Coords { get; set; }

        public int CoordsId { get; set; }

        private static MySqlConnection _connection;
        private const string ConnectionString = "server=localhost;port=3336;user=root;database=database;password=1234;";

        public static List<City> GetAllCity()
        {
            _connection = new MySqlConnection(ConnectionString);
            _connection.Open();

            var sql = "SELECT * FROM City LEFT JOIN Coords ON City.coords_id = Coords.id";
            var command = new MySqlCommand(sql, _connection);
            var reader = command.ExecuteReader();
            var cities = new List<City>();
            while(reader.Read())
            {
                var courseId = reader.GetInt16("coords_id");
                var city = new City
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    District = reader.GetString("district"),
                    Population = reader.GetInt32("population"),
                    Subject = reader.GetString("subject"),
                    CoordsId = courseId,
                    Coords = new Coords
                    {
                        Lat = reader.GetString("lat"),
                        Lon = reader.GetString("lon"),
                        Id = courseId
                    }
                };
                cities.Add(city);
            }
            reader.Close();
            _connection.Close();
            return cities;
        }

        public static City GetCityById(int id)
        {
            _connection = new MySqlConnection(ConnectionString);
            _connection.Open();

            var sql = $"SELECT * FROM City LEFT JOIN Coords ON City.coords_id = Coords.id WHERE City.id = {id}";
            var command = new MySqlCommand(sql, _connection);
            var reader = command.ExecuteReader();
            reader.Read();
            var courseId = reader.GetInt16("coords_id");
            var city = new City
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                District = reader.GetString("district"),
                Population = reader.GetInt32("population"),
                Subject = reader.GetString("subject"),
                CoordsId = courseId,
                Coords = new Coords
                {
                    Lat = reader.GetString("lat"),
                    Lon = reader.GetString("lon"),
                    Id = courseId
                }
            };
            reader.Close();
            _connection.Close();
            return city;
        }
    }
}
