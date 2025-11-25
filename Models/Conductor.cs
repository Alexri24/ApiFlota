using ApiFlota.Models;
public class Conductor
{
    public int Id { get; set; }

    public string Nombre { get; set; } 

    public string Licencia { get; set; } 

    public int Antiguedad { get; set; } 

    public decimal SalarioBase { get; set; }  

    public bool EsInternacional { get; set; }  

    public DateTime FechaNacimiento { get; set; }  

    
    public List<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
}