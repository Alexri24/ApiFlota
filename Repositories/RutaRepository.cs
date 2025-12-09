using ApiFlota.Repositories;
using System.Data.SqlClient;
using ApiFlota.Models;


public class RutaRepository : IRutaRepository
    {
        private readonly string _connectionString;

        public RutaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiFlotaDB") ?? "Not found";
        }

        public async Task<List<Ruta>> GetAllAsync()
        {
            var rutas = new List<Ruta>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Origen, Destino, DistanciaKm, Prioridad, DuracionEstimada, FechaInicio FROM Ruta";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var ruta = new Ruta
                            {
                                Id = reader.GetInt32(0),
                                Origen = reader.GetString(1),
                                Destino = reader.GetString(2),
                                DistanciaKm = reader.GetDecimal(3),
                                Prioridad = reader.GetInt32(4),
                                DuracionEstimada =reader.GetInt32 (5),
                                FechaInicio = reader.GetDateTime (6)
                            }; 

                            rutas.Add(ruta);
                        }
                    }
                }
            }
            return rutas;
        }

        public async Task<Ruta> GetByIdAsync(int id)
        {
            Ruta ruta = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Origen, Destino, DistanciaKm, Prioridad, DuracionEstimada, FechaInicio FROM Ruta WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            ruta = new Ruta
                            {
                                Id = reader.GetInt32(0),
                                Origen = reader.GetString(1),
                                Destino = reader.GetString(2),
                                DistanciaKm = reader.GetDecimal(3),
                                Prioridad = reader.GetInt32(4),
                                DuracionEstimada =reader.GetInt32 (5),
                                FechaInicio = reader.GetDateTime (6)
                            }; 

                        }
                    }
                }
            } 
            return ruta;
        }

        public async Task AddAsync(Ruta ruta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Ruta (Origen, Destino, DistanciaKm, Prioridad, DuracionEstimada, FechaInicio) VALUES (@Origen, @Destino, @DistanciaKm, @Prioridad, @DuracionEstimada, @FechaInicio)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Origen", ruta.Origen);
                    command.Parameters.AddWithValue("@Destino", ruta.Destino);
                    command.Parameters.AddWithValue("@DistanciaKm", ruta.DistanciaKm);
                    command.Parameters.AddWithValue("@Prioridad", ruta.Prioridad);
                    command.Parameters.AddWithValue("@DuracionEstimada", ruta.DuracionEstimada);
                    command.Parameters.AddWithValue("@FechaInicio", ruta.FechaInicio);
                                       
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Ruta ruta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Ruta SET Origen = @Origen, Destino = @Destino, DistanciaKm = @DistanciaKm, Prioridad = @Prioridad, DuracionEstimada = @DuracionEstimada, FechaInicio = @FechaInicio  WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", ruta.Id);
                    command.Parameters.AddWithValue("@Origen", ruta.Origen);
                    command.Parameters.AddWithValue("@Destino", ruta.Destino);
                    command.Parameters.AddWithValue("@DistanciaKm", ruta.DistanciaKm);
                    command.Parameters.AddWithValue("@Prioridad", ruta.Prioridad);
                    command.Parameters.AddWithValue("@DuracionEstimada", ruta.DuracionEstimada);
                    command.Parameters.AddWithValue("@FechaInicio", ruta.FechaInicio);
                                       
                    

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Ruta WHERE Id = @Id";
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
                    INSERT INTO Ruta (Origen, Destino, DistanciaKm, Prioridad, DuracionEstimada, FechaInicio)
                    VALUES 
                    (@Origen1, @Destino1, @DistanciaKm1, @Prioridad1, @DuracionEstimada1, @FechaInicio1),
                    (@Origen2, @Destino2, @DistanciaKm2, @Prioridad2, @DuracionEstimada2, @FechaInicio2),";
                using (var command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@Origen1", "Zaragoza ");
                    command.Parameters.AddWithValue("@destino1", "Madrid");
                    command.Parameters.AddWithValue("@DistanciaKm1", 300);
                    command.Parameters.AddWithValue("@Prioridad1", 2);
                    command.Parameters.AddWithValue("@DuracionEstimada1", 3);
                    command.Parameters.AddWithValue("@FechaInicio1", 08/12/2025);

                    command.Parameters.AddWithValue("@Origen2", "Madrid ");
                    command.Parameters.AddWithValue("@destino2", "Barcelona");
                    command.Parameters.AddWithValue("@DistanciaKm2", 550);
                    command.Parameters.AddWithValue("@Prioridad2", 5);
                    command.Parameters.AddWithValue("@DuracionEstimada2", 5.5);
                    command.Parameters.AddWithValue("@FechaInicio2", 12/12/2025);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

