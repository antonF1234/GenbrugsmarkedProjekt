namespace GenbrugsmarkedProjekt.Models;

public class Annonce
{
    [BsonId] public ObjectId Id { get; set; }           
    public int AnnonceId { get; set; }                      

    // Annonce info 
    public string Titel { get; set; } = "";
    public string Beskrivelse { get; set; } = "";
    public decimal Pris { get; set; }
    public string Stand { get; set; } = "God";      
    public string Str { get; set; } = "";              
    public string FotoUrl { get; set; } = "";

    // Status & flow 
    public string Status { get; set; } = "Active";      
    public DateTime Dato { get; set; } = DateTime.Now;

    // Bruger 
    public int BrugerId { get; set; }
    public string BrugerNavn { get; set; } = "";
    public string BrugerEmail { get; set; } = "";
    

    // Lokation 
    public string Lokalitet { get; set; } = "EAAA";     
    public string? Lokale { get; set; }
}