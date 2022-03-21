using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Veterinar
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }
        [MaxLength(15)]
        public string BrojTelefona { get; set; } 
        [JsonIgnore]  
        public List<Veza> VeterinarZivotinja { get; set; }
        //Foreign key za tip struke veterinara 
        public TipStruke TipStrukeVeterinara { get; set; }
        
    }
}