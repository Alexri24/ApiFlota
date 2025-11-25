using ApiFlota.Models;
public class Asignacion
{

    public int Id { get; set; }

    public int CamionId { get; set; } 
    public int ConductorId { get; set; }
    
    public DateTime FechaAsignacion { get; set; } 

    public DateTime? FechaFin { get; set; } 

    public bool EstaActiva { get; set; } 

    public string Motivo { get; set; } 
    public decimal PrimaAsignacion { get; set; } 

    
    public Camion Camion { get; set; }
    public Conductor Conductor { get; set; }
}    
 

