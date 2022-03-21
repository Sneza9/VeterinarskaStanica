using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class TipStruke
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Tip { get; set; }
        [JsonIgnore]  
        public List<Veterinar> TipVeterinara { get; set; }

    }
}