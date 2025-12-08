using ApiFlota.Repositories;
using System.Data.SqlClient;
using ApiFlota.Models;


public class MantenimientoRepository : IMantenimientoRepository
    {
        private readonly string _connectionString;

        public MantenimientoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApiFlotaDB") ?? "Not found";
        }

        public async Task<List<Mantenimiento>> GetAllAsync()
        {
            var mantenimientos = new List<Mantenimiento>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Tipo, Coste, HorasTrabajo, EsPreventivo, FechaProgramada, Descripcion FROM Mantenimiento";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var mantenimiento = new Mantenimiento
                            {
                                Id = reader.GetInt32(0),
                                Tipo = reader.GetString(1),
                                Coste = reader.GetDecimal(2),
                                HorasTrabajo = reader.GetInt32(3),
                                EsPreventivo = reader.GetBoolean(4),
                                FechaProgramada =reader.DateTime (5),
                                Descripcion = reader.DateString (6)
                            }; 

                            mantenimientos.Add(Mantenimiento);
                        }
                    }
                }
            }
            return mantenimientos;
        }

        public async Task<Mantenimiento> GetByIdAsync(int id)
        {
            Mantenimiento mantenimiento = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Tipo, Coste, HorasTrabajo, EsPreventivo, FechaProgramada, Descripcion FROM Mantenimiento WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            mantenimiento = new Mantenimiento
                            {
                                Id = reader.GetInt32(0),
                                Tipo = reader.GetString(1),
                                Coste = reader.GetDecimal(2),
                                HorasTrabajo = reader.GetInt32(3),
                                EsPreventivo = reader.GetBoolean(4),
                                FechaProgramada =reader.DateTime (5),
                                Descripcion = reader.DateString (6)
                            }; 

                        }
                    }
                }
            }
            return mantenimiento;
        }

        public async Task AddAsync(Mantenimiento mantenimiento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Mantenimiento (Tipo, Coste, HorasTrabajo, EsPreventivo, FechaProgramada, Descripcion) VALUES (@Tipo, @Coste, @HorasTrabajo, @EsPreventivo, @FechaProgramada, @Descripcion)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tipo", mantenimiento.Tipo);
                    command.Parameters.AddWithValue("@Coste", mantenimiento.Coste);
                    command.Parameters.AddWithValue("@HorasTrabajo", mantenimiento.HorasTrabajo);
                    command.Parameters.AddWithValue("@EsPreventivo", mantenimiento.EsPreventivo);
                    command.Parameters.AddWithValue("@FechaProgramada", mantenimiento.FechaProgramada);
                    command.Parameters.AddWithValue("@Descripcion", mantenimiento.Descripcion);
                                       
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Mantenimiento mantenimiento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Mantenimiento SET Tipo = @Tipo, Coste = @Coste, HorasTrabajo = @HorasTrabajo, EsPreventivo = @EsPreventivo, FechaProgramada = @FechaProgramada, Decripcion = @Decripcion  WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", mantenimiento.Id);
                    command.Parameters.AddWithValue("@Tipo", mantenimiento.Tipo);
                    command.Parameters.AddWithValue("@Coste", mantenimiento.Coste);
                    command.Parameters.AddWithValue("@HorasTrabajo", mantenimiento.HorasTrabajo);
                    command.Parameters.AddWithValue("@EsPreventivo", mantenimiento.EsPreventivo);
                    command.Parameters.AddWithValue("@FechaProgramada", mantenimiento.FechaProgramada);
                    command.Parameters.AddWithValue("@Descripcion", mantenimiento.Descripcion);
                    

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Mantenimiento WHERE Id = @Id";
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
                    INSERT INTO Mantenimiento (Tipo, Coste, HorasTrabajo, EsPreventivo, FechaProgramada, Descripcion)
                    VALUES 
                    (@Tipo1, @Coste1, @HorasTrabajo1, @EsPreventivo1, @FechaProgramada1, @Descripcion1),
                    (@Tipo2, @Coste2, @HorasTrabajo2, @EsPreventivo2, @FechaProgramada2, @Descripcion2),";
                using (var command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@Tipo1", "Cambio Aceite ");
                    command.Parameters.AddWithValue("@Coste1", 100);
                    command.Parameters.AddWithValue("@HorasTrabajo1", 2);
                    command.Parameters.AddWithValue("@EsPreventivo1", 1);
                    command.Parameters.AddWithValue("@FechaProgramada1", 23/11/2025);
                    command.Parameters.AddWithValue("@Descripcion1", "Cambio aceite");

                    command.Parameters.AddWithValue("@Tipo2", "Cambio Rueda ");
                    command.Parameters.AddWithValue("@Coste2", 1050);
                    command.Parameters.AddWithValue("@HorasTrabajo2", 25);
                    command.Parameters.AddWithValue("@EsPreventivo2", 0);
                    command.Parameters.AddWithValue("@FechaProgramada2", 24/01/2026);
                    command.Parameters.AddWithValue("@Descripcion2", "Cambio rueda por pinchazo");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

