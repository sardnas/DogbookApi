using Newtonsoft.Json;
using Npgsql;
using System;

namespace DogApi.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
        public int BreedId { get; set; }

        public User() { }

        public User(string name, string email, string password, int breedId, DateTime createdDate)
        {
            Name = name;
            Email = email;
            Password = password;
            BreedId = breedId;
            CreatedDate = createdDate;
        }

        public User(string name, string email, string password, int breedId, DateTime createdDate, int id)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            BreedId = breedId;
            CreatedDate = createdDate;
        }

        public static User FromReader(NpgsqlDataReader reader)
        {
            int? role = reader["role"] as int?;

            return new User()
            {
                Name = reader["name"] as string,
                Email = reader["email"] as string,
                Password = reader["password"] as string,
                BreedId = (reader["breed_id"] as int?) ?? 0, // om det är null blir det 0
                CreatedDate = (DateTime)reader["created_date"],
                Id = (int)reader["id"],
                Role = (role ?? 0).ToString()
            };
        }
    }
}
