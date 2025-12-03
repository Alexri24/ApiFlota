using ApiFlota.Repositories;
using System.Data.SqlClient;
using ApiFlota.Models;


public class CamionRepository : ICamionRepository
    {
        private readonly string _connectionString;

        public CamionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiFlotaDB") ?? "Not found";
        }

        public async Task<List<Camion>> GetAllAsync()
        {
            var camiones = new List<Camion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Matricula, Marca, Kilometraje, CapacidadCarga, EsOperativo, FechaCompra FROM Camion";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var camion = new Camion
                            {
                                Id = reader.GetInt32(0),
                                Marca = reader.GetString(1),
                                Kilometraje = reader.GetInt32(2),
                                CapacidadCarga = reader.GetDecimal(3),
                                EsOperativo = reader.GetBoolean(4),
                                FechaCompra = reader.GetDateTime (5)
                            }; 

                            camiones.Add(camion);
                        }
                    }
                }
            }
            return camiones;
        }

        public async Task<Camion> GetByIdAsync(int id)
        {
            Camion camion = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Matricula, Marca, Kilometraje, CapacidadCarga, EsOperativo, FechaCompra FROM Camion WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            camion = new Camion
                            {
                                Id = reader.GetInt32(0),
                                Marca = reader.GetString(1),
                                Kilometraje = reader.GetInt32(2),
                                CapacidadCarga = reader.GetDecimal(3),
                                EsOperativo = reader.GetBoolean(4),
                                FechaCompra = reader.GetDateTime (5)
                            };
                        }
                    }
                }
            }
            return camion;
        }

        public async Task AddAsync(Camion camion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Camion (Matricula, Marca, Kilometraje, CapacidadCarga, EsOperativo, FechaCompra) VALUES (@Matricula, @Marca, @Kilometraje, @CapacidadCarga, @EsOperativo, @FechaCompra)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Matricula", camion.Matricula);
                    command.Parameters.AddWithValue("@Marca", camion.Marca);
                    command.Parameters.AddWithValue("@Kilometraje", camion.Kilometraje);
                    command.Parameters.AddWithValue("@CapacidadCarga", camion.CapacidadCarga);
                    command.Parameters.AddWithValue("@EsOperativo", camion.EsOperativo);
                    command.Parameters.AddWithValue("@FechaCompra", camion.FechaCompra);
                    
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Camion camion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Camion SET Matricula = @Matricula, Marca = @Marca, Kilometraje = @Kilometraje, CapacidadCarga = @CapacidadCarga, EsOperativo = @EsOperativo, FechaCompra = @FechaCompra WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", camion.Id);
                    command.Parameters.AddWithValue("@Matricula", camion.Matricula);
                    command.Parameters.AddWithValue("@Marca", camion.Marca);
                    command.Parameters.AddWithValue("@Kilometraje", camion.Kilometraje);
                    command.Parameters.AddWithValue("@CapacidadCarga", camion.CapacidadCarga);
                    command.Parameters.AddWithValue("@EsOperativo", camion.EsOperativo);
                    command.Parameters.AddWithValue("@FechaCompra", camion.FechaCompra);




                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Camion WHERE Id = @Id";
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
                    INSERT INTO Camion (Matricula, Marca, Kilometraje, CapacidadCarga, EsOperativo, FechaCompra)
                    VALUES 

                    (@Matricula1, @Marca1, @Kilometraje1, @CapacidadCarga1, @EsOperativo1, @FechaCompra1),
                    (@Matricula2, @Marca2, @Kilometraje2, @CapacidadCarga2, @EsOperativo2, @FechaCompra2)";
                using (var command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@Matricula1", "1234ABC");
                    command.Parameters.AddWithValue("@Marca1", "Mercedes");
                    command.Parameters.AddWithValue("@Kilometraje1", 123456);
                    command.Parameters.AddWithValue("@CapacidadCarga1", 1200);
                    command.Parameters.AddWithValue("@EsOperativo1", 0);
                    command.Parameters.AddWithValue("@FechaCompra1", 10/10/2010);

                    command.Parameters.AddWithValue("@Matricula2", "5678DEF");
                    command.Parameters.AddWithValue("@Marca2", "Volvo");
                    command.Parameters.AddWithValue("@Kilometraje2", 1200);
                    command.Parameters.AddWithValue("@CapacidadCarga2", 1500);
                    command.Parameters.AddWithValue("@EsOperativo2", 1);
                    command.Parameters.AddWithValue("@FechaCompra2", 22/09/2012);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

