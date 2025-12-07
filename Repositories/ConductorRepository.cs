using ApiFlota.Repositories;
using System.Data.SqlClient;
using ApiFlota.Models;


public class ConductorRepository : IConductorRepository
    {
        private readonly string _connectionString;

        public ConductorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiFlotaDB") ?? "Not found";
        }

        public async Task<List<Conductor>> GetAllAsync()
        {
            var conductores = new List<Conductor>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Licencia, Antiguedad, SalarioBase, EsInternacional, FechaNacimiento FROM Conductor";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var conductor = new Conductor
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Licencia = reader.GetString32(2),
                                Antiguedad = reader.GetInt32(3),
                                SalarioBase= reader.decimal(4),
                                EsInternacional = reader.GetBoolean (5),
                                FechaNacimiento = reader.DateTime (6)
                            }; 

                            conductores.Add(Conductor);
                        }
                    }
                }
            }
            return conductores;
        }

        public async Task<Conductor> GetByIdAsync(int id)
        {
            Conductor conductor = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Licencia, Antiguedad, SalarioBase, EsInternacional, FechaNacimiento FROM Conductor WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            conductor = new Conductor
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Licencia = reader.GetString32(2),
                                Antiguedad = reader.GetInt32(3),
                                SalarioBase= reader.decimal(4),
                                EsInternacional = reader.GetBoolean (5),
                                FechaNacimiento = reader.DateTime (6)
                            }; 

                        }
                    }
                }
            }
            return conductor;
        }

        public async Task AddAsync(Conductor conductor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Conductor (Nombre, Licencia, Antiguedad, SalarioBase, EsInternacional, FechaNacimiento) VALUES (@Nombre, @Licencia, @Antiguedad, @SalarioBase, @EsInternacional, @FechaNacimiento)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", conductor.Nombre);
                    command.Parameters.AddWithValue("@Licencia", conductor.Licencia);
                    command.Parameters.AddWithValue("@Antiguedad", conductor.Antiguedad);
                    command.Parameters.AddWithValue("@SalarioBase", conductor.SalarioBase);
                    command.Parameters.AddWithValue("@EsInternacional", camion.EsInternacional);
                    command.Parameters.AddWithValue("@FechaNacimiento", camion.FechaNacimiento);
                                       
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Conductor conductor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Conductor SET Nombre = @Nombre, Licencia = @Licencia, Antiguedad = @Antiguedad, SalarioBase = @SalarioBase, EsInternacional = @EsInternacional, FechaNacimiento = @FechaNacimiento  WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Conductor.Id);
                    command.Parameters.AddWithValue("@Nombre", conductor.Nombre);
                    command.Parameters.AddWithValue("@Licencia", conductor.Licencia);
                    command.Parameters.AddWithValue("@Antiguedad", conductor.Antiguedad);
                    command.Parameters.AddWithValue("@SalarioBase", conductor.SalarioBase);
                    command.Parameters.AddWithValue("@EsInternacional", camion.EsInternacional);
                    command.Parameters.AddWithValue("@FechaNacimiento", camion.FechaNacimiento);
                    



                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Conductor WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Comando SQL para insertar datos iniciales
                var query = @"
                    INSERT INTO Conductor (Nombre, Licencia, Antiguedad, SalarioBase, EsInternacional, FechaNacimiento)
                    VALUES 

                    (@Nombre1, @Licencia1, @Antiguedad1, @SalarioBase1, @EsInternacional1, @FechaNacimiento1),
                    (@Nombre2, @Licencia2, @Antiguedad2, @SalarioBase2, @EsInternacional2, @FechaNacimiento2)";
                using (var command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@Nombre1", "Alejandro");
                    command.Parameters.AddWithValue("@Licencia1", "C1");
                    command.Parameters.AddWithValue("@Antiguedad1", "3 años");
                    command.Parameters.AddWithValue("@SalarioBase1", 1900);
                    command.Parameters.AddWithValue("@EsInternacional1", 1);
                    command.Parameters.AddWithValue("@FechaNacimiento1", 10/10/1085);

                    command.Parameters.AddWithValue("@Nombre2", "Jorge");
                    command.Parameters.AddWithValue("@Licencia2", "C");
                    command.Parameters.AddWithValue("@Antiguedad2", "4 años");
                    command.Parameters.AddWithValue("@SalarioBase2", 1500);
                    command.Parameters.AddWithValue("@EsInternacional2", 1);
                    command.Parameters.AddWithValue("@FechaNacimiento2", 12/05/2000);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

