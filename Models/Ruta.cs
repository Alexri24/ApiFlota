using ApiFlota.Models;
public class Ruta
{
    
    public int Id { get; set; }

    public string Origen { get; set; } 
    public string Destino { get; set; } 

    public decimal DistanciaKm { get; set; } 

    public int Prioridad { get; set; } // (1=Baja, 5=Alta)

    public bool EsPrioritaria { get; set; } 

    public DateTime FechaInicio { get; set; } 


    public int CamionId { get; set; }
    public Camion Camion { get; set; } 
}