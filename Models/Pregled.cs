using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Pregled
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(10)]
        public string Mesec { get; set; }
        
        [JsonIgnore]
        public List<Veza> ZivotinjeVeterinari { get; set; }
    }
}