using DogApi.Models;
using Npgsql;
using WebApiUtilities.Helpers;

namespace DogApi.Managers
{
    public class DatabaseManager
    {
        private string connectionString;
        public DatabaseManager()
        {
            string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            connectionString = ConnectionStringHelper.GetConnectionStringFromUrl(connectionUrl);
        }
        public async Task<DogBreed> GetBreedByIdAsync(long id) // Om man inte vill att UI ska frysa medan funktionen väntar på ett anrop
        {
            string query = @"SELECT * FROM ""Breed"" WHERE ""Id"" = @id";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Bigint).Value = id;
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            return new DogBreed()
                            {
                                Exercise = (string)reader["Exercise"],
                                Grooming = (string)reader["Grooming"],
                                Name = (string)reader["Name"],
                                Temperament = (string)reader["Temperament"],
                                Size = Size.Create((int)reader["MinHeight"], (int)reader["MaxHeight"], (int)reader["MinWeight"], (int)reader["MaxWeight"]), // TODO
                                Image = (string)reader["Image"]
                            };
                        }
                    }
                }
            }
        }
    }
}
