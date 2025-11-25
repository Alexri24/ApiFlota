using ApiFlota.Models;

public class Mantenimiento
{
    public int Id { get; set; }

    public string Tipo { get; set; }  
    

    public decimal Coste { get; set; }  

    public int HorasTrabajo { get; set; }  

    public bool EsPreventivo { get; set; } 

    public DateTime FechaProgramada { get; set; } 
    
    public string Descripcion { get; set; } 


    public int CamionId { get; set; }
    public Camion Camion { get; set; }
}