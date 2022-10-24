using DogApi.Models;
using Npgsql;
using NpgsqlTypes;
using Sakur.WebApiUtilities.Models;
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

        public async Task<List<DogBreed>> GetBreedsAsync()
        {
            List<DogBreed> listOfBreeds = new List<DogBreed>();
            string query = @"SELECT name, id FROM breed";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listOfBreeds.Add( new DogBreed()
                            {
                                Name = (string)reader["name"],
                                Id = (int)(long)reader["id"],
                            });
                        }
                    }
                }
            }

            return listOfBreeds;
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
                                Temperament = (string)reader["Temperament"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                const string query = @"INSERT INTO site_user (name, email, password, breed_id, created_date)
                                    VALUES (@name, @email, @password, @breedId, NOW())
                                    RETURNING id";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = user.Name.ToLower();
                    command.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = user.Email.ToLower();
                    command.Parameters.Add("@breedId", NpgsqlDbType.Integer).Value = user.BreedId;
                    command.Parameters.Add("@password", NpgsqlDbType.Varchar).Value = user.Password;

                    user.Id = (int)await command.ExecuteScalarAsync();

                    return user;
                }
            }
            catch (PostgresException postgresException)
            {
                if (postgresException.SqlState == "23505") //unique constraint violation
                    throw new ApiException("Email already registered", System.Net.HttpStatusCode.BadRequest);
                else
                    throw new ApiException(postgresException.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            User result = null;

            const string query = "SELECT id, name, email, password, breed_id, created_date, role FROM site_user WHERE name = @name";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                await connection.OpenAsync();

                command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = username.ToLower();

                using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result = User.FromReader(reader);
                        return result;
                    }
                }
            }

            return result;
        }
    }
}
