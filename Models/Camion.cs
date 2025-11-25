namespace ApiFlota.Models;

public class Camion
{
    public int Id { get; set; }

    public string Matricula { get; set; } 

    public string Marca { get; set; } 

    public int Kilometraje { get; set; } 

    public decimal CapacidadCarga { get; set; } 

    public bool EsOperativo { get; set; } = true; 

    public DateTime FechaCompra { get; set; } 

    
    // Relación 1: Un camión tiene muchos Mantenimientos
    public List<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    // Relación 2: Un camión puede tener muchas Asignaciones (Historial de conductores)
    public List<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();

    // Relación 3: Un camión puede ser parte de muchas Rutas
    public List<Ruta> Rutas { get; set; } = new List<Ruta>();
}