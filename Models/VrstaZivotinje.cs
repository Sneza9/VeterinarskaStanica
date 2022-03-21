using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class VrstaZivotinje
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(40)]
        public string Vrsta { get; set; }
        [JsonIgnore]  
        public List<Zivotinja> VrstaZivotinja { get; set; }

    }
}