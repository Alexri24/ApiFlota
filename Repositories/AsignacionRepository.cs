using ApiFlota.Repositories;
using System.Data.SqlClient;
using ApiFlota.Models;


public class AsignacionRepository : IAsignacionRepository
    {
        private readonly string _connectionString;

        public AsignacionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiFlotaDB") ?? "Not found";
        }

        public async Task<List<Asignacion>> GetAllAsync()
        {
            var asignaciones = new List<Asignacion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, CamionId, ConductorId, FechaAsignacion, FechaFin, EstaActiva, Motivo, PrimaAsignacion FROM Asignacion";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var asignacion = new Asignacion
                            {
                                Id = reader.GetInt32(0),
                                CamionId = reader.GetInt32(1),
                                ConductorId = reader.GetInt32(2),
                                FechaAsignacion = reader.GetDateTime(3),
                                FechaFin = reader.GetDateTime(4),
                                EstaActiva = reader.GetBoolean(5),
                                Motivo = reader.GetString(6),
                                PrimaAsignacion = reader.Getdecimal (7)
                                
                            }; 

                            asignaciones.Add(Asignacion);
                        }
                    }
                }
            }
            return asignaciones;
        }

        public async Task<Asignacion> GetByIdAsync(int id)
        {
            Asignacion asignacion = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, CamionId, ConductorId, FechaAsignacion, FechaFin, EstaActiva, Motivo, PrimaAsignacion FROM Asignacion WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            asignacion = new Asignacion
                            {
                                Id = reader.GetInt32(0),
                                CamionId = reader.GetInt32(1),
                                ConductorId = reader.GetInt32(2),
                                FechaAsignacion = reader.GetDateTime(3),
                                FechaFin = reader.GetDateTime(4),
                                EstaActiva = reader.GetBoolean(5),
                                Motivo = reader.GetString(6),
                                PrimaAsignacion = reader.Getdecimal (7)
                            }; 

                        }
                    }
                }
            }
            return asignacion;
        }

        public async Task AddAsync(Asignacion asignacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Asignacion (CamionId, ConductorId, FechaAsignacion, FechaFin, EstaActiva, Motivo, PrimaAsignacion) VALUES (@CamionId, @ConductorId, @FechaAsignacion, @FechaFin, @EstaActiva, @Motivo, @PrimaAsignacion)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CamionId", asignacion.CamionId);
                    command.Parameters.AddWithValue("@ConductorId", asignacion.ConductorId);
                    command.Parameters.AddWithValue("@FechaAsignacion", asignacion.FechaAsignacion);
                    command.Parameters.AddWithValue("@FechaFin", asignacion.FechaFin);
                    command.Parameters.AddWithValue("@EstaActiva", asignacion.EstaActiva);
                    command.Parameters.AddWithValue("@PrimaAsignacion", asignacion.PrimaAsignacion);
                                       
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Asignacion asignacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Asignacion SET CamionId = @CamionId, ConductorId = @ConductorId, FechaAsignacion= @FechaAsignacion, FechaFin = @FechaFin, EstaActiva = @EstaActiva, Motivo = @Motivo, PrimaAsignacion = @PrimaAsignacion  WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", asignacion.Id);
                    command.Parameters.AddWithValue("@CamionId", asignacion.CamionId);
                    command.Parameters.AddWithValue("@ConductorId", asignacion.ConductorId);
                    command.Parameters.AddWithValue("@FechaAsignacion", asignacion.FechaAsignacion);
                    command.Parameters.AddWithValue("@FechaFin", asignacion.FechaFin);
                    command.Parameters.AddWithValue("@EstaActiva", asignacion.EstaActiva);
                    command.Parameters.AddWithValue("@PrimaAsignacion", asignacion.PrimaAsignacion);
                    



                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Asignacion WHERE Id = @Id";
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
                    INSERT INTO Asignacion ( FechaAsignacion, FechaFin, EstaActiva, Motivo, PrimaAsignacion)
                    VALUES 

                    (@FechaAsignacion1, @FechaFin1, @EstaActiva1, @Motivo1, @PrimaAsignacion1),
                    (@FechaAsignacion2, @FechaFin2, @EstaActiva2, @Motivo2, @PrimaAsignacion2)";
                using (var command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@FechaAsignacion1", 12/10/2025);
                    command.Parameters.AddWithValue("@FechaFin1", 13/11/2025);
                    command.Parameters.AddWithValue("@EstaActiva1", 0);
                    command.Parameters.AddWithValue("@Motivo1", "Ruta larga");
                    command.Parameters.AddWithValue("@PrimaAsignacion1", 1000);

                    command.Parameters.AddWithValue("@FechaAsignacion2", 12/11/2025);
                    command.Parameters.AddWithValue("@FechaFin2", 15/11/2025);
                    command.Parameters.AddWithValue("@EstaActiva2", 0);
                    command.Parameters.AddWithValue("@Motivo2", "Ruta corta");
                    command.Parameters.AddWithValue("@PrimaAsignacion2", 150);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

 